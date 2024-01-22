import React, {useState, useEffect} from 'react';
import { useNavigate, useParams } from 'react-router-dom';

const ProjectDetails = () => {
    const [description, setDescription] = useState("");
    const [title, setTitle] = useState("");
    const [lastModified, setLastModified] = useState("");
    const [createdAt, setCreatedAt] = useState("");
    
    const token = sessionStorage.getItem('token');
    const navigate = useNavigate("/dashboard");
    const {Id} = useParams();

    const handleReturn = () => {
        navigate('/dashboard')
    };

    useEffect(() => {
        fetch(`/api/Project/${Id}/${token}`)
            .then((res) => {
                if(res.ok){
                    return res.json();
                }else if(res.status === 401){
                    navigate('/no-access');
                }else{
                    navigate("/")
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

    const handleEdit = () => {
        navigate(`/edit-project/${Id}`)
    };

    return (
        <div className="details-page">
            <div className="details-container">
                <h2 className="details-heading">Project Details</h2>
                <div className="details-content">
                    <p><strong>Title: </strong>{title}</p>
                    <p><strong>Description: </strong>{description}</p>
                    <p><strong>Created At: </strong>{createdAt}</p>
                    <p><strong>Last Modified: </strong>{lastModified}</p>
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
