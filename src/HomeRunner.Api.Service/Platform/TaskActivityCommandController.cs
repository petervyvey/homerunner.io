
using HomeRunner.Domain.WriteModel.Platform.TaskActivities.Commands;
using HomeRunner.Foundation.Cqrs;
using HomeRunner.Foundation.Logging;
using HomeRunner.Foundation.Web;
using MassTransit;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace HomeRunner.Api.Service.Platform
{
	[RoutePrefix("command/{tenantId}/taskactivity")]
	public class TaskActivityCommandController
		: ApiController
	{
		private readonly IBus bus;

		public TaskActivityCommandController(IBus bus)
			: base()
		{
			this.bus = bus;
		}

		[HttpPost, Route("")]
		public async Task<HttpResponseMessage> PostCreateTaskActivityCommand(string tenantId, [FromBody] V1.Intents.CreateTaskActivity intent)
		{
			CreateTaskActivityCommand command = new CreateTaskActivityCommand(intent.Description);

			HomeRunner.Api.Service.Logger.Log.Info(string.Format("Submiting command: {0} [{1}] ", command.GetType().FullName, command.Id));
			ISendEndpoint endpoint = await this.bus.GetSendEndpoint(Foundation.RabbitMQ.Configuration.Exchange);
			await endpoint.Send(new CommandMessage<CreateTaskActivityCommand>(command));

			HomeRunner.Api.Service.Logger.Log.Info(string.Format("Command submitted: {0} [{1}]", command.GetType().FullName, command.Id));

			return ApiResponse.CommandResponse(command);
		}

		[HttpPut, Route("{id:guid}/claim")]
		public async Task<HttpResponseMessage> PutClaimTaskActivityCommand(string tenantId, Guid id)
		{
			ClaimTaskActivityCommand command = new ClaimTaskActivityCommand(id);

			ISendEndpoint endpoint = await this.bus.GetSendEndpoint(Foundation.RabbitMQ.Configuration.Exchange);
			await endpoint.Send(new CommandMessage<ClaimTaskActivityCommand>(command));

			return ApiResponse.CommandResponse(command);
		}

		[HttpPut, Route("{id:guid}/unclaim")]
		public async Task<HttpResponseMessage> PutUnclaimTaskActivityCommand(string tenantId, Guid id)
		{
			UnclaimTaskActivityCommand command = new UnclaimTaskActivityCommand(id);

			ISendEndpoint endpoint = await this.bus.GetSendEndpoint(Foundation.RabbitMQ.Configuration.Exchange);
			await endpoint.Send(new CommandMessage<UnclaimTaskActivityCommand>(command));

			return ApiResponse.CommandResponse(command);
		}
	}
}

