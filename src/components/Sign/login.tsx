import { useState } from 'react';
import '../../css/sign.css';
import { useNavigate } from'react-router-dom';
import '../../assets/login-pictures/login-img.png';
import { sendRequest } from './sendrequest';


interface LoginRequest{
    email: string;
    password: string;
  }
  
  export const Login = () => 
    {
  
      const navigate = useNavigate();
  
      const goToRegister = () => navigate('/register');
      const goToMainform = () => navigate('/mainform');

      const [authenticatingUser, setAuthenticatingUser] = useState({
        login: "",
        password: "",
    });

     const handleInputChange = (event) => {
      const { name, value } = event.target;
      setAuthenticatingUser(prevAuthenticatingUser => ({
        ...prevAuthenticatingUser,
        [name]: value
      }));
     };
    
       const handleLogin = async (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        console.log(authenticatingUser);
        try {
            //sendRequest("POST", "/User/CreateNewUser", authenticateUserRequest);
            console.log('Ответ сервера:', sendRequest("https:localhost:7256/api/User/AuthenticateUser", authenticatingUser));
            const data = await sendRequest("https:localhost:7256/api/User/AuthenticateUser", authenticatingUser);
            const { accessToken } = data;
            
            localStorage.setItem('accessToken', accessToken);
            //goToMainform();
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
          <form onSubmit={handleLogin}>
            <h2>Вход</h2>
            <div className='register-inputs'>
            <div>
            <input type="text" placeholder="email" onChange={handleInputChange} name="login" value={authenticatingUser.login}  />
            </div>
            <div>
            <input type="password" placeholder="password" onChange={handleInputChange} name="password" value={authenticatingUser.password} />
            </div>
            <button type="submit">Войти</button>
            </div>
            <p className="message">Not registered? <a onClick={goToRegister}>Create an account</a></p>
          </form>
        </div>
      </div>
      );
  }