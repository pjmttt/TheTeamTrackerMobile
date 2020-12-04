using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheTeamTracker.Mobile.Classes;
using TheTeamTracker.Mobile.ViewModels.Inventory;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TheTeamTracker.Mobile.Views.Inventory
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class InventoryEditPage : ContentPage
	{
		public InventoryEditPage()
		{
			InitializeComponent();
			MessagingCenter.Subscribe<InventoryEditViewModel>(this, InventoryEditViewModel.SUCCESS, async (sender) =>
			{
				await ((MainPage)App.Current.MainPage).GoBack();
			});
			quantityOnHand.SetBinding(Entry.TextProperty, new Binding("InventoryItem.QuantityOnHand", converter: new IntegerConverter()));
			minimumQuantity.SetBinding(Entry.TextProperty, new Binding("InventoryItem.MinimumQuantity", converter: new IntegerConverter()));
		}
	}
}