import React from "react";

function ConfirmationAlert(props){
    return (
        <div id="confirmationAlert" className={`confirmation-alert ${props.showAlert ? 'show' : ''}`}>
            <p>{props.message}</p>
        </div>
    );
}

export default ConfirmationAlert;