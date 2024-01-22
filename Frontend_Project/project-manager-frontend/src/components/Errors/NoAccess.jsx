import React from "react";
import Img from "../../styles/images/project-manager_69759.png"

function NotFound(){
    return(
        <div className="utility-page-wrap">
            <div className="utility-page-content">
                <img src={Img} loading="lazy" width="52" alt="" className="company-logo"/>
                <h2>No Access</h2>
                <div>You don't have access to this page</div>
            </div>
        </div>
    );
}

export default NotFound;