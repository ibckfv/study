namespace HelloWorld;

class Program
{
    static void Main(string[] args)
    {
        string name = Console.ReadLine();
        int age = int.Parse(Console.ReadLine());
        int course = 2024 - 2023;
        string groupNumber = Console.ReadLine();

        Console.WriteLine("Анкета:");
        Console.WriteLine($"Вас зовут {name}, вам {age} лет, вы на {course} курсе");
    }
}
