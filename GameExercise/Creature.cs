namespace GameExercise
{
    class Creature
    {
        private bool isAlive = true;
        public bool IsAlive { get => isAlive; set => isAlive = value; }
        public Creature(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        private int x;

        public int X { get => x; set => x = value; }

        private int y;

        public int Y { get => y; set => y = value; }
        
    }
}
