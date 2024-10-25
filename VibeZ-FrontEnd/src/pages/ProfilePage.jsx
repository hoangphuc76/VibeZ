import React from 'react';
import { Search, ChevronRight, CreditCard, Gift, Grid, Bell, Lock, Key, LogOut } from 'lucide-react';
import Navbar from '../components/Navbar2';

const ProfilePage = () => {
  return (
    <div className="bg-black text-white min-h-screen p-6">
      <Navbar />
      <div className="max-w-3xl mx-auto">
        {/* <div className="relative mb-6">
          <Search className="absolute left-3 top-1/2 transform -translate-y-1/2 text-gray-400" size={20} />
          <input
            type="text"
            placeholder="Search account or help articles"
            className="w-full bg-gray-800 rounded-full py-2 pl-10 pr-4 text-white placeholder-gray-400"
          />
        </div> */}

        <div className="bg-[#2A2A2A] rounded-lg px-4 py-8 mb-6 flex justify-between items-center">
          <div>
            <h2 className="text-xl font-bold">Spotify Free</h2>
            <button className="mt-2 px-4 py-1 bg-white text-black rounded-full text-sm transition-all duration-200 hover:bg-green-600 font-bold">Explore plans</button>
          </div>
          <button className="bg-purple-600 text-white px-4 py-2 rounded-full transition-all duration-200 hover:bg-black font-bold">Join Premium</button>
        </div>

        <div className="space-y-6">
          <Section title="Account">
            <MenuItem icon={<CreditCard size={20} />} text="Manage your subscription" />
            <MenuItem icon={<Grid size={20} />} text="Edit profile" />
            <MenuItem icon={<Gift size={20} />} text="Recover playlists" />
          </Section>

          <Section title="Payment">
            <MenuItem icon={<CreditCard size={20} />} text="Order history" />
            <MenuItem icon={<CreditCard size={20} />} text="Saved payment cards" />
            <MenuItem icon={<Gift size={20} />} text="Redeem" />
          </Section>

          <Section title="Security and privacy">
            <MenuItem icon={<Grid size={20} />} text="Manage apps" />
            <MenuItem icon={<Bell size={20} />} text="Notification settings" />
            <MenuItem icon={<Lock size={20} />} text="Privacy settings" />
            <MenuItem icon={<Key size={20} />} text="Edit login methods" />
            <MenuItem icon={<Key size={20} />} text="Set device password" />
            <MenuItem icon={<LogOut size={20} />} text="Sign out everywhere" />
          </Section>
        </div>
      </div>
    </div>
  );
};

const Section = ({ title, children }) => (
  <div>
    <h3 className="text-[20] font-bold mb-2">{title}</h3>
    <div className="bg-[#2A2A2A] rounded-lg overflow-hidden ">{children}</div>
  </div>
);

const MenuItem = ({ icon, text }) => (
  <div className="flex items-center justify-between px-4 py-6 hover:bg-gray-700 cursor-pointer">
    <div className="flex items-center space-x-3">
      {icon}
      <span>{text}</span>
    </div>
    <ChevronRight size={20} />
  </div>
);

export default ProfilePage;