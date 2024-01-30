import React, { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';

const EditProjectForm = () => {
    const { Id } = useParams();
    const token = sessionStorage.getItem('token');
    const [description, setDescription] = useState("");
    const [title, setTitle] = useState("");
    const [lastModified, setLastModified] = useState("");
    const [createdAt, setCreatedAt] = useState("");

    const navigate = useNavigate("/dashboard");
    const handleDescriptionChange = (event) => {
        setDescription(event.target.value);
    };

    useEffect(() => {
        fetch(`/api/Project/${Id}/${token}`)
            .then(response => {
                if(response.ok){
                    return response.json();
                }else if(response.status === 401){
                    navigate('/no-access');
                }else{
                    navigate('/');
                }
              })
            .then((data) => {
                setTitle(data.title);
                setDescription(data.description);
                setCreatedAt(data.createAt);
                setLastModified(data.lastModified);
            })
            .catch(error =>{
                console.log(error);
            });
    }, [])

    const handleUpdate = () => {
        fetch(`/api/Project/${Id}/${token}`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            title: title,
            description: description
        })
        })
        // ok
        .then(response => {
            if(response.ok){
                navigate(`/details-project/${Id}`)
            }else{
                navigate('/');
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
                    <input type="text" id="projectTitle" name="projectTitle" disabled value = {title}/>

                    <label htmlFor="projectDescription">Project Description</label>
                    <textarea 
                        id="projectDescription"
                        name="projectDescription"
                        rows={6}
                        value={description}
                        onChange={handleDescriptionChange}
                    />
                    <p>Last Modified: {lastModified}</p>
                    <p id="edit-form-p">Created At: {createdAt}</p>
                    <button id="button-save" type="button" onClick={handleUpdate}>Save Changes</button>
                    <button type="button" onClick={() => navigate(`/dashboard`)}>Cancel</button>
                </form>
            </div>
        </div>
    );
};

export default EditProjectForm;
