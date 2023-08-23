using System;
using System.Diagnostics.CodeAnalysis;
using MasterMemory;
using MessagePack;

namespace WanwanLand.Slayer.Domain.Asset.Sprites
{
    /// <summary>
    /// Represents a sprite sheet in the game.
    /// </summary>
    [MemoryTable("sprite_sheet"), MessagePackObject(true)]
    public sealed class SpriteSheet : IEquatable<SpriteSheet>
    {
        // --- field ---
        
        /// <summary>
        /// Gets the identifier of the sprite sheet.
        /// </summary>
        [PrimaryKey]
        public int SpriteSheetId { get; }
        
        /// <summary>
        /// Gets the asset address of the sprite.
        /// </summary>
        [NotNull]
        public string SpriteAssetAddress { get; }
        
        /// <summary>
        /// Gets the number of vertical divisions in the sprite sheet.
        /// </summary>
        public int VerticalDivisionCount { get; }
        
        /// <summary>
        /// Gets the number of horizontal divisions in the sprite sheet.
        /// </summary>
        public int HorizontalDivisionCount { get; }

        // --- ctor ---
        
        /// <summary>
        /// Initializes a new instance of the SpriteSheet class with the specified sprite sheet id, sprite asset address, vertical division count, and horizontal division count.
        /// </summary>
        /// <param name="spriteSheetId">The identifier of the sprite sheet.</param>
        /// <param name="spriteAssetAddress">The asset address of the sprite.</param>
        /// <param name="verticalDivisionCount">The number of vertical divisions in the sprite sheet.</param>
        /// <param name="horizontalDivisionCount">The number of horizontal divisions in the sprite sheet.</param>
        public SpriteSheet(int spriteSheetId, [DisallowNull] string spriteAssetAddress, int verticalDivisionCount, int horizontalDivisionCount)
        {
            SpriteSheetId = spriteSheetId;
            SpriteAssetAddress = spriteAssetAddress;
            VerticalDivisionCount = verticalDivisionCount;
            HorizontalDivisionCount = horizontalDivisionCount;
        }
        
        // --- method ---

        public bool Equals(SpriteSheet other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            if (SpriteSheetId != other.SpriteSheetId) return false;
            if (SpriteAssetAddress != other.SpriteAssetAddress) return false;
            if (VerticalDivisionCount != other.VerticalDivisionCount) return false;
            if (HorizontalDivisionCount != other.HorizontalDivisionCount) return false;
            return true;
        }
        
        public override bool Equals(object obj) => Equals(obj as SpriteSheet);
    }
}