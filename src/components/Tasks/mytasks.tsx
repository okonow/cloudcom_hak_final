import '../../css/alltasks.css';
import { useNavigate } from 'react-router-dom';
import {useState, useEffect, ReactNode} from'react';
import { MyTaskCard } from './mytaskcard';

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
  

    const navigate = useNavigate();
    const gotoAllTasks = () => navigate('/alltasks');
    const gotoMyTasks = () => navigate('/mytasks');
    const gotoMainForm = () => navigate('/mainform');
  
 
  
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
              {tasks.map((task) => (
                <MyTaskCard key={task.id} task={task} />
              ))}
          </div>
        </div>
      </div>
      
    );
    
  };