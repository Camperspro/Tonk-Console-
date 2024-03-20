using System;
using System.Collections.Generic;
using System.Text;

namespace Tonk__Console_
{
	class Game
	{

		public void Update(Player P1, Cpu P2, Deck Deck)
		{
			P2.Counthand();
			Deck.Decktotal();
		}

		public void Start(Player P1, Cpu P2, Deck Deck)
		{
			Console.Clear();
			Console.WriteLine("Have you played Tonk Before?");
			Console.WriteLine("1.) Yes Lets Go!  2.) No.. ");
			int uc = 0;
			ConsoleKeyInfo _uc = Console.ReadKey();
			if (char.IsDigit(_uc.KeyChar))
			{
				uc = int.Parse(_uc.KeyChar.ToString());
			}
			//Set our friends name :)
			P2.Setname();
			//run tutorial
			switch (uc)
			{
				case 1:
					Console.Clear();
					Console.WriteLine("Hey! take a seat, I was just about to shuffle. Your friend ran to the bathroom \n im sure we can play a quick game. I didn't get your name, " + "i'm " + P2.Getname() + " by the way."); //Name Input
					P1.Setname();
					break;

				case 2:
					Console.Clear();
					Console.WriteLine("Take a seat, I'm about to shuffle and explain how to play. Trust me, eh- what was your name again? ");//Name Input
					P1.Setname();
					Console.WriteLine("And I'm " + P2.Getname() + " by the way." + "\nwell Tonk is real simple check this out.");
					Console.WriteLine("Deck: # " + " Cards | " + P2.Getname() + "'s Hand:" + P2.Gethandtotal() + " Cards | " + "Table Spreads: None");
					Console.WriteLine("\n");
					Console.WriteLine("So to win you want to either have the lowest or no cards at all, or the lowest card score total before the deck is empty or before other players. \n");
					Console.WriteLine("Spreads allow you to drop 3 cards that are the same number (ex: 3 clubs, 3 spades, 3 hearts), or same suit in # order by 3 or more (ex: ace spades, 2 spades, 3 spades +) \n");
					Console.WriteLine("This is the fastest way of losing cards, but beware! you can become a target and get hit on your spread. You can do the same to others as well");
					Console.WriteLine("\n");
					Console.WriteLine("Hits are follow ups on your spread(s), that anyone even you can apply to yourself and vice versa. (ex: someone adding the final 3 diamonds to the 3 spread");
					Console.WriteLine("Card values: Ace = 1, J(jack) = 11 , Queen = 12 , King = 13");
					Console.WriteLine("\n");
					Console.WriteLine("1.) Draw  2.) Pick Up  3.) Spread  4.) Hit  5.) Drop");
					Console.WriteLine("When its a players turn you can choose one option.");
					Console.WriteLine("When you draw you pick a card from the deck and decide what you'll do with it");
					Console.WriteLine("If you pick up then the 'Card On Deck' (previous card placed) then you'll decide what you can with that");
					Console.WriteLine("And lastly dropping is best when your card total is low or your hand count is low, as this will make you drop your current hand and total up your final score");
					Console.WriteLine("\n");
					Console.WriteLine("Got it all? Let's try a game");
					Console.ReadLine();
					break;
			}
					
			Deck.ShuffleDeck(Deck); //Card Setup

			Card[] p1hand = Deck.Dealhand(Deck);
			P1.Sethand(p1hand);

			Card[] p2hand = Deck.Dealhand(Deck);
			P2.Sethand(p2hand);

			Update(P1, P2, Deck);
			Table(P1, P2, Deck);

		}

		public void Table(Player P1, Cpu P2, Deck Deck)
		{
			//Update the game & clear console
			Update(P1, P2, Deck);
			Console.Clear();

			//Cpu's Hand & Name
			int p2ht = P2.Gethandtotal();
			string p2name = P2.Getname();
			Console.WriteLine("Deck:" + Deck.iDecktotal + " Cards | " + p2name + "'s Hand:" + P2.Gethandtotal() + " Cards | " + "Table Spreads: " + P1.ShowSpread() + P2.ShowSpread());

			//Check the gdeck for the one card
			for (int i = 0; i < Deck.gDeck.Length; i++)
			{
				if (Deck.gDeck[i] == null)
				{
					Console.WriteLine("Card On Deck: None");
				}
				else
				{
					Console.WriteLine("Card On Deck: " + Deck.gDeck[i].Getnumber() + " of " + Deck.gDeck[i].Getsuit());
				}
			}

			//Show players info
			P1.DisplayHand();

			int uc = 0;
			Console.WriteLine("*WHAT WILL YOU DO?*");
			Console.WriteLine("1.) Draw  2.) Pick Up  3.) Spread  4.) Hit  5.) Drop");
			ConsoleKeyInfo _uc = Console.ReadKey();
			if (char.IsDigit(_uc.KeyChar))
			{
				uc = int.Parse(_uc.KeyChar.ToString());
			}

			//Obviously the input menu 
			switch (uc)
			{
				case 1:
					P1.Draw(P1, P2, Deck, this);
					Update(P1, P2, Deck);
					break;

				case 2:
					//Check the gdeck for the one card
					for (int i = 0; i < Deck.gDeck.Length; i++)
					{
						if (Deck.gDeck[i] == null)
						{
							Console.WriteLine("There are no cards to swap..");
							Console.WriteLine("*PLEASE RESELECT (PRESS ENTER TO CONTINUE)*");
							Console.ReadLine();
							Table(P1, P2, Deck);
						}
						else
						{
							Console.Clear();
							P1.Swap(P1, P2, Deck, this);
							Update(P1, P2, Deck);
							break;
						}
					}

					break;


				case 3:
					P1.Spread(P1, P2, Deck, this);
					Update(P1, P2, Deck);
					break;

				case 4:
					P1.Hit(P1, P2, Deck, this);
					Update(P1, P2, Deck);
					break;

				case 5:
					P1.Drop(P2, Deck);
					End(P1, P2, Deck);
					break;
			}

			Console.WriteLine("*" + p2name + " BEGINS THEIR TURN*");
			Console.WriteLine("*PRESS ENTER TO CONTINUE*");
			Console.ReadLine();

			P2.Play(P1, P2, Deck, this);
			Table(P1, P2, Deck);

		}

		public void Restart(Player P1, Cpu P2, Deck Deck)
		{
			//reset everything
			Deck.gDeck[0] = null;
			P1.Setscore(0);
			P2.Setscore(0);
			Deck = null;
			//Card[] temp = Deck.GetDdeck();
			//temp = null;
			//Deck.SetDdeck(temp);

			Deck tmp = new Deck();
			tmp.ShuffleDeck(tmp);
			Deck = tmp;

			while(P1.GetHand().Count > 0)
			{
				for (int i = 0; i < P1.GetHand().Count; i++)
				{
					P1.GetHand().Remove(P1.GetHand()[i]);
				}
			}

			while (P2.GetHand().Count > 0)
			{
				for (int i = 0; i < P2.GetHand().Count; i++)
				{
					P2.GetHand().Remove(P2.GetHand()[i]);
				}
			}


			P1.Resetspreads();
			P2.cResetspreads();

			Card[] p1hand = Deck.Dealhand(Deck);
			P1.Sethand(p1hand);

			Card[] p2hand = Deck.Dealhand(Deck);
			P2.Sethand(p2hand);

			Update(P1, P2, Deck);
			Table(P1, P2, Deck);
		}

		public void End(Player P1, Cpu P2, Deck Deck)
		{
			Console.Clear();
			int uc = 0;
			Console.WriteLine("*WHAT WOULD YOU LIKE TO DO NOW?*");
			Console.WriteLine("1.) Play Again  2.) Exit ");
			ConsoleKeyInfo _uc = Console.ReadKey();
			if (char.IsDigit(_uc.KeyChar))
			{
				uc = int.Parse(_uc.KeyChar.ToString());
			}
			else
			{
				uc = 1;
			}

			switch (uc)
			{
				case 1:
					Console.WriteLine(P2.Getname() + ": " + "Alright sounds like fun! I'll reshuffle the deck.");
					Console.WriteLine("*PRESS ENTER TO CONTINUE*");
					Console.ReadLine();
					Restart(P1, P2, Deck);
					break;

						case 2:
					Console.WriteLine(P2.Getname() + ": " + "Thanks For Playing, I'll be here if you ever wanna play again.");
					break;
			}
		}

		
	}
}
