using System;

namespace SlotMachineGame
{
    class Program
    {
        static void Main(string[] args)
        {
            // Game initialization
            int playerMoney = 100; // Initial money
            int betAmount = 10; // Default bet amount

            // Game loop
            while (playerMoney > 0)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Welcome to Slot Machine!");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"You have ${playerMoney}\n");

                // Get user input for bet
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Enter your bet amount (or 0 to quit): ");
                Console.ForegroundColor = ConsoleColor.White;

                bool isValidBet = int.TryParse(Console.ReadLine(), out betAmount);
                if (!isValidBet || betAmount < 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid bet amount! Please enter a positive number.");
                    Console.ForegroundColor = ConsoleColor.White;
                    continue;
                }

                if (betAmount == 0)
                {
                    break;
                }

                if (betAmount > playerMoney)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("You don't have enough money! Try again.");
                    Console.ForegroundColor = ConsoleColor.White;
                    continue;
                }

                // Subtract bet amount from player money
                playerMoney -= betAmount;

                // Spin the reels
                string[] reel1 = SpinReel();
                string[] reel2 = SpinReel();
                string[] reel3 = SpinReel();

                // Display the reels
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Spinning...");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"{reel1[0]} | {reel2[0]} | {reel3[0]}");
                Console.WriteLine($"{reel1[1]} | {reel2[1]} | {reel3[1]}");
                Console.WriteLine($"{reel1[2]} | {reel2[2]} | {reel3[2]}\n");

                // Calculate winnings
                int winnings = CalculateWinnings(reel1, reel2, reel3, betAmount);

                // Update player money and display result
                playerMoney += winnings;
                if (winnings > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Congratulations! You won ${winnings}.");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Sorry, you didn't win this time.");
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"Your balance is now: ${playerMoney}\n");

                if (playerMoney <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("You have run out of money!");
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                }

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Game over! You ran out of money or quit.");
            Console.ForegroundColor = ConsoleColor.White;
        }

        // Spin the reel and return 3 random symbols
        static string[] SpinReel()
        {
            string[] symbols = { "Cherry", "Lemon", "Orange", "Plum", "Bell", "Bar", "7" };
            Random rand = new Random();
            string[] reel = new string[3];
            for (int i = 0; i < 3; i++)
            {
                reel[i] = symbols[rand.Next(symbols.Length)];
            }
            return reel;
        }

        // Calculate winnings based on the combination
        static int CalculateWinnings(string[] reel1, string[] reel2, string[] reel3, int betAmount)
        {
            if (reel1[0] == reel2[0] && reel2[0] == reel3[0]) // If all 3 symbols match
            {
                switch (reel1[0])
                {
                    case "Cherry":
                        return betAmount * 2; // Win 2x your bet
                    case "Lemon":
                        return betAmount * 3; // Win 3x your bet
                    case "Orange":
                        return betAmount * 4; // Win 4x your bet
                    case "Plum":
                        return betAmount * 5; // Win 5x your bet
                    case "Bell":
                        return betAmount * 6; // Win 6x your bet
                    case "Bar":
                        return betAmount * 7; // Win 7x your bet
                    case "7":
                        return betAmount * 10; // Win 10x your bet
                    default:
                        return 0;
                }
            }
            return 0; // No win
        }
    }
}
