using System.Net.Mail;
using TackleWiki.Business.ArticleLogic;

namespace TackleWiki.API;

public interface IArticleRepository
{
    // Create
    public Task CreateArticle(string authorName, string title, string content);
    public Task AddComment(Guid articleId, string authorName, string comment);
    public Task AddAttachment(Guid articleId, Attachment attachment);
    public Task AddRating(Guid articleId, int rating);
    public Task AddTag(Guid articleId, string tag);
    
    // Read
    public Task<Article> GetArticle(Guid articleId);
    public Task<List<Article>> GetArticles();
    public Task<List<Article>> GetArticlesByTag(string tag);
    public Task<List<Article>> GetArticlesByAuthor(string authorName);
    public Task<List<Article>> GetArticlesByRating(int rating);
    
    // Update
    public Task UpdateArticle(Guid articleId, string title, string content);
    public Task UpdateComment(Guid articleId, string authorName, DateTime commentTime, string newContent);
    public Task UpdateAttachment(Guid articleId, string attachmentName, Attachment newAttachment);
    public Task UpdateRating(Guid articleId, int oldRating, int newRating);
    
    // Delete
    public Task DeleteArticle(Guid articleId);
    public Task DeleteComment(Guid articleId, string authorName, DateTime commentTime);
    public Task DeleteAttachment(Guid articleId, string attachmentName);
    public Task DeleteRating(Guid articleId, int rating);
    public Task DeleteTag(Guid articleId, string tag);
    
    
}