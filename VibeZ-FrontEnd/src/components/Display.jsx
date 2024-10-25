// Display.js
import React, { useEffect, useRef } from 'react';
import { Outlet, useLocation } from 'react-router-dom';
import { albumsData } from '../assets/assets';

const Display = () => {
  const displayRef = useRef();
  const location = useLocation();
  const isAlbum = location.pathname.includes('album');
  const isArtist = location.pathname.includes('artist');
  const isPlaylist = location.pathname.includes('playlist');
  const albumId = isAlbum ? location.pathname.split('/').pop() : '';
  const bgColor = isAlbum && albumsData[Number(albumId)] ? albumsData[Number(albumId)].bgColor : '#121212';

  useEffect(() => {
    if (isAlbum && displayRef.current) {
      displayRef.current.style.background = `linear-gradient(${bgColor}, #121212)`;
    } else if (displayRef.current) {
      displayRef.current.style.background = `#121212`;
    }
  }, [isAlbum, bgColor]);

  return (
    <>
      <div ref={displayRef} className='flex-1 m-2 px-6 pt-4 rounded bg-[#121212] text-white overflow-auto'>
        <Outlet />
      </div>
    </>
  );
}

export default Display;
