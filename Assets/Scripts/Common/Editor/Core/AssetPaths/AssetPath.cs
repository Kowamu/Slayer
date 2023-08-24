using System;
using System.Runtime.Serialization;

namespace WanwanLand.Common.Editor.Core.AssetPaths
{
    /// <summary>
    /// Represents a value object class of the asset path.
    /// </summary>
    [Serializable]
    public sealed class AssetPath : IEquatable<AssetPath>, ISerializable
    {
        // --- field ---

        /// <summary>
        /// Represents the root directory of the asset path.
        /// </summary>
        private const string RootDirectory = "Assets";
        
        /// <summary>
        /// Gets the asset directory path.
        /// </summary>
        public string Value { get; }
        
        // --- ctor ---

        /// <summary>
        /// Initializes a new instance of AssetPath with the specified path.
        /// </summary>
        /// <param name="value">The asset directory path.</param>
        public AssetPath(string value)
        {
            Validate(value);
            Value = value;
        }

        private AssetPath(SerializationInfo info, StreamingContext context)
        {
            Value = info.GetString(nameof(Value));
        }
        
        // --- operator ---
        
        public static bool operator ==(AssetPath left, AssetPath right) => Equals(left, right);

        public static bool operator !=(AssetPath left, AssetPath right) => !Equals(left, right);
        
        // --- method ---
        
        public bool Equals(AssetPath other) => other != null && Value == other.Value;

        public override bool Equals(object obj) => Equals(obj as AssetPath);

        public override int GetHashCode() => Value.GetHashCode();
        
        public override string ToString() => Value;
        
        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Value), Value);
        }

        // --- static method ---

        /// <summary>
        /// Validates the specified path. Throws an ArgumentException if the path is null or empty or it does not start with the root directory.
        /// </summary>
        /// <param name="path">The asset directory path to validate.</param>
        public static void Validate(string path)
        {
            if (string.IsNullOrEmpty(path)) throw new ArgumentException("Path cannot be null or empty");
            if (!path.StartsWith(RootDirectory)) throw new ArgumentException($"Path must start with {RootDirectory}");
        }
    }
}