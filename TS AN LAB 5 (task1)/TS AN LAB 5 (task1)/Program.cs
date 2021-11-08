using System;
using System.Drawing;



namespace TS_AN_LAB_5__task1_
{

    public class Square
    {
        protected PointF[] squareVertexes;

        public void ReadSquareParamsFromConsole()
        {
            Console.Write("X: ");
            float x = float.Parse(Console.ReadLine());

            Console.Write("Y: ");
            float y = float.Parse(Console.ReadLine());

            Console.Write("Сторона: ");
            float side = float.Parse(Console.ReadLine());

            squareVertexes = new PointF[]
            {
                new PointF(x,        y       ),
                new PointF(x + side, y       ),
                new PointF(x + side, y - side),
                new PointF(x,        y - side)
            };
        }

        public virtual void LeftArrowPress()
        {
            ThrowIfNotInitialized();
            Move(new SizeF(-1f, 0f));
        }

        public virtual void RightArrowPress()
        {
            ThrowIfNotInitialized();
            Move(new SizeF(1f, 0f));
        }

        public void PrintSquareParamsToConsole()
        {
            ThrowIfNotInitialized();

            Console.WriteLine("Координати вершин квадрата:");

            int i;
            for (i = 0; i < squareVertexes.Length - 1; i++)
                Console.Write(
                    $"({squareVertexes[i].X};" +
                    $"{squareVertexes[i].Y}), "
                );

            Console.WriteLine(squareVertexes[i]);
        }

        protected void Move(SizeF offset)
        {
            for (int i = 0; i < squareVertexes.Length; i++)
                squareVertexes[i] += offset;
        }

        protected void ThrowIfNotInitialized()
        {
            if (squareVertexes is null)
                throw new Exception($"{this.GetType().Name} не ініціалізований");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                Console.WriteLine("Переміщувати квадрат вправо, вліво або обертати?");
                Console.WriteLine("1 - переміщувати");
                Console.WriteLine("2 - обертати");
                Console.Write("Ваш вибір: ");
                int choice = int.Parse(Console.ReadLine());

                Square square = null;

                if (choice == 1)
                    square = new Square();
                else if (choice == 2)
                    square = new RotationSquare();
                else
                    throw new Exception("Невідоме введення!");

                square.ReadSquareParamsFromConsole();

                Console.Clear();
                PrintKeys();
                Console.WriteLine();
                square.PrintSquareParamsToConsole();

                while (true)
                {
                    var keyInfo = Console.ReadKey();

                    bool exit = false;

                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.LeftArrow:
                            square.LeftArrowPress();
                            break;
                        case ConsoleKey.RightArrow:
                            square.RightArrowPress();
                            break;
                        case ConsoleKey.Escape:
                            exit = true;
                            break;
                    }

                    if (exit)
                        return;

                    Console.Clear();
                    PrintKeys();
                    Console.WriteLine();
                    square.PrintSquareParamsToConsole();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
                Console.ReadLine();
            }
        }

        private static void PrintKeys()
        {
            Console.WriteLine("Стрілка вліво - обернути або перемістити вліво");
            Console.WriteLine("Стрілка вправо - обернути або перемістити вправо");
            Console.WriteLine("Escape - вихід");
        }
    }


    public class RotationSquare
        : Square
    {
      
        protected const double RotationAngle = 10d;
                
        public override void LeftArrowPress()
        {
            ThrowIfNotInitialized();
            Rotate(RotationAngle);
        }

        public override void RightArrowPress()
        {
            ThrowIfNotInitialized();
            Rotate(-RotationAngle);
        }

        protected void Rotate(double degrees)
        {
            double radians = degrees / 180d * Math.PI;

            for (int i = 0; i < squareVertexes.Length; i++)
            {
                double x = squareVertexes[i].X;
                double y = squareVertexes[i].Y;

                double x1 = x * Math.Cos(radians) - y * Math.Sin(radians);
                double y1 = x * Math.Sin(radians) + y * Math.Cos(radians);

                squareVertexes[i].X = Convert.ToSingle(x1);
                squareVertexes[i].Y = Convert.ToSingle(y1);
            }
        }
    }


}
