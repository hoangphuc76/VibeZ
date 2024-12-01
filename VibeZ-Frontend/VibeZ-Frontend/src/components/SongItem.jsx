import React, { useContext, useState, useEffect } from "react";
import { PlayerContext } from "../context/PlayerContext";
import trackService from "../services/trackService";

const SongItem = ({ name, image, artistName, id }) => {
  const { playWithId, setSongsData, songsData, setCurrentIndex, currentIndex, setPlayStatus } = useContext(PlayerContext);
  const [clickedTrackId, setClickedTrackId] = useState(null);

  // useEffect để phát nhạc sau khi songsData được cập nhật
  useEffect(() => {
    if (clickedTrackId && songsData.length > 0 && currentIndex == 0) {
      playWithId(clickedTrackId);
      setClickedTrackId(null);  // Đặt lại sau khi phát nhạc
    }
  }, [songsData, clickedTrackId, playWithId]);

  const handleClick = async () => {
    try {
      // Lấy dữ liệu bài hát đề xuất từ trackService
      const data = await trackService.fetchRecommendations(id);
      setSongsData(data); // Cập nhật danh sách bài hát
      setCurrentIndex(0);
      setClickedTrackId(id);
      setPlayStatus(true);  // Đặt bài hát cần phát sau khi dữ liệu được cập nhật
    } catch (error) {
      console.error("Error fetching song recommendations:", error.message);
    } 
  };

  return (
    <div
      onClick={handleClick}
      className="max-w-[180px] p-2 px-3 rounded cursor-pointer hover:bg-[#ffffff26]"
    >
      {/* Hiển thị hình ảnh */}
      <img className="rounded" src={image} alt={name} />

      {/* Hiển thị tên bài hát */}
      <p className="font-bold mt-2 mb-1">
        {name}
      </p>

      {/* Hiển thị tên nghệ sĩ */}
      <p className="text-slate-200 text-sm">
        {artistName}
      </p>
    </div>
  );
};

export default SongItem;
