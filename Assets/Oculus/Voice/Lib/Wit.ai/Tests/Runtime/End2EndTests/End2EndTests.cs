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
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Facebook.WitAi;
using Facebook.WitAi.Data.Configuration;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Facebook.WitAi.Tests
{
    [TestFixture]
    public class End2EndTests
    {
        //  TODO: update This test suit so that it can run on release output.
        private static string configPath = @"Packages/com.facebook.witai/Tests/Runtime/WitConfiguration.asset";
        private WitConfiguration witConfig = AssetDatabase.LoadAssetAtPath<WitConfiguration>(configPath);
        // TODO: these hard-coded paths should be configurable or programatically retreived.
        // TODO: Write an environment sanity check that verifies node is installed as part of the test setup phase.
        string pathToNodeExe = @"C:\Program Files\nodejs\node";
        string pathToTestServerScript = @"C:\open\ovrsource\arvr\assistant\OculusAssistant\TestScripts\voicesdk-testserver.js";

        private Process _process;
        public IEnumerator TestConnection(ConnectionTest test)
        {
            LogAssert.ignoreFailingMessages = true;  // To prevent failing a unit test when Debug.LogError() happens.
                                                     // Here, we expect to get the error codes and have them logged but we want our test
                                                     // to pass. So we ignore them here.
            var witGameObject = new GameObject("WitGO");
            // Add Wit component
            Wit wit = witGameObject.AddComponent<Wit>();
            // Setup wit component
            wit.RuntimeConfiguration.witConfiguration = witConfig;

            yield return SetupServer();

            // Now server is up and running. Do you tests here.
            yield return RunTest(wit, test);
            bool testSuccess = test.Success;
            CloseServer(_process);
            // Note: Do not destroy all gameObjects because Unity Test Framework
            // creates a special GameObject taking care of unit tests.
            GameObject.Destroy(witGameObject);

            Assert.IsTrue(testSuccess);  // Note: Any after a failing Assert statement is not executed! So it should be the last statement.      

        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest, Timeout(10000)]
        public IEnumerator End2EndTests_TestErrorCode_400()
        {
            var testErrorCode = new TestErrorCode
            {
                code = 400
            };
            yield return TestConnection(testErrorCode);
        }


        [UnityTest, Timeout(10000)]
        public IEnumerator End2EndTests_TestErrorCode_500()
        {
            var testErrorCode = new TestErrorCode
            {
                code = 500
            };
            yield return TestConnection(testErrorCode);

        }

        [UnityTest, Timeout(10000)]
        public IEnumerator End2EndTests_TestErrorCode_503()
        {
            var testErrorCode = new TestErrorCode
            {
                code = 503
            };
            yield return TestConnection(testErrorCode);
        }


        [UnityTest, Timeout(5000)]
        public IEnumerator End2EndTests_Message1024BoundaryTest_LessThan1024()
        {
            ExpectedResult expectedResult = new ExpectedResult
            {
                path = "result",
                value = "Test result less than 1024 bytes."
            };

            Message1024BoundaryTest boundaryTest = new Message1024BoundaryTest
            {
                endpoint = "speech",
                testEndpoint = "lessthan1024bytes",
                testName = "Less than 1024 bytes",
                expectedResults = new ExpectedResult[1] { expectedResult }
            };

            yield return TestConnection(boundaryTest);
        }

        [UnityTest, Timeout(5000)]
        public IEnumerator End2EndTests_Message1024BoundaryTest_Equal1024()
        {
            ExpectedResult expectedResult = new ExpectedResult
            {
                path = "result",
                value = "Test result is 1024 bytes."
            };

            Message1024BoundaryTest boundaryTest = new Message1024BoundaryTest
            {
                endpoint = "speech",
                testEndpoint = "1024bytes",
                testName = "Test result is 1024 bytes.",
                expectedResults = new ExpectedResult[1] { expectedResult }
            };

            yield return TestConnection(boundaryTest);
        }


        [UnityTest]
        public IEnumerator End2EndTests_Message1024BoundaryTest_MoreThan1024()
        {
            ExpectedResult expectedResult = new ExpectedResult
            {
                path = "result",
                value = "Test result is more than 1024 bytes."
            };

            Message1024BoundaryTest boundaryTest = new Message1024BoundaryTest
            {
                endpoint = "speech",
                testEndpoint = "morethan1024bytes",
                testName = "More than 1024 bytes.",
                expectedResults = new ExpectedResult[1] { expectedResult }
            };

            yield return TestConnection(boundaryTest);
        }


        [UnityTest, Timeout(5000)]
        public IEnumerator End2EndTests_Message1024BoundaryTest_LessThan1024_Chunked()
        {
            ExpectedResult expectedResult = new ExpectedResult
            {
                path = "result",
                value = "Test result less than 1024 bytes."
            };

            Message1024BoundaryTest boundaryTest = new Message1024BoundaryTest
            {
                endpoint = "speech",
                testEndpoint = "lessthan1024bytes-chunked",
                testName = "Less than 1024 bytes",
                expectedResults = new ExpectedResult[1] { expectedResult }
            };

            yield return TestConnection(boundaryTest);
        }

        [UnityTest, Timeout(5000)]
        public IEnumerator End2EndTests_Message1024BoundaryTest_Equal1024_Chunked()
        {
            ExpectedResult expectedResult = new ExpectedResult
            {
                path = "result",
                value = "Test result is 1024 bytes."
            };

            Message1024BoundaryTest boundaryTest = new Message1024BoundaryTest
            {
                endpoint = "speech",
                testEndpoint = "1024bytes-chunked",
                testName = "Test result is 1024 bytes.",
                expectedResults = new ExpectedResult[1] { expectedResult }
            };

            yield return TestConnection(boundaryTest);
        }


        [UnityTest]
        public IEnumerator End2EndTests_Message1024BoundaryTest_MoreThan1024_Chunked()
        {
            ExpectedResult expectedResult = new ExpectedResult
            {
                path = "result",
                value = "Test result is more than 1024 bytes."
            };

            Message1024BoundaryTest boundaryTest = new Message1024BoundaryTest
            {
                endpoint = "speech",
                testEndpoint = "morethan1024bytes-chunked",
                testName = "More than 1024 bytes.",
                expectedResults = new ExpectedResult[1] { expectedResult }
            };

            yield return TestConnection(boundaryTest);
        }


        private IEnumerator RunTest(Wit wit, ConnectionTest test)
        {
            wit.events.OnResponse.AddListener(test.OnResponse);
            wit.events.OnError.AddListener(test.OnError);
            wit.events.OnRequestCreated.AddListener(test.OnRequestCreated);
            wit.events.OnRequestCompleted.AddListener(test.OnRequestCompleted);
            test.Execute(wit);
            yield return new WaitUntil(() => test.TestComplete);
            wit.events.OnResponse.RemoveListener(test.OnResponse);
            wit.events.OnError.RemoveListener(test.OnError);
            wit.events.OnRequestCreated.RemoveListener(test.OnRequestCreated);
            wit.events.OnRequestCompleted.RemoveListener(test.OnRequestCompleted);
        }

        private void CloseServer(Process p)
        {
            if (p == null || p.HasExited)
                return;
            p.Kill(); // Note: p.Close() is unreliable and often does not close the node server.
        }

        public IEnumerator SetupServer()
        {
            _process = null;
            var p = new Process()
            {
                StartInfo =
                {
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    RedirectStandardInput = true,
                    FileName = pathToNodeExe,
                    Arguments = $"\"{pathToTestServerScript}\""
                },
            };

            p.Start();
            // Note: This test has timeout which means if this while loop becomes an infinite one, the nUnit timeout mechanism terminates it.
            while (true)
            {
                var task = p.StandardOutput.ReadLineAsync();
                while (task.Status != TaskStatus.RanToCompletion && task.Status != TaskStatus.Faulted && task.Status != TaskStatus.Canceled)
                {
                    yield return null;
                }
                if (task.Result == "Ready")
                {
                    break; // it means the server is ready. We can continue.
                }
            }
            _process = p;
        }
    }
}
