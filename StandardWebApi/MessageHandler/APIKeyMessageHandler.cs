﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace StandardWebApi.MessageHandler
{
    public class APIKeyMessageHandler : DelegatingHandler
    {
        private const string APIKeyToCheck = "212sspdvfdpp=were";

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage httpRequest, CancellationToken cancellationToken)
        {
            bool validKey = false;
            IEnumerable<string> requestHeaders;
            var checkApiKeyExists = httpRequest.Headers.TryGetValues("APIKey", out requestHeaders);
            if (checkApiKeyExists)
            {
                if (requestHeaders.FirstOrDefault().Equals(APIKeyToCheck))
                    validKey = true;
            }

            if (!validKey)
                return httpRequest.CreateResponse(HttpStatusCode.Forbidden, "Invalid access");

            var response = await base.SendAsync(httpRequest, cancellationToken);

            return response;
        }
    }
}