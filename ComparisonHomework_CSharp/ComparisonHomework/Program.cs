using System;

namespace ComparisonHomework
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            ComparedObject first = new ComparedObject(10, "Привет");
            ComparedObject second = new ComparedObject(15, "Дом");
            ComparedObject third = new ComparedObject(10, "Привет");

            Console.WriteLine("Домашнее задание: перегрузка операторов сравнения");
            Console.WriteLine();

            Console.WriteLine("Объекты:");
            Console.WriteLine("first  = " + first);
            Console.WriteLine("second = " + second);
            Console.WriteLine("third  = " + third);
            Console.WriteLine();

            Console.WriteLine("Сравнение > и < выполняется по длине текстового поля:");
            Console.WriteLine("first > second  : " + (first > second));
            Console.WriteLine("first < second  : " + (first < second));
            Console.WriteLine();

            Console.WriteLine("Сравнение >= и <= выполняется по целочисленному полю:");
            Console.WriteLine("first >= second : " + (first >= second));
            Console.WriteLine("first <= second : " + (first <= second));
            Console.WriteLine();

            Console.WriteLine("Сравнение == и != выполняется по целочисленному и текстовому полям:");
            Console.WriteLine("first == second : " + (first == second));
            Console.WriteLine("first != second : " + (first != second));
            Console.WriteLine("first == third  : " + (first == third));
            Console.WriteLine("first.Equals(third): " + first.Equals(third));
            Console.WriteLine();

            Console.WriteLine("HashCode first: " + first.GetHashCode());
            Console.WriteLine("HashCode third: " + third.GetHashCode());
            Console.WriteLine();

            Console.WriteLine("Нажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }

    public class ComparedObject
    {
        public int Number { get; set; }
        public string Text { get; set; }

        public ComparedObject(int number, string text)
        {
            Number = number;
            Text = text ?? string.Empty;
        }

        // Больше / меньше: сравнение по длине текстовых значений.
        public static bool operator >(ComparedObject left, ComparedObject right)
        {
            CheckNullArguments(left, right);
            return left.Text.Length > right.Text.Length;
        }

        public static bool operator <(ComparedObject left, ComparedObject right)
        {
            CheckNullArguments(left, right);
            return left.Text.Length < right.Text.Length;
        }

        // Больше или равно / меньше или равно: сравнение по целочисленным полям.
        public static bool operator >=(ComparedObject left, ComparedObject right)
        {
            CheckNullArguments(left, right);
            return left.Number >= right.Number;
        }

        public static bool operator <=(ComparedObject left, ComparedObject right)
        {
            CheckNullArguments(left, right);
            return left.Number <= right.Number;
        }

        // Равно / не равно: сравнение и целочисленного, и текстового поля.
        public static bool operator ==(ComparedObject left, ComparedObject right)
        {
            if (ReferenceEquals(left, right))
            {
                return true;
            }

            if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
            {
                return false;
            }

            return left.Number == right.Number && left.Text == right.Text;
        }

        public static bool operator !=(ComparedObject left, ComparedObject right)
        {
            return !(left == right);
        }

        // Переопределение Equals() должно соответствовать логике оператора ==.
        public override bool Equals(object obj)
        {
            ComparedObject other = obj as ComparedObject;
            if (ReferenceEquals(other, null))
            {
                return false;
            }

            return this == other;
        }

        // Если переопределяется Equals(), нужно переопределить и GetHashCode().
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + Number.GetHashCode();
                hash = hash * 23 + (Text == null ? 0 : Text.GetHashCode());
                return hash;
            }
        }

        public override string ToString()
        {
            return string.Format("Number = {0}, Text = \"{1}\"", Number, Text);
        }

        private static void CheckNullArguments(ComparedObject left, ComparedObject right)
        {
            if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
            {
                throw new ArgumentNullException("Нельзя сравнивать null-объекты операторами >, <, >=, <=.");
            }
        }
    }
}
