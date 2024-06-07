import Header from './components/Header';
import NewsListComponent from './components/news/NewsListComponent';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';


const App = () => {

  return (
    <Router>
        <div className="App">
        <Header />
        <Routes>
          <Route path='/' element={<NewsListComponent/>} />
        </Routes>
        
    </div>
    </Router>
);
}

export default App
