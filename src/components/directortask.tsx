import React, { useEffect, useState } from 'react';
import { useNavigate } from'react-router-dom';
import '../css/alltasks.css';

interface Task {
    id: number;
    title: string;
    description: string;
    status: 'done' | 'in_progress' | 'not_started';
  }

const tasks: Task[] = [
    { id: 1, title: 'Задача 1', description: '', status: 'done' },
    { id: 2, title: 'Задача 2', description: '', status: 'in_progress' },
    { id: 3, title: 'Задача 3', description: '', status: 'not_started' },
    // Добавьте другие задачи по аналогии
  ];

export const DirectorTasks: React.FC = () => {

    // Состояние для текущего выбранного раздела
  const [currentTab, setCurrentTab] = useState<'done' | 'in_progress' | 'not_started'>('done');

  // Фильтрация списка задач в зависимости от выбранного раздела
  const filteredTasks = tasks.filter(task => task.status === currentTab);
  const [tasks, setTasks] = useState<Task[]>([]);
  const [newTitle, setNewTitle] = useState('');
  const [newDescription, setNewDescription] = useState('');
  const [newStatus, setNewStatus] = useState('');
  const [expandedTaskId, setExpandedTaskId] = useState<number | null>(null);
  const navigate = useNavigate();
  const gotoMyTasks = () => navigate('/mytasks');

  

  const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    if (newTitle.trim() && newDescription.trim()) {
      const newTask: Task = {
        id: Date.now(),
        title: newTitle.trim(),
        description: newDescription.trim(),
        //status: newStatus,
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
        <div className='now-page-title' onClick={gotoUnDoingTasks}>
          <h1>Свободные задачи</h1>
      </div>
      <div className='other-page-title' onClick={gotoDoneTasks}>
       <h1>Выполненные задачи</h1>
      </div>
      <div className='other-page-title' onClick={gotoDoingTasks}>
       <h1>Задачи в процессе</h1>
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

//import React, { useState } from 'react';

// // Представление типа данных для задачи
// interface Task {
//   id: number;
//   title: string;
//   description: string;
//   status: 'done' | 'in_progress' | 'not_started';
// }

// // Пример данных
// const tasks: Task[] = [
//   { id: 1, title: 'Задача 1', description: '', status: 'done' },
//   { id: 2, title: 'Задача 2', description: '', status: 'in_progress' },
//   { id: 3, title: 'Задача 3', description: '', status: 'not_started' },
//   // Добавьте другие задачи по аналогии
// ];

export const DirectorTasksFilter = () => {
  // Состояние для текущего выбранного раздела
  const [currentTab, setCurrentTab] = useState<'done' | 'in_progress' | 'not_started'>('done');

  // Фильтрация списка задач в зависимости от выбранного раздела
  const filteredTasks = tasks.filter(task => task.status === currentTab);

  return (
    <div>
      <h1>Список задач</h1>
      <div>
        <button onClick={() => setCurrentTab('done')}>Сделанные</button>
        <button onClick={() => setCurrentTab('in_progress')}>Выполняющиеся</button>
        <button onClick={() => setCurrentTab('not_started')}>Не начатые</button>
      </div>
      <ul>
        {filteredTasks.map(task => (
          <li key={task.id}>{task.title}</li>
        ))}
      </ul>
    </div>
  );
};

