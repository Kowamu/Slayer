using Unity.Entities;
using Unity.Mathematics;

namespace Unity.Rendering
{
    [MaterialProperty("_VerticalSlice")]
    struct VerticalSliceFloatOverride : IComponentData
    {
        public float Value;
    }
}
