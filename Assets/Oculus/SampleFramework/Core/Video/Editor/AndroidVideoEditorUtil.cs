/*
 * Copyright (c) Meta Platforms, Inc. and affiliates.
 * All rights reserved.
 *
 * Licensed under the Oculus SDK License Agreement (the "License");
 * you may not use the Oculus SDK except in compliance with the License,
 * which is provided at the time of installation or download, or which
 * otherwise accompanies this software in either electronic or hard copy form.
 *
 * You may obtain a copy of the License at
 *
 * https://developer.oculus.com/licenses/oculussdk/
 *
 * Unless required by applicable law or agreed to in writing, the Oculus SDK
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System.Collections.Generic;
using UnityEditor;
using System.IO;

public class AndroidVideoEditorUtil
{
    private static readonly string videoPlayerFileName = "Assets/Oculus/SampleFramework/Core/Video/Plugins/Android/java/com/oculus/videoplayer/NativeVideoPlayer.java";
    private static readonly string disabledPlayerFileName = videoPlayerFileName + ".DISABLED";

#if !UNITY_2018_2_OR_NEWER
    private static readonly string gradleSourceSetPath = "$projectDir/../../Assets/Oculus/SampleFramework/Core/Video/Plugins/Android/java";
#endif

    private static readonly string audio360PluginPath = "Assets/Oculus/SampleFramework/Core/Video/Plugins/Android/Audio360/audio360.aar";
    private static readonly string audio360Exo29PluginPath = "Assets/Oculus/SampleFramework/Core/Video/Plugins/Android/Audio360/audio360-exo29.aar";

    [MenuItem("Oculus/Samples/Video/Enable Native Android Video Player")]
    public static void EnableNativeVideoPlayer()
    {
        // rename NativeJavaPlayer.java.DISABLED to NativeJavaPlayer.java
        if (File.Exists(disabledPlayerFileName))
        {
            File.Move(disabledPlayerFileName, videoPlayerFileName);
            File.Move(disabledPlayerFileName + ".meta", videoPlayerFileName + ".meta");
        }

        AssetDatabase.ImportAsset(videoPlayerFileName);
        AssetDatabase.DeleteAsset(disabledPlayerFileName);

        // Enable audio plugins
        PluginImporter audio360 = (PluginImporter)AssetImporter.GetAtPath(audio360PluginPath);
        PluginImporter audio360exo29 = (PluginImporter)AssetImporter.GetAtPath(audio360Exo29PluginPath);

        if (audio360 != null && audio360exo29 != null)
        {
            audio360.SetCompatibleWithPlatform(BuildTarget.Android, true);
            audio360exo29.SetCompatibleWithPlatform(BuildTarget.Android, true);
            audio360.SaveAndReimport();
            audio360exo29.SaveAndReimport();
        }

        // Enable gradle build with exoplayer
        Gradle.Configuration.UseGradle();
        var lines = Gradle.Configuration.ReadLines();
        Gradle.Dependencies.AddDependency("com.google.android.exoplayer:exoplayer", "2.9.5", lines);
        Gradle.Configuration.WriteLines(lines);
    }

    [MenuItem("Oculus/Samples/Video/Disable Native Android Video Player")]
    public static void DisableNativeVideoPlayer()
    {
        if (File.Exists(videoPlayerFileName))
        {
            File.Move(videoPlayerFileName, disabledPlayerFileName);
            File.Move(videoPlayerFileName + ".meta", disabledPlayerFileName + ".meta");
        }

        AssetDatabase.ImportAsset(disabledPlayerFileName);
        AssetDatabase.DeleteAsset(videoPlayerFileName);

        // Disable audio plugins
        PluginImporter audio360 = (PluginImporter)AssetImporter.GetAtPath(audio360PluginPath);
        PluginImporter audio360exo29 = (PluginImporter)AssetImporter.GetAtPath(audio360Exo29PluginPath);

        if (audio360 != null && audio360exo29 != null)
        {
            audio360.SetCompatibleWithPlatform(BuildTarget.Android, false);
            audio360exo29.SetCompatibleWithPlatform(BuildTarget.Android, false);
            audio360.SaveAndReimport();
            audio360exo29.SaveAndReimport();
        }

        // remove exoplayer and sourcesets from gradle file (leave other parts since they are harmless).
        if (Gradle.Configuration.IsUsingGradle())
        {
            var lines = Gradle.Configuration.ReadLines();
            Gradle.Dependencies.RemoveDependency("com.google.android.exoplayer:exoplayer", lines);
            Gradle.Dependencies.RemoveSourceSet("sourceSets.main.java.srcDir", lines);
            Gradle.Configuration.WriteLines(lines);
        }

    }
}
