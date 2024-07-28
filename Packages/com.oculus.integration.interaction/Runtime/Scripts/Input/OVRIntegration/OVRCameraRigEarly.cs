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

using UnityEngine;

namespace Oculus.Interaction.Input
{
    /// <summary>
    /// A subclass of OVRCameraRig that overrides the execution order to be early (-1).
    /// If not updating on FixedUpdate, this allows device updates to be handled
    /// at the start of the frame.
    /// </summary>
    [DefaultExecutionOrder(-1)]
    public class OVRCameraRigEarly : OVRCameraRig
    {
        public static OVRCameraRig Replace(OVRCameraRig rig)
        {
            return rig;
            /*
            OVRCameraRigEarly earlyRig = rig.gameObject.AddComponent<OVRCameraRigEarly>();
            earlyRig.Copy(rig);
            Destroy(rig);
            return earlyRig;
            */
        }

        public void Copy(OVRCameraRig rig)
        {
            usePerEyeCameras = rig.usePerEyeCameras;
            useFixedUpdateForTracking = rig.useFixedUpdateForTracking;
            disableEyeAnchorCameras = rig.disableEyeAnchorCameras;
        }
    }
}
