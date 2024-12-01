import React, { useContext, useState, useEffect, useRef } from "react";
import { useParams } from "react-router-dom";
// Assets Connection
import { assets, songsData } from "../assets/assets";
// Context Connection
import { PlayerContext } from "../context/PlayerContext";
import artistService from "../services/artistService";
import playlistService from "../services/playlistService";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faPlay, faPause, faEllipsis } from "@fortawesome/free-solid-svg-icons";
import { LoginContext } from "../context/LoginContext";

const DisplayArtist = () => {

  const { id } = useParams()
  const [artistData, setArtist] = useState([]);
  const {
    playWithId, setSongsData, setCurrentIndex, setPlayStatus,
    playStatus, play, pause, setCurrentAlbumId, currentAlbumId, currentIndex, setLoading,
  } = useContext(PlayerContext);
  const { isChange, setChange } = useContext(LoginContext);
  const [tracks, setTracks] = useState([]);
  const [isFollowing, setFollow] = useState();
  const [status, setStatus] = useState(false);
  const [clickedId, setClickedId] = useState(false);
  const [playCount, setPlayCount] = useState(0);
  const [showMenu, setShowMenu] = useState(false);

  useEffect(() => {
    const fetchArtist = async () => {
      try {
        const data = await artistService.getArtistById(id); // Gọi API lấy dữ liệu album
        setArtist(data); // Lưu dữ liệu vào state albums
        console.log(data);
        const track = await artistService.getAllTrackByArtistId(id);
        setTracks(track);

      } catch (error) {
        console.error(error.message); // Lưu lỗi nếu xảy ra
      }
    };

    fetchArtist(); // Gọi hàm lấy dữ liệu
  }, [id]);

  useEffect(() => {
    const fetchFollow = async () => {
      try {
        const libId = JSON.parse(localStorage.getItem('libId'));
        const isFollow = await artistService.getFollowArtist(libId, id);
        console.log(isFollow)
        if (isFollow) {
          setFollow(true);
        } else {
          setFollow(false);
        }
      } catch(error) {
        console.error(error.message); // Lưu lỗi nếu xảy ra

      }
    }
    fetchFollow();
  }, [id]);
  useEffect(() => {
    const fetchPlayStatus = async () => {
      try {
        if (currentAlbumId === artistData.id) {
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
    if (currentAlbumId !== artistData.id) {
      setCurrentAlbumId(artistData.id); // Cập nhật album hiện tại
      setSongsData(tracks); // Cập nhật danh sách bài hát mới
      setCurrentIndex(0); // Đặt bài hát đầu tiên
      setClickedId(!clickedId); // Đánh dấu đã nhấn play
      setStatus(true); // Đặt trạng thái phát nhạc
      setPlayCount(0); // Đặt lại bộ đếm mỗi khi nhấn play
    } else {
      play();
    }
  };

  const handleFollow = async () => {
    try {
      setLoading(true);
      const libId = JSON.parse(localStorage.getItem('libId'));
      const userId = JSON.parse(localStorage.getItem('userId'));
      const respone = await artistService.followArtist(libId, id);
      const respone1 = await artistService.follow(userId, id);

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

  const handleUnfollowAritst = async () => {
    try {
      const userId = JSON.parse(localStorage.getItem('userId'));
      const libId = JSON.parse(localStorage.getItem('libId'));
      const result = await artistService.unfollowArtist(id, libId);
      const result2 = await artistService.unfollow(userId, id);
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
    if (currentAlbumId !== artistData.id) {
      setCurrentAlbumId(artistData.id); // Cập nhật album hiện tại
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
      <div
        className="flex flex-col mt-10 gap-8 md:flex-row md:items-end bg-cover bg-center bg-no-repeat relative rounded-lg overflow-hidden"
        style={{ backgroundImage: `url(${artistData.imgBackground})`, height: '400px' }}
      >
        <div className="relative z-10 p-6">
          <p className="inline-flex items-center text-white">
            <span className="mr-2">
              {/* <img src={verified_icon} alt="" /> */}
            </span>
            Verified Artist
          </p>
          <h2 className="text-5xl font-bold mb-4 md:text-7xl text-white">{artistData.name}</h2>
        </div>
      </div>
      <div className="mt-5 flex gap-5 relative">
        {status ? (
          <div className="bg-green-600 w-14 h-14 rounded-full flex justify-center relative items-center hover:bg-[#3BE477] transition-transform  hover:scale-110 hover:opacity-80" onClick={() => stop()}>
            <FontAwesomeIcon className="text-black text-[20px]" icon={faPause} />
          </div>
        ) : (
          <div className="bg-green-600 w-14 h-14 rounded-full flex justify-center relative items-center hover:bg-[#3BE477] transition-transform  hover:scale-110 hover:opacity-80" onClick={handleClick}>
            <FontAwesomeIcon className="text-black text-[20px]" icon={faPlay} />
          </div>
        )}
        <div className="flex items-center">
          {isFollowing ? (
            <button className="transition-transform border-2 border-gray-400 hover:border-white hover:scale-110 hover:opacity-80 px-4 rounded-full text-sm font-semibold h-10" onClick={handleUnfollowAritst}>
              Following
            </button>
          ) : (
            <button className="transition-transform border-2 border-gray-400 hover:border-white hover:scale-110 hover:opacity-80 px-4 rounded-full text-sm font-semibold h-10" onClick={handleFollow}>
              Follow
            </button>
          )
          }
        </div>
        <div className="flex items-center">
          <button className="transition-transform  hover:scale-110 hover:opacity-80 px-4 rounded-full text-sm font-semibold h-10" onClick={() => setShowMenu(!showMenu)}>
            <FontAwesomeIcon icon={faEllipsis} />
          </button>
          {showMenu && (
            <div className='absolute top-[100%] left-[16%] bg-[#2A2A2A] shadow-lg rounded-md p-2  text-white z-50'>
              <ul>
                <li className="p-3 hover:bg-[#3E3E3E] text-[14px]">
                  UnFollow
                </li>
                <li className="p-3 hover:bg-[#3E3E3E] text-[14px]">
                  Don't play this artist
                </li>
              </ul>
            </div>
          )
          }
        </div>

      </div>


      <div className="grid grid-cols-3 sm:grid-cols-4 mt-10 mb-4 pl-2 text-[#a7a7a7]">
        <h2 className="text-white text-2xl font-bold tracking-wide">Popular</h2>
      </div>

      <hr />

      {
        tracks.map((item, index) => (
          <div onClick={() => handleClickId(item.trackId)} key={index} className="grid grid-cols-3 sm:grid-cols-4 gap-2 p-2 items-center text-[#a7a7a7] hover:bg-[#ffffff2b] cursor-pointer">
            <p className="text-white">
              <b className="mr-4 text-[#a7a7a7]">{index + 1}</b>
              <img className="inline w-10 mr-5" src={item.image} alt="" />
              {item.name}
            </p>
            {/* <p className='text-[15px]'>{artistData.name}</p> */}
            <p className="text-[15px] hidden sm:block">{item.listener}</p>
            <p className="text-[15px] text-center">{item.time}</p>
          </div>
        ))
      }
    </>
  )
}
export default DisplayArtist;