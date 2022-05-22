using System;
using Fodinha.Models;
using Fodinha.ViewModel;
using Xamarin.Forms;

namespace Fodinha
{	
	public partial class GamePage : ContentPage
	{
		private readonly GamePageViewModel viewModel;

		public GamePage ()
		{
			InitializeComponent ();

			viewModel = new GamePageViewModel();
			BindingContext = viewModel;
		}

		private void AddLife(object sender, EventArgs e)
		{
			if (((TappedEventArgs)e).Parameter is Player player)
            {
				viewModel.AddLifeById(player.Id);
            }
		}

		private void SubtractLife(object sender, EventArgs e)
		{
			if (((TappedEventArgs)e).Parameter is Player player)
			{
				viewModel.SubtractLifeById(player.Id);
			}
		}

		private void AddGuess(object sender, EventArgs e)
		{
			if (((TappedEventArgs)e).Parameter is Player player)
			{
				viewModel.AddGuessById(player.Id);
			}
		}

		private void SubtractGuess(object sender, EventArgs e)
		{
			if (((TappedEventArgs)e).Parameter is Player player)
			{
				viewModel.SubtractGuess(player.Id);
			}
		}
    }
}
