using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.IO;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;

namespace WMP.Alexa
{
    public abstract class SimpleAlexaSkill
    {
        public abstract void HandleRequest(AlexaSession session, AlexaRequest request, AlexaResponse response);

        public AlexaResponse HandleHttpRequest(HttpRequestMessage httpRequest)
        {
            AlexaRequest request = JsonConvert.DeserializeObject<AlexaRequest>(httpRequest.Content.ReadAsStringAsync().Result);
            byte[] alexaBytes = httpRequest.Content.ReadAsByteArrayAsync().Result;

            DateTime now = DateTime.Now; // reference time for this request

            string chainUrl = null;
            if (!httpRequest.Headers.Contains("SignatureCertChainUrl") || String.IsNullOrEmpty(chainUrl = httpRequest.Headers.GetValues("SignatureCertChainUrl").First()))
            {
                throw new UnauthorizedAccessException();
            }

            string signature = null;
            if (!httpRequest.Headers.Contains("Signature") || String.IsNullOrEmpty(signature = httpRequest.Headers.GetValues("Signature").First()))
            {
                throw new UnauthorizedAccessException();
            }

            if (!SpeechletRequestSignatureVerifier.VerifyRequestSignature(alexaBytes, signature, chainUrl))
                throw new UnauthorizedAccessException();

            DateTime parsed = DateTime.Parse(request.Body.Timestamp);

            if ((now - parsed).TotalSeconds > 150)
                throw new UnauthorizedAccessException();

            AlexaResponse response = new AlexaResponse();
            response.SessionAttributes = request.Session.Attributes;

            HandleRequest(response.SessionAttributes, request, response);

            return response;
        }
    }
}