using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Xam.Plugin;
using Xamarin.Forms;

namespace TheTeamTracker.Mobile.Classes
{
	public class MenuButton : Button
	{
		private PopupMenu _popupMenu;

		ObservableCollection<MenuButtonItem> _menuItems;
		public IList<MenuButtonItem> MenuItems
		{
			get
			{
				if (_menuItems == null)
				{
					_menuItems = new ObservableCollection<MenuButtonItem>();
					_menuItems.CollectionChanged += MenuItems_CollectionChanged;
				}

				return _menuItems;
			}
		}

		public MenuButton()
		{
			// TODO: find cleaner way
			if (Device.Idiom == TargetIdiom.Tablet)
				Scale = 1.5;

			Image = "ic_more_vert_black";
			WidthRequest = 35;
			if (Device.Idiom == TargetIdiom.Tablet)
				this.WidthRequest += 15;

			BackgroundColor = Color.Transparent;
			BorderColor = Color.Transparent;
			HorizontalOptions = LayoutOptions.End;
			// Margin = new Thickness(0, 0, Device.Idiom == TargetIdiom.Tablet ? 18 : 10, 0);

			_popupMenu = new PopupMenu();
			_popupMenu.OnItemSelected += _popupMenu_OnItemSelected;

			this.Clicked += MenuButton_Clicked;
		}

		private void MenuItems_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			foreach (var mi in MenuItems)
			{
				mi.PropertyChanged -= MenuItem_PropertyChanged;
				mi.PropertyChanged += MenuItem_PropertyChanged;
			}
			updateMenuItems();
		}

		private void MenuItem_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			updateMenuItems();
		}

		private void updateMenuItems()
		{
			_popupMenu.ItemsSource = this.MenuItems.Where(mi => mi.IsVisible).Select(mi => mi.Text).ToList();
		}

		private void _popupMenu_OnItemSelected(string item)
		{
			var mnuItem = this.MenuItems.First(mi => mi.Text == item);
			mnuItem.Activate();
		}

		protected override void OnBindingContextChanged()
		{
			base.OnBindingContextChanged();
			if (_menuItems.Any())
			{
				for (var i = 0; i < _menuItems.Count; i++)
					SetInheritedBindingContext(_menuItems[i], BindingContext);
			}
			updateMenuItems();
		}

		private void MenuButton_Clicked(object sender, EventArgs e)
		{
			_popupMenu.ShowPopup(this);
		}
	}

	public class MenuButtonItem : MenuItem
	{
		public static readonly BindableProperty IsVisibleProperty =
			BindableProperty.Create("IsVisible", typeof(bool), typeof(MenuButtonItem), true);

		public bool IsVisible
		{
			get { return (bool)GetValue(IsVisibleProperty); }
			set { SetValue(IsVisibleProperty, value); }
		}
	}
}
