import React, { useEffect,  useState, useRef } from "react";
import Project from "./Project";

function ProjectDashboard(){
    const[projectData, setProjectData] = useState([]);
    // not working
    const fetchProjects = () => {
        fetch('/api/Project')
            .then((res) => {
                return res.json();
            })
            .then((data) => {
                console.log(data);
                setProjectData(data);
            })
            .catch(error => console.log(error)); 
    }
    useEffect(() =>{
        fetchProjects();
    }, []);

    const handleProjectDelete = (deletedDataId) => {
        setProjectData(projectData.filter(e => e.idProject !== deletedDataId));
    }

    return(
        <section className="crappo-section">
            <div>
                <div className="div-block">
                    <a href="#" data-w-id="2333b2d0-c779-4514-db04-d3dbf49952ad" className="addbutton w-button">Add Project</a>
                    <div className="div-block-2">
                        {projectData.map((data) => (
                            <Project 
                                key = {data.idProject}
                                title = {data.title}
                                description = {data.description} 
                                Id = {data.idProject}
                                onDelete ={handleProjectDelete}
                            />
                        ))}
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