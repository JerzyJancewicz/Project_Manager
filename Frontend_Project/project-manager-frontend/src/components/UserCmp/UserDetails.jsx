import React, {useEffect, useState} from "react";
import { useNavigate } from 'react-router-dom';

function UserDetails(){
    const [name, setName] = useState("");
    const [surname, setSurname] = useState("");
    const [email, setEmail] = useState("");
    const [phoneNumber, setPhoneNumber] = useState("");
    
    const token = sessionStorage.getItem('token');
    const navigate = useNavigate("/dashboard");

    const handleReturn = () => {
        navigate('/dashboard')
    };

    useEffect(() => {
        fetch(`/api/User/${token}`)
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
                setName(data.name);
                setSurname(data.surname);
                setEmail(data.email);
                setPhoneNumber(data.phoneNumber);
            })
            .catch(error =>{
                console.log(error);
            });
    }, [])

    const handleEdit = () => {
        navigate(`/edit-user/${token}`)
    };

    return (
        <div className="details-page">
            <div className="details-container">
                <h2 className="details-heading">User Details</h2>
                <div className="details-content">
                    <p><strong>Email: </strong>{email}</p>
                    <p><strong>Name: </strong>{name}</p>
                    <p><strong>Surname: </strong>{surname}</p>
                    {/* <p><strong>Phone number: </strong>{PhoneNumber}</p> */}
                    {/* <p><strong>Last Login: </strong>{lastLogin}</p> */}
                </div>
                <div className="details-buttons">
                    <button onClick={handleReturn}>Return</button>
                    {/* <button onClick={handleEdit}>Edit</button> */}
                </div>
            </div>
        </div>
    );
};

export default UserDetails;
