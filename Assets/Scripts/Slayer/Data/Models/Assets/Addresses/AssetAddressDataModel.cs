using System.IO;
using MasterMemory;
using MessagePack;
using Slayer.Data.Models.Abstractions;

namespace Slayer.Data.Models.Assets.Addresses
{
    /// <summary>
    /// Addressableのアドレスを表すデータモデル
    /// </summary>
    [MessagePackObject(keyAsPropertyName: true)]
    public sealed class AssetAddressDataModel : IDataModel<AssetAddressDataModel>
    {
        // --- フィールド ---
        
        /// <summary>
        /// アセットのアドレス
        /// </summary>
        public string Value { get; }

        // --- コンストラクタ ---
        
        public AssetAddressDataModel(string value)
        {
            Value = value;
        }
        
        // --- 演算子 ---
        
        public static bool operator ==(AssetAddressDataModel left, AssetAddressDataModel right) => Equals(left, right);
        public static bool operator !=(AssetAddressDataModel left, AssetAddressDataModel right) => !Equals(left, right);
        
        // --- メソッド ---
        
        void IValidatable<AssetAddressDataModel>.Validate(IValidator<AssetAddressDataModel> validator)
        {
            validator.Validate(self => !string.IsNullOrWhiteSpace(self.Value));
            validator.Validate(self => !self.Value.Contains(Path.DirectorySeparatorChar));
            validator.Validate(self => !self.Value.Contains(Path.AltDirectorySeparatorChar));
        }
        
        public bool Equals(AssetAddressDataModel other) => other != null && Value == other.Value;

        public override bool Equals(object obj) => Equals(obj as AssetAddressDataModel);
        
        public override int GetHashCode() => Value.GetHashCode();
        
        public override string ToString() => MessagePackSerializer.SerializeToJson(this);
    }
}