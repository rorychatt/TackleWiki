using System.Net.Mail;

namespace TackleWiki.Business.ArticleLogic;

public class Article(ArticleSettings articleSettings) : IArticle
{
    public string AuthorName { get; } = articleSettings.AuthorName;
    public string Title { get; set; } = articleSettings.Title;
    public string Content { get; set; } = articleSettings.Content;
    public DateTime CreatedAt { get; } = articleSettings.CreatedAt;
    public DateTime UpdatedAt { get; } = articleSettings.UpdatedAt;
    public List<Comment> Comments { get; } = articleSettings.Comments;
    public List<string> Tags { get; } = articleSettings.Tags;
    public List<Attachment> Attachments { get; } = articleSettings.Attachments;
    public List<int> Ratings { get; } = articleSettings.Ratings;
}