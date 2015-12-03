
using AutoMapper;

namespace HomeRunner.Api.Host
{
    public static class AutoMapperConfig
    {
        public static void Config()
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<Domain.Service.Platform.ITaskActivity, Rest.Service.Platform.Representation.TaskActivity>();
            });
        }
    }
}