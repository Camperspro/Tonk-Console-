using System;
using System.Collections.Generic;
using System.Text;

namespace Tonk__Console_
{
	class Card
	{
		private string number = null;
		private string suit = null;
		private int value = 0;

		public Card(string _number, string _suit, int _value)
		{
			number = _number;
			suit = _suit;
			value = _value;
		}

		public int CompareTo(Card card)
		{

			if (card == null)
			{
				return -1;
			}
			else if (this == null)
			{
				return -1;
			}
			else if (card.Getsuit() == null)
			{
				return -1;
			}
			else
				return this.value.CompareTo(card.GetVal());
		}

		public string Getnumber()
		{
			return number;
		}

		public string Getsuit()
		{
			return suit;
		}

		public void Setnumber(string _number)
		{
			number = _number;
		}

		public void Setsuit(string _suit)
		{
			suit = _suit;
		}

		public void SetVal()
		{
			switch (this.Getnumber())
			{
				case "Ace":
					value = 1;
					break;

				case "2":
					value = 2;
					break;

				case "3":
					value = 3;
					break;

				case "4":
					value = 4;
					break;

				case "5":
					value = 5;
					break;

				case "6":
					value = 6;
					break;

				case "7":
					value = 7;
					break;

				case "8":
					value = 8;
					break;

				case "9":
					value = 9;
					break;

				case "10":
					value = 10;
					break;

				case "Jack":
					value = 11;
					break;

				case "Queen":
					value = 12;
					break;

				case "King":
					value = 13;
					break;

				case "NULL":
					value = 99;
					break;
			}
		}

		public int GetVal()
		{
			int c1 = 0;
			switch (this.Getnumber())
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

				case "NULL":
					c1 = 99;
					break;
			}

			return c1;
		}
	}
}
