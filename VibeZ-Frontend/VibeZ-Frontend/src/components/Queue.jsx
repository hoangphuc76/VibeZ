import React, { useContext, useEffect, useState } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faXmark, faEllipsis, faPlus } from '@fortawesome/free-solid-svg-icons';
import { PlayerContext } from '../context/PlayerContext';
import { assets } from '../assets/assets'; // Tải hình ảnh mặc định
import { useListVisibility } from '../context/VisibleContext';
import playlistService from '../services/playlistService';
import { Menu, Dropdown } from 'antd';
import libraryService from '../services/libraryService';
import Popup from '../components/Popup';
import { toast, ToastContainer } from 'react-toastify'; // Import ToastContainer
import 'react-toastify/dist/ReactToastify.css'; // Import CSS Toastify




const Queue = ({ queueWidth }) => {
  const { currentIndex, playWithId, songsData, setLoading, QueueTrack, setQueue, track, setSongsData, playQueue } = useContext(PlayerContext); // Lấy state từ PlayerContext
  const { isListVisible, setIsListVisible } = useListVisibility(); //Dùng để bật tắt menu
  const [visibleSubMenu, setVisibleSubMenu] = useState(false);
  const [visibleMain, setVisibleMain] = useState(false); // Điều khiển trạng thái dropdown chính
  const [playlist, setPlaylist] = useState([]);
  const [showPopup, setShowPopup] = useState(false);

  useEffect(() => {
    const getPlaylist = async () => {
      try {
        const libId = JSON.parse(localStorage.getItem('libId'));
        const playlist = await libraryService.getPlaylistsByLibraryId(libId);
        setPlaylist(playlist);
      } catch (error) {
        console.error(error.message);
      }
    }
    getPlaylist();
  }, []);

  const togglePopup = () => {
    setShowPopup(!showPopup);
  };

  const AddTrackToQueue = (track) => {
    setQueue([...QueueTrack, track]);
  }

  const removeTrackFromQueue = (track, name) => {
    let index = 0
    if (name === 'queue') {
      index = QueueTrack.indexOf(track);
      QueueTrack.splice(index, 1);
      setQueue(QueueTrack);
    } else {
     index = songsData.indexOf(track);
     songsData.splice(index, 1);
     setSongsData(songsData);
    }
  }
  // Lấy các bài hát tiếp theo sau currentIndex
  const nextTracks = songsData.slice(currentIndex + 1);

  const AddTrack = async (playlistId, trackId, playlistName) => {
    event.domEvent.stopPropagation();
    try {
      const response = await playlistService.AddTrackToPlaylist(playlistId, trackId);
      setLoading(true);
      if (response) {
        setLoading(false);
        toast.success(`Added to "${playlistName}"`, {
          position: "bottom",
          autoClose: 2000, // Đóng thông báo sau 5 giây
          hideProgressBar: false,
          closeOnClick: true,
          pauseOnHover: true,
          draggable: true,
          progress: undefined,
        });
      }
    } catch (error) {
      console.error(error.message);
      toast.error('Track existed', {
        position: "bottom",
        autoClose: 3000,
        hideProgressBar: false,
        closeOnClick: true,
        pauseOnHover: true,
        draggable: true,
        progress: undefined,
      });
    }
  };

  return (
    <div className='w-full h-full bg-[#121212] rounded mt-1 gap-3 flex-col text-white hidden lg:flex px-7'>
      <ToastContainer newestOnTop /> {/* Thêm ToastContainer vào đây */}

      <div className='h-[10%]'>
        <div className='flex justify-between h-full items-center'>
          <div className='text-[25px] font-bold'>
            <h1>Queue</h1>
          </div>
          <div className='cursor-pointer'>
            {/* Khi nhấn vào Xmark sẽ đóng hoặc mở danh sách */}
            <FontAwesomeIcon
              className='hover:text-green-500'
              onClick={() => setIsListVisible(!isListVisible)} // Toggle danh sách Queue
              icon={faXmark}
            />
          </div>
        </div>
      </div>

      {/* Chỉ hiển thị nội dung khi isListVisible = true */}
      {isListVisible && (
        <div className='h-[90%] flex flex-col gap-3 overflow-auto'>
          <div className='text-[24px] font-bold'>
            <h1>Now Playing</h1>
          </div>

          {/* Hiển thị bài hát hiện tại */}
          {track && (
            <div
              key={currentIndex}
              className={`relative flex w-[100%] h-[12%] cursor-pointer hover:bg-[#3E3E3E] p-2 hover:rounded bg-[#3E3E3E]`}
              onClick={() => playWithId(track.trackId)}
            >
              <div>
                <img
                  className='rounded h-full max-w-[50px] max-h-[50px]'
                  src={track.image || assets.img1}
                  alt={track.name || 'Unknown'}
                />
              </div>
              <div className='flex flex-col ml-2'>
                <p
                  className='text-[16px] font-semibold truncate'
                  style={{ maxWidth: `${queueWidth - 200}px` }}
                >
                  {track.name || 'Không có tên'}
                </p>
                <a className='text-gray-400 text-[14px] hover:text-white hover:underline hover:decoration-1'>
                  {track.artistName || 'Unknown Artist'}
                </a>
              </div>
              <div className='absolute right-[2%] hover:text-2xl'>
                <Dropdown
                  overlay={
                    <Menu className="!bg-[#2A2A2A] text-white !rounded-md">
                      <Menu.Item key="1" className="!p-3 hover:!bg-[#3E3E3E] text-[14px]">
                        <Dropdown
                          overlay={
                            <Menu className="!bg-[#2A2A2A] text-white !rounded-md">
                              <Menu.Item key="1-1" className="!p-3 hover:!bg-[#3E3E3E] text-[14px]" onClick={(e) => {
                                e.domEvent.stopPropagation();
                                togglePopup();
                              }} >
                                <div className='flex gap-2 items-center'>
                                  <FontAwesomeIcon className='text-white' icon={faPlus} />
                                  <p className="!text-white">New playlist</p>
                                </div>
                              </Menu.Item>
                              <hr />
                              {
                                playlist.length > 0 &&
                                playlist.map((playlist, index) => (
                                  <Menu.Item key={`${index}`} className="!p-3 hover:!bg-[#3E3E3E] text-[14px]" onClick={(e) => {
                                    e.domEvent.stopPropagation();
                                    AddTrack(e, playlist.playlistId, track.trackId, playlist.name);
                                  }}>
                                    <label href="#" className="!text-white">{playlist.name}</label> {/* Sử dụng thuộc tính name của track */}
                                  </Menu.Item>
                                ))
                              }

                            </Menu>
                          }
                          trigger={['click', 'hover']}
                          placement="left"
                          visible={visibleSubMenu}
                          onVisibleChange={(flag) => {
                            setVisibleSubMenu(flag); // Điều khiển hiển thị dropdown phụ
                          }}
                        >
                          <a
                            href="#"
                            className="!text-white"
                            onClick={(e) => e.stopPropagation()} // Ngăn chặn hành vi mặc định khi click vào "Add to playlist"
                          >
                            Add to playlist
                          </a>
                        </Dropdown>
                      </Menu.Item>
                      <Menu.Item key="2" className="!p-3 hover:!bg-[#3E3E3E] text-[14px]">
                        <a href="#" className="!text-white">Save to your Liked Songs</a>
                      </Menu.Item>
                      <Menu.Item key="3" className="!p-3 hover:!bg-[#3E3E3E] text-[14px]" onClick={(e) => {
                        e.domEvent.stopPropagation();
                        AddTrackToQueue(track);
                      }}>
                        <a href="#" className="!text-white !hover:!bg-white">Add to queue</a>
                      </Menu.Item>
                    </Menu>
                  }
                  trigger={['click', 'contextMenu']}
                  placement="bottomRight"
                  visible={visibleMain}
                  onVisibleChange={(flag) => {
                    setVisibleMain(flag); // Điều khiển hiển thị dropdown chính
                  }}
                >
                  <div onClick={(e) => e.stopPropagation()}>
                    <FontAwesomeIcon icon={faEllipsis} />
                  </div>
                </Dropdown>
              </div>

            </div>
          )}

          {QueueTrack.length > 0 && (
            <>
              <div className='text-[24px] font-bold'>
                <h1>Queue</h1>
              </div>
              {QueueTrack && QueueTrack.map((track, index) => (
                <div
                  key={currentIndex + 1 + index}
                  className='relative flex w-[100%] h-[11%] cursor-pointer hover:bg-[#3E3E3E] p-2 hover:rounded'
                  onClick={() => playQueue(track.trackId)}
                >
                  <div>
                    <img
                      className='rounded h-full max-w-[50px] max-h-[50px]'
                      src={track.image || assets.img1}
                      alt={track.name || 'Unknown'}
                    />
                  </div>
                  <div className='flex flex-col ml-2 w-full'>
                    <p
                      className='text-[16px] font-semibold truncate'
                      style={{ maxWidth: `${queueWidth - 200}px` }}
                    >
                      {track.name || 'Không có tên'}
                    </p>
                    <a className='text-gray-400 text-[14px] hover:text-white hover:underline hover:decoration-1'>
                      {track.artistName || 'Unknown Artist'}
                    </a>
                  </div>
                  <div className='absolute right-[2%] hover:text-2xl'>
                    <Dropdown
                      overlay={
                        <Menu className="!bg-[#2A2A2A] text-white !rounded-md">
                          <Menu.Item key="1" className="!p-3 hover:!bg-[#3E3E3E] text-[14px]">
                            <Dropdown
                              overlay={
                                <Menu className="!bg-[#2A2A2A] text-white !rounded-md">
                                  <Menu.Item key="1-1" className="!p-3 hover:!bg-[#3E3E3E] text-[14px]" onClick={(e) => {
                                    e.stopPropagation(); // Sử dụng e.stopPropagation() thay vì e.domEvent.stopPropagation()
                                    togglePopup();
                                  }} >
                                    <div className='flex gap-2 items-center'>
                                      <FontAwesomeIcon className='text-white' icon={faPlus} />
                                      <p className="!text-white">New playlist</p>
                                    </div>
                                  </Menu.Item>
                                  <hr />
                                  {
                                    playlist.length > 0 &&
                                    playlist.map((playlist, index) => (
                                      <Menu.Item key={`${index}`} className="!p-3 hover:!bg-[#3E3E3E] text-[14px]" onClick={(e) => {
                                        e.stopPropagation(); // Sử dụng e.stopPropagation()
                                        AddTrack(e, playlist.playlistId, track.trackId, playlist.name);
                                      }}>
                                        <label href="#" className="!text-white">{playlist.name}</label>
                                      </Menu.Item>
                                    ))
                                  }

                                </Menu>
                              }
                              trigger={['hover']}
                              placement="left"
                              open={visibleSubMenu} // Thay visible bằng open
                              onOpenChange={(flag) => {
                                setVisibleSubMenu(flag); // Điều khiển hiển thị dropdown phụ
                              }}
                            >
                              <a
                                href="#"
                                className="!text-white"
                                onClick={(e) => e.stopPropagation()} // Ngăn chặn hành vi mặc định khi click vào "Add to playlist"
                              >
                                Add to playlist
                              </a>
                            </Dropdown>
                          </Menu.Item>
                          <Menu.Item key="2" className="!p-3 hover:!bg-[#3E3E3E] text-[14px]">
                            <a href="#" className="!text-white">Save to your Liked Songs</a>
                          </Menu.Item>
                          <Menu.Item key="3" className="!p-3 hover:!bg-[#3E3E3E] text-[14px]" onClick={(e) => {
                            e.domEvent.stopPropagation();
                            AddTrackToQueue(track);
                          }}>
                            <a href="#" className="!text-white !hover:!bg-white">Add to queue</a>
                          </Menu.Item>
                          <Menu.Item key="3" className="!p-3 hover:!bg-[#3E3E3E] text-[14px]" onClick={(e) => {
                            e.domEvent.stopPropagation();
                            removeTrackFromQueue(track, 'queue');
                          }}>
                            <a href="#" className="!text-white !hover:!bg-white">Remove from queue</a>
                          </Menu.Item>
                        </Menu>
                      }
                      trigger={['click']}
                      placement="bottomRight"
                    >
                      <div onClick={(e) => e.stopPropagation()}>
                        <FontAwesomeIcon icon={faEllipsis} />
                      </div>
                    </Dropdown>
                  </div>
                </div>
              ))}
            </>
          )}


          {/* Các bài hát tiếp theo */}
          <div className='text-[24px] font-bold'>
            <h1>Next</h1>
          </div>

          {nextTracks.length > 0 ? (
            nextTracks.map((track, index) => (
              <div
                key={currentIndex + 1 + index}
                className='relative flex w-[100%] h-[11%] cursor-pointer hover:bg-[#3E3E3E] p-2 hover:rounded'
                onClick={() => playWithId(track.trackId)}
              >
                <div>
                  <img
                    className='rounded h-full max-w-[50px] max-h-[50px]'
                    src={track.image || assets.img1}
                    alt={track.name || 'Unknown'}
                  />
                </div>
                <div className='flex flex-col ml-2 w-full'>
                  <p
                    className='text-[16px] font-semibold truncate'
                    style={{ maxWidth: `${queueWidth - 200}px` }}
                  >
                    {track.name || 'Không có tên'}
                  </p>
                  <a className='text-gray-400 text-[14px] hover:text-white hover:underline hover:decoration-1'>
                    {track.artistName || 'Unknown Artist'}
                  </a>
                </div>
                <div className='absolute right-[2%] hover:text-2xl'>
                  <Dropdown
                    overlay={
                      <Menu className="!bg-[#2A2A2A] text-white !rounded-md">
                        <Menu.Item key="1" className="!p-3 hover:!bg-[#3E3E3E] text-[14px]">
                          <Dropdown
                            overlay={
                              <Menu className="!bg-[#2A2A2A] text-white !rounded-md">
                                <Menu.Item key="1-1" className="!p-3 hover:!bg-[#3E3E3E] text-[14px]" onClick={(e) => {
                                  e.domEvent.stopPropagation();
                                  togglePopup();
                                }} >
                                  <div className='flex gap-2 items-center'>
                                    <FontAwesomeIcon className='text-white' icon={faPlus} />
                                    <p className="!text-white">New playlist</p>
                                  </div>
                                </Menu.Item>
                                <hr />
                                {
                                  playlist.length > 0 &&
                                  playlist.map((playlist, index) => (
                                    <Menu.Item key={`${index}`} className="!p-3 hover:!bg-[#3E3E3E] text-[14px]" onClick={(e) => {
                                      e.domEvent.stopPropagation();
                                      AddTrack(e, playlist.playlistId, track.trackId, playlist.name);
                                    }}>
                                      <label href="#" className="!text-white">{playlist.name}</label> {/* Sử dụng thuộc tính name của track */}
                                    </Menu.Item>
                                  ))
                                }

                              </Menu>
                            }
                            trigger={['hover']}
                            placement="left"
                            visible={visibleSubMenu}
                            onVisibleChange={(flag) => {
                              setVisibleSubMenu(flag); // Điều khiển hiển thị dropdown phụ
                            }}
                          >
                            <a
                              href="#"
                              className="!text-white"
                              onClick={(e) => e.stopPropagation()} // Ngăn chặn hành vi mặc định khi click vào "Add to playlist"
                            >
                              Add to playlist
                            </a>
                          </Dropdown>
                        </Menu.Item>
                        <Menu.Item key="2" className="!p-3 hover:!bg-[#3E3E3E] text-[14px]">
                          <a href="#" className="!text-white">Save to your Liked Songs</a>
                        </Menu.Item>
                        <Menu.Item key="3" className="!p-3 hover:!bg-[#3E3E3E] text-[14px]" onClick={(e) => {
                          e.domEvent.stopPropagation();
                          AddTrackToQueue(track);
                        }}>
                          <a href="#" className="!text-white !hover:!bg-white">Add to queue</a>
                        </Menu.Item>
                        <Menu.Item key="3" className="!p-3 hover:!bg-[#3E3E3E] text-[14px]" onClick={(e) => {
                          e.domEvent.stopPropagation();
                          removeTrackFromQueue(track, 'next');
                        }}>
                          <a href="#" className="!text-white !hover:!bg-white">Remove from queue</a>
                        </Menu.Item>
                      </Menu>
                    }
                    trigger={['click']}
                    placement="bottomRight"

                  >
                    <div onClick={(e) => e.stopPropagation()}>
                      <FontAwesomeIcon icon={faEllipsis} />
                    </div>
                  </Dropdown>
                </div>
              </div>
            ))
          ) : (
            <p className='text-center'>Không còn bài hát nào tiếp theo.</p>
          )}
        </div>
      )}
      <Popup show={showPopup} onClose={togglePopup} />

    </div>
  );
};

export default Queue;
