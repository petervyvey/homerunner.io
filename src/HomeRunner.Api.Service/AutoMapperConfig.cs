
using AutoMapper;

namespace HomeRunner.Api.Service
{
    public static class AutoMapperConfig
    {
        public static void Config()
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<Domain.ReadModel.Platform.TaskActivities.Entities.TaskActivity, V1.Platform.Representations.TaskActivity>();
            });
        }
    }
}
