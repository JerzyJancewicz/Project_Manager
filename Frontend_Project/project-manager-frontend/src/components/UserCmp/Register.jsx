import React, {useState, useEffect} from 'react';
import { useNavigate } from 'react-router-dom';
import Img from "../../styles/images/project-manager_69759.png"

const Register = (props) => {
  const[isOpen, setIsOpen] = useState(false);
  const[email, setEmail] = useState("");
  const[password, setPassword] = useState("");
  const[confirmationPassword, setConfirmationPassword] = useState("");
  
  const[emailError, setEmailError] = useState("");
  const[registerError, setRegisterError] = useState("");
  const[specialLetter, setSpecialLetter] = useState("❌");
  const[numberOfCharacters, setNumberOfCharacters] = useState("❌");
  const[capitalLetter, setCapitalLetter] = useState("❌");
  const[lowercaseLetter, setLowercaseLetter] = useState("❌");
  const[sameConfirmationPassword, setSameConfirmationPassword] = useState("❌");

  const navigate = useNavigate("/dashboard");
  const handleCreate = () => {
    fetch(`/api/User`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
        name: "",
        surname: "",
        email: email,
        password: password
      })
    })
    .then(response => {
        if(response.status === 201){
          navigate("/dashboard");
        }else if(response.status === 400 || response.status === 409){
          setRegisterError("There is already user registered on this email");
        }else if(response.status === 500){
          setRegisterError("Can't connect to the server");
        }
    })
    .catch((error) => {
      console.error('Error:', error);
    });
  }

  const handleRegister = (event) => {
    event.preventDefault();
    if(emailError === "" && specialLetter === "✅" && numberOfCharacters === "✅" && capitalLetter === "✅" && sameConfirmationPassword === "✅"){
      setRegisterError("");
      handleCreate();
    }else{
      setRegisterError("Invalid values");
    }
  };
  const validateCapitalLetter = (value) => {
    const regex = /[A-Z]/;
    return regex.test(value);
  }
  const validateLowercaseLetter = (value) => {
    const regex = /[a-z]/;
    return regex.test(value);
  }
  const validateNumberOfCharacters = (value) => {
    const regex = /.{8,}/;
    return regex.test(value);
  }
  const validateSpecialCharacter = (value) => {
    const regex = /[!@#$%^&*]/;
    return regex.test(value);
  }
  const validateEmail = (email) => {
    const regex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return regex.test(email);
  };
  const validateConfirmationPassword = () => {
    let valid = password === confirmationPassword && password !== "" && confirmationPassword !== "";
    return valid;
  }

  useEffect(() => {
    if(!validateConfirmationPassword()){
      setSameConfirmationPassword("❌");
    }else{
      setSameConfirmationPassword("✅");
    }
  });
  const handleEmailChange = (event) => {
    let tmpEmail = event.target.value;
    setEmail(tmpEmail);
    if(!validateEmail(tmpEmail)){
      setEmailError("Invalid Email");
    }else{
      setEmailError("");
    }
  }
  const handlePasswordChange = (event) => {
    let tmpPass = event.target.value;
    setPassword(tmpPass);
    if(!validateCapitalLetter(tmpPass)){
      setCapitalLetter("❌");
    }else{
      setCapitalLetter("✅");
    }
    if(!validateLowercaseLetter(tmpPass)){
      setLowercaseLetter("❌");
    }else{
      setLowercaseLetter("✅");
    }
    if(!validateNumberOfCharacters(tmpPass)){
      setNumberOfCharacters("❌");
    }else{
      setNumberOfCharacters("✅");
    }
    if(!validateSpecialCharacter(tmpPass)){
      setSpecialLetter("❌");
    }else{
      setSpecialLetter("✅");
    }
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
            <form className="register-form" onSubmit={handleRegister}>
                <h2 className="register-form-heading">Register</h2>
                <label htmlFor="registerEmail">Email</label>
                <input
                  type="text"
                  id="registerEmail"
                  placeholder="Email"
                  value={email}
                  onChange={handleEmailChange}
                />
                {emailError && (
                    <div style={{ color: 'red', fontSize: '1.1rem', lineHeight: '1.2rem', fontWeight: 'bold', marginBottom: "10px" }}>
                        {emailError}
                    </div>
                )}
                <label htmlFor="registerPassword">Password</label>
                <input
                  type="password"
                  id="registerPassword" 
                  placeholder="Password"
                  value={password}
                  onChange={handlePasswordChange}
                />
                <label htmlFor="registerConfirmPassword">Confirm Password</label>
                <input
                  type="password"
                  id="registerConfirmPassword" 
                  placeholder="Confirm Password"
                  value={confirmationPassword}
                  onChange={(e) => {setConfirmationPassword(e.target.value)}}
                />
                <div>
                  <p style={{marginLeft:"5px"}}>Password must have at least:</p>
                  <ul>
                      <li><p>1 capital letter: {capitalLetter}</p></li>
                      <li><p>1 lowercase letter: {lowercaseLetter}</p></li>
                      <li><p>1 special character: {specialLetter}</p></li>
                      <li><p>8 characters: {numberOfCharacters}</p></li>
                      <li><p>Confirmation password: {sameConfirmationPassword}</p></li>
                  </ul>
                </div>
                <button type="submit" className="save-button">Register</button>
                {registerError && (
                    <div style={{ color: 'red', fontSize: '1.1rem', lineHeight: '1.2rem', fontWeight: 'bold', marginTop: "10px",textAlign:"center"}}>
                        {registerError}
                    </div>
                )}
            </form>
        </div>
    </div>
  );
};

export default Register;
