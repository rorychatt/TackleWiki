using System.Net.Mail;

namespace TackleWiki.Business.ArticleLogic;

public class ArticleSettings
{
    public string? AuthorName { get; private set; }
    public string? Title { get; private set; }
    public string? Content { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public List<Comment?> Comments { get; private set; }
    public List<string> Tags { get; private set; }
    public List<Attachment> Attachments { get; private set; }
    public List<int> Ratings { get; private set; }

    private ArticleSettings(List<Comment?>? comments, List<string>? tags, List<Attachment>? attachments,
        List<int>? ratings)
    {
        Comments = comments ?? [];
        Tags = tags ?? [];
        Attachments = attachments ?? [];
        Ratings = ratings ?? [];
    }

    private ArticleSettings()
    {
        Comments = [];
        Tags = [];
        Attachments = [];
        Ratings = [];
    }

    public class Builder
    {
        private readonly ArticleSettings _articleSettings = new();

        public Builder SetAuthorName(string? authorName)
        {
            _articleSettings.AuthorName = authorName;
            return this;
        }

        public Builder SetTitle(string? title)
        {
            _articleSettings.Title = title;
            return this;
        }

        public Builder SetContent(string? content)
        {
            _articleSettings.Content = content;
            return this;
        }

        public Builder SetCreatedAt(DateTime createdAt)
        {
            _articleSettings.CreatedAt = createdAt;
            return this;
        }

        public Builder SetUpdatedAt(DateTime updatedAt)
        {
            _articleSettings.UpdatedAt = updatedAt;
            return this;
        }

        public Builder SetComments(List<Comment?> comments)
        {
            _articleSettings.Comments = comments;
            return this;
        }

        public Builder SetTags(List<string> tags)
        {
            _articleSettings.Tags = tags;
            return this;
        }

        public Builder SetAttachments(List<Attachment> attachments)
        {
            _articleSettings.Attachments = attachments;
            return this;
        }

        public Builder SetRatings(List<int> ratings)
        {
            _articleSettings.Ratings = ratings;
            return this;
        }

        public ArticleSettings Build()
        {
            return _articleSettings;
        }
    }
}