import React, { useContext, useEffect, useState } from 'react';
import { assets } from '../assets/assets';
import { PlayerContext } from '../context/PlayerContext';
import artistService from '../services/artistService';
import { LoginContext } from '../context/LoginContext';
import { useNavigate } from 'react-router-dom';

const SongInfo = ({ artist, track, isFollow, setFollow }) => {
  const { isChange, setChange } = useContext(LoginContext);
  const { setLoading } = useContext(PlayerContext);
  const navigate = useNavigate();
  const handleUnfollowAritst = async () => {
    try {
      const userId = JSON.parse(localStorage.getItem('userId'));
      const libId = JSON.parse(localStorage.getItem('libId'));
      const result = await artistService.unfollowArtist(track.artistId, libId);
      const result2 = await artistService.unfollow(userId, track.artistId);
      setLoading(true);
      if (result === 200 && result2) {
        console.log("Unfollow successful!");
        setLoading(false);
        setChange(!isChange);
        setFollow(false);
      }
    } catch (error) {
      console.error("Failed to unfollow", error);
      // Thông báo lỗi cho người dùng
    }
  };
  const handleFollow = async () => {
    try {
      setLoading(true);
      const libId = JSON.parse(localStorage.getItem('libId'));
      const userId = JSON.parse(localStorage.getItem('userId'));
      const respone = await artistService.followArtist(libId, track.artistId);
      const respone1 = await artistService.follow(userId, track.artistId);

      if (respone && respone1) {
        setLoading(false);
        setChange(!isChange);
        setFollow(true);
      }
    } catch (error) {
      setLoading(false);
      console.error("Failed to follow", error);
    }
  }

  return (
    <div className='overflow-scroll h-full'>

      <div className="bg-[#121212] text-white h-full flex flex-col gap-3 items-center p-2 overflow-scroll">

        <div className="w-[18vw] bg-[#121212] rounded-lg  shadow-lg relative">
          <div
            className="relative h-[300px] bg-cover bg-center rounded-3xl"
            style={{
              backgroundImage: `url(${track.songInfoImg})`,
            }}
          ></div>
          <h1 className="font-bold text-[25px] mt-3  z-auto relative">{track.name}</h1>
          <p className="text-gray-400  mb-4 z-20 relative">{track.artistName}</p>
        </div>

        {/* Div chứa ảnh thứ hai */}
        <div className="w-[18vw] bg-[#272727] rounded-lg shadow-lg cursor-pointer" onClick={() => navigate(`artist/${track.artistId}`)}>
          {/* Phần ảnh của nghệ sĩ */}
          <div
            className="bg-cover bg-center "
          >
            <div className="px-4 py-3 text-white font-semibold">About the artist</div>
          </div>
          <div className='max-w-[140px] px-4'>
            <img className='rounded-full' src={artist.image} alt="" />
          </div>
          {/* Thông tin về nghệ sĩ */}
          <div className="px-4 py-3 ">
            <div className="text-lg font-bold">{artist.name}</div>
            <div className="text-[14px] flex text-gray-400 items-center gap-20">1,664,944 monthly listeners
              {isFollow ? (
                <button className="mt-3 px-3 py-1 border border-gray-500 rounded-full text-white text-end hover:text-white hover:border-white font-bold"
                  onClick={(e) => {
                    handleUnfollowAritst(),
                    e.stopPropagation()
                  }}
                >
                  following
                </button>
              ) : (
                <button className="mt-3 px-3 py-1 border border-gray-500 rounded-full text-white text-end hover:text-white hover:border-white font-bold"
                   onClick={(e) => {
                    handleFollow(),
                    e.stopPropagation()
                  }}
                >
                  follow
                </button>
              )
              }

            </div>
          </div>
        </div>
      </div>
    </div>

  );
};

export default SongInfo;
