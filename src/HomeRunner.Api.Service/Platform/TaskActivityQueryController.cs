
using HomeRunner.Api.V1.Platform.Representations;
using HomeRunner.Domain.ReadModel.Platform.TaskActivities.Queries;
using HomeRunner.Foundation.Web;
using MediatR;
using System;
using System.Web.Http;

namespace HomeRunner.Api.Service.Platform
{
	[RoutePrefix("query/{tenantId}/taskactivity")]
	public class TaskActivityQueryController
		: ApiController
	{
		private readonly IMediator mediator;

		public TaskActivityQueryController(IMediator mediator)
			: base()
		{
			this.mediator = mediator;
		}

		[HttpGet, Route("")]
		public TaskActivityList GetTaskActivityListQuery(string tenantId)
		{
			TaskActivityListQuery query = new TaskActivityListQuery();
			var represenations =
				this.mediator.Send(query)
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
	}
}

