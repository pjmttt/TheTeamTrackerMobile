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
	public partial class TextArea : ContentView
	{
		public static readonly BindableProperty TextProperty =
			BindableProperty.Create("Text", typeof(string), typeof(TextArea), null, BindingMode.TwoWay);

		public string Text
		{
			get { return (string)GetValue(TextProperty); }
			set { SetValue(TextProperty, value); }
		}

		public double EditorHeight
		{
			get { return editor.HeightRequest; }
			set { editor.HeightRequest = value; }
		}

		public TextArea()
		{
			InitializeComponent();
			editor.BindingContext = this;
			EditorHeight = 150;
		}
	}
}