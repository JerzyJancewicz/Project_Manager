import React, { useEffect, useState } from "react";
import DeleteAlert from "./Alerts/DeleteAlert";
import EditProjectForm from "./EditProjectForm";
import ConfirmationAlert from "./Alerts/ConfirmationAlert";

function Project(props){

  const [isModalOpen, setIsModalOpen] = useState(false);
  const [isEditBoxOpen, setIsEditBoxOpen] = useState(false);

  // not working
  const [confAlertMessage, setConfAlertMessage] = useState("Seuccess");
  const [isConfAlertOpen, setIsConfAlertOpen] = useState(false);

  const [alertMessage, setAlertMessage] = useState("Are you sure you want to delete this project?");
  const handleDelete = (Id) => {
    fetch(`/api/Project/${Id}`, {
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json',
      },
    })
    .then((response) => {
      if (response.ok) {
        props.onDelete(Id);
      } else {
        setIsModalOpen(true);
        setAlertMessage("Failed to delete a Project");
        console.error('Delete request failed.');
      }
    })
    .catch((error) => {
      console.error('Error:', error);
    });
  }

  const handleCancel = () =>{
    setIsModalOpen(false);
    setIsEditBoxOpen(false);
  }

  const handleConfirmDelete = () => {
    handleDelete(props.Id);
    setIsModalOpen(false);

    setIsConfAlertOpen(true);
    setConfAlertMessage("Project has been successfully deleted");
  }
  const handleConfirmEdit = () =>{}


  // Not working
  useEffect(() => {
    if(isConfAlertOpen){
      console.log('Closing oepeeeewaeawda...');
      const timer = setTimeout(() => {
        console.log('Closing alert...');
        setIsConfAlertOpen(false);

      }, 3000)
      return () => {
        console.log('Clearing timer...');
        clearTimeout(timer);
       };
    }
  }, [isConfAlertOpen]);

  return(
      <div className="div-block-3">
          {/* not working */}
          <ConfirmationAlert
            showAlert = {isConfAlertOpen}
            message = {confAlertMessage}
          />
          <DeleteAlert
            isOpen={isModalOpen}
            onConfirm={handleConfirmDelete}
            onCancel={handleCancel}
            message={alertMessage}
          />
          <EditProjectForm
            isEditing = {isEditBoxOpen}
            onConfirm={handleConfirmEdit}
            onCancel={handleCancel}
          />
          <div className="div-block-4">
            <div className="div-block-5">
              <div className="div-block-7">
                <h4 className="projectdivheading-4">{props.title}</h4>
              </div>
              <p className="paragraph">{props.description}</p>
            </div>
          </div>
          <div className="div-block-6">
            <button href="#" data-w-id="82ea1f37-f6ad-0f8f-721c-096beb408c6a" className="editbutton w-button">Details</button>
            <button href="#" data-w-id="3da02ee8-2133-4824-68fd-c5607db2eace" className="editbutton w-button" onClick={() => setIsEditBoxOpen(true)}>Edit</button>
            <button data-w-id="07eb0d6e-7b0f-0e32-c833-a20b46740659" className="deletebutton w-button" onClick={() => setIsModalOpen(true)}>Delete</button>
          </div>
      </div>
  )
}

export default Project;