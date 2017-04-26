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
    }   
}
