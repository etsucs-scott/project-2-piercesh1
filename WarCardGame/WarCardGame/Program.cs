using WarGame.Core;
using System.Collections.Generic;

namespace WarCardGame 
{
    
    class Program
    {
        static void Main(string[] args)
        {
            
            Deck deck = new Deck();
            
            ///
            /// This will allow you to choose the amount of players to play in the game
            ///
            Console.WriteLine("How many players?: ");
            string input = Console.ReadLine();
            int numplayer = int.Parse(input);
            
            var playerHands = new Dictionary<string, Hand>();

             ///
             ///This is what will make it capable of adding more players.
             ///
            for (int i = 0; i < numplayer; i++)
            {
                playerHands[$"player {i + 1}"] = new Hand();
            }


            

            var players = new List<string>(playerHands.Keys);
            int currentPlayerIndex = 0;

            ///
            /// This will let the player gain the cards after they have one the round
            ///
            while (deck.HasCards()) 
            {
                var player = players[currentPlayerIndex];

                playerHands[player].AddCard(deck.Draw());

                currentPlayerIndex = (currentPlayerIndex + 1) % players.Count;
            }
            ///
            /// This will keep count on how many rounds its been and the max has been set to 10000
            /// It will also make sure that the rounds keep going until it hits 10000 unless a player loses the game.
            ///
            int round = 0;
            const int MAX_ROUNDS = 10000;
            while (round < MAX_ROUNDS)
            {
                round++;

                foreach (var player in players)
                {
                    ///
                    /// Once a player has reached 0, the game will end.
                    ///
                    if (playerHands[player].Count == 0)
                    {
                        Console.WriteLine($"{player} has no cards left. Game over.");
                        return;
                    }
                }
                ///
                /// The pot here is to keep track on what cards have been shown and who placed them.
                /// 
                var playerCards = new Dictionary<string, Card>();
                var pot = new List<Card>();

                ///
                ///This here will tell you which player put down what card and add the card to the pot.
                ///
                foreach (var player in players)
                {
                    Card card = playerHands[player].PlayCard();
                    playerCards[player] = card;
                    pot.Add(card);

                    Console.WriteLine($"{player} plays {card}");
                }
                ///
                ///This is to tell who one the round and who has the highest cards
                ///
                string winner = null;
                Card highestCard = null;

                foreach (var player in players)
                {

                }

                foreach (var kvp in playerCards)
                {
                    if (highestCard == null || kvp.Value.Rank > highestCard.Rank)
                    {
                        highestCard = kvp.Value;
                        winner = kvp.Key;
                    }
                }
                ///
                ///This will make sure to keep count on how many cards there are and if there is a tie
                ///
                bool isTie = false;
                int sameCount = 0;
                foreach (var card in playerCards.Values)
                {
                    if (card.Rank == highestCard.Rank)
                        sameCount++;
                }
                ///
                ///Here this will either continue a round if there is no tie.
                ///Or if the players do end up in a tie they will be forced to go to war!
                ///
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

                ///
                /// If there is no tie then player with the highest cards wins the round and takes the pot.
                ///
                else
                {
                    Console.WriteLine($"{winner} wins the round and takes {pot.Count} cards");
                    foreach (var card in pot)
                    {
                        playerHands[winner].AddCard(card);
                    }
                }
                ///
                /// This will show up when a round has been complete and what round the players are on.
                ///
                Console.WriteLine($"Round {round} complete\n");

            }
            if (players.Count == 1)
                Console.WriteLine($"\n{players[0]} wins the game!");
            ///
            /// This will only show if the max rounds have been met.
            ///
            Console.WriteLine("Reached max round limit 10000. game is a draw.");

        }
    }
}