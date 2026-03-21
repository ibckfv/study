namespace oop_test_ibckfv;

public class WoodyPerfume : Perfume
{
    public string WoodType { get; }

    public WoodyPerfume(string name, string brand, PerfumeConcentration concentration, SexOfParfum sexOfParfum,
        AgeCategory ageCategory, string woodType) : base(name, brand, concentration, sexOfParfum, ageCategory)
    {
        WoodType = woodType;
    }

    public override void DescribeScent()
    {
        Console.WriteLine($"Аромат {Name} имеет ноты {WoodType}");
    }

    public override string GetFullDescription()
    {
        return $"Древесный аромат бренда {Brand} под названием {Name} с концентрацией {Concentration}" +
               $"и преобладащими нотами {WoodType}, предназначенный для {Sex} в возрасте {AgeCategory}";
    }
}