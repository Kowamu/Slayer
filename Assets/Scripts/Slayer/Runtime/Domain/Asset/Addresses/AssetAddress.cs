using System;
using System.Runtime.Serialization;

namespace Slayer.Runtime.Domains.Assets.Addresses
{
    /// <summary>
    /// Represents an asset address in the game.
    /// </summary>
    [Serializable]
    public sealed class AssetAddress : IEquatable<AssetAddress>, ISerializable
    {
        // --- field ---
        
        /// <summary>
        /// The address of the asset.
        /// </summary>
        public string Address { get; }

        // --- ctor ---
        
        /// <summary>
        /// Initializes a new instance of the AssetAddress class with the specified address.
        /// </summary>
        /// <param name="address">The address of the asset.</param>
        public AssetAddress(string address)
        {
            Validate(address);
            Address = address;
        }

        private AssetAddress(SerializationInfo info, StreamingContext context)
        {
            Address = info.GetString(nameof(Address));
        }
        
        // --- operator ---
        
        public static bool operator ==(AssetAddress left, AssetAddress right) => Equals(left, right);

        public static bool operator !=(AssetAddress left, AssetAddress right) => !Equals(left, right);
        
        // --- method ---

        public override bool Equals(object obj) => Equals(obj as AssetAddress);
        
        public bool Equals(AssetAddress other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Address == other.Address;
        }

        public override int GetHashCode() => Address.GetHashCode();

        public override string ToString() => Address;

        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Address), Address);
        }
        
        // --- static method ---

        /// <summary>
        /// Validates the address for the asset. Throws an ArgumentException if the address is null or empty.
        /// </summary>
        /// <param name="address">The address to be validated.</param>
        private static void Validate(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
            {
                throw new ArgumentException($"Invalid address: {address}");
            }
        }
    }
}