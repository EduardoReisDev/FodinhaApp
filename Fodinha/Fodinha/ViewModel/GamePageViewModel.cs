using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Fodinha.Helpers;
using Fodinha.Models;
using Xamarin.Forms;

namespace Fodinha.ViewModel
{
	public class GamePageViewModel : BaseViewModel
	{
		public string Rounds
        {
			get => rounds;
			set => SetProperty(ref rounds, value);
        }

		public ObservableCollection<Player> Players { get; set; } = new ObservableCollection<Player>();

		public ICommand AddRoundCommand { get; private set; }
        public ICommand SubtractRoundCommand { get; private set; }

        private string rounds;
		private readonly Game game;

		public GamePageViewModel()
		{
			game = Game.Instance;

			PopulateList();

			Rounds = "Rodada de número " + game.Rounds.ToString();

			AddRoundCommand = new Command(
				execute: () =>
				{
					AddRound();
				});

			SubtractRoundCommand = new Command(
				execute: () =>
				{
					SubtractRound();
				});
		}

		public void AddGuessById(int id)
		{
			if (id > 0)
            {
				Player player = game.Players.FirstOrDefault(x => x.Id == id);

				if (player != null)
				{
					player.Guess++;

					game.Players.Remove(player);
					game.Players.Add(player);

					PopulateList();
				}
			}
		}

		public async void SubtractLifeById(int id)
		{
			if (id > 0)
			{
				Player player = game.Players.FirstOrDefault(x => x.Id == id);

				if (player != null)
				{
					player.Lives--;

					if(player.Lives <= 0)
                    {
						bool playerWasEliminated = await Application.Current.MainPage.DisplayAlert(string.Empty, player.Name + " foi eliminado?", "Sim", "Não");

                        if (playerWasEliminated)
                        {
							game.Players.Remove(player);

							if(game.Players.Count() == 1)
                            {
								await Application.Current.MainPage.DisplayAlert(string.Empty, game.Players.FirstOrDefault().Name + " ganhou!!", "Ok");
								await Application.Current.MainPage.Navigation.PushAsync(new WinnerPage());
							}
						}
					}
                    else
                    {
						game.Players.Remove(player);
						game.Players.Add(player);
					}

					PopulateList();
				}
			}
		}

		public void AddLifeById(int id)
		{
			if (id > 0)
			{
				Player player = game.Players.FirstOrDefault(x => x.Id == id);

				if(player != null)
                {
					player.Lives++;

					game.Players.Remove(player);
					game.Players.Add(player);

					PopulateList();
				}
			}
		}

		public void SubtractGuess(int id)
        {
			if (id > 0)
			{
				Player player = game.Players.FirstOrDefault(x => x.Id == id);

				if (player != null)
				{
					player.Guess--;

					game.Players.Remove(player);
					game.Players.Add(player);

					PopulateList();
				}
			}
		}

        private void PopulateList()
        {
			Players.Clear();

			foreach (Player player in game.Players.OrderBy(x => x.Name))
            {
				Players.Add(player);
            }
		}

        private void SubtractRound()
        {
			game.Rounds--;
			Rounds = "Rodada de número " + game.Rounds.ToString();
		}

		private async void AddRound()
        {
			await Application.Current.MainPage.DisplayAlert("Fim da rodada " + game.Rounds, "Não se esqueça de atualizar a vida e o palpite dos jogadores.", "Ok");

			game.Rounds++;

			Rounds = "Rodada de número " + game.Rounds.ToString();
		}
    }
}
