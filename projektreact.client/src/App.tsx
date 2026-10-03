import React, { ReactNode, useEffect, useState } from 'react';
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import axios from 'axios';
import LoginPage from './LoginPage';
import RegisterPage from './RegisterPage';
import Dashboard from './Dashboard';
import UserRecipes from './UserRecipes';

interface PrivateRouteProps {
    element: ReactNode;
}

const PrivateRoute: React.FC<PrivateRouteProps> = ({ element }) => { 
    const [status, setStatus] = useState<'loading' | 'ok' | 'unauthorized'>('loading');

    useEffect(() => {
        axios.get('/Login/me')
            .then(() => setStatus('ok'))
            .catch(() => setStatus('unauthorized'));
    }, []);

    if (status === 'loading') return null;
    if (status === 'unauthorized') return <Navigate to="/" />;

    return <>{element}</>;
};

const App = () => {
    return (
        <Router>
            <Routes>
              
                <Route path="/" element={<LoginPage />} />

                <Route path="/register" element={<RegisterPage />} />
             
                <Route path="/dashboard" element={<PrivateRoute element={<Dashboard />} />} />

                <Route path="/recipes" element={<PrivateRoute element={<UserRecipes />} />} />
            </Routes>
        </Router>
    );
};

export default App;
