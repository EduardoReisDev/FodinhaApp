using System;
using System.Collections.Generic;
using System.Linq;
using Fodinha.Helpers;
using Xamarin.Forms;

namespace Fodinha
{	
	public partial class WinnerPage : ContentPage
	{	
		public WinnerPage ()
		{
			InitializeComponent ();

			WinningPlayerLabel.Text = "Parabéns " + Game.Instance.Players.FirstOrDefault().Name + "!";
		}

        private void NewGame(object sender, EventArgs e)
        {
			Game.Instance.Rounds = 1;
			Game.Instance.Players = new List<Models.Player>();

            Application.Current.MainPage.Navigation.PushAsync(new MainPage());
		}
    }
}

