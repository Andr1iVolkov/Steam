import React from 'react';
import { newsData } from '../news/newsData';
import NewsItemComponent from './NewsItemComponent';

const NewsListComponent: React.FC = () => {
    return (
        <div className="div-list-news news-list">
            {newsData.map(news => (
                <NewsItemComponent key={news.id} news={news} />
            ))}
        </div>
    );
};

export default NewsListComponent;