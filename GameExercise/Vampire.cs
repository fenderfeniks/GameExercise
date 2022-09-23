using System;

namespace GameExercise
{
    class Vampire : Creature
    {      
        public Vampire(int x, int y) : base(x, y) { }

        Random random = new Random();      

        public Creature[,] Action(Creature[,] gameSquare)
        {
            int num = random.Next(0, 100);

            /*if (num <= 20)
            {
                Console.WriteLine("Вампир выбросил число " + num + " и решил атаковать");
                Attack(player,vampire);
            } */
            if (num > 20 && num < 60)
            {
                Console.WriteLine("Вампир выбросил число " + num + " и решил переместиться");
                gameSquare = Move(gameSquare);
            }
            else
            {
                Console.WriteLine("Вампир выбросил число " + num + " и решил остаться на месте");
            }
            return gameSquare;
        }
        /*public void Attack(Player player)
        {
            if (++X == player.X && ++Y == player.Y || ++X == player.X && --Y == player.Y)
            {
                Console.WriteLine("Вампир атаковал и убил вас");
                Player.isPlayerAlive = false;
            }
            else if (--X == player.X && ++Y == player.Y || --X == player.X && --Y == player.Y)
            {
                Console.WriteLine("Вампир атаковал и убил вас");
                Player.isPlayerAlive = false;
            }
            else if (X == player.X && ++Y == player.Y || X == player.X && --Y == player.Y)
            {
                Console.WriteLine("Вампир атаковал и убил вас");
                Player.isPlayerAlive = false;
            }           
            else
            {
                Console.WriteLine("Вампир атаковал и промахнулся");
            }

        }*/
        public Creature[,] Move(Creature[,] gameSquare)
        {
            int randomX = random.Next(0, 1);
            int randomY = random.Next(0, 1);
            //Если равен 0, то вампри не ходит по Х
            if (randomX != 0)
            {
                if (randomX == 0)
                {
                    if (X != 0)
                    {
                        X--;
                    }
                }
                else
                {
                    if (X != gameSquare.GetLength(1) - 1)
                    {
                        X++;
                    }

                }
            }
            //Если равен 0, то вампри не ходит по Y  
            if (randomY != 0)
            {
                if (randomY == 0)
                {
                    if (Y != 0)
                    {
                        Y--;
                    }
                }
                else
                {
                    if (Y != gameSquare.GetLength(0) - 1)
                    {
                        Y++;
                    }

                }
            }
            
            gameSquare[Y, X] = this;
            return gameSquare;
        }
    }
}
