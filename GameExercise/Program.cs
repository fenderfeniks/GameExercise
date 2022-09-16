using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GameExercise
{
    interface Movable
    {

    }
    class Creature
    {
        public Creature(int x, int y)
        {
            this.width = x;
            this.height = y;
        }
        public int width;
        public int height;
    }
    class PlayerWasHere : Creature
    {
        public PlayerWasHere(int x, int y):base(x, y)
        {

        }
    }

    class Player : Creature
    {
        public static bool isPlayerAlive = true;
        public Player(int x, int y) : base(x, y)
        {

        }
                
        public Creature[,] Move(ConsoleKeyInfo key, Creature[,] gameSquare, Player player)
        {
            gameSquare[player.height, player.width] = new PlayerWasHere(player.height, player.width);
            if (key.Key == ConsoleKey.A)
            {
                if (player.width != 0)
                {
                    player.width--;
                }
                else
                {
                    Console.WriteLine("Вы достигли края карты");
                }
            }
            if (key.Key == ConsoleKey.D)
            {
                if (player.width < Program.width-1)
                {
                    player.width++;
                }
                else
                {
                    Console.WriteLine("Вы достигли края карты");
                }
            }
            if(key.Key == ConsoleKey.W)
            {
                if (player.height != 0)
                {
                    player.height--;
                }
                else
                {
                    Console.WriteLine("Вы достигли края карты");
                }
            }
            if(key.Key == ConsoleKey.S)
            {
                if (player.height != Program.height-1)
                {
                    player.height++;
                }
                else
                {
                    Console.WriteLine("Вы достигли края карты");
                }
            }
            gameSquare[player.height, player.width] = player;
            return gameSquare;
        }
        public void Attack(Creature creature, ConsoleKeyInfo key)
        {
            string creat;
            if (creature is Vampire)
            {
                creat = "вампиру";
            }else if(creature is Bat)
            {
                creat = "летучей мыши";
            }
            else
            {
                creat = "яме";
            }
            if(key.Key == ConsoleKey.LeftArrow)
            {
                if(width - 1 == creature.width)
                {
                    Console.WriteLine("Вы попали по " + creat);
                }
                else
                {
                    Console.WriteLine("Вы промахнулись");
                }             
            }
            if (key.Key == ConsoleKey.RightArrow)
            {
                if (width + 1 == creature.width)
                {
                    Console.WriteLine("Вы попали по " + creat);
                }
                else
                {
                    Console.WriteLine("Вы промахнулись");
                }
            }
            if (key.Key == ConsoleKey.UpArrow)
            {
                if (height + 1 == creature.width)
                {
                    Console.WriteLine("Вы попали по " + creat);
                }
                else
                {
                    Console.WriteLine("Вы промахнулись");
                }
            }
            if (key.Key == ConsoleKey.DownArrow)
            {
                if (height - 1 == creature.width)
                {
                    Console.WriteLine("Вы попали по " + creat);
                }
                else
                {
                    Console.WriteLine("Вы промахнулись");
                }
            }
        }
    }
    class Vampire : Creature
    {
        public Vampire(int x, int y) : base(x, y)
        {

        }
        public Creature[,] Action(Player player, Creature[,] gameSquare, Vampire vampire)
        {
            int num = Program.random.Next(0, 100);
            
            if (num <= 20)
            {
                Console.WriteLine("Вампир выбросил число " + num + " и решил атаковать");
                Attack(player,vampire);
            } 
            else if (num > 20 && num < 60)
            {
                Console.WriteLine("Вампир выбросил число " + num + " и решил переместиться");
                gameSquare = Move(gameSquare,vampire);
            }else
            {
                Console.WriteLine("Вампир выбросил число " + num + " и решил остаться на месте");
            }
            return gameSquare;
        }
        public void Attack(Player player, Vampire vampire)
        {
            if (vampire.width + 1 == player.width && vampire.height + 1 == player.height || vampire.width + 1 == player.width && vampire.height - 1 == player.height)
            {
                Console.WriteLine("Вампир атаковал и убил вас");
                Player.isPlayerAlive = false;
            }
            else if (vampire.width - 1 == player.width && vampire.height + 1 == player.height || vampire.width - 1 == player.width && vampire.height - 1 == player.height)
            {
                Console.WriteLine("Вампир атаковал и убил вас");
                Player.isPlayerAlive = false;
            }
            else if (vampire.width == player.width && vampire.height + 1 == player.height || vampire.width == player.width && vampire.height - 1 == player.height)
            {
                Console.WriteLine("Вампир атаковал и убил вас");
                Player.isPlayerAlive = false;
            }           
            else
            {
                Console.WriteLine("Вампир атаковал и промахнулся");
            }

        }
        public Creature[,] Move(Creature[,] gameSquare, Vampire vampire)
        {
            int x = Program.RandomWidth();
            int y = Program.RandomHeight();
            if (x < vampire.width)
            {
                if (vampire.width != 0)
                {
                    vampire.width--;
                }
            }
            else { 
            if (vampire.width != Program.width-1)
                {
                    vampire.width++;
                }

            } 
                 
            if (y > vampire.height)
            {
                if (vampire.height != Program.height-1)
                {
                    vampire.height++;
                }
             }
            else
            {
                if (vampire.height != 0)
                {
                    vampire.height--;
                }
                
            }
            gameSquare[vampire.height, vampire.width] = vampire;
            return gameSquare;
        }
    }
    class Bat : Creature
    {
        public Bat(int x, int y) : base(x, y)
        {

        }
    }
    class Pit : Creature
    {
        public Pit(int x, int y) : base(x, y)
        {

        }

    }



    internal class Program    
    {
        public static int width;
        public static int height;
        public static Random random = new Random();
        static void Main(string[] args)
        {   
            
            TakeGameSquare();
            Creature[,] gameSquare = CreateGameSqeare();
            GameProcess(gameSquare);
            Console.ReadLine();
                    
        }

        static void GameProcess(Creature[,] gameSquare)
        {
            Player player = new Player(0, 0);

            gameSquare[player.height, player.width] = player;

            Vampire vampire = new Vampire(RandomWidth(), RandomHeight());

            gameSquare[vampire.height, vampire.width] = vampire;

            Bat bat1 = new Bat(RandomWidth(), RandomHeight());
            bat1 = (Bat)CheckingPosition(bat1, vampire);
            gameSquare[bat1.height, bat1.width] = bat1;

            Pit pit1 = new Pit(RandomWidth(), RandomHeight());
            pit1 = (Pit)CheckingPosition(pit1, vampire);
            gameSquare[pit1.height, pit1.width] = pit1;

            
            while (Player.isPlayerAlive)
            {
                Console.Clear();
                PrintGameSquare(gameSquare);

                if (player.width == vampire.width && player.height == vampire.height || player.width == pit1.width && player.height == pit1.height)
                {
                    Player.isPlayerAlive = false;
                    Console.WriteLine("Вы умерли");
                }
                if (player.width == bat1.width && player.height == bat1.height)
                {
                    Console.WriteLine("Вы наступили на летучую мышь, принудительная телепортация!");
                    Teleport(player, gameSquare);
                }

                CheckingPlayerPosition(player, vampire);
                CheckingPlayerPosition(player, bat1);
                CheckingPlayerPosition(player, pit1);
                Console.WriteLine("Вы можете передвигаться используя клавиши W A S D и атаковать используя клавиши " + '\u2190' + '\u2191' + '\u2192' + '\u2193');
                Console.WriteLine("Сделайте свой ход");
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.A || key.Key == ConsoleKey.W || key.Key == ConsoleKey.S || key.Key == ConsoleKey.D)
                {
                   gameSquare = player.Move(key,gameSquare,player);
                }
                else if (key.Key == ConsoleKey.LeftArrow || key.Key == ConsoleKey.RightArrow || key.Key == ConsoleKey.UpArrow || key.Key == ConsoleKey.DownArrow)
                {
                    player.Attack(vampire,key);
                    player.Attack(bat1, key);
                    player.Attack(pit1, key);
                }
                else
                {
                    Console.WriteLine("Неизвестный символ, в качестве наказания вы пропускаете ход");
                }
                vampire.Action(player,gameSquare,vampire);

                
            }
        }
        static void Teleport(Player player, Creature[,] gameSquare)
        {
            gameSquare[player.height,player.width] = new PlayerWasHere(player.height,player.width);
            player.width = RandomWidth();
            player.height = RandomHeight();
            gameSquare[player.height, player.width] = player;
        }
        static void CheckingPlayerPosition(Player player, Creature creature)
        {
            string message;
            if (creature is Vampire)
            {
                message = "Вы чувствуете вонь";
                
            }
            else if(creature is Bat)
            {
                message = "Вы слышите шелест";
            }else
            {
                message = "Вы чувствуете сквозняк";
              
            }

            
            if(player.width+1 == creature.width && player.height +1 == creature.height || player.width + 1 == creature.width && player.height-1 == creature.height)
            {
                Console.WriteLine(message);
            }
            else if (player.width - 1 == creature.width && player.height + 1 == creature.height || player.width - 1 == creature.width && player.height - 1 == creature.height)
            {
                Console.WriteLine(message);
            }
            else if(player.width == creature.width && player.height+1 == creature.height || player.width == creature.width && player.height - 1 == creature.height)
            {
                Console.WriteLine(message);
            }           
        }


        static void PrintGameSquare(Creature[,] gameSquare)
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Console.Write("[");
                    if (gameSquare[i,j] is Player)
                    {
                        Console.Write("@");
                    }
                    else if(gameSquare[i,j] is PlayerWasHere)
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
        static Creature[,] CreateGameSqeare()
        {
            Creature[,] gameSquare = new Creature[height, width];

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    gameSquare[i, j] = new Creature(i,j);
                }
            }


            return gameSquare;
        }
        
        static Creature CheckingPosition(Creature creature, Creature creatureForCheck)
        { 
            if(creature.width == creatureForCheck.width || creature.height == creatureForCheck.height)
            {
                creature.width = RandomWidth();
                creature.height = RandomHeight();
                CheckingPosition(creature, creatureForCheck);
            }

            return creature;
        }
        static Creature CheckingPosition(Creature creature, Creature creatureForCheck1, Creature creatureForCheck2)
        {
            if (creature.width == creatureForCheck1.width || creature.height == creatureForCheck1.height || creature.width == creatureForCheck2.width || creature.height == creatureForCheck2.height)
            {
                creature.width = RandomWidth();
                creature.height = RandomHeight();
                CheckingPosition(creature, creatureForCheck1,creatureForCheck2);
            }

            return creature;
        }

        public static int RandomWidth()
        {
            int num;
            num = random.Next(2,width);
            return num;
        }
        public static int RandomHeight()
        {
            int num;
            num = random.Next(2, height);
            return num;
        }

        static void TakeGameSquare()
        {
            
            try
            {
                Console.WriteLine("Добро Пожаловать в игру");
                Console.WriteLine("Введите ширину поля, она должна быть больше 4");
                width = int.Parse(Console.ReadLine());
                if(width <= 4)
                {
                    Console.WriteLine("Ширина поля должна быть больше 4");
                    throw new Exception();
                }
                Console.WriteLine("Введите Высоту поля, она должна быть больше 4");
                height = int.Parse(Console.ReadLine());
                if (height <= 4)
                {
                    Console.WriteLine("Высота поля должна быть больше 4");
                    throw new Exception();
                }
            }
            catch(Exception)
            {             
                TakeGameSquare();
            }

            
        }
        
    }
}
