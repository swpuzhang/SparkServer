using Commons.Extenssions.Defines;
using System;
using System.Collections.Generic;
using System.Text;
using MassTransit;
using Commons.Domain.Models;
using Newtonsoft.Json;

namespace Commons.Extenssions
{
    public static class MQHelper
    {
        public static string ConnectingString = "";
        public static void PublishExt<T>(this IBusControl _bus, long id, T request) 
            where T : MQBaseMessage
        {
            _bus.Publish(new ServerRequest(id, request.ClassName, JsonConvert.SerializeObject(request), Guid.NewGuid()));
        }

        public static void PublishExt<T>(this IBusControl _bus, T request)
            where T : class
        {
            _bus.Publish(request);
        }

    }
}
