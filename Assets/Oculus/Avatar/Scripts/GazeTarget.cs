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
using UnityEngine;
using Oculus.Avatar;

public class GazeTarget : MonoBehaviour
{
    public ovrAvatarGazeTargetType Type;
    private static ovrAvatarGazeTargets RuntimeTargetList;

    static GazeTarget()
    {
        // This size has to match the 'MarshalAs' attribute in the ovrAvatarGazeTargets declaration.
        RuntimeTargetList.targets = new ovrAvatarGazeTarget[128];
        RuntimeTargetList.targetCount = 1;
    }

    void Start()
    {
        UpdateGazeTarget();
        transform.hasChanged = false;
    }

    void Update()
    {
        if (transform.hasChanged)
        {
            transform.hasChanged = false;
            UpdateGazeTarget();
        }
    }

    void OnDestroy()
    {
        UInt32[] targetIds = new UInt32[1];
        targetIds[0] = (UInt32)transform.GetInstanceID();
        CAPI.ovrAvatar_RemoveGazeTargets(1, targetIds);
    }

    private void UpdateGazeTarget()
    {
        ovrAvatarGazeTarget target = CreateOvrGazeTarget((UInt32)transform.GetInstanceID(), transform.position, Type);
        RuntimeTargetList.targets[0] = target;
        CAPI.ovrAvatar_UpdateGazeTargets(RuntimeTargetList);
    }

    private ovrAvatarGazeTarget CreateOvrGazeTarget(UInt32 targetId, Vector3 targetPosition, ovrAvatarGazeTargetType targetType)
    {
        return new ovrAvatarGazeTarget
        {
            id = targetId,
            // Do coordinate system switch.
            worldPosition = new Vector3(targetPosition.x, targetPosition.y, -targetPosition.z),
            type = targetType
        };
    }
}
