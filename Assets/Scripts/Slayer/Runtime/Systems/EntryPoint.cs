using MessagePack;
using MessagePack.Resolvers;
using UnityEngine;

namespace Common.Core
{
    internal static class EntryPoint
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
        private static void Entry()
        {
            SetUp();
        }

        private static void SetUp()
        {
#if UNITY_EDITOR //Domain Reload
            if (MessagePackSerializerOptions.Standard == MessagePackSerializer.DefaultOptions)
            {
#endif
                StaticCompositeResolver.Instance.Register(
                    //MasterMemoryResolver.Instance,
                    //GeneratedResolver.Instance,
                    StandardResolver.Instance);

                var options = MessagePackSerializerOptions.Standard.WithResolver(StaticCompositeResolver.Instance);
                MessagePackSerializer.DefaultOptions = options;
#if UNITY_EDITOR
            }
#endif
        }
    }
}
