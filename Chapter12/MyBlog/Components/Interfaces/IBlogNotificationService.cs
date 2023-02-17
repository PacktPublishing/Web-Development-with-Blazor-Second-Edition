using Data.Models;
namespace Components.Interfaces;

public interface IBlogNotificationService
{
    event Action<BlogPost>? BlogPostChanged;
    Task SendNotification(BlogPost post);
}
