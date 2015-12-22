
using HomeRunner.Domain.ReadModel.Platform.TaskActivities.Entities;
using HomeRunner.Domain.ReadModel.Platform.TaskActivities.Queries;
using HomeRunner.Foundation.Web;
using MediatR;
using System;
using System.Web.Http;

namespace HomeRunner.Api.ReadModel.Platform
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
        public V1.Platform.Representations.TaskActivityList GetTaskActivityListQuery(string tenantId)
		{
			TaskActivityListQuery query = new TaskActivityListQuery();
			var represenations =
				this.mediator.Send(query)
                    .ToRepresentations<TaskActivity, V1.Platform.Representations.TaskActivity>();

            return new V1.Platform.Representations.TaskActivityList(represenations);
		}

		[HttpGet, Route("{id:guid}")]
        public V1.Platform.Representations.TaskActivity GetTaskActivityQuery(string tenantId, Guid id)
		{
			TaskActivityQuery query = new TaskActivityQuery(id);
			var representation =
				this.mediator
					.Send(query)
                    .ToRepresentation<TaskActivity, V1.Platform.Representations.TaskActivity>();

			return ApiResponse.Found(representation);
		}
	}
}

