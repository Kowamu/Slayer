using Unity.Entities;
using Unity.Mathematics;

namespace Unity.Rendering
{
    [MaterialProperty("_TintColor")]
    struct TintColorVector4Override : IComponentData
    {
        public float4 Value;
    }
}
