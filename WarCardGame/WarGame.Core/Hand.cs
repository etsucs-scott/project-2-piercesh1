using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarGame.Core
{
    public class Hand
    {
        public Queue<Card> Cards {  get; private set; }

        public Hand()
        {
            Cards = new Queue<Card>();
        }
        /// <summary>
        /// This will add a card to the players hand
        /// </summary>
        /// <param name="card"></param>
        public void AddCard(Card card)
        {
            Cards.Enqueue(card);
        }
        /// <summary>
        /// This will allow you to play the card.
        /// </summary>
        /// <returns></returns>
        public Card PlayCard()
        {
            return Cards.Dequeue();
        }
        /// <summary>
        /// This keeps count of the cards
        /// </summary>
        public int Count => Cards.Count();
    }
}
