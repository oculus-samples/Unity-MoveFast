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

using Facebook.WitAi.Data;
using NUnit.Framework;

namespace Facebook.WitAi.Tests
{
    public class RingBufferTests
    {
        // Public methods (interface) of RingBuffer:
        //  Clear
        //  CreateMarker
        //  Push
        //  Read
        //  OnDataAddedEvent
        //  Capacity
        // A Test behaves as an ordinary method
        [Test]
        public void Test_Capacity()
        {
            var ringBuffer = new RingBuffer<int>(5);
            Assert.AreEqual(ringBuffer.Capacity, 5);
        }
        [Test]
        public void Test_RingBuffer_Push_And_Read()
        {
            byte[] data = { 0, 1, 10, 100, 255 }; // sample input.
            var micDataBuffer = new RingBuffer<byte>(data.Length);
            RingBuffer<byte>.Marker lastSampleMarker = micDataBuffer.CreateMarker(0);
            micDataBuffer.Push(data, 0, data.Length);
            var readBuffer = new byte[data.Length];
            int result = lastSampleMarker.Read(readBuffer, 0, readBuffer.Length, true);
            Assert.IsTrue(result > 0);
            Assert.AreEqual(data, readBuffer);
        }

        // Tests whether .Clear() method works when doing .Read() using Marker subclass.
        [Test]
        public void Test_RingBuffer_Clear_And_MarkerRead()
        {
            byte[] data = { 1, 3, 4, 5, 255 }; // sample input.
            var micDataBuffer = new RingBuffer<byte>(data.Length);
            RingBuffer<byte>.Marker lastSampleMarker = micDataBuffer.CreateMarker(0);

            micDataBuffer.Push(data, 0, data.Length);
            micDataBuffer.Clear();
            var readBuffer = new byte[data.Length];
            int result = lastSampleMarker.Read(readBuffer, 0, readBuffer.Length, true);

            // Not only the elements of the input array should remain zero but 
            // the number of read bytes should also be zero.
            Assert.AreEqual(new byte[5], readBuffer);
            Assert.AreEqual(result, 0);
        }
        // Tests whether .Clear() method works when doing .Read() using RingBuffer class.
        [Test]
        public void Test_RingBuffer_Clear_And_Read()
        {
            byte[] data = { 1, 3, 4, 5, 255 }; // sample input.
            var micDataBuffer = new RingBuffer<byte>(data.Length);
            micDataBuffer.Push(data, 0, data.Length);
            micDataBuffer.Clear();
            var readBuffer = new byte[data.Length];
            int result = micDataBuffer.Read(readBuffer, 0, readBuffer.Length, 0);

            // Not only the input array should remain zero but 
            // the number of read bytes should also be zero.
            Assert.AreEqual(new byte[5], readBuffer);
            Assert.AreEqual(result, 0);
        }

        // Tests whether three consecutive .Read() operations work properly.
        // The last .Read() test assures nothing can be read when no data is available.
        [Test]
        public void Test_RingBuffer_Push_And_Read_And_Read()
        {
            byte[] data = { 0, 1, 10, 100, 255 }; // sample input.
            var micDataBuffer = new RingBuffer<byte>(data.Length);
            RingBuffer<byte>.Marker lastSampleMarker = micDataBuffer.CreateMarker(0);
            micDataBuffer.Push(data, 0, data.Length);
            var readBuffer = new byte[3];

            // First .Read()
            int result = lastSampleMarker.Read(readBuffer, 0, 3, true);
            Assert.IsTrue(result > 0);
            Assert.AreEqual(new byte[3] { 0, 1, 10 }, readBuffer);

            // Second .Read()
            var readBuffer2 = new byte[2];
            result = lastSampleMarker.Read(readBuffer2, 0, 2, true);
            Assert.IsTrue(result > 0);
            Assert.AreEqual(new byte[2] { 100, 255 }, readBuffer2);

            // Third .Read(). This test assures nothing can be read when no data is available.
            var readBuffer3 = new byte[1];
            result = lastSampleMarker.Read(readBuffer3, 0, 1, true);
            Assert.IsTrue(result <= 0);
        }
    }
}
