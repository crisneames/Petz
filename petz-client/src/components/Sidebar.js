import './Sidebar.css';
import React from 'react';
// import dashboard from '../assets/dashboard.png';
// import add_icon from '../assets/add.jpeg';
import { NavLink } from 'react-router-dom';
import { useAuthContext } from '../hooks/useAuthContext';

const Sidebar = () => {
  const { user } = useAuthContext();

  return (
    <div className="sidebar">
      <div className="sidebar-content">
        <div className="user">
          {/* username here */}
          <p>{user.displayName}</p>
        </div>
        <nav className="links">
          <ul>
            <li>
              <NavLink to="/">
                {/* <img src={dashboard} alt="dashboard icon" /> */}
                <span>Dashboard</span>
              </NavLink>
            </li>
            <li>
              <NavLink to="/create">
                {/* <img src={add_icon} alt="add icon" /> */}
                <span>New Post</span>
              </NavLink>
            </li>
          </ul>
        </nav>
      </div>
    </div>
  );
};

export default Sidebar;
