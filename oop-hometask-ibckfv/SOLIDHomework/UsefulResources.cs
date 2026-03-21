namespace SOLIDHomework;

public class UsefulResources
{
    private string[] _resourcesList;

    public UsefulResources(string[] resourcesList)
    {
        _resourcesList = resourcesList;
    }
    
    public void GetResourceUsage(int resourceNumber)
    {
        if (resourceNumber >= 0 && ((_resourcesList.Length) >= resourceNumber))
        {
            Console.WriteLine($"Ресурс под названием {_resourcesList[resourceNumber - 1]} применяется для ...");
        }
        else 
        { 
            throw new Exception("Элемента с таким номером не существует"); 
        }
    }

    public void GetResourceUsage(string resourceName)
    {
        if (_resourcesList.Contains(resourceName))
        {
            Console.WriteLine($"Ресурс под названием {resourceName} применяется для ...");
        }
        else
        {
            throw new Exception("Такого элемента не существует");
        }
    }
}    