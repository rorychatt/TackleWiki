using TackleWiki.API;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IArticleRepository, ArticleRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost8080",
        corsPolicyBuilder => corsPolicyBuilder.WithOrigins("http://localhost:8080")
            .AllowAnyMethod()
            .AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowLocalhost8080");

var articleRepository = app.Services.GetRequiredService<IArticleRepository>();
await articleRepository.RegisterFakeArticlesAsync();

app.MapGet("/api/v1/articles", async () =>
{
    var articles = await articleRepository.GetArticlesAsync();
    return Results.Ok(articles);
});

app.MapGet("/api/v1/article", async () =>
{
    var article = await articleRepository.GetRandomArticleAsync();
    return Results.Ok(article);
});

app.MapGet("/api/v1/articles/{id}", async (string id) =>
{
    var article = await articleRepository.GetArticleAsync(Guid.Parse(id));
    return Results.Ok(article);
});

app.Run($"http://localhost:5090");