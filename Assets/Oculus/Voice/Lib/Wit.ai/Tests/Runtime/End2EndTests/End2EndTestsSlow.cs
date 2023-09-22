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
    public class End2EndTestsSlow
    {
        private End2EndTests _end2endTests;
        [SetUp]
        public void SetUp()
        {
            // Put startup code here.
            _end2endTests = new End2EndTests();
        }

        [UnityTest, Timeout(15000)]
        public IEnumerator End2EndTests_Test_Timeout()
        {
            var testErrorCode = new TestErrorCode
            {
                code = -7,
                Name = "Timeout",
                TestEndpoint = "timeout",
                isSlowTest = true                
            };
            yield return _end2endTests.TestConnection(testErrorCode);
        }

        [UnityTest, Timeout(15000)]
        public IEnumerator End2EndTests_Test_Unclean_Termination()
        {
            var testErrorCode = new TestErrorCode
            {
                code = -7,
                Name = "Unclean Termination",
                TestEndpoint = "uncleantermination",
                isSlowTest = true,
                anyErrorCode = true
            };
            yield return _end2endTests.TestConnection(testErrorCode);
        }

    }
}
