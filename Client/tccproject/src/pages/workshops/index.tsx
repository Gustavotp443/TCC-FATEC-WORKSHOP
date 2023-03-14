import React from "react";
import api from "../../services/api";
import jwtDecode from "jwt-decode";
import WorkshopCard from "../../components/WorkshopCard";
import ModalButton from "../../components/Modal/ModalButton";
import CreateForm from "../../components/Workshop/CreateForm";


interface JwtPayload{
  userId:number;
}

interface UserGet{
  username: string,
  email: string,
  createdAt: Date,
  updatedAt: Date
}

const Workshops = () => {

  const [userId, setUserId] = React.useState<number>();
  const [userData, setUserData] = React.useState<UserGet>();
  const [workshops, setWorkshops] = React.useState<any[]>([]);
  const [token, setToken] = React.useState<string | null>();
  const [counter, setCounter] = React.useState(0);


  const getRequisition=async (id:number,token:string)=>{
    await api.get(`user/${id}`,{
      headers:{
        Authorization:`Bearer ${token}`
      },
    })
    .then((response)=>{
      const {data} = response;
      setUserData(data);
    });

    await api.get(`user/${id}/workshop`,{
      headers:{
        Authorization:`Bearer ${token}`
      },
    })
    .then((response)=>{
      const data=response.data.$values;
      if (Array.isArray(data)) {
        setWorkshops(data);
      } else if (data) {
        setWorkshops([data]);
      } else {
        setWorkshops([]);
      }
    });
  };

  const handleCounterChange = (newCounter:number)=>{
    setCounter(newCounter);
  };


  React.useEffect(()=>{
    setToken(sessionStorage.getItem("token"));
    if(token){
      const tokenDecoded:JwtPayload= jwtDecode(token);
      setUserId(tokenDecoded.userId);
    }
    if(userId && token){
      getRequisition(userId,token);
    }
  },[counter,token,userId]);



  return (
    <div>
    <h2>Bem vinda de novo {userData?.username}</h2>
    <h2>WORKSHOPS</h2>
    {workshops.length > 0 ? (
      workshops.map((workshop) => (
        <WorkshopCard key={workshop.id} props={workshop} keyValue={workshop.id}
        counter={counter} onCounterChange={handleCounterChange} userId={userId} token={token}/>
      ))
    ) : (
      <p>Crie um novo Workshop!</p>
    )}
    <div>
      <ModalButton text="Adicionar">
        <CreateForm userId={userId} token={token} counter={counter} onCounterChange={handleCounterChange}/>
      </ModalButton>
    </div>
    </div>
  );
  };

export default Workshops;
