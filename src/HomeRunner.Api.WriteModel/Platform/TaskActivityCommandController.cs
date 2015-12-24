
using HomeRunner.Domain.WriteModel.Platform.TaskActivities.Commands;
using HomeRunner.Foundation.Web;
using MassTransit;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace HomeRunner.Api.WriteModel.Platform
{
    [RoutePrefix("command/{tenantId}/taskactivity"), ApiExceptionFilter]
	public class TaskActivityCommandController
		: ApiController
	{
		private readonly IBus bus;
	    private readonly MassTransitConnector massTransitConnector;

	    public TaskActivityCommandController(IBus bus)
			: base()
	    {
	        this.bus = bus;
	    }

	    [HttpPost, Route("")]
		public async Task<HttpResponseMessage> PostCreateTaskActivityCommand(string tenantId, [FromBody] V1.Platform.Intents.CreateTaskActivity intent)
		{
			CreateTaskActivityCommand command = new CreateTaskActivityCommand(intent.Description);
            await MassTransitConnector.SendCommand(command, this.bus);

		    return ApiResponse.CommandResponse(command);
		}

	    [HttpPut, Route("{id:guid}/claim")]
		public async Task<HttpResponseMessage> PutClaimTaskActivityCommand(string tenantId, Guid id)
		{
			ClaimTaskActivityCommand command = new ClaimTaskActivityCommand(id);
            await MassTransitConnector.SendCommand(command, this.bus);

			return ApiResponse.CommandResponse(command);
		}

		[HttpPut, Route("{id:guid}/unclaim")]
		public async Task<HttpResponseMessage> PutUnclaimTaskActivityCommand(string tenantId, Guid id)
		{
			UnclaimTaskActivityCommand command = new UnclaimTaskActivityCommand(id);
            await MassTransitConnector.SendCommand(command, this.bus);

			return ApiResponse.CommandResponse(command);
		}
	}
}

