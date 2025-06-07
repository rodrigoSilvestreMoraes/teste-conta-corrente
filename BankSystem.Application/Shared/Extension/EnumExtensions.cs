﻿using System.ComponentModel;
using System.Reflection;

namespace bank.system.Application.Shared.Extension
{
	public static class EnumExtensions
	{
		public static string GetDescription(this Enum value)
		{
			var field = value.GetType().GetField(value.ToString());
			var attr = field?.GetCustomAttribute<DescriptionAttribute>();
			return attr?.Description ?? value.ToString();
		}
	}
}
