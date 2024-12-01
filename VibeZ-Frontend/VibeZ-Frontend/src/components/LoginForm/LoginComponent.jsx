import React, { useContext, useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import './LoginComponent.css'; // Corrected import
import { GoogleLogin } from '@react-oauth/google'; // Corrected import

// Importing images from the assets folder
import vibezIcon from "../../assets/vibez_logo.svg";
import showIcon from "../../assets/show-icon.svg";
import { LoginContext } from "../../context/LoginContext";
import { assets } from "../../assets/assets";

const LoginComponent = () => {
  const { login, isLoggedIn, user, googleLogin } = useContext(LoginContext);
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const navigate = useNavigate();

  const handleLogin = async () => {
    try {
      await login(username, password);
    } catch (error) {
      console.error("Login failed:", error.message);
      alert(error.message);
    }
  };

  useEffect(() => {
    console.log('Logged in:', isLoggedIn, 'User:', user);
    if (isLoggedIn) {
      navigate('/');
    }
  }, [isLoggedIn, user, navigate]);

  const responseGoogle = async (response) => {
    try {
      console.log(response);
      await googleLogin(response);
    } catch (error) {
      console.error("Login failed:", error.message);
      alert(error.message);
    }
  };
  
  return (
    <div className="login-page">
      <div>
          <div className="login-container">
            <div className="spotify-logo">
              <button>
                <img src={vibezIcon} alt="VibeZ" />
              </button>
            </div>
            <h1 className="login-title">Log in to VibeZ</h1>
            <div className="input-group">
              <input
                type="text"
                id="username"
                placeholder="Email or username"
                value={username}
                onChange={(e) => setUsername(e.target.value)}
              />
            </div>
            <div className="input-group password-input">
              <input
                id="password"
                type="password"
                value={password}
                placeholder="Password"
                onChange={(e) => setPassword(e.target.value)}
              />
            </div>
            <button className="login-button" onClick={handleLogin}>Login</button>
            <div className="google-login">
              <GoogleLogin
                buttonText="Continue with Google"
                onSuccess={responseGoogle}
                onFailure={responseGoogle}
                cookiePolicy={'single_host_origin'}
              />
            </div>

            <div className="forgot-password">
              <a href="/Forgot">Forgot your password?</a>
            </div>
            <div className="signup-prompt">
              Don't have an account? <a href="/signup">Sign up for VibeZ</a>
            </div>
          </div>
      </div>
    </div>
  );
};

export default LoginComponent;