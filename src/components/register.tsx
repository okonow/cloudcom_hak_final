import { useState } from 'react';
import '../css/sign.css';
import { useNavigate } from'react-router-dom';
import '../assets/login-pictures/login-img.png';
import { sendRequest } from './sendrequest';


interface CreateUserRequest {
    firstname: string;
    middlename: string;
    lastname: string;
    email: string;
    password: string;
}


export const Register = () =>
    {
      const navigate = useNavigate();
    
      const goToLogin = () => navigate('/');
      const goToMainform = () => navigate('/mainform');
    

      const [createUserRequest, setCreateUserRequest] = useState<CreateUserRequest>({
        firstname: '',
        middlename: '',
        lastname: '',
        email: '',
        password: '',
    });
        
        
      
        const handleRegister = async (e: React.FormEvent<HTMLFormElement>) => {
            e.preventDefault();
            try {
                const createUserRequest = { /* данные для создания нового пользователя */ };
                const response = await sendRequest('POST', '/User/CreateNewUser', createUserRequest);
                console.log('Ответ сервера:', response);
                goToLogin();
            } catch (error) {
                console.error('Произошла ошибка:', error);
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
            <form onSubmit={handleRegister}>
              <h2>Регистрация</h2>
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
              <button type="submit">Зарегистрироваться</button>
              <p className="message">Not registered? <a onClick={goToLogin}>Create an account</a></p>
            </form>
          </div>
          </div>
        );
    }