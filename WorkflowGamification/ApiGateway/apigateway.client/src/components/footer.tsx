import React from 'react';
import '../css/footer.css';


export const Footer: React.FC = () => {
  return (
    <footer>
      <div className="container">
        <div className="row">
          <div className="col-md-6">
            <p>&copy; 2024 Ваш сайт. Все права защищены.</p>
          </div>
          <div className="col-md-6 text-md-right">
            <ul className="list-inline">
              <li className="list-inline-item">
                <a href="https://ru.wikipedia.org/wiki/%D0%93%D0%BB%D0%B0%D1%81%D0%BD%D0%BE%D1%81%D1%82%D1%8C">Политика гласности</a>
              </li>
              <li className="list-inline-item">
                <a href="https://lenta.ru/articles/2020/11/24/fashionslaves/">Условия использования</a>
              </li>
            </ul>
          </div>
        </div>
      </div>
    </footer>
  );
};
