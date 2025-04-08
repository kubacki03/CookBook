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
    email: yup.string().email("Nieprawid³owy email").required("Email jest wymagany"),
    password: yup.string().min(6, "Has³o musi mieæ co najmniej 6 znaków").required("Has³o jest wymagane"),
});

const LoginForm: React.FC<LoginFormProps> = ({ onLogin, serverError }) => {
    const {
        register,
        handleSubmit,
        formState: { errors },
    } = useForm<LoginFormInputs>({
        resolver: yupResolver(schema),
    });

    const onSubmit = (data: LoginFormInputs) => {
        onLogin(data);
    };

    return (
        <form onSubmit={handleSubmit(onSubmit)} className="max-w-sm mx-auto p-4 border rounded shadow bg-white">
            <h2 className="text-xl font-bold mb-4">Zaloguj siê</h2>

            <div className="mb-4">
                <label htmlFor="email" className="block text-sm font-medium text-gray-700">Email</label>
                <input
                    id="email"
                    type="email"
                    {...register("email")}
                    className={`mt-1 block w-full border px-3 py-2 rounded ${errors.email ? "border-red-500" : "border-gray-300"
                        }`}
                />
                {errors.email && <p className="text-red-500 text-sm mt-1">{errors.email.message}</p>}
            </div>

            <div className="mb-4">
                <label htmlFor="password" className="block text-sm font-medium text-gray-700">Has³o</label>
                <input
                    id="password"
                    type="password"
                    {...register("password")}
                    className={`mt-1 block w-full border px-3 py-2 rounded ${errors.password ? "border-red-500" : "border-gray-300"
                        }`}
                />
                {errors.password && <p className="text-red-500 text-sm mt-1">{errors.password.message}</p>}
            </div>

            {serverError && <p className="text-red-600 text-sm mb-3">{serverError}</p>}

            <button
                type="submit"
                className="w-full bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700 transition"
            >
                Zaloguj siê
            </button>
        </form>
    );
};

export default LoginForm;
