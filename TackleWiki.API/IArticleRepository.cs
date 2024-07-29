using System.Net.Mail;
using TackleWiki.Business.ArticleLogic;

namespace TackleWiki.API;

public interface IArticleRepository
{
    // Create
    public Task CreateArticleAsync(string authorName, string title, string content);
    public Task AddCommentAsync(Guid articleId, string authorName, string comment);
    public Task AddAttachmentAsync(Guid articleId, Attachment attachment);
    public Task AddRatingAsync(Guid articleId, int rating);
    public Task AddTagAsync(Guid articleId, string tag);
    
    // Read
    public Task<Article> GetArticle(Guid articleId);
    public Task<List<Article>> GetArticles();
    public Task<List<Article>> GetArticlesByTag(string tag);
    public Task<List<Article>> GetArticlesByAuthor(string authorName);
    public Task<List<Article>> GetArticlesByRating(int rating);
    
    // Update
    public Task UpdateArticleAsync(Guid articleId, string title, string content);
    public Task UpdateCommentAsync(Guid articleId, string authorName, DateTime commentTime, string newContent);
    public Task UpdateAttachmentAsync(Guid articleId, string attachmentName, Attachment newAttachment);
    public Task UpdateRatingAsync(Guid articleId, int oldRating, int newRating);
    
    // Delete
    public Task DeleteArticleAsync(Guid articleId);
    public Task DeleteCommentAsync(Guid articleId, string authorName, DateTime commentTime);
    public Task DeleteAttachmentAsync(Guid articleId, string attachmentName);
    public Task DeleteRatingAsync(Guid articleId, int rating);
    public Task DeleteTagAsync(Guid articleId, string tag);
    
    
}