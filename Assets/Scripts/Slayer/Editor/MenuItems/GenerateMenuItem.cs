using UnityEditor;
using WanwanLand.Slayer.Editor.Generators;

namespace WanwanLand.Slayer.Editor.TooltipGenerators
{
    public static class GenerateMenuItem
    {
        private static MessagePackCodeGenerator _messagePackCodeGenerator = new();
        private static MasterMemoryCodeGenerator _masterMemoryCodeGenerator = new();

        [MenuItem("Generator/All")]
        private static void GenerateAllCode()
        {
            GenerateSlayerMessagePackCode();
            GenerateSlayerMasterMemoryCode();
        }
        
        [MenuItem("Generator/MessagePack")]
        private static void GenerateSlayerMessagePackCode()
        {
            _messagePackCodeGenerator.Generate(
                @"Assets\Scripts\Slayer\Domain",
                @"Assets\Scripts\Slayer\Domain\Generated\MessagePack");
        }
        
        [MenuItem("Generator/MasterMemory")]
        private static void GenerateSlayerMasterMemoryCode()
        {
            _masterMemoryCodeGenerator.Generate(
                @"Assets\Scripts\Slayer\Domain",
                @"Assets\Scripts\Slayer\Domain\Generated\MasterMemory",
                "WanwanLand.Slayer.Runtime.Domain.Generated");
        }
    }
}