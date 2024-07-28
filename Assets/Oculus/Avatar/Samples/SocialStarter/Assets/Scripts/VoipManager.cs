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

using Oculus.Platform;
using Oculus.Platform.Models;

// Helper class to manage the Voice-over-IP connection to the
// remote users
public class VoipManager
{
    public VoipManager()
    {
        Voip.SetVoipConnectRequestCallback(VoipConnectRequestCallback);
        Voip.SetVoipStateChangeCallback(VoipStateChangedCallback);
    }

    public void ConnectTo(ulong userID)
    {
        // ID comparison is used to decide who initiates and who gets the Callback
        if (SocialPlatformManager.MyID < userID)
        {
            Voip.Start(userID);
            SocialPlatformManager.LogOutput("Voip connect to " + userID);
        }
    }


    public void Disconnect(ulong userID)
    {
        if (userID != 0)
        {
            Voip.Stop(userID);

            RemotePlayer remote = SocialPlatformManager.GetRemoteUser(userID);
            if (remote != null)
            {
                remote.voipConnectionState = PeerConnectionState.Unknown;
            }
        }
    }

    void VoipConnectRequestCallback(Message<NetworkingPeer> msg)
    {
        SocialPlatformManager.LogOutput("Voip request from " + msg.Data.ID);

        RemotePlayer remote = SocialPlatformManager.GetRemoteUser(msg.Data.ID);
        if (remote != null)
        {
            SocialPlatformManager.LogOutput("Voip request accepted from " + msg.Data.ID);
            Voip.Accept(msg.Data.ID);
        }
    }

    void VoipStateChangedCallback(Message<NetworkingPeer> msg)
    {
        SocialPlatformManager.LogOutput("Voip state to " + msg.Data.ID + " changed to  " + msg.Data.State);

        RemotePlayer remote = SocialPlatformManager.GetRemoteUser(msg.Data.ID);
        if (remote != null)
        {
            remote.voipConnectionState = msg.Data.State;

            // ID comparison is used to decide who initiates and who gets the Callback
            if (msg.Data.State == PeerConnectionState.Timeout && SocialPlatformManager.MyID < msg.Data.ID)
            {
                // keep trying until hangup!
                Voip.Start(msg.Data.ID);
                SocialPlatformManager.LogOutput("Voip re-connect to " + msg.Data.ID);
            }
        }
    }
}
