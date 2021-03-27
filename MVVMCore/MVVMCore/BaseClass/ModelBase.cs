using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMCore.BaseClass
{
    public class ModelBase : INotifyPropertyChanged
    {
		#region INotifyPropertyChanged 
		public event PropertyChangedEventHandler PropertyChanged;

		protected void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}
		#endregion
	}
}
