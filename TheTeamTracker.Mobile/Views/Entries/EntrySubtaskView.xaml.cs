using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheTeamTracker.Mobile.ViewModels.Entries;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
namespace TheTeamTracker.Mobile.Views.Entries
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EntrySubtaskView : ContentView
	{
		public static readonly BindableProperty ForUserProperty =
			BindableProperty.Create("ForUser", typeof(bool), typeof(EntrySubtaskView), true, BindingMode.OneWay);
		public bool ForUser
		{
			get { return (bool)GetValue(ForUserProperty); }
			set { SetValue(ForUserProperty, value); }
		}
		public static readonly BindableProperty ForSearchProperty =
			BindableProperty.Create("ForSearch", typeof(bool), typeof(EntrySubtaskView), true, BindingMode.OneWay);
		public bool ForSearch
		{
			get { return (bool)GetValue(ForSearchProperty); }
			set { SetValue(ForSearchProperty, value); }
		}
		public static readonly BindableProperty IsNotGeneralProperty =
			BindableProperty.Create("IsNotGeneral", typeof(bool), typeof(EntrySubtaskView), true, BindingMode.OneWay);
		public bool IsNotGeneral
		{
			get { return (bool)GetValue(IsNotGeneralProperty); }
			set { SetValue(IsNotGeneralProperty, value); }
		}
		private EntryItem entryItem
		{
			get { return this.BindingContext as EntryItem; }
		}
		private void populateControls()
		{
			if (entryItem == null) return;
			lblHeaderText.Text = entryItem.Entry.HeaderText;
			if (ForSearch)
				lblHeaderText.Text += " - " + entryItem.Entry.EntryDateValue.ToShortDateString();
			if (IsNotGeneral)
			{
				stackStatuses.Children.Clear();
				foreach (var s in entryItem.Statuses)
				{
					stackStatuses.Children.Add(new Label()
					{
						Text = s.StatusName,
						BackgroundColor = s.BackgroundColor,
						TextColor = s.TextColor,
						HorizontalTextAlignment = TextAlignment.Center,
						HorizontalOptions = LayoutOptions.FillAndExpand
					});
				}
			}
		}
		public EntrySubtaskView()
		{
			InitializeComponent();
		}
		protected override void OnBindingContextChanged()
		{
			base.OnBindingContextChanged();
			populateControls();
		}
		protected async void Handle_ItemTapped(object sender, EventArgs e)
		{
			if (entryItem == null) return;
			var editViewModel = new EntryEditViewModel(entryItem.Entry, entryItem.Entry.EntryTypeValue, ForUser, entryItem.Entry.EntryDateValue);
			editViewModel.EntrySaved += (object sender2, EventArgs e2) =>
			{
				entryItem.ResetStatuses();
				populateControls();
			};
			var page = new EntryEditPage();
			page.BindingContext = editViewModel;
			await ((MainPage)App.Current.MainPage).NavigateTo(page, true);
			//Deselect Item
			// ((ListView)sender).SelectedItem = null;
		}
	}
}
