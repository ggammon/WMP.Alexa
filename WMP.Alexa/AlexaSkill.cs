using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.Web.Http;

namespace WMP.Alexa
{
    public abstract class AlexaSkill : SimpleAlexaSkill
    {
        public override void HandleRequest(AlexaSession session, AlexaRequest request, AlexaResponse response)
        {
            if (request.Body.Type == "LaunchRequest")
            {
                StartSession(session, request, response);
                return;
            }
            else if (request.Body.Type == "SessionEndedRequest")
            {
                EndSession(session, request, response);
                return;
            }
            else if (request.Body.Type == "IntentRequest")
            {
                Type child = this.GetType();
                foreach (MethodInfo method in child.GetMethods())
                {
                    foreach(Intent intent in method.GetCustomAttributes<Intent>())
                    {
                        if (intent.Name == request.Body.Intent.Name)
                        {
                            method.Invoke(this, new object[] { session, request, response });
                            return;
                        }
                    }
                }
            }
            throw new NotImplementedException();
        }

        public abstract void StartSession(AlexaSession session, AlexaRequest request, AlexaResponse response);

        public abstract void EndSession(AlexaSession session, AlexaRequest request, AlexaResponse response);
    }
}
