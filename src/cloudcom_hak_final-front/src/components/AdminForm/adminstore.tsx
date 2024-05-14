import React, { useState } from 'react';

interface Product {
  id: number;
  name: string;
  description: string;
  type: string;  // 'Услуга' или 'Вещь'
  cost: number;
}

const initialProducts: Product[] = [
  { id: 1, name: 'Клавиатура', description: 'Механическая клавиатура', type: 'Вещь', cost: 2500 }
];

export const AdminStore: React.FC = () => {
  const [products, setProducts] = useState<Product[]>(initialProducts);
  const [newProduct, setNewProduct] = useState({ name: '', description: '', type: '', cost: 0 });

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setNewProduct({ ...newProduct, [e.target.name]: e.target.value });
  };

  const addProduct = () => {
    const newId = products.length + 1;
    const productToAdd = { id: newId, ...newProduct, cost: Number(newProduct.cost) };
    setProducts([...products, productToAdd]);
    setNewProduct({ name: '', description: '', type: '', cost: 0 });
  };

  return (
    <div>
      <h1>Магазин</h1>
      <div>
        <input type="text" name="name" placeholder="Название" value={newProduct.name} onChange={handleInputChange} />
        <input type="text" name="description" placeholder="Описание" value={newProduct.description} onChange={handleInputChange} />
        <input type="text" name="type" placeholder="Тип (Услуга или Вещь)" value={newProduct.type} onChange={handleInputChange} />
        <input type="number" name="cost" placeholder="Стоимость" value={newProduct.cost.toString()} onChange={handleInputChange} />
        <button onClick={addProduct}>Добавить товар</button>
      </div>
      <table>
        <thead>
          <tr>
            <th>Название</th>
            <th>Описание</th>
            <th>Тип</th>
            <th>Стоимость</th>
          </tr>
        </thead>
        <tbody>
          {products.map((product) => (
            <tr key={product.id}>
              <td>{product.name}</td>
              <td>{product.description}</td>
              <td>{product.type}</td>
              <td>{product.cost}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}
