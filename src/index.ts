async function fetchArticle() {
    try {
        const response = await fetch('http://localhost:5090/api/v1/article');
        if (!response.ok) {
            throw new Error('Network response was not ok');
        }
        const article: Article = await response.json();
        displayArticle(article);
    } catch (error) {
        console.error('There was a problem with the fetch operation:', error);
    }
}

function displayArticle(article: Article) {
    const articleList = document.querySelector('.article-category-articles');
    if (articleList) {
        const listItem = document.createElement('li');
        listItem.classList.add('article-category-articles__list-item');
        listItem.innerHTML = `
                <li class="article-category-articles__list-item">
                    <article class="article">
                    <h3 class="article__header">${article.title}</h3>
                    <p class="article__description">${article.content}</p>
                    <a class="article__link" href="#">Read more</a>
                </article>
                </li>
            `;
        articleList.appendChild(listItem);
    }
}

document.addEventListener('DOMContentLoaded', (event) => {
    event.preventDefault();
    for (let i = 0; i < 5; i++) {
        fetchArticle();
    }
});

type Article = {
    id: string;
    authorName: string;
    title: string;
    content: string;
    createdAt: string;
    updatedAt: string;
    comments: [];
    tags: [];
    ratings: [];
}