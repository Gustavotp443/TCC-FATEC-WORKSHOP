import React from "react";
import ProductsCard from "../../../components/Products/ProductsCard";
import ServicesCard from "../../../components/Services/ServicesCard";
import {useLocation} from "react-router-dom";
const SubWorkshop = () => {
  const location = useLocation();
  const param = location.state?.param;
  const props = location.state?.props;
  console.log(param);
  console.log(props);


  return (
    <div>
      <h2>Workshop: {props.Props.name}</h2>
      <ProductsCard props={props} param={param}/>
      <ServicesCard props={props} param={param} />
    </div>
  );
};

export default SubWorkshop;
