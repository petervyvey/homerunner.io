
using AutoMapper;

namespace HomeRunner.Api.ReadModel
{
    public static class AutoMapperConfig
    {
        public static void Config()
        {
			Mapper.CreateMap<Domain.ReadModel.Platform.TaskActivities.Entities.TaskActivity, V1.Platform.Representations.TaskActivity>();
        }
    }
}
