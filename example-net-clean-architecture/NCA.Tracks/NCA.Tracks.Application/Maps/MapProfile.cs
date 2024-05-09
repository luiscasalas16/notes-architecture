using System.Reflection;

namespace NCA.Tracks.Application.Maps
{
    public class MapProfile : MapProfileBase
    {
        public MapProfile()
            : base(Assembly.GetExecutingAssembly()) { }
    }
}
