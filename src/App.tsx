import { BrowserRouter, Routes, Route } from 'react-router-dom';
import { Login } from './components/Sign/login';
import { Register } from './components/Sign/register';
import { MainForm } from './components/MainForm/mainform';
import { AllTasks} from './components/Tasks/alltasks';
import { MyTasks } from './components/Tasks/mytasks';
import { Store } from './components/store';
import { Rating } from './components/rating';
import { DirectorTasks } from './components/Tasks/directortasks';
import { AdminForm } from './components/AdminForm/adminform';


const App = () => {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Login />} />
        <Route path="/register" element={<Register />} />
        <Route path="/mainform" element={<MainForm />} />
        <Route path="/alltasks" element={<AllTasks />} />
        <Route path="/mytasks" element={<MyTasks />} />
        <Route path="/store" element={<Store />} />
        <Route path="/rating" element={<Rating />} />
        <Route path="/directortasks" element={<DirectorTasks />} />
        <Route path="/adminform" element={<AdminForm />} />
        
      </Routes>
    </BrowserRouter>
  );
};

export default App;

