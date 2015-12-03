
using AutoMapper;

namespace HomeRunner.Foundation.Web
{
    public static class MapExtension
    {
        public static TMapped Map<TMapped>(this object instance)
            where TMapped : class, new()
        {
            TMapped mapping = Mapper.Map(instance, new TMapped(), instance.GetType(), typeof(TMapped)) as TMapped;

            return mapping;
        }
    }
}