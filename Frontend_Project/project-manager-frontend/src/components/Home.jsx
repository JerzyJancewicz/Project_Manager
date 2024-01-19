import React from "react";
import Introduction from "./HomeComponents/Introduction";
import CssComponent1 from "./HomeComponents/CssComponent1";
import About from "./HomeComponents/About";
import CssComponent2 from "./HomeComponents/CssComponent2";
import Footer from "./HomeComponents/Footer";
import Login from "./UserCmp/Login";
import Register from "./UserCmp/Register";

function Home(){
    return(
        <div>
            <Login/>
            <Register/>
            <Introduction/>
            <CssComponent1/>
            <About/>
            <CssComponent2/>
            <Footer/>
        </div>
    );
}

export default Home;