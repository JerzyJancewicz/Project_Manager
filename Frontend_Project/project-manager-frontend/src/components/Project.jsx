import React from "react";

function Project(props){
    return(
        <div className="div-block-3">
            <div className="div-block-4">
              <div className="div-block-5">
                <div className="div-block-7">
                  <h4 className="projectdivheading-4">{props.title}</h4>
                </div>
                <p className="paragraph">{props.description}</p>
              </div>
            </div>
            <div className="div-block-6">
              <a href="#" data-w-id="82ea1f37-f6ad-0f8f-721c-096beb408c6a" className="editbutton w-button">Details</a>
              <a href="#" data-w-id="3da02ee8-2133-4824-68fd-c5607db2eace" className="editbutton w-button">Edit</a>
              <a href="#" data-w-id="07eb0d6e-7b0f-0e32-c833-a20b46740659" className="deletebutton w-button">Delete</a>
            </div>
        </div>
    )
}

export default Project;