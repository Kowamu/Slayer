using System;
using System.Diagnostics.CodeAnalysis;
using MasterMemory;
using MessagePack;
using Slayer.Data.Models.Abstractions;
using Slayer.Data.Models.Assets.Addresses;

namespace Slayer.Data.Models.Assets.SpriteSheets
{
    /// <summary>
    /// ゲーム内のスプライトシートを表す
    /// </summary>
    [MemoryTable("sprite_sheet")]
    [MessagePackObject(keyAsPropertyName: true)]
    public sealed class SpriteSheetDataModel : IDataModel<SpriteSheetDataModel>
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
        public AssetAddressDataModel SpriteAssetAddress { get; }
        
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
        public SpriteSheetDataModel(int spriteSheetId, [DisallowNull] AssetAddressDataModel spriteAssetAddress, int verticalDivisionCount, int horizontalDivisionCount)
        {
            SpriteSheetId = spriteSheetId;
            SpriteAssetAddress = spriteAssetAddress;
            VerticalDivisionCount = verticalDivisionCount;
            HorizontalDivisionCount = horizontalDivisionCount;
        }
        
        // --- 演算子 ---
        
        public static bool operator ==(SpriteSheetDataModel left, SpriteSheetDataModel right) => Equals(left, right);
        public static bool operator !=(SpriteSheetDataModel left, SpriteSheetDataModel right) => !Equals(left, right);
        
        // --- メソッド ---

        void IValidatable<SpriteSheetDataModel>.Validate(IValidator<SpriteSheetDataModel> validator)
        {
            validator.Validate(self => self.SpriteSheetId > 0);
            validator.Validate(self => self.SpriteAssetAddress != null);
            validator.Validate(self => self.VerticalDivisionCount > 0);
            validator.Validate(self => self.HorizontalDivisionCount > 0);
        }
        
        public bool Equals(SpriteSheetDataModel other)
        {
            return 
                other != null &&
                SpriteSheetId == other.SpriteSheetId &&
                SpriteAssetAddress == other.SpriteAssetAddress && 
                VerticalDivisionCount == other.VerticalDivisionCount && 
                HorizontalDivisionCount == other.HorizontalDivisionCount;
        }
        
        public override bool Equals(object obj) => Equals(obj as SpriteSheetDataModel);

        public override int GetHashCode()
        {
            return HashCode.Combine(SpriteSheetId, SpriteAssetAddress, VerticalDivisionCount, HorizontalDivisionCount);
        }

        public override string ToString() => MessagePackSerializer.SerializeToJson(this);
    }
}