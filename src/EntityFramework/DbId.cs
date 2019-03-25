using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace System.Data.Entity
{
    public partial struct DbId<T> : IConvertible, IComparable, IComparable<DbId<T>>, IEquatable<DbId<T>>, IEquatable<int> /*where T : DbEntityBase*/
    {
        private int _Id;

        public DbId(int value) : this()
        {
            _Id = value;
        }

        public static explicit operator int(DbId<T> id)
        {
            return id._Id;
        }

        public static implicit operator DbId<T>(int value)
        {
            return new DbId<T>(value);
        }

        public bool Equals(DbId<T> other)
        {
            return other._Id == _Id;
        }

        public override bool Equals(object obj)
        {
            return obj is DbId<T> && Equals((DbId<T>)obj);
        }

        public override int GetHashCode()
        {
            return _Id.GetHashCode();
        }

        public override string ToString()
        {
            return _Id.ToString();
        }

        public static bool operator ==(DbId<T> x, DbId<T> y) => x._Id == y._Id;

        public static bool operator !=(DbId<T> x, DbId<T> y) => x._Id != y._Id;

        public static bool operator >(DbId<T> x, DbId<T> y) => x._Id > y._Id;

        public static bool operator <(DbId<T> x, DbId<T> y) => x._Id < y._Id;

        public static bool operator >=(DbId<T> x, DbId<T> y) => x._Id >= y._Id;

        public static bool operator <=(DbId<T> x, DbId<T> y) => x._Id <= y._Id;

        public int CompareTo(object other)
        {
            if (!(other is DbId<T>) && !(other is int))
            {
                throw new ArgumentException($"'other' must be of type {nameof(DbId<T>)} or of type int");
            }

            if (other == null)
            {
                return 1;
            }

            var otherId = ((DbId<T>)other)._Id;
            if (_Id > otherId)
            {
                return 1;
            }

            if (_Id < otherId)
            {
                return -1;
            }

            return 0;
        }

        public bool Equals(int other)
        {
            return _Id.Equals(other);
        }

        public int CompareTo(DbId<T> other)
        {
            return _Id.CompareTo(other._Id);
        }

        public TypeCode GetTypeCode()
        {
            return _Id.GetTypeCode();
        }

        public bool ToBoolean(IFormatProvider provider)
        {
            return ((IConvertible)_Id).ToBoolean(provider);
        }

        public char ToChar(IFormatProvider provider)
        {
            return ((IConvertible)_Id).ToChar(provider);
        }

#pragma warning disable CS3002 // Return type is not CLS-compliant
        public sbyte ToSByte(IFormatProvider provider)
#pragma warning restore CS3002 // Return type is not CLS-compliant
        {
            return ((IConvertible)_Id).ToSByte(provider);
        }

        public byte ToByte(IFormatProvider provider)
        {
            return ((IConvertible)_Id).ToByte(provider);
        }

        public short ToInt16(IFormatProvider provider)
        {
            return ((IConvertible)_Id).ToInt16(provider);
        }

#pragma warning disable CS3002 // Return type is not CLS-compliant
        public ushort ToUInt16(IFormatProvider provider)
#pragma warning restore CS3002 // Return type is not CLS-compliant
        {
            return ((IConvertible)_Id).ToUInt16(provider);
        }

        public int ToInt32(IFormatProvider provider)
        {
            return ((IConvertible)_Id).ToInt32(provider);
        }

#pragma warning disable CS3002 // Return type is not CLS-compliant
        public uint ToUInt32(IFormatProvider provider)
#pragma warning restore CS3002 // Return type is not CLS-compliant
        {
            return ((IConvertible)_Id).ToUInt32(provider);
        }

        public long ToInt64(IFormatProvider provider)
        {
            return ((IConvertible)_Id).ToInt64(provider);
        }

#pragma warning disable CS3002 // Return type is not CLS-compliant
        public ulong ToUInt64(IFormatProvider provider)
#pragma warning restore CS3002 // Return type is not CLS-compliant
        {
            return ((IConvertible)_Id).ToUInt64(provider);
        }

        public float ToSingle(IFormatProvider provider)
        {
            return ((IConvertible)_Id).ToSingle(provider);
        }

        public double ToDouble(IFormatProvider provider)
        {
            return ((IConvertible)_Id).ToDouble(provider);
        }

        public decimal ToDecimal(IFormatProvider provider)
        {
            return ((IConvertible)_Id).ToDecimal(provider);
        }

        public DateTime ToDateTime(IFormatProvider provider)
        {
            return ((IConvertible)_Id).ToDateTime(provider);
        }

        public string ToString(IFormatProvider provider)
        {
            return _Id.ToString(provider);
        }

        public object ToType(Type conversionType, IFormatProvider provider)
        {
            return ((IConvertible)_Id).ToType(conversionType, provider);
        }
    }
}

