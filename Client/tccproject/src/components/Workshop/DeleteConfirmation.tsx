import React from "react";
import api from "../../services/api";

const DeleteConfirmation = ({text, keyValue, counter ,onCounterChange, userId, token}:any) => {


  const handleDelete=async ()=>{
    console.log(keyValue);
    console.log(userId);
    await api.delete(`user/${userId}/workshop/${keyValue}`,{
      headers:{
        Authorization:`Bearer ${token}`
      },
    }).then((response)=>{
      console.log(response);
      onCounterChange(counter+1);
    });
  };

  return (
    <div>
      <h2>{text}</h2>
      <button onClick={e=> handleDelete()}>Deletar</button>
    </div>
  );
};

export default DeleteConfirmation;
