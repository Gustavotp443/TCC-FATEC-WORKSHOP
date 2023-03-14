import React from "react";

const ServicesCard = ({props,param}:any) => {


  const handleServiceClick=()=>{
    console.log(props);
  };

  return (
    <div onClick={(e:any)=> handleServiceClick()} style={{padding:"20px", backgroundColor:"yellow"}}>ServicesCard</div>
  );
};

export default ServicesCard;
