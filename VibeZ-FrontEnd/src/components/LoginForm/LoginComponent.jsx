import React, { useContext, useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import googleIcon from "../../assets/google-icon.svg";
import facebookIcon from "../../assets/facebook-icon.svg";
import showIcon from "../../assets/show-icon.svg";
import { LoginContext } from "../../context/LoginContext";
import { assets } from "../../assets/assets";

const LoginComponent = () => {
  const { login, isLoggedIn, user } = useContext(LoginContext);
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
    if (isLoggedIn) {
      navigate("/");
    }
  }, [isLoggedIn, navigate]);

  return (
    <div className="min-h-screen bg-black flex items-center justify-center">
      {isLoggedIn ? (
        <p className="text-white">Welcome, {user}</p>
      ) : (
        <div className="bg-[#1A1A1A] p-10 rounded-xl text-center w-[550px]">
          <div className="mb-8">
            <img className="w-16 h-16 mx-auto rounded-full" src={assets.logo} alt="" />
          </div>
          <h1 className="text-[30px] text-white mb-6 font-bold ">Log in to VibeZ</h1>
          <button className="w-full bg-[#2A2A2A] text-white py-3 rounded-full flex items-center justify-center mb-4">
            <img src={googleIcon} alt="Google" className="w-5 h-5 mr-3" />
            Continue with Google
          </button>
          <button className="w-full bg-[#2A2A2A] text-white py-3 rounded-full flex items-center justify-center mb-4">
            <img src={facebookIcon} alt="Facebook" className="w-5 h-5 mr-3" />
            Continue with Facebook
          </button>
          <button className="w-full bg-[#2A2A2A] text-white py-3 rounded-full mb-4">
            Continue with phone number
          </button>
          <div className="w-full">
            <input
              type="text"
              placeholder="Email or username"
              value={username}
              onChange={(e) => setUsername(e.target.value)}
              className="w-full bg-[#2A2A2A] text-white py-3 px-4 rounded-md mb-4 outline-none focus:ring-2 "
            />
          </div>
          <div className="relative w-full">
            <input
              type="password"
              placeholder="Password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              className="w-full bg-[#2A2A2A] text-white py-3 px-4 rounded-md mb-4 outline-none focus:ring-2 "
            />
            <button className="absolute right-4 top-1/2 transform -translate-y-1/2">
              <img src={showIcon} alt="Show" className="w-5 h-5" />
            </button>
          </div>
          <button
            onClick={handleLogin}
            className="w-full bg-green-500 text-black py-3 rounded-full font-bold text-lg hover:ring-2 hover:ring-white"
          >
            Login
          </button>
          <div className="mt-4">
            <a href="#" className="text-white underline">
              Forgot your password?
            </a>
          </div>
          <div className="mt-4 text-gray-400">
            Don't have an account?{" "}
            <a href="/signup.html" className="text-white underline">
              Sign up for VibeZ
            </a>
          </div>
        </div>
      )}
    </div>
  );
};

export default LoginComponent;
