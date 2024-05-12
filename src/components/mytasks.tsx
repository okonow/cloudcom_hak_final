import { InProgress } from '../components/inprogress';
import "../css/alltasks.css";
import { useNavigate } from 'react-router-dom';
import {useState, useEffect, ReactNode} from'react';

interface Task {
  id: number;
  title: string;
  description: string;
}

interface TaskReply {
  title: string;
  description: string;
  comments: string[];
}

const tasks1: Task[] = [
    { id: 1, title: 'Task 1', description: 'Description 1' },
    { id: 2, title: 'Task 2', description: 'Description 2' },
    { id: 3, title: 'Task 3', description: 'Description 3' },
  ];


  interface ModalProps {
    isOpen: boolean;
    onClose: () => void;
    children?: ReactNode;
  }
  
  const Modal: React.FC<ModalProps> = ({ isOpen, onClose, children }) => {
    if (!isOpen) return null;
  
    return (
      <div style={{ position: 'fixed', top: 0, left: 0, right: 0, bottom: 0, backgroundColor: 'rgba(0, 0, 0, 0.5)', display: 'flex', alignItems: 'center', justifyContent: 'center' }}>
        <div style={{ backgroundColor: 'white', padding: 20 }}>
          {children}
          <button onClick={onClose}>Закрыть</button>
        </div>
      </div>
    );
  };

export const MyTasks = () =>{
    const [isModalOpen, setModalOpen] = useState(false);
    const openModal = () => setModalOpen(true);
  const closeModal = () => setModalOpen(false);
    const [tasks, setTasks] = useState<Task[]>([]);
    const [newTitle, setNewTitle] = useState('');
    const [newDescription, setNewDescription] = useState('');
    const [expandedTaskId, setExpandedTaskId] = useState<number | null>(null);
    const [newReply, setNewReply] = useState('');
    const navigate = useNavigate();
    const gotoAllTasks = () => navigate('/alltasks');
    const gotoMyTasks = () => navigate('/mytasks');
    const gotoMainForm = () => navigate('/mainform');
  
    const handleAlert = (e: React.MouseEvent<HTMLButtonElement>) => {
        e.preventDefault();
        
        alert(newReply);
        
    }
  
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
        
        
        <div className="task-list">
          {tasks.map((task) => (
            <div key={task.id} className="task-card-shell">
              
              <div className="task-card" >
              <h2>{task.title}</h2>
              <p>{task.description}</p>
              <p>ID: {task.id}</p>
            <button onClick={openModal}>Открыть модальное окно</button>
            <Modal isOpen={isModalOpen} onClose={closeModal}>
                <div>
                    <div className='comments'>
                        <p>Комментарии к работе:</p>

                    </div>
                    <form>
                    <input type='text'
                        value={newReply}
                        onChange={(e) => setNewReply(e.target.value)} placeholder='bebe'></input>
                    <button onClick={handleAlert}>Добавить ответ на задание</button>
                    </form>
                </div>
            </Modal>
            
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
                    <div className='reply-window'>
                    <button onClick={openModal}>Ответ на задание</button>
                    <Modal isOpen={isModalOpen} onClose={closeModal}>
                        <div>
                            <div className='comments'>
                                <p>Комментарии к работе:</p>

                            </div>
                            <form>
                            <input type='text'
                                value={newReply}
                                onChange={(e) => setNewReply(e.target.value)} placeholder='bebe'></input>
                            <button onClick={handleAlert}>Добавить ответ на задание</button>
                            </form>
                        </div>
                    </Modal>
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