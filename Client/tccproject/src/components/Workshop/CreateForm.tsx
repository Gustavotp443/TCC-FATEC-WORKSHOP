import React from "react";
import api from "../../services/api";

interface props{
  userId:number | undefined;
  token: string | null | undefined;
  counter:any;
  onCounterChange:any;
}

const CreateForm = ({userId, token , counter  ,onCounterChange}:props) => {

  interface FormValues {
    name:string;
    email:string;
    description:string;
}

interface FormValuesErrors {
    [key:string]:string;
}

const initialErrors: FormValuesErrors = {};

const [formValue, setFormValue] = React.useState<FormValues>({
    name:"",
    email:"",
    description:"",
});

const [errors, setErrors] = React.useState<FormValuesErrors>(initialErrors);


const handleFormChange = (e:React.ChangeEvent<HTMLInputElement>) =>{
    const {name, value} = e.target;
    setFormValue({...formValue, [name]:value});
};

const handleFormSubmit = async (e: React.FormEvent<HTMLFormElement>, formValue: FormValues) => {
    e.preventDefault();

    console.log(formValue);
    await api.post(`user/${userId}/workshop`, formValue , {
      headers:{
        Authorization:`Bearer ${token}`
      },
    }).then(
        (response) =>  {
          console.log(response.data);
          onCounterChange(counter+1);
          setFormValue({
            name:"",
            email:"",
            description:"",
          });
        }
    ).catch(error => {
        if (error.response) {
            // A resposta da API foi recebida, mas está com status de erro
            console.log(error.response.data); // Exibe a mensagem de erro na console do navegador
            const { errors } = error.response.data;
            const errorObject: FormValuesErrors = Object.keys(errors).reduce((acc, curr) => {
                acc[curr.toLowerCase()] = errors[curr];
                return acc;
            }, {} as FormValuesErrors);
            setErrors(errorObject);
            console.log(errors);
            console.log(errors.Username);
        } else if (error.request) {
            console.log(error.request);
        } else {
            // Ocorreu um erro ao configurar a solicitação
            console.log("Erro", error.message);
        }
        console.log(error.config);
    });
};


  return (
      <form onSubmit={(e: React.FormEvent<HTMLFormElement>)=> handleFormSubmit(e, formValue)}>
        <label htmlFor="name">Name: </label>
        <div>
          <input type="text" name="name" value={formValue.name} onChange={e=> handleFormChange(e)}/>
          {errors.name ? (Array.isArray(errors.name) ? (errors.name.map((error, index) => (
                <div key={index} className="error">{error}</div>))
            ) : (
                <div className="error">{errors.name}</div>
            )
            ) : null}
        </div>
        <label htmlFor="email">Email: </label>
        <div>
          <input type="text" name="email" value={formValue.email} onChange={e=> handleFormChange(e)}/>
          {errors.email ? (Array.isArray(errors.email) ? (errors.email.map((error, index) => (
                <div key={index} className="error">{error}</div>))
            ) : (
                <div className="error">{errors.email}</div>
            )
            ) : null}
        </div>
        <label htmlFor="description">Description: </label>
        <div>
          <input type="" name="description" value={formValue.description} onChange={e=> handleFormChange(e)}/>
          {errors.description ? (Array.isArray(errors.description) ? (errors.description.map((error, index) => (
                <div key={index} className="error">{error}</div>))
            ) : (
                <div className="error">{errors.description}</div>
            )
            ) : null}
        </div>
        <input type="submit" value="Adicionar" />
      </form>
  );
};

export default CreateForm;
