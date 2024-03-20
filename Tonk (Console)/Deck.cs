using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using Microsoft.VisualBasic.FileIO;

namespace Tonk__Console_
{
	class Deck
	{
		//52 draw card deck
		Card[] dDeck = new Card[52];

		//This deck is for the cards that are placed and can be picked up
		public Card[] gDeck = new Card[1];

		public int iDecktotal = 0;

		//RandomCard Maker
		public Card RandomCard()
		{
			string[] suits = new string[4] { "Spades", "Club", "Hearts", "Diamonds" };
			string[] nums = new string[13] { "Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King" };
			Random rnd = new Random();

			int _rsuit = rnd.Next(suits.Length);
			string rsuit = suits[_rsuit];

			int rnum = rnd.Next(nums.Length);
			string num = nums[rnum];

			Card nCard = new Card(num,rsuit,0);

			return nCard;
		}

		//"Shuffles" the deck, use for new games/ reset as this is the initial start method for a deck.
		public Deck ShuffleDeck(Deck _deck)
		{
			//First check if deck is actually done (for recursion reasons)
			for (int i = 0; i < 52; i++)
			{
				if (_deck.dDeck[51] != null)
					return _deck;
			}

			//Random Card Gen
			Card temp = RandomCard();

			//Card Dupe Check Loop
			for (int i = 0; i < 52; i++)
			{
				if (_deck.dDeck[i] == null)
					continue;

				if (temp.Getnumber() == _deck.dDeck[i].Getnumber() && temp.Getsuit() == _deck.dDeck[i].Getsuit())
				{
					ShuffleDeck(_deck);
				}
			}

			//Adding new card to deck and then recall
			for (int i = 0; i < 52; i++)
			{
				if (_deck.dDeck[i] == null)
				{
					_deck.dDeck[i] = temp;
					ShuffleDeck(_deck);
				}

				if (_deck.dDeck[51] != null)
				{
					return _deck;
				}
			}

			return _deck;
		}

		public Card[] GetDdeck()
		{
			return dDeck;
		}

		public void SetDdeck(Card[] _gdeck)
		{
			_gdeck = dDeck;
		}

		//For a hand
		public Card[] Dealhand(Deck deck)
		{
			Card[] nhand = new Card[5];
			//tally for cards
			int max = 0;

			for (int i = 0; i < 5; i++)
			{
				if (max == 5)
				{
					return nhand;
				}

				if (deck.dDeck[i] == null)
				{
					
					for (int j = 0; j < 52; j++)
					{
						if (max == 5)
						{
							return nhand;
						}

						if (deck.dDeck[j] != null)
						{
							nhand[i] = deck.dDeck[j];
							deck.dDeck[j] = null;
							max++;
							i++;
						}
					}
				}
				else
				{
					nhand[i] = deck.dDeck[i];
					deck.dDeck[i] = null;
					max++;
				}
			}

			return nhand;
		}

		public int Decktotal()
		{
			int cardtotal = 0;

			for (int i = 0; i < 52; i++)
			{
				if (dDeck[i] != null)
				{
					cardtotal++;
				}
			}

			iDecktotal = cardtotal;
			return cardtotal;
		}

		public bool ChronoCheck(Card one, Card two, Card three, Player P1)
		{
			bool a = false;
			int c1 = one.GetVal();
			int c2 = two.GetVal();
			int c3 = three.GetVal();

			int[] cardorder = new int[3];
			cardorder[0] = c1;
			cardorder[1] = c2;
			cardorder[2] = c3;

			for (int i = 1; i < 3; i++)
			{
				//card 1 is more than card 2
				if (cardorder[i - 1] > cardorder[i])
				{
					a = false;
					return a;
				}

				//card 3 is less than other cards
				if (cardorder[2] < cardorder[i])
				{
					a = false;
					return a;
				}

				//card 2 is more than 1 but more than 3
				if (cardorder[1] > cardorder[0] && cardorder[1] > cardorder[2])
				{
					a = false;
					return a;
				}

				a = true;
				return a;

			}

				return a;
		}
	}
}
