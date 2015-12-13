
using HomeRunner.Api.V1.Platform.Representations;
using HomeRunner.Domain.ReadModel.Platform.TaskActivities.Queries;
using HomeRunner.Domain.WriteModel.Platform.TaskActivities.Commands;
using HomeRunner.Foundation.Cqrs;
using HomeRunner.Foundation.Logging;
using HomeRunner.Foundation.Web;
using MassTransit;
using MediatR;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace HomeRunner.Api.Service.Platform
{
    [RoutePrefix("{tenantId}/taskactivity")]
    public sealed class TaskActivityController
        : ApiController
    {
        private readonly IMediator mediator;

        private readonly IBus bus;

        public TaskActivityController(IMediator mediator, IBus bus) : base()
        {
            this.mediator = mediator;
            this.bus = bus;
        }

        [HttpGet, Route("")]
		public TaskActivityList GetTaskActivityListQuery(string tenantId)
        {
            TaskActivityListQuery query = new TaskActivityListQuery();
			var represenations = 
				this.mediator.Send (query)
					.ToRepresentations<Domain.ReadModel.Platform.TaskActivities.Entities.TaskActivity, TaskActivity>();

			return new TaskActivityList(represenations);
        }

        [HttpGet, Route("{id:guid}")]
        public TaskActivity GetTaskActivityQuery(string tenantId, Guid id)
        {
            TaskActivityQuery query = new TaskActivityQuery(id);
            var representation =
                this.mediator
                    .Send(query)
                    .ToRepresentation<Domain.ReadModel.Platform.TaskActivities.Entities.TaskActivity, TaskActivity>();

            return ApiResponse.Found(representation);
        }

        [HttpGet, Route("{id:guid}/problem")]
        public TaskActivity GetProblem(string tenantId, Guid id)
        {
            throw new Exception();

            TaskActivityQuery query = new TaskActivityQuery(id);
            var representation =
                this.mediator
                    .Send(query)
                    .ToRepresentation<Domain.ReadModel.Platform.TaskActivities.Entities.TaskActivity, TaskActivity>();

            return ApiResponse.Found(representation);
        }

		[HttpPost, Route("")]
		public async Task<HttpResponseMessage> PostCreateTaskActivityCommand(string tenantId, [FromBody]V1.Intents.CreateTaskActivity intent)
		{
			CreateTaskActivityCommand command = new CreateTaskActivityCommand(intent.Description);

			HomeRunner.Api.Service.Logger.Log.Info(string.Format("Submiting command: {0} [{1}] ", command.GetType().FullName, command.Id));
			ISendEndpoint endpoint = await this.bus.GetSendEndpoint(RabbitMQConfiguration.Exchange);
			await endpoint.Send(new CommandMessage<CreateTaskActivityCommand>(command));

			HomeRunner.Api.Service.Logger.Log.Info(string.Format("Command submitted: {0} [{1}]", command.GetType().FullName, command.Id));

			return ApiResponse.CommandResponse(command);
		}

        [HttpPut, Route("{id:guid}/claim")]
        public async Task<HttpResponseMessage> PutClaimTaskActivityCommand(string tenantId, Guid id)
        {
            ClaimTaskActivityCommand command = new ClaimTaskActivityCommand(id);

			ISendEndpoint endpoint = await this.bus.GetSendEndpoint(RabbitMQConfiguration.Exchange);
            await endpoint.Send(new CommandMessage<ClaimTaskActivityCommand>(command));

            return ApiResponse.CommandResponse(command);
        }

        [HttpPut, Route("{id:guid}/unclaim")]
        public async Task<HttpResponseMessage> PutUnclaimTaskActivityCommand(string tenantId, Guid id)
        {
            UnclaimTaskActivityCommand command = new UnclaimTaskActivityCommand(id);

			ISendEndpoint endpoint = await this.bus.GetSendEndpoint(RabbitMQConfiguration.Exchange);
            await endpoint.Send(new CommandMessage<UnclaimTaskActivityCommand>(command));

            return ApiResponse.CommandResponse(command);
        }
    }

}
