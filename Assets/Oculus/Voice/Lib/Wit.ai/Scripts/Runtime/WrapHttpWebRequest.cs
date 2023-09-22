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
using System.IO;
using System.Net;
using UnityEngine;

namespace Facebook.WitAi
{
    public class WrapHttpWebRequest : IRequest
    {
        HttpWebRequest _httpWebRequest;

        public WrapHttpWebRequest(HttpWebRequest httpWebRequest)
        {
            if (Application.isBatchMode)
            {
                httpWebRequest.KeepAlive = false;
            }
            _httpWebRequest = httpWebRequest;
        }

        public WebHeaderCollection Headers { get => _httpWebRequest.Headers; set => _httpWebRequest.Headers = value; }
        public string Method { get => _httpWebRequest.Method; set => _httpWebRequest.Method = value; }
        public string ContentType { get => _httpWebRequest.ContentType; set => _httpWebRequest.ContentType = value; }
        public long ContentLength { get => _httpWebRequest.ContentLength; set => _httpWebRequest.ContentLength = value; }
        public bool SendChunked { get => _httpWebRequest.SendChunked; set => _httpWebRequest.SendChunked = value; }
        public string UserAgent { get => _httpWebRequest.UserAgent; set => _httpWebRequest.UserAgent = value; }
        public int Timeout { get => _httpWebRequest.Timeout; set => _httpWebRequest.Timeout = value; }

        public void Abort()
        {
            _httpWebRequest.Abort();
        }

        public void Dispose()
        {
            _httpWebRequest.Abort();
            _httpWebRequest = null;
        }

        public IAsyncResult BeginGetRequestStream(AsyncCallback callback, object state)
        {
            return _httpWebRequest.BeginGetRequestStream(callback, state);
        }

        public IAsyncResult BeginGetResponse(AsyncCallback callback, object state)
        {
            return _httpWebRequest.BeginGetResponse(callback, state);
        }

        public Stream EndGetRequestStream(IAsyncResult asyncResult)
        {
            return _httpWebRequest.EndGetRequestStream(asyncResult);
        }

        public WebResponse EndGetResponse(IAsyncResult asyncResult)
        {
            return (_httpWebRequest).EndGetResponse(asyncResult);
        }
    }
}
