import { useEffect, useState } from "react";
import { ICategoryItem } from "../../../interfaces/categories";
import axios from "axios";

const CategoryListDiv = () => {

  const [list, setList] = useState<ICategoryItem[]>([]);

  useEffect(() => {
    axios.get<ICategoryItem[]>("http://localhost:5002/api/Category")
    .then(resp => {
      const {data} = resp;
      console.log("good", data)
      setList(data);
    })
    .catch(error => {
      console.log("error", error)
    });
  }, []);

  const content = list.map(x=> (
    

    <li key={x.id}>{x.name}</li>
  ));

  return (
    <>
    <h1>Hello</h1>
      <ul className="category-blok">
        {content}
      </ul>
    </>
    
  );
}

export default CategoryListDiv;