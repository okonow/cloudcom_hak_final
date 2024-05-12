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
  { id: 3, title: 'Task 3', description: 'Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description 1' },
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
      const accessToken = localStorage.getItem('accessToken');

      const fetchData = async () => {
        try {
          const response = await fetch('/api/Job/GetFreeJobsInDepartment', {
            method: 'GET',
            headers: {
              departmentId: '1',
              'Authorization': `Bearer ${accessToken}`
            }
          });
  
          if (response.ok) {
            const data = await response.json();
            setData(data);
          } else {
            throw new Error('Ошибка при выполнении запроса');
          }
        } catch (error) {
          console.error('Ошибка:', error);
        }
      };
  
      fetchData();

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
          {tasks.map((task) => (
            <div key={task.id} className="taskcard">
              <div className="visible-items">
                  <div className='mini-info'>
                    <div className="title"><h2>{task.title}</h2></div>
                    <div className="description"><textarea readOnly={true} disabled={true}>{task.description}</textarea></div>
                  </div>
                    <div className='full-info-button'>
                      <img src="src\assets\full-info.png" alt="full-info" onClick={() => setExpandedTaskId(task.id === expandedTaskId ? null : task.id)}/>
                      </div>
                </div>
                  {expandedTaskId === task.id && (
                  <div className="full-info">
                  {/* Здесь можно добавить дополнительную информацию для развернутой карточки */}
                    <div className='other-info'>
                      <div className='deadline'>
                        <p>Дедлайн: {task.id}</p>
                      </div>
                      <div className='difficult'>
                        <p>Сложность задания: {task.id}</p>
                      </div>
                    </div>
                    <div className='take-task-button'>
                      <p>Забрать задание</p>
                    </div>
                  </div>
                  )}
                  
              </div>
            
            
          ))}


        </div>
    </div>
    </div>
    
    
  );
  
};