using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TheTeamTracker.Mobile.Classes
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TTTTimePicker : ContentView
	{
		public static readonly BindableProperty TimeProperty = BindableProperty.Create("Time", typeof(TimeSpan?), typeof(TTTTimePicker), null, BindingMode.TwoWay);

		private bool _lockChange;
		public TimeSpan? Time
		{
			get { return (TimeSpan?)GetValue(TimeProperty); }
			set
			{
				SetValue(TimeProperty, value);
				UpdateTime();
				if (_lockChange) return;
				OnPropertyChanged("TimeValue");
			}
		}

		public TimeSpan TimeValue
		{
			get { return this.Time.GetValueOrDefault(); }
			set
			{
				_lockChange = true;
				this.Time = value;
				_lockChange = false;
			}
		}

		public TTTTimePicker()
		{
			InitializeComponent();
			timeMain.BindingContext = this;
		}

		private void UpdateTime()
		{
			if (Time.HasValue)
				timeMain.TextColor = Color.Black;
			else
				timeMain.TextColor = Color.Transparent;
		}
		protected override void OnBindingContextChanged()
		{
			base.OnBindingContextChanged();
			UpdateTime();
			OnPropertyChanged("TimeValue");

		}
		private void btn_Click(object sender, EventArgs e)
		{
			_lockChange = true;
			this.Time = null;
			_lockChange = false;
			UpdateTime();
		}
	}
}