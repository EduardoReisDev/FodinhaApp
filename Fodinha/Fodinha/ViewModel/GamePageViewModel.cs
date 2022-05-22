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

		public GamePageViewModel()
		{
			PopulateList();
			Rounds = "Rodada de número " + Game.Instance.Rounds.ToString();

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
				Player player = Game.Instance.Players.FirstOrDefault(x => x.Id == id);

				if (player != null)
				{
					player.Guess++;

					Game.Instance.Players.Remove(player);
					Game.Instance.Players.Add(player);

					PopulateList();
				}
			}
		}

		public async void SubtractLifeById(int id)
		{
			if (id > 0)
			{
				Player player = Game.Instance.Players.FirstOrDefault(x => x.Id == id);

				if (player != null)
				{
					player.Lives--;

					if(player.Lives <= 0)
                    {
						bool playerWasEliminated = await Application.Current.MainPage.DisplayAlert(string.Empty, player.Name + " foi eliminado?", "Sim", "Não");

                        if (playerWasEliminated)
                        {
							Game.Instance.Players.Remove(player);

							if(Game.Instance.Players.Count() == 1)
                            {
								await Application.Current.MainPage.DisplayAlert(string.Empty, Game.Instance.Players.FirstOrDefault().Name + " ganhou!!", "Ok");

								await Application.Current.MainPage.Navigation.PushAsync(new WinnerPage());
							}
						}
					}
                    else
                    {
						Game.Instance.Players.Remove(player);
						Game.Instance.Players.Add(player);
					}

					PopulateList();
				}
			}
		}

		public void AddLifeById(int id)
		{
			if (id > 0)
			{
				Player player = Game.Instance.Players.FirstOrDefault(x => x.Id == id);

				if(player != null)
                {
					player.Lives++;

					Game.Instance.Players.Remove(player);
					Game.Instance.Players.Add(player);

					PopulateList();
				}
			}
		}

		public void SubtractGuess(int id)
        {
			if (id > 0)
			{
				Player player = Game.Instance.Players.FirstOrDefault(x => x.Id == id);

				if (player != null)
				{
					player.Guess--;

					Game.Instance.Players.Remove(player);
					Game.Instance.Players.Add(player);

					PopulateList();
				}
			}
		}

        private void PopulateList()
        {
			Players.Clear();

			foreach (Player player in Game.Instance.Players.OrderBy(x => x.Name))
            {
				Players.Add(player);
            }
		}

        private void SubtractRound()
        {
			Game.Instance.Rounds--;
			Rounds = "Rodada de número " + Game.Instance.Rounds.ToString();
		}

		private async void AddRound()
        {
			await Application.Current.MainPage.DisplayAlert("Fim da rodada " + Game.Instance.Rounds, "Não se esqueça de atualizar a vida e o palpite dos jogadores.", "Ok");

			Game.Instance.Rounds++;

			Rounds = "Rodada de número " + Game.Instance.Rounds.ToString();
		}
    }
}
