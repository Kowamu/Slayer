using Slayer.Runtime.Domain.Generated.Tables;
using WanwanLand.Slayer.Domain.Asset.SpriteSheets;

namespace Slayer.Runtime.Domain.Asset.SpriteSheets
{
    public class SpriteSheetRepository
    {
        SpriteSheetTable _table;
        
        public SpriteSheetRepository(SpriteSheetTable table)
        {
            _table = table;
        }
        
        public SpriteSheet FindById(int spriteSheetId)
        {
            return _table.FindBySpriteSheetId(spriteSheetId);
        }
    }
}