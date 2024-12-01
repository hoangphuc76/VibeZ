import React, { useState, useContext } from 'react';
import { useNavigate } from 'react-router-dom';
import './SignUpForm.css';
import img from '../../assets/register-background.png';
import googleicon from '../../assets/google-icon.svg';
import { LoginContext } from "../../context/LoginContext";
import { assets } from "../../assets/assets";

const SignUpForm = () => {
  const { signUp } = useContext(LoginContext);
  const [name, setName] = useState('');
  const [username, setUsername] = useState('');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');

  const [errorMessage, setErrorMessage] = useState('');

  const navigate = useNavigate();

  const handleSubmit = async (event) => {
    event.preventDefault();
    setErrorMessage('')

    try {
      await signUp(name, username, email, password);

      console.log('Registration successful. Redirecting to login...');
      navigate('/login');
    } catch (error) {
      console.error("Registration error:", error.message || error);
      setErrorMessage(error.message || 'Registration failed. Please try again.');
    }
  };

  return (
    <div className="signup-signup-container">
      <div className="signup-signup-form">
        <div className="mb-8">
          <img className="w-16 h-16 mx-auto rounded-full" src={assets.logo} alt="" />
        </div>
        <h2>Create your account for free and start listening</h2>
        <form onSubmit={handleSubmit}>
          {errorMessage && <p className="error-message">{errorMessage}</p>}

          <input
            type="text"
            placeholder="Full Name"
            value={name}
            onChange={(e) => setName(e.target.value)}
            required
          />
          <input
            type="text"
            placeholder="Username"
            value={username}
            onChange={(e) => setUsername(e.target.value)}
            required
          />
          <input
            type="email"
            placeholder="Email address"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            required
          />
          <input
            type="password"
            placeholder="Password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            required
          />
          <button type="submit">Sign up</button>
          <div className="signup-signup-options">
            <span>or</span>
            <button className="signup-google-signup" type="button">
              <img src={googleicon} alt="Google icon" />
              Continue with Google
            </button>
          </div>
        </form>
      </div>
      <div className="signup-advertisement">
        <img src={img} alt="NF" className="signup-artist-image" />
      </div>
    </div>
  );
};

export default SignUpForm;