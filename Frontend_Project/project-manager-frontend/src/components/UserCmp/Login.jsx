import React from 'react';
import Img from "../../styles/images/project-manager_69759.png"
const LoginModal = ({ onClose }) => {
  const handleLogin = (event) => {
    event.preventDefault();
    // Handle login logic here
  };

  return (
    <div className="login-overlay" onClick={onClose}>
        <div className="login-modal">
        <div className='login-modal-grid'>
            <button className="close-button" onClick={onClose}>Close</button>
            <img Id="login-image" src={Img} loading="lazy" width="200" alt="" />
        </div>
            <form className="login-form" onSubmit={handleLogin}>
            <h2 className="login-form-heading">Login</h2>
            <label htmlFor="loginEmail">Email</label>
            <input type="email" id="loginEmail" placeholder="Email" required />
            
            <label htmlFor="loginPassword">Password</label>
            <input type="password" id="loginPassword" placeholder="Password" required />
            
            <button type="submit" className="save-button">Login</button>
            </form>
        </div>
    </div>
  );
};

export default LoginModal;
