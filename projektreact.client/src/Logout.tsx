import React from 'react';
import { useNavigate } from 'react-router-dom';

function Logout() {
    const navigate = useNavigate();

    const handleLogout = () => {
        
        localStorage.removeItem('token');
        
        navigate('/');
    };

    return (
        <button onClick={handleLogout}>Wyloguj</button>
    );
}

export default Logout;
