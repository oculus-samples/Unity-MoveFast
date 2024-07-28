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
using System.Collections;
using System.Collections.Generic;
using System;
using Oculus.Avatar;
public class OvrAvatarBody : OvrAvatarComponent
{
    public ovrAvatarBodyComponent component = new ovrAvatarBodyComponent();

    public ovrAvatarComponent? GetNativeAvatarComponent()
    {
        if (owner == null)
        {
            return null;
        }

        if (CAPI.ovrAvatarPose_GetBodyComponent(owner.sdkAvatar, ref component))
        {
            CAPI.ovrAvatarComponent_Get(component.renderComponent, true, ref nativeAvatarComponent);
            return nativeAvatarComponent;
        }

        return null;
    }

    void Update()
    {
        if (owner == null)
        {
            return;
        }

        if (CAPI.ovrAvatarPose_GetBodyComponent(owner.sdkAvatar, ref component))
        {
            UpdateAvatar(component.renderComponent);
        }
        else
        {
            owner.Body = null;
            Destroy(this);
        }
    }
}
