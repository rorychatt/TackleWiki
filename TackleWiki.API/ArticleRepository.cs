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
        await Task.Run(() =>
        {
            var article = _articles[articleId];
            article?.AddComment(new Comment(authorName, content, DateTime.Now, DateTime.Now));
        });
    }

    public async Task AddAttachmentAsync(Guid articleId, Attachment attachment)
    {
        await Task.Run(() =>
        {
            var article = _articles[articleId];
        });
    }

    public async Task AddRatingAsync(Guid articleId, int rating)
    {
        await Task.Run(() =>
        {
            var article = _articles[articleId];
            article?.AddRate(rating);
        });
    }

    public async Task AddTagAsync(Guid articleId, string tag)
    {
        await Task.Run(() =>
        {
            var article = _articles[articleId];
            article?.AddTag(tag);
        });
    }

    public async Task<Article> GetArticleAsync(Guid articleId)
    {
        var result = await Task.FromResult(_articles[articleId]);
        return result;
    }

    public async Task<List<Article>> GetArticlesAsync(int amount)
    {
        var results = await Task.Run(() => _articles.Values
            .Take(amount)
            .ToList());
        return results;
    }
    
    public async Task<Article> GetRandomArticleAsync()
    {
        var result = await Task.Run(() => _articles.Values
            .ElementAt(Random.Next(0, _articles.Count)));
        return result;
    }

    public async Task<List<Article>> GetArticlesByTagAsync(string tag)
    {
        var results = await Task.Run(() => _articles.Values
            .Where(a =>
                a.Tags
                    .Contains(tag))
            .ToList());
        return results;
    }

    public async Task<List<Article>> GetArticlesByAuthorAsync(string authorName)
    {
        var results = await Task.Run(() =>
            _articles.Values
                .Where(a => a.AuthorName == authorName)
                .ToList());
        return results;
    }

    public async Task<List<Article>> GetArticlesByRatingAsync(int rating)
    {
        var results = await Task.Run(() =>
            _articles.Values
                .Where(a => a.Ratings
                    .Contains(rating))
                .ToList());
        return results;
    }

    public async Task UpdateArticleAsync(Guid articleId, string title, string content)
    {
        await Task.Run(() =>
        {
            var article = _articles[articleId];
            article.UpdateContent(content);
        });
    }

    public async Task UpdateCommentAsync(Guid articleId, string authorName, DateTime commentTime, string newContent)
    {
        await Task.Run(() =>
        {
            var article = _articles[articleId];
            article?.UpdateComment(authorName, commentTime, newContent);
        });
    }

    public async Task UpdateAttachmentAsync(Guid articleId, string attachmentName, Attachment newAttachment)
    {
        await Task.Run(() =>
        {
            var article = _articles[articleId];
            article?.RemoveAttachment(attachmentName);
            article?.AddAttachment(newAttachment);
        });
    }

    public async Task UpdateRatingAsync(Guid articleId, int oldRating, int newRating)
    {
        await Task.Run(() =>
        {
            var article = _articles[articleId];
            article?.Ratings.Remove(oldRating);
            article?.AddRate(newRating);
        });
    }

    public async Task DeleteArticleAsync(Guid articleId)
    {
        await Task.Run(() => _articles.Remove(articleId));
    }

    public async Task DeleteCommentAsync(Guid articleId, string authorName, DateTime commentTime)
    {
        await Task.Run(() =>
        {
            var article = _articles[articleId];
            article?.Comments.Remove(article.Comments.Find(c =>
                c?.AuthorName == authorName && c.CreatedAt == commentTime));
        });
    }

    public async Task DeleteAttachmentAsync(Guid articleId, string attachmentName)
    {
        await Task.Run(() =>
        {
            var article = _articles[articleId];
            article?.RemoveAttachment(attachmentName);
        });
    }

    public async Task DeleteRatingAsync(Guid articleId, int rating)
    {
        await Task.Run(() =>
        {
            var article = _articles[articleId];
            article?.Ratings.Remove(rating);
        });
    }

    public async Task DeleteTagAsync(Guid articleId, string tag)
    {
        await Task.Run(() =>
        {
            var article = _articles[articleId];
            article?.Tags.Remove(tag);
        });
    }

    private static readonly Random Random = new();

    private static string GenerateRandomContent(int wordCount)
    {
        var words = new List<string>();
        for (var i = 0; i < wordCount; i++)
        {
            words.Add(Guid.NewGuid().ToString().Substring(0, 5));
        }

        return string.Join(" ", words);
    }

    public async Task RegisterFakeArticlesAsync(int amount)
    {
        await Task.Run(() =>
        {
            var articleSettings = new ArticleSettings();
            for (var i = 0; i < amount; i++)
            {
                var article = new Article(new ArticleSettings.Builder()
                    .SetAuthorName("Author" + i)
                    .SetTitle("Title" + i)
                    .SetContent(GenerateRandomContent(40))
                    .SetCreatedAt(DateTime.Now)
                    .Build());
                _articles.Add(article.Id, article);
            }
        });
    }
}