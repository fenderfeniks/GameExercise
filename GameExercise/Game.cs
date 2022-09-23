using System;
using System.Collections.Generic;
using System.Text;

namespace GameExercise
{
    internal class Game
    {
        private int width;

        public int Width { get => width; set => width = value; }

        private int height;

        public int Height { get => height; set => height = value; }


        private Random random = new Random();

        public void Run()
        {
            Console.OutputEncoding = Encoding.UTF8;
            GetGameSquare();
            Creature[,] gameSquare = CreateGameSqeare();
            GameProcess(gameSquare);
            Restart(gameSquare);
        }
        private void GameProcess(Creature[,] gameSquare)
        {           
            Player player = new Player(0, 0);

            gameSquare[player.Y, player.X] = player;

            Vampire vampire = new Vampire(RandomWidth(), RandomHeight());

            gameSquare[vampire.Y, vampire.X] = vampire;

            Bat bat = new Bat(RandomWidth(), RandomHeight());
            bat = (Bat)CheckingPosition(bat, vampire);
            gameSquare[bat.Y, bat.X] = bat;

            Pit pit = new Pit(RandomWidth(), RandomHeight());
            pit = (Pit)CheckingPosition(pit, vampire, bat);
            gameSquare[pit.Y, pit.X] = pit;


            while (player.IsAlive)
            {
                Console.Clear();
                PrintGameSquare(gameSquare);
                CheckingPlayerAliveStatus(player, vampire, pit);
                if(!player.IsAlive) break;
                CheckingForTeleport(player, gameSquare, bat);
                CheckingPlayerPosition(player, vampire, bat, pit);

                Console.WriteLine("Вы можете передвигаться используя клавиши W A S D и атаковать используя клавиши \u2190 \u2191 \u2192 \u2193");
                Console.WriteLine("Сделайте свой ход");
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.A
                    || key.Key == ConsoleKey.W
                    || key.Key == ConsoleKey.S
                    || key.Key == ConsoleKey.D)
                {
                    gameSquare = player.Move(key, gameSquare);
                }
                else if (key.Key == ConsoleKey.LeftArrow
                    || key.Key == ConsoleKey.RightArrow
                    || key.Key == ConsoleKey.UpArrow
                    || key.Key == ConsoleKey.DownArrow)
                {
                    player.Attack(key, vampire, bat, pit);
                    if (!vampire.IsAlive)
                    {
                        Console.WriteLine("Вы победили!");
                        break;
                    }
                    if(bat != null)
                    {
                        if (!bat.IsAlive)
                        {
                            gameSquare[bat.Y, bat.X] = new Creature(bat.X, bat.Y);
                            bat = null;
                        }
                    }
                    if(pit != null)
                    {
                        if (!pit.IsAlive)
                        {
                            gameSquare[pit.Y, pit.X] = new Creature(pit.X, pit.Y);
                            pit = null;
                        }
                    }
                    

                }
                else
                {
                    Console.WriteLine("Неизвестный символ, в качестве наказания вы пропускаете ход");
                }
                vampire.Action(gameSquare);
            }
        }
        private void Restart(Creature[,] gameSquare)
        {
            while (true)
            {
                Console.WriteLine("Желаете сыграть еще раз?");
                Console.WriteLine("Если хотите переиграть введите Да или +");
                string decision = (string)Console.ReadLine().Trim();
                if (decision.Equals("Да", StringComparison.OrdinalIgnoreCase) || decision.Equals("+"))
                {
                    GetGameSquare();
                    gameSquare = CreateGameSqeare();
                    GameProcess(gameSquare);
                }
                else
                {
                    break;
                }
            }
        }
        private void CheckingPlayerAliveStatus(Player player, Vampire vampire, Pit pit1)
        {
            if(pit1 != null) {
                if (player.X == pit1.X && player.Y == pit1.Y)
                {
                    player.IsAlive = false;
                    Console.WriteLine("Вы умерли");
                }
            }           
            if (player.X == vampire.X && player.Y == vampire.Y) {
                    player.IsAlive = false;
                    Console.WriteLine("Вы умерли");
            }
          
            
        }
        private void CheckingForTeleport(Player player, Creature[,] gameSquare, Bat bat)
        {   
            if (bat != null)
            {
                if (player.X == bat.X && player.Y == bat.Y)
                {
                    Console.WriteLine("Вы наступили на летучую мышь, принудительная телепортация!");
                    gameSquare[player.Y, player.X] = new PlayerWasHere(player.Y, player.X);
                    player.X = RandomWidth();
                    player.Y = RandomHeight();
                    gameSquare[player.Y, player.X] = player;
                    Console.Clear();
                    PrintGameSquare(gameSquare);
                }
            }           
        }
        private void CheckingPlayerPosition(Player player, Vampire vampire, Bat bat, Pit pit)
        {
            List<Creature> list = new List<Creature>();
            list.Add(vampire);
            list.Add(bat);
            list.Add(pit);
            foreach (Creature creature in list)
            {
                if(creature != null)
                {
                    string message;
                    if (creature is Vampire)
                    {
                        message = "Вы чувствуете вонь";

                    }
                    else if (creature is Bat)
                    {
                        message = "Вы слышите шелест";
                    }
                    else
                    {
                        message = "Вы чувствуете сквозняк";

                    }


                    if (player.X + 1 == creature.X && player.Y + 1 == creature.Y
                        || player.X + 1 == creature.X && player.Y - 1 == creature.Y
                        || player.X + 1 == creature.X && player.Y == creature.Y)
                    {
                        Console.WriteLine(message);
                    }
                    else if (player.X - 1 == creature.X && player.Y + 1 == creature.Y
                        || player.X - 1 == creature.X && player.Y - 1 == creature.Y
                        || player.X - 1 == creature.X && player.Y == creature.Y)
                    {
                        Console.WriteLine(message);
                    }
                    else if (player.X == creature.X && player.Y + 1 == creature.Y
                        || player.X == creature.X && player.Y - 1 == creature.Y)
                    {
                        Console.WriteLine(message);
                    }
                }                
            }
        }
        private void PrintGameSquare(Creature[,] gameSquare)
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    Console.Write("[");
                    if (gameSquare[i, j] is Player)
                    {
                        Console.Write("@");
                    }
                    else if (gameSquare[i, j] is PlayerWasHere)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("#");
                        Console.ResetColor();
                    }
                    else { Console.Write(" "); }
                    Console.Write("]");

                }
                Console.WriteLine();
            }
        }
        private Creature[,] CreateGameSqeare()
        {
            Creature[,] gameSquare = new Creature[Height, Width];

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    gameSquare[i, j] = new Creature(i, j);
                }
            }


            return gameSquare;
        }
        private Creature CheckingPosition(Creature creature, Creature creatureForCheck)
        {
            do
            {
                if (creature.X == creatureForCheck.X
                    || creature.Y == creatureForCheck.Y)
                {
                    creature.X = RandomWidth();
                    creature.Y = RandomHeight();
                }
                else
                {
                    break;
                }
            } while (true);


            return creature;
        }
        private Creature CheckingPosition(Creature creature, Creature creatureForCheck1, Creature creatureForCheck2)
        {
            do
            {
                if (creature.X == creatureForCheck1.X
                    || creature.Y == creatureForCheck1.Y
                    || creature.X == creatureForCheck2.X
                    || creature.Y == creatureForCheck2.Y)
                {
                    creature.X = RandomWidth();
                    creature.Y = RandomHeight();
                }
                else
                {
                    break;
                }
            }
            while (true);


            return creature;
        }
        private int RandomWidth()
        {
            int num;
            num = random.Next(2, Width);
            return num;
        }
        private int RandomHeight()
        {
            int num;
            num = random.Next(2, height);
            return num;
        }
        private void GetGameSquare()
        {
            Console.WriteLine("Добро Пожаловать в игру");
            do
            {
                Console.WriteLine("Введите ширину поля, она должна быть больше 4");
                Width = int.Parse(Console.ReadLine());
                if (Width <= 4)
                {
                    Console.WriteLine("Ширина поля должна быть больше 4");
                    continue;
                }
                Console.WriteLine("Введите Высоту поля, она должна быть больше 4");
                Height = int.Parse(Console.ReadLine());
                if (Height <= 4)
                {
                    Console.WriteLine("Высота поля должна быть больше 4");
                    continue;
                }
                break;
            } while (true);

        }
    }
}
