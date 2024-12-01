import React, { useContext, useEffect, useState, useRef } from "react";
import { useParams } from "react-router-dom";
import { albumsData, assets, songsData } from "../assets/assets";
import { PlayerContext } from "../context/PlayerContext";
import Navbar from "./Navbar";
import albumService from "../services/albumService";
import trackService from "../services/trackService";
import artistService from "../services/artistService";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faPlay, faPause, faPlus, faEllipsis } from "@fortawesome/free-solid-svg-icons";
import { Dropdown, Menu } from 'antd';
import playlistService from "../services/playlistService";
import libraryService from "../services/libraryService";
import { toast, ToastContainer } from 'react-toastify'; // Import ToastContainer
import Popup from '../components/Popup';




const DisplayAlbum = () => {
  const { id } = useParams();
  const {
    playWithId, setSongsData, setCurrentIndex, setPlayStatus,
    playStatus, play, pause, setCurrentAlbumId, currentAlbumId, currentIndex, QueueTrack, setQueue
  } = useContext(PlayerContext);

  const [albumData, setAlbums] = useState({});
  const [artists, setArtists] = useState({});
  const [tracks, setTracks] = useState([]);
  const [clickedId, setClickedId] = useState(false);
  const [status, setStatus] = useState(false);
  const [playCount, setPlayCount] = useState(0); // State để đếm số lần gọi playWithI
  const [menu, setMenu] = useState(null);
  const [visibleSubMenu, setVisibleSubMenu] = useState(false);
  const [playlist, setPlaylistData] = useState([]);
  const [userId, setUserId] = useState(null);
  const [isChange, setChange] = useState(false);
  const [showPopup, setShowPopup] = useState(false);





  const hoverMenu = (index) => {
    setMenu(index); // Cập nhật menu với index hiện tại
  };
  const handleMouseOut = () => {
    setMenu(null); // Đặt menu về null khi rời chuột
  };
  const togglePopup = () => {
    setShowPopup(!showPopup);
  };

  const AddTrack = async (playlistId, trackId, playlistName) => {
    try {
      const response = await playlistService.AddTrackToPlaylist(playlistId, trackId);
      if (response) {
        toast.success(`Added to "${playlistName}"`, {
          position: "bottom-center",
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
        position: "bottom-center",
        autoClose: 3000,
        hideProgressBar: false,
        closeOnClick: true,
        pauseOnHover: true,
        draggable: true,
        progress: undefined,
      });
    }
  };

  const AddTrackToQueue = (track) => {
    setQueue([...QueueTrack, track]);
  }

  const deleteTrack = async (playlistId, trackId) => {
    try {
      const respone = await playlistService.DeleteTrackFromPlaylist(playlistId, trackId);
      if (respone === "Ok") {
        setChange(!isChange);
      }
    } catch (error) {
      console.error(error.message);
    }
  }

  useEffect(() => {
    const getPlaylist = async () => {
      try {
        const libId = JSON.parse(localStorage.getItem('libId'));
        const userid = JSON.parse(localStorage.getItem('userId'));
        setUserId(userid);
        const playlists = await libraryService.getPlaylistsByLibraryId(libId);
        setPlaylistData(playlists);
        console.log("abc" + playlists);
      } catch (error) {
        console.error(error.message);
      }
    }
    getPlaylist();
  }, []);

  useEffect(() => {
    const fetchAlbums = async () => {
      try {
        const data = await albumService.getAlbumsById(id); // Gọi API lấy dữ liệu album
        setAlbums(data); // Lưu dữ liệu vào state albums
        const trackk = await trackService.getAllTrackByAlbumId(id);
        setTracks(trackk);
        console.log(currentAlbumId);
        // Lấy tên nghệ sĩ cho mỗi album
        const artistPromises = data.map(async (album) => {
          const artistName = await artistService.getNameArtistById(album.artistId);
          return { [album.id]: artistName };
        });

        const artistResults = await Promise.all(artistPromises);
        const artistMap = Object.assign({}, ...artistResults); //   Tạo object từ kết quả
        setArtists(artistMap);
      } catch (error) {
        console.error(error.message); // Lưu lỗi nếu xảy ra
      }
    };

    fetchAlbums(); // Gọi hàm lấy dữ liệu
  }, []);

  useEffect(() => {
    const fetchPlayStatus = async () => {
      try {
        if (currentAlbumId === albumData.id) {
          setStatus(playStatus);
        }
      } catch (error) {
        console.error(error.message); // Lưu lỗi nếu xảy ra
      }
    };

    fetchPlayStatus(); // Gọi hàm lấy dữ liệu
  }, [status, playStatus]);


  useEffect(() => {
    if (clickedId && tracks.length > 0 && playCount < 2 && currentIndex >= 0 && tracks[currentIndex]) {
      console.log("Phát nhạc lần:", playCount + 1);
      playWithId(tracks[currentIndex].trackId);  // Phát bài hát theo chỉ số
      setPlayCount((prevCount) => prevCount + 1); // Tăng số lần gọi hàm
    }
  }, [clickedId, tracks, playCount, playWithId, currentIndex]);



  const handleClick = () => {
    if (tracks.length === 0) {
      console.error("Không có bài hát nào trong danh sách.");
      return; // Ngăn không cho tiếp tục nếu không có bài hát
    }

    // Chỉ cập nhật album và phát nhạc nếu là album mới
    if (currentAlbumId !== albumData.id) {
      setCurrentAlbumId(albumData.id); // Cập nhật album hiện tại
      setSongsData(tracks); // Cập nhật danh sách bài hát mới
      setCurrentIndex(0); // Đặt bài hát đầu tiên
      setClickedId(!clickedId); // Đánh dấu đã nhấn play
      setStatus(true); // Đặt trạng thái phát nhạc
      setPlayCount(0); // Đặt lại bộ đếm mỗi khi nhấn play
    } else {
      play();
    }
  };

  const handleClickId = (id) => {
    if (tracks.length === 0) {
      console.error("Không có bài hát nào trong danh sách.");
      return; // Ngăn không cho tiếp tục nếu không có bài hát
    }

    const trackIndex = tracks.findIndex((track) => track.trackId === id); // Tìm vị trí bài hát trong mảng

    if (trackIndex === -1) {
      console.error("Không tìm thấy bài hát với ID:", id);
      return;
    }

    // Chỉ cập nhật album và phát nhạc nếu là album mới
    if (currentAlbumId !== albumData.id) {
      setCurrentAlbumId(albumData.id); // Cập nhật album hiện tại
      setSongsData(tracks); // Cập nhật danh sách bài hát mới
      setCurrentIndex(trackIndex); // Đặt bài hát theo chỉ số tìm thấy
      setClickedId(!clickedId); // Đánh dấu đã nhấn play
      setStatus(true); // Đặt trạng thái phát nhạc
      setPlayCount(0); // Đặt lại bộ đếm mỗi khi nhấn play
      console.log("abc");
    } else {
      playWithId(id); // Phát nhạc nếu là cùng album
    }
  };
  const stop = () => {
    setPlayStatus(!playStatus);
    pause();
  };

  return (
    <>
      <ToastContainer newestOnTop /> {/* Thêm ToastContainer vào đây */}
      <div className="flex flex-col mt-10 gap-8 md:flex-row md:items-end">
        <img className="w-48 rounded" src={albumData.image} alt="" />

        <div className="flex-flex-col">
          <p className="font-semibold">Album</p>
          <h2 className="text-5xl font-bold mb-4 md:text-7xl">{albumData.name}</h2>
          <h4>{artists[albumData.id]}</h4>
          <p className="mt-1">
            <img className="inline-block w-5 mr-1.5 rounded-full" src={assets.logo} alt="" />
            <b>VibeZ</b> • 1.313.336 likes • <b>{tracks.length} songs,</b> about 2 hr 30 min
          </p>
        </div>
      </div>

      <div className="mt-5">
        {status ? (
          <div className="bg-green-600 w-14 h-14 rounded-full flex justify-center relative items-center hover:bg-[#3BE477]" onClick={() => stop()}>
            <FontAwesomeIcon className="text-black text-[20px]" icon={faPause} />
          </div>
        ) : (
          <div className="bg-green-600 w-14 h-14 rounded-full flex justify-center relative items-center hover:bg-[#3BE477]" onClick={handleClick}>
            <FontAwesomeIcon className="text-black text-[20px]" icon={faPlay} />
          </div>
        )}
      </div>

      <div className="grid grid-cols-3 sm:grid-cols-[2fr_1fr_1fr_0.5fr_0.2fr] mt-10 mb-4 pl-2 text-[#a7a7a7]">
        <p><b className="mr-4">#</b>Title</p>
        <p>Album</p>
        <p className="hidden sm:block">Date Added</p>
        <img className="m-auto w-4" src={assets.clock_icon} alt="" />
      </div>

      <hr />

      {
        tracks
          .filter(item => item.pendingApproval === false) // Lọc item có pendingApproval bằng false
          .map((item, index) => (
            <div onClick={() => handleClickId(item.trackId)} key={index} className="grid grid-cols-3 sm:grid-cols-[2fr_1fr_1fr_0.5fr_0.2fr] gap-2 p-2 items-center text-[#a7a7a7] hover:bg-[#ffffff2b] cursor-pointer"
              onMouseOver={() => hoverMenu(index)} // Đặt index hiện tại khi hover
              onMouseOut={handleMouseOut} // Đặt menu về null khi rời chuột
            >
              <p className="text-white flex">
                <b className="mr-4 text-[#a7a7a7]">{index + 1}</b>
                <div>
                  <img className="inline max-w-10 mr-5" src={item.image} alt="" />
                </div>
                <span className="ml-1 truncate">{item.name}</span>
              </p>
              <p className="text-[15px] truncate">{item.albumName}</p>
              <p className="text-[15px] hidden sm:block">{item.createDate}</p>
              <div className="flex justify-center ml-10">
                <p className="text-[15px] text-center">{item.time}</p>
              </div>
              {menu === index && (
                <button className="ml-5">
                  <Dropdown
                    overlay={
                      <Menu className="!bg-[#2A2A2A] text-white !rounded-md">
                        <Menu.Item key="1" className="!p-3 hover:!bg-[#3E3E3E] text-[14px]">
                          <Dropdown
                            overlay={
                              <Menu className="!bg-[#2A2A2A] text-white !rounded-md">
                                <Menu.Item key="1-1" className="!p-3 hover:!bg-[#3E3E3E] text-[14px]" onClick={(e) => togglePopup(e)}>
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
                                      AddTrack(playlist.playlistId, item.trackId, playlist.name);
                                    }}>
                                      <label className="!text-white">{playlist.name}</label>
                                    </Menu.Item>
                                  ))
                                }
                              </Menu>
                            }
                            trigger={['hover']}
                            placement="left"
                            visible={visibleSubMenu}
                            onVisibleChange={(flag) => setVisibleSubMenu(flag)}
                          >
                            <label
                              className="!text-white"
                              onClick={(e) => e.stopPropagation()}
                            >
                              Add to playlist
                            </label>
                          </Dropdown>
                        </Menu.Item>
                        <Menu.Item key="2" className="!p-3 hover:!bg-[#3E3E3E] text-[14px]">
                          <a href="#" className="!text-white">Save to your Liked Songs</a>
                        </Menu.Item>
                        <Menu.Item key="3" className="!p-3 hover:!bg-[#3E3E3E] text-[14px]" onClick={(e) => {
                          e.domEvent.stopPropagation();
                          AddTrackToQueue(item);
                        }}>
                          <a href="#" className="!text-white !hover:!bg-white">Add to queue</a>
                        </Menu.Item>
                      </Menu>
                    }
                    trigger={['click']}
                    placement="bottomRight"
                  >
                    <div onClick={(e) => e.stopPropagation()} className="transition-transform hover:scale-110 hover:opacity-80">
                      <FontAwesomeIcon icon={faEllipsis} />
                    </div>
                  </Dropdown>
                </button>
              )}
            </div>
          ))
      }

      <Popup show={showPopup} onClose={togglePopup} />

    </>
  );
};

export default DisplayAlbum;
