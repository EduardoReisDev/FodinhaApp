using System;
using Fodinha.Helpers;
using Fodinha.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Fodinha
{
    public partial class MainPage : ContentPage
    {
		private readonly Game game;

		public MainPage()
		{
			InitializeComponent();

			game = Game.Instance;
		}

		private async void AddNewPlayer(object sender, EventArgs args)
        {
            if (!string.IsNullOrEmpty(NameOfPlayerEntry.Text))
            {
                Random randomNumber = new();

                Player player = new()
                {
                    Id = randomNumber.Next(1, 99),
                    Name = NameOfPlayerEntry.Text
                };

                game.Players.Add(player);

                PlayersLabel.IsVisible = game.Players.Count != 0;

                if (game.Players.Count != 0)
                {
                    PlayersLabel.Text = $"{game.Players.Count} jogadores";
                    PlayersLabel.IsVisible = true;
                }

                NameOfPlayerEntry.Text = string.Empty;

                ListOfPlayers.ItemsSource = null;
                ListOfPlayers.ItemsSource = game.Players;
            }
            else
            {
                await DisplayAlert(string.Empty, "Digite o nome do jogador para adiciona-lo.", "Ok");
            }
        }

		private async void FinishAddPlayers(object sender, EventArgs args)
        {
            if (game.Players.Count > 1)
            {
                AddPlayersStack.IsVisible = false;
                AddLifePlayersStack.IsVisible = true;
            }
            else
            {
                await DisplayAlert(string.Empty, "Adicione 2 jogadores antes de continuar.", "Ok");
            }
        }

        private async void AddLifePlayers(object sender, EventArgs args)
        {
            if (!string.IsNullOrEmpty(LifeOfPlayerEntry.Text))
            {
                if (game.Players.Count != 0)
                {
                    foreach(Player player in game.Players)
                    {
                        player.Lives = int.Parse(LifeOfPlayerEntry.Text);
                    }
                }

                if (game.Players.TrueForAll(x => x.Lives == int.Parse(LifeOfPlayerEntry.Text)))
                {
                    AddLifePlayersStack.IsVisible = false;

                    await Navigation.PushAsync(new GamePage());
                }
                else
                {
                    await DisplayAlert("Erro!", "Tente novamente.", "Ok");
                }
            }
            else
            {
                await DisplayAlert(string.Empty, "Digite a quantidade de vidas para continuar.", "Ok");
            }
        }

        async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
            {
                Uri uri = new("https://copag.com.br/blog/detalhes/fodinha");
                await Browser.OpenAsync(uri);
            }
            catch(Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "Ok");
            }
        }
    }
}
