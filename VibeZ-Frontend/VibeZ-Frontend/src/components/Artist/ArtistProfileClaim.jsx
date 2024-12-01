import React from 'react';
import { useNavigate } from 'react-router-dom';

function ArtistProfileClaim() {

  const navigate = useNavigate();  
  const verificationForm = () => {
        navigate('/profile/artist-profile-claim/verify');
    }

  return (
    <div className="bg-gray-800 text-white min-h-screen flex flex-col justify-center items-center p-6">
      <header className="text-center mb-6">
        <h1 className="text-3xl font-bold text-purple-600">forArtists</h1>
      </header>

      <div className="bg-[#2A2A2A] p-6 rounded-lg shadow-lg max-w-md w-full">
        <h2 className="text-xl font-semibold text-white mb-4">Claim your Artist Profile</h2>
        <p className="text-gray-400 mb-4">
          To become a verified artist, request verification by submitting your artist information. Our admin team will review it and verify your profile.
        </p>

        <button className="w-full bg-purple-600 text-white py-2 rounded-lg hover:bg-purple-500 transition-all mb-4"
        onClick={verificationForm}>
          Request Verification
        </button>

        <p className="text-gray-400 text-sm text-center">
          Once reviewed, you will receive an email confirmation if your profile is verified.
        </p>
      </div>
    </div>
  );
}

export default ArtistProfileClaim;
