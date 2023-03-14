import React from "react";
import Modal from "./Modal";

const ModalButton = ({text:Text, children}:any) => {
  const [showModal, setShowModal] = React.useState<boolean>(false);

  const handleShowModal = () => {
    setShowModal(true);
  };

  const handleCloseModal = () => {
    setShowModal(false);
  };
  return (
    <>
      <button onClick={handleShowModal}>{Text}</button>
      <Modal show={showModal} handleClose={handleCloseModal}>
        {children}
      </Modal>
    </>
  );
};

export default ModalButton;
