import React, {useState, useContext} from 'react';
import { useNavigate } from 'react-router-dom';
import Img from "../../styles/images/project-manager_69759.png"
import AuthContext from './AuthContext';

const Login = (props) => {
  const[isOpen, setIsOpen] = useState(false);
  const[email, setEmail] = useState("");
  const[password, setPassword] = useState("");
  const[loginError, setLoginError] = useState(false);
  const authCtx = useContext(AuthContext);

  const navigate = useNavigate();

  const handleLogin = (event) => {
    event.preventDefault();
    fetch(`/api/Accounts/login`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
        email: email,
        password: password
      })
    })
    .then(response => {
      if(response.ok){
        console.log("login ok");
        return response.json();
      }else{
        console.log("cs");
        setLoginError(true);
      }
    })
    .then((data) => {
        authCtx.login(data.token);
        //authCtx.showLogin(email);
        setIsOpen(!isOpen);
        props.onClose(isOpen);
        navigate("/dashboard");
    })
    .catch((error) => {
      console.error('Error:', error);
    });
  }

  const handleEmailChange = (event) => {
    setEmail(event.target.value);
  }
  const handlePasswordChange = (event) => {
    setPassword(event.target.value)
  }
  const handleClose = () => {
    setIsOpen(!isOpen);
    props.onClose(isOpen);
  }
  return (
    <div className="login-overlay">
        <div className="login-modal">
        <div className='login-modal-grid'>
            <button className="close-button" onClick={handleClose}>Close</button>
            <img id="login-image" src={Img} loading="lazy" width="200" alt="" />
        </div>
            <form className="login-form" onSubmit={handleLogin}>
              <h2 className="login-form-heading">Login</h2>
              <label htmlFor="loginEmail">Email</label>
              <input
                type="email"
                id="loginEmail"
                placeholder="Email"
                value={email}
                onChange={handleEmailChange}
              />
              <label htmlFor="loginPassword">Password</label>
              <input
                type="password"
                id="loginPassword" 
                placeholder="Password"
                value={password}
                onChange={handlePasswordChange}
              />
              <button type="submit" className="save-button">Login</button>
              {loginError && <div style={{ color: 'red', fontSize: '1rem', lineHeight: '1.2rem', fontWeight: 'bold', marginTop: "10px" }}>Check your email or password and try again</div>}
            </form>
        </div>
    </div>
  );
};

export default Login;
