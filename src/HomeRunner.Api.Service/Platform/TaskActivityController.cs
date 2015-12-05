
using HomeRunner.Api.V1.Platform.Representations;
using HomeRunner.Domain.ReadModel.Platform.TaskActivities.Queries;
using HomeRunner.Domain.WriteModel.Commands;
using HomeRunner.Foundation.Cqrs;
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
        public IEnumerable<TaskActivity> GetTaskActivityListQuery(string tenantId)
        {
            TaskActivityListQuery query = new TaskActivityListQuery();
            List<TaskActivity> representations =
                this.mediator
                    .Send(query)
                    .ToRepresentations<Domain.ReadModel.Platform.TaskActivities.Entities.TaskActivity, TaskActivity>();

            return ApiResponse.Found(representations);
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

        [HttpPut, Route("{id:guid}/claim")]
        public async Task<HttpResponseMessage> PutClaimTaskActivityCommand(string tenantId, Guid id)
        {
            ClaimTaskActivityCommand command = new ClaimTaskActivityCommand(id);

            ISendEndpoint endpoint = await this.bus.GetSendEndpoint(new Uri("rabbitmq://localhost/command/NormalPriority"));
            await endpoint.Send(new CommandMessage<ClaimTaskActivityCommand>(command));

            return ApiResponse.CommandResponse(command);
        }

        [HttpPut, Route("{id:guid}/unclaim")]
        public async Task<HttpResponseMessage> PutUnclaimTaskActivityCommand(string tenantId, Guid id)
        {
            UnclaimTaskActivityCommand command = new UnclaimTaskActivityCommand(id);

            ISendEndpoint endpoint = await this.bus.GetSendEndpoint(new Uri("rabbitmq://localhost/command/NormalPriority"));
            await endpoint.Send(new CommandMessage<UnclaimTaskActivityCommand>(command));

            return ApiResponse.CommandResponse(command);
        }
    }
}
