import axios from "axios";
import { useEffect, useState } from 'react';

interface ICategoryItem {
  nickName: string,
  password: string,
  email: string,
  imageBase64: string 
}

const App = () => {

  const [list, setList] = useState<ICategoryItem[]>([]);

  useEffect(() => {
    axios.get<ICategoryItem[]>("http://localhost:5173/api/categories")
    .then(resp => {
      const {data} = resp
      console.log("good", data)
      setList(data);
    })
    .catch(error => {
      console.log("error", error)
    });
  }, []);

  return (
    <>
      <h1>Hello</h1>
    </>
  )
}

export default App
