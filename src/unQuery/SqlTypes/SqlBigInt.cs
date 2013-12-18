﻿using Microsoft.SqlServer.Server;
using System.Data;
using System.Data.SqlClient;

namespace unQuery.SqlTypes
{
	public class SqlBigInt : SqlType, ISqlType
	{
		private readonly long? value;

		public SqlBigInt(long? value)
		{
			this.value = value;
		}

		public static explicit operator SqlBigInt(long? value)
		{
			return new SqlBigInt(value);
		}

		public SqlParameter GetParameter()
		{
			return GetParameter(value);
		}

		public object GetRawValue()
		{
			return value;
		}

		internal static SqlParameter GetParameter(long? value)
		{
			return new SqlParameter {
				SqlDbType = SqlDbType.BigInt,
				Value = GetDBNullableValue(value)
			};
		}
	}

	internal class SqlBigIntTypeHandler : ITypeHandler
	{
		private static readonly SqlBigIntTypeHandler instance = new SqlBigIntTypeHandler();

		internal static SqlBigIntTypeHandler GetInstance()
		{
			return instance;
		}

		public SqlParameter CreateParamFromValue(object value)
		{
			return SqlBigInt.GetParameter((long?)value);
		}

		public SqlDbType GetSqlDbType()
		{
			return SqlDbType.BigInt;
		}

		public SqlMetaData CreateSqlMetaData(string name)
		{
			return new SqlMetaData(name, SqlDbType.BigInt);
		}
	}
}