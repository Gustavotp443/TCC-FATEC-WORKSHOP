import React from 'react'
import api from '../../services/api';

const SingUp = () => {

    interface FormValues {
        username:string;
        email:string;
        password:string;
    }

    interface FormValuesErrors {
        [key:string]:string;
    }

    const initialErrors: FormValuesErrors = {};

    const [formValue, setFormValue] = React.useState<FormValues>({
        username:'',
        email:'',
        password:'',
    });

    const [errors, setErrors] = React.useState<FormValuesErrors>(initialErrors);
    

    const handleFormChange = (e:React.ChangeEvent<HTMLInputElement>) =>{
        const {name, value} = e.target;
        setFormValue({...formValue, [name]:value});
    };

    const handleFormSubmit = async (e: React.FormEvent<HTMLFormElement>, formValue: FormValues) => {
        e.preventDefault();

        console.log(formValue)
        await api.post("user", formValue).then(
            (response) =>   console.log(response.data)
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
                console.log('Erro', error.message);
            }
            console.log(error.config);
        })
    }

  return (
    <div>
        <h2>SignUp</h2>
        <form onSubmit={(e: React.FormEvent<HTMLFormElement>)=> handleFormSubmit(e, formValue)}>
            <label htmlFor="username">Username</label>
            <div>
            <input name="username" type="text" value={formValue.username} onChange={e => handleFormChange(e)}/>
            {errors.username ? (Array.isArray(errors.username) ? (errors.username.map((error, index) => (
                <div key={index} className="error">{error}</div>))
            ) : (
                <div className="error">{errors.username}</div>
            )
            ) : null}
            </div>
            <label htmlFor="email">Email</label>
            <div>     
            <input name="email" type="text" value={formValue.email} onChange={e => handleFormChange(e)}/>
            {errors.email ? (Array.isArray(errors.email) ? (errors.email.map((error, index) => (
                <div key={index} className="error">{error}</div>))
            ) : (
                <div className="error">{errors.email}</div>
            )
            ) : null}
            </div>
            <label htmlFor="password">Password</label>
            <div>     
            <input name="password" type="password"  value={formValue.password} onChange={e => handleFormChange(e)}/>
            {errors.password ? (Array.isArray(errors.password) ? (errors.password.map((error, index) => (
                <div key={index} className="error">{error}</div>))
            ) : (
                <div className="error">{errors.password}</div>
            )
            ) : null}
            </div>
            <button type="submit">Sing Up</button>
        </form>
    </div>
  )
}

export default SingUp