namespace TackleWiki.Business.ArticleLogic;

public class Comment(string authorName, string content, DateTime? createdAt, DateTime? updatedAt)
{
    public string AuthorName { get; set; } = authorName;

    public string Content { get; set; } = content;

    public DateTime CreatedAt { get; } = createdAt ?? DateTime.Now;

    public DateTime UpdatedAt { get; set; } = updatedAt ?? DateTime.Now;
    
}