import React, { useEffect, useState } from 'react';
import { NewsItem } from '../../interfaces/news/index';
import axios from 'axios'

interface NewsItemProps {
    news: NewsItem;
}

const NewsItemComponent: React.FC<NewsItemProps> = ({ news }) => {
    
    const [loadedNews, setLoadedNews] = useState<NewsItem | null>(null);

    useEffect(() => {
        axios.get('http://localhost:5002/api/News')
            .then(response => {
                setLoadedNews(response.data);
            })
            .catch(error => {
                console.error('Error loading news:', error);
            });
    }, []);
   
    return (
        <div className="div-news-item">
            <h2>{loadedNews ? loadedNews.title : news.title}</h2>
            <p>{loadedNews ? loadedNews.description : news.description}</p>
            <img src={loadedNews ? loadedNews.image : news.image} alt={loadedNews ? loadedNews.title : news.title} />
            <p>Новина {loadedNews ? new Date(loadedNews.dateOfRelease).toLocaleDateString() : new Date(news.dateOfRelease).toLocaleDateString()}</p>
        </div>
    );
};

export default NewsItemComponent;