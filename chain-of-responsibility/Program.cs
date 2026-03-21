using chain_of_responsibility_ibckfv;

var package = new Package("Dangerous Cargo", "Coca-Cola", 120);
var application = new Application(package, "The Coca-Cola Company", "Pyaterochka");

var PartnersList = new List<string>
{
    "SuperStorage",
    "AbcStorage",
    "PyaterochkaStorage"
};

var transportPackage = new LogisticsHandler(PartnersList)
{
    Successor = new StorageHandler()
    {
        Successor = new CarrierHandler("Paris", "Kazan")
    }
};

transportPackage.Handle(application);