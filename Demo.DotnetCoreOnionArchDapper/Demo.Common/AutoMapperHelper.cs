using AutoMapper;

namespace Demo.Common
{
    public static class AutoMapperHelper
    {
        public static async Task<TDestination> MapAsync<TSource, TDestination>(
                this IMapper mapper, Task<TSource> sourceTask)
        {
            var source = await sourceTask.ConfigureAwait(false);
            return mapper.Map<TDestination>(source);
        }

    }
}
