import React, { useEffect, useState } from 'react';
import { useNavigate } from'react-router-dom';
import '../../../css/alltasks.css';
import { DirectorTaskCard } from './directortaskcard';
import { sendRequestWithAccessWithId, sendRequestWithAccess } from '../../Sign/sendrequest';

interface Task {
  id: string;
  title: string;
  description: string;
  IsFinished: boolean;
}

interface TaskCreate {
  title: string;
  description: string;
  deadline: string;
  complexity: 0 | 1 | 2;
  creatorID: string;
  workerId: string;
}


export const DirectorTasks: React.FC = () => {

  const [activeTab, setActiveTab] = useState('done');
  const renderComponent = () => {
    switch (activeTab) {
      case 'done':
        return <DoneTasksDirector />;
      case 'doing':
        return <DoingTasksDirector />;
      case 'untaken':
        return <UntakenTasksDirector />;
      default:
        return <DoneTasksDirector />;
    }
  };

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
    title: "",
    description: "",
    deadline: "",
    complexity: 0,
    creatorID: localStorage.getItem('id'),
    workerId: "",
    
});
    
    const handleInputChange = (event) => {
      const { name, value } = event.target;
      setCreatingTask(prevCreatingTask => ({
        ...prevCreatingTask,
        [name]: value
      }));
    }; 

    const handleNewTask = (e: React.FormEvent<HTMLFormElement>) => {
      e.preventDefault();
      console.log(creatingTask);
          try {
              const accessToken = localStorage.getItem('accessToken');
              console.log('Ответ сервера:', sendRequestWithAccess("PATCH", "https:localhost:7256/api/Job/AddAnswerToJob", creatingTask, accessToken));
              fetchTasks();
          } catch (error) {
              console.error('Произошла ошибка:', error);
           }
       };


  

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

 


  useEffect(() => {

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
        <div className='other-page' onClick={() => setActiveTab('done')}>
          <h1>Выполненное</h1>
        </div>
        <div className='other-page' onClick={() => setActiveTab('doing')}>
          <h1>В процессе</h1>
        </div>
        <div className='other-page' onClick={() => setActiveTab('untaken')}>
          <h1>Свободное</h1>
        </div>
      </div>

      <div className='create-task-form'>
      <form onSubmit={handleNewTask}>
        <div className='input-fields'>
          <div className='input-field'>
            
            <input
              type="text"
              value={creatingTask.title}
              name="title"
              onChange={handleInputChange}
              placeholder="Введите название задачи"
            />
          </div>
          
          <div className='input-field'>
            <textarea
              value={creatingTask.description}
              name="description"
              onChange={handleInputChange}
              placeholder="Введите описание задачи"
            />
          
          </div>
          <div className='input-field'>
            <label>Укажите дедлайн</label>
          <input
              type="date"
              value={creatingTask.deadline}
              name="deadline"
              onChange={handleInputChange}
              placeholder="Укажите дедлайн"
            />
          </div>
          <div className='input-field'>
            <input type="number" 
            value={creatingTask.complexity}
             name="complexity" 
             onChange={handleInputChange} 
             placeholder="Укажите сложность задачи (0, 1 или 2)" />
          </div>
          <div className='input-field'>
            <button type="submit">Добавить задачу</button>
          </div>
        </div>
      </form>
      </div>

      
    </div>
  </div>
    
    
  );
  
};