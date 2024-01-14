import React, { useState } from 'react';
import { useParams, useNavigate, useLocation } from 'react-router-dom';

const EditProjectForm = () => {
    const location = useLocation();
    const { Id } = useParams();

    const [description, setDescription] = useState(location.state?.description || "Faile to load description");

    const navigate = useNavigate("/dashboard");
    const handleDescriptionChange = (event) => {
        setDescription(event.target.value);
    };

    const handleUpdate = () => {
        fetch(`/api/Project/${Id}`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            title: location.state?.title || "Project Title",
            description: description
        })
        })
        .then(response => {
            if(response.ok){
                navigate(`/details-project/${Id}`,
                { state: {
                    title: location.state?.title || "Failed to load title", 
                    description: description || "Failed to load description" ,
                    createdAt : location.state?.createdAt || "Failed to load creation date",
                    lastModified : location.state?.lastModified || "Failed to load last modification date"
                }});
            }
        })
        .catch((error) => {
        console.error('Error:', error);
        });
    }

    return (
        <div className="edit-page">
            <div className="edit-form-container">
                <h2 className="edit-form-heading">Edit Project</h2>
                <form className="edit-form">
                    <label htmlFor="projectTitle">Project Title</label>
                    <input type="text" id="projectTitle" name="projectTitle" disabled value= {location.state?.title || "Faile to load description"}/>

                    <label htmlFor="projectDescription">Project Description</label>
                    <textarea 
                        id="projectDescription"
                        name="projectDescription"
                        rows={6}
                        value={description}
                        onChange={handleDescriptionChange}
                    />
                    <p>Last Modified: {location.state?.lastModified || "0000:00:00"}</p>
                    <p id="edit-form-p">Created At: {location.state?.createdAt || "0000:00:00"}</p>
                    <button id="button-save" type="button" onClick={handleUpdate}>Save Changes</button>
                    <button type="button" onClick={() => navigate(`/dashboard`)}>Cancel</button>
                </form>
            </div>
        </div>
    );
};

export default EditProjectForm;
