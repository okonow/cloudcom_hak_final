import '../../css/alltasks.css';
import { useNavigate } from 'react-router-dom';
import {useState, useEffect, ReactNode} from'react';
import { MyTaskCard } from './mytaskcard';
import { sendRequestWithAccessWithId } from '../Sign/sendrequest';

interface Task {
  id: string;
  title: string;
  description: string;
  IsFinished: boolean;
}

  
export const MyTasks = () =>{
  

    const navigate = useNavigate();
    const gotoAllTasks = () => navigate('/alltasks');
    const gotoMyTasks = () => navigate('/mytasks');
    const gotoMainForm = () => navigate('/mainform');

    const [tasks, setTasks] = useState<Task[]>([]);
  
    useEffect(() => {
      const fetchTasks = async () => {
        try {
          const accessToken = localStorage.getItem('accessToken');
          const response = await sendRequestWithAccessWithId("GET", "https://localhost:7024/api/Job/GetEmployeeUnfinishedJobs", null, accessToken, "1");
          console.log('Ответ сервера:', response);
          setTasks(response);
        } catch (error) {
          console.error('Произошла ошибка:', error);
        }
      };
        // Вызов асинхронной функции для загрузки данных
    fetchTasks();
  }, []);
  
  const tasks1: Task[] = [
    {
      id: "1",
      title: "Complete project documentation",
      description: "Write the final sections of the project documentation including summaries and conclusions.",
      IsFinished: false
    },
    {
      id: "2",
      title: "Review codebase",
      description: "Go through the new commits and review for any potential bugs or improvements.",
      IsFinished: true
    },
    {
      id: "3",
      title: "Team meeting",
      description: "Organize a weekly team meeting to discuss project progress and distribute new tasks.",
      IsFinished: false
    },
    {
      id: "4",
      title: "Update dependencies",
      description: "Check for outdated dependencies in the project and update them.",
      IsFinished: true
    }
  ];

    return (
      <div className='alltasks-background'>
        <div className='alltasks-container'>
          <div className='navbar'>
              <div className='exit-icon' onClick={gotoMainForm}>
                <img src="src\assets\exit-icon.png" alt=""/>
              </div>
              <div className='now-page' onClick={gotoAllTasks}>
                <h1>Все задачи</h1>
              </div>
              <div className='other-page' onClick={gotoMyTasks}>
                <h1>Мои задачи</h1>
              </div>
            </div>
          

            <div className="tasklist">
              {tasks1.map((task) => (
                <MyTaskCard key={task.id} task={task} />
              ))}
          </div>
        </div>
      </div>
      
    );
    
  };