using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;

namespace Tonk__Console_
{
	class Cpu : Player
	{
		private int score = 0;
		private string cname = null;
		private int handtotal = 0;
		public int hits = 0;
		int Scount = 0;
		private List<Card> chand = new List<Card>();
		public List<Card[]> cspread = new List<Card[]>();


		public void cResetspreads()
		{
			while (cspread.Count > 0)
			{
				for (int i = 0; i < cspread.Count; i++)
				{
					cspread.Remove(cspread[i]);
				}
			}
		}

		public void DisplayHand()
		{
			int cardn = 0;

			Console.WriteLine(" ");
			Console.WriteLine("* " + cname + " HAND*");
			Console.WriteLine("------------------------");

			for (int i = 0; i < chand.Count; i++)
			{
				string cnum = chand[i].Getnumber();
				string cs = chand[i].Getsuit();

				Console.WriteLine("[" + cardn + ".] " + cnum + " of " + cs + "\n");
				Console.WriteLine("------------------------");
				cardn++;
			}

			Console.WriteLine(" ");
		}
		public int Getsscore()
		{
			return score;
		}
		public void Setscore(int _score)
		{
			score = _score;
		}
		public void Counthand()
		{
			int temp = 0;
			for (int i = 0; i < chand.Count; i++)
			{
				if (chand[i] != null)
					temp++;
			}
			handtotal = temp;
		}

		public int Gethandtotal()
		{
			return handtotal;
		}

		public List<Card> GetHand()
		{
			return chand;
		}

		public void Setname()
		{
			string[] names = new string[19] { "Ivan","Ethan","Anthony","Karma","Kamryn","Rosa","Rosario","Mason","Travis","Lucas","Honey","Nestor","Ashley","Jazmine","Edward","Rae","Marielis","Garrett","WHOI$NTDRE"};
			Random r = new Random();
			int rnum = r.Next(names.Length);
			cname = names[rnum];
		}

		public string Getname()
		{
			return cname;
		}

		public void Sethand(Card[] _hand)
		{
			for (int i = 0; i < 5; i++)
			{
				chand.Add(_hand[i]);
				//hand[i] = _hand[i];
				_hand[i] = null;
			}
		}

		public int Hit(Player P1, Deck Deck) //hits suit spread with same #
		{
			int hit = 0;
			Card temp;

			//Check our hand for cards that can hit the spreads
			for (int i = 0; i < chand.Count; i++)
			{
				//Check Player First
				if (P1.Getspreads().Count() > 0)
				{
					//loop the list of spreads
					for (int x = 0; x < P1.Getspreads().Count(); x++)
					{
						for (int y = 0; y < P1.Getspreads()[x].Length; y++)
						{

							if (P1.Getspreads()[x][y] == null)
							{
								break;
							}

							if (hit == 1)
							{
								break;
							}

							//CHECK 1 - Same # Check, Ez
							if (chand[i].Getnumber() == P1.Getspreads()[x][y].Getnumber())
							{
								if (chand[i].Getnumber() == P1.Getspreads()[x + 1][y + 1].Getnumber())
								{
									for (int fs = 0; fs < P1.Getspreads()[x].Length; fs++)
									{
										//Find Next Free Spot
										if (P1.Getspreads()[x][fs] == null)
										{
											temp = chand[i];
											P1.Getspreads()[x][fs] = temp;
											chand.Remove(temp);
											hit = 1;
											break;
										}
									}
								}
								
							}

							//CHECK 2 - Chronological Order
							if (chand[i].Getsuit() == P1.Getspreads()[x][y].Getsuit())
							{
								for (int fs = 0; fs < P1.Getspreads()[x].Length; fs++)
								{
									//Find Next Free Spot [FORWARD]
									if (P1.Getspreads()[x][fs].GetVal() < chand[i].GetVal()) //card 1
									{
										if (P1.Getspreads()[x][fs + 1].GetVal() < chand[i].GetVal()) //card 2
										{
											if (P1.Getspreads()[x][fs + 2].GetVal() < chand[i].GetVal() && chand[i].GetVal() == P1.Getspreads()[x][fs + 2].GetVal()+1) //card 3
											{
												temp = chand[i];
												P1.Getspreads()[x][fs + 3] = temp;
												chand.Remove(temp);
												hit = 1;
												break;
											}
											else if (P1.Getspreads()[x][fs + 2] == null)
											{
												temp = chand[i];
												P1.Getspreads()[x][fs + 2] = temp;
												chand.Remove(temp);
												hit = 1;
												break;
											}
										}
										else if (P1.Getspreads()[x][fs + 1] == null)
										{
											temp = chand[i];
											P1.Getspreads()[x][fs + 1] = temp;
											chand.Remove(temp);
											hit = 1;
											break;
										}

									}
									else if (P1.Getspreads()[x][fs].GetVal() > chand[i].GetVal()) //[BACKWARDS]
									{
										if (P1.Getspreads()[x][fs - 1].GetVal() > chand[i].GetVal()) //card 2
										{
											if (P1.Getspreads()[x][fs - 2].GetVal() > chand[i].GetVal() && chand[i].GetVal() == P1.Getspreads()[x][fs - 2].GetVal() - 1) //card 3
											{
												temp = chand[i];
												P1.Getspreads()[x][fs - 3] = temp;
												chand.Remove(temp);
												hit = 1;
												break;
											}
											else if (P1.Getspreads()[x][fs - 2] == null)
											{
												temp = chand[i];
												P1.Getspreads()[x][fs - 2] = temp;
												chand.Remove(temp);
												hit = 1;
												break;
											}
											else
											{
												break;
											}
										}
										else if (P1.Getspreads()[x][fs - 1] == null)
										{
											temp = chand[i];
											P1.Getspreads()[x][fs - 1] = temp;
											chand.Remove(temp);
											hit = 1;
											break;
										}
										else
										{
											break;
										}
										////ADD TO ARRAY BACKWARDS
										//List<Card> tempS = new List<Card>();
										//temp = chand[i];
										//tempS.Add(temp);
										//for (int c = 0; c < P1.Getspreads()[x].Length; c++)
										//{
										//	tempS.Add(P1.Getspreads()[x][c]);
										//}

										//P1.Getspreads()[x] = tempS.ToArray();
										//chand.Remove(temp);
										//hit = 1;
										//break;
									}
									else if (P1.Getspreads()[x][fs] == null) //Free spot
									{
										temp = chand[i];
										P1.Getspreads()[x][fs] = temp;
										chand.Remove(temp);
										hit = 1;
										break;
									}
								}
							}

						}
					}
				}

				//Now Check Us
				if (cspread.Count > 0)
				{
					//loop the list of spreads
					for (int x = 0; x < cspread.Count; x++)
					{
						for (int y = 0; y < cspread[x].Length; y++)
						{
							//CHECK 1 - Same # Check, Ez
							if (chand[i].Getnumber() == cspread[x][y].Getnumber())
							{
								for (int fs = 0; fs < cspread[x].Length; fs++)
								{
									//Find Next Free Spot
									if (cspread[x][fs] == null)
									{
										temp = chand[i];
										cspread[x][fs] = temp;
										chand.Remove(temp);
										hit = 1;
										break;
									}
								}

							}

							//CHECK 2 - Chronological Order
							if (chand[i].Getsuit() == cspread[x][y].Getsuit())
							{
								for (int fs = 0; fs < cspread[x].Length; fs++)
								{
									//Find Next Free Spot [FORWARD]
									if (cspread[x][fs].GetVal() < chand[i].GetVal()) //card 1
									{
										if (cspread[x][fs + 1].GetVal() < chand[i].GetVal()) //card 2
										{
											if (cspread[x][fs + 2].GetVal() < chand[i].GetVal()) //card 3
											{
												temp = chand[i];
												cspread[x][fs + 3] = temp;
												chand.Remove(temp);
												hit = 1;
												break;
											}
											else if (cspread[x][fs + 2] == null)
											{
												temp = chand[i];
												cspread[x][fs + 2] = temp;
												chand.Remove(temp);
												hit = 1;
												break;
											}
										}
										else if (cspread[x][fs + 1] == null)
										{
											temp = chand[i];
											cspread[x][fs + 1] = temp;
											chand.Remove(temp);
											hit = 1;
											break;
										}

									}
									else if (cspread[x][fs].GetVal() > chand[i].GetVal()) //[BACKWARDS]
									{
										//ADD TO ARRAY BACKWARDS
										List<Card> tempS = new List<Card>();
										temp = chand[i];
										tempS.Add(temp);
										for (int c = 0; c < cspread[x].Length; c++)
										{
											tempS.Add(cspread[x][c]);
										}

										cspread[x] = tempS.ToArray();
										chand.Remove(temp);
										hit = 1;
										break;
									}
									else if (cspread[x][fs] == null) //Free spot
									{
										temp = chand[i];
										cspread[x][fs] = temp;
										chand.Remove(temp);
										hit = 1;
										break;
									}
								}
							}

						}
					}
				}
			}

			return hit;
		}

		public void Spread(Player P1, Deck Deck)
		{
			//Sort our hand
			List<Card> Nsorted = chand.OrderBy(x => x.GetVal()).ToList();
			List<Card> Ssorted = chand.OrderBy(x => x.Getsuit()).ThenBy(x => x.GetVal()).ToList();

			Card c1 = null;
			Card c2 = null;
			Card c3 = null;

			//CHECK 1 - Same # Check, Ez
			for (int i = 1; i < Nsorted.Count; i++)
			{
				//End of Hand
				if (Nsorted[i] == null)
				{
					break;
				}

				c1 = Nsorted[i - 1];
				c2 = Nsorted[i];

				if (c1.Getnumber() == c2.Getnumber()) 
				{
					if (i + 1 < Ssorted.Count)
					{
						c3 = Nsorted[i + 1];
						if (c2.Getnumber() == c3.Getnumber()) //If card 2 is good card 1 is not needed, and card 3 must be tested
						{
							Card[] Nspread = new Card[4];
							Nspread[0] = c1;
							Nspread[1] = c2;
							Nspread[2] = c3;

							cspread.Add(Nspread);

							chand.Remove(c1);
							chand.Remove(c2);
							chand.Remove(c3);

							break;
						}
					}
					
				}
			}

			//CHECK 2 - Chronological Order
			for (int i = 1; i < Ssorted.Count; i++)
			{
				//End of Hand
				if (Ssorted[i] == null)
				{
					break;
				}

				c1 = Ssorted[i - 1];
				c2 = Ssorted[i];

				if (c1.Getsuit() == c2.Getsuit() && c2.GetVal() == c1.GetVal()+1 ) //NEEDS NUMBER ORDER ALSO
				{
					if (i + 1 < Ssorted.Count)
					{
						c3 = Ssorted[i + 1];
						if (c2.Getsuit() == c3.Getsuit() && c3.GetVal() == c2.GetVal()+1) //If card 2 is good card 1 is not needed, and card 3 must be tested 
						{
							Card[] Sspread = new Card[4];
							Sspread[0] = c1;
							Sspread[1] = c2;
							Sspread[2] = c3;

							cspread.Add(Sspread);

							chand.Remove(c1);
							chand.Remove(c2);
							chand.Remove(c3);

							break;
						}
					}
					else
					{
						break;
					}

				}
			}

		}

		public Deck Play(Player P1, Cpu P2, Deck Deck, Game Game) //We play a little different
		{
			Counthand();

			//Check Placed card for any use in our hand
			if (Deck.gDeck[0] != null)
			{
				if (Swap(P1, Deck) == 1)
					return Deck;
			}
			
			//If we have 1 card left in our hand DROP!
			if (chand.Count == 1)
			{
				Drop(P1, Deck);
				Game.End(P1, P2, Deck);
			}

			//check ourselves for a spread first! (fastest way for us to win! >:))
			if (chand.Count >= 3)
			{
				Spread(P1, Deck);
				Draw(P1, P2, Deck, Game);
				return Deck;
			}

			//check for a hit on a P1 spread
			else if (P1.spread.Count > 0)
			{
				if (Hit(P1, Deck) == 1)
				{
					Console.WriteLine(cname + " Hit!");
				}

				Draw(P1, P2, Deck, Game);
				return Deck;
			}
			//check for a hit on myself
			else if (cspread.Count > 0)
			{
				if (Hit(P1, Deck) == 1)
				{
					Console.WriteLine(cname + " Hit!");
				}

				Draw(P1, P2, Deck, Game);
				return Deck;
			}
			
			//Draw(P1, P2, Deck, Game);

			return Deck;
		}

		public int Swap(Player P1, Deck Deck)
		{
			int ans = 0;
			for (int i = 0; i < chand.Count; i++)
			{

				//if drawed card #'s match one in our hand 
				if (Deck.gDeck[0].Getnumber() == chand[i].Getnumber())
				{
					chand = chand.OrderBy(x => x.GetVal()).ToList();

					for (int j = 0; j < chand.Count; i++)
					{
						Card temp = chand[chand.Count - 1];

						if (Deck.gDeck[0].Getnumber() == chand[chand.Count - 1].Getnumber())
						{
							for (int x = 0; x < chand.Count; x++)
							{
								if (chand[x].Getnumber() != Deck.gDeck[0].Getnumber())
								{
									chand[x] = null;
									chand[x] = Deck.gDeck[0];
									Deck.gDeck[0] = null;
									Deck.gDeck[0] = temp;
									ans = 1;

									break;
								}
							}
						}


							//Now we replace
						chand[chand.Count - 1] = null;
							chand[chand.Count - 1] = Deck.gDeck[0];
							Deck.gDeck[0] = null;
							Deck.gDeck[0] = temp;
						ans = 1;

							break;
			
					}
				}

				if (ans == 1)
				{
					break;
				}

				//Chronological Order check
				else if (Deck.gDeck[0].Getsuit() == chand[i].Getsuit()) 
				{
					List<Card> Ssorted = chand.OrderBy(x => x.Getsuit()).ThenBy(x => x.GetVal()).ToList();
					chand = Ssorted;

					Card temp = null;
					if (Deck.gDeck[0].Getsuit() == chand[chand.Count - 1].Getsuit())
					{
						int x = 0;
						for (int m = 0; m < chand.Count; m++)
						{
							if (chand[i].Getsuit() != Deck.gDeck[0].Getsuit())
							{
								x = i;
								break;
							}
						}
						temp = chand[x];
						chand[x] = null;
						chand[x] = Deck.gDeck[0];
						Deck.gDeck[0] = null;
						Deck.gDeck[0] = temp;
						ans = 1;
					}
					else
					{
						temp = chand.Last();
						chand[chand.Count - 1] = null;
						chand[chand.Count - 1] = Deck.gDeck[0];
						Deck.gDeck[0] = null;
						Deck.gDeck[0] = temp;
						ans = 1;
					}

					break;
				}

				if (ans == 1)
				{
					break;
				}
			}

			return ans;
			
		}

		public Deck Draw(Player P1, Cpu P2, Deck Deck, Game Game)
		{
			//Empty Deck
			if (Deck.Decktotal() == 0)
			{
				Console.WriteLine("Theres nothing left to draw...");
				Drop(P1, Deck);
				Game.End(P1, P2, Deck);
			}

			//Make a copy of the deck
			Card[] tmpd = Deck.GetDdeck();
			int check = 0;

			//Loop the deck
			for (int i = 0; i < 52; i++)
			{
				if (tmpd[i] == null)
				{
					//do nothing
				}
				else if (tmpd[i] != null)
				{
					for (int j = 0; j < chand.Count; j++)
					{
						//First Check for possible same numbers spread
						if (tmpd[i].Getnumber() == chand[j].Getnumber())
						{
							//if so lets replace!
							chand.OrderBy(x => x.GetVal());
							Card temp = null;
							temp = chand.Last();
							chand[chand.Count - 1] = null;
							chand[chand.Count - 1] = tmpd[i];
							Deck.gDeck[0] = null;
							Deck.gDeck[0] = temp;
							tmpd[i] = null;
							check = 1;

							break;

						}
						//Check for possible Chronological order spread
						else if (tmpd[i].Getsuit() == chand[j].Getsuit())
						{
							List<Card> Ssorted = chand.OrderBy(x => x.Getsuit()).ThenBy(x => x.GetVal()).ToList();

							chand = Ssorted;

							Card temp = null;
							temp = chand.Last();
							chand[chand.Count - 1] = null;
							chand[chand.Count - 1] = tmpd[i];
							Deck.gDeck[0] = null;
							Deck.gDeck[0] = temp;
							tmpd[i] = null;
							check = 1;

							break;
						}
						
					}

					if (check == 0) //If no spread then random replace
					{
						Random choice = new Random();
						int cchoice = choice.Next(1);

						if (cchoice == 1) //Drop the drawed card
						{
							Card temp = null;
							temp = tmpd[i];
							tmpd[i] = null;
							Deck.gDeck[0] = null;
							Deck.gDeck[0] = temp;
							check = 1;

						}
						else //Random replace a card in hand
						{
							Random r = new Random();
							int rnum = r.Next(chand.Count);
							Card temp = null;
							temp = chand[rnum];

							//Now we replace
							chand[rnum] = null;
							chand[rnum] = tmpd[i];
							Deck.gDeck[0] = null;
							Deck.gDeck[0] = temp;
							tmpd[i] = null;
							check = 1;
						}

					}

					if (check == 1)
					{
						break;
					}

				}

			}

			return Deck;
		}

		public void Drop(Player P1, Deck Deck)
		{
			Console.WriteLine(cname + " drops for their turn!");

			int playertotal = 0;

			for (int i = 0; i < P1.GetHand().Count; i++)
			{
				int temp = 0;
				switch (P1.GetHand()[i].Getnumber())
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

			for (int i = 0; i < chand.Count; i++)
			{
				int temp = 0;
				switch (chand[i].Getnumber())
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

			Setscore(cputotal);

			switch (P1.Getscore() < score)
			{
				case true:
					Console.Clear();
					P1.DisplayHand(); DisplayHand();
					Console.WriteLine("You Drop With A Card Total Of " + P1.Getscore() + " While " + cname + " Had " + score);
					Console.WriteLine("*" + P1.Getname() + " WINS*");
					Console.WriteLine("*PRESS ENTER TO CONTINUE*");
					Console.ReadLine();

					break;

				case false:
					Console.Clear();
					P1.DisplayHand(); DisplayHand();
					Console.WriteLine("You Drop With A Card Total Of " + P1.Getscore() + " While " + cname + " Had " + score);

					Console.WriteLine("*" + cname + " WINS*");
					Console.WriteLine("*PRESS ENTER TO CONTINUE*");
					Console.ReadLine();
					break;
			}
		}

		//Use to end commands faster
		public Deck End(Deck Deck)
		{
			return Deck;
		}

		public string ShowSpread()
		{
			string temp = "";

			if (cspread == null)
			{
				return temp;
			}

			if (cspread.Count > 0)
			{
				string spread1 = "";
				//string spread2 = "";

				for (int i = 0; i < cspread.Count; i++)
				{
					for (int j = 0; j < cspread[i].Length; j++)
					{
						//First check if its all numbers or an chronological spread
						if (cspread[i][j].Getnumber() == cspread[i][j++].Getnumber())
						{
							if (spread1 == "")
							{
								spread1 = "Spread of " + cspread[i][j].Getnumber() + "'s ";
							}
							else
							{
								//spread2 = "Spread of " + cspread[i][j].Getnumber() + "'s ";
							}
						}
						else
						{
							if (cspread[i][j].Getsuit() == cspread[i][j++].Getsuit())
							{

							}
						}
					}
				}

				string final = cname + " : " + spread1 + " ";

				return final;

			}

			return temp;

		}
	}
}
