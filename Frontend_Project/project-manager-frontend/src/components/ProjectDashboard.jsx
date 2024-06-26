import React, { useEffect,  useState } from "react";
import { useNavigate } from 'react-router-dom';
import Project from "./Project";
import FailedToLoadData from "./Errors/FaileToLoadData"
import ConfirmationAlert from "./Alerts/ConfirmationAlert";
import SharedProjectDashboard from './SharedProjectDashboard';

function ProjectDashboard(){
    const[projectData, setProjectData] = useState([]);
    const[isFailedToLoad, setIsFailedToLoad] = useState(false);
    const[isProjectChanged, setIsProjectChanged] = useState(false);
    const[message, setMessage] = useState("message");
    const[currentPage, setCurrentPage] = useState(1);
    const[pageSize, setPageSize] = useState(15);
    const token = sessionStorage.getItem('token');
    const navigate = useNavigate("/dashboard");
    const[type, setType] = useState("single");

    useEffect(() => {
        handleResize();
        window.addEventListener('resize', handleResize);
        handleGet();
        return () => window.removeEventListener('resize', handleResize);
    }, [pageSize, currentPage]);

    const handleGet = () => {
        fetch(`/api/Project/${token}/group/false?page=${currentPage}&pageSize=${pageSize}`)
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

    const calculatePageSize = () => {
        const width = window.innerWidth;
        if (width > 1780) {
            return 15;
        } else if (width > 1440) {
            return 12;
        } else if(width > 1090){
            return 9;
        } else if(width > 760){
            return 6;
        }
    };

    const handleResize = () => {
        const newPageSize = calculatePageSize();
        setPageSize(newPageSize);
    };

    const handleCreate = (value) => {
        navigate(`/create-project/${value}`);
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

    const handleNextPage = () => {
        if(projectData.length < pageSize){
            return
        }
        setCurrentPage(currentPage + 1);
    };
    
    const handlePreviousPage = () => {
        setCurrentPage(currentPage - 1);
    };
    
    const handlePage = (value) => {
        if(projectData.length < pageSize && currentPage < value){
            return
        }
        setCurrentPage(value);
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
                <h2 style={{marginLeft: '20px', paddingTop: '10px'}}>Own Projects:</h2>
                <div className="div-block">
                    <button data-w-id="2333b2d0-c779-4514-db04-d3dbf49952ad" className="addbutton w-button" onClick={() => {handleCreate("single")}}>Add Project</button>
                    <div className="div-block-2">
                        {Array.isArray(projectData) ? (
                            projectData.map((data) => (
                                <Project 
                                    key = {data.idProject}
                                    description = {data.description}
                                    title = {data.title}
                                    descredAt = {data.createAt}
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
            <div className="pagination">
                <div className="pagination-background">
                    <a href="#" onClick={handlePreviousPage}>&laquo;</a>
                    {[1, 2, 3, 4, 5, 6].map(number => (
                        <a 
                            href="#" 
                            key={number}
                            className={currentPage === number ? "active" : ""} 
                            onClick={() => handlePage(number)}
                        >
                            {number}
                        </a>
                    ))}
                    <a href="#" onClick={handleNextPage}>&raquo;</a>
                </div>
            </div>
            <div className="page-padding">
                <div className="w-layout-blockcontainer w-container"></div>
            </div>
            
            <h2 style={{marginLeft: '20px'}}>Group Projects:</h2>
            <SharedProjectDashboard/>
        </section>
    );
}

export default ProjectDashboard;