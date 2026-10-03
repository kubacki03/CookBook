import { useState } from "react";
import axios from "axios";
import { useNavigate } from "react-router-dom";
import React from "react";
import RegisterForm from "./RegisterForm";
 
const RegisterPage: React.FC = () => {
    const [serverError, setServerError] = useState<string | undefined>(undefined);
    const navigate = useNavigate();

    const handleRegister = async (data: { email: string; password: string }) => {
        setServerError(undefined);
        try {
            await axios.post("/Register", { username: data.email, password: data.password });
            await axios.post("/Login/login", { email: data.email, password: data.password });

            navigate("/dashboard");
        } catch (error: any) {
            if (error.response) {
                const body = error.response.data;
                const validationError = body?.errors ? Object.values(body.errors).flat()[0] : undefined;
                setServerError(body?.message || (validationError as string) || "Błąd rejestracji");
            } else {
                setServerError("Nie udało się połączyć z serwerem.");
            }
        }
    };

    return <RegisterForm onRegister={handleRegister} serverError={serverError} />;
};

export default RegisterPage;
