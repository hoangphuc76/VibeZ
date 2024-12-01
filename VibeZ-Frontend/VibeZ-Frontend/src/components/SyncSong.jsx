import { useContext, useEffect, useRef, useState } from "react";
import clsx from "clsx";
import { PlayerContext } from "../context/PlayerContext"; // Đảm bảo đường dẫn đúng
import { useParams } from "react-router-dom";

function convertLrcToJSON(lrcText) {
  const lines = lrcText
    .split('\n')
    .filter(line => line.trim())
    .map(line => {
      const timeMatch = line.match(/\[(\d+):(\d+\.\d+)\]/);
      const words = line.replace(/\[\d+:\d+\.\d+\]/, '').trim();

      if (timeMatch) {
        const minutes = parseInt(timeMatch[1], 10);
        const seconds = parseFloat(timeMatch[2]);
        const startTimeMs = Math.floor((minutes * 60 + seconds) * 1000);

        return { startTimeMs, words };
      }
      return null;
    })
    .filter(line => line !== null);

  return { lines };
}

function SyncSong() {
  const { audioRef, time, track } = useContext(PlayerContext); // Lấy thời gian từ PlayerContext
  const [lyrics, setLyricsData] = useState(null);
  const currentLineRef = useRef(null);

  // Fetch dữ liệu từ link và chuyển đổi
  useEffect(() => {
    fetch(track.trackLRC)
      .then(response => response.text())
      .then(data => {
        const lyricsJSON = convertLrcToJSON(data);
        setLyricsData(lyricsJSON); // Cập nhật lyrics vào state
      })
      .catch(error => console.error("Error fetching LRC file:", error));
  }, []);

  // Tính toán currentTimeMs dựa trên thời gian phát nhạc hiện tại
  const currentTimeMs = (time.currentTime.minute * 60 + time.currentTime.second) * 1000; // Chuyển đổi thời gian thành milliseconds
  const currentLine = lyrics?.lines.find(
    (line, index) =>
      Number(line.startTimeMs) <= currentTimeMs &&
      (index === lyrics.lines.length - 1 || Number(lyrics.lines[index + 1].startTimeMs) > currentTimeMs)
  );

  // Cuộn đến dòng hiện tại khi thay đổi
  useEffect(() => {
    if (currentLineRef.current) {
      currentLineRef.current.scrollIntoView({
        behavior: "smooth",
        block: "center",
      });
    }
  }, [currentLine]);

  // Seek nhạc khi bấm vào dòng lyrics
  const handleLineClick = (startTimeMs) => {
    if (audioRef.current) {
      audioRef.current.currentTime = startTimeMs / 1000; // Đặt lại thời gian phát nhạc theo milliseconds
    }
  };

  return (
    <main className="grid gap-5 justify-center p-5 ">
      {lyrics?.lines.map((line) => {
        const isCurrentLine = currentLine?.startTimeMs === line.startTimeMs;
        const isPastLine = Number(line.startTimeMs) < currentTimeMs;

        return (
          <p
            ref={isCurrentLine ? currentLineRef : null}
            onClick={() => handleLineClick(line.startTimeMs)} // Sự kiện onClick để seek
            className={clsx(
              "text-[35px] font-bold transition-colors duration-300 cursor-pointer", // Thêm cursor-pointer để hiển thị rằng có thể click
              isCurrentLine
                ? "text-white"
                : isPastLine
                ? "text-gray-400"
                : "text-black"
            )}
            key={line.startTimeMs}
          >
            {line.words}
          </p>
        );
      })}
    </main>
  );
}

export default SyncSong;
