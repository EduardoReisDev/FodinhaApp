using System.Collections.Generic;
using Fodinha.Models;

namespace Fodinha.Helpers
{
	public sealed class Game
	{
		static Game instance;

		public static Game Instance
		{
			get
			{
				return instance ??= new Game();
			}
		}

		private Game()
		{

		}

		public int Rounds { get; set; } = 1;
		public List<Player> Players { get; set; } = new List<Player>();
	}
}
