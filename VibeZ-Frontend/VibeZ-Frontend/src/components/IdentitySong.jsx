import React, { useContext, useState, useEffect } from "react";
import axios from "axios";
import { ReactMediaRecorder } from "react-media-recorder";
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faMicrophone } from '@fortawesome/free-solid-svg-icons';
import { PlayerContext } from "../context/PlayerContext";

function IdentitySong({ onCaptionDetect }) {
  const [songInfo, setSongInfo] = useState(null);
  const { isLoading, setLoading } = useContext(PlayerContext);
  const [isRecording, setIsRecording] = useState(false);
  const [mediaBlobUrl, setMediaBlobUrl] = useState(null);
  const [isRecord, setRecorde] = useState(false);

  const identifySong = async (audioFile) => {
    if (!audioFile) {
      alert("Vui lòng chọn hoặc ghi âm file âm thanh trước khi nhận diện.");
      return;
    }

    setLoading(true);
    try {
      const formData = new FormData();
      formData.append("upload_file", audioFile, "recording.mp3");

      const response = await axios.post("https://shazam-api6.p.rapidapi.com/shazam/recognize/", formData, {
        headers: {
          'x-rapidapi-key': '',
          'x-rapidapi-host': 'shazam-api6.p.rapidapi.com',
        }
      });
      setSongInfo(response.data);
      const caption = response.data.track.title; // Lấy caption là tiêu đề bài hát
      onCaptionDetect(caption); 
      console.log("Kết quả nhận dạng bài hát:", response.data);
    } catch (error) {
      if (error.response) {
        console.error("Lỗi từ API:", error.response.status);
        console.error("Chi tiết lỗi:", error.response.data);
      } else {
        console.error("Lỗi khác:", error.message);
      }
    } finally {
      setLoading(false);
    }
  };

  const toggleRecording = (startRecording, stopRecording) => {
    if (isRecording) {
      alert("Dừng ghi âm");
      setRecorde(false);
      stopRecording();
    } else {
      alert("Bắt đầu ghi âm");
      setRecorde(true);
      startRecording();
    }
    setIsRecording(!isRecording); // Đảo ngược trạng thái ghi âm
  };

  // Theo dõi mediaBlobUrl và gọi identifySong khi có mediaBlobUrl mới
  useEffect(() => {
    if (mediaBlobUrl) {
      const fetchAudioFile = async () => {
        const audioBlob = await fetch(mediaBlobUrl).then((res) => res.blob());
        const audioFile = new File([audioBlob], "recording.mp3", { type: "audio/mp3" });
        identifySong(audioFile);  // Gọi identifySong khi có file ghi âm
      };
      fetchAudioFile();
    }
  }, [mediaBlobUrl]);

  return (
    <div className="App">
      <ReactMediaRecorder
        audio
        onStop={(blobUrl) => setMediaBlobUrl(blobUrl)} // Cập nhật mediaBlobUrl sau khi dừng ghi âm
        render={({ startRecording, stopRecording }) => (
          <div>
            <div
              className={` w-10 h-10 rounded-full flex justify-center items-center cursor-pointer  ${isRecord ? `bg-[#999999]` : `bg-[#333333]`} hover:bg-[#999999]` }
              onClick={() => toggleRecording(startRecording, stopRecording)}
            >
              <FontAwesomeIcon className="text-white" icon={faMicrophone} />
            </div>
          </div>
        )}
      />

    </div>
  );
}

export default IdentitySong;
