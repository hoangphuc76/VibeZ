import React, { useState } from 'react';
import axios from 'axios';
import artistDashboardService from '../../services/artistDashboardService';
import artistService from '../../services/artistService';
import { ClipLoader } from 'react-spinners';
import { NutIcon } from 'lucide-react';
import { useNavigate } from 'react-router-dom';

function ArtistVerificationRequestForm() {
  const navigate = useNavigate();
  const [isLoading, setLoading] = useState(false);
  const [formData, setFormData] = useState({
    artistName: '',
    email: '',
    reason: '',
    audioFile: null,
    imgFile: null,
    imgBackground: null,
    songImg: null,
    lyrics: null,
    lyricLRC: null,
    genre: '',
    hour: '',
    minute: '',
    second: '',
    songName: '', // Thêm songName
    nation: ''    // Thêm nation
  });

  const [message, setMessage] = useState('');

  const handleChange = (e) => {
    const { id, value, files } = e.target;
    if (files) {
      setFormData((prevData) => ({ ...prevData, [id]: files[0] }));
    } else {
      setFormData((prevData) => ({ ...prevData, [id]: value }));
    }
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setLoading(true);

    const userId = JSON.parse(localStorage.getItem('userId')); // hoặc giá trị userId khác

    try {
      const formDataObj = new FormData();
      formDataObj.append('artistName', formData.artistName);
      formDataObj.append('email', formData.email);
      formDataObj.append('userId', userId);
      formDataObj.append('image', formData.imgFile);
      formDataObj.append('imgBackground', formData.imgBackground);
      formDataObj.append('lyrics', formData.lyrics);
      formDataObj.append('lyricLRC', formData.lyricLRC);
      formDataObj.append('song', formData.audioFile);
      formDataObj.append('genre', formData.genre);
      formDataObj.append('minute', parseInt(formData.minute, 10));
      formDataObj.append('second', parseInt(formData.second, 10));
      formDataObj.append('songImg', formData.songImg);
      formDataObj.append('songName', formData.songName); // Gửi songName
      formDataObj.append('nation', formData.nation);     // Gửi nation

      formDataObj.forEach((value, key) => {
        console.log(`${key}:`, value);
      });
      const response = await artistDashboardService.addArtistPending(formDataObj);

      console.log("Response:", response);
      setMessage("Artist pending added successfully.");
      // Reset form if successful
      setFormData({
        artistName: '',
        email: '',
        audioFile: null,
        imgFile: null,
        imgBackground: null,
        lyrics: null,
        lyricLRC: null,
        genre: '',
        hour: '',
        minute: '',
        second: '',
        songName: '', // Reset songName
        nation: ''    // Reset nation
      });
      navigate('/');
    } catch (error) {
      setMessage("An error occurred. Please try again later.");
      console.error("Error adding artist pending:", error.message || error);
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="bg-gray-800 text-white min-h-screen flex flex-col justify-center items-center p-6">
      <header className="text-center mb-6">
        <h1 className="text-3xl font-bold text-purple-600">forArtists</h1>
      </header>

      <div className="bg-[#2A2A2A] p-6 rounded-lg shadow-lg max-w-md w-full">
        <h2 className="text-xl font-semibold text-white mb-4">Claim your Artist Profile</h2>
        <p className="text-gray-400 mb-4">
          To become a verified artist, please fill in the form below and submit it. Our system will review your track for originality.
        </p>

        {message && (
          <div className={`p-2 rounded-lg mb-4 ${message.includes("successfully") ? 'bg-green-500' : 'bg-red-500'}`}>
            {message}
          </div>
        )}

        <form onSubmit={handleSubmit}>
          <div className="mb-4">
            <label htmlFor="artistName" className="block text-gray-300">Artist Name</label>
            <input
              type="text"
              id="artistName"
              value={formData.artistName}
              onChange={handleChange}
              className="w-full px-4 py-2 bg-[#2A2A2A] border border-gray-600 text-white rounded-md"
              required
            />
          </div>

          <div className="mb-4">
            <label htmlFor="songName" className="block text-gray-300">Song Name</label>
            <input
              type="text"
              id="songName"
              value={formData.songName}
              onChange={handleChange}
              className="w-full px-4 py-2 bg-[#2A2A2A] border border-gray-600 text-white rounded-md"
              required
            />
          </div>

          <div className="mb-4">
            <label htmlFor="nation" className="block text-gray-300">Nation</label>
            <input
              type="text"
              id="nation"
              value={formData.nation}
              onChange={handleChange}
              className="w-full px-4 py-2 bg-[#2A2A2A] border border-gray-600 text-white rounded-md"
              required
            />
          </div>

          <div className="mb-4">
            <label htmlFor="genre" className="block text-gray-300">Genre</label>
            <input
              type="text"
              id="genre"
              value={formData.genre}
              onChange={handleChange}
              className="w-full px-4 py-2 bg-[#2A2A2A] border border-gray-600 text-white rounded-md"
              required
            />
          </div>

          <div className="mb-4">
            <label htmlFor="email" className="block text-gray-300">Email</label>
            <input
              type="email"
              id="email"
              value={formData.email}
              onChange={handleChange}
              className="w-full px-4 py-2 bg-[#2A2A2A] border border-gray-600 text-white rounded-md"
              required
            />
          </div>

          <div className="mb-4">
            <label htmlFor="audioFile" className="block text-gray-300">Upload Your Track (Audio File)</label>
            <input
              type="file"
              id="audioFile"
              onChange={handleChange}
              className="w-full text-white"
              accept="audio/*"
              required
            />
          </div>

          <div className="mb-4">
            <label htmlFor="songImg" className="block text-gray-300">Upload Song Image</label>
            <input
              type="file"
              id="songImg"
              onChange={handleChange}
              className="w-full text-white"
              accept="image/*"
              required
            />
          </div>

          {/* Minute and Second inputs */}
          <div className="mb-4">
            <label htmlFor="minute" className="block text-gray-300">Minute</label>
            <input
              type="number"
              id="minute"
              value={formData.minute}
              onChange={handleChange}
              className="w-full px-4 py-2 bg-[#2A2A2A] border border-gray-600 text-white rounded-md"
              required
              min="0"
              max="59"
            />
          </div>

          <div className="mb-4">
            <label htmlFor="second" className="block text-gray-300">Second</label>
            <input
              type="number"
              id="second"
              value={formData.second}
              onChange={handleChange}
              className="w-full px-4 py-2 bg-[#2A2A2A] border border-gray-600 text-white rounded-md"
              required
              min="0"
              max="59"
            />
          </div>

          <div className="mb-4">
            <label htmlFor="imgFile" className="block text-gray-300">Upload Artist Image</label>
            <input
              type="file"
              id="imgFile"
              onChange={handleChange}
              className="w-full text-white"
              accept="image/*"
              required
            />
          </div>

          <div className="mb-4">
            <label htmlFor="imgBackground" className="block text-gray-300">Upload Background Image</label>
            <input
              type="file"
              id="imgBackground"
              onChange={handleChange}
              className="w-full text-white"
              accept="image/*"
            />
          </div>

          <div className="mb-4">
            <label htmlFor="lyrics" className="block text-gray-300">Upload Lyrics (Text)</label>
            <input
              type="file"
              id="lyrics"
              onChange={handleChange}
              className="w-full text-white"
              accept=".txt"
            />
          </div>

          <div className="mb-4">
            <label htmlFor="lyricLRC" className="block text-gray-300">Upload Lyric LRC File</label>
            <input
              type="file"
              id="lyricLRC"
              onChange={handleChange}
              className="w-full text-white"
              accept=".lrc"
            />
          </div>

          <button
            type="submit"
            className="w-full bg-purple-600 text-white py-2 rounded-lg hover:bg-purple-500 transition-all mb-4"
            disabled={isLoading}
          >
            {isLoading ? 'Submitting...' : 'Request Verification'}
          </button>
        </form>

        <p className="text-gray-400 text-sm text-center">
          Once reviewed, you will receive an email confirmation if your profile is verified.
        </p>
      </div>

      {isLoading && (
        <div className='absolute inset-0 flex items-center justify-center bg-black bg-opacity-50 z-50'>
          <ClipLoader color="#ffffff" size={60} />
        </div>
      )}
    </div>
  );
}

export default ArtistVerificationRequestForm;
