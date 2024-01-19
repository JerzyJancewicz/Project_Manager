import React, {useState} from "react";
import Introduction from "./HomeComponents/Introduction";
import CssComponent1 from "./HomeComponents/CssComponent1";
import About from "./HomeComponents/About";
import CssComponent2 from "./HomeComponents/CssComponent2";
import Footer from "./HomeComponents/Footer";

function Home(){
    return(
        <div>
            <Introduction/>
            <CssComponent1/>
            <About/>
            <CssComponent2/>
            <Footer/>
        </div>
    );
}

export default Home;