import React, { ReactNode } from 'react';
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import LoginPage from './LoginPage';
import Dashboard from './Dashboard';
import { jwtDecode } from 'jwt-decode';

interface JwtPayload {
    exp: number;
}
interface PrivateRouteProps {
    element: ReactNode;
}

const PrivateRoute: React.FC<PrivateRouteProps> = ({ element }) => {
    const token = localStorage.getItem('token');

    if (!token) return <Navigate to="/" />;

    try {
        const decoded = jwtDecode<JwtPayload>(token);
        const currentTime = Date.now() / 1000;

        if (decoded.exp < currentTime) {
            localStorage.removeItem('token');
            return <Navigate to="/" />;
        }

        return <>{element}</>;

    } catch (error) {
     
        localStorage.removeItem('token');
        return <Navigate to="/" />;
    }
};

const App = () => {
    return (
        <Router>
            <Routes>
              
                <Route path="/" element={<LoginPage />} />
             
                <Route path="/dashboard" element={<PrivateRoute element={<Dashboard />} />} />
            </Routes>
        </Router>
    );
};

export default App;
