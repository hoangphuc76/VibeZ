import React, { useContext } from 'react';
import { useNavigate } from 'react-router-dom';
import { PlayerContext } from '../context/PlayerContext'; // Import PlayerContext
import { assets } from '../assets/assets';
import './NavbarComponent.css';
import { useState } from 'react';

const Navbar = () => {
  const { isLoggedIn, user } = useContext(PlayerContext); // Lấy isLoggedIn và user từ context
  const navigate = useNavigate();
  const [isListVisible, setIsListVisible] = useState(false);
  const handleToggleList = () => {
    setIsListVisible(!isListVisible);
  };
  console.log(user);
  return (
    <>
      <div className='flex h-[8%]  relative' >
        <div className='w-1/12 flex items-center justify-center'>
          <img className='rounded-full w-14 h-14' src={assets.logo} alt="" />
          <span className='text-[30px] font-semibold ml-[4%] cursor-pointer' onClick={() => navigate('/')}>
            VibeZ
          </span>
        </div>
        <div className='absolute right-0 inset-y-0 w-[15%] flex justify-around items-center' >
          <button className='h-[60%] w-[40%] rounded-[90px] text-black text-[18px] font-bold bg-white transition-all duration-200 hover:w-40'>
            <span>
              Premium
            </span>
          </button>
          <button className='h-10 w-10 rounded-full text-black text-[18px] font-bold bg-white transition-all duration-200 hover:w-12 hover:h-12' onClick={handleToggleList}>
            <span>H</span>
          </button>
          {isListVisible && (
            <ul className='absolute top-[100%] right-0 bg-[#2A2A2A] shadow-lg rounded-md p-2 w-[70%] text-[18px] text-white z-20'>
              <li className='p-3 hover:bg-[#3E3E3E] cursor-pointer'>
                <a onClick={() => navigate('/profile')} >Account</a>
              </li>
              <li className='p-3 hover:bg-[#3E3E3E] cursor-pointer'>
                <a href="">Profile</a>
              </li>
              <li className='p-3 hover:bg-[#3E3E3E] cursor-pointer'>
                <a href="">Upgrade Premium</a>
              </li>
              <li className='p-3 hover:bg-[#3E3E3E] cursor-pointer'>
                <a href="">Log out</a>
              </li>
            </ul>
          )
          }
        </div>
        {/* <div className='absolute right-0 inset-y-0 w-[15%] flex justify-around items-center'>
          <button className='h-full w-[30%]  text-white text-[20px] font-bold duration-200 hover:text-[22px] transition-all' onClick={() => navigate('/signup')}>
            <span>Sign Up</span>
          </button>
          <button className='h-[80%] w-[40%] rounded-[90px] text-black text-[20px] font-bold bg-white transition-all duration-200 hover:w-40 ' onClick={() => navigate('/login')}>
            <span>
              Log In
            </span>
          </button>
        </div> */}
      </div>
    </>
  );
}

export default Navbar;
