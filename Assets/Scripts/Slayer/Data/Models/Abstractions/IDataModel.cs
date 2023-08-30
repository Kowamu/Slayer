using System;
using MasterMemory;

namespace Slayer.Data.Models.Abstractions
{ 
    public interface IDataModel<TSelf> : IEquatable<TSelf>, IValidatable<TSelf>
        where TSelf : IDataModel<TSelf>
    {
        bool Equals(object obj);

        int GetHashCode();
        
        string ToString();
    }
}