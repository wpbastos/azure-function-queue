﻿using Newtonsoft.Json;

namespace SendEmail.AzureQueueLibrary.MessageSerializer
{
    public class JsonMessageSerializer : IMessageSerializer
    {
        public T Deserialize<T>(string message)
        {
            var obj = JsonConvert.DeserializeObject<T>(message);
            return obj;
        }

        public string Serialize(object obj)
        {
            var msg = JsonConvert.SerializeObject(obj);
            return msg;
        }
    }
}
