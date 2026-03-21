namespace oop_test_ibckfv;

public class FloralPerfume : Perfume
{
    public string DominantFlower { get; }

    public FloralPerfume(string name, string brand, PerfumeConcentration concentration, SexOfParfum sexOfParfum,
        AgeCategory ageCategory, string dominantFlower) : base(name, brand, concentration, sexOfParfum, ageCategory)
    {
        DominantFlower = dominantFlower;
    }

    public override void DescribeScent()
    {
        Console.WriteLine($"{Name} - цветочный аромат с преобладающими нотами {DominantFlower}");
    }

    public override string GetFullDescription()
    {
        return $"Цветочный аромат бренда {Brand} под названием {Name} с концентрацией {Concentration}" +
               $"и преобладащими нотами {DominantFlower}, предназначенный для {Sex} в возрасте {AgeCategory}";
    }
}