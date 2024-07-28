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
using System;
using Oculus.Avatar;

public abstract class OvrAvatarDriver : MonoBehaviour
{

    public enum PacketMode
    {
        SDK,
        Unity
    };

    public PacketMode Mode;
    protected PoseFrame CurrentPose;
    public PoseFrame GetCurrentPose() { return CurrentPose; }
    public abstract void UpdateTransforms(IntPtr sdkAvatar);

    private ovrAvatarControllerType ControllerType = ovrAvatarControllerType.Quest;
    public struct ControllerPose
    {
        public ovrAvatarButton buttons;
        public ovrAvatarTouch touches;
        public Vector2 joystickPosition;
        public float indexTrigger;
        public float handTrigger;
        public bool isActive;

        public static ControllerPose Interpolate(ControllerPose a, ControllerPose b, float t)
        {
            return new ControllerPose
            {
                buttons = t < 0.5f ? a.buttons : b.buttons,
                touches = t < 0.5f ? a.touches : b.touches,
                joystickPosition = Vector2.Lerp(a.joystickPosition, b.joystickPosition, t),
                indexTrigger = Mathf.Lerp(a.indexTrigger, b.indexTrigger, t),
                handTrigger = Mathf.Lerp(a.handTrigger, b.handTrigger, t),
                isActive = t < 0.5f ? a.isActive : b.isActive,
            };
        }
    }

    public struct PoseFrame
    {
        public Vector3 headPosition;
        public Quaternion headRotation;
        public Vector3 handLeftPosition;
        public Quaternion handLeftRotation;
        public Vector3 handRightPosition;
        public Quaternion handRightRotation;
        public float voiceAmplitude;

        public ControllerPose controllerLeftPose;
        public ControllerPose controllerRightPose;

        public static PoseFrame Interpolate(PoseFrame a, PoseFrame b, float t)
        {
            return new PoseFrame
            {
                headPosition = Vector3.Lerp(a.headPosition, b.headPosition, t),
                headRotation = Quaternion.Slerp(a.headRotation, b.headRotation, t),
                handLeftPosition = Vector3.Lerp(a.handLeftPosition, b.handLeftPosition, t),
                handLeftRotation = Quaternion.Slerp(a.handLeftRotation, b.handLeftRotation, t),
                handRightPosition = Vector3.Lerp(a.handRightPosition, b.handRightPosition, t),
                handRightRotation = Quaternion.Slerp(a.handRightRotation, b.handRightRotation, t),
                voiceAmplitude = Mathf.Lerp(a.voiceAmplitude, b.voiceAmplitude, t),
                controllerLeftPose = ControllerPose.Interpolate(a.controllerLeftPose, b.controllerLeftPose, t),
                controllerRightPose = ControllerPose.Interpolate(a.controllerRightPose, b.controllerRightPose, t),
            };
        }
    };

    void Start()
    {
        var headsetType = OVRPlugin.GetSystemHeadsetType();
        switch (headsetType)
        {
            case OVRPlugin.SystemHeadset.Oculus_Quest:
            case OVRPlugin.SystemHeadset.Rift_S:
                ControllerType = ovrAvatarControllerType.Quest;
                break;
            case OVRPlugin.SystemHeadset.Rift_DK1:
            case OVRPlugin.SystemHeadset.Rift_DK2:
            case OVRPlugin.SystemHeadset.Rift_CV1:
            default:
                ControllerType = ovrAvatarControllerType.Touch;
                break;
        }
    }

    public void UpdateTransformsFromPose(IntPtr sdkAvatar)
    {
        if (sdkAvatar != IntPtr.Zero)
        {
            ovrAvatarTransform bodyTransform = OvrAvatar.CreateOvrAvatarTransform(CurrentPose.headPosition, CurrentPose.headRotation);
            ovrAvatarHandInputState inputStateLeft = OvrAvatar.CreateInputState(OvrAvatar.CreateOvrAvatarTransform(CurrentPose.handLeftPosition, CurrentPose.handLeftRotation), CurrentPose.controllerLeftPose);
            ovrAvatarHandInputState inputStateRight = OvrAvatar.CreateInputState(OvrAvatar.CreateOvrAvatarTransform(CurrentPose.handRightPosition, CurrentPose.handRightRotation), CurrentPose.controllerRightPose);

            CAPI.ovrAvatarPose_UpdateBody(sdkAvatar, bodyTransform);
            CAPI.ovrAvatarPose_UpdateHandsWithType(sdkAvatar, inputStateLeft, inputStateRight, ControllerType);
        }
    }

    public static bool GetIsTrackedRemote()
    {
        return false;
    }
}
