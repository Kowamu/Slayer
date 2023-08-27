using System;
using MessagePack;

namespace WanwanLand.Slayer.Domain.Asset.Addresses
{
    /// <summary>
    /// Addressableのアセットのアドレスを表す
    /// </summary>
    [MessagePackObject(keyAsPropertyName: true)]
    public sealed class AssetAddress : IEquatable<AssetAddress>
    {
        // --- フィールド ---
        
        /// <summary>
        /// アセットのアドレス
        /// </summary>
        public string Value { get; }

        // --- コンストラクタ ---
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="value">アセットのアドレス</param>
        public AssetAddress(string value)
        {
            Validate(value);
            Value = value;
        }
        
        // --- 演算子 ---
        
        public static bool operator ==(AssetAddress left, AssetAddress right) => Equals(left, right);
        
        public static bool operator !=(AssetAddress left, AssetAddress right) => !Equals(left, right);
        
        /// <summary>
        /// stringからAssetAddressへの暗黙的な変換演算子
        /// </summary>
        /// <param name="value">AssetAddressに変換される文字列</param>
        public static implicit operator AssetAddress(string value) => new(value);
        
        // --- メソッド ---
        
        public bool Equals(AssetAddress other) => other != null && Value == other.Value;
        
        public override bool Equals(object obj) => Equals(obj as AssetAddress);
        
        public override int GetHashCode() => Value.GetHashCode();
        
        public override string ToString() => Value;
        
        // --- staticメソッド ---

        /// <summary>
        /// アセットのアドレスを検証します
        /// </summary>
        /// <param name="value">検証するアドレス</param>
        /// <exception cref="ArgumentException">アドレスがnullまたは空の場合</exception>
        private static void Validate(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException($"無効なアドレス: {value}");
        }
    }
}