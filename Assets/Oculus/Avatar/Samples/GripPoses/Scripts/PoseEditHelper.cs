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

public class PoseEditHelper : MonoBehaviour
{

    public Transform poseRoot;

    void OnDrawGizmos()
    {
        if (poseRoot != null)
        {
            DrawJoints(poseRoot);
        }
    }

    private void DrawJoints(Transform joint)
    {
        Gizmos.DrawWireSphere(joint.position, 0.005f);
        for (int i = 0; i < joint.childCount; ++i)
        {
            Transform child = joint.GetChild(i);
            if (child.name.EndsWith("_grip") || child.name.EndsWith("hand_ignore"))
            {
                continue;
            }
            Gizmos.DrawLine(joint.position, child.position);
            DrawJoints(child);
        }
    }
}
