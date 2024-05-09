using AutoMapper;

namespace NCA.Common.Application.Maps
{
    public interface IMapFrom<T>
    {
        void Map(Profile profile) => profile.CreateMap(typeof(T), GetType());
    }
}
