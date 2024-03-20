using System;
using System.Reflection.Metadata.Ecma335;

namespace Tonk__Console_
{
	class Menu
	{
		//MENU

		string Qyn(string phrase)
		{
			Console.WriteLine(phrase);
			Console.WriteLine("1.) Yes");
			Console.WriteLine("2.) No");
			string answer = Console.ReadLine();

			if (answer == "1" || answer == "2")
			{
			
			}
			else if (answer != "1" || answer != "2")
			{
				Console.WriteLine("Invalid Entry. Please Try Again");
				answer = null;
				Qyn(phrase);
			}

			return answer;
		}


		static void Main(string[] args)
		{
			Menu MM = new Menu();
			Player P1 = new Player();
			Cpu P2 = new Cpu();
			Deck Deck = new Deck();
			Game Game = new Game();
			int uc = 0;

			Console.WriteLine("┌─────────────── •✧✧• ─────────────────┐");
			Console.WriteLine(" Welcome To The Tonk Table! (Alpha v0.1)");
			Console.WriteLine("└─────────────── •✧✧• ─────────────────┘");
			Console.WriteLine("Created by Camper \n");
			Console.WriteLine("\n");
			Console.WriteLine("Would you like to play a game?");
			Console.WriteLine("1.) Yes  2.) No ");
		
			ConsoleKeyInfo _uc = Console.ReadKey();
			if (char.IsDigit(_uc.KeyChar))
			{
				uc = int.Parse(_uc.KeyChar.ToString());
			}
			else
			{
				Console.WriteLine("*INVALID INPUT*");
				uc = 2;
			}

			if (uc == 1)
			{
				Game.Start(P1, P2, Deck);
			}
			else
			{
				Console.Clear();
				//make random gen here too for phrases
				Console.WriteLine("\n Alright no worries, whenever you wanna play I'll deal you a hand! \n");
			}



		}
	}
}
