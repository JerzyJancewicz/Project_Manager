import React, { useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';

const EditProjectForm = () => {
    const [description, setDescription] = useState("Project Description");
    const [title, setTitle] = useState("Project Title");
    const [user, setUser] = useState();
    const [userList, setUserList] = useState([]);
    const [isInvalid, setIsInvalid] = useState(false);
    const [errorDesc, setErrorDesc] = useState("");
    const token = sessionStorage.getItem('token');
    const { type } = useParams();
    const navigate = useNavigate();

    const handleDescriptionChange = (event) => {
        setDescription(event.target.value);
    };
    const handleTitleChange = (event) =>{
        setTitle(event.target.value);
    }
    const handleUserChange = (event) => {
        setUser(event.target.value);
    }

    const validateEmail = (email) => {
        const regex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        return regex.test(email);
    };
    const containsUser = (username) => {
        return userList.includes(username);
    };

    const addItem = () => {
        const name = "";
        const surname = "";
        if (!user || !validateEmail(user)) {
            setIsInvalid(true);
            setErrorDesc("Invalid email")
        }else if(containsUser(user)){
            setIsInvalid(true);
            setErrorDesc("There is already user with this email")
        }else {
            fetch(`/api/User/${token}/${user}`)
            .then((res) => {
                if(res.ok){
                    setIsInvalid(false);
                    setUserList([...userList, { name, surname, email: user }]);
                    setUser('');
                }else if(res.status == 404){
                    setIsInvalid(true);
                    setErrorDesc("There is not such a user")
                }else{
                    setIsInvalid(true);
                    setErrorDesc("Server error")
                }
            })
            .catch(error =>{
                console.log(error);
            });
        }
    };
    const removeItem = (index) => {
        setUserList(userList.filter((_, i) => i !== index));
    };

    const handleCreate = () => {
        var isGroup = type == 'multi' ? true : false
        console.log(userList);
        console.log(type);
        fetch(`/api/Project/${token}/${isGroup}`, {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json'
          },
          body: JSON.stringify({
            title,
            description,
            users: userList
          })
        })
        .then(response => {
            if(response.ok){
                navigate("/dashboard", {state:{isCreated: true}});
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
                {type === 'multi' ? <h2 className="edit-form-heading">Create Group Project</h2> : <h2 className="edit-form-heading">Create Project</h2>}
                <form className="edit-form" onSubmit={(e) => e.preventDefault()}>
                    <label htmlFor="projectTitle">Project Title</label>
                    <input type="text" id="projectTitle" name="projectTitle" value={title} onChange={handleTitleChange}/>

                    <label htmlFor="projectDescription">Project Description</label>
                    <textarea 
                        id="projectDescription"
                        name="projectDescription"
                        rows={6}
                        value={description}
                        onChange={handleDescriptionChange}
                    />
                    {type === 'multi' && (
                        <div>
                            <label htmlFor="projectUser">Add User's Email</label>
                            <input type="text" id="projectUser" name="projectUser" onChange={handleUserChange} value={user}/>
                            {isInvalid ? 
                                <div style={{display:'flex', justifyContent:'center', color: 'red', fontSize: '1rem', lineHeight: '1.2rem', fontWeight: 'bold', marginTop: "10px", marginBottom:'10px' }}>{errorDesc}</div>
                            : 
                                <></>}
                            <div style={{display: 'flex', justifyContent:'center', marginBottom:'20px'}}>
                                <button id='button-save' type="button" onClick={addItem}>Add User</button>
                            </div>
                            <ul>
                                {userList.map((user, index) => (
                                    <div style={{display:'grid', gridTemplateColumns: '40% 100px'}}>
                                        <li key={index}>{user.email}</li>
                                        <button type="button" style={{marginBottom:'10px'}} onClick={() => removeItem(index)}>Remove</button>
                                    </div>
                                ))}
                            </ul>
                        </div>
                    )}
                    <button style={{marginTop:'10px'}} id="button-save" type="button" onClick={handleCreate}>Save Changes</button>
                    <button type="button" onClick={() => navigate(`/dashboard`)}>Cancel</button>
                </form>
            </div>
        </div>
    );
};

export default EditProjectForm;
