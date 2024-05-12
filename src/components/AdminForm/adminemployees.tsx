import React, { useState } from 'react';

interface Employee {
  id: number;
  firstName: string;
  lastName: string;
  middleName: string;
  email: string;
  role: string;
}

const initialEmployees: Employee[] = [
  { id: 1, firstName: 'Иван', lastName: 'Иванов', middleName: 'Иванович', email: 'ivanov@example.com', role: 'Разработчик' }
];

export const AdminEmployees: React.FC = () => {
  const [employees, setEmployees] = useState<Employee[]>(initialEmployees);
  const [newEmployee, setNewEmployee] = useState({ firstName: '', lastName: '', middleName: '', email: '', role: '' });

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setNewEmployee({ ...newEmployee, [e.target.name]: e.target.value });
  };

  const addEmployee = () => {
    const newId = employees.length + 1;
    const employeeToAdd = { id: newId, ...newEmployee };
    setEmployees([...employees, employeeToAdd]);
    setNewEmployee({ firstName: '', lastName: '', middleName: '', email: '', role: '' });
  };

  return (
    <div className='employees'>
      <h1>Сотрудники</h1>
      <div className='create-employee-form'>
        <input type="text" name="firstName" placeholder="Имя" value={newEmployee.firstName} onChange={handleInputChange} />
        <input type="text" name="lastName" placeholder="Фамилия" value={newEmployee.lastName} onChange={handleInputChange} />
        <input type="text" name="middleName" placeholder="Отчество" value={newEmployee.middleName} onChange={handleInputChange} />
        <input type="email" name="email" placeholder="Email" value={newEmployee.email} onChange={handleInputChange} />
        <input type="text" name="role" placeholder="Роль" value={newEmployee.role} onChange={handleInputChange} />
        <button onClick={addEmployee}>Добавить сотрудника</button>
      </div>
      <table className='table'>
        <thead>
          <tr>
            <th>Имя</th>
            <th>Фамилия</th>
            <th>Отчество</th>
            <th>Email</th>
            <th>Роль</th>
          </tr>
        </thead>
        <tbody>
          {employees.map((employee) => (
            <tr key={employee.id}>
              <td>{employee.firstName}</td>
              <td>{employee.lastName}</td>
              <td>{employee.middleName}</td>
              <td>{employee.email}</td>
              <td>{employee.role}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}
