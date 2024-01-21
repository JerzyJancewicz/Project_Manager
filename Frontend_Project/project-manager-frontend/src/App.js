import { BrowserRouter as Router, Route, Routes, useLocation } from 'react-router-dom';
import NavBar from './components/NavBar';
import ProjectDashboard from './components/ProjectDashboard';
import StyleComp from './components/StyleComp';
import EditProjectForm from './components/EditProjectForm';
import NotFound from './components/Errors/NotFound';
import CreateProjectForm from "./components/CreateProjectForm"
import ProjectDetails from "./components/ProjectDetails"
import Home from './components/Home';
import {AuthContext} from './components/UserCmp/AuthContext'

const ConditionalStyleComp = () => {
  const location = useLocation();
  if (location.pathname !== "/") {
      return <StyleComp />;
  }
  return null;
};

function App() {
  const token = sessionStorage.getItem('token')

  return (
    <div className="App">
      <AuthContext.Provider value={token}>
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
            <ConditionalStyleComp/>
        </Router>
      </AuthContext.Provider>
    </div>
  );
}

export default App;
