'use client';
import { useState } from 'react';
import '../css/sign.css';
import { useNavigate } from'react-router-dom';
import '../assets/login-pictures/login-img.png';
import axios from 'axios';

interface RegisterRequest{
  firstName: string;
  middleName: string;
  lastName: string;
  email: string;
  password: string;
}

interface LoginRequest{
  login: string;
  password: string;
}

export const  Login = () => 
  {

    const navigate = useNavigate();

    const goToRegister = () => navigate('/register');
    const goToMainform = () => navigate('/mainform');

    const [login, setLogin] = useState('');
    const [password, setPassword] = useState('');
  
    const handleSubmit = async (event) => {
      event.preventDefault();
    
      try {
        const loginRequest: LoginRequest = { login, password };
        const response = await axios.post('/api/login', loginRequest);
    
        // Обработка успешного ответа от сервера
        if (response.status === 200) {
          // Например, сохранение токена аутентификации в localStorage
          localStorage.setItem('token', response.data.token);
          goToMainform();
        } else {
          // Обработка ошибки входа
          console.error('Ошибка входа:', response.data.error);
        }
      } catch (error) {
        console.error('Ошибка входа:', error);
      }
    };

    return (
      
      <div className="sign-container">
      <div className="login-register-background">
        <img src="src\assets\login-pictures\login-img-background.jpg" alt="Background" />
      </div>
      {/*
      <div className='content'>
       <div className="login-page" onSubmit={handleSubmit}>
        
         <div className='login-img'>
         <p><img src="src\assets\login-pictures\login-img-background.jpg" alt="Cat entered the space"/></p>
         </div>
         <div className="form">
           <form className="login-form">
             <input type="text" id="login" value={login} onChange={(event) => setLogin(event.target.value)} placeholder="username" />
             <input type="password" id="password" value={password} onChange={(event) => setPassword(event.target.value)} placeholder="password"/>
             <button type="submit" onClick={goToMainform}>login</button>
             <p className="message">Not registered? <a onClick={goToRegister}>Create an account</a></p>
           </form>
         </div>
       </div>
       </div> */}
       <div className="login-form" onSubmit={handleSubmit}>
        <div className='login-img'>
        <img src="src\assets\login-pictures\login-img-background.jpg" alt="Cat entered the space"/>
        </div>
        <form className='register-inputs'>
          <h2>Вход</h2>
          <div>
          <input type="text" id="login" value={login} onChange={(event) => setLogin(event.target.value)} placeholder="username" />
          </div>
          <div>
          <input type="password" id="password" value={password} onChange={(event) => setPassword(event.target.value)} placeholder="password"/>
          </div>
          <button type="submit" onClick={goToMainform}>Войти</button>
          <p className="message">Not registered? <a onClick={goToRegister}>Create an account</a></p>
        </form>
      </div>
    </div>
      // <div>
      // <div className='content'>
      // <div className="login-page" onSubmit={handleSubmit}>
        
      //   <div className='login-img'>
      //   <p><img src="src\assets\login-pictures\login-img-background.jpg" alt="Cat entered the space"/></p>
      //   </div>
      //   <div className="form">
      //     <form className="login-form">
      //       <input type="text" id="login" value={login} onChange={(event) => setLogin(event.target.value)} placeholder="username" />
      //       <input type="password" id="password" value={password} onChange={(event) => setPassword(event.target.value)} placeholder="password"/>
      //       <button type="submit" onClick={goToMainform}>login</button>
      //       <p className="message">Not registered? <a onClick={goToRegister}>Create an account</a></p>
      //     </form>
      //   </div>
      // </div>
      // </div>
      // </div >
    );
}


export const Register = () =>
{
  const navigate = useNavigate();

  const goToLogin = () => navigate('/');
  const goToMainform = () => navigate('/mainform');
  const handleRegister = async (event) => {
    event.preventDefault();
  
    const firstName = event.target.elements.firstName.value;
    const middleName = event.target.elements.middleName.value;
    const lastName = event.target.elements.lastName.value;
    const email = event.target.elements.email.value;
    const password = event.target.elements.password.value;
  
    try {
      const registerRequest: RegisterRequest = {
        firstName,
        middleName,
        lastName,
        email,
        password,
      };
      const response = await axios.post('/api/register', registerRequest);
  
      // Обработка успешного ответа от сервера
      if (response.status === 200) {
        // Например, сохранение токена аутентификации в localStorage
        localStorage.setItem('token', response.data.token);
        goToMainform();
      } else {
        // Обработка ошибки регистрации
        console.error('Ошибка регистрации:', response.data.error);
      }
    } catch (error) {
      console.error('Ошибка регистрации:', error);
    }
  };

    return (
      <div className="sign-container">
      <div className="login-register-background">
        <img src="src\assets\login-pictures\login-img-background.jpg" alt="Background" />
      </div>
      <div className="login-form">
        <div className='login-img'>
        <img src="src\assets\login-pictures\login-img-background.jpg" alt="Cat entered the space"/>
        </div>
        <form>
          <h2>Вход</h2>
          <div className='register-inputs'>
            <div>
          <input type="text" placeholder="name" name="firstName" />
          </div>
          <div>
          <input type="text" placeholder="middle name" name="middleName" />
          </div>
          <div>
          <input type="text" placeholder="last name" name="lastName" />
          </div>
          <div>
          <input type="password" placeholder="password" name="password" />
          </div>
          <div>
          <input type="text" placeholder="email address"/>
          </div>
          
          </div>
          <button type="submit" onClick={goToMainform}>Войти</button>
          <p className="message">Not registered? <a onClick={goToLogin}>Create an account</a></p>
        </form>
      </div>
      </div>
    );
}
/*<div className="login-page">
    <div className="form">
      <form className="register-form" onSubmit={handleRegister}>
          <input type="text" placeholder="name" name="firstName" />
          <input type="text" placeholder="middle name" name="middleName" />
          <input type="text" placeholder="last name" name="lastName" />
          <input type="password" placeholder="password" name="password" />
          <input type="text" placeholder="email address"/>
        <button>create</button>
        <p className="message">Already registered? <a onClick={goToLogin}>Sign In</a></p>
      </form>
    </div>
  </div>;*/
