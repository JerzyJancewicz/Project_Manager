import React from 'react';
import Img from "../../styles/images/project-manager_69759.png"

const Register = ({ onClose }) => {
  const handleRegister = (event) => {
    event.preventDefault();
    // Handle register logic here
  };

  return (
    <div className="login-overlay" onClick={onClose}>
        <div className="login-modal">
            <div className='login-modal-grid'>
                <button className="close-button" onClick={onClose}>Close</button>
                <img id="login-image" src={Img} loading="lazy" width="200" alt="" />
            </div>
            <form className="register-form" onSubmit={handleRegister}>
                <h2 className="register-form-heading">Register</h2>
                <label htmlFor="registerName">Name</label>
                <input type="text" id="registerName" placeholder="Name" required />

                <label htmlFor="registerEmail">Email</label>
                <input type="email" id="registerEmail" placeholder="Email" required />
                
                <label htmlFor="registerPassword">Password</label>
                <input type="password" id="registerPassword" placeholder="Password" required />

                <label htmlFor="registerConfirmPassword">Confirm Password</label>
                <input type="password" id="registerConfirmPassword" placeholder="Confirm Password" required />
                
                <button type="submit" className="save-button">Register</button>
            </form>
        </div>
    </div>
  );
};

export default Register;
