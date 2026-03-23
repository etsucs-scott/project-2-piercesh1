using WarGame.Core;
using System.Collections.Generic;

namespace WarCardGame 
{
    public enum Suit { Hearts, Diamonds, Clubs, Spades }
    public enum Rank { Two = 2, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace }
    class Program
    {
        static void Main(string[] args)
        {
            Deck deck = new Deck();

            var playerHands = new Dictionary<string, Hand>
            {
                {"Player1", "new Hand()" },
                {"Player2", "new Hand()" }
            };

            var players = new List<string>(playerHands.Keys);
            int currentPlayerIndex = 0;

            while (deck.HasCards()) 
            {
                var player = players[currentPlayerIndex];

                playerHands[player].AddCard(deck.Draw());

                currentPlayerIndex = (currentPlayerIndex + 1) % players.Count;
            }

            int round = 0;
            const int MAX_ROUNDS = 10000;
            while (round < MAX_ROUNDS)
            {
                round++;

                foreach (var player in players)
                {
                    if (playerHands[player].Count == 0)
                    {
                        Console.WriteLine($"{player} has no cards left. Game over.");
                        return;
                    }
                }

                var playerCards = new Dictionary<string, Card>();
                var pot = new List<Card>();

                foreach (var player in players)
                {
                    Card card = playerHands[player].PlayCard();
                    playerCards[player] = card;
                    pot.Add(card);

                    Console.WriteLine($"{player} plays {card}");
                }

                string winner = null;
                Card highestCard = null;

                foreach (var kvp in playerCards)
                {
                    if (highestCard == null || kvp.Value.Rank > highestCard.Rank)
                    {
                        highestCard = kvp.Value;
                        winner = kvp.Key;
                    }
                }

                bool isTie = false;
                int sameCount = 0;
                foreach (var card in playerCards.Values)
                {
                    if (card.Rank == highestCard.Rank)
                        sameCount++;
                }

                if (sameCount > 1)
                {
                    isTie = true;
                }
                if (isTie)
                {
                    Console.WriteLine("ITS TIME FOR WAR!");
                    foreach (var player in players)
                    {
                        if (playerHands[player].Count >= 1)
                        {
                            pot.Add(playerHands[player].PlayCard());
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"{winner} winds the round and takes {pot.Count} cards");
                    foreach (var card in pot)
                    {
                        playerHands[winner].AddCard(card);
                    }
                }

                Console.WriteLine($"Round {round} complete\n");

            }
            Console.WriteLine("Reached max round limit(10000). game is a draw.");

        }
    }
}