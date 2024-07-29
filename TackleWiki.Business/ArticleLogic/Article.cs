using System.Net.Mail;

namespace TackleWiki.Business.ArticleLogic;

public class Article(ArticleSettings articleSettings) : IArticle
{
    public Guid Id { get; } = Guid.NewGuid();
    public string? AuthorName { get; } = articleSettings.AuthorName;
    public string? Title { get; set; } = articleSettings.Title;
    public string? Content { get; set; } = articleSettings.Content;
    public DateTime CreatedAt { get; } = articleSettings.CreatedAt;
    public DateTime UpdatedAt { get; private set; } = articleSettings.UpdatedAt;
    public List<Comment?> Comments { get; } = articleSettings.Comments;
    public List<string> Tags { get; } = articleSettings.Tags;
    public List<Attachment> Attachments { get; } = articleSettings.Attachments;
    public List<int> Ratings { get; } = articleSettings.Ratings;

    public async Task AddComment(Comment? comment)
    {
        await new Task(() => Comments.Add(comment));
    }

    public async Task UpdateComment(string authorName, DateTime commentTime, string newContent)
    {
        await new Task(() =>
        {
            var comment = Comments.Find(c => c?.AuthorName == authorName && c.CreatedAt == commentTime);
            if (comment != null)
            {
                comment.Content = newContent;
                comment.UpdatedAt = DateTime.Now;
            }
            else
            {
                throw new Exception("Could not find comment to update.");
            }
        });
    }

    public async Task AddTag(string newTagName)
    {
        await new Task(() =>
        {
            if (!Tags.Contains(newTagName))
            {
                Tags.Add(newTagName);
            }
        });
    }

    public async Task RemoveTag(string tagName)
    {
        await new Task(() => Tags.Remove(tagName));
    }

    public async Task AddAttachment(Attachment attachment)
    {
        await new Task(() => Attachments.Add(attachment));
    }

    public Task RemoveAttachment(string attachmentName)
    {
        return new Task(() =>
        {
            var attachment = Attachments.Find(a => a.Name == attachmentName);
            if (attachment != null)
            {
                Attachments?.Remove(attachment);
            }
            else
            {
                throw new Exception("Could not find attachment to remove.");
            }
        });
    }

    public Task AddRate(int newRate)
    {
        return new Task(() => Ratings?.Add(newRate));
    }

    public Task UpdateContent(string newContent)
    {
        return new Task(() =>
        {
            Content = newContent;
            UpdatedAt = DateTime.Now;
        });
    }
}