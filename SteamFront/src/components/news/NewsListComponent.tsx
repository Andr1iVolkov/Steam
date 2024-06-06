import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { NewsItem } from '../../interfaces/news';
import NewsItemComponent from './NewsItemComponent';

const NewsListComponent: React.FC = () => {
    const [news, setNews] = useState<NewsItem[]>([]);
    const [isLoading, setIsLoading] = useState<boolean>(true);

    useEffect(() => {
        const fetchNews = async () => {
            try {
                const response = await axios.get('http://localhost:5002/api/News');
                if (response.status === 200) {
                    console.log('Загруженные данные:', response.data);
                    setNews(response.data);
                } else {
                    console.error('Ошибка при загрузке новостей:', response.status);
                }
            } catch (error) {
                console.error('Ошибка при загрузке новостей:', error);
            } finally {
                setIsLoading(false);
            }
        };

        fetchNews();
    }, []);

    if (isLoading) {
        return <div>Loading...</div>;
    }

    if (news.length === 0) {
        return <div>No data available</div>;
    }

    return (
        <div className="div-list-news">
            {news.map((item, index) => (
                <NewsItemComponent key={index} news={item} />
            ))}
        </div>
    );
};

export default NewsListComponent;
