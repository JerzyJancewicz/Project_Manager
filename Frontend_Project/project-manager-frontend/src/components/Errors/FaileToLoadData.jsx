import React from "react";

function FailedToLoadData(props){
    if(!props.isFailedToLoad){return null}
    return (
        <div className="custom-modal">
        <div className="modal-content">
          <h4 className="modal-heading">Failed to load</h4>
          <p className="paragraph">It seems to be an error with loading data</p>
          <div className="button-container">
            <button onClick={props.onCancel}>Cancel</button>
          </div>
        </div>
      </div>
    );
}

export default FailedToLoadData;