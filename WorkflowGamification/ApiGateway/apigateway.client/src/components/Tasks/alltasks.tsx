import React, { useEffect, useState } from 'react';
import { useNavigate } from'react-router-dom';
import '../../css/alltasks.css';
import { TaskCard } from './alltaskcard';
import { sendGetRequest } from '../sendrequest';
import Cookies from 'js-cookie';

interface Task {
  id: string;
  title: string;
  description: string;
  IsFinished: boolean;
}


export const AllTasks: React.FC = () => {
  const [tasks, setTasks] = useState<Task[]>([]);
  const [newTaskId, setNewTaskId] = useState('');
  const [newTitle, setNewTitle] = useState('');
  const [newDescription, setNewDescription] = useState('');
  const [newIsFinished, setNewIsFinished] = useState(false);
  const [expandedTaskId, setExpandedTaskId] = useState<number | null>(null);
  const navigate = useNavigate();
  const gotoMyTasks = () => navigate('/mytasks');
  const gotoMainForm = () => navigate('/mainform');


  

  // const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
  //   e.preventDefault();
  //   if (newTitle.trim() && newDescription.trim()) {
  //     const newTask: Task = {
  //       id: newTaskId.trim(),
  //       title: newTitle.trim(),
  //       description: newDescription.trim(),
  //       IsFinished: newIsFinished,
  //     };
  //     setTasks([...tasks, newTask]);
  //     setNewTitle('');
  //     setNewDescription('');
  //   }
  // };
 

  useEffect(() => {
    const fetchTasks = async () => {
      try {
        // const userId = localStorage.get('userId');
        const accessToken = localStorage.getItem('accessToken');
        const response = await sendGetRequest("https://localhost:7288/WorkspaceApi/Job/GetFreeJobsInDepartment", accessToken);
        console.log('Ответ сервера:', response);
        setTasks(response);
      } catch (error) {
        console.error('Произошла ошибка:', error);
      }
    };
  
    // Вызов асинхронной функции для загрузки данных
    fetchTasks();
  }, []);

  // const fetchData = async () => {
  //   try {
  //     const response = await fetch('/api/data');
  //     const data = await response.json();
  //     setData(data);
  //   } catch (error) {
  //     console.error('Ошибка при получении данных:', error);
  //   }
  // };
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
          <div className='other-page'>
            <h1>Все задачи</h1>
          </div>
          <div className='now-page' onClick={gotoMyTasks}>
            <h1>Мои задачи</h1>
          </div>
        </div>
        <div className="tasklist">
          {tasks1.map((task) => (
            <TaskCard key={task.id} task={task} />
          ))}


        </div>
    </div>
    </div>
    
    
  );
  
};