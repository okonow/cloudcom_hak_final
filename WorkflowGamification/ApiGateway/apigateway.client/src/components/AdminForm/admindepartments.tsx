import React, { useState } from 'react';

interface Department {
  id: number;
  name: string;
}

const initialDepartments: Department[] = [
  { id: 1, name: 'Разработка' }
];

export const AdminDepartments: React.FC = () => {
  const [departments, setDepartments] = useState({ 
    UserId: '', 
    RoleName: '' 
  });
  // {
  //   "departmentName": "string",
  //   "departmentDescription": "string",
  //   "directorId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  //   "departmentEmployeesId": [
  //     "3fa85f64-5717-4562-b3fc-2c963f66afa6"
  //   ]
  // }

  return (
    <div>
      <div className='create-department'>
      <input type="text" name="de" placeholder="Имя" value={newEmployee.firstName} onChange={handleInputChange} />
        </div>
      <h1>Департаменты</h1>
      <ul>
        {departments.map((department) => (
          <li key={department.id}>{department.name}</li>
        ))}
      </ul>
    </div>
  );
}
