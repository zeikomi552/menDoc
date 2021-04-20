using menDoc.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace menDoc.Common.Converters
{
	[System.Windows.Data.ValueConversion(typeof(Multiplicity), typeof(string))]
	public class MultiplicityToImagePathConverter : System.Windows.Data.IValueConverter
	{
		#region IValueConverter メンバ
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			Multiplicity target = (Multiplicity)value;

			string direction = parameter.ToString();

			if (direction.Equals("left"))
			{
				switch (target)
				{
					case Multiplicity.OneOne:
					default:
						return "/Common/Themes/Icons/multiplicity-oneone-left.png";
					case Multiplicity.OneMulti:
						return "/Common/Themes/Icons/multiplicity-onemulti-left.png";
					case Multiplicity.ZeroMulti:
						return "/Common/Themes/Icons/multiplicity-zeromulti-left.png";
					case Multiplicity.ZeroOne:
						return "/Common/Themes/Icons/multiplicity-zeroone-left.png";
				}
			}
			else
			{
				switch (target)
				{
					case Multiplicity.OneOne:
					default:
						return "/Common/Themes/Icons/multiplicity-oneone-right.png";
					case Multiplicity.OneMulti:
						return "/Common/Themes/Icons/multiplicity-onemulti-right.png";
					case Multiplicity.ZeroMulti:
						return "/Common/Themes/Icons/multiplicity-zeromulti-right.png";
					case Multiplicity.ZeroOne:
						return "/Common/Themes/Icons/multiplicity-zeroone-right.png";
				}
			}
		}

		// TwoWayの場合に使用する
		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		#endregion
	}

}
