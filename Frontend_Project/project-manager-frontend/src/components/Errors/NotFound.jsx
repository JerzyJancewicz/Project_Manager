import React from "react";
import Img from "../../styles/images/project-manager_69759.png"

function NotFound(){
    return(
        <div className="utility-page-wrap">
            <div className="utility-page-content">
                <img src={Img} loading="lazy" width="52" alt="" className="company-logo"/>
                <h2>Page Not Found</h2>
                <div>The page you are looking for doesn't exists or has been moved</div>
            </div>
        </div>
    );
}

export default NotFound;