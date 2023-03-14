import React from "react";

const ProductsCard = ({props,param}:any) => {

  const handleProductClick=()=>{
    console.log(param);
  };

  return (
    <div onClick={(e:any)=>handleProductClick()} style={{padding:"20px", backgroundColor:"green"}}>ProductsCard</div>
  );
};

export default ProductsCard;
