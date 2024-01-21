import React from "react";
import { useState } from "react";
import { Link } from "react-router-dom";
import Img from "../styles/images/project-manager_69759.png"
import Login from "./UserCmp/Login";
import Register from "./UserCmp/Register";

function NavBar(){
    const [showNav, setShowNav] = useState(false);
    const [isLogin, setIsLogin] = useState(false);
    const [isRegister, setIsRegister] = useState(false);

    const handleCloseLogin = (isOpen) => {
        setIsLogin(isOpen);
    }
    const handleCloseRegister = (isOpen) => {
        setIsRegister(isOpen);
    }
    const toggleNav = () => {
        setShowNav(!showNav);
    };

    return (
        <div data-collapse="medium" data-animation="default" data-duration="400" data-easing="ease" data-easing2="ease" role="banner" className="navbar-copy w-nav">
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
            <div className="nav-container w-container">
                <a href="#" className="brand w-nav-brand">
                    <Link to="/"><img src={Img} loading="lazy" width="52" alt="" className="company-logo"/></Link>
                    <Link to="/" className="link-no-decoration"><h3 className="heading-2">ManageIT</h3></Link>
                </a>
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
                        <Link to="/dashboard">All Projects</Link>
                        <Link onClick={handleCloseLogin}>Login</Link>
                        <Link onClick={handleCloseRegister}>Register</Link>
                    </div>
                )}
            </div>
        </div>
    );
}

export default NavBar;