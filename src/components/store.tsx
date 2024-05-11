import {InProgress} from '../components/inprogress';

//export const Store = () => <InProgress/>;


import React, { useState, ReactNode } from 'react';

// Определение интерфейса модального окна
interface ModalProps {
  isOpen: boolean;
  onClose: () => void;
  children?: ReactNode; // Добавьте свойство children
}

// Компонент модального окна
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

// Основной компонент приложения
export const Store: React.FC = () => {
  const [isModalOpen, setModalOpen] = useState(false);

  const openModal = () => setModalOpen(true);
  const closeModal = () => setModalOpen(false);

  return (
    <div>
      <button onClick={openModal}>Открыть модальное окно</button>
      <Modal isOpen={isModalOpen} onClose={closeModal}>
        <p>Содержимое модального окна</p>
      </Modal>
    </div>
  );
};

