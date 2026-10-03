import React from "react";
import { useForm } from "react-hook-form";
import { Link } from "react-router-dom";
import * as yup from "yup";
import { yupResolver } from "@hookform/resolvers/yup";

interface RegisterFormInputs {
    email: string;
    password: string;
    confirmPassword: string;
}

interface RegisterFormProps {
    onRegister: (data: RegisterFormInputs) => void;
    serverError?: string;
}

const schema = yup.object().shape({
    email: yup.string().email("Nieprawidłowy email").required("Email jest wymagany"),
    password: yup.string().min(6, "Hasło musi mieć co najmniej 6 znaków").required("Hasło jest wymagane"),
    confirmPassword: yup.string()
        .oneOf([yup.ref("password")], "Hasła muszą być takie same")
        .required("Powtórz hasło"),
});

const RegisterForm: React.FC<RegisterFormProps> = ({ onRegister, serverError }) => {
    const {
        register,
        handleSubmit,
        formState: { errors, isSubmitting }
    } = useForm<RegisterFormInputs>({
        resolver: yupResolver(schema),
    });

    return (
        <div className="mt-3">
            <form onSubmit={handleSubmit(onRegister)} className="mx-auto max-w-sm rounded border bg-white p-4 shadow">
                <h2 className="mb-4 text-xl font-bold">Zarejestruj się</h2>

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

                <div className="mb-4">
                    <label htmlFor="confirmPassword" className="block text-sm font-medium text-gray-700">Powtórz hasło</label>
                    <input
                        id="confirmPassword"
                        type="password"
                        {...register("confirmPassword")}
                        className={`mt-1 block w-full border px-3 py-2 rounded ${errors.confirmPassword ? "border-red-500" : "border-gray-300"}`}
                    />
                    {errors.confirmPassword && <p className="mt-1 text-sm text-red-500">{errors.confirmPassword.message}</p>}
                </div>

                {serverError && <p className="mb-3 text-sm text-red-600">{serverError}</p>}

                <button
                    type="submit"
                    disabled={isSubmitting}
                    className="w-full rounded bg-blue-600 px-4 py-2 text-white transition hover:bg-blue-700 disabled:opacity-50"
                >
                    Zarejestruj się
                </button>

                <p className="mt-4 text-center text-sm text-gray-600">
                    Masz już konto? <Link to="/" className="text-blue-600 hover:underline">Zaloguj się</Link>
                </p>
            </form>
        </div>
    );
};

export default RegisterForm;
