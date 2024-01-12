import React, { useEffect,  useState, useRef } from "react";
import Project from "./Project";
import FailedToLoadData from "./Errors/FaileToLoadData"
import ConfirmationAlert from "./Alerts/ConfirmationAlert";

function ProjectDashboard(){
    const[projectData, setProjectData] = useState([]);
    const[isFailedToLoad, setIsFailedToLoad] = useState(false);
    const[isProjectChanged, setIsProjectChanged] = useState(false);

    const[message, setMessage] = useState("message");
    useEffect(() => { 
        handleGet();
    }, []);

    const handleGet = () => {
        fetch('/api/Project')
            .then((res) => {
                return res.json();
            })
            .then((data) => {
                console.log(data)
                setProjectData(data);
            })
            .catch(error =>{
                console.log(error)
                setIsFailedToLoad(true);
            });
    }

    const handleCreate = () => {
        fetch(`/api/Project`, {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify({
            title: 'cos',
            description: 'csodesc'
          })
        })
        .then(response => {
            if(response.ok){
                handleGet();
                setIsProjectChanged(true);
                setMessage("Project has been successfully created")
            }
        })
        .catch((error) => {
          console.error('Error:', error);
        });
      }

    const handleProjectDelete = (deletedDataId) => {
        setProjectData(projectData.filter(e => e.idProject !== deletedDataId));
    }
    const handleCancel = () => {
        setIsFailedToLoad(false);
    }

    const handleMessageOnAction = (message) =>{
        setMessage(message)
    }
    const handleIsDeleted = (isDeleted) => {
        setIsProjectChanged(isDeleted);
    }

    useEffect(() => {
    if(isProjectChanged){
        const timer = setTimeout(() => {
            setIsProjectChanged(false);
        }, 2000)
        return () => {
        clearTimeout(timer);
        };
    }
  }, [isProjectChanged]);

    return(
        <section className="crappo-section">
            <div>
                <FailedToLoadData
                    isFailedToLoad={isFailedToLoad}
                    onCancel={handleCancel}
                />
                {isProjectChanged ?
                    <ConfirmationAlert
                        showAlert = {isProjectChanged}
                        message = {message}
                    />
                 : <></>}
                <div className="div-block">
                    <button data-w-id="2333b2d0-c779-4514-db04-d3dbf49952ad" className="addbutton w-button" onClick={handleCreate}>Add Project</button>
                    <div className="div-block-2">
                        {projectData.map((data) => (
                            <Project 
                                key = {data.idProject}
                                title = {data.title}
                                description = {data.description} 
                                Id = {data.idProject}
                                messageOnAction = {handleMessageOnAction}
                                onDelete ={handleProjectDelete}
                                isDeleted = {handleIsDeleted}
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