using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

namespace Tonk__Console_
{
	class Player
	{
		private int score = 0;
		private string pname = null;
		public int hits = 0;
		private List<Card> hand = new List<Card>();
		public List<Card[]> spread = new List<Card[]>();

		public void CallTable(Game Game, Player P1, Cpu P2, Deck Deck)
		{
			Game.Table(P1, P2, Deck);
		}

		public void Resetspreads()
		{
			while (spread.Count > 0)
			{
				for (int i = 0; i < spread.Count; i++)
				{
					spread.Remove(spread[i]);
				}
			}
			
		}

		public List<Card[]> Getspreads()
		{
			return spread;
		}

		public List<Card> GetHand()
		{
			return hand;
		}

		public string Getname()
		{
			return pname;
		}

		public void Setname()
		{
			string phrase = "";

			Console.WriteLine("*TYPE YOUR NAME AND PRESS ENTER TO CONTINUE* : ");
			pname = Console.ReadLine();
			Console.Clear();
			Console.WriteLine("Oh, " + pname + " is it?" + " Thats a nice name");
			Console.WriteLine("Here's your hand mate,");
			Console.WriteLine("*PRESS ENTER TO CONTINUE*");
			Console.ReadLine();
			Console.Clear();

		}

		public void Sethand(Card[] _hand)
		{
			for (int i = 0; i < 5; i++)
			{
				hand.Add(_hand[i]);
				//hand[i] = _hand[i];
				_hand[i] = null;
			}
		}

		public void Setscore(int _score)
		{
			score = _score;
		}

		public void DisplayHand()
		{
			bool a = false;
			int cardn = 0;

			Console.WriteLine(" ");
			Console.WriteLine("*YOUR HAND*");
			Console.WriteLine("------------------------");

			//hand = hand.OrderBy(x => x.GetVal()).ThenBy(x => x.Getsuit()).ToList();
			hand = hand.OrderBy(x => x.Getsuit() ?? "").ThenBy(x => x.GetVal()).ToList();

			for (int i = 0; i < hand.Count; i++)
			{
				if (hand[i] == null)
				{
					break;
				}

				string cnum = hand[i].Getnumber();
				string cs = hand[i].Getsuit();

				Console.WriteLine("["+cardn+".] " + cnum + " of " + cs + "\n");
				Console.WriteLine("------------------------");
				cardn++;

				
			}

			Console.WriteLine(" ");
		}

		public Deck Draw(Player P1, Cpu P2, Deck Deck, Game Game)
		{
			Console.Clear();
			int uc = 0;

			//Check if deck is empty
			if (Deck.Decktotal() == 0)
			{
				Console.WriteLine("Theres nothing left to draw...");
				Console.WriteLine("*PLEASE SELECT AGAIN*");
				Console.ReadLine();
				Console.Clear();
				return Deck;
			}

			Card[] tmpd = Deck.GetDdeck();

			for (int i = 0; i < 52; i++)
			{
				//Check if its null or card
				if(tmpd[i] != null)
				{
					Console.WriteLine("Spreads: " + P1.ShowSpread() + P2.ShowSpread());
					DisplayHand();
					Console.WriteLine("You drawed : " + tmpd[i].Getnumber() + " of " + tmpd[i].Getsuit() + "\n");
					Console.WriteLine("1.) Keep   2.) Drop This Card   3.) Hit    4.) Spread");
					ConsoleKeyInfo _uc = Console.ReadKey();
					if (char.IsDigit(_uc.KeyChar))
					{
						uc = int.Parse(_uc.KeyChar.ToString());
					}
					else
					{
						uc = 2;
						Console.WriteLine("*INVALID INPUT*");
					}

					Card temp;

					switch (uc)
					{
						case 1: //KEEP
							Console.WriteLine("Please select which card you wish to replace from your hand");
							Console.WriteLine("*[ENTER INDEX IN BRACKET]*");
							//temp card now has card info and is ready to be dropped
							int replace = 0;
							ConsoleKeyInfo _replace = Console.ReadKey();
							if (char.IsDigit(_replace.KeyChar))
							{
								replace = int.Parse(_replace.KeyChar.ToString());
							}
							else
							{
								replace = 2;
								Console.WriteLine("*INVALID INPUT*");
								Console.ReadLine();
								Draw(P1, P2, Deck, Game);
							}
							temp = hand[replace];

							if (hand[replace] == null)
							{
								Console.WriteLine("*INVALID INPUT*");
								Console.ReadLine();
								Draw(P1, P2, Deck, Game);
							}

							//Now we replace
							hand[replace] = null;
							hand[replace] = tmpd[i];
							Deck.gDeck[0] = null;
							Deck.gDeck[0] = temp;
							tmpd[i] = null;

							Console.Clear();
							DisplayHand();
							Console.WriteLine("*YOU END YOUR TURN*");
							return Deck;

						case 2: //DROP
							Console.WriteLine("You dropped: " + tmpd[i].Getnumber() + " of " + tmpd[i].Getsuit() + "\n");

							Deck.gDeck[0] = tmpd[i];
							tmpd[i] = null;
						
							Console.WriteLine("*YOU END YOUR TURN*");
							return Deck;

						case 3: //HIT
							List<Card> hittemp = hand;
							hand.Add(tmpd[i]);
							Hit(P1, P2, Deck, Game);
							if (hand == hittemp)
							{
								hand.Remove(tmpd[i]);
								Console.WriteLine("Invalid Spread");
								Console.ReadLine();
								Draw(P1, P2, Deck, Game);
							}
							else
							{
								Console.Clear();
								DisplayHand();
								Console.WriteLine("Please select which card you wish to drop from your hand");
								Console.WriteLine("*[ENTER INDEX IN BRACKET]*");
								int drop = 0;
								ConsoleKeyInfo _drop = Console.ReadKey();
								if (char.IsDigit(_drop.KeyChar))
								{
									replace = int.Parse(_drop.KeyChar.ToString());
								}
								else
								{
									replace = 2;
									Console.WriteLine("*INVALID INPUT*");
									Console.ReadLine();
								}
								temp = hand[drop];

								//Now we replace
								hand.Remove(hand[drop]);
								Deck.gDeck[0] = null;
								Deck.gDeck[0] = temp;

								Console.Clear();
								DisplayHand();
								Console.WriteLine("*YOU END YOUR TURN*");
								return Deck;
							}

							return Deck;
							

						case 4: //SPREAD
							List<Card> htemp = hand;
							hand.Add(tmpd[i]);
							Console.Clear();
							DisplayHand();

							Spread(P1, P2, Deck, Game);

							if (hand == htemp)
							{
								hand.Remove(tmpd[i]);
								Console.WriteLine("Invalid Spread");
								Console.ReadLine();
								Draw(P1, P2, Deck, Game);
							}
							else
							{
								Console.Clear();
								if(hand.Count == 0)
                                {
									Console.WriteLine("*YOU END YOUR TURN*");
									return Deck;
								}
								DisplayHand();
								Console.WriteLine("Please select which card you wish to drop from your hand");
								Console.WriteLine("*[ENTER INDEX IN BRACKET]*");
								int drop = 0;
								ConsoleKeyInfo _drop = Console.ReadKey();
								if (char.IsDigit(_drop.KeyChar))
								{
									replace = int.Parse(_drop.KeyChar.ToString());
								}
								else
								{
									replace = 2;
									Console.WriteLine("*INVALID INPUT*");
									Console.ReadLine();
								}
								temp = hand[drop];

								//Now we replace
								hand.Remove(hand[drop]);
								Deck.gDeck[0] = null;
								Deck.gDeck[0] = temp;

								Console.Clear();
								DisplayHand();
								Console.WriteLine("*YOU END YOUR TURN*");
								return Deck;
							}
							//Console.Clear();
							break;
					}
					
				}
			}

			return Deck;
		}

		public void Hit(Player P1, Cpu P2, Deck Deck, Game Game)
		{

			if (spread.Count == 0 && P2.cspread.Count == 0) // No Spreads
			{
				Console.WriteLine("Theres no spreads on the table...");
				Console.WriteLine("*PLEASE SELECT AGAIN*");
				Console.WriteLine("*PRESS ENTER TO CONTINUE*");
				Console.ReadLine();
				Console.Clear();
				Game.Table(P1, P2, Deck);
			}
			else if (spread.Count > 0 && P2.cspread.Count == 0) //P1 Spreads only
			{
				Card temp;
				DisplayHand();
				Console.WriteLine("Select which card you wish to hit with from your hand");
				Console.WriteLine("*[ENTER INDEX IN BRACKET]*");
				//temp card now has card info and is ready to be dropped
				int replace = 0;
				ConsoleKeyInfo _drop = Console.ReadKey();
				if (char.IsDigit(_drop.KeyChar))
				{
					replace = int.Parse(_drop.KeyChar.ToString());
				}
				else
				{
					Console.WriteLine("*INVALID INPUT*");
					Console.ReadLine();
					Hit(P1, P2, Deck, Game);
				}
				temp = hand[replace];


				//loop into list
				for (int j = 0; j < spread.Count; j++)
				{
					//then the arry from list index
					for (int i = 0; i < spread[j].Length; i++)
					{
					
					  //check for same # spread first
					  if (hand[replace].Getnumber() == spread[j][i].Getnumber())
					  {
							for (int x = 0; x < spread[j].Length; x++)
							{
								//Find next free spot in spread and add + remove
								if (spread[j][x] == null)
								{
									spread[j][x] = temp;
									hand.Remove(temp);

									Console.WriteLine("You Hit Your Spread Of " + temp.Getnumber() + "'s");
								}
							}
							
					  }
					  //check for chronological
					  else if (hand[replace].Getsuit() == spread[j][i].Getsuit())
					  {
							int c1 = 0;
							switch (hand[replace].Getnumber())
							{
								case "Ace":
									c1 = 1;
									break;

								case "2":
									c1 = 2;
									break;

								case "3":
									c1 = 3;
									break;

								case "4":
									c1 = 4;
									break;

								case "5":
									c1 = 5;
									break;

								case "6":
									c1 = 6;
									break;

								case "7":
									c1 = 7;
									break;

								case "8":
									c1 = 8;
									break;

								case "9":
									c1 = 9;
									break;

								case "10":
									c1 = 10;
									break;

								case "Jack":
									c1 = 11;
									break;

								case "Queen":
									c1 = 12;
									break;

								case "King":
									c1 = 13;
									break;
							}

							for (int x = 0; x < spread[j].Length; x++)
							{
								//ONLY HERE FOR NOW
								if (spread[j][x] == null)
								{
									spread[j][x] = temp;
									hand.Remove(hand[replace]);

									Console.WriteLine("You Hit Your Spread Of " + temp.Getnumber() + "'s");
									
								}

								//if selected card val is less than first card val
								//if (c1 < spread[j][0].GetVal())
								//{
									
								//}

							}
					  }

					}
				}

			}
			else if (spread.Count == 0 && P2.cspread.Count < 0) //P2 Spreads Only
			{
				Card temp;
				DisplayHand();
				Console.WriteLine("Select which card you wish to hit with from your hand");
				Console.WriteLine("*[ENTER INDEX IN BRACKET]*");
				//temp card now has card info and is ready to be dropped
				int replace = Convert.ToInt32(Console.ReadKey());
				temp = hand[replace];
			}
			else if (spread.Count > 0 && P2.cspread.Count < 0) //Both Spreads
			{
				Card temp;
				DisplayHand();
				Console.WriteLine("Select which card you wish to hit with from your hand");
				Console.WriteLine("*[ENTER INDEX IN BRACKET]*");
				//temp card now has card info and is ready to be dropped
				int replace = Convert.ToInt32(Console.ReadKey());
				temp = hand[replace];
			}

		}

		public void Spread(Player P1, Cpu P2, Deck Deck, Game Game)
		{
			int u1 = 0;
			int u2 = 0;
			int u3 = 0;
			Card c1 = null;
			Card c2 = null;
			Card c3 = null;

			Console.WriteLine("* !IN ORDER! PLEASE SELECT THE FIRST CARD [ENTER INDEX IN BRACKET]*");
			ConsoleKeyInfo _uc = Console.ReadKey();
			if (char.IsDigit(_uc.KeyChar))
			{
				u1 = int.Parse(_uc.KeyChar.ToString());
			}
			else
			{
				u1 = 0;
			}
			c1 = hand[u1];
			Console.Clear();
			DisplayHand();
			Console.WriteLine("Selected: " + c1.Getnumber() + " of " + c1.Getsuit());


			Console.WriteLine("*PLEASE SELECT THE SECOND CARD [ENTER INDEX IN BRACKET]*");
			_uc = Console.ReadKey();
			if (char.IsDigit(_uc.KeyChar))
			{
				u2 = int.Parse(_uc.KeyChar.ToString());
			}
			else
			{
				u2 = 1;
			}
			c2 = hand[u2];
			Console.Clear();
			DisplayHand();
			Console.WriteLine("Selected: " + c1.Getnumber() + " of " + c1.Getsuit() + " + " + c2.Getnumber() + " of " + c2.Getsuit());


			Console.WriteLine("*PLEASE SELECT THE LAST CARD [ENTER INDEX IN BRACKET]*");
			_uc = Console.ReadKey();
			if (char.IsDigit(_uc.KeyChar))
			{
				u3 = int.Parse(_uc.KeyChar.ToString());
			}
			else
			{
				u3 = 1;
			}
			c3 = hand[u3];
			Console.Clear();
			DisplayHand();
			Console.WriteLine("Selected: " + c1.Getnumber() + " of " + c1.Getsuit() + " + " + c2.Getnumber() + " of " + c2.Getsuit() + " + " + c3.Getnumber() + " of " + c3.Getsuit());
			Console.WriteLine("*PRESS ENTER TO CONTINUE*");
			Console.ReadLine();

			//Now we check their selections smh
			//Same Number Spread
			if (c1.Getnumber() == c2.Getnumber() && c2.Getnumber() == c3.Getnumber())
			{
				if (c1 == c2)
				{
					Console.WriteLine("*INVALID SPREAD*");
					Console.WriteLine("*PLEASE SELECT AGAIN*");
					Console.ReadLine();
					Console.Clear();
					CallTable(Game, P1, P2, Deck);
				}

				if (c1 == c3)
				{
					Console.WriteLine("*INVALID SPREAD*");
					Console.WriteLine("*PLEASE SELECT AGAIN*");
					Console.ReadLine();
					Console.Clear();
					CallTable(Game, P1, P2, Deck);
				}

				if (c2 == c3)
				{
					Console.WriteLine("*INVALID SPREAD*");
					Console.WriteLine("*PLEASE SELECT AGAIN*");
					Console.ReadLine();
					Console.Clear();
					CallTable(Game, P1, P2, Deck);
				}


				Card[] Nspread = new Card[5];
				Nspread[0] = c1;
				Nspread[1] = c2;
				Nspread[2] = c3;

				spread.Add(Nspread);

				hand.Remove(c1);
				hand.Remove(c2);
				hand.Remove(c3);

				Console.Clear();

				string finished = ShowSpread();

				Console.WriteLine(finished);
				DisplayHand();
				End(Deck);


			}
			else if (c1.Getsuit() == c2.Getsuit() && c2.Getsuit() == c3.Getsuit())
			{
				bool check = Deck.ChronoCheck(c1, c2, c3, P1);

				if (check == true)
				{
					Card[] Cspread = new Card[13];
					Cspread[0] = c1;
					Cspread[1] = c2;
					Cspread[2] = c3;

					spread.Add(Cspread);

					hand.Remove(c1);
					hand.Remove(c2);
					hand.Remove(c3);

					Console.Clear();

					string finished = ShowSpread();

					Console.WriteLine(finished);
					DisplayHand();
				}
				else
				{
					Console.WriteLine("*INVALID SPREAD*");
					Console.WriteLine("*PLEASE SELECT AGAIN*");
					Console.ReadLine();
					Console.Clear();
					CallTable(Game, P1, P2, Deck);
				}

			}
			else
			{
				Console.WriteLine("*INVALID SPREAD*");
				Console.WriteLine("*PLEASE SELECT AGAIN*");
				Console.ReadLine();
				Console.Clear();
				CallTable(Game, P1, P2, Deck);
			}

		}

		public void Swap(Player P1, Cpu P2, Deck Deck, Game Game)
		{
			int u1 = 0;
			Console.WriteLine("You Pick Up The " + Deck.gDeck[0].Getnumber() + " of " + Deck.gDeck[0].Getsuit() + "\n");

			DisplayHand();
			Console.WriteLine("What Shall You Do With It?");
			Console.WriteLine("1.) Keep  2.) Spread  3.) Hit ");
			ConsoleKeyInfo _uc = Console.ReadKey();
			if (char.IsDigit(_uc.KeyChar))
			{
				u1 = int.Parse(_uc.KeyChar.ToString());
			}
			else
			{
				u1 = 4;
			}

			switch (u1)
			{
				//Keep Picked Up Card
				case 1:
					{
						Console.WriteLine("Please select which card you wish to replace from your hand");
						Console.WriteLine("*TYPE NUMBER IN BRACKET*");
						//temp card now has card info and is ready to be dropped
						int replace = 0;
						while (!Int32.TryParse(Console.ReadLine(), out replace))
						{
							Swap(P1, P2, Deck, Game);
						}

						Card temp = hand[replace];

						//Now we replace
						hand[replace] = null;
						hand[replace] = Deck.gDeck[0];
						Deck.gDeck[0] = null;
						Deck.gDeck[0] = temp;

						Console.Clear();
						DisplayHand();
						Console.WriteLine("*YOU END YOUR TURN*");
						break;
					}

				//Spread
				case 2:
					{
						List<Card> temp = hand;

						hand.Add(Deck.gDeck[0]);
						Console.Clear();
						DisplayHand();

						Spread(P1, P2, Deck, Game);
						if (hand == temp)
						{
							hand.Remove(Deck.gDeck[0]);
							Console.WriteLine("*INVALID INPUT TRY AGAIN*");
							Console.ReadLine();
							Swap(P1, P2, Deck, Game);
						}
						else
						{
							Console.WriteLine("Please select which card you wish to drop from your hand");
							Console.WriteLine("*TYPE NUMBER IN BRACKET*");
							//temp card now has card info and is ready to be dropped
							int replace = Convert.ToInt32(Console.ReadLine());
							Card ctemp = hand[replace];

							//Now we replace
							hand.Remove(hand[replace]);
							Deck.gDeck[0] = null;
							Deck.gDeck[0] = ctemp;

							Console.Clear();
							DisplayHand();
							Console.WriteLine("*YOU END YOUR TURN*");
						}

						break;
					}


				//Hit
				case 3:
					{
						List<Card> temp = hand;

						hand.Add(Deck.gDeck[0]);
						Console.Clear();
						DisplayHand();

						Hit(P1, P2, Deck, Game);

						if (hand == temp)
						{
							hand.Remove(Deck.gDeck[0]);
							Console.WriteLine("*INVALID INPUT TRY AGAIN*");
							Console.ReadLine();
							Swap(P1, P2, Deck, Game);
						}
						else
						{
							Console.WriteLine("Please select which card you wish to drop from your hand");
							Console.WriteLine("*TYPE NUMBER IN BRACKET*");
							//temp card now has card info and is ready to be dropped
							int replace = Convert.ToInt32(Console.ReadLine());
							Card ctemp = hand[replace];

							//Now we replace
							hand.Remove(hand[replace]);
							Deck.gDeck[0] = null;
							Deck.gDeck[0] = ctemp;

							Console.Clear();
							DisplayHand();
							Console.WriteLine("*YOU END YOUR TURN*");
						}

						break;
					}


				//Invalid Input
				case 4:
					{
						Console.WriteLine("*INVALID INPUT TRY AGAIN*");
						Console.ReadLine();
						Swap(P1, P2, Deck, Game);
						break;
					}

			}

		}

		public Deck End(Deck Deck)
		{
			return Deck;
		}

		public string ShowSpread()
		{
			string temp = "";

			if (spread == null)
			{
				return temp;
			}

			if (spread.Count > 0)
			{
				string spread1 = "";
				string spread2 = "";

				for (int i = 0; i < spread.Count; i++)
				{
					for (int j = 0; j < spread[i].Length; j++)
					{
						//First check if its all numbers or an chronological spread
						if (spread[i][0].Getnumber() == spread[i][1].Getnumber())
						{
							if (spread1 == "")
							{
								spread1 = "Spread of " + spread[i][0].Getnumber() + "'s ";
							}
							else
							{
								//spread2 = "Spread of " + spread[i][j].Getnumber() + "'s ";
							}
						}
						else
						{
							//For Chronological
							if (spread[i][0].Getsuit() == spread[i][1].Getsuit())
							{
								if (spread1 == "")
								{
									spread1 = "Spread of " + spread[i][0].Getsuit() + "'s " + spread[i][0].Getnumber() + spread[i][1].Getnumber() + spread[i][2].Getnumber() + "...";
								}
								else
								{

								}
							}
						}
					}
				}
				

				string final = pname + " : " + spread1 + " " + spread2;

				return final;
				
			}
			

			return temp;
			
		}

		public int Add(int val1, int val2)
		{
			int total = 0;
			total = val1 + val2;
			return total;
		}

		public int Getscore()
		{
			return score;
		}

		public void Drop(Cpu P2, Deck Deck)
		{
			int playertotal = 0;

			for (int i = 0; i < hand.Count; i++)
			{
				int temp = 0;
				switch (hand[i].Getnumber())
				{
					case "Ace":
						temp = 1;
						playertotal = Add(playertotal, temp);
						break;

					case "2":
						temp = 2;
						playertotal = Add(playertotal, temp);
						break;

					case "3":
						temp = 3;
						playertotal = Add(playertotal, temp);
						break;

					case "4":
						temp = 4;
						playertotal = Add(playertotal, temp);
						break;

					case "5":
						temp = 5;
						playertotal = Add(playertotal, temp);
						break;

					case "6":
						temp = 6;
						playertotal = Add(playertotal, temp);
						break;

					case "7":
						temp = 7;
						playertotal = Add(playertotal, temp);
						break;

					case "8":
						temp = 8;
						playertotal = Add(playertotal, temp);
						break;

					case "9":
						temp = 9;
						playertotal = Add(playertotal, temp);
						break;

					case "10":
						temp = 10;
						playertotal = Add(playertotal, temp);
						break;

					case "Jack":
						temp = 10;
						playertotal = Add(playertotal, temp);
						break;

					case "Queen":
						temp = 10;
						playertotal = Add(playertotal, temp);
						break;

					case "King":
						temp = 10;
						playertotal = Add(playertotal, temp);

						break;

				}
			}

			Setscore(playertotal);

			int cputotal = 0;

			for (int i = 0; i < P2.GetHand().Count; i++)
			{
				int temp = 0;
				switch (P2.GetHand()[i].Getnumber())
				{
					case "Ace":
						temp = 1;
						cputotal = Add(cputotal, temp);
						break;

					case "2":
						temp = 2;
						cputotal = Add(cputotal, temp);
						break;

					case "3":
						temp = 3;
						cputotal = Add(cputotal, temp);
						break;

					case "4":
						temp = 4;
						cputotal = Add(cputotal, temp);
						break;

					case "5":
						temp = 5;
						cputotal = Add(cputotal, temp);
						break;

					case "6":
						temp = 6;
						cputotal = Add(cputotal, temp);
						break;

					case "7":
						temp = 7;
						cputotal = Add(cputotal, temp);
						break;

					case "8":
						temp = 8;
						cputotal = Add(cputotal, temp);
						break;

					case "9":
						temp = 9;
						cputotal = Add(cputotal, temp);
						break;

					case "10":
						temp = 10;
						cputotal = Add(cputotal, temp);
						break;

					case "Jack":
						temp = 10;
						cputotal = Add(cputotal, temp);
						break;

					case "Queen":
						temp = 10;
						cputotal = Add(cputotal, temp);
						break;

					case "King":
						temp = 10;
						cputotal = Add(cputotal, temp);
						break;

				}
			}

			P2.Setscore(cputotal);

			switch (score < P2.Getsscore())
			{
				case true:
					Console.Clear();
					DisplayHand(); P2.DisplayHand();
					Console.WriteLine("You Drop With A Card Total Of " + score + " While " + P2.Getname() + " Had " + P2.Getsscore());
					Console.WriteLine("*" + pname + " WINS*");
					Console.WriteLine("*PRESS ENTER TO CONTINUE*");
					Console.ReadLine();

					break;

				case false:
					Console.Clear();
					DisplayHand(); P2.DisplayHand();
					Console.WriteLine("You Drop With A Card Total Of " + score + " While " + P2.Getname() + " Had " + P2.Getsscore());

					Console.WriteLine("*" + P2.Getname() + " WINS*");
					Console.WriteLine("*PRESS ENTER TO CONTINUE*");
					Console.ReadLine();
					break;
			}


		}

	}
}
