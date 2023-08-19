using MasterMemory;
using MessagePack;

namespace Slayer.Domains
{
    [MemoryTable("sprite_sheet"), MessagePackObject(true)]
    public class SpriteSheet
    {
        [PrimaryKey]
        public int SpriteSheetId { get; }
        
        public string MaterialPath { get; }

        public SpriteSheet(int spriteSheetId, string materialPath)
        {
            SpriteSheetId = spriteSheetId;
            MaterialPath = materialPath;
        }
    }
}