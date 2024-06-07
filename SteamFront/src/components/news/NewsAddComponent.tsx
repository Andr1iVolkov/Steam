import React, { useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';

const NewsAddComponent = () => {
  const [title, setTitle] = useState<string>('');
  const [description, setDescription] = useState<string>('');
  const [dateOfRealease, setDateOfRelease] = useState<string>('');
  const [image, setImage] = useState<string>('');
  const [videoURL, setVideoURL] = useState<string>('');
  const [gameId, setGameId] = useState<string>('');
  const navigate = useNavigate();

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
  
    try{
      await axios.post('http://localhost:5002/api/News', { title, description, image, videoURL, gameId });
      navigate('/');
      console.log('Новина додана успішно!');
    } catch (error) {
      console.error('Помилка при створені новини!', error);
    }
  };

  return (
    <form onSubmit={handleSubmit}>
       <div>
        <label>Title:</label>
        <input type="text" value={title} onChange={(e) => setTitle(e.target.value)} required />
      </div>
      <div>
        <label>Description:</label>
        <textarea value={description} onChange={(e) => setDescription(e.target.value)} required />
      </div>
      <div>
        <label>Image URL:</label>
        <input type="text" value={image} onChange={(e) => setImage(e.target.value)} required />
      </div>
      <div>
        <label>Video URL:</label>
        <input type="text" value={videoURL} onChange={(e) => setVideoURL(e.target.value)} required />
      </div>
      <div>
        <label>Game ID:</label>
        <input type="text" value={gameId} onChange={(e) => setGameId(e.target.value)} required />
      </div>
      <button type="submit">Add News</button>
    </form>
  );
};

export default NewsAddComponent;