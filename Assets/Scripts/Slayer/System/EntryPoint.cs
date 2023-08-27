using MessagePack;
using MessagePack.Resolvers;
using Slayer.Runtime.Domain.Generated;
using UnityEngine;

namespace WanwanLand.Slayer.System
{
    internal static class EntryPoint
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
        private static void Entry()
        {
            StartUp();
        }

        private static void StartUp()
        {
#if UNITY_EDITOR
            if (MessagePackSerializerOptions.Standard == MessagePackSerializer.DefaultOptions)
            {
#endif
                StaticCompositeResolver.Instance.Register(
                    MasterMemoryResolver.Instance, 
                    GeneratedResolver.Instance,
                    StandardResolver.Instance);

                var options = MessagePackSerializerOptions.Standard.WithResolver(StaticCompositeResolver.Instance);
                MessagePackSerializer.DefaultOptions = options;
#if UNITY_EDITOR
            }
#endif
        }
    }
}
