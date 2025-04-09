
import { useState } from "react";
import axios from "axios";
import LoginForm from "./LoginForm";
import { useNavigate } from "react-router-dom";
import React from "react";


const LoginPage: React.FC = () => {
    const [serverError, setServerError] = useState<string | undefined>(undefined);
   const navigate = useNavigate(); 

    const handleLogin = async (data: { email: string; password: string }) => {
        try {
            const response = await axios.post("http://localhost:5119/Login/login", data);

            console.log("Zalogowano pomyślnie:", response.data);

            localStorage.setItem("token", response.data.token);

          navigate("/dashboard"); 

        } catch (error: any) {
            if (error.response) {
                setServerError(error.response.data.message || "Błąd logowania");
            } else {
                setServerError("Nie udało się połączyć z serwerem.");
            }
        }
    };

    return <LoginForm onLogin={handleLogin} serverError={serverError} />;
};

export default LoginPage;
