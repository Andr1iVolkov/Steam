import React from 'react';
import { NewsItem } from '../../interfaces/news';

interface NewsItemProps {
    news: NewsItem;
}

const NewsItemComponent: React.FC<NewsItemProps> = ({ news }) => {
    return (
        <div className="div-news-item">
            <h2>{news.title}</h2>
            <p>{news.description}</p>
            <img src={news.image} alt={news.title} />
        </div>
    );
};

export default NewsItemComponent;
