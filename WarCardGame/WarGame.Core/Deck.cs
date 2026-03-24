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
        /// <summary>
        /// This stacks the cards and the random rng should make it random for what type of card the player gets.
        /// </summary>
        private Stack<Card> cards;
        private static Random rng = new Random();
        /// <summary>
        /// Here the public deck holds the Stack<Card>(), Initialize, & Shuffle
        /// </summary>
        public Deck()
        {
            cards = new Stack<Card>();
            Initialize();
            Shuffle();
        }
        /// <summary>
        /// This will help tell which players cards are the best and who wins the round.
        /// </summary>
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

        /// <summary>
        /// This shuffles the cards everytime the code is started.
        /// </summary>
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
        /// <summary>
        /// This keeps count on how many cards the player got from the round.
        /// </summary>
        /// <returns></returns>
        public bool HasCards()
        {
            return cards.Count > 0;
        }
        /// <summary>
        /// This will just draw out the cards for the players
        /// </summary>
        /// <returns></returns>
        public Card Draw()
        {
            return cards.Pop();
        }
    }
}
