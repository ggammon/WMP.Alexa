using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace WMP.Alexa
{
    public class AlexaRequest
    {
        [JsonProperty(PropertyName = "version")]
        public string Version { get; set; }

        [JsonProperty(PropertyName = "session")]
        public AlexaRequestSession Session { get; set; }

        [JsonProperty(PropertyName = "context")]
        public AlexaRequestContext Context { get; set; }

        [JsonProperty(PropertyName = "request")]
        public AlexaRequestBody Body { get; set; }

        public class AlexaRequestSession
        {
            [JsonProperty(PropertyName="new")]
            public Boolean NewSession { get; set; }

            [JsonProperty(PropertyName = "sessionId")]
            public string SessionId { get; set; }

            [JsonProperty(PropertyName = "application")]
            public AlexaRequestApplication Application { get; set; }

            [JsonProperty(PropertyName = "attributes")]
            public AlexaSession Attributes { get; set; } = new AlexaSession();

            [JsonProperty(PropertyName = "user")]
            public AlexaRequestUser User { get; set; }
        }

        public class AlexaRequestContext
        {
            [JsonProperty(PropertyName = "System")]
            public AlexaRequestContextSystem System { get; set; }

            public class AlexaRequestContextSystem
            {
                [JsonProperty(PropertyName = "application")]
                public AlexaRequestApplication Application { get; set; }

                [JsonProperty(PropertyName = "user")]
                public AlexaRequestUser User { get; set; }
            }
        }

        public class AlexaRequestApplication
        {
            [JsonProperty(PropertyName = "applicationId")]
            public string ApplicationId { get; set; }
        }

        public class AlexaRequestUser
        {
            [JsonProperty(PropertyName = "userId")]
            public string UserId { get; set; }

            [JsonProperty(PropertyName = "accessToken")]
            public string AccessToken { get; set; }
        }
    }

    public class AlexaRequestBody
    {
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "requestId")]
        public string RequestId { get; set; }

        [JsonProperty(PropertyName = "timestamp")]
        public string Timestamp { get; set; }

        [JsonProperty(PropertyName = "locale")]
        public string Locale { get; set; }

        [JsonProperty(PropertyName = "intent")]
        public AlexaRequestIntent Intent { get; set; }
    }

    public class AlexaRequestIntent
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "slots")]
        public SlotDictionary Slots { get; set; }
    }

    public class AlexaRequestSlot
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "value")]
        public string Value { get; set; }

        public bool IsValid()
        {
            return Value != null && Value != String.Empty;
        }
    }
    

    public class SlotDictionary : Dictionary<string, AlexaRequestSlot>
    {
        private AlexaRequestSlot _default;

        public SlotDictionary()
        {
            _default = new AlexaRequestSlot();
        }

        public new AlexaRequestSlot this[string key]
        {
            get
            {
                AlexaRequestSlot potentialValue;
                if (base.TryGetValue(key, out potentialValue))
                    return potentialValue;

                return _default;
            }
        }
    }
}
