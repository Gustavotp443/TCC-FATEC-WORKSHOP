import React from "react";
import {useNavigate} from "react-router-dom";
const Home = () => {
  const navigate = useNavigate();


  return (
    <div>
      <h2>Home</h2>
      <button onClick={e=>navigate("/signin")}>Sing In</button>
      <button onClick={e=>navigate("/signup")}>Sign Up</button>
    </div>
  );
};

export default Home;
