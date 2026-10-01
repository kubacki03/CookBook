import React from "react";
import { useForm } from "react-hook-form";
import * as yup from "yup";
import { yupResolver } from "@hookform/resolvers/yup";
 
interface LoginFormInputs {
    email: string;
    password: string;
}
 
interface LoginFormProps {
    onLogin: (data: LoginFormInputs) => void; 
    serverError?: string; 
}
 
const schema = yup.object().shape({
    email: yup.string().email("Nieprawidłowy email").required("Email jest wymagany"),
    password: yup.string().min(6, "Hasło musi mieć co najmniej 6 znaków").required("Hasło jest wymagane"),
});
 
const LoginForm: React.FC<LoginFormProps> = ({ onLogin, serverError }) => { 
    const {
        register, 
        handleSubmit,  
        formState: { errors } 
    } = useForm<LoginFormInputs>({
        resolver: yupResolver(schema), 
    });
     
    const onSubmit = (data: LoginFormInputs) => {
        onLogin(data); 
    };

    return (
        <div className="mt-3">
            <form onSubmit={handleSubmit(onSubmit)} className="mx-auto max-w-sm rounded border bg-white p-4 shadow">
                <h2 className="mb-4 text-xl font-bold">Zaloguj się</h2>
                 
                <div className="mb-4">
                    <label htmlFor="email" className="block text-sm font-medium text-gray-700">Email</label>
                    <input
                        id="email"
                        type="email"
                        {...register("email")} 
                        className={`mt-1 block w-full border px-3 py-2 rounded ${errors.email ? "border-red-500" : "border-gray-300"}`}
                    />
                    {errors.email && <p className="mt-1 text-sm text-red-500">{errors.email.message}</p>}
                </div>
                 
                <div className="mb-4">
                    <label htmlFor="password" className="block text-sm font-medium text-gray-700">Hasło</label>
                    <input
                        id="password"
                        type="password"
                        {...register("password")} 
                        className={`mt-1 block w-full border px-3 py-2 rounded ${errors.password ? "border-red-500" : "border-gray-300"}`}
                    />
                    {errors.password && <p className="mt-1 text-sm text-red-500">{errors.password.message}</p>}
                </div>
                 
                {serverError && <p className="mb-3 text-sm text-red-600">{serverError}</p>}
                 
                <button
                    type="submit"
                    className="w-full rounded bg-blue-600 px-4 py-2 text-white transition hover:bg-blue-700"
                >
                    Zaloguj się
                </button>
            </form>
        </div>
    );
};

export default LoginForm;
