import React, { useState } from 'react';

interface Task {
    id: string;
    title: string;
    description: string;
    IsFinished: boolean;
  }

interface TaskCardProps {
  task: Task;
}

export const TaskCard: React.FC<TaskCardProps> = ({ task }) => {
  const [showDetails, setShowDetails] = useState(false);

  const toggleDetails = () => setShowDetails(!showDetails);

  return (
    <div className="taskcard">
      <div className="visible-items">
        <div className='mini-info'>
        <div className="title"><h2>{task.title}</h2></div>
        <div className="description"><textarea readOnly={true} disabled={true}>{task.description}</textarea></div>
        </div>
        <div className='full-info-button'>
            <img src="src\assets\full-info.png" alt="full-info" onClick={toggleDetails}/>
        </div>
      {showDetails && (
        <div className="full-info">
                 
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
    </div>
  );
};
