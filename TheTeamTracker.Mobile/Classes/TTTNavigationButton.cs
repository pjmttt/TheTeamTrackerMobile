using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace TheTeamTracker.Mobile.Classes
{
	public class TTTNavigationButton : Button
	{
		public TTTNavigationButton()
		{
			this.WidthRequest = 44;
			this.HeightRequest = 44;
			this.CornerRadius = 22;
			this.BackgroundColor = Color.Navy;
		}
	}

	public class TTTNavigateLeft : TTTNavigationButton
	{
		public TTTNavigateLeft() : base()
		{
			this.Image = "ic_keyboard_arrow_left_white";
		}
	}

	public class TTTNavigateRight : TTTNavigationButton
	{
		public TTTNavigateRight() : base()
		{
			this.Image = "ic_keyboard_arrow_right_white";
		}
	}
}
