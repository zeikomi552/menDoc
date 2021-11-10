using menDoc.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace menDoc.Common.Converters
{
	[System.Windows.Data.ValueConversion(typeof(gRPCTypeEnum), typeof(string))]
	public class gRPCTypeToStringConverter : System.Windows.Data.IValueConverter
	{

		#region IValueConverter メンバ
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			var target = (gRPCTypeEnum)value;

			// ここに処理を記述する
			return gRPCTypeEnum.bool_.ToString().Replace("_","");
		}

		// TwoWayの場合に使用する
		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
		#endregion
	}


}
