import React from "react";

function DeleteAlert(props){
  if (!props.isOpen) return null;

  return (
      <div className="custom-modal">
        <div className="modal-content">
          <h4 className="modal-heading">Confirm Action</h4>
          <p className="paragraph">{props.message}</p>
          <div className="button-container">
              <button id="danger-button" onClick={props.onConfirm}>Confirm</button>
              <button onClick={props.onCancel}>Cancel</button>
          </div>
        </div>
      </div>
  );
}

export default DeleteAlert;