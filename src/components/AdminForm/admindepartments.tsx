import React, { useState } from 'react';

interface Department {
  id: number;
  name: string;
}

const initialDepartments: Department[] = [
  { id: 1, name: 'Разработка' }
];

export const AdminDepartments: React.FC = () => {
  const [departments, setDepartments] = useState<Department[]>(initialDepartments);

  return (
    <div>
      <h1>Департаменты</h1>
      <ul>
        {departments.map((department) => (
          <li key={department.id}>{department.name}</li>
        ))}
      </ul>
    </div>
  );
}
