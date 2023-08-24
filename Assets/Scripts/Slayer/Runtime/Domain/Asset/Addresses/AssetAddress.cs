using System;
using MessagePack;

namespace Slayer.Runtime.Domain.Asset.Addresses
{
    /// <summary>
    /// Represents an asset address in the game.
    /// </summary>
    [MessagePackObject(true)]
    public sealed class AssetAddress : IEquatable<AssetAddress>
    {
        // --- field ---
        
        /// <summary>
        /// The address of the asset.
        /// </summary>
        public string Value { get; }

        // --- ctor ---
        
        /// <summary>
        /// Initializes a new instance of the AssetAddress class with the specified address.
        /// </summary>
        /// <param name="value">The address of the asset.</param>
        public AssetAddress(string value)
        {
            Validate(value);
            Value = value;
        }
        
        // --- operator ---
        
        public static bool operator ==(AssetAddress left, AssetAddress right) => Equals(left, right);
        
        public static bool operator !=(AssetAddress left, AssetAddress right) => !Equals(left, right);
        
        // --- method ---
        public bool Equals(AssetAddress other) => other != null && Value == other.Value;
        
        public override bool Equals(object obj) => Equals(obj as AssetAddress);
        
        public override int GetHashCode() => Value.GetHashCode();
        
        public override string ToString() => Value;
        
        // --- static method ---

        /// <summary>
        /// Validates the address for the asset. Throws an ArgumentException if the address is null or empty.
        /// </summary>
        /// <param name="address">The address to be validated.</param>
        private static void Validate(string address)
        {
            if (string.IsNullOrWhiteSpace(address)) throw new ArgumentException($"Invalid address: {address}");
        }
    }
}