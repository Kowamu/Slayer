using Unity.Entities;
using Unity.Mathematics;

namespace Unity.Rendering
{
    [MaterialProperty("_TileIndex")]
    struct TileIndexFloatOverride : IComponentData
    {
        public float Value;
    }
}
