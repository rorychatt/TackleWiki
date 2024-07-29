using System.Net.Mail;

namespace TackleWiki.Business.ArticleLogic;

public interface IArticle
{
    public Task AddComment(Comment? comment);
    public Task UpdateComment(string authorName, DateTime commentTime, string newContent);
    public Task AddTag(string newTagName);
    public Task RemoveTag(string tagName);
    public Task AddAttachment(Attachment attachment);
    public Task RemoveAttachment(string attachmentName);
    public Task AddRate(int newRate);
    public Task UpdateContent(string newContent);
}