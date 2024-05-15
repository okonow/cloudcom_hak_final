import React, { useState } from 'react';
import { AdminStore} from './adminstore';
import { AdminEmployees } from './adminemployees';
import { AdminDepartments } from './admindepartments';
import '../../css/adminform.css';

interface User {
  id?: number;
  name: string;
  roles: string[];
}

interface Department {
  id?: number;
  name: string;
}

interface Product {
  id?: number;
  name: string;
  price: number;
}

export const AdminForm: React.FC = () => {
  const [activeTab, setActiveTab] = useState('employees');

  const renderComponent = () => {
    switch (activeTab) {
      case 'employees':
        return <AdminEmployees />;
      case 'departments':
        return <AdminDepartments />;
      case 'store':
        return <AdminStore />;
      default:
        return <AdminEmployees />;
    }
  };

  return (
    <div className='adminform-background'>
      <div className='adminform-container'>
        <div className="navbar">
          <div className='other-page' onClick={() => setActiveTab('employees')}><h1>Сотрудники</h1></div>
          <div className='other-page' onClick={() => setActiveTab('departments')}><h1>Департаменты</h1></div>
          <div className='other-page' onClick={() => setActiveTab('store')}><h1>Магазин</h1></div>
        </div>
        <div className="content">
          {renderComponent()}
        </div>
      </div>
    </div>
  );
};