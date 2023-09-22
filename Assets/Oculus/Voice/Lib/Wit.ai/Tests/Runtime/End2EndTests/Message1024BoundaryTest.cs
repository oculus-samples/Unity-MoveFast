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
using Facebook.WitAi.CallbackHandlers;
using Facebook.WitAi.Lib;
using NUnit.Framework;
using UnityEngine;

namespace Facebook.WitAi.Tests
{
    public class Message1024BoundaryTest : ConnectionTest
    {
        [SerializeField] public string testEndpoint;
        [SerializeField] public string testName;
        [SerializeField] public ExpectedResult[] expectedResults;
        public override string Name => testName;
        public override string TestEndpoint
        {
            get
            {
                return testEndpoint;
            }
            set
            {
                testEndpoint = value;
            }
        }

        protected override void OnExecute(Wit wit)
        {
            wit.ActivateImmediately();
        }

        public override void OnResponse(WitResponseNode response)
        {
            for (int i = 0; i < expectedResults.Length; i++)
            {
                var pathRef = WitResultUtilities.GetWitResponseReference(expectedResults[i].path);
                var value = pathRef.GetStringValue(response);
                var expected = expectedResults[i].value;
                
                if (value != expected)
                {
                    TestFailed($"Expected: {expected}, got: {value}");
                    return;
                }
            }

            TestPassed();
        }
    }

    [Serializable]
    public class ExpectedResult
    {
        public string path;
        public string value;
    }
}
