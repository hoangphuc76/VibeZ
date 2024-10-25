import React, { useContext, useState } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faXmark, faEllipsis } from '@fortawesome/free-solid-svg-icons';
import { PlayerContext } from '../context/PlayerContext';
import { assets, songsData } from '../assets/assets'; // Tải hình ảnh mặc định
import { useListVisibility } from '../context/VisibleContext';


const Queue = () => {
  const { currentIndex, playWithId, songsData} = useContext(PlayerContext); // Lấy state từ PlayerContext
  const savedTracks = songsData || []; // Lấy danh sách bài hát
  const [visibleMenus, setVisibleMenus] = useState({}); // State để quản lý menu từng bài hát
  const { isListVisible, setIsListVisible } = useListVisibility(); //Dùng để bật tắt menu


  // Hàm bật/tắt menu cho từng bài hát theo index
  const handleToggleMenu = (index) => {
    console.log(currentIndex);
    setVisibleMenus((prev) => ({
      ...prev,
      [index]: !prev[index],
    }));
  };

  // Lấy các bài hát tiếp theo sau currentIndex
  const nextTracks = savedTracks.slice(currentIndex + 1);

  return (
    <div className='w-full h-full bg-[#121212] rounded mt-1 gap-5 flex-col text-white hidden lg:flex px-7'>
      <div className='h-[10%]'>
        <div className='flex justify-between h-full items-center'>
          <div className='text-[25px] font-bold '>
            <h1>Queue</h1>
          </div>
          <div className='cursor-pointer'>
          <FontAwesomeIcon className='hover:text-green-500 ' onClick={() => setIsListVisible(!isListVisible)} icon={faXmark} />
          </div>
        </div>
      </div>

      <div className='h-[90%] flex flex-col gap-5 overflow-auto '>
        <div className='text-[24px] font-bold'>
          <h1>Now Playing</h1>
        </div>

        {savedTracks.length > 0 && savedTracks[currentIndex] ? (
          <div
            key={currentIndex}
            className={`relative flex w-[100%] h-[12%] cursor-pointer hover:bg-[#3E3E3E] p-2 hover:rounded bg-[#3E3E3E]`}
            onClick={() => playWithId(track.trackId)} // Phát bài hát khi click
          >
            <div>
              <img
                className='rounded h-full'
                src={savedTracks[currentIndex].image || assets.img1} // Hiển thị ảnh bài hát
                alt={savedTracks[currentIndex].name || 'Unknown'}
              />
            </div>
            <div className='flex flex-col ml-5'>
              <p className='text-[18px] font-semibold'>
                {savedTracks[currentIndex].name || 'Không có tên'}
              </p>
              <a className='text-gray-400 text-[18px]  hover:text-white hover:underline hover:decoration-1'>
                {savedTracks[currentIndex].artistName || 'Unknown Artist'}
              </a>
            </div>
            <div
              className='absolute right-[2%] hover:text-2xl'
              onClick={(e) => {
                e.stopPropagation(); // Ngăn chặn sự kiện click trên toàn bộ item
                handleToggleMenu(currentIndex); // Bật/tắt menu cho bài hát hiện tại
              }}
            >
              <FontAwesomeIcon icon={faEllipsis} />
            </div>

            {visibleMenus[currentIndex] && (
              <div className='absolute top-[100%] right-0 bg-[#2A2A2A] shadow-lg rounded-md p-2 w-[70%] text-white z-20'>
                <ul>
                  <li className='p-3 hover:bg-[#3E3E3E] text-[14px]'>
                    <a href='#'>Add to playlist</a>
                  </li>
                  <li className='p-3 hover:bg-[#3E3E3E] text-[14px]'>
                    <a href='#'>Save to your Liked Songs</a>
                  </li>
                  <li className='p-3 hover:bg-[#3E3E3E] text-[14px]'>
                    <a href='#'>Remove from queue</a>
                  </li>
                </ul>
              </div>
            )}
          </div>
        ) : (
          <p className='text-center'>Không có bài hát nào trong danh sách.</p>
        )}

        <div className='text-[24px] font-bold'>
          <h1>Next</h1>
        </div>

        {nextTracks.length > 0 ? (
          nextTracks.map((track, index) => (
            <div
              key={currentIndex + 1 + index} // Tạo key duy nhất cho mỗi phần tử
              className='relative flex w-[100%] h-[11%] cursor-pointer hover:bg-[#3E3E3E] p-2 hover:rounded'
              onClick={() => playWithId(track.trackId)} // Phát bài hát tiếp theo khi click
            >
              <div>
                <img
                  className='rounded h-full'
                  src={track.image || assets.img1} // Hiển thị ảnh bài hát
                  alt={track.name || 'Unknown'}
                />
              </div>
              <div className='flex flex-col ml-5'>
                <p className='text-[18px] font-semibold'>{track.name || 'Không có tên'}</p>
                <a className='text-gray-400 text-[18px] hover:text-white hover:underline hover:decoration-1'>
                  {track.artistName || 'Unknown Artist'}
                </a>
              </div>
              <div
                className='absolute right-[2%] hover:text-2xl'
                onClick={(e) => {
                  e.stopPropagation(); // Ngăn chặn sự kiện click trên toàn bộ item
                  handleToggleMenu(currentIndex + 1 + index); // Bật/tắt menu cho bài hát hiện tại
                }}
              >
                <FontAwesomeIcon icon={faEllipsis} />
              </div>

              {visibleMenus[currentIndex + 1 + index] && (
                <div className='absolute top-[100%] right-0 bg-[#2A2A2A] shadow-lg rounded-md p-2 w-[70%]  text-white z-20'>
                  <ul>
                    <li className='p-3 hover:bg-[#3E3E3E] text-[14px]'>
                      <a href='#'>Add to playlist</a>
                    </li>
                    <li className='p-3 hover:bg-[#3E3E3E] text-[14px]'>
                      <a href='#'>Save to your Liked Songs</a>
                    </li>
                    <li className='p-3 hover:bg-[#3E3E3E] text-[14px]'>
                      <a href='#'>Remove from queue</a>
                    </li>
                  </ul>
                </div>
              )}
            </div>
          ))
        ) : (
          <p className='text-center'>Không còn bài hát nào tiếp theo.</p>
        )}
      </div>
    </div>
  );
};

export default Queue;
