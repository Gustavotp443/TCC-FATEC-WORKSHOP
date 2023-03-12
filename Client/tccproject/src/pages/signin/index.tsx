import React from "react";
import api from "../../services/api";
import {useNavigate} from "react-router-dom";

const Signin = () => {

  interface FormValues{
      email:string,
      password:string
  }

  interface FormValuesErrors {
      [key:string]:string;
  }

  const [formValues, setFormValues] = React.useState<FormValues>({
      email:"",
      password:""
  });

  const initialErrors: FormValuesErrors = {};
  const [errors, setErrors] = React.useState<FormValuesErrors>(initialErrors);
  const navigate = useNavigate();


  const handleFormChange = (e:React.ChangeEvent<HTMLInputElement>) =>{
    const {name, value} = e.target;
    setFormValues({...formValues, [name]:value});
  };

  const handleFormSubmit = async (e: React.FormEvent<HTMLFormElement>, formValue: FormValues) => {
    e.preventDefault();

    console.log(formValue);
    await api.post("v1/auth", formValue).then(
        (response) =>   {
          const {token} = response.data.token;
          sessionStorage.setItem("token", token);
          navigate("/workshops");
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
    <div>
        <h2>Sign In</h2>
        <form  onSubmit={(e: React.FormEvent<HTMLFormElement>)=> handleFormSubmit(e, formValues)}>
            <label htmlFor="email" >Email</label>
            <div>
                <input name="email" type="text" value={formValues.email} onChange={e => handleFormChange(e)}/>
                {errors.email ? (Array.isArray(errors.email) ? (errors.email.map((error, index) => (
                <div key={index} className="error">{error}</div>))
                ) : (
                <div className="error">{errors.email}</div>
                )
                ) : null}
            </div>
            <label htmlFor="password">Password</label>
            <div>
                <input name="password" type="text" value={formValues.password} onChange={e => handleFormChange(e)}/>
                {errors.password ? (Array.isArray(errors.password) ? (errors.password.map((error, index) => (
                <div key={index} className="error">{error}</div>))
                ) : (
                <div className="error">{errors.password}</div>
                )
            ) : null}
            </div>

            <button type="submit">Login</button>
        </form>
    </div>
  );
};

export default Signin;
