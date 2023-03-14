import React from "react";

const Modal = ({handleClose, show, children}:any) => {
  const showHideClassName = show ? "modal display-block" : "modal display-none";
  const showId = show? "fade" : "";
  return (
    <div className={showHideClassName} id={showId}>
      <section className="modal-main">
        {children}
        <button onClick={handleClose}>Fechar</button>
      </section>
    </div>
  );
};

export default Modal;
