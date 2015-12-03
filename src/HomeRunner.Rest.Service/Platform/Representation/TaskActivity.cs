
using HalJsonNet.Configuration;
using HalJsonNet.Configuration.Interfaces;
using System;
using System.Collections.Generic;

namespace HomeRunner.Rest.Service.Platform.Representation
{
    public class TaskActivity
        : IHaveHalJsonLinks
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        public string AssignedTo { get; set; }

        public bool IsClaimed { get; set; }

        public bool IsCompleted { get; set; }

        public DateTime UpdateTime { get; set; }

        public IDictionary<string, Link> GetLinks()
        {
            //var tenant = ((ClaimsPrincipal) Thread.CurrentPrincipal).Claims.Single(x => x.Type.Equals(CustomClaimTypes.TenantId, StringComparison.InvariantCultureIgnoreCase)).Value;
            var tenant = "tenant";
            return new Dictionary<string, Link> { { "self", string.Format("/v1/{0}/taskactivity/{1}", tenant, this.Id) } };
        }
    }
}
