using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace TheTeamTracker.Mobile.Classes
{
    public class DisplayItem : INotifyPropertyChanged
    {
		public string Line1 { get; set; }
		public string Line2 { get; set; }
		public string Line3 { get; set; }
		public string Line4 { get; set; }

		public object Tag { get; set; }
		public object Tag2 { get; set; }
		public object Tag3 { get; set; }
		public object Tag4 { get; set; }
		public object Tag5 { get; set; }
		public object Tag6 { get; set; }

		public void Refresh()
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Line1"));
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Line2"));
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Line3"));
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Line4"));

			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Tag"));
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Tag2"));
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Tag3"));
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Tag4"));
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Tag5"));
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Tag6"));
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}
}
