import React, { useState } from 'react';
import Navbar from './Navbar2';

function ProfileEdit() {
  const [gender, setGender] = useState('male');
  const [day, setDay] = useState(7);
  const [month, setMonth] = useState(6);
  const [year, setYear] = useState(2004);
  const [country, setCountry] = useState('vietnam');
  const [marketingConsent, setMarketingConsent] = useState(true);

  const handleSubmit = (e) => {
    e.preventDefault();
    alert('Profile has been saved!');
  };

  return (
    <div className="min-h-screen bg-black text-white">
      <Navbar />
      <div className="container mx-auto p-8">
        <h1 className="text-6xl font-bold mb-6 ml-80 ">Edit Profile</h1>
        <form onSubmit={handleSubmit} className="w-3/6 mx-auto bg-black p-6 rounded shadow-lg">
          <label htmlFor="username" className="block mb-1">Username</label>
          <input type="text" id="username" readOnly className="block w-full mb-4 p-2 border border-gray-300 rounded text-black" value='phong' />

          <label htmlFor="email" className="block mb-1">Email</label>
          <input type="email" id="email" className="block w-full mb-4 p-2 border border-gray-300 rounded" />

          <label htmlFor="gender" className="block mb-1">Gender</label>
          <select id="gender" value={gender} onChange={(e) => setGender(e.target.value)} className="block w-full mb-4 p-2 border border-gray-300 rounded text-black">
            <option value="male">Male</option>
            <option value="female">Female</option>
            <option value="other">Other</option>
          </select>

          <label htmlFor="dob" className="block mb-1">Birth Date</label>
          <div className="flex mb-4 space-x-4 justify-center">
            <input
              type="number"
              id="day"
              placeholder="Day"
              min="1"
              max="31"
              value={day}
              onChange={(e) => setDay(e.target.value)}
              className="w-2/6 p-2 border border-gray-300 rounded text-black"
            />
            <select
              id="month"
              value={month}
              onChange={(e) => setMonth(e.target.value)}
              className="w-2/6 p-2 border border-gray-300 rounded text-black"
            >
              <option value="1">January</option>
              <option value="2">February</option>
              <option value="3">March</option>
              <option value="4">April</option>
              <option value="5">May</option>
              <option value="6">June</option>
              <option value="7">July</option>
              <option value="8">August</option>
              <option value="9">September</option>
              <option value="10">October</option>
              <option value="11">November</option>
              <option value="12">December</option>
            </select>
            <input
              type="number"
              id="year"
              placeholder="Year"
              min="1900"
              max="2024"
              value={year}
              onChange={(e) => setYear(e.target.value)}
              className="w-2/6 p-2 border border-gray-300 rounded text-black"
            />
          </div>

          <label htmlFor="country" className="block mb-1">Country or Region</label>
          <select id="country" value={country} onChange={(e) => setCountry(e.target.value)} className="block w-full mb-4 p-2 border border-gray-300 rounded text-black">
            <option value="vietnam">Vietnam</option>
          </select>

          <div className="flex items-start mb-4">
            <input
              type="checkbox"
              id="marketing"
              checked={marketingConsent}
              onChange={(e) => setMarketingConsent(e.target.checked)}
              className="mr-2"
            />
            <label htmlFor="marketing" className="text-sm">
              Share registration data with content providers for marketing purposes.
            </label>
          </div>

          <div className="flex justify-end space-x-4">
            <button
              type="button"
              className="px-4 py-2 bg-gray-700 text-white font-bold rounded hover:bg-gray-600"
            >
              Cancel
            </button>
            <button
              type="submit"
              className="px-4 py-2 bg-green-600 text-white font-bold rounded hover:bg-green-700"
            >
              Save Profile
            </button>
          </div>
        </form>
      </div>
    </div>
  );
}

export default ProfileEdit;
