import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';

const EditProjectForm = () => {
    const [description, setDescription] = useState("Project Description");
    const [title, setTitle] = useState("Project Title");

    const navigate = useNavigate("/dashboard");

    const handleDescriptionChange = (event) => {
        setDescription(event.target.value);
    };
    const handleTitleChange = (event) =>{
        setTitle(event.target.value);
    }

    const handleCreate = () => {
        fetch(`/api/Project`, {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify({
            title: title,
            description: description
          })
        })
        .then(response => {
            if(response.ok){
                navigate("/dashboard", {state:{isCreated: true}});
            }
        })
        .catch((error) => {
          console.error('Error:', error);
        });
    }

    return (
        <div className="edit-page">
            <div className="edit-form-container">
                <h2 className="edit-form-heading">Create Project</h2>
                <form className="edit-form">
                    <label htmlFor="projectTitle">Project Title</label>
                    <input type="text" id="projectTitle" name="projectTitle" onChange={handleTitleChange}/>

                    <label htmlFor="projectDescription">Project Description</label>
                    <textarea 
                        id="projectDescription"
                        name="projectDescription"
                        rows={6}
                        onChange={handleDescriptionChange}
                    />
                    <button id="button-save" type="button" onClick={handleCreate}>Save Changes</button>
                    <button type="button" onClick={() => navigate(`/dashboard`)}>Cancel</button>
                </form>
            </div>
        </div>
    );
};

export default EditProjectForm;