import React, { useState } from 'react';
import { FaGoogle, FaFacebook, FaApple } from 'react-icons/fa';
import './SpotifySignup.css';

const SpotifySignup = () => {
  const [email, setEmail] = useState('');

  const handleSubmit = (e) => {
    e.preventDefault();
    // Handle form submission
  };

  return (
    <div className="spotify-signup">
      <div className="spotify-logo">
        {/* Add Spotify logo here */}
      </div>
      <h1>Sign up to start listening</h1>
      <form onSubmit={handleSubmit}>
        <div className="form-group">
          <label htmlFor="email">Email address</label>
          <input
            type="email"
            id="email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            placeholder="name@domain.com"
          />
        </div>
        <div className="use-phone">
          <a href="#">Use phone number instead.</a>
        </div>
        <button type="submit" className="btn-next">Next</button>
      </form>
      <div className="divider">or</div>
      <div className="social-signup">
        <button className="btn-social google">
          <FaGoogle /> Sign up with Google
        </button>
        <button className="btn-social facebook">
          <FaFacebook /> Sign up with Facebook
        </button>
        <button className="btn-social apple">
          <FaApple /> Sign up with Apple
        </button>
      </div>
      <div className="login-link">
        Already have an account? <a href="#">Log in here.</a>
      </div>
      <div className="terms">
        This site is protected by reCAPTCHA and the Google
        <a href="#">Privacy Policy</a> and <a href="#">Terms of Service</a> apply.
      </div>
    </div>
  );
};

export default SpotifySignup;