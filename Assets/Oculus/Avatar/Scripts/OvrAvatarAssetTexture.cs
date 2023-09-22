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

using System;
using Oculus.Avatar;
using UnityEngine;

public class OvrAvatarAssetTexture : OvrAvatarAsset
{
    public Texture2D texture;
    private const int ASTCHeaderSize = 16;

    public OvrAvatarAssetTexture(UInt64 _assetId, IntPtr asset) {
        assetID = _assetId;
        ovrAvatarTextureAssetData textureAssetData = CAPI.ovrAvatarAsset_GetTextureData(asset);
        TextureFormat format;
        IntPtr textureData = textureAssetData.textureData;
        int textureDataSize = (int)textureAssetData.textureDataSize;

        AvatarLogger.Log(
            "OvrAvatarAssetTexture - "
            + _assetId
            + ": "
            + textureAssetData.format.ToString()
            + " "
            + textureAssetData.sizeX
            + "x"
            + textureAssetData.sizeY);

        switch (textureAssetData.format)
        {
            case ovrAvatarTextureFormat.RGB24:
                format = TextureFormat.RGB24;
                break;
            case ovrAvatarTextureFormat.DXT1:
                format = TextureFormat.DXT1;
                break;
            case ovrAvatarTextureFormat.DXT5:
                format = TextureFormat.DXT5;
                break;
            case ovrAvatarTextureFormat.ASTC_RGB_6x6:
#if UNITY_2020_1_OR_NEWER
                format = TextureFormat.ASTC_6x6;
#else
                format = TextureFormat.ASTC_RGB_6x6;
#endif
                textureData = new IntPtr(textureData.ToInt64() + ASTCHeaderSize);
                textureDataSize -= ASTCHeaderSize;
                break;
            case ovrAvatarTextureFormat.ASTC_RGB_6x6_MIPMAPS:
#if UNITY_2020_1_OR_NEWER
                format = TextureFormat.ASTC_6x6;
#else
                format = TextureFormat.ASTC_RGB_6x6;
#endif
                break;
            default:
                throw new NotImplementedException(
                    string.Format("Unsupported texture format {0}",
                                  textureAssetData.format.ToString()));
        }
        texture = new Texture2D(
            (int)textureAssetData.sizeX, (int)textureAssetData.sizeY,
            format, textureAssetData.mipCount > 1,
            QualitySettings.activeColorSpace == ColorSpace.Gamma ? false : true)
        {
            filterMode = FilterMode.Trilinear,
            anisoLevel = 4,
        };
        texture.LoadRawTextureData(textureData, textureDataSize);
        texture.Apply(true, false);
    }
}
