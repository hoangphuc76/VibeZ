import React, { useContext, useEffect, useState } from 'react'
// Assets Connection
import { assets } from '../assets/assets';
// Context Component Connection
import { PlayerContext } from '../context/PlayerContext';
import { useListVisibility } from '../context/VisibleContext';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faShuffle, faBackwardStep, faPlay, faPause, faForwardStep, faRepeat, faBars, faDisplay, faMicrophone, faVolumeHigh, faVolumeLow, faVolumeOff } from '@fortawesome/free-solid-svg-icons'; // Import thêm biểu tượng âm lượng
import { LoginContext } from '../context/LoginContext';
import { info } from 'autoprefixer';
import { useNavigate } from 'react-router-dom';

const Player = () => {
  const { track, seekBar, seekBg, playStatus, play, pause, time, previous, next, seekSong, songsData, setSongsData, currentIndex, isLyrics, setLyrics, toggleRepeat, repeat, volume, setVolume } = useContext(PlayerContext); // Thêm setVolume từ PlayerContext
  const [IsShuffle, SetIsShuffle] = useState(false); // Correctly initializing state here
  const { isListVisible, setIsListVisible } = useListVisibility();

  const [originalSongsData, setOriginalSongsData] = useState([]);
  const { Info, setInfo } = useContext(LoginContext);
  const navigate = useNavigate();

  useEffect(() => {
    if (!originalSongsData.length) {
      setOriginalSongsData(songsData); // Lưu danh sách gốc vào state
    }
  }, [songsData, originalSongsData]);

  //hàm để trộn
  const handleShuffleClick = () => {
    const newShuffleState = !IsShuffle; // Toggle shuffle state
    SetIsShuffle(newShuffleState);

    if (newShuffleState) {
      setSongsData((prevSongs) => {
        // Create a copy of the array to avoid mutating the original array
        const beforeIndex = prevSongs.slice(0, currentIndex + 1);
        const afterIndex = [...prevSongs.slice(currentIndex + 1)];

        // Shuffle the songs after the current index
        for (let i = afterIndex.length - 1; i > 0; i--) {
          const j = Math.floor(Math.random() * (i + 1));
          [afterIndex[i], afterIndex[j]] = [afterIndex[j], afterIndex[i]];
        }

        // Combine with the part before the current index and return the new array
        const shuffledSongs = [...beforeIndex, ...afterIndex];
        return shuffledSongs;
      });
      console.log(newShuffleState);
    } else {
      // Nếu shuffle tắt, chỉ reset lại phần danh sách sau current index
      setSongsData((prevSongs) => {
        const beforeIndex = prevSongs.slice(0, currentIndex + 1);
        const afterIndex = originalSongsData.slice(originalSongsData.indexOf(track) + 1);

        // Kết hợp lại phần trước và sau để khôi phục thứ tự sau current index
        const resetSongs = [...beforeIndex, ...afterIndex];
        return resetSongs;
      });
    }
  };

  // Hàm điều chỉnh âm lượng
  const handleVolumeChange = (event) => {
    const newVolume = event.target.value / 100; // Chuyển đổi từ 0-100 về 0-1
    setVolume(newVolume); // Cập nhật âm lượng trong PlayerContext
  };

  return (
    <>
      {
        track && (
          <div className='h-[10%] bg-black flex justify-between items-center text-white px-4'>
            <div className="hidden lg:flex items-center gap-4">
              <img className='w-12' src={track.image} alt="" />

              <div className='cursor-pointer'>
                <p className='text-[20px] font-semibold'>{track.name}</p>
                <p className='text-gray-400 hover:text-white hover:underline hover:decoration-1 '>{track.artistName}</p>
              </div>
            </div>

            <div className="flex flex-col items-center gap-1 m-auto">
              <div className="flex gap-4">
                <div className={`text-[18px] cursor-pointer ${IsShuffle ? 'text-green-500' : 'text-white'}`}>
                  <FontAwesomeIcon className=' hover:text-green-500' icon={faShuffle} onClick={handleShuffleClick} />
                </div>
                <div className='text-[18px] cursor-pointer hover:text-green-500' onClick={previous}>
                  <FontAwesomeIcon icon={faBackwardStep} />
                </div>
                {playStatus
                  ? <div className='text-[18px] cursor-pointer hover:text-green-500' onClick={pause}><FontAwesomeIcon icon={faPause} /></div>
                  : <div className='text-[18px] cursor-pointer hover:text-green-500' onClick={play}><FontAwesomeIcon icon={faPlay} /></div>
                }

                <div className='text-[18px] cursor-pointer hover:text-green-500' onClick={next}>
                  <FontAwesomeIcon icon={faForwardStep} />
                </div>
                <div className={`text-[18px] cursor-pointer hover:text-green-500 ${repeat ? 'text-green-500' : 'text-white'}`} >
                  <FontAwesomeIcon className=' hover:text-green-500' onClick={() => toggleRepeat()} icon={faRepeat} />
                </div>
              </div>

              <div className="flex items-center gap-5">
                <p>{time.currentTime.minute} : {time.currentTime.second}</p>
                <div ref={seekBg} onClick={seekSong} className="w-[60vw] max-w-[500px] bg-white rounded-full cursor-pointer">
                  <hr ref={seekBar} className='h-1 border-none w-0 bg-green-950 rounded-full' />
                </div>
                <p>{time.totalTime.minute} : {time.totalTime.second}</p>
              </div>
            </div>

            <div className="hidden lg:flex items-center gap-2 opacity-75">
              <div className={`text-[18px] cursor-pointer hover:text-green-500}`} onClick={() => setInfo(!Info)}>
                <FontAwesomeIcon className=' hover:text-green-500' icon={faDisplay} />
              </div>
              <div className={`text-[18px] cursor-pointer hover:text-green-500 ${isLyrics ? 'text-green-500' : 'text-white'} `} onClick={() => {
                setLyrics(!isLyrics),
                  navigate(isLyrics ? '/' : `/lyrics`);
              }}>
                <FontAwesomeIcon className=' hover:text-green-500' icon={faMicrophone} />
              </div>
              <div className={`text-[18px] cursor-pointer hover:text-green-500 ${isListVisible ? 'text-green-500' : 'text-white'}`} >
                <FontAwesomeIcon className=' hover:text-green-500' onClick={() => setIsListVisible(!isListVisible)} icon={faBars} />
              </div>

              {/* Biểu tượng âm lượng dựa vào mức volume */}
              <div className='text-[18px] cursor-pointer hover:text-green-500'>
                {volume === 0 ? (
                  <FontAwesomeIcon onClick={() => setVolume(1)} icon={faVolumeOff} />
                ) : volume <= 0.5 ? (
                  <FontAwesomeIcon onClick={() => setVolume(0)} icon={faVolumeLow} />
                ) : (
                  <FontAwesomeIcon onClick={() => setVolume(0)} icon={faVolumeHigh} />
                )}
              </div>

              {/* Thanh trượt âm lượng */}
              <input
                type="range"
                min="0"
                max="100"
                value={volume * 100} // Giá trị âm lượng từ 0-100
                onChange={handleVolumeChange}
                className="w-20 bg-green-600"
              />
            </div>
          </div>
        )
      }
    </>
  );
}

export default Player;
