
import axios from 'axios';
import { useNavigate } from 'react-router-dom';

function Logout() {
    const navigate = useNavigate();

    const handleLogout = async () => {
        try { 
            await axios.post('/Login/logout');
        } finally {
            navigate('/');
        }
    };

    return (
        <button onClick={handleLogout} className=" text-lg font-bold">Wyloguj</button>
    );
}

export default Logout;
