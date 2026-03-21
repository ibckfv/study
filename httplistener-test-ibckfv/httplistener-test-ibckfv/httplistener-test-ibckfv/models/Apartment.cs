namespace httplistener_test_ibckfv.models;

public class Apartment
{
    public int Id {get;set;}
    public string Title {get;set;}
    public string Description {get;set;}
    public string PricePerDay {get;set;}
    public int OwnerId {get;set;}
    public int CityId {get;set;}
    public DateTime CreatedAt {get;set;}
}