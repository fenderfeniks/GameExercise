using System;
using System.Collections.Generic;

namespace GameExercise
{
    class Player : Creature
    {

        public Player(int x, int y) : base(x, y) { }

        public Creature[,] Move(ConsoleKeyInfo key, Creature[,] gameSquare)
        {
            gameSquare[Y, X] = new PlayerWasHere(Y, X);

            if (key.Key == ConsoleKey.A)
            {
                if (X != 0)
                {
                    X--;
                }
                else
                {
                    Console.WriteLine("Вы достигли края карты");
                }
            }
            if (key.Key == ConsoleKey.D)
            {
                if (X < gameSquare.GetUpperBound(0))
                {
                    X++;
                }
                else
                {
                    Console.WriteLine("Вы достигли края карты");
                }
            }
            if (key.Key == ConsoleKey.W)
            {
                if (Y != 0)
                {
                    Y--;
                }
                else
                {
                    Console.WriteLine("Вы достигли края карты");
                }
            }
            if (key.Key == ConsoleKey.S)
            {
                if (Y != gameSquare.GetUpperBound(1))
                {
                    Y++;
                }
                else
                {
                    Console.WriteLine("Вы достигли края карты");
                }
            }
            gameSquare[Y, X] = this;
            return gameSquare;
        }
        public void Attack(ConsoleKeyInfo key, Vampire vampire, Bat bat, Pit pit)
        {
            List<Creature> list = new List<Creature>();
            list.Add(bat);
            list.Add(vampire);
            list.Add(pit);
            foreach (Creature creature in list)
            {
                string creat;
                if (creature is Vampire)
                {
                    creat = "Вы убили вампира";
                }
                else if (creature is Bat)
                {
                    creat = "Вы убили летучую мышь";
                }
                else
                {
                    creat = "Вы закопали яму";
                }
                if (key.Key == ConsoleKey.LeftArrow)
                {
                    if (X - 1 == creature.X)
                    {
                        Console.WriteLine(creat);
                        
                    }
                   
                }
                else if (key.Key == ConsoleKey.RightArrow)
                {
                    if (X + 1 == creature.X)
                    {
                        Console.WriteLine(creat);
                        creature.IsAlive = false;
                    }
                    
                }
                else if (key.Key == ConsoleKey.UpArrow)
                {
                    if (Y + 1 == creature.Y)
                    {
                        Console.WriteLine(creat);
                        creature.IsAlive = false;
                    }
                    
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    if (Y - 1 == creature.Y)
                    {
                        Console.WriteLine(creat);
                        creature.IsAlive = false;
                    }
                   
                }               
            }           
        }
    }
}
