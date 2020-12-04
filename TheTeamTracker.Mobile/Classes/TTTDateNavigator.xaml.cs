using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TheTeamTracker.Mobile.Classes
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TTTDateNavigator : ContentView
	{
		public static readonly BindableProperty DateChangedCommandProperty = BindableProperty.Create(nameof(DateChangedCommand), typeof(ICommand), typeof(TTTDateNavigator));
		public ICommand DateChangedCommand
		{
			get { return (ICommand)GetValue(DateChangedCommandProperty); }
			set { SetValue(DateChangedCommandProperty, value); }
		}

		public static readonly BindableProperty DateProperty = BindableProperty.Create(nameof(Date), typeof(DateTime), typeof(TTTDateNavigator), DateTime.MinValue, BindingMode.TwoWay);
		public DateTime Date
		{
			get { return (DateTime)GetValue(DateProperty); }
			set
			{
				SetValue(DateProperty, value);
				OnPropertyChanged("DateFormatted");
			}
		}

		public Color TextColor
		{
			get { return lblFormatted.TextColor; }
			set { lblFormatted.TextColor = value; }
		}

		public int DayIncrement { get; set; }

		public string DateFormatted
		{
			get
			{
				if (this.DayIncrement > 1)
				{
					var end = this.Date.AddDays(this.DayIncrement - 1);
					return $"{this.Date.ToString("MM/dd")} - {end.ToString("MM/dd")}";
				}
				return CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedDayNames[(int)this.Date.DayOfWeek] + " - " + this.Date.ToString("MM/dd");
			}
		}

		public TTTDateNavigator()
		{
			InitializeComponent();
			lblFormatted.BindingContext = this;
			this.DayIncrement = 1;
		}

		protected override void OnBindingContextChanged()
		{
			base.OnBindingContextChanged();
			OnPropertyChanged("DateFormatted");
		}

		private void left_Clicked(object sender, EventArgs e)
		{
			this.Date = this.Date.AddDays(-1 * this.DayIncrement);
			DateChangedCommand?.Execute(this.Date);
		}

		private void right_Clicked(object sender, EventArgs e)
		{
			this.Date = this.Date.AddDays(this.DayIncrement);
			DateChangedCommand?.Execute(this.Date);
		}
	}
}