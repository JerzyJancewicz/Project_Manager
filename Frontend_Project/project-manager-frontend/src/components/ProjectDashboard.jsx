import React from "react";
import Project from "./Project";


function ProjectDashboard(){
    return(
        <section className="crappo-section">
            <div>
                <div className="div-block">
                    <a href="#" data-w-id="2333b2d0-c779-4514-db04-d3dbf49952ad" className="addbutton w-button">Add Project</a>
                    <div className="div-block-2">
                      <Project/>
                    </div>
                </div>
            </div>
            <div className="page-padding">
                <div className="w-layout-blockcontainer w-container"></div>
            </div>
        </section>
    );
}

export default ProjectDashboard;