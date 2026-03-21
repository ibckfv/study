namespace httplistener_test_ibckfv.models;

public class Booking
{
    public int Id {get;set;}
    public int ApartmentId {get;set;}
    public int UserId {get;set;}
    public DateTime DateStart {get;set;}
    public DateTime DateEnd {get;set;}
    public string Status {get;set;}
}