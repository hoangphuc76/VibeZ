import React, { useContext, useState, useEffect } from "react";
import { PlayerContext } from "../context/PlayerContext";
import trackService from "../services/trackService";
import libraryService from "../services/libraryService";
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faEllipsis, faPlus } from '@fortawesome/free-solid-svg-icons';
import { Dropdown, Menu } from 'antd';

const SongItemInSearch = ({ name, image, artistName, id, index, time }) => {
    const { playWithId, setSongsData, songsData, setCurrentIndex, currentIndex, setPlayStatus } = useContext(PlayerContext);
    const [clickedTrackId, setClickedTrackId] = useState(null);
    const [isMenu, setMenu] = useState(false);
    const [indexx, setIndex] = useState(null);
    const [visibleSubMenu, setVisibleSubMenu] = useState(false);
    const [visibleMain, setVisibleMain] = useState(false);
    const [showPopup, setShowPopup] = useState(false);
    const [playlist, setPlaylist] = useState([]);


    
    const displayMenu = (index) => {
        setIndex(index);
        setMenu(true);
    }
    useEffect(() => {
        const getPlaylist = async () => {
            try {
                const libId = JSON.parse(localStorage.getItem('libId'));
                const playlist = await libraryService.getPlaylistsByLibraryId(libId);
                setPlaylist(playlist);
            } catch (error) {
                console.error(error.message);
            }
        };
        getPlaylist();
    }, []);


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

    const togglePopup = () => setShowPopup(!showPopup);

    return (
        <div key={index} className='flex w-[100%] cursor-pointer hover:bg-[#3E3E3E] p-2 hover:rounded'
            onMouseOver={() => displayMenu(index)}
            onMouseOut={() => setMenu(false)}
            onClick={handleClick}
        >
            <img className='rounded h-full max-w-[50px] max-h-[50px]' src={image} alt={name || 'Unknown'} />
            <div className='flex flex-col ml-2 w-9/12'>
                <p className='text-[16px] font-semibold truncate'>{name || 'No Title'}</p>
                <span className='text-gray-400 text-[14px]'>{artistName || 'Unknown Artist'}</span>
            </div>
            <p className='text-gray-400 w-2/12'>{time || '--:--'}</p>
            {isMenu && indexx === index && (
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
                    <div onClick={(e) => e.stopPropagation()} className="transition-transform  hover:scale-110 hover:opacity-80">
                        <FontAwesomeIcon icon={faEllipsis} />
                    </div>
                </Dropdown>
            )
            }

        </div>
    );
};

export default SongItemInSearch;
