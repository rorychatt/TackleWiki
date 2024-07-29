using System.Net.Mail;

namespace TackleWiki.Business.ArticleLogic;

public class Article(ArticleSettings articleSettings) : IArticle
{
    public string? AuthorName { get; } = articleSettings.AuthorName;
    public string? Title { get; set; } = articleSettings.Title;
    public string? Content { get; set; } = articleSettings.Content;
    public DateTime CreatedAt { get; } = articleSettings.CreatedAt;
    public DateTime UpdatedAt { get; } = articleSettings.UpdatedAt;
    private List<Comment> Comments { get; } = articleSettings.Comments;
    private List<string> Tags { get; } = articleSettings.Tags;
    private List<Attachment> Attachments { get; } = articleSettings.Attachments;
    private List<int> Ratings { get; } = articleSettings.Ratings;

    public Task AddComment(Comment comment)
    {
        return new Task(() => Comments?.Add(comment));
    }

    public Task UpdateComment(string authorName, DateTime commentTime, string newContent)
    {
        return new Task(() =>
        {
            var comment = Comments.Find(c => c.AuthorName == authorName && c.CreatedAt == commentTime);
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

    public Task AddTag(string newTagName)
    {
        return new Task(() =>
        {
            if (!Tags.Contains(newTagName))
            {
                Tags.Add(newTagName);
            }
        });
    }

    public Task RemoveTag(string tagName)
    {
        return new Task(() => Tags.Remove(tagName));
    }

    public Task AddAttachment(Attachment attachment)
    {
        return new Task(() => Attachments.Add(attachment));
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
}