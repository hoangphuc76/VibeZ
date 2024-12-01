import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import authService from '../../services/authService';

const NewPass = () => {
    const [newPassword, setNewPassword] = useState('');
    const [email, setEmail] = useState('');
    const navigate = useNavigate();
  
    useEffect(() => {
      const storedEmail = localStorage.getItem('resetEmail');
      if (storedEmail) setEmail(storedEmail);
    }, []);
  
    const handleSubmit = async (e) => {
        e.preventDefault();
        if (!newPassword) {
          alert("Please enter a new password.");
          return;
        }
    
        try {
          const response = await authService.resetPassword(email, newPassword);
          if (response) {
            localStorage.removeItem('resetEmail');
            navigate('/login');
          }
        } catch (error) {
          console.error("Password Reset error:", error.message);
        }
      };

  return (
    <div className="flex justify-center min-h-screen bg-black">
      <div className="bg-black p-10 rounded-lg text-center w-[400px]">
        <h1 className="text-white mb-5 text-4xl font-bold">Reset Password</h1>
        <form onSubmit={handleSubmit}>
          <input
            type="password"
            value={newPassword}
            onChange={(e) => setNewPassword(e.target.value)}
            placeholder="Enter new password"
            required
            className="w-full p-4 mb-5 border border-[#535353] rounded-md text-base text-white bg-black"
          />
          <button type="submit" className="w-full p-3 bg-[#1DB954] text-[#191414] text-lg rounded transition duration-300 hover:bg-[#14833B]">
            Reset Password
          </button>
        </form>
      </div>
    </div>
  );
};

export default NewPass;
