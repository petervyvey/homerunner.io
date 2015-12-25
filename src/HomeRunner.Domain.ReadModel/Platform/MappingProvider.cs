
using HomeRunner.Domain.ReadModel.Platform.TaskActivities.Representations;
using HomeRunner.Foundation.Dapper;
using System;
using System.Collections.Generic;

namespace HomeRunner.Domain.ReadModel.Platform
{
    public class MappingProvider
        : IMappingProvider
    {
        private static readonly Dictionary<Type, string> MAPPINGS = new Dictionary<Type, string>
        {
            {typeof(TaskActivity), "Task"}
        };

        public Dictionary<Type, string> Mappings{ get { return MappingProvider.MAPPINGS; }}
    }
}
