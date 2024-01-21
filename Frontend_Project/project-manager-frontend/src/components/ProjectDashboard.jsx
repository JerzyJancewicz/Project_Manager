import React, { useEffect,  useState, useContext } from "react";
import { useNavigate, useLocation } from 'react-router-dom';
import Project from "./Project";
import FailedToLoadData from "./Errors/FaileToLoadData"
import ConfirmationAlert from "./Alerts/ConfirmationAlert";
import { AuthContext } from './UserCmp/AuthContext';

function ProjectDashboard(){
    const[projectData, setProjectData] = useState([]);
    const[isFailedToLoad, setIsFailedToLoad] = useState(false);
    const[isProjectChanged, setIsProjectChanged] = useState(false);
    const[message, setMessage] = useState("message");
    const token = useContext(AuthContext);

    const navigate = useNavigate("/dashboard");
    //const location = useLocation();
    //const isCreated = location.state?.isCreated || false;
    useEffect(() => { 
        handleGet();
    }, []);

    const handleGet = () => {
        fetch(`/api/Project/${token}`)
            .then((res) => {
                return res.json();
            })
            .then((data) => {
                console.log(data);
                setProjectData(data);
            })
            .catch(error =>{
                console.log(error);
                setIsFailedToLoad(true);
            });
    }

    const handleCreate = () => {
        navigate("/create-project");
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
        if(isProjectChanged){ // || isCreated
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
                        {Array.isArray(projectData) ? (
                            projectData.map((data) => (
                                <Project 
                                    key = {data.idProject}
                                    title = {data.title}
                                    description = {data.description}
                                    createdAt = {data.createAt}
                                    lastModified = {data.lastModified} 
                                    Id = {data.idProject}
                                    messageOnAction = {handleMessageOnAction}
                                    onDelete ={handleProjectDelete}
                                    isDeleted = {handleIsDeleted}
                                />
                            ))
                        ) : (
                            <div>No projects available.</div>
                        )}
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