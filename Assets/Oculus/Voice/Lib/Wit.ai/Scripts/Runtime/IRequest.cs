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
using System.Net;
using System.IO;

namespace Facebook.WitAi
{
    public interface IRequest
    {
        WebHeaderCollection Headers { get; set; }
        string Method { get; set; }
        string ContentType { get; set; }
        long ContentLength { get; set; }
        bool SendChunked { get; set; }
        string UserAgent { get; set; }
        int Timeout { get; set; }

        IAsyncResult BeginGetRequestStream(AsyncCallback callback, object state);
        IAsyncResult BeginGetResponse(AsyncCallback callback, object state);
        /// <summary>
        /// Returns a Stream for writing data to the Internet resource.
        /// </summary>
        /// <param name="asyncResult"></param>
        /// <returns></returns>
        Stream EndGetRequestStream(IAsyncResult asyncResult);
        WebResponse EndGetResponse(IAsyncResult asyncResult);

        void Abort();
        void Dispose();
    }
}
