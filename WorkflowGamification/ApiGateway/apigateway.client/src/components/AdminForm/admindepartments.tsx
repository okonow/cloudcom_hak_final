import React, { useEffect, useState } from 'react';
import { sendPostRequestWithAccess, sendRefreshTokenRequest } from '../sendrequest';

interface Department {
    departmentName: string, 
    departmentDescription: string,
    directorId: string,
    departmentEmployeesId: string[],
}

export const AdminDepartments: React.FC = () => {

  const [departments, setDepartments] = useState<Department[]>([]);
  const [newDepartment, setNewDepartment] = useState({ 
    departmentName: '', 
    departmentDescription: '',
    directorId: '',
    departmentEmployeesId: [],
  });

  useEffect(() => {
    sendRefreshTokenRequest();
  }, []);

  const handleInputChangeDepartment = (e: React.ChangeEvent<HTMLInputElement>) => {
    setNewDepartment({ ...newDepartment, [e.target.name]: e.target.value });
  };

  const handleNewDepartment = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    console.log(newDepartment);
    try {
        const accessToken = localStorage.getItem('accessToken');
        const response = sendPostRequestWithAccess("https://localhost:7288/UserApi/User/AuthenticateUser", newDepartment, accessToken)
        console.log('Ответ сервера:', response);
    } catch (error) {
        console.error('Произошла ошибка:', error);
     }
 };


 

  return (
    <div>
      <form onSubmit={handleNewDepartment} className='create-department'>
      <input type="text" name="departmentName" placeholder="название департамента" value={newDepartment.departmentName} onChange={handleInputChangeDepartment} />
      <input type="text" name="departmentDescription" placeholder="описание" value={newDepartment.departmentDescription} onChange={handleInputChangeDepartment} />
      <input type="text" name="directorId" placeholder="id директора" value={newDepartment.directorId} onChange={handleInputChangeDepartment} />
      <button type="submit">Добавить департамент</button>
        </form>
      <h1>Департаменты</h1>
      <ul>
        {departments.map((department) => (
          <li key={department.departmentName}>{department.departmentName}</li>
        ))}
      </ul>
    </div>
  );
}
