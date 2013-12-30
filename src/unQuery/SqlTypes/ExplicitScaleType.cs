﻿using Microsoft.SqlServer.Server;
using System.Data;
using System.Data.SqlClient;

namespace unQuery.SqlTypes
{
	public abstract class ExplicitScaleType<TValue> : SqlType, ISqlType, ITypeHandler
	{
		private readonly TValue value;
		private readonly byte? scale;
		private readonly bool hasValue;
		private readonly SqlDbType dbType;

		internal ExplicitScaleType(SqlDbType dbType)
		{
			this.dbType = dbType;
		}

		internal ExplicitScaleType(TValue value, byte? scale, SqlDbType dbType)
		{
			this.value = value;
			this.scale = scale;
			this.dbType = dbType;

			hasValue = true;
		}

		object ISqlType.GetRawValue()
		{
			return value;
		}

		SqlParameter ISqlType.GetParameter()
		{
			var param = new SqlParameter {
				SqlDbType = dbType,
				Value = GetDBNullableValue(value)
			};

			if (scale != null)
				param.Scale = scale.Value;

			return param;
		}

		SqlParameter ITypeHandler.CreateParamFromValue(object value)
		{
			throw new TypeCannotBeUsedAsAClrTypeException();
		}

		SqlMetaData ITypeHandler.CreateMetaData(string name)
		{
			if (!hasValue)
				throw new TypeCannotBeUsedAsAClrTypeException();

			if (scale == null)
				throw new TypePropertiesMustBeSetExplicitlyException("scale");

			return new SqlMetaData(name, dbType, 0, scale.Value);
		}
	}
}