import React from 'react';
import { useLocation } from 'react-router-dom';
import {useNavigate } from 'react-router-dom';
const StudentVerify = () => {
  const navigate = useNavigate();
  const location = useLocation();
  const myData = location.state;
  // console.log("Received myData in StudentVerify:", myData);
  const handleNextClick = () => {
    window.location.href = `http://localhost:3000`;
    // navigate('/verify');
  };

  const handleBackToPlan = () => {
    navigate('/plan');
  };
  return (
    <div className="min-h-screen flex flex-col items-center justify-center bg-purple-200">
      <div className="max-w-md w-full bg-white rounded-lg shadow-lg p-8">
        <h1 className="text-2xl font-bold text-black mb-2">Verify you are a student</h1>
        <p className="text-gray-700 mb-4">
          First, we need to check you are enrolled at an accredited college or university.
          Not sure if you are eligible? <a href="/learn-more" className="text-blue-600 hover:underline">Learn more</a>.
        </p>
        <button 
          onClick={handleNextClick} 
          className="bg-purple-300 text-black font-bold py-2 px-6 rounded-full hover:bg-purple-400"
        >
          Next
        </button>

        <button 
          onClick={handleBackToPlan} 
          className="bg-gray-300 text-black font-bold py-2 px-6 rounded-full hover:bg-gray-400"
        >
        Back to PlanCard
        </button>
        
      </div>
    </div>
  );
};

export default StudentVerify;
