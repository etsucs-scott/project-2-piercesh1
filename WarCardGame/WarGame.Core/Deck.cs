using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace WarGame.Core
{
    public class Deck
    {
        private Stack<Card> cards;
        private static Random rng = new Random();

        public Deck()
        {
            cards = new Stack<Card>();
            Initialize();
            Shuffle();
        }

        private void Initialize()
        {
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (Rank rank in Enum.GetValues(typeof(Rank)))
                {
                    cards.Push(new Card(suit, rank));
                }
            }
        }

        private void Shuffle()
        {
            var tempList = new List<Card>(cards);
            cards.Clear();

            for (int i = tempList.Count - 1; i > 0; i--)
            {
                int j = rng.Next(i + 1);

                var temp = tempList[i];
                tempList[i] = tempList[j];
                tempList[j] = temp;
            }

            foreach (var card in tempList)
            {
                cards.Push(card);
            }
        }

        public bool HasCards()
        {
            return cards.Count > 0;
        }

        public Card Draw()
        {
            return cards.Pop();
        }
    }
}
