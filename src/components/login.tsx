import { useState } from 'react';
import '../css/sign.css';
import { useNavigate } from'react-router-dom';
import '../assets/login-pictures/login-img.png';
import { sendRequest } from './sendrequest';


interface LoginRequest{
    email: string;
    password: string;
  }
  
  export const  Login = () => 
    {
  
      const navigate = useNavigate();
  
      const goToRegister = () => navigate('/register');
      const goToMainform = () => navigate('/mainform');
  
      const [login, setLogin] = useState('');
      const [password, setPassword] = useState('');

      const [authenticateUserRequest, setAuthenticateUserRequest] = useState<LoginRequest>({
        email: '',
        password: '',
    });
    
      const handleLogin = async (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        try {
            //sendRequest("POST", "/User/CreateNewUser", authenticateUserRequest);
            console.log('Ответ сервера:', sendRequest("POST", "/User/CreateNewUser", authenticateUserRequest));
            const data = await sendRequest("POST", "/User/CreateNewUser", authenticateUserRequest);
            const { accessToken } = data;

            localStorage.setItem('accessToken', accessToken);
            goToMainform();
        } catch (error) {
            console.error('Произошла ошибка:', error);
        }
    };
  
      return (
        
        <div className="sign-container">
        <div className="login-register-background">
          <img src="src\assets\login-pictures\login-img-background.jpg" alt="Background" />
        </div>
         <div className="login-form" >
          <div className='login-img'>
          <img src="src\assets\login-pictures\login-img-background.jpg" alt="Cat entered the space"/>
          </div>
          <form className='register-inputs' onSubmit={handleLogin}>
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
      );
  }