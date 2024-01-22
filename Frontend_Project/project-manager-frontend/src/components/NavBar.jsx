import React, {useContext} from "react";
import { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import Img from "../styles/images/project-manager_69759.png"
import Login from "./UserCmp/Login";
import Register from "./UserCmp/Register";
import AuthContext from './/UserCmp/AuthContext';

function NavBar(){
    const [showNav, setShowNav] = useState(false);
    const [isLogin, setIsLogin] = useState(false);
    const [isRegister, setIsRegister] = useState(false);
    const navigate = useNavigate();
    const authContext = useContext(AuthContext);

    const handleCloseLogin = (isOpen) => {
        setIsLogin(isOpen);
    }
    const handleCloseRegister = (isOpen) => {
        setIsRegister(isOpen);
    }
    const toggleNav = () => {
        setShowNav(!showNav);
    };
    const handleLogout = () => {
        authContext.logout();
        // navigate after logout
        setTimeout(() => navigate("/"), 0);
    }

    return (
        <div data-collapse="medium" role="banner" className="navbar-copy w-nav">
            {isLogin ?
                <Login
                    onClose = {handleCloseLogin}
                /> :
                <></> 
            }
            {isRegister ? 
                <Register
                    onClose = {handleCloseRegister}
                /> : 
                <></>
            }
            {!authContext.isLoggedIn ?
                <div className="nav-container w-container">
                    <div className="brand w-nav-brand">
                        <Link to="/"><img src={Img} loading="lazy" width="52" alt="" className="company-logo"/></Link>
                        <Link to="/" className="link-no-decoration"><h3 className="heading-2">ManageIT</h3></Link>
                    </div>
                    <nav role="navigation" className="nav-menu w-nav-menu">
                        <Link to="/" className="nav-link text-xs text-height-base text-white tracking-normal w-nav-link">Home</Link>
                        <Link className="nav-link text-xs text-height-base text-white tracking-normal w-nav-link" onClick={handleCloseLogin}>Login</Link>
                        <Link className="nav-link text-xs text-height-base text-white tracking-normal w-nav-link" onClick={handleCloseRegister}>Register</Link>
                    </nav>
                    <div data-w-id="eff18ad7-b7dd-fb2a-3602-22539b7accce" className="menu-button w-nav-button" onClick={toggleNav}>
                        <div className="menu-icon w-icon-nav-menu"></div>
                    </div>
                    <div className={`nav-overlay ${showNav ? 'overlay-active' : ''}`} onClick={toggleNav}></div>
                    {showNav && (
                        <div className={`nav-box ${showNav ? 'show' : ''}`}>
                            <Link to="/">Home</Link>
                            <Link onClick={handleCloseLogin}>Login</Link>
                            <Link onClick={handleCloseRegister}>Register</Link>
                        </div>
                    )}
                </div>
             : 
                <div className="nav-container w-container">
                        <div className="brand w-nav-brand">
                            <Link to="/dashboard"><img src={Img} loading="lazy" width="52" alt="" className="company-logo"/></Link>
                            <Link to="/dashboard" className="link-no-decoration"><h3 className="heading-2">ManageIT</h3></Link>
                        </div>
                        <nav role="navigation" className="nav-menu w-nav-menu">
                            <Link to="/dashboard" className="nav-link text-xs text-height-base text-white tracking-normal w-nav-link">Projects</Link>
                            <Link to="/user-details" className="nav-link text-xs text-height-base text-white tracking-normal w-nav-link">email@email.com</Link>
                            <Link className="nav-link text-xs text-height-base text-white tracking-normal w-nav-link" onClick={handleLogout}>Logout</Link>
                        </nav>
                        <div className="menu-button w-nav-button" onClick={toggleNav}>
                            <div className="menu-icon w-icon-nav-menu"></div>
                        </div>
                        <div className={`nav-overlay ${showNav ? 'overlay-active' : ''}`} onClick={toggleNav}></div>
                        {showNav && (
                            <div className={`nav-box ${showNav ? 'show' : ''}`}>
                                <Link to="/dashboard">Projects</Link>
                                <Link to="/user-details">email@email.com</Link>
                                <Link onClick={handleLogout}>Logout</Link>
                            </div>
                        )}
                    </div>
             }
        </div>
    );
}

export default NavBar;