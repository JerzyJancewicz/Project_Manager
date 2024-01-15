import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import NavBar from './components/NavBar';
import ProjectDashboard from './components/ProjectDashboard';
import StyleComp from './components/StyleComp';
import EditProjectForm from './components/EditProjectForm';
import NotFound from './components/Errors/NotFound';
import CreateProjectForm from "./components/CreateProjectForm"
import ProjectDetails from "./components/ProjectDetails"
import Home from './components/Home';

function App() {
  return (
    <div className="App">
      <Router>
        <NavBar />
          <Routes>
            <Route path="/" element={<Home />} />
            <Route path="/dashboard" element={<ProjectDashboard />} />
            <Route path="/edit-project/:Id" element={<EditProjectForm />} />
            <Route path="/create-project" element={<CreateProjectForm />} />
            <Route path="/details-project/:Id" element={<ProjectDetails />} />
            <Route path="*" element={<NotFound />}/>
          </Routes>
          <StyleComp />
      </Router>
    </div>
  );
}

export default App;
