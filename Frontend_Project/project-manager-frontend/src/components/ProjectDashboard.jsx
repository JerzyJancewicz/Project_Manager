import React, { useEffect,  useState, useRef } from "react";
import Project from "./Project";

function ProjectDashboard(){
    const[projectData, setProjectData] = useState([]);
    const hasFetchedDataRef = useRef(false);
    useEffect(() =>{
        if(!hasFetchedDataRef.current){
            fetch('/api/Project', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({
                    title : 'TESTREACT',
                    description: 'TEEESTDESC'
                })
            })
                .then((res) => {
                    return res.json();
                })
                .then((data) => {
                    console.log(data);
                    setProjectData(data);
                    hasFetchedDataRef.current = true; // Update the ref value
                })
                .catch(error => console.log(error)); 
        }
    }, []);
    function handleClick(){
        
    }
    return(
        <section className="crappo-section">
            <div>
                <div className="div-block">
                    <a href="#" data-w-id="2333b2d0-c779-4514-db04-d3dbf49952ad" className="addbutton w-button" onClick={handleClick()}>Add Project</a>
                    <div className="div-block-2">
                        {projectData.map((data) => (
                            <Project title = {data.title} description = {data.description}/>
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