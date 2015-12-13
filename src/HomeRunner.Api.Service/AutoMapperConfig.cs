
using AutoMapper;
using System.Collections.Generic;

namespace HomeRunner.Api.Service
{
    public static class AutoMapperConfig
    {
        public static void Config()
        {
			Mapper.CreateMap<Domain.ReadModel.Platform.TaskActivities.Entities.TaskActivity, V1.Platform.Representations.TaskActivity>();
        }
    }
}
