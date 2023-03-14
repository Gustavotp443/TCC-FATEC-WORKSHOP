import React from "react";
import DeleteConfirmation from "./Workshop/DeleteConfirmation";
import ModalButton from "./Modal/ModalButton";
import UpdateForm from "./Workshop/UpdateForm";
import {useNavigate} from "react-router-dom";
const WokshopCard = ({props:Props , keyValue ,counter ,onCounterChange,userId , token}:any) => {

  const navigate = useNavigate();

  const localDate = (date:Date) =>{
    return new Date(date).toLocaleString();
  };

  const handleClick =()=>{
    navigate("/subworkshops/",{ state: { param:{keyValue}, props:{Props} } });
  };


  return (
    <div  style={{marginBottom:"20px",padding:"20px",backgroundColor:"gray",width:"300px"}}>
      <h2>{Props.name}</h2>
      {Props.email ? <p>email: {Props.email}</p> : null}
      {Props.description ? <p>description: {Props.description}</p> : null}
      {Props.createdAt ? <p>criado em: {localDate(Props.createdAt)}</p>: null}
      {Props.updatedAt ? <p>atualizado em: {localDate(Props.updatedAt)}</p>:null}
      <div>
        <ModalButton text="Atualizar">
          <UpdateForm token={token} props={Props} counter={counter} onCounterChange={onCounterChange} userId={userId} keyValue={keyValue}></UpdateForm>
        </ModalButton>
        <ModalButton text="Deletar">
          <DeleteConfirmation counter={counter} token={token} onCounterChange={onCounterChange} userId={userId} keyValue={keyValue} text="Tem certeza que deseja deletar esse Workshop ?"></DeleteConfirmation>
        </ModalButton>
        <button onClick={e=> handleClick()}>ir</button>
      </div>
    </div>
  );
};

export default WokshopCard;
