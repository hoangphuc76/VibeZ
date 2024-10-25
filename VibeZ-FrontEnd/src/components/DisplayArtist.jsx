import React, { useContext, useState, useEffect, useRef } from "react";
import { useParams } from "react-router-dom";
// Assets Connection
import { assets, songsData } from "../assets/assets";
// Context Connection
import { PlayerContext } from "../context/PlayerContext";
import artistService from "../services/artistService";
import playlistService from "../services/playlistService";
// Component Connection
import Navbar from "./Navbar";
// import verified_icon from "../assets/verified-icon.svg";
// import play_artist from "../assets/play-album-artist.svg";
const DisplayArtist = () => {

  const { id } = useParams()
  const [artistData, setArtist] = useState([]);
  const { playWithId, setSongsData } = useContext(PlayerContext);
  const [tracks, setTracks] = useState([]);
  const previousTracks = useRef([]);


  useEffect(() => {
    const fetchArtist = async () => {
      try {
        const data = await artistService.getArtistById(id); // Gọi API lấy dữ liệu album
        console.log(data);
        setArtist(data); // Lưu dữ liệu vào state albums
        const track = await artistService.getAllTrackByArtistId(id);
        console.log(track);
        setTracks(track);
      } catch (error) {
        console.error(error.message); // Lưu lỗi nếu xảy ra
      } 
    };

    fetchArtist(); // Gọi hàm lấy dữ liệu
  }, []);
  useEffect(() => {
    if (previousTracks.current.length > 0 && previousTracks.current !== tracks) {
      console.log("Tracks đã thay đổi!");
      console.log("Tracks cũ:", previousTracks.current);
      console.log("Tracks mới:", tracks);
    }

    // Cập nhật phiên bản trước đó của tracks
    setSongsData(tracks);

  }, [tracks]);
  useEffect(() => {
    const saveTrackList = () => {
      setSongsData(tracks);
    };
    saveTrackList();
  }, [tracks]); // Phụ thuộc vào tracks

  return (
    <>
      <div
        className="flex flex-col mt-10 gap-8 md:flex-row md:items-end bg-cover bg-center bg-no-repeat relative rounded-lg overflow-hidden"
        style={{ backgroundImage: `url(${artistData.image})`, height: '400px' }}
      >
        <div className="relative z-10 p-6">
          <p className="inline-flex items-center text-white">
            <span className="mr-2">
              {/* <img src={verified_icon} alt="" /> */}
            </span>
            Verified Artist
          </p>
          <h2 className="text-5xl font-bold mb-4 md:text-7xl text-white">{artistData.name}</h2>
          <h4 className="text-white">1000</h4>
        </div>
      </div>

      <div className="mt-4 flex items-center space-x-6"> {/* Tăng khoảng cách giữa các nút */}
        <button className="transition-transform transform hover:scale-110 hover:opacity-80">
          {/* <img src={play_artist} alt="Play Button" /> */}
        </button>
        <button className="transition-transform border-2 border-gray-400 hover:border-white hover:scale-110 hover:opacity-80 px-2 py-1 rounded-full text-sm">
          Follow
        </button>
      </div>


      <div className="grid grid-cols-3 sm:grid-cols-4 mt-10 mb-4 pl-2 text-[#a7a7a7]">
        <h2 className="text-white text-2xl font-bold tracking-wide">Popular</h2>
      </div>

      <hr />

      {
        tracks.map((item, index) => (
          <div onClick={() => playWithId(item.trackId)} key={index} className="grid grid-cols-3 sm:grid-cols-4 gap-2 p-2 items-center text-[#a7a7a7] hover:bg-[#ffffff2b] cursor-pointer">
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