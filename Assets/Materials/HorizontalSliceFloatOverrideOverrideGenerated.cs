using Unity.Entities;
using Unity.Mathematics;

namespace Unity.Rendering
{
    [MaterialProperty("_HorizontalSlice")]
    struct HorizontalSliceFloatOverride : IComponentData
    {
        public float Value;
    }
}
