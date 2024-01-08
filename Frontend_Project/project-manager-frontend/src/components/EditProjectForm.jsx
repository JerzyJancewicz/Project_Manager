import React from "react";

function EditProjectForm(props){
    if(!props.isEditing){return null}
    return (
        <div className="overlay-edit">
            <div className="content-box">
                <h4 className="heading-edit">Edit Project Details</h4>
                <form className="form-edit">
                <label htmlFor="editProjectName">Project Name:</label>
                <input type="text" id="editProjectName" name="projectName" required/>

                <label htmlFor="editProjectDesc">Description:</label>
                <textarea id="editProjectDesc" name="projectDescription" required></textarea>

                <div className="edit-buttons">
                    <button type="submit" className="save-edit" onClick={props.onConfirm}>Save</button>
                    <button type="button" className="cancel-edit" onClick={props.onCancel}>Cancel</button>
                </div>
                </form>
            </div>
        </div>
    );
}

export default EditProjectForm;