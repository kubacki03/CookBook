
import { useState } from "react";
import axios from "axios";
import LoginForm from "./LoginForm";
import { useNavigate } from "react-router-dom";
import React from "react";


const LoginPage: React.FC = () => {
    const [serverError, setServerError] = useState<string | undefined>(undefined);
   const navigate = useNavigate(); // u¿yj hooka

    const handleLogin = async (data: { email: string; password: string }) => {
        try {
            const response = await axios.post("http://localhost:5119/Login/login", data);

            console.log("Zalogowano pomyœlnie:", response.data);

            localStorage.setItem("token", response.data.token);

          navigate("/dashboard"); // <-- przekierowanie po udanym logowaniu

        } catch (error: any) {
            if (error.response) {
                setServerError(error.response.data.message || "B³¹d logowania");
            } else {
                setServerError("Nie uda³o siê po³¹czyæ z serwerem.");
            }
        }
    };

    return <LoginForm onLogin={handleLogin} serverError={serverError} />;
};

export default LoginPage;
