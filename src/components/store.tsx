import React from 'react';
import { useNavigate } from 'react-router-dom';
import '../css/store.css';


export const Store: React.FC = () => {
  return (
    <div className="store-background">
      
        <img src="src\assets\revolver.png" alt="" id="first" className="revolver"/>
        {/*<img src="src\assets\revolver.png" alt="" id="second" className="revolver"/>*/}
      
      <div className="store-container">
        <Navbar />
        <ProductList />
        <Navigation />
      </div>
    </div>
  );
};


const Navbar: React.FC = () => {      
  const navigate = useNavigate();
  const gotoMainForm = () => navigate('/mainform');

  return (
    <div className='navbar'>
          <div className='exit-icon' onClick={gotoMainForm}>
            <img src="src\assets\exit-icon.png" alt=""/>
          </div>
          <div className='now-page' >
            <h1>Товары</h1>
          </div>
          <div className='other-page' >
            <h1>Услуги</h1>
          </div>
        </div>
  );
};


interface ProductData {
  name: string;
  price: number;
  sizes?: string[];
}

const products: ProductData[] = [
  { name: 'GHOST GREEN HOODIE', price: 10000, sizes: ['S'] },
  { name: 'COZER TEE GREY', price: 5000, sizes: ['S', 'M', 'L'] },
  { name: 'CAP BOY', price: 4000, sizes: ['M'] },
  { name: 'SANITIZER PACK', price: 2200 },
];

const ProductList: React.FC = () => {
  return (
    <div className='product-list'>
      {products.map((product, index) => (
        <Product key={index} product={product} />
      ))}
    </div>
  );
};




interface ProductProps {
  product: {
    name: string;
    price: number;
    sizes?: string[];
  };
}

const Product: React.FC<ProductProps> = ({ product }) => {
  return (
    <div className='product'>
      <img src={`/${product.name.toLowerCase().replace(/ /g, '-')}.png`} alt={product.name} />
      <h3>{product.name}</h3>
      <p>{product.price} ₽</p>
      {product.sizes && <p>Размеры: {product.sizes.join(', ')}</p>}
      <button>Add to cart</button>
    </div>
  );
};



const Navigation: React.FC = () => {
  return (
    <nav>
      <a href="#">PROJECTS</a>
      <a href="#">SHOP</a>
      <a href="#">ABOUT</a>
      <a href="#">CONTACT</a>
      <a href="#">HOME</a>
    </nav>
  );
};



