import React from 'react';
import { useLocation, useNavigate, useParams } from 'react-router-dom'; // If you're using react-router

const ProjectDetails = () => {
    const location = useLocation();
    const navigate = useNavigate("/dashboard");
    const {Id} = useParams();

    const handleReturn = () => {
        navigate('/dashboard')
    };

    const handleEdit = () => {
        navigate(`/edit-project/${Id}`,
        { state: {
            title: location.state?.title || "Failed to load title", 
            description: location.state?.description || "Failed to load description" ,
            createdAt : location.state?.createdAt || "Failed to load creation date",
            lastModified : location.state?.lastModified || "Failed to load last modification date"
        }});
    };

    return (
        <div className="details-page">
            <div className="details-container">
                <h2 className="details-heading">Project Details</h2>
                <div className="details-content">
                    <p><strong>Title: </strong>{ location.state?.title || "Failed to load title"}</p>
                    <p><strong>Description: </strong>{location.state?.description || "Failed to load description"}</p>
                    <p><strong>Created At: </strong>{location.state?.createdAt || "Failed to load creation date"}</p>
                    <p><strong>Last Modified: </strong>{location.state?.lastModified || "Failed to load last modification date"}</p>
                </div>
                <div className="details-buttons">
                    <button onClick={handleReturn}>Return</button>
                    <button onClick={handleEdit}>Edit</button>
                </div>
            </div>
        </div>
    );
};

export default ProjectDetails;
