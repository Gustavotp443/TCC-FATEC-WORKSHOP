import jwtDecode from "jwt-decode";

export const isAuthenticated = () =>{

  interface JwtPayload{
    sub:string;
    exp:number;
  }

  const token = sessionStorage.getItem("token");
  if(!token) return false;

  try{
    const decodedToken:JwtPayload = jwtDecode(token);
    const now = Date.now().valueOf()/1000;  //seconds
    if(decodedToken.exp < now){
      return false;
    }
  }catch(err){
    return false;
  }
  return true;
};
