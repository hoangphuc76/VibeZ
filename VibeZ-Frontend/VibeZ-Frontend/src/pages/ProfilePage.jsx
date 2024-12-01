import React, { useState, useEffect, useContext } from 'react';
import { useNavigate } from 'react-router-dom';
import { Spin } from 'antd'; // Import Spin from Ant Design
import { ChevronRight, CreditCard, Gift, Grid, Bell, Lock, Key, LogOut } from 'lucide-react';
import Navbar from '../components/Navbar2';
import { LoginContext } from '../context/LoginContext';
import authService from '../services/authService';

const ProfilePage = () => {
  const navigate = useNavigate();
  const [loading, setLoading] = useState(false); // Loading state
  const [percent, setPercent] = useState(0);
  const [premium, setPremium] = useState();
  const [roles, setRoles] = useState(false);
  const [user, setUser] = useState();

  const handleEditProfile = () => {
    setLoading(true);
    let ptg = -10;

    const interval = setInterval(() => {
      ptg += 5;
      setPercent(ptg);

      if (ptg > 120) {
        clearInterval(interval);
        setLoading(false);
        setPercent(0);
        navigate('/profileedit');
      }
    }, 100);
  };
  useEffect(() => {
    const fetchUser = async () => {
      const userId = JSON.parse(localStorage.getItem('userId'));
      const respone = await authService.getUserById(userId);
      setUser(respone);
      console.log(respone);
    };
    fetchUser();
  }, [])

  useEffect(() => {
    const fetchPremium = () => {
      const premiumData = JSON.parse(localStorage.getItem('premium'));
      const roles = JSON.parse(localStorage.getItem('roles'));
      setRoles(roles);
      setPremium(premiumData);
    };
    fetchPremium();
  }, []);

  return (
    <div className="bg-black text-white min-h-screen p-6">
      <Navbar />
      {loading ? (
        <div className="flex justify-center items-center h-screen">
          <Spin spinning={loading} percent={percent} />
        </div>
      ) : (
        <div className="max-w-3xl mx-auto">
          <div className="bg-[#2A2A2A] rounded-lg px-4 py-8 mb-6 flex justify-between items-center">
            <div>
              <h2 className="text-xl font-bold">{premium ? "VibeZ Premium" : "VibeZ Free"}</h2>
              <button className="mt-2 px-4 py-1 bg-white text-black rounded-full text-sm transition-all duration-200 hover:bg-green-600 font-bold">Explore plans</button>
            </div>

          </div>

          <div className="space-y-6">
            <Section title="Account">
              <MenuItem icon={<CreditCard size={20} />} text="Manage your subscription" />
              <MenuItem icon={<Grid size={20} />} text="Edit profile" onClick={handleEditProfile} />
              <MenuItem icon={<Gift size={20} />} text="Recover playlists" />
              {user && user.role === 'Admin' && (
                <MenuItem icon={<Gift size={20} />} text="Admin Management" onClick={() => navigate('/admin')} />
              )
              }
              {user && user.role === 'Artist' ? (
                <MenuItem icon={<Gift size={20} />} text="Artist Management" onClick={() => navigate('/artistDashboard')} />
              ) : (
                <MenuItem icon={<Gift size={20} />} text="Get Access Artist" onClick={() => navigate('/profile/artist-profile-claim')} />
              )
              }
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
      )}
    </div>
  );
};

const Section = ({ title, children }) => (
  <div>
    <h3 className="text-[20px] font-bold mb-2">{title}</h3>
    <div className="bg-[#2A2A2A] rounded-lg overflow-hidden">{children}</div>
  </div>
);

const MenuItem = ({ icon, text, onClick }) => (
  <div className="flex items-center justify-between px-4 py-6 hover:bg-gray-700 cursor-pointer" onClick={onClick}>
    <div className="flex items-center space-x-3">
      {icon}
      <span>{text}</span>
    </div>
    <ChevronRight size={20} />
  </div>
);

export default ProfilePage;
