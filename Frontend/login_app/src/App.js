import React, { useState } from "react";
import './App.css';
import LoginForm from './LoginForm';
import LoginAttemptList from './LoginAttemptList';

const App = () => {
  const [loginAttempts, setLoginAttempts] = useState([]);

  const handleLoginAttempt = ({ login, password }) => {
    // Add the login attempt to the state
    setLoginAttempts(prevAttempts => [...prevAttempts, { login }]);
  };

  return (
    <div className="App">
      <LoginForm onSubmit={handleLoginAttempt} />
      <LoginAttemptList attempts={loginAttempts} />
    </div>
  );
};

export default App;