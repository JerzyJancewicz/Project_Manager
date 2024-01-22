import { Navigate } from 'react-router-dom';
import React from 'react';

function ProtectedRoute({ children }) {
    const storedToken = sessionStorage.getItem('token');

    if (storedToken === undefined || storedToken === null) {
        return <Navigate to="/no-access" />;
    }
    return children;
}

export default ProtectedRoute;