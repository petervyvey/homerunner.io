
using HomeRunner.Domain.WriteModel.Platform.TaskActivities.Commands;
using HomeRunner.Foundation.MessageBus;
using HomeRunner.Foundation.Web;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace HomeRunner.Api.WriteModel.Platform
{
    [RoutePrefix("command/{tenantId}/taskactivity"), ApiExceptionFilter]
	public class TaskActivityController
		: ApiController
	{
        private readonly IBusConnector connector;

	    public TaskActivityController(IBusConnector connector)
			: base()
	    {
            this.connector = connector;
	    }

	    [HttpPost, Route("")]
		public async Task<HttpResponseMessage> PostCreateTaskActivityCommand(string tenantId, [FromBody] V1.Platform.Intents.CreateTaskActivity intent)
		{
			CreateTaskActivityCommand command = new CreateTaskActivityCommand(intent.Description);
            await this.connector.SendCommand(command);

		    return ApiResponse.CommandResponse(command);
		}

	    [HttpPut, Route("{id:guid}/claim")]
		public async Task<HttpResponseMessage> PutClaimTaskActivityCommand(string tenantId, Guid id)
		{
			ClaimTaskActivityCommand command = new ClaimTaskActivityCommand(id);
            await this.connector.SendCommand(command);

			return ApiResponse.CommandResponse(command);
		}

		[HttpPut, Route("{id:guid}/unclaim")]
		public async Task<HttpResponseMessage> PutUnclaimTaskActivityCommand(string tenantId, Guid id)
		{
			UnclaimTaskActivityCommand command = new UnclaimTaskActivityCommand(id);
            await this.connector.SendCommand(command);

			return ApiResponse.CommandResponse(command);
		}
	}
}

