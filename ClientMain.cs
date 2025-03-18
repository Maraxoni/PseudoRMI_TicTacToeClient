
using System.ServiceModel;

namespace PseudoRMI_TicTacToeClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var binding = new BasicHttpBinding();
            var endpoint = new EndpointAddress("http://localhost:8080/TicTacToeService");

            ChannelFactory<ITicTacToeService> factory = new ChannelFactory<ITicTacToeService>(binding, endpoint);
            ITicTacToeService client = factory.CreateChannel();

            Console.WriteLine("Tic-Tac-Toe Game\n");

            Console.WriteLine("Choose game mode:");
            Console.WriteLine("1. Play on the same computer");
            Console.WriteLine("2. Play on two computers");
            Console.Write("Enter your choice (1 or 2): ");
            string gameModeChoice = Console.ReadLine();

            // Determine if the game will be local or remote
            bool isLocalGame = gameModeChoice == "1";
            char player;

            if (isLocalGame)
            {
                player = 'X';
            }
            else
            {
                do
                {
                    Console.Write("Enter your player symbol (X or O): ");
                    player = char.ToUpper(Console.ReadKey().KeyChar);
                    Console.WriteLine();
                } while (player != 'X' && player != 'O');
            }
            

            while (true)
            {
                Console.Clear();
                Console.WriteLine(await Task.Run(() => client.GetBoard()));
                Console.WriteLine($"Player {player}, enter your move (row and column, e.g., '1 2'):");

                int row = -1, col = -1;
                while (true)
                {
                    string input = Console.ReadLine();
                    string[] parts = input.Split(' ');

                    if (parts.Length == 2 && int.TryParse(parts[0], out row) && int.TryParse(parts[1], out col)
                        && row >= 0 && row < 3 && col >= 0 && col < 3)
                    {
                        break;
                    }
                    Console.WriteLine("Invalid input! Enter row and column as 'row column' (e.g., '1 2'):");
                }

                string result = await Task.Run(() => client.MakeMove(row, col, player));
                Console.WriteLine(result);

                if (result.Contains("wins"))
                    break;
                if (!result.Contains("Invalid move!"))
                {
                    if (isLocalGame)
                    {
                        player = (player == 'X') ? 'O' : 'X';
                    }
                }
                
            }

            Console.WriteLine("Game over!");
        }
    }
}