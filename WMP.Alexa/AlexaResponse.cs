using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WMP.Alexa
{
    public class AlexaResponse
    {
        public readonly string version = "1.0";

        public AlexaSession SessionAttributes { get; set; } = new AlexaSession();

        [JsonProperty(PropertyName = "response")]
        public AlexaResponseBody Body { get; set; } = new AlexaResponseBody();

        public void SayText(string text)
        {
            if (Body.OutputSpeech != null)
            {
                Body.OutputSpeech.text += " " + text;
                return;
            }

            Body.OutputSpeech = new AlexaResponseOutputSpeech()
            {
                type = "PlainText",
                text = text
            };
        }

        public void SaySSML(string ssml)
        {
            if (Body.OutputSpeech != null)
            {
                Body.OutputSpeech.ssml += " " + ssml;
                return;
            }

            Body.OutputSpeech = new AlexaResponseOutputSpeech()
            {
                type = "SSML",
                ssml = ssml
            };
        }

        public void RepromptText(string text)
        {
            if (Body.Reprompt != null)
            {
                Body.Reprompt.text += " " + text;
                return;
            }

            Body.Reprompt = new AlexaResponseReprompt()
            {
                type = "PlainText",
                text = text
            };
        }

        public void RepromptSSML(string ssml)
        {
            if (Body.Reprompt != null)
            {
                Body.Reprompt.ssml += " " + ssml;
                return;
            }

            Body.Reprompt = new AlexaResponseReprompt()
            {
                type = "SSML",
                ssml = ssml
            };
        }

        public void AddDirective(AlexaResponseDirective directive)
        {
            Body.Directives.Add(directive);
        }

        public void ShowCard(string title, string content)
        {
            Body.Card = new AlexaResponseCard()
            {
                type = "Simple",
                title = title,
                content = content
            };
        }

        public void ShowCard(string title, string text, string image)
        {
            Body.Card = new AlexaResponseCard()
            {
                type = "Standard",
                title = title,
                text = text,
                image = image
            };
        }

        public void LinkAccount()
        {
            Body.Card = new AlexaResponseCard()
            {
                type = "LinkAccount"
            };
        }

        public void EndSession()
        {
            Body.ShouldEndSession = true;
        }

        public class AlexaResponseBody
        {
            public AlexaResponseOutputSpeech OutputSpeech { get; set; }
            public AlexaResponseCard Card { get; set; }
            public AlexaResponseReprompt Reprompt { get; set; }

            public Boolean ShouldEndSession { get; set; } = false;

            public List<AlexaResponseDirective> Directives { get; set; } = new List<AlexaResponseDirective>();
        }
    }

    public class AlexaResponseOutputSpeech
    {
        public string type { get; set; }
        public string text { get; set; }
        public string ssml { get; set; }
    }

    public class AlexaResponseCard
    {
        public string type { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public string text { get; set; }
        public string image { get; set; }
    }

    public class AlexaResponseReprompt : AlexaResponseOutputSpeech
    {

    }

    public class AlexaResponseDirective
    {

    }

}
