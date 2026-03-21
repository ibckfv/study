namespace chain_of_responsibility_ibckfv;

public class Package
{
    public string TypeOfPackage { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }

    public Package(string typeOfPackage, string name, double price)
    {
        TypeOfPackage = typeOfPackage;
        Name = name;
        Price = price;
    }
}