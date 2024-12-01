import React, { useState } from 'react';
import authService from '../../services/authService';
import { useNavigate } from 'react-router-dom';

const ForgotPass = () => {
    const [email, setEmail] = useState('');
    const navigate = useNavigate();
  
    const handleSubmit = async (e) => {
      e.preventDefault();
      try {
        const response = await authService.forgotPassword(email);
        if (response) {
          localStorage.setItem('resetEmail', email);
          navigate('/verify');
        }
      } catch (error) {
        console.error("Error sending reset email:", error.message);
      }
    };

  return (
    <div className="flex justify-center min-h-screen bg-black">
      <div className="bg-black p-10 rounded-lg w-[500px]">
        <h1 className="text-white mb-5 text-[36px] font-bold">Reset your password</h1>
        <p className="text-white mb-5 text-[18px]">Enter your email for the verification process, we will send a 4 digits code to your email.</p>
        <form onSubmit={handleSubmit}>
          <label htmlFor="email" className="block mb-2 text-lg text-white text-[16px] font-bold ">Email</label>
          <input
            type="email"
            id="email"
            name="email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            required
            className="w-full p-4 mb-5 border border-[#535353] rounded-md text-base text-white bg-black placeholder-text-black"
            placeholder="Enter your email"
          />
          <button type="submit" className="w-full p-4 bg-[#40935d] text-black font-bold text-lg rounded-full cursor-pointer hover:bg-[#14833B]">
            Continue
          </button>
        </form>
      </div>
    </div>
  );
};

export default ForgotPass;
