using ThriveActiveWellnessAPI.Domain.Shared;

namespace ThriveActiveWellness.Modules.Exercises.Domain.Exercises;

public class Media
{
    public string Url { get; private set; }
    public string Description { get; private set; }
    public MediaType Type { get; private set; }
    
    
    public static Media Create(string url, string description, MediaType type)
    {
        return new Media(url, description, type);
    }
    
    public static Media Create(Uri url, string description, MediaType type)
    {
        return new Media(url.AbsoluteUri, description, type);
    }
    
    private Media(string url, string description, MediaType type)
    {
        Url = url ?? throw new ArgumentNullException(nameof(url));
        Description = description ?? throw new ArgumentNullException(nameof(description));
        Type = type;
    }
    
    public void Update(string url, string description, MediaType type)
    {
        Url = url ?? throw new ArgumentNullException(nameof(url));
        Description = description ?? throw new ArgumentNullException(nameof(description));
        Type = type;
    }
    
    public void UpdateUrl(string url)
    {
        Url = url ?? throw new ArgumentNullException(nameof(url));
    }

}
