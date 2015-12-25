
using HomeRunner.Domain.ReadModel.Platform.TaskActivities.Queries;
using HomeRunner.Foundation.Web;
using MediatR;
using System;
using System.Web.Http;

namespace HomeRunner.Api.ReadModel.Platform
{
	[RoutePrefix("query/{tenantId}/taskactivity"), ApiExceptionFilter]
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
        public Domain.ReadModel.Platform.TaskActivities.Representations.TaskActivityList GetTaskActivityListQuery(string tenantId)
		{
			TaskActivityListQuery query = new TaskActivityListQuery();
			var representations = this.mediator.Send(query);

		    return representations;
		}

		[HttpGet, Route("{id:guid}")]
        public Domain.ReadModel.Platform.TaskActivities.Representations.TaskActivity GetTaskActivityQuery(string tenantId, Guid id)
		{
			TaskActivityQuery query = new TaskActivityQuery(id);
			var representation = this.mediator.Send(query);

			return ApiResponse.Found(representation);
		}
	}
}

