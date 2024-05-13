import React, { useEffect, useState } from 'react';
import { useNavigate } from'react-router-dom';
import '../../css/alltasks.css';
import { DirectorTaskCard } from './directortaskcard';
import { sendRequestWithAccessWithId } from '../Sign/sendrequest';

interface Task {
  title: string;
  description: string;
  deadline: string;
  complexity: 0 | 1 | 2;
  creatorID: string;
  workerId: string;
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

  const [creatingTask, setCreatingTask] = useState({
    id: "",
    title: "",
    description: "",
    IsFinished: false,
    
});
    
    const handleInputChange = (event) => {
      const { name, value } = event.target;
      setCreatingUser(prevCreatingUser => ({
        ...prevCreatingUser,
        [name]: value
      }));
    }; 


  

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

  const handleNewTask = (e: React.FormEvent<HTMLFormElement>) => {

    
  }


  useEffect(() => {
    const fetchTasks = async () => {
      try {
        const accessToken = localStorage.getItem('accessToken');
        const response = await sendRequestWithAccessWithId("GET", "https://localhost:7256/api/Job/GetUnfinishedJobsInDepartment", null, accessToken, "1");
        console.log('Ответ сервера:', response);
        setTasks(response);
      } catch (error) {
        console.error('Произошла ошибка:', error);
      }
    };

    fetchTasks();
  }, []);


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

      <div className='create-task-form'>
      <form onSubmit={handleNewTask}>
        <div className='input-fields'>
          <div className='input-field'>
            
            <input
              type="text"
              value={newTitle}
              onChange={(e) => setNewTitle(e.target.value)}
              placeholder="Введите название задачи"
            />
          </div>
          
          <div className='input-field'>
            <textarea
              value={newDescription}
              onChange={(e) => setNewDescription(e.target.value)}
              placeholder="Введите описание задачи"
            />
          
          </div>
          <div className='input-field'>
            <button type="submit">Добавить задачу</button>
          </div>
        </div>
      </form>
      </div>

      <div className="tasklist">
        {tasks.map((task) => (
          <DirectorTaskCard key={task.id} task={task} />
        ))}


      </div>
    </div>
  </div>
    
    
  );
  
};