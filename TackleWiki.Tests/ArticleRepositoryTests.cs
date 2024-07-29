using System.Net.Mail;
using TackleWiki.API;

namespace TackleWiki.Tests;
public class ArticleRepositoryTests
{
    private readonly ArticleRepository _repository;

    public ArticleRepositoryTests()
    {
        _repository = new ArticleRepository();
    }

    [Fact]
    public async Task CreateArticleAsync_ShouldCreateArticle()
    {
        var authorName = "Author";
        var title = "Title";
        var content = "Content";

        await _repository.CreateArticleAsync(authorName, title, content);

        var articles = await _repository.GetArticlesAsync(1);
        Assert.Single(articles);
        Assert.Equal(authorName, articles.First().AuthorName);
        Assert.Equal(title, articles.First().Title);
        Assert.Equal(content, articles.First().Content);
    }

    [Fact]
    public async Task AddCommentAsync_ShouldAddComment()
    {
        var authorName = "Author";
        var title = "Title";
        var content = "Content";

        await _repository.CreateArticleAsync(authorName, title, content);
        var article = (await _repository.GetArticlesAsync(1)).First();

        var commentAuthor = "CommentAuthor";
        var commentContent = "CommentContent";

        await _repository.AddCommentAsync(article.Id, commentAuthor, commentContent);

        var updatedArticle = await _repository.GetArticleAsync(article.Id);
        Assert.Single(updatedArticle.Comments);
        Assert.Equal(commentAuthor, updatedArticle.Comments.First()!.AuthorName);
        Assert.Equal(commentContent, updatedArticle.Comments.First()!.Content);
    }

    [Fact]
    public async Task AddAttachmentAsync_ShouldAddAttachment()
    {
        var authorName = "Author";
        var title = "Title";
        var content = "Content";

        await _repository.CreateArticleAsync(authorName, title, content);
        var article = (await _repository.GetArticlesAsync(1)).First();

        var attachment = new Attachment("dummy.txt");

        await _repository.AddAttachmentAsync(article.Id, attachment);

        var updatedArticle = await _repository.GetArticleAsync(article.Id);
        Assert.Single(updatedArticle.Attachments);
    }

    [Fact]
    public async Task AddRatingAsync_ShouldAddRating()
    {
        var authorName = "Author";
        var title = "Title";
        var content = "Content";

        await _repository.CreateArticleAsync(authorName, title, content);
        var article = (await _repository.GetArticlesAsync(1)).First();

        var rating = 5;

        await _repository.AddRatingAsync(article.Id, rating);

        var updatedArticle = await _repository.GetArticleAsync(article.Id);
        Assert.Contains(rating, updatedArticle.Ratings);
    }

    [Fact]
    public async Task AddTagAsync_ShouldAddTag()
    {
        var authorName = "Author";
        var title = "Title";
        var content = "Content";

        await _repository.CreateArticleAsync(authorName, title, content);
        var article = (await _repository.GetArticlesAsync(1)).First();

        var tag = "Tag";

        await _repository.AddTagAsync(article.Id, tag);

        var updatedArticle = await _repository.GetArticleAsync(article.Id);
        Assert.Contains(tag, updatedArticle.Tags);
    }

    [Fact]
    public async Task GetArticleAsync_ShouldReturnArticle()
    {
        var authorName = "Author";
        var title = "Title";
        var content = "Content";

        await _repository.CreateArticleAsync(authorName, title, content);
        var article = (await _repository.GetArticlesAsync(1)).First();

        var retrievedArticle = await _repository.GetArticleAsync(article.Id);

        Assert.Equal(article, retrievedArticle);
    }

    [Fact]
    public async Task GetArticlesAsync_ShouldReturnArticles()
    {
        var authorName = "Author";
        var title = "Title";
        var content = "Content";

        await _repository.CreateArticleAsync(authorName, title, content);

        var articles = await _repository.GetArticlesAsync(1);

        Assert.Single(articles);
    }

    [Fact]
    public async Task GetRandomArticleAsync_ShouldReturnArticle()
    {
        var authorName = "Author";
        var title = "Title";
        var content = "Content";

        await _repository.CreateArticleAsync(authorName, title, content);

        var article = await _repository.GetRandomArticleAsync();

        Assert.NotNull(article);
    }

    [Fact]
    public async Task GetArticlesByTagAsync_ShouldReturnArticles()
    {
        var authorName = "Author";
        var title = "Title";
        var content = "Content";
        var tag = "Tag";

        await _repository.CreateArticleAsync(authorName, title, content);
        var article = (await _repository.GetArticlesAsync(1)).First();
        await _repository.AddTagAsync(article.Id, tag);

        var articles = await _repository.GetArticlesByTagAsync(tag);

        Assert.Single(articles);
        Assert.Contains(tag, articles.First().Tags);
    }

    [Fact]
    public async Task GetArticlesByAuthorAsync_ShouldReturnArticles()
    {
        var authorName = "Author";
        var title = "Title";
        var content = "Content";

        await _repository.CreateArticleAsync(authorName, title, content);

        var articles = await _repository.GetArticlesByAuthorAsync(authorName);

        Assert.Single(articles);
        Assert.Equal(authorName, articles.First().AuthorName);
    }

    [Fact]
    public async Task GetArticlesByRatingAsync_ShouldReturnArticles()
    {
        var authorName = "Author";
        var title = "Title";
        var content = "Content";
        var rating = 5;

        await _repository.CreateArticleAsync(authorName, title, content);
        var article = (await _repository.GetArticlesAsync(1)).First();
        await _repository.AddRatingAsync(article.Id, rating);

        var articles = await _repository.GetArticlesByRatingAsync(rating);

        Assert.Single(articles);
        Assert.Contains(rating, articles.First().Ratings);
    }

    [Fact]
    public async Task UpdateArticleAsync_ShouldUpdateArticle()
    {
        var authorName = "Author";
        var title = "Title";
        var content = "Content";
        var newContent = "New Content";

        await _repository.CreateArticleAsync(authorName, title, content);
        var article = (await _repository.GetArticlesAsync(1)).First();

        await _repository.UpdateArticleAsync(article.Id, title, newContent);

        var updatedArticle = await _repository.GetArticleAsync(article.Id);

        Assert.Equal(newContent, updatedArticle.Content);
    }

    [Fact]
    public async Task UpdateCommentAsync_ShouldUpdateComment()
    {
        var authorName = "Author";
        var title = "Title";
        var content = "Content";
        var commentAuthor = "CommentAuthor";
        var commentContent = "CommentContent";
        var newCommentContent = "NewCommentContent";

        await _repository.CreateArticleAsync(authorName, title, content);
        var article = (await _repository.GetArticlesAsync(1)).First();
        await _repository.AddCommentAsync(article.Id, commentAuthor, commentContent);
        var comment = article.Comments.First();

        await _repository.UpdateCommentAsync(article.Id, commentAuthor, comment!.CreatedAt, newCommentContent);

        var updatedArticle = await _repository.GetArticleAsync(article.Id);
        var updatedComment = updatedArticle.Comments.First();

        Assert.Equal(newCommentContent, updatedComment!.Content);
    }

    [Fact]
    public async Task UpdateAttachmentAsync_ShouldUpdateAttachment()
    {
        var authorName = "Author";
        var title = "Title";
        var content = "Content";
        var attachment = new Attachment("dummy.txt");
        var newAttachment = new Attachment("newDummy.txt");

        await _repository.CreateArticleAsync(authorName, title, content);
        var article = (await _repository.GetArticlesAsync(1)).First();
        await _repository.AddAttachmentAsync(article.Id, attachment);

        await _repository.UpdateAttachmentAsync(article.Id, "dummy.txt", newAttachment);

        var updatedArticle = await _repository.GetArticleAsync(article.Id);
        Assert.Contains(newAttachment, updatedArticle.Attachments);
    }

    [Fact]
    public async Task UpdateRatingAsync_ShouldUpdateRating()
    {
        var authorName = "Author";
        var title = "Title";
        var content = "Content";
        var oldRating = 5;
        var newRating = 10;

        await _repository.CreateArticleAsync(authorName, title, content);
        var article = (await _repository.GetArticlesAsync(1)).First();
        await _repository.AddRatingAsync(article.Id, oldRating);

        await _repository.UpdateRatingAsync(article.Id, oldRating, newRating);

        var updatedArticle = await _repository.GetArticleAsync(article.Id);
        Assert.Contains(newRating, updatedArticle.Ratings);
        Assert.DoesNotContain(oldRating, updatedArticle.Ratings);
    }

    [Fact]
    public async Task DeleteArticleAsync_ShouldDeleteArticle()
    {
        var authorName = "Author";
        var title = "Title";
        var content = "Content";

        await _repository.CreateArticleAsync(authorName, title, content);
        var article = (await _repository.GetArticlesAsync(1)).First();

        await _repository.DeleteArticleAsync(article.Id);

        var articles = await _repository.GetArticlesAsync(1);
        Assert.Empty(articles);
    }

    [Fact]
    public async Task DeleteAttachmentAsync_ShouldDeleteAttachment()
    {
        var authorName = "Author";
        var title = "Title";
        var content = "Content";
        var attachment = new Attachment("dummy.txt");

        await _repository.CreateArticleAsync(authorName, title, content);
        var article = (await _repository.GetArticlesAsync(1)).First();
        await _repository.AddAttachmentAsync(article.Id, attachment);

        await _repository.DeleteAttachmentAsync(article.Id, "dummy.txt");

        var updatedArticle = await _repository.GetArticleAsync(article.Id);
        Assert.Empty(updatedArticle.Attachments);
    }

    [Fact]
    public async Task DeleteRatingAsync_ShouldDeleteRating()
    {
        var authorName = "Author";
        var title = "Title";
        var content = "Content";
        var rating = 5;

        await _repository.CreateArticleAsync(authorName, title, content);
        var article = (await _repository.GetArticlesAsync(1)).First();
        await _repository.AddRatingAsync(article.Id, rating);

        await _repository.DeleteRatingAsync(article.Id, rating);

        var updatedArticle = await _repository.GetArticleAsync(article.Id);
        Assert.DoesNotContain(rating, updatedArticle.Ratings);
    }

    [Fact]
    public async Task DeleteTagAsync_ShouldDeleteTag()
    {
        var authorName = "Author";
        var title = "Title";
        var content = "Content";
        var tag = "Tag";

        await _repository.CreateArticleAsync(authorName, title, content);
        var article = (await _repository.GetArticlesAsync(1)).First();
        await _repository.AddTagAsync(article.Id, tag);

        await _repository.DeleteTagAsync(article.Id, tag);

        var updatedArticle = await _repository.GetArticleAsync(article.Id);
        Assert.DoesNotContain(tag, updatedArticle.Tags);
    }

    [Fact]
    public async Task RegisterFakeArticlesAsync_ShouldCreateArticles()
    {
        var amount = 10;

        await _repository.RegisterFakeArticlesAsync(amount);

        var articles = await _repository.GetArticlesAsync(amount);
        Assert.Equal(amount, articles.Count);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(5)]
    [InlineData(10)]
    public async Task GetArticlesAsync_ShouldReturnRequestedAmount(int amount)
    {
        await _repository.RegisterFakeArticlesAsync(10);

        var articles = await _repository.GetArticlesAsync(amount);

        Assert.Equal(amount, articles.Count);
    }

    [Theory]
    [InlineData("Author1")]
    [InlineData("Author5")]
    public async Task GetArticlesByAuthorAsync_ShouldReturnCorrectArticles(string authorName)
    {
        await _repository.RegisterFakeArticlesAsync(10);

        var articles = await _repository.GetArticlesByAuthorAsync(authorName);

        Assert.All(articles, article => Assert.Equal(authorName, article.AuthorName));
    }

    [Theory]
    [InlineData("Title1")]
    [InlineData("Title5")]
    public async Task UpdateArticleAsync_ShouldUpdateTitle(string newTitle)
    {
        await _repository.RegisterFakeArticlesAsync(1);
        var article = (await _repository.GetArticlesAsync(1)).First();

        await _repository.UpdateArticleAsync(article.Id, newTitle, article.Content!);

        var updatedArticle = await _repository.GetArticleAsync(article.Id);

        Assert.Equal(newTitle, updatedArticle.Title);
    }

    [Fact]
    public async Task GetArticlesByTagAsync_ShouldReturnCorrectArticles()
    {
        var tag = "TestTag";
        await _repository.RegisterFakeArticlesAsync(1);
        var article = (await _repository.GetArticlesAsync(1)).First();
        await _repository.AddTagAsync(article.Id, tag);

        var articles = await _repository.GetArticlesByTagAsync(tag);

        Assert.Single(articles);
        Assert.Contains(tag, articles.First().Tags);
    }

    [Fact]
    public async Task GetArticlesByRatingAsync_ShouldReturnCorrectArticles()
    {
        var rating = 5;
        await _repository.RegisterFakeArticlesAsync(1);
        var article = (await _repository.GetArticlesAsync(1)).First();
        await _repository.AddRatingAsync(article.Id, rating);

        var articles = await _repository.GetArticlesByRatingAsync(rating);

        Assert.Single(articles);
        Assert.Contains(rating, articles.First().Ratings);
    }
}