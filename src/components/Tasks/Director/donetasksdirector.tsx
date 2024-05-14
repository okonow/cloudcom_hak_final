import { useEffect, useState } from "react";
import { sendGetRequestId } from "../../Sign/sendrequest";
interface Task {
    id: string;
    title: string;
    description: string;
    IsFinished: boolean;
  }


export const DoneTasksDirector = () => {


    export const DoingTasksDirector = () => {
    
        const [tasks, setTasks] = useState<Task[]>([]);
    
        const fetchTasks = async () => {
            try {
              const accessToken = localStorage.getItem('accessToken');
              const response = await sendGetRequestId("https://localhost:7256/api/Job/GetUnfinishedJobsInDepartment", accessToken, localStorage.get('userId') );
              console.log('Ответ сервера:', response);
              setTasks(response);
            } catch (error) {
              console.error('Произошла ошибка:', error);
            }
          };
    
    
        useEffect(() => {
    
        }, []); 
    } 
}