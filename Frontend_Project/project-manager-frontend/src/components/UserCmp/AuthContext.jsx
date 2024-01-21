import { createContext } from 'react';

export const AuthContext = createContext(undefined);

// export const AuthProvider = ({ children }) => {
//     const [token, setToken] = useState(sessionStorage.getItem('token'));
    
//     return (
//         <AuthContext.Provider value={{ token, setToken }}>
//             {console.log(token)}
//             {children}
//         </AuthContext.Provider>
//     );
// };
