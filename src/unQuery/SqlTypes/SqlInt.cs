﻿using System.Data;
using System.Data.SqlClient;

namespace unQuery.SqlTypes
{
	public class SqlInt : ISqlType
	{
		private readonly int? value;

		public SqlInt(int? value)
		{
			this.value = value;
		}

		public SqlParameter GetParameter()
		{
			return GetParameter(value);
		}

		public static SqlParameter GetParameter(object value)
		{
			return new SqlParameter {
				SqlDbType = SqlDbType.Int,
				Value = value
			};
		}
	}
}