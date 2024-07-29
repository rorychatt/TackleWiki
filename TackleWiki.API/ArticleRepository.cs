using System.Net.Mail;
using TackleWiki.Business.ArticleLogic;

namespace TackleWiki.API;

public class ArticleRepository : IArticleRepository
{
    public Task CreateArticle(string authorName, string title, string content)
    {
        throw new NotImplementedException();
    }

    public Task AddComment(Guid articleId, string authorName, string comment)
    {
        throw new NotImplementedException();
    }

    public Task AddAttachment(Guid articleId, Attachment attachment)
    {
        throw new NotImplementedException();
    }

    public Task AddRating(Guid articleId, int rating)
    {
        throw new NotImplementedException();
    }

    public Task AddTag(Guid articleId, string tag)
    {
        throw new NotImplementedException();
    }

    public Task<Article> GetArticle(Guid articleId)
    {
        throw new NotImplementedException();
    }

    public Task<List<Article>> GetArticles()
    {
        throw new NotImplementedException();
    }

    public Task<List<Article>> GetArticlesByTag(string tag)
    {
        throw new NotImplementedException();
    }

    public Task<List<Article>> GetArticlesByAuthor(string authorName)
    {
        throw new NotImplementedException();
    }

    public Task<List<Article>> GetArticlesByRating(int rating)
    {
        throw new NotImplementedException();
    }

    public Task UpdateArticle(Guid articleId, string title, string content)
    {
        throw new NotImplementedException();
    }

    public Task UpdateComment(Guid articleId, string authorName, DateTime commentTime, string newContent)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAttachment(Guid articleId, string attachmentName, Attachment newAttachment)
    {
        throw new NotImplementedException();
    }

    public Task UpdateRating(Guid articleId, int oldRating, int newRating)
    {
        throw new NotImplementedException();
    }

    public Task DeleteArticle(Guid articleId)
    {
        throw new NotImplementedException();
    }

    public Task DeleteComment(Guid articleId, string authorName, DateTime commentTime)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAttachment(Guid articleId, string attachmentName)
    {
        throw new NotImplementedException();
    }

    public Task DeleteRating(Guid articleId, int rating)
    {
        throw new NotImplementedException();
    }

    public Task DeleteTag(Guid articleId, string tag)
    {
        throw new NotImplementedException();
    }
}