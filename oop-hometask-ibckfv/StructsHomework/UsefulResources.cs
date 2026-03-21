namespace StructsHomework;

public class UsefulResources
{
    private List<string> _resourcesList;

    public UsefulResources(List<string> resourcesList)
    {
        _resourcesList = resourcesList;
    }
    
    public void GetResourceUsage(int resourceNumber)
    {
        if (resourceNumber >= 0 && ((_resourcesList.Count) >= resourceNumber))
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

    public void AddResource(string resource)
    {
        _resourcesList.Add(resource);
    }
}    