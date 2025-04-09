import React, { ReactNode } from 'react';
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import LoginPage from './LoginPage';
import Dashboard from './Dashboard';


interface PrivateRouteProps {
    element: ReactNode;
}

const PrivateRoute: React.FC<PrivateRouteProps> = ({ element }) => {
    const token = localStorage.getItem('token');
    return token ? <>{element}</> : <Navigate to="/" />;
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
