using System.Net.Mail;
using TackleWiki.Business.ArticleLogic;

namespace TackleWiki.API;

public class ArticleRepository : IArticleRepository
{
    private readonly Dictionary<Guid, Article> _articles = new();

    public Task CreateArticle(string authorName, string title, string content)
    {
        var article = new Article(new ArticleSettings.Builder()
            .SetAuthorName(authorName)
            .SetTitle(title)
            .SetContent(content)
            .SetCreatedAt(DateTime.Now)
            .Build());
        {
            _articles.Add(article.Id, article);
        }

        return Task.CompletedTask;
    }

    public Task AddComment(Guid articleId, string authorName, string content)
    {
        return new Task(() =>
        {
            var article = _articles[articleId];
            article?.AddComment(new Comment(authorName, content, DateTime.Now, DateTime.Now));
        });
    }

    public Task AddAttachment(Guid articleId, Attachment attachment)
    {
        return new Task(() =>
        {
            var article = _articles[articleId];
        });
    }

    public Task AddRating(Guid articleId, int rating)
    {
        return new Task(() =>
        {
            var article = _articles[articleId];
            article?.AddRate(rating);
        });
    }

    public Task AddTag(Guid articleId, string tag)
    {
        return new Task(() =>
        {
            var article = _articles[articleId];
            article?.AddTag(tag);
        });
    }

    public Task<Article> GetArticle(Guid articleId)
    {
        return new Task<Article>(() => _articles[articleId]);
    }

    public Task<List<Article>> GetArticles()
    {
        return new Task<List<Article>>(() => _articles
            .Values
            .ToList());
    }

    public Task<List<Article>> GetArticlesByTag(string tag)
    {
        return new Task<List<Article>>(() => _articles.Values
            .Where(a =>
                a.Tags
                    .Contains(tag))
            .ToList());
    }

    public Task<List<Article>> GetArticlesByAuthor(string authorName)
    {
        return new Task<List<Article>>(() =>
            _articles.Values
                .Where(a => a.AuthorName == authorName)
                .ToList());
    }

    public Task<List<Article>> GetArticlesByRating(int rating)
    {
        return new Task<List<Article>>(() =>
            _articles.Values
                .Where(a => a.Ratings
                    .Contains(rating))
                .ToList());
    }

    public Task UpdateArticle(Guid articleId, string title, string content)
    {
        return new Task(() =>
        {
            var article = _articles[articleId];
            article.UpdateContent(content);
        });
    }

    public Task UpdateComment(Guid articleId, string authorName, DateTime commentTime, string newContent)
    {
        return new Task(() =>
        {
            var article = _articles[articleId];
            article?.UpdateComment(authorName, commentTime, newContent);
        });
    }

    public Task UpdateAttachment(Guid articleId, string attachmentName, Attachment newAttachment)
    {
        return new Task(() =>
        {
            var article = _articles[articleId];
            article?.RemoveAttachment(attachmentName);
            article?.AddAttachment(newAttachment);
        });
    }

    public Task UpdateRating(Guid articleId, int oldRating, int newRating)
    {
        return new Task(() =>
        {
            var article = _articles[articleId];
            article?.Ratings.Remove(oldRating);
            article?.AddRate(newRating);
        });
    }

    public Task DeleteArticle(Guid articleId)
    {
        return new Task(() => _articles.Remove(articleId));
    }

    public Task DeleteComment(Guid articleId, string authorName, DateTime commentTime)
    {
        return new Task(() =>
        {
            var article = _articles[articleId];
            article?.Comments.Remove(article.Comments.Find(c =>
                c?.AuthorName == authorName && c.CreatedAt == commentTime));
        });
    }

    public Task DeleteAttachment(Guid articleId, string attachmentName)
    {
        return new Task(() =>
        {
            var article = _articles[articleId];
            article?.RemoveAttachment(attachmentName);
        });
    }

    public Task DeleteRating(Guid articleId, int rating)
    {
        return new Task(() =>
        {
            var article = _articles[articleId];
            article?.Ratings.Remove(rating);
        });
    }

    public Task DeleteTag(Guid articleId, string tag)
    {
        return new Task(() =>
        {
            var article = _articles[articleId];
            article?.Tags.Remove(tag);
        });
    }
}