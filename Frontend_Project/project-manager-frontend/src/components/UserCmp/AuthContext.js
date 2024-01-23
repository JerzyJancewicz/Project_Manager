import React from 'react';

const AuthContext = React.createContext({
  isLoggedIn: false,
  login: () => {},
  logout: () => {},
  showLogin: () => {}
});

export default AuthContext;
