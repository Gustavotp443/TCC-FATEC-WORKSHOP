import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import * as P from "../pages/Index";
import PrivateRoute from "./PrivateRoute";



const Routering = () => {
  return (
    <Router>
        <Routes>
            <Route path="/" element={<P.Home/>}/>
            <Route path="/signup" element={<P.SingUp/>}/>
            <Route path="/signin" element={<P.SingIn/>}/>
            <Route path="/workshops" element={<PrivateRoute/>}>
              <Route path="/workshops" element={<P.Workshops/>}/>
            </Route>
            <Route path="/subworkshops" element={<PrivateRoute/>}>
              <Route path="/subworkshops" element={<P.SubWorkshops/>}/>
            </Route>
            <Route path="*" element={<P.Error/>}/>
        </Routes>
    </Router>
  );
  };

export default Routering;

