import './Signup.css';
import React from 'react';
import { useState } from 'react';
import { useSignup } from '../../hooks/useSignup';

const Signup = () => {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [displayName, setDisplayName] = useState('');
  const [thumbnail, setThumbnail] = useState(null);
  const { signup, isPending, error } = useSignup();

  const handleSubmit = (e) => {
    e.preventDefault();
    signup(email, password, displayName);
  };

  return (
    <form className="auth-form" onSubmit={handleSubmit}>
      <h2>Sign up</h2>
      <label>
        <span>email:</span>

        <input
          required
          type="email"
          onChange={(e) => setEmail(e.target.value)}
          value={email}
        />
      </label>

      <label>
        <span>password:</span>

        <input
          required
          type="password"
          onChange={(e) => setPassword(e.target.value)}
          value={password}
        />
      </label>

      <label>
        <span>display name:</span>

        <input
          required
          type="text"
          onChange={(e) => setDisplayName(e.target.value)}
          value={displayName}
        />
      </label>

      {/* <label>
        <span>profile thumbnail:</span>

        <input
          required
          type="file"
          // onChange={(e) => setEmail(e.target.value)}
          // value={email}
        />
      </label> */}
      <button className="btn">Sign up</button>
      {error && <div className="error">{error}</div>}
    </form>
  );
};
export default Signup;
