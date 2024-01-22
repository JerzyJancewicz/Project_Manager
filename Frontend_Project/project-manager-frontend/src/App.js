import {useEffect, useState} from 'react';
import { BrowserRouter as Router, Route, Routes, useLocation } from 'react-router-dom';
import NavBar from './components/NavBar';
import ProjectDashboard from './components/ProjectDashboard';
import StyleComp from './components/StyleComp';
import EditProjectForm from './components/EditProjectForm';
import NotFound from './components/Errors/NotFound';
import CreateProjectForm from "./components/CreateProjectForm"
import ProjectDetails from "./components/ProjectDetails"
import Home from './components/Home';
import ProtectedRoute from './components/ProtectedRoute'
import AuthContext from './components/UserCmp/AuthContext';

const ConditionalStyleComp = () => {
  const location = useLocation();
  if (location.pathname !== "/") {
      return <StyleComp />;
  }
  return null;
};

function App() {
  const [isLoggedIn, setIsLoggedIn] = useState(false);

  useEffect(() => {
    const storedToken = sessionStorage.getItem('token');
    if (storedToken) {
      setIsLoggedIn(true);
    }
  }, []);

  const loginHandler = (token) => {
    sessionStorage.setItem('token', token);
    setIsLoggedIn(true);
  };

  const logoutHandler = () => {
    sessionStorage.removeItem('token');
    setIsLoggedIn(false);
  };

  // TODO: add fetch to edit component and details component. on backend add get by id and authorize it

  return (
    <div className="App">
      <AuthContext.Provider value={{ isLoggedIn, login: loginHandler, logout: logoutHandler }}>
        <Router>
          <NavBar />
            <Routes>
              <Route path="/" element={<Home />} />
              <Route path="/dashboard" element={<ProtectedRoute><ProjectDashboard /></ProtectedRoute>} />
              <Route path="/edit-project/:Id" element={<ProtectedRoute><EditProjectForm /></ProtectedRoute>} />
              <Route path="/create-project" element={<ProtectedRoute><CreateProjectForm /></ProtectedRoute>} />
              <Route path="/details-project/:Id" element={<ProtectedRoute><ProjectDetails /></ProtectedRoute>} />
              <Route path="*" element={<NotFound />}/>
              <Route path='/no-access' element={<NotFound/>}></Route>
            </Routes>
            <ConditionalStyleComp/>
        </Router>
      </AuthContext.Provider>
    </div>
  );
}

export default App;
