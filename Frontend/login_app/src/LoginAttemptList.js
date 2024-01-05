import React, { useState } from "react";
import "./LoginAttemptList.css";

const LoginAttempt = (props) => <li {...props}>{props.children}</li>;

const LoginAttemptList = ({ attempts }) => {
  const [filter, setFilter] = useState('');

  const handleFilterChange = (event) => {
    setFilter(event.target.value);
  };

  const filteredAttempts = attempts.filter(attempt => attempt.login.includes(filter));

  return (
    <div className="Attempt-List-Main">
      <p>Recent activity</p>
      <input type="input" placeholder="Filter..." value={filter} onChange={handleFilterChange} />
      <ul className="Attempt-List">
        {filteredAttempts.map((attempt, index) => (
          <LoginAttempt key={index}>
            {attempt.login} attempted to log in.
          </LoginAttempt>
        ))}
      </ul>
    </div>
  );
};

export default LoginAttemptList;