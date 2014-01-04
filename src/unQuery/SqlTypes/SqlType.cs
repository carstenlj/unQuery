﻿using System;

namespace unQuery.SqlTypes
{
	public abstract class SqlType
	{
		protected object GetDBNullableValue(object value)
		{
			return value ?? DBNull.Value;
		}

		protected int GetAppropriateSizeFromLength(int length)
		{
			if (length <= 64)
				return 64;

			if (length <= 256)
				return 256;

			if (length <= 1024)
				return 1024;

			if (length <= 4096)
				return 4096;

			return 8000;
		}
	}
}