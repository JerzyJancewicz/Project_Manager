import React from "react";
import Half_Magnet_Img from "../../styles/images/Half-Magnet.png"
import Project_Management from "../../styles/images/img-hero-project-management.jpg";
import Half_Moon from "../../styles/images/Half-Moon.png";

function About(){
    return(
        <section className="crappo-section">
            <div className="page-padding">
            <div className="crappo-container w-container">
                <div className="w-layout-grid about-why-crappo margin-bottom-xxhuge">
                <div id="w-node-eee50d31-98ec-ed90-7e4f-db525d4039c2-15d0c59e" className="about-left-content"><img src={Project_Management} loading="lazy" width="471" sizes="(max-width: 479px) 91vw, (max-width: 767px) 88vw, 471px" alt="" className="crappo-image"/></div>
                <div id="w-node-eee50d31-98ec-ed90-7e4f-db525d4039c4-15d0c59e" className="about-right-content">
                    <h2 className="crappo-title margin-bottom-xs">Why you should choose ManageIT</h2>
                    <p className="crappo-paragraph text-gray margin-bottom-sm">Experience the next generation management platform. No financial borders, extra fees, and fake reviews.</p>
                </div><img src={Half_Magnet_Img} alt="Background U Symbol" width="154" className="u-side-image"/>
                </div><img src="../../styles/images/.png" loading="lazy" width="924.5" sizes="100vw" alt="Background Side Bar " className="side-bar-crappo"/>
            </div>
            </div><img src={Half_Moon} alt="Back Ground D Symbol" width="185" className="d-side-image"/>
        </section>
    );
}

export default About;