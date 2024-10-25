import React, { useContext } from 'react';
import { useNavigate } from 'react-router-dom';
import { PlayerContext } from '../context/PlayerContext'; // Import PlayerContext
import { assets } from '../assets/assets';
import './NavbarComponent.css';
import { useState } from 'react';
import { LoginContext } from '../context/LoginContext';
import {faHouse} from '@fortawesome/free-solid-svg-icons'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';



const Navbar = () => {
  const { isLoggedIn, user, userId, logout } = useContext(LoginContext); // Lấy isLoggedIn và user từ context
  const navigate = useNavigate();
  const [isListVisible, setIsListVisible] = useState(false);
  const handleToggleList = () => {
    setIsListVisible(!isListVisible);
  };
  const handleLogOut = () => {
    logout();
    window.location.reload();
  };
  return (
    <>
      <div className='flex h-[8%]  relative' >
        <div className='w-1/12 flex items-center justify-center cursor-pointer' >
          <img className='rounded-full w-10 h-10 ml-[30%]' src={assets.logo} alt="" />
          <a className='text-[30px] text-white font-semibold ml-4'>VibeZ</a>
        </div>
        <div className='absolute inset-0 flex justify-center items-center '>
          <div className='w-[40%] h-[100%]  flex justify-center items-center'>
            <form className='relative flex w-[100%] h-[100%] items-center justify-center' action="">
              <div className='mr-[2%] text-white cursor-pointer text-[20px] hover:text-[25px]' onClick={() => navigate('/')}>
              <FontAwesomeIcon icon={faHouse} />
              </div>
              <div className='absolute left-[25%]'>
                <img className='w-[50%]' src={assets.search_icon} alt="" />
              </div>
              <div className='w-7/12 h-[55%]'>
                <input className='w-[100%] h-full rounded-[100px] bg-[#2A2A2A] text-[14px] text-white pl-[14%] py-[2%]' type="search" placeholder='What do you want to play?' autoComplete='on' />
              </div>
            </form>
          </div>
        </div>

        {isLoggedIn ? (
        <div className='absolute right-0 inset-y-0 w-[15%] flex justify-around items-center' >
          <button className='h-[60%] w-[40%] rounded-[90px] text-black text-[18px] font-bold bg-white transition-all duration-200 hover:w-40'>
            <span>
              Premium
            </span>
          </button>
          <button className='h-12 w-12 rounded-full text-black text-[18px] font-bold bg-white transition-all duration-200 hover:w-16 hover:h-16' onClick={handleToggleList}>
            <span>{user}</span>
          </button>
          {isListVisible && (
            <ul className='absolute top-[100%] right-0 bg-[#2A2A2A] shadow-lg rounded-md p-2 w-[70%] text-[18px] text-white z-20'>
              <li className='p-3 hover:bg-[#3E3E3E] cursor-pointer text-[16px]'>
                <a onClick={() => navigate('/profile')} >Account</a>
              </li>
              <li className='p-3 hover:bg-[#3E3E3E] cursor-pointer text-[16px]'>
                <a href="">Profile</a>
              </li>
              <li className='p-3 hover:bg-[#3E3E3E] cursor-pointer text-[16px]'>
                <a href="">Upgrade Premium</a>
              </li>
              <li className='p-3 hover:bg-[#3E3E3E] cursor-pointer text-[16px]' onClick={() => handleLogOut()}>
                <a href="">Log out</a>
              </li>
            </ul>
          )
          }
        </div>
        ) : (
        <div className='absolute right-0 inset-y-0 w-[15%] flex justify-around items-center'>
          <button className='h-full w-[30%]  text-white text-[16px] font-bold duration-200 hover:text-[18px] transition-all' onClick={() => navigate('/signup')}>
            <span>Sign Up</span>
          </button>
          <button className='h-[80%] w-[40%] rounded-[90px] text-black text-[16px] font-bold bg-white transition-all duration-200 hover:w-40 ' onClick={() => navigate('/login')}>
            <span>
              Log In
            </span>
          </button>
        </div>
        ) }

      </div>
    </>
  );
}

export default Navbar;
