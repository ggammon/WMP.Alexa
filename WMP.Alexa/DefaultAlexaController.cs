using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WMP.Alexa
{
    public class DefaultAlexaController<T> : ApiController where T : SimpleAlexaSkill, new()
    {
        public IHttpActionResult Post(HttpRequestMessage httpRequest)
        {
            return Ok((new T()).HandleHttpRequest(httpRequest));
        }
    }
}