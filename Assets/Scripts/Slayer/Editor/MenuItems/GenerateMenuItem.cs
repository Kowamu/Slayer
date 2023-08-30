using Slayer.Editor.Generators;
using UnityEditor;

namespace Slayer.Editor.TooltipGenerators
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
                @"Assets\Scripts\Slayer\Data",
                @"Assets\Scripts\Slayer\Data\Generated\MessagePack");
        }
        
        [MenuItem("Generator/MasterMemory")]
        private static void GenerateSlayerMasterMemoryCode()
        {
            _masterMemoryCodeGenerator.Generate(
                @"Assets\Scripts\Slayer\Data",
                @"Assets\Scripts\Slayer\Data\Generated\MasterMemory",
                "Slayer.Data.Tables");
        }
    }
}