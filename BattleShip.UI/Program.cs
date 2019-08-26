using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShip.BLL.GameLogic;
using BattleShip.BLL.Requests;
using BattleShip.BLL.Responses;
using BattleShip.BLL.Ships;

namespace BattleShip.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            Workflow workflow = new Workflow();
            workflow.Run();
        }
    }
    public class Input
    {
        Output output = new Output();

        public bool Play()
        {
            char result;

            while (!char.TryParse(Console.ReadLine().ToUpper(),out result))
            {
                output.InvalidEntry();
            }
            if (result == 'Y')
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public (int, int) GetCoordinate()
        {
            int x = 0;
            int y = 0;
            while (true)
            {
                string coordinate = Console.ReadLine().ToUpper();
                if (coordinate.Length > 3)
                {
                    output.InvalidEntry();
                }
                else
                {
                    while (char.Parse(coordinate.Substring(0, 1)) > 'J')
                    {
                        output.InvalidEntry();
                    }
                    x = char.Parse(coordinate.Substring(0, 1)) - 64;
                    
                    while (!int.TryParse(coordinate.Substring(1, 1), out y))
                    {
                        output.InvalidEntry();
                    }
                    y = int.Parse(coordinate.Substring(1, 1));
                    break;
                }
            }
            return (x, y);
        }

        public string GetName1()
        {
            string name1 = Console.ReadLine();
            return name1;
        }
        public string GetName2()
        {
            string name2 = Console.ReadLine();
            return name2;
        }
        public ShipDirection ShipPlacement()
        {
            string direction = Console.ReadLine().ToLower();
            while (true)
            {
                if (direction == "up")
                {
                    return ShipDirection.Up;
                }
                else if (direction == "down")
                {
                    return ShipDirection.Down;
                }
                else if (direction == "right")
                {
                    return ShipDirection.Right;
                }
                else if (direction == "left")
                {
                    return ShipDirection.Left;
                }
                else
                {
                    output.InvalidEntry();
                }
            }
        }
    }
    public class Output
    {
        public char[,] gameBoard1 = new char[10, 10];
        public char[,] gameBoard2 = new char[10, 10];
        public void StartScreen()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.WriteLine("888             888   888   888                888     d8b         ");
            Console.WriteLine("888             888   888   888                888     Y8P         ");
            Console.WriteLine("888             888   888   888                888                 ");
            Console.WriteLine("88888b.  8888b. 888888888888888 .d88b. .d8888b 88888b. 88888888b.  ");
            Console.WriteLine("888 '88b    '88b888   888   888d8P  Y8b88K     888 '88b888888  '88b");
            Console.WriteLine("888  888.d888888888   888   88888888888'Y8888b.888  888888888  888 ");
            Console.WriteLine("888 d88P888  888Y88b. Y88b. 888Y8b.         X88888  888888888 d88P ");
            Console.WriteLine("88888P' 'Y888888 'Y888 'Y888888 'Y8888  88888P'888  88888888888P'  ");
            Console.WriteLine("                                                          888      ");
            Console.WriteLine("                                                          888      ");
            Console.WriteLine("                                                          888      ");
            Console.ResetColor();
            Console.WriteLine("              Would you like to play Battle Ship?                  ");
        }
        public void CreateBoard1()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    gameBoard1[i, j] = '-';
                }
            }
        }
        public void CreateBoard2()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    gameBoard2[i, j] = '-';
                }
            }
        }
        public void ShowBoard1()
        {
            Console.WriteLine("       |       |       |       |       |       |       |       |       |       |       |");
            Console.WriteLine("       |   A   |   B   |   C   |   D   |   E   |   F   |   G   |   H   |   I   |   J   |");
            Console.WriteLine("_______|_______|_______|_______|_______|_______|_______|_______|_______|_______|_______|");
            for (int i = 0; i < 10; i++) 
            {
                Console.WriteLine("       |       |       |       |       |       |       |       |       |       |       |");
                if (i == 9)
                {
                    Console.Write($"   {i + 1}  |");

                }
                else
                {
                    Console.Write($"   {i + 1}   |");
                }
                for (int j = 0; j < 10; j++)
                {
                    if (gameBoard1[i, j] ==  'H')
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else if (gameBoard1[i, j] == 'M')
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    if (j == 9)
                    {
                        Console.WriteLine($"   {gameBoard1[i, j]}   |");
                    }
                    else
                    {
                        Console.Write($"   {gameBoard1[i, j]}   |");
                    }
                    Console.ResetColor();
                }
                Console.WriteLine("_______|_______|_______|_______|_______|_______|_______|_______|_______|_______|_______|");
            }
        }
        public void ShowBoard2()
        {
            Console.WriteLine("       |       |       |       |       |       |       |       |       |       |       |");
            Console.WriteLine("       |   A   |   B   |   C   |   D   |   E   |   F   |   G   |   H   |   I   |   J   |");
            Console.WriteLine("_______|_______|_______|_______|_______|_______|_______|_______|_______|_______|_______|");
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("       |       |       |       |       |       |       |       |       |       |       |");
                if (i == 9)
                {
                    Console.Write($"   {i + 1}  |");
                }
                else
                {
                    Console.Write($"   {i + 1}   |");
                }
                for (int j = 0; j < 10; j++)
                {
                    if (gameBoard2[i, j] == 'H')
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else if (gameBoard2[i, j] == 'M')
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    if (j == 9)
                    {
                        Console.WriteLine($"   {gameBoard2[i, j]}   |");
                    }
                    else
                    {
                        Console.Write($"   {gameBoard2[i, j]}   |");
                    }
                    Console.ResetColor();
                }
                Console.WriteLine("_______|_______|_______|_______|_______|_______|_______|_______|_______|_______|_______|");
            }
        }
        public void InvalidEntry()
        {
            Console.WriteLine("That is not a valid entry. Please try again.");
        }
        public void ExitGame()
        {
            Console.WriteLine("Thanks for playing.");
        }
        public void AskName1()
        {
            Console.Write("Player 1 please enter your name... ");
        }
        public void AskName2()
        {
            Console.Write("Player 2 please enter your name... ");
        }
        public void AskCoordinates(string name)
        {
            Console.Write($"{name}, please enter a coordinate to fire at...");
        }
        public void PlaceCarrier(string name)
        {
            Console.WriteLine($"{name}, please enter a coordinate to place your Carrier.");
        }
        public void PlaceBattleship(string name)
        {
            Console.WriteLine($"{name}, please enter a coordinate to place your Battleship.");
        }
        public void PlaceCruiser(string name)
        {
            Console.WriteLine($"{name}, please enter a coordinate to place your Cruiser.");
        }
        public void PlaceSubmarine(string name)
        {
            Console.WriteLine($"{name}, please enter a coordinate to place your Submarine.");
        }
        public void PlaceDestroyer(string name)
        {
            Console.WriteLine($"{name}, please enter a coordinate to place your Destroyer.");
        }
        public void ShipDirection()
        {
        Console.WriteLine("What direction would you like to place your ship?");
        }
        public void ChangeTurns(string name1, string name2)
        {
            Console.Clear();
            Console.WriteLine($"{name1}, your turn has finished. It is now {name2}'s turn.");
            Console.WriteLine($"{name2}, press any key to begin your turn.");
            Console.ReadKey();
        }
        public void InitiateGame(string name)
        {
            Console.Write($"{name}, you will go first. Please press any key to begin your turn.");
            Console.ReadKey();
        }
        public void Hit()
        {
            Console.WriteLine("Your shot was a hit!");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
        public void HitAndSink(string ship)
        {
            Console.WriteLine($"You hit and sunk your enemies {ship}!");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
        public void Miss()
        {
            Console.WriteLine("Your shot missed.");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
        public void Victory(string name)
        {
            Console.WriteLine($"Congratulations {name} your won!");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
    public class Workflow
    {
        public void Run()
        {
            Output output = new Output();
            Input input = new Input();
            Board board1 = new Board();
            Board board2 = new Board();
            PlaceShipRequest shipRequest = new PlaceShipRequest();
            PlaceShipRequest shipRequest2 = new PlaceShipRequest();
            char Winner = 'n';
            
            

            output.StartScreen();
            if (input.Play())
            {
                output.AskName1();
                string P1 = input.GetName1();
                output.AskName2();
                string P2 = input.GetName2();

                // player 1 place ships
                output.CreateBoard1();
                output.ShowBoard1();
                while (true)
                {
                    output.PlaceBattleship(P1);
                    var valuesB = input.GetCoordinate();
                    int x = valuesB.Item1;
                    int y = valuesB.Item2;
                    output.ShipDirection();
                    ShipDirection direction = input.ShipPlacement();
                    Coordinate coordinate = new Coordinate(x,y);
                    shipRequest.Coordinate = coordinate;
                    shipRequest.Direction = direction;
                    shipRequest.ShipType = ShipType.Battleship;
                    if (board1.PlaceShip(shipRequest) != ShipPlacement.Ok)
                    {
                        output.InvalidEntry();
                    }
                    else
                    {
                        break;
                    }
                }
                while (true)
                {
                    output.PlaceCarrier(P1);
                    var valuesC = input.GetCoordinate();
                    int x = valuesC.Item1;
                    int y = valuesC.Item2;
                    output.ShipDirection();
                    ShipDirection direction = input.ShipPlacement();
                    Coordinate coordinate = new Coordinate(x, y);
                    shipRequest.Coordinate = coordinate;
                    shipRequest.Direction = direction;
                    shipRequest.ShipType = ShipType.Carrier;
                    if (board1.PlaceShip(shipRequest) != ShipPlacement.Ok)
                    {
                        output.InvalidEntry();
                    }
                    else
                    {
                        break;
                    }
                }
                while (true)
                {
                    output.PlaceCruiser(P1);
                    var valuesCr = input.GetCoordinate();
                    int x = valuesCr.Item1;
                    int y = valuesCr.Item2;
                    output.ShipDirection();
                    ShipDirection direction = input.ShipPlacement();
                    Coordinate coordinate = new Coordinate(x, y);
                    shipRequest.Coordinate = coordinate;
                    shipRequest.Direction = direction;
                    shipRequest.ShipType = ShipType.Cruiser;
                    if (board1.PlaceShip(shipRequest) != ShipPlacement.Ok)
                    {
                        output.InvalidEntry();
                    }
                    else
                    {
                        break;
                    }
                }
                while (true)
                {
                    output.PlaceDestroyer(P1);
                    var valuesD = input.GetCoordinate();
                    int x = valuesD.Item1;
                    int y = valuesD.Item2;
                    output.ShipDirection();
                    ShipDirection direction = input.ShipPlacement();
                    Coordinate coordinate = new Coordinate(x, y);
                    shipRequest.Coordinate = coordinate;
                    shipRequest.Direction = direction;
                    shipRequest.ShipType = ShipType.Destroyer;
                    if (board1.PlaceShip(shipRequest) != ShipPlacement.Ok)
                    {
                        output.InvalidEntry();
                    }
                    else
                    {
                        break;
                    }
                }
                while (true)
                {
                    output.PlaceSubmarine(P1);
                    var valuesS = input.GetCoordinate();
                    int x = valuesS.Item1;
                    int y = valuesS.Item2;
                    output.ShipDirection();
                    ShipDirection direction = input.ShipPlacement();
                    Coordinate coordinate = new Coordinate(x, y);
                    shipRequest.Coordinate = coordinate;
                    shipRequest.Direction = direction;
                    shipRequest.ShipType = ShipType.Submarine;
                    if (board1.PlaceShip(shipRequest) != ShipPlacement.Ok)
                    {
                        output.InvalidEntry();
                    }
                    else
                    {
                        break;
                    }
                }

                output.ChangeTurns(P1, P2);

                //player 2 place ships
                output.CreateBoard2();
                output.ShowBoard2();
                while (true)
                {
                    output.PlaceBattleship(P2);
                    var valuesB = input.GetCoordinate();
                    int x = valuesB.Item1;
                    int y = valuesB.Item2;
                    output.ShipDirection();
                    ShipDirection direction = input.ShipPlacement();
                    Coordinate coordinate = new Coordinate(x, y);
                    shipRequest2.Coordinate = coordinate;
                    shipRequest2.Direction = direction;
                    shipRequest2.ShipType = ShipType.Battleship;
                    if (board2.PlaceShip(shipRequest2) != ShipPlacement.Ok)
                    {
                        output.InvalidEntry();
                    }
                    else
                    {
                        break;
                    }
                }
                while (true)
                {
                    output.PlaceCarrier(P2);
                    var valuesC = input.GetCoordinate();
                    int x = valuesC.Item1;
                    int y = valuesC.Item2;
                    output.ShipDirection();
                    ShipDirection direction = input.ShipPlacement();
                    Coordinate coordinate = new Coordinate(x, y);
                    shipRequest2.Coordinate = coordinate;
                    shipRequest2.Direction = direction;
                    shipRequest2.ShipType = ShipType.Carrier;
                    if (board2.PlaceShip(shipRequest2) != ShipPlacement.Ok)
                    {
                        output.InvalidEntry();
                    }
                    else
                    {
                        break;
                    }
                }
                while (true)
                {
                    output.PlaceCruiser(P2);
                    var valuesCr = input.GetCoordinate();
                    int x = valuesCr.Item1;
                    int y = valuesCr.Item2;
                    output.ShipDirection();
                    ShipDirection direction = input.ShipPlacement();
                    Coordinate coordinate = new Coordinate(x, y);
                    shipRequest2.Coordinate = coordinate;
                    shipRequest2.Direction = direction;
                    shipRequest2.ShipType = ShipType.Cruiser;
                    if (board2.PlaceShip(shipRequest2) != ShipPlacement.Ok)
                    {
                        output.InvalidEntry();
                    }
                    else
                    {
                        break;
                    }
                }
                while (true)
                {
                    output.PlaceDestroyer(P2);
                    var valuesD = input.GetCoordinate();
                    int x = valuesD.Item1;
                    int y = valuesD.Item2;
                    output.ShipDirection();
                    ShipDirection direction = input.ShipPlacement();
                    Coordinate coordinate = new Coordinate(x, y);
                    shipRequest2.Coordinate = coordinate;
                    shipRequest2.Direction = direction;
                    shipRequest2.ShipType = ShipType.Destroyer;
                    if (board2.PlaceShip(shipRequest2) != ShipPlacement.Ok)
                    {
                        output.InvalidEntry();
                    }
                    else
                    {
                        break;
                    }
                }
                while (true)
                {
                    output.PlaceSubmarine(P2);
                    var valuesS = input.GetCoordinate();
                    int x = valuesS.Item1;
                    int y = valuesS.Item2;
                    output.ShipDirection();
                    ShipDirection direction = input.ShipPlacement();
                    Coordinate coordinate = new Coordinate(x, y);
                    shipRequest2.Coordinate = coordinate;
                    shipRequest2.Direction = direction;
                    shipRequest2.ShipType = ShipType.Submarine;
                    if (board2.PlaceShip(shipRequest2) != ShipPlacement.Ok)
                    {
                        output.InvalidEntry();
                    }
                    else
                    {
                        break;
                    }
                }

                output.ChangeTurns(P2, P1);
                Random random = new Random();
                int order = random.Next(1, 10);
                if (order % 2 == 0)                   //PLAYER 1 GOES FIRST
                {
                    output.InitiateGame(P1);
                    while (Winner != 'y')
                    {
                        while (true)
                        {

                            output.ShowBoard1();
                            output.AskCoordinates(P1);
                            var values = input.GetCoordinate();
                            int x = values.Item1;
                            int y = values.Item2;
                            Coordinate coordinate = new Coordinate(x, y);
                            var shot = board2.FireShot(coordinate);
                            ShotStatus shotStatus = shot.ShotStatus;
                            string ship = shot.ShipImpacted;
                            if (shotStatus == ShotStatus.Hit)
                            {
                                output.Hit();
                                output.gameBoard1[y-1, x-1] = 'H';
                                break;
                            }
                            else if (shotStatus == ShotStatus.Miss)
                            {
                                output.Miss();
                                output.gameBoard1[y - 1, x - 1] = 'M';

                                break;
                            }
                            else if (shotStatus == ShotStatus.HitAndSunk)
                            {
                                output.HitAndSink(ship);
                                output.gameBoard1[y - 1, x - 1] = 'H';

                                break;
                            }
                            else if (shotStatus == ShotStatus.Victory)
                            {
                                output.Victory(P1);
                                output.gameBoard1[y - 1, x - 1] = 'H';
                                Winner = 'y';
                                break;
                            }
                        }


                        output.ChangeTurns(P1, P2);
                        while (true)
                        {

                            output.ShowBoard2();
                            output.AskCoordinates(P2);
                            var values2 = input.GetCoordinate();
                            int x2 = values2.Item1;
                            int y2 = values2.Item2;
                            Coordinate coordinate2 = new Coordinate(x2, y2);
                            var shot2 = board1.FireShot(coordinate2);
                            ShotStatus shotStatus2 = shot2.ShotStatus;
                            string ship2 = shot2.ShipImpacted;
                            if (shotStatus2 == ShotStatus.Hit)
                            {
                                output.Hit();
                                output.gameBoard2[y2 - 1, x2 - 1] = 'H';
                                break;
                            }
                            else if (shotStatus2 == ShotStatus.Miss)
                            {
                                output.Miss();
                                output.gameBoard2[y2 - 1, x2 - 1] = 'M';

                                break;
                            }
                            else if (shotStatus2 == ShotStatus.HitAndSunk)
                            {
                                output.HitAndSink(ship2);
                                output.gameBoard2[y2 - 1, x2 - 1] = 'H';
                                break;
                            }
                            else if (shotStatus2 == ShotStatus.Victory)
                            {
                                output.Victory(P2);
                                Winner = 'y';
                                break;
                            }
                            else
                            {
                                output.InvalidEntry();
                            }
                            output.ChangeTurns(P2, P1);
                        }
                    }
                }
                else                             // PLAYER 2 GOES FIRST
                {
                    while (Winner != 'y')
                    {
                        while (true)
                        {
                            output.InitiateGame(P2);
                            output.ShowBoard2();
                            output.AskCoordinates(P2);
                            var values = input.GetCoordinate();
                            int x = values.Item1;
                            int y = values.Item2;
                            Coordinate coordinate = new Coordinate(x, y);
                            var shot = board1.FireShot(coordinate);
                            ShotStatus shotStatus = shot.ShotStatus;
                            string ship = shot.ShipImpacted;
                            if (shotStatus == ShotStatus.Hit)
                            {
                                output.Hit();
                                output.gameBoard2[y - 1, x - 1] = 'H';
                                break;
                            }
                            else if (shotStatus == ShotStatus.Miss)
                            {
                                output.Miss();
                                output.gameBoard2[y - 1, x - 1] = 'M';
                                break;
                            }
                            else if (shotStatus == ShotStatus.HitAndSunk)
                            {
                                output.HitAndSink(ship);
                                output.gameBoard2[y - 1, x - 1] = 'H';
                                break;
                            }
                            else if (shotStatus == ShotStatus.Victory)
                            {
                                output.Victory(P2);
                                Winner = 'y';
                                break;
                            }
                        }


                        output.ChangeTurns(P2, P1);
                        while (true)
                        {
                            output.ShowBoard1();
                            output.AskCoordinates(P1);
                            var values2 = input.GetCoordinate();
                            int x2 = values2.Item1;
                            int y2 = values2.Item2;
                            Coordinate coordinate2 = new Coordinate(x2, y2);
                            var shot2 = board2.FireShot(coordinate2);
                            ShotStatus shotStatus2 = shot2.ShotStatus;
                            string ship2 = shot2.ShipImpacted;
                            if (shotStatus2 == ShotStatus.Hit)
                            {
                                output.Hit();
                                output.gameBoard1[y2 - 1, x2 - 1] = 'H';
                                break;
                            }
                            else if (shotStatus2 == ShotStatus.Miss)
                            {
                                output.Miss();
                                output.gameBoard1[y2 - 1, x2 - 1] = 'M';
                                break;
                            }
                            else if (shotStatus2 == ShotStatus.HitAndSunk)
                            {
                                output.HitAndSink(ship2);
                                output.gameBoard1[y2 - 1, x2 - 1] = 'H';
                                break;
                            }
                            else if (shotStatus2 == ShotStatus.Victory)
                            {
                                output.Victory(P1);
                                Winner = 'y';
                                break;
                            }
                            else
                            {
                                output.InvalidEntry();
                            }
                        }
                        output.ChangeTurns(P1, P2);
                    }
                }

            }
            else
            {
                output.ExitGame();
            }
            Console.ReadLine();

        }
    }
}

