namespace oop_test_ibckfv;

public class Program
{
    static void Main()
    {
        var pefumesCollection = new List<Perfume>
        {
            new FloralPerfume("Flower", "Garden", PerfumeConcentration.EauDeParfum, SexOfParfum.ForFemale, AgeCategory.Elderly, "Rose"),
            new WoodyPerfume("Wood", "Forest", PerfumeConcentration.EauDeToilette, SexOfParfum.ForMale, AgeCategory.Young, "Oak")
        };
        var perfumeStore = new PerfumeStore(pefumesCollection);
        perfumeStore.IsMatch(AgeCategory.Elderly, SexOfParfum.ForFemale);
        perfumeStore.FindPerfume("Wood");
    }
}    

