import {useEffect, useState, useContext} from 'react';
import { BrowserRouter as Router, Route, Routes, useLocation } from 'react-router-dom';
import NavBar from './components/NavBar';
import ProjectDashboard from './components/ProjectDashboard';
import StyleComp from './components/StyleComp';
import EditProjectForm from './components/EditProjectForm';
import NotFound from './components/Errors/NotFound';
import NoAccess from './components/Errors/NoAccess'
import CreateProjectForm from "./components/CreateProjectForm"
import ProjectDetails from "./components/ProjectDetails"
import Home from './components/Home';
import ProtectedRoute from './components/ProtectedRoute'
import AuthContext from './components/UserCmp/AuthContext';
import UserDetails from './components/UserCmp/UserDetails';

const ConditionalStyleComp = () => {
  const location = useLocation();
  if (location.pathname !== "/" && location.pathname !== "/no-access" && location.pathname !== "*") {
      return <StyleComp />;
  }
  return null;
};

const CustomHome = () => {
  const authContext = useContext(AuthContext);

  useEffect(() => {
      authContext.logout();
  }, [authContext]);

  return <Home />;
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

  return (
    <div className="App">
      <AuthContext.Provider value={{ isLoggedIn, login: loginHandler, logout: logoutHandler }}>
        <Router>
          <NavBar />
            <Routes>
              <Route path="/" element={<CustomHome />} />
              <Route path="/dashboard" element={<ProtectedRoute><ProjectDashboard /></ProtectedRoute>} />
              <Route path="/edit-project/:Id" element={<ProtectedRoute><EditProjectForm /></ProtectedRoute>} />
              <Route path="/create-project" element={<ProtectedRoute><CreateProjectForm /></ProtectedRoute>} />
              <Route path="/details-project/:Id" element={<ProtectedRoute><ProjectDetails /></ProtectedRoute>} />
              <Route path="/details-user" element={<ProtectedRoute><UserDetails /></ProtectedRoute>} />
              <Route path="*" element={<NotFound />}/>
              <Route path='/no-access' element={<NoAccess/>}></Route>
            </Routes>
            <ConditionalStyleComp/>
        </Router>
      </AuthContext.Provider>
    </div>
  );
}

export default App;
