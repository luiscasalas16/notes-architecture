using AutoMapper;

namespace NCA.Common.Application.Maps
{
    public interface IMapTo<T>
    {
        void Map(Profile profile) => profile.CreateMap(GetType(), typeof(T));
    }
}
