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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Platform;

public class RemotePlayer
{
    public ulong remoteUserID;
    public bool stillInRoom;

    // the result of the last connection state update message
    public PeerConnectionState p2pConnectionState;
    // the last reported state of the VOIP connection
    public PeerConnectionState voipConnectionState;

    public OvrAvatar RemoteAvatar;

    // the last received root transform position updates, equivalent to local tracking space transform
    public Vector3 receivedRootPosition;

    // the previous received positions to interpolate from
    public Vector3 receivedRootPositionPrior;

    // the last received root transform rotation updates, equivalent to local tracking space transform
    public Quaternion receivedRootRotation;

    // the previous received rotations to interpolate from
    public Quaternion receivedRootRotationPrior;

    // the voip tracker for the player
    public VoipAudioSourceHiLevel voipSource;
}
