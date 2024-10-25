import React, { useContext, useEffect, useState, useRef } from "react";
import { useParams } from "react-router-dom";
import { albumsData, assets, songsData } from "../assets/assets";
import { PlayerContext } from "../context/PlayerContext";
import Navbar from "./Navbar";
import albumService from "../services/albumService";
import trackService from "../services/trackService";
import artistService from "../services/artistService";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faPlay, faPause } from "@fortawesome/free-solid-svg-icons";
import eventBus from "../services/eventBus"

const DisplayAlbum = () => {
  const { id } = useParams();
  const { 
    playWithId, setSongsData, setCurrentIndex, setPlayStatus, 
    playStatus, play, pause, setCurrentAlbumId, currentAlbumId, currentIndex, track 
  } = useContext(PlayerContext);
  
  const [albumData, setAlbums] = useState({});
  const [artists, setArtists] = useState({});
  const [tracks, setTracks] = useState([]);
  const [clickedId, setClickedId] = useState(false);
  const [status, setStatus] = useState(false);
  const [playCount, setPlayCount] = useState(0); // State để đếm số lần gọi playWithI

  useEffect(() => {
    const fetchAlbums = async () => {
      try {
        const data = await albumService.getAlbumsById(id); // Gọi API lấy dữ liệu album
        setAlbums(data); // Lưu dữ liệu vào state albums
        const trackk = await trackService.getAllTrackByAlbumId(id);
        setTracks(trackk);
        console.log(currentAlbumId);
        if (currentAlbumId === data.id) {
            setStatus(playStatus);
        }
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
  }, [id, status, playStatus]);

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

      <div className="grid grid-cols-3 sm:grid-cols-4 mt-10 mb-4 pl-2 text-[#a7a7a7]">
        <p><b className="mr-4">#</b>Title</p>
        <p>Album</p>
        <p className="hidden sm:block">Date Added</p>
        <img className="m-auto w-4" src={assets.clock_icon} alt="" />
      </div>

      <hr />

      {tracks.map((item, index) => (
        <div onClick={() => handleClickId(item.trackId)} key={index} className="grid grid-cols-3 sm:grid-cols-4 gap-2 p-2 items-center text-[#a7a7a7] hover:bg-[#ffffff2b] cursor-pointer">
          <p className="text-white flex">
            <b className="mr-4 text-[#a7a7a7]">{index + 1}</b>
            <div>
              <img className="inline w-10 mr-5" src={item.image} alt="" />
            </div>
            <span className="ml-1">{item.name}</span>
          </p>
          <p className="text-[15px]">{albumData.name}</p>
          <p className="text-[15px] hidden sm:block">{item.createDate}</p>
          <p className="text-[15px] text-center">{item.time}</p>
        </div>
      ))}
    </>
  );
};

export default DisplayAlbum;
