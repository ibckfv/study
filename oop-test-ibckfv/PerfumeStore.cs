namespace oop_test_ibckfv;

public class PerfumeStore : IDiscountable
{
    public List<Perfume> PerfumesCollection { get; }
    private int _countOfSelectedParfums;

    public PerfumeStore(List<Perfume> perfumesCollection)
    {
        PerfumesCollection = perfumesCollection;
    }    

    private void AddNewParfum(Perfume perfume)
    {
        PerfumesCollection.Add(perfume);
    }

    public void FindPerfume(string nameOrBrand)
    {
        foreach (var perfume in PerfumesCollection)
        {
            if (nameOrBrand == perfume.Brand || nameOrBrand == perfume.Name)
            {
                Console.WriteLine("Аромат в наличии");
                return;
            }    
        }

        throw new PerfumeNotFoundException();
    }

    public void IsMatch(AgeCategory categoryOfAge, SexOfParfum sexOfParfum)
    {
        foreach (var perfume in PerfumesCollection)
        {
            if (perfume.AgeCategory == categoryOfAge && perfume.Sex == sexOfParfum)
            {
                Console.WriteLine($"Один из подходящих ароматов - {perfume.Name}");
                _countOfSelectedParfums++;
            }    
        }
    }
    
    public void ApplyDiscount(decimal precentage)
    {
        throw new NotImplementedException();
    }
}   