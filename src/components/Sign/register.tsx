import { useState } from 'react';
import '../../css/sign.css';
import { useNavigate } from'react-router-dom';
import '../../assets/login-pictures/login-img.png';
import { sendRequest } from './sendrequest';


export const Register = () =>
    {
      const navigate = useNavigate();
    
      const goToLogin = () => navigate('/');
      const goToMainform = () => navigate('/mainform');
    

      const [creatingUser, setCreatingUser] = useState({
        firstName: "",
        middleName: "",
        lastName: "",
        login: "",
        password: "",
    });
        
        const handleInputChange = (event) => {
          const { name, value } = event.target;
          setCreatingUser(prevCreatingUser => ({
            ...prevCreatingUser,
            [name]: value
          }));
        };
      
        const handleRegister = async (e: React.FormEvent<HTMLFormElement>) => {
            e.preventDefault();
            try {
              const result = await sendRequest("https:localhost:7256/api/User/CreateNewUser", creatingUser); // Отправляем данные на сервер
              console.log('Response from server:', result); // Выводим результат в консоль
            } catch (error) {
              console.error('Failed to send data:', error); // Вывод ошибки, если что-то пошло не так
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
              <input type="text" placeholder="name" onChange={handleInputChange} value={creatingUser.firstName} name="firstName" />
              </div>
              <div>
              <input type="text" placeholder="middle name" onChange={handleInputChange} value={creatingUser.middleName} name="middleName" />
              </div>
              <div>
              <input type="text" placeholder="last name" onChange={handleInputChange} value={creatingUser.lastName} name="lastName" />
              </div>
              <div>
              <input type="password" placeholder="password" onChange={handleInputChange} value={creatingUser.password} name="password" />
              </div>
              <div>
              <input type="text" placeholder="email address" onChange={handleInputChange} value={creatingUser.login} name="login"/>
              </div>
              
              </div>
              <button type="submit">Зарегистрироваться</button>
              <p className="message">Already registered? <a onClick={goToLogin}>Log in</a></p>
            </form>
          </div>
          </div>
        );
    }