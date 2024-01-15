import React from "react";
import Project_Manager from "../../styles/images/project-manager_69759.png";

function Footer(){
    return(
        <section className="footer-section">
            <div className="w-layout-grid footer-grid">
            <div className="footer-logo-content">
                <a href="#Hero-Section" className="footer-link w-inline-block"></a>
                <a href="#" className="brand w-nav-brand"><img src={Project_Manager} loading="lazy" width="52" alt="" className="company-logo"/>
                <h3 className="heading-2">ManageIT</h3>
                </a>
            </div>
            <div id="w-node-a042c7c1-ce81-fccc-9ec4-39344edc7c7e-15d0c59e" className="resource-wrap">
                <div className="footer-header text-base text-height-lg font-medium text-white margin-bottom-xs">Quick Link</div>
                <a href="#" className="footer-sub-text text-xs text-height-xl">Home</a>
                <a href="#" className="footer-sub-text text-xs text-height-xl">About</a>
            </div>
            </div>
            <div className="copy-right-text text-xs text-height-base tracking-normal text-white">©2024. All rights reserved</div>
        </section>
    );
}

export default Footer;