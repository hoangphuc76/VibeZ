import React from 'react';
import { Outlet, Link } from 'react-router-dom';
import Header from './Header';
import Footer from './Footer';

const ArtistDashboard = () => {
  return (
    <div className="artist-dashboard">
      <Header />
      <div className="content">
        <Outlet />
      </div>
      <Footer />
    </div>
  );
};

export default ArtistDashboard;
