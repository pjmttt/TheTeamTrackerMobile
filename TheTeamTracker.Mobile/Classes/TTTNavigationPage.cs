using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace TheTeamTracker.Mobile.Classes
{
	public class TTTNavigationPage : NavigationPage
	{
		const string BACKGROUND_COLOR = "#3548a3";

		public TTTNavigationPage() : base()
		{
			setColor();
		}

		public TTTNavigationPage(Page root) : this(root, false)
		{
		}

		public TTTNavigationPage(Page root, bool hasNavigationBar) : base(root)
		{
			setColor();
			NavigationPage.SetHasNavigationBar(root, hasNavigationBar);
		}

		private void setColor()
		{
			if (Config.Instance == null || Config.Instance.Development)
				this.BarBackgroundColor = Color.FromHex("#800020");
			else
				this.BarBackgroundColor = Color.FromHex(BACKGROUND_COLOR);
			this.BarTextColor = Color.White;
		}
	}
}
