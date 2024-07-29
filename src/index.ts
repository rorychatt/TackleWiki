async function fetchArticles() {
    try {
        const response = await fetch('http://localhost:5090/api/v1/article');
        if (!response.ok) {
            throw new Error('Network response was not ok');
        }
        const articles = await response.json();
        displayArticles(articles);
    } catch (error) {
        console.error('There was a problem with the fetch operation:', error);
    }
}

function displayArticles(articles: any[]) {
    const articleList = document.querySelector('.article-category-articles');
    if (articleList) {
        articles.forEach(article => {
            const listItem = document.createElement('li');
            listItem.classList.add('article-category-articles__list-item');
            listItem.innerHTML = `
                <article class="article">
                    <h3 class="article__header">${article.title}</h3>
                    <p class="article__description">${article.content}</p>
                    <a class="article__link" href="#">Read more</a>
                </article>
            `;
            articleList.appendChild(listItem);
        });
    }
}

document.addEventListener('DOMContentLoaded', fetchArticles);