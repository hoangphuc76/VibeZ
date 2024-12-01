import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import authService from '../../services/authService';

const Verification = () => {
    const [otp, setOtp] = useState(['', '', '', '']);
    const [email, setEmail] = useState('');
    const [timeLeft, setTimeLeft] = useState(5 * 60);
    const navigate = useNavigate();
  
    useEffect(() => {
      const storedEmail = localStorage.getItem('resetEmail');
      if (storedEmail) setEmail(storedEmail);
    }, []);
  
    const handleSubmit = async (e) => {
      e.preventDefault();
      const otpString = otp.join('');
      try {
        const response = await authService.verifyOtp(email, otpString);
        if (response) {
          navigate('/newpass');
        }
      } catch (error) {
        console.error("OTP Verification error:", error.message);
      }
    };

  // Move focus to the next field after entering a digit
  const moveToNext = (event, nextFieldId) => {
    if (event.target.value.length === event.target.maxLength) {
      const nextField = document.getElementById(nextFieldId);
      if (nextField) nextField.focus();
    }
  };

  // Handle OTP input change
  const handleChange = (e, index) => {
    const newOtp = [...otp];  // Create a copy of the otp array
    newOtp[index] = e.target.value.slice(0, 1);  // Only take the first character
    setOtp(newOtp);
  };

  // Format the timer
  const formatTime = () => {
    const seconds = timeLeft % 60;
    return `00:${seconds < 10 ? '0' : ''}${seconds}`;
  };

  return (
    <div className="flex justify-center min-h-screen bg-black">
      <div className="bg-black p-10 rounded-lg text-center w-[400px]">
        <h1 className="text-white mb-5 text-4xl font-bold">Verification</h1>
        <p className="text-white mb-5 text-sm">Enter your 4-digit code that you received on your email.</p>
        <form onSubmit={handleSubmit}>
          <div className="flex justify-between mb-5">
            {otp.map((digit, index) => (
              <input
                key={index}
                type="text"
                maxLength="1"
                value={digit}
                onChange={(e) => handleChange(e, index)} // Handle change for each input
                onInput={(e) => moveToNext(e, `input${index + 1}`)} // Move focus to the next input
                id={`input${index}`}
                className="w-[60px] h-[60px] text-2xl text-center border border-white rounded bg-[#333333] text-white"
              />
            ))}
          </div>
          <div className="text-white text-lg mb-5">{formatTime()}</div>
          <button type="submit" className="w-full p-3 bg-[#1DB954] text-[#191414] text-lg rounded transition duration-300 hover:bg-[#14833B]">
            Continue
          </button>
        </form>
      </div>
    </div>
  );
};

export default Verification;
