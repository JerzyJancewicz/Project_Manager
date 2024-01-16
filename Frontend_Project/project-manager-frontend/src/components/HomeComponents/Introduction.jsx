import React from "react";
import { Link } from "react-router-dom"
import Project_Management from "../../styles/images/best-free-project-management.jpg";

function Introduction(){
    return(
        <section id="Hero-Section" className="hero-section">
            <div className="page-padding">
                <div className="hero-container w-container">
                    <div className="w-layout-grid hero-grid w-clearfix">
                        <div id="w-node-fde0637e-3e28-0f07-8911-7a0c5fb16700-15d0c59e" className="hero-wrapper w-clearfix">
                            <h1 className="hero-title margin-bottom-xs">Fastest &amp; easiest platform to manage your projects</h1>
                            <p className="hero-subtitle text-gray margin-bottom-sm">Create and manage your own projects in a very easy way.<br/>Don&#x27;t waste your time.</p>
                            <a href="#" className="primary-button hero w-inline-block">
                                <Link to="/dashboard" className="link-no-decoration"><div className="button-text text-sm text-height-sm font-medium">Try for FREE</div></Link>
                            </a>
                        </div><img src={Project_Management} loading="lazy" width="615" sizes="(max-width: 767px) 100vw, 615px" alt="" className="hero-image"/>
                    </div>
                </div>
            </div>
        </section>
    );
}

export default Introduction;