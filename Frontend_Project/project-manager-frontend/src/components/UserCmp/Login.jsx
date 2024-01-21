import React, {useState, createContext} from 'react';
import { useNavigate } from 'react-router-dom';
import Img from "../../styles/images/project-manager_69759.png"

export const AuthContext = createContext();

const Login = (props) => {
  const[isOpen, setIsOpen] = useState(false);
  const[email, setEmail] = useState("");
  const[password, setPassword] = useState("");
  const[emailError, setEmailError] = useState("");
  const[passwordError, setPasswordError] = useState("");

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
        navigate("/dashboard");
        setIsOpen(!isOpen);
        props.onClose(isOpen);
        return response.json();
      }else{
        console.log("dupa");
      }
    })
    .then((data) => {
        sessionStorage.setItem('token', data.token);
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
            <img Id="login-image" src={Img} loading="lazy" width="200" alt="" />
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
              {emailError && <div style={{ color: 'red' }}><p>{emailError}</p></div>}
              <label htmlFor="loginPassword">Password</label>
              <input
                type="password"
                id="loginPassword" 
                placeholder="Password"
                value={password}
                onChange={handlePasswordChange}
              />
              {passwordError && <div style={{ color: 'red' }}><p>{passwordError}</p></div>}
              <button type="submit" className="save-button">Login</button>
            </form>
        </div>
    </div>
  );
};

export default Login;
