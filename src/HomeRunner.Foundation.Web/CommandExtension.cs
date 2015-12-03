
using HomeRunner.Foundation.Cqrs;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace HomeRunner.Foundation.Web
{
    public static class CommandExtension
    {
        public static HttpResponseMessage ToResponse(this ICommand<ICommandResult> command)
        {
            return new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.Accepted,
                Content =
                    new ObjectContent(
                        typeof (object),
                        new CommandResponse(command.Id),
                        new JsonMediaTypeFormatter
                        {
                            SerializerSettings = new JsonSerializerSettings {ContractResolver = new CamelCasePropertyNamesContractResolver()}
                        })
            };
        }
    }
}