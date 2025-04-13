
import { useNavigate } from 'react-router-dom';

function Logout() {
    const navigate = useNavigate();

    const handleLogout = () => {
        
        localStorage.removeItem('token');
        
        navigate('/');
    };

    return (
        <button onClick={handleLogout} className=" text-lg font-bold">Wyloguj</button>
    );
}

export default Logout;
