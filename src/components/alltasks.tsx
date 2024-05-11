import React, { useEffect, useState } from 'react';
import { useNavigate } from'react-router-dom';
import '../css/alltasks.css';

interface Task {
  id: number;
  title: string;
  description: string;
}

const tasks1: Task[] = [
  { id: 1, title: 'Task 1', description: 'Description 1' },
  { id: 2, title: 'Task 2', description: 'Description 2' },
  { id: 3, title: 'Task 3', description: 'Description 3' },
];

export const AllTasks: React.FC = () => {
  const [tasks, setTasks] = useState<Task[]>([]);
  const [newTitle, setNewTitle] = useState('');
  const [newDescription, setNewDescription] = useState('');
  const [expandedTaskId, setExpandedTaskId] = useState<number | null>(null);
  const navigate = useNavigate();
  const gotoMyTasks = () => navigate('/mytasks');
  const gotoMainForm = () => navigate('/mainform');


  

  const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    if (newTitle.trim() && newDescription.trim()) {
      const newTask: Task = {
        id: Date.now(),
        title: newTitle.trim(),
        description: newDescription.trim(),
      };
      setTasks([...tasks, newTask]);
      setNewTitle('');
      setNewDescription('');
    }
  };

  const [data, setData] = useState<any[]>([]);

  useEffect(() => {
    setTasks([...tasks1]);
    fetchData();
  }, []); // Пустой массив зависимостей гарантирует, что эффект выполнится только один раз после первой отрисовки компонента

  const fetchData = async () => {
    try {
      const response = await fetch('/api/data');
      const data = await response.json();
      setData(data);
    } catch (error) {
      console.error('Ошибка при получении данных:', error);
    }
  };

  return (
    <div className='mytasks-container'>
    <div className='alltasks-container'>
      <div className='navbar'>
      <div className='exit-icon' onClick={gotoMainForm}>
                <img src="src\assets\exit-icon.png" alt=""/>
            </div>
        <div className='now-page-title'>
          <h1>Все задачи</h1>
      </div>
      <div className='other-page-title' onClick={gotoMyTasks}>
       <h1>Мои задачи</h1>
      </div>
      </div>
      
      <form onSubmit={handleSubmit}>
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
      
      <div className="task-list">
        {tasks.map((task) => (
          <div key={task.id} className="task-card-shell">
            
            <div className="task-card" onClick={() => setExpandedTaskId(task.id === expandedTaskId ? null : task.id)}>
            <h2>{task.title}</h2>
            <p>{task.description}</p>
            <p>ID: {task.id}</p>
            <button onClick={() => setExpandedTaskId(task.id === expandedTaskId ? null : task.id)}>Развернуть</button>
            {expandedTaskId === task.id && (
          <div className="expanded-info">
            {/* Здесь можно добавить дополнительную информацию для развернутой карточки */}
            <p>Дополнительная информация для задачи с ID: {task.id}</p>
            <button>Приступить к выполнению задания</button>
          </div>
        )}
            </div>
          </div>
          
        ))}
      </div>
    </div>
    </div>
    
  );
  
};