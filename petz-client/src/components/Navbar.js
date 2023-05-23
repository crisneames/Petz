import './Navbar.css';
import petz from '../assets/petz.png';
import { Link } from 'react-router-dom';
import React from 'react';

export const Navbar = () => {
  return (
    <div className="navbar">
      <ul>
        <li className="logo">
          <img src={petz} alt="Petz logo" />
        </li>
        <li>
          <Link to="/login">Login</Link>
        </li>

        <li>
          <Link to="/signup">Signup</Link>
        </li>
        <li>
          <button className="btn">Logout</button>
        </li>
      </ul>
    </div>
  );
};
export default Navbar;
