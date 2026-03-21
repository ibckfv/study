namespace oop_test_ibckfv;

public abstract class Perfume
{
    public string Name { get; }
    public string Brand { get; }
    public PerfumeConcentration Concentration { get; protected set; }
    public SexOfParfum Sex { get; protected set; }
    public AgeCategory AgeCategory { get; protected set; }

    public Perfume(string name, string brand, PerfumeConcentration concentraion, SexOfParfum sexOfParfum,
        AgeCategory ageCategory)
    {
        Name = name;
        Brand = brand;
        Concentration = concentraion;
        Sex = sexOfParfum;
        AgeCategory = ageCategory;
    }

    public virtual string GetFullDescription()
    {
        throw new Exception("Описание не известно");
    }

    public abstract void DescribeScent();
}