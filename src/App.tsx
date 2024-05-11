import { BrowserRouter, Routes, Route } from 'react-router-dom';
import { Login, Register} from './components/sign'; 
import { MainForm } from './components/mainform';
import { AllTasks} from './components/alltasks';
import { MyTasks } from './components/mytasks';
import { Store } from './components/store';
import { Rating } from './components/rating';
import { DirectorTasks } from './components/directortask';


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
      </Routes>
    </BrowserRouter>
  );
};

export default App;

