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
	public partial class TTTDatePicker : ContentView
	{
		public event EventHandler<DateChangedEventArgs> DateSelected;
		public static readonly BindableProperty DateProperty = BindableProperty.Create("Date", typeof(DateTime?), typeof(TTTDatePicker), null, BindingMode.TwoWay);

		private bool _lockChange;
		public DateTime? Date
		{
			get { return (DateTime?)GetValue(DateProperty); }
			set
			{
				SetValue(DateProperty, value);
				UpdateDate();
				if (_lockChange) return;
				OnPropertyChanged("DateValue");
			}
		}

		private DateTime _defaultDate;
		public DateTime DateValue
		{
			get { return this.Date.GetValueOrDefault(_defaultDate); }
			set
			{
				_lockChange = true;
				this.Date = value;
				_lockChange = false;
			}
		}

		public TTTDatePicker()
		{
			_defaultDate = new DateTime(DateTime.Now.Year, 1, 1);
			InitializeComponent();
			dateMain.BindingContext = this;
			dateMain.DateSelected += DateMain_DateSelected;
		}

		private void DateMain_DateSelected(object sender, DateChangedEventArgs e)
		{
			this.DateSelected?.Invoke(this, e);
		}

		private void UpdateDate()
		{
			if (Date.HasValue)
				dateMain.TextColor = Color.Black;
			else
				dateMain.TextColor = Color.Transparent;
		}
		protected override void OnBindingContextChanged()
		{
			base.OnBindingContextChanged();
			UpdateDate();
			OnPropertyChanged("DateValue");

		}
		private void btn_Click(object sender, EventArgs e)
		{
			var oldDt = this.Date;
			_lockChange = true;
			this.Date = null;
			_lockChange = false;
			UpdateDate();
			this.DateSelected?.Invoke(this, new DateChangedEventArgs(oldDt.GetValueOrDefault(), DateTime.MinValue));
		}
	}
}