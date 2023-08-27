using System;
using System.Diagnostics.CodeAnalysis;
using MasterMemory;
using MessagePack;
using WanwanLand.Slayer.Domain.Asset.Addresses;

namespace WanwanLand.Slayer.Domain.Asset.SpriteSheets
{
    /// <summary>
    /// ゲーム内のスプライトシートを表す
    /// </summary>
    [MemoryTable("sprite_sheet")]
    [MessagePackObject(keyAsPropertyName: true)]
    public sealed class SpriteSheet : IEquatable<SpriteSheet>
    {
        // --- フィールド ---
        
        /// <summary>
        /// スプライトシートの識別子を取得する
        /// </summary>
        [PrimaryKey]
        public int SpriteSheetId { get; }
        
        /// <summary>
        /// スプライトのアセットアドレスを取得する
        /// </summary>
        [NotNull]
        public AssetAddress SpriteAssetAddress { get; }
        
        /// <summary>
        /// スプライトシート内の垂直方向の分割数を取得する
        /// </summary>
        public int VerticalDivisionCount { get; }
        
        /// <summary>
        /// スプライトシート内の水平方向の分割数を取得する
        /// </summary>
        public int HorizontalDivisionCount { get; }

        // --- コンストラクタ ---
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="spriteSheetId">スプライトシートの識別子</param>
        /// <param name="spriteAssetAddress">スプライトのアセットアドレス</param>
        /// <param name="verticalDivisionCount">スプライトシート内の垂直方向の分割数</param>
        /// <param name="horizontalDivisionCount">スプライトシート内の水平方向の分割数</param>
        public SpriteSheet(int spriteSheetId, [DisallowNull] AssetAddress spriteAssetAddress, int verticalDivisionCount, int horizontalDivisionCount)
        {
            SpriteSheetId = spriteSheetId;
            SpriteAssetAddress = spriteAssetAddress;
            VerticalDivisionCount = verticalDivisionCount;
            HorizontalDivisionCount = horizontalDivisionCount;
        }
        
        // --- メソッド ---

        public bool Equals(SpriteSheet other)
        {
            return 
                other != null &&
                SpriteSheetId == other.SpriteSheetId &&
                SpriteAssetAddress == other.SpriteAssetAddress && 
                VerticalDivisionCount == other.VerticalDivisionCount && 
                HorizontalDivisionCount == other.HorizontalDivisionCount;
        }
        
        public override bool Equals(object obj) => Equals(obj as SpriteSheet);

        public override int GetHashCode()
        {
            return HashCode.Combine(SpriteSheetId, SpriteAssetAddress, VerticalDivisionCount, HorizontalDivisionCount);
        }
    }
}