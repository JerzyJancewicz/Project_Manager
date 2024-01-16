import React, { useState } from 'react';
import { Link } from "react-router-dom";
function NavigationBox() {
    const [showNav, setShowNav] = useState(false);

    const toggleNav = () => {
        setShowNav(!showNav);
    };

    return (
        <div className="navigation-container">
            <button onClick={toggleNav} className="nav-toggle-button">
                {showNav ? 'Close' : 'Menu'}
            </button>
            {showNav && (
                <div className="nav-box">
                    <Link to="/dashboard"><p>All Projects</p></Link>
                    <Link to="/user"><p>email@email.com</p></Link>
                    <Link to="/user"><p>Logout</p></Link>
                </div>
            )}
        </div>
    );
}

export default NavigationBox;
