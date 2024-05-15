import React, { useState } from 'react';
import { Modal } from './modalwindow';
import { sendPatchRequest } from '../sendrequest';

interface Task {
    id: string;
    title: string;
    description: string;
    IsFinished: boolean;
  }

interface TaskCardProps {
  task: Task;
}

interface Reply {
    jobId: string;
    description: string;
    departureTime: string;
}

export const MyTaskCard: React.FC<TaskCardProps> = ({ task }) => {
  const [showDetails, setShowDetails] = useState(false);
  
  const [sendingReply, setSendingReply] = useState({
    jobId: task.id,
    description: "",
    departureTime: (new Date).toISOString(),
});

  const [isModalOpen, setModalOpen] = useState(false);
  const openModal = () => setModalOpen(true);
  const closeModal = () => setModalOpen(false);

  const toggleDetails = () => setShowDetails(!showDetails);

  const handleInputChange = (event) => {
    const { name, value } = event.target;
          setSendingReply(prevSendingReply => ({
            ...prevSendingReply,
            [name]: value
          }));
        };

  const handleReply = async (e: React.FormEvent) => {
    e.preventDefault();
    console.log(sendingReply);
        try {
            const accessToken = localStorage.getItem('accessToken');
            console.log('Ответ сервера:', sendPatchRequest("https:localhost:7256/api/Job/AddAnswerToJob", sendingReply, accessToken));
        } catch (error) {
            console.error('Произошла ошибка:', error);
         }
     };

     

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
        <div className='reply-window'>
                    <button onClick={openModal}>Ответ на задание</button>
                    <Modal isOpen={isModalOpen} onClose={closeModal}>
                        <div>
                            <div className='comments'>
                                <p>Комментарии к работе:</p>

                            </div>
                            <form onSubmit={handleReply}>
                            <input type='text' value={sendingReply.description} name="description" onChange={handleInputChange} placeholder='Ответ на задание'></input>
                            <button type="submit">Добавить ответ на задание</button>
                            </form>
                        </div>
                    </Modal>
                    </div>
      </div>
      )}
    </div>
    </div>
  );
};
