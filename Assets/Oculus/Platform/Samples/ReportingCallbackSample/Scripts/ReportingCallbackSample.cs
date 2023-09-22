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

// Uncomment this if you have the Touch controller classes in your project
//#define USE_OVRINPUT

using System;
using Oculus.Platform;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * This class shows a very simple way to integrate the Reporting Callback
 * by registering a listener and responding to the notification
 */
public class ReportingCallbackSample : MonoBehaviour
{
  public Text InVRConsole;
  public Text DestinationsConsole;

  // Start is called before the first frame update
  void Start()
  {
    UpdateConsole("Init Oculus Platform SDK...");
    Core.AsyncInitialize().OnComplete(message => {
      if (message.IsError)
      {
        // Init failed, nothing will work
        UpdateConsole(message.GetError().Message);
      }
      else
      {
        UpdateConsole("Init complete!");

        /**
         * Listen for when user clicks AUI report button
         */
        AbuseReport.SetReportButtonPressedNotificationCallback(OnReportButtonIntentNotif);
        UpdateConsole("Registered callback");
      }
    });
  }

  // User has interacted with the AUI outside this app
  void OnReportButtonIntentNotif(Message<string> message)
  {
    if (message.IsError)
    {
      UpdateConsole(message.GetError().Message);
    } else
    {
      UpdateConsole("Send that response is handled");
      AbuseReport.ReportRequestHandled(ReportRequestResponse.Handled);
      UpdateConsole("Response has been handled!");
    }
  }

  #region Helper Functions

  private void UpdateConsole(string value)
  {
    Debug.Log(value);

    InVRConsole.text =
      "Welcome to the Sample Reporting Callback App\n\n" + value;
  }
  #endregion
}
