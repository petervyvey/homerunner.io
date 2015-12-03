
using HomeRunner.Domain.Service.Platform.Commands;
using HomeRunner.Domain.Service.Platform.Queries;
using HomeRunner.Foundation.Web;
using HomeRunner.Rest.Service.Platform.Representation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace HomeRunner.Rest.Service.Platform
{
    [RoutePrefix("{tenantId}/taskactivity")]
    public sealed class TaskActivityController
        : RestController
    {
        [HttpGet, Route("")]
        public IEnumerable<TaskActivity> GetTaskActivityListQuery(string tenantId)
        {
            TaskActivityListQuery query = new TaskActivityListQuery();
            var taskActivities =
                this.ExecuteQuery(query)
                    .Map<List<TaskActivity>>();

            return taskActivities;
        }

        [HttpGet, Route("{id:guid}")]
        public TaskActivity GetTaskActivityQuery(string tenantId, Guid id)
        {
            TaskActivityListQuery query = new TaskActivityListQuery(id);
            var taskActivities = this.ExecuteQuery(query);

            var representations =
                taskActivities
                    .Map<List<TaskActivity>>()
                    .Single();

            return representations;
        }

        [HttpGet, Route("{id:guid}/problem")]
        public TaskActivity GetProblem(string tenantId, Guid id)
        {
            throw new Exception();

            TaskActivityListQuery query = new TaskActivityListQuery(id);
            TaskActivity taskActivity =
                this.ExecuteQuery(query)
                    .Map<List<TaskActivity>>()
                    .Single();

            return taskActivity;
        }

        [HttpPut, Route("{id:guid}/claim")]
        public async Task<HttpResponseMessage> PutClaimTaskActivityCommand(string tenantId, Guid id)
        {
            ClaimTaskActivityCommand claimTaskActivityCommand = new ClaimTaskActivityCommand(id);

            return (await this.SubmitCommand(claimTaskActivityCommand)).ToResponse();
        }

        [HttpPut, Route("{id:guid}/unclaim")]
        public async Task<HttpResponseMessage> PutUnclaimTaskActivityCommand(string tenantId, Guid id)
        {
            UnclaimTaskActivityCommand unclaimTaskActivityCommand = new UnclaimTaskActivityCommand(id);
            return (await this.SubmitCommand(unclaimTaskActivityCommand)).ToResponse();
        }
    }
}
