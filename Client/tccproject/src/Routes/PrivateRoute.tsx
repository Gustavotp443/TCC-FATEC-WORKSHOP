import {Outlet, Navigate} from "react-router-dom";
import { isAuthenticated } from "../services/auth";

const PrivateRoute = (  ) => {
  return isAuthenticated()? <Outlet/> : <Navigate to="/signin"/>;
};

export default PrivateRoute;
