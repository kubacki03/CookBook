import React, { useState } from "react";
import axios from "axios";
import LoginForm from "./LoginForm";

const LoginPage: React.FC = () => {
    const [serverError, setServerError] = useState<string | undefined>(undefined);

    const handleLogin = async (data: { email: string; password: string }) => {
        try {
            const response = await axios.post("http://localhost:8080/login", data);

            console.log("Zalogowano pomyœlnie:", response.data);
            
            // localStorage.setItem("token", response.data.token);
            //przekieruj do home
    
        // eslint-disable-next-line @typescript-eslint/no-explicit-any
        } catch (error: any) {
            if (error.response) {
                // jesli odpowiedz bedzie jakimsbledem 
                setServerError(error.response.data.message || "B³¹d logowania");
            } else {

                //jesli bedzie timeout
                setServerError("Nie uda³o siê po³¹czyæ z serwerem.");
            }
        }
    };

    return <LoginForm onLogin={handleLogin} serverError={serverError} />;
};

export default LoginPage;
