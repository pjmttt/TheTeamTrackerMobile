using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace TheTeamTracker.Mobile.Classes
{
	public class DecimalConverter : IValueConverter
	{

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null)
				return "0";
			decimal thedecimal = (decimal)value;
			return thedecimal.ToString();
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			string strValue = value as string;
			if (string.IsNullOrEmpty(strValue))
				strValue = "0";
			decimal resultdecimal;
			if (decimal.TryParse(strValue, out resultdecimal))
			{
				return resultdecimal;
			}
			return 0;
		}
	}

	public class IntegerConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null)
				return "0";
			return ((int)value).ToString();
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			string strValue = value as string;
			if (string.IsNullOrEmpty(strValue))
				strValue = "0";
			int result;
			if (int.TryParse(strValue, out result))
			{
				return result;
			}
			return 0;
		}

	}

	public class NullConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value ?? Activator.CreateInstance(targetType);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value;
		}
	}
}
