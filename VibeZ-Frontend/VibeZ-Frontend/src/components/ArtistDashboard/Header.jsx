import React from 'react';
import {Link } from 'react-router-dom';
import './HeaderFooter.css';

const Header = () => {
    return (
      <header>
        <h1>VibeZ</h1>
        <nav>
        <Link to="/artistdashboard/home">Home</Link>
        <Link to="/artistdashboard/music">Music</Link>
        <Link to="/artistdashboard/profile">Profile</Link>
        </nav>
      </header>
    );
}
export default Header;