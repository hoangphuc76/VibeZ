import { createContext, useEffect, useRef, useState } from "react";
import Queue from 'queue-fifo';
import trackService from "../services/trackService";

export const PlayerContext = createContext();

const PlayerContextProvider = (props) => {
  const audioRef = useRef(); // Khởi tạo audio ref
  const seekBg = useRef();
  const seekBar = useRef();

  const [currentIndex, setCurrentIndex] = useState(0); // Index của bài hát hiện tại
  const [songsData, setSongsData] = useState([]); // Lưu danh sách bài hát (ai muốn chỉnh nhạc thì dùng hàm ni set)
  const [track, setTrack] = useState(null); // Bài hát hiện tại
  const [playStatus, setPlayStatus] = useState(false); // Trạng thái phát nhạc
  const [repeat, setRepeat] = useState(false); // Trạng thái lặp lại bài hát
  const [volume, setVolume] = useState(1); // Giá trị mặc định âm lượng
  const [currentAlbumId, setCurrentAlbumId] = useState(null);
  const [status, setStatus] = useState(false);
  const [isLoading, setLoading] = useState(false);
  const [isFollowing, setFollow] = useState(false);
  const [QueueTrack, setQueue] = useState([]);
  const [result, setResult] = useState({});
  const [isLyrics, setLyrics] = useState(false);

  const [time, setTime] = useState({
    currentTime: { second: 0, minute: 0 },
    totalTime: { second: 0, minute: 0 },
  });

  const [recentlyPlayedQueue, setRecentlyPlayedQueue] = useState(new Queue());
  const maxQueueSize = 10;

  // Lấy danh sách bài hát từ API và đặt bài hát đầu tiên
  useEffect(() => {
    const fetchSongs = async () => {
      try {
        if (songsData.length > 0) {
          setTrack(songsData[currentIndex]);
        }
        console.log(songsData);
      } catch (error) {
        console.error("Lỗi khi lấy dữ liệu:", error.message);
      }
    };

    fetchSongs();
  }, [songsData]);



  // Cập nhật trạng thái thanh thời gian
  useEffect(() => {
    if (audioRef.current) {
      audioRef.current.ontimeupdate = () => {
        const currentTime = audioRef.current.currentTime;
        const duration = audioRef.current.duration;

        if (duration) {
          const progress = (currentTime / duration) * 100;
          seekBar.current.style.width = `${progress}%`; // Cập nhật đúng chiều rộng của thanh seekBar

          setTime({
            currentTime: {
              second: Math.floor(currentTime % 60) || 0,
              minute: Math.floor(currentTime / 60) || 0,
            },
            totalTime: {
              second: Math.floor(duration % 60) || 0,
              minute: Math.floor(duration / 60) || 0,
            },
          });
        }
      };
    }
  }, [audioRef, track]);

  // Cập nhật sự kiện khi bài hát kết thúc
  useEffect(() => {
    if (audioRef.current) {
      audioRef.current.onended = async () => {
        if (repeat) {
          // Nếu chế độ lặp lại bật, phát lại bài hát hiện tại
          await audioRef.current.play();
        } else {
          // Nếu không lặp lại, chuyển sang bài hát tiếp theo
          await next();
        }
      };
    }
  }, [track, currentIndex, songsData, repeat]);

  const updateRecentlyPlayedQueue = (trackId) => {
    const recentlyPlayedArray = queueToArray(recentlyPlayedQueue);
    if (!recentlyPlayedArray.includes(trackId)) {
      recentlyPlayedQueue.enqueue(trackId);
      if (recentlyPlayedQueue.size() > maxQueueSize) {
        recentlyPlayedQueue.dequeue();
      }
      const updatedRecentlyPlayedArray = queueToArray(recentlyPlayedQueue);
      localStorage.setItem('RecentlyPlayed', JSON.stringify(updatedRecentlyPlayedArray));
    }
  };

  const queueToArray = (queue) => {
    const array = [];
    const tempQueue = new Queue();
    while (!queue.isEmpty()) {
      const item = queue.dequeue();
      array.push(item);
      tempQueue.enqueue(item);
    }
    while (!tempQueue.isEmpty()) {
      queue.enqueue(tempQueue.dequeue());
    }
    return array;
  };

  const play = () => {
    if (track && audioRef.current) {
      audioRef.current.onended = async () => {
        if (repeat) {
          await audioRef.current.play();
        } else {
          await next();
        }
      };
      audioRef.current.play();
      setPlayStatus(true);
    }
  };

  const pause = () => {
    if (audioRef.current) {
      audioRef.current.pause();
      setPlayStatus(false);
    }
  };

  const playQueue = async (id) => {
    let trackToPlay = null;
    let index = 0;
    if (QueueTrack.length > 0) {
      trackToPlay = QueueTrack.find((t) => t.trackId === id);
      index = QueueTrack.indexOf(trackToPlay);
      QueueTrack.splice(index, 1);
      setQueue(QueueTrack);
    }

    if (trackToPlay && audioRef.current) {
      updateRecentlyPlayedQueue(id);

      trackService.UpdateListener(id).catch((error) => {
        console.error("Error updating listener:", error);
      });

      await setTrack(trackToPlay);
      await audioRef.current.play().then(() => {
        setPlayStatus(true);
      }).catch((error) => {
        console.error("Error playing the track:", error);
        setPlayStatus(false);
      });
    } else {
      console.error("Bài hát không tồn tại hoặc audioRef chưa được khởi tạo!");
    }
  }
  const playWithId = async (id) => {
    let trackToPlay = null;
    let index = 0;
    trackToPlay = songsData.find((t) => t.trackId === id);
    index = songsData.indexOf(trackToPlay);
    setCurrentIndex(index);


    if (trackToPlay && audioRef.current) {
      updateRecentlyPlayedQueue(id);

      trackService.UpdateListener(id).catch((error) => {
        console.error("Error updating listener:", error);
      });

      await setTrack(trackToPlay);
      await audioRef.current.play().then(() => {
        setPlayStatus(true);
      }).catch((error) => {
        console.error("Error playing the track:", error);
        setPlayStatus(false);
      });
    } else {
      console.error("Bài hát không tồn tại hoặc audioRef chưa được khởi tạo!");
    }
  };

  const previous = async () => {
    let previousTrack = null;
    if (currentIndex > 0) {
      const newIndex = currentIndex - 1;
      previousTrack = songsData[newIndex];
      setCurrentIndex(newIndex);
    }

    if (previousTrack) {
      trackService.UpdateListener(previousTrack.trackId).catch((error) => {
        console.error("Error updating listener for previous track:", error);
      });

      setTrack(previousTrack);
      updateRecentlyPlayedQueue(previousTrack.trackId);
      audioRef.current.src = previousTrack.path;
      await audioRef.current.play();
      setPlayStatus(true);
    }
  };

  const next = async () => {
    let nextTrack = null;
    if (QueueTrack.length > 0) {
      nextTrack = QueueTrack[0];
      QueueTrack.shift();
    } else {
      const newIndex = currentIndex + 1;
      nextTrack = songsData[newIndex];
      setCurrentIndex(newIndex);
    }

    if (nextTrack) {
      trackService.UpdateListener(nextTrack.trackId).catch((error) => {
        console.error("Error updating listener for next track:", error);
      });

      setTrack(nextTrack);
      updateRecentlyPlayedQueue(nextTrack.trackId);
      audioRef.current.src = nextTrack.path;
      audioRef.current.load(); // Tải lại bài hát mới
      await audioRef.current.play();
      setPlayStatus(true);
    }
  };

  const seekSong = async (e) => {
    if (audioRef.current && audioRef.current.duration) {
      const newTime = (e.nativeEvent.offsetX / seekBg.current.offsetWidth) * audioRef.current.duration;
      audioRef.current.currentTime = newTime;
      setTimeout(() => {
        if (audioRef.current.currentTime !== newTime) {
          audioRef.current.currentTime = newTime;
        }
      }, 100);
    }
  };

  const toggleRepeat = () => {
    setRepeat(!repeat);
  };

  const handleVolumeChange = (event) => {
    const newVolume = event.target.value / 100;
    setVolume(newVolume);
    if (audioRef.current) {
      audioRef.current.volume = newVolume;
    }
  };

  useEffect(() => {
    if (audioRef.current) {
      audioRef.current.volume = volume;
    }
  }, [volume]);

  const contextValue = {
    audioRef,
    seekBg,
    seekBar,
    track,
    setTrack,
    playStatus,
    setPlayStatus,
    time,
    setTime,
    play,
    pause,
    playWithId,
    previous,
    next,
    seekSong,
    currentIndex,
    setCurrentIndex,
    setSongsData,
    songsData,
    repeat,
    toggleRepeat,
    handleVolumeChange,
    setVolume,
    volume,
    currentAlbumId,
    setCurrentAlbumId,
    status,
    setStatus,
    isLoading,
    setLoading,
    isFollowing,
    setFollow,
    setQueue,
    QueueTrack,
    result,
    setResult,
    playQueue,
    isLyrics,
    setLyrics
  };

  return (
    <PlayerContext.Provider value={contextValue}>
      {props.children}
    </PlayerContext.Provider>
  );
};

export default PlayerContextProvider;
