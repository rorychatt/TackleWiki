using System.Net.Mail;
using TackleWiki.Business.ArticleLogic;

namespace TackleWiki.API;

public class ArticleRepository : IArticleRepository
{
    private readonly Dictionary<Guid, Article> _articles = new();

    public async Task CreateArticleAsync(string authorName, string title, string content)
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

        await Task.CompletedTask;
    }

    public async Task AddCommentAsync(Guid articleId, string authorName, string content)
    {
        await new Task(() =>
        {
            var article = _articles[articleId];
            article?.AddComment(new Comment(authorName, content, DateTime.Now, DateTime.Now));
        });
    }

    public async Task AddAttachmentAsync(Guid articleId, Attachment attachment)
    {
        await new Task(() =>
        {
            var article = _articles[articleId];
        });
    }

    public async Task AddRatingAsync(Guid articleId, int rating)
    {
        await new Task(() =>
        {
            var article = _articles[articleId];
            article?.AddRate(rating);
        });
    }

    public async Task AddTagAsync(Guid articleId, string tag)
    {
        await new Task(() =>
        {
            var article = _articles[articleId];
            article?.AddTag(tag);
        });
    }

    public Task<Article> GetArticle(Guid articleId)
    {
        return Task.FromResult(_articles[articleId]);
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

    public async Task UpdateArticleAsync(Guid articleId, string title, string content)
    {
        await new Task(() =>
        {
            var article = _articles[articleId];
            article.UpdateContent(content);
        });
    }

    public async Task UpdateCommentAsync(Guid articleId, string authorName, DateTime commentTime, string newContent)
    {
        await new Task(() =>
        {
            var article = _articles[articleId];
            article?.UpdateComment(authorName, commentTime, newContent);
        });
    }

    public async Task UpdateAttachmentAsync(Guid articleId, string attachmentName, Attachment newAttachment)
    {
        await new Task(() =>
        {
            var article = _articles[articleId];
            article?.RemoveAttachment(attachmentName);
            article?.AddAttachment(newAttachment);
        });
    }

    public async Task UpdateRatingAsync(Guid articleId, int oldRating, int newRating)
    {
        await new Task(() =>
        {
            var article = _articles[articleId];
            article?.Ratings.Remove(oldRating);
            article?.AddRate(newRating);
        });
    }

    public async Task DeleteArticleAsync(Guid articleId)
    {
        await new Task(() => _articles.Remove(articleId));
    }

    public async Task DeleteCommentAsync(Guid articleId, string authorName, DateTime commentTime)
    {
        await new Task(() =>
        {
            var article = _articles[articleId];
            article?.Comments.Remove(article.Comments.Find(c =>
                c?.AuthorName == authorName && c.CreatedAt == commentTime));
        });
    }

    public async Task DeleteAttachmentAsync(Guid articleId, string attachmentName)
    {
        await new Task(() =>
        {
            var article = _articles[articleId];
            article?.RemoveAttachment(attachmentName);
        });
    }

    public async Task DeleteRatingAsync(Guid articleId, int rating)
    {
        await new Task(() =>
        {
            var article = _articles[articleId];
            article?.Ratings.Remove(rating);
        });
    }

    public async Task DeleteTagAsync(Guid articleId, string tag)
    {
        await new Task(() =>
        {
            var article = _articles[articleId];
            article?.Tags.Remove(tag);
        });
    }
}