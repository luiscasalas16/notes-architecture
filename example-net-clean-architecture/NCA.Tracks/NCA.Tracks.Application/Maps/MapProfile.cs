using System.Reflection;
using NCA.Common.Application.Maps;

namespace NCA.Production.Application.Maps
{
    public class MapProfile : MapProfileBase
    {
        public MapProfile()
            : base(Assembly.GetExecutingAssembly()) { }
    }
}
