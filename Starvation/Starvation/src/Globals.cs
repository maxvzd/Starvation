using Vintagestory.API.Common;

namespace Starvation;

public static class Globals
{
     public static ICoreAPI? CoreApiInstance
     {
         get => _coreApiInstance;
         set => _coreApiInstance ??= value;
     }

    private static ICoreAPI? _coreApiInstance;
}