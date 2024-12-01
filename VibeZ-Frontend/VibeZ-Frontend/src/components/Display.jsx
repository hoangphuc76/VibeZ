// Display.js
import React, { useContext, useEffect, useRef } from 'react';
import { Outlet, useLocation } from 'react-router-dom';
import { albumsData } from '../assets/assets';
import { PlayerContext } from '../context/PlayerContext';

const Display = () => {
  const displayRef = useRef();
  const location = useLocation();
  const isAlbum = location.pathname.includes('album');
  const isArtist = location.pathname.includes('artist');
  const isPlaylist = location.pathname.includes('playlist');
  const lyrics = location.pathname.includes('lyrics');
  const albumId = isAlbum ? location.pathname.split('/').pop() : '';
  const bgColor = isAlbum && albumsData[Number(albumId)] ? albumsData[Number(albumId)].bgColor : '#121212';
  const { isLyrics } = useContext(PlayerContext);

  useEffect(() => {
    if (isAlbum && displayRef.current) {
      displayRef.current.style.background = `linear-gradient(${bgColor}, #121212)`;
    } else if (lyrics && displayRef.current) {
      displayRef.current.style.background = `#C45249`;
    }
    else if (displayRef.current) {
      displayRef.current.style.background = `#121212`;
    }
  }, [isAlbum, lyrics, bgColor]);

  return (
    <>
      <div ref={displayRef}className={`h-full flex-1  mx-2 px-6 pt-2 rounded bg-[#121212]  text-white overflow-y-scroll `}>
        <Outlet />
      </div>
    </>
  );
}

export default Display;
