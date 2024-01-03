import React from "react";
import Img from "../styles/images/project-manager_69759.png"

function NavBar(){
    return (
        <div data-collapse="medium" data-animation="default" data-duration="400" data-easing="ease" data-easing2="ease" role="banner" className="navbar-copy w-nav">
            <div className="nav-container w-container">
                <a href="#" className="brand w-nav-brand">
                    <img src={Img} loading="lazy" width="52" alt="" className="company-logo"/>
                    <h3 className="heading-2">ManageIT</h3>
                </a>
                <nav role="navigation" className="nav-menu w-nav-menu">
                    <a href="#" className="nav-link text-xs text-height-base text-white tracking-normal w-nav-link">All Projects</a>
                    <a href="#" className="nav-link text-xs text-height-base text-white tracking-normal w-nav-link">email@email.com</a>
                    <a href="#" className="nav-link text-xs text-height-base text-white tracking-normal w-nav-link">Logout</a>
                </nav>
                <div data-w-id="eff18ad7-b7dd-fb2a-3602-22539b7accce" className="menu-button w-nav-button">
                    <div className="menu-icon w-icon-nav-menu"></div>
                </div>
            </div>
        </div>
    );
}

export default NavBar;