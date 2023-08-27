using System;
using System.Runtime.Serialization;

namespace WanwanLand.Slayer.Editor.Domain.Assets
{
    /// <summary>
    /// アセットパスの値オブジェクトクラスを表す
    /// </summary>
    [Serializable]
    public sealed class AssetPath : IEquatable<AssetPath>, ISerializable
    {
        // --- フィールド ---

        /// <summary>
        /// アセットパスのルートディレクトリを表す
        /// </summary>
        private const string RootDirectory = "Assets";
        
        /// <summary>
        /// アセットディレクトリパスを取得する
        /// </summary>
        public string Value { get; }
        
        // --- コンストラクタ ---

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="path">アセットディレクトリパス</param>
        public AssetPath(string path)
        {
            var normalizedPath = Normalize(path);
            Validate(normalizedPath);
            Value = normalizedPath;
        }

        private AssetPath(SerializationInfo info, StreamingContext context)
        {
            Value = info.GetString(nameof(Value));
        }
        
        // --- 演算子 ---
        
        public static bool operator ==(AssetPath left, AssetPath right) => Equals(left, right);

        public static bool operator !=(AssetPath left, AssetPath right) => !Equals(left, right);
        
        /// <summary>
        /// stringからAssetPathへの暗黙的な変換演算子
        /// </summary>
        /// <param name="path">AssetPathに変換する文字列</param>
        public static implicit operator AssetPath(string path) => new(path);
        
        // --- メソッド ---
        
        public bool Equals(AssetPath other) => other != null && Value == other.Value;

        public override bool Equals(object obj) => Equals(obj as AssetPath);

        public override int GetHashCode() => Value.GetHashCode();
        
        public override string ToString() => Value;
        
        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Value), Value);
        }

        // --- staticメソッド ---
        
        /// <summary>
        /// 指定したパスを正規化する
        /// </summary>
        /// <param name="path">正規化するアセットディレクトリパス</param>
        private static string Normalize(string path)
        {
            return path.Replace('\\', '/');
        }

        /// <summary>
        /// 指定したパスを検証する。パスがnullまたは空、またはルートディレクトリで始まらない場合、ArgumentExceptionをスローする
        /// </summary>
        /// <param name="path">検証するアセットディレクトリパス</param>
        private static void Validate(string path)
        {
            if (string.IsNullOrEmpty(path)) throw new ArgumentException("パスはnullまたは空にできません");
            if (!path.StartsWith(RootDirectory)) throw new ArgumentException($"{RootDirectory}で始まるパスでなければなりません");
        }
    }
}