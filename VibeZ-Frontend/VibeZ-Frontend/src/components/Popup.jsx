import React, { useRef, useState, useEffect, useContext } from 'react';
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faMusic, faPen, faEllipsis } from '@fortawesome/free-solid-svg-icons';
import playlistService from '../services/playlistService';
import { ClipLoader } from 'react-spinners'; // Thêm spinner loader từ thư viện react-spinners
import { LoginContext } from '../context/LoginContext';

const Popup = ({ show, onClose }) => {
    const fileInputRef = useRef(null);
    const nameInputRef = useRef(null);
    const descriptionInputRef = useRef(null);
    const popupRef = useRef(null);
    const [userId, setUserId] = useState();
    const [username, setUserName] = useState();
    const { isChange, setChange } = useContext(LoginContext);
    const [isNameFocused, setIsNameFocused] = useState(false);
    const [isDescriptionFocused, setIsDescriptionFocused] = useState(false);
    const [isPen, setIsPen] = useState(false);
    const [isDelete, setIsDelete] = useState(false);
    const [loading, setLoading] = useState(false); // Thêm state để quản lý trạng thái loading

    const handleImageClick = () => {
        fileInputRef.current.click();
    };

    useEffect(() => {
        const user = JSON.parse(localStorage.getItem('userId'));
        setUserId(user);
        const userName = JSON.parse(localStorage.getItem('username'));
        setUserName(userName);
    }, []);

    const handleSubmit = async (e) => {
        e.preventDefault();

        // Lấy giá trị từ các thẻ input qua ref
        const name = nameInputRef.current.value;
        const description = descriptionInputRef.current.value;
        const selectedFile = fileInputRef.current.files[0];

        try {
            setLoading(true); // Hiển thị loading spinner
            const libId = JSON.parse(localStorage.getItem('libId'));
            const response = await playlistService.createPlaylist(name, description, username, selectedFile, userId);
            if (response) {
                const responseLib = await playlistService.createLibrary_playlist(libId, response.playlistId);
                if (responseLib) {
                    setTimeout(() => {
                        setChange(!isChange); // Thay đổi trạng thái
                        onClose(); // Đóng popup sau khi chờ 1-2 giây để đảm bảo API có thời gian cập nhật
                    }, 1000); // Chờ 2 giây để đảm bảo dữ liệu đã được cập nhật trong cơ sở dữ liệu
                }
            }


        } catch (error) {
            setLoading(false); // Ẩn loading nếu có lỗi
            console.error("Error creating playlist:", error);
        }
    };

    useEffect(() => {
        function handleClickOutside(event) {
            if (popupRef.current && !popupRef.current.contains(event.target)) {
                onClose();
            }
        }

        document.addEventListener('mousedown', handleClickOutside);
        return () => {
            document.removeEventListener('mousedown', handleClickOutside);
        };
    }, [popupRef, onClose]);

    if (!show) return null;

    return (
        <>
            <div
                className="fixed inset-0 bg-black bg-opacity-50 backdrop-blur-sm z-40"
                onClick={onClose}
            ></div>

            <form className="fixed inset-0 flex items-center justify-center z-50 w-full" onSubmit={handleSubmit}>
                <div
                    className="bg-[#1C1C1C] p-6 rounded-lg shadow-lg max-w-lg w-full max-h-80 h-full"
                    ref={popupRef}
                    onClick={(e) => e.stopPropagation()}
                >
                    <h2 className="text-[25px] text-white font-bold mb-4 font-sans">Details</h2>

                    <div className="flex relative">
                        <div
                            className="cursor-pointer bg-[#282828] shadow-2xl h-44 w-44 rounded-md flex items-center justify-center mb-4 relative"
                            onClick={handleImageClick}
                            onMouseEnter={() => setIsPen(true)}
                            onMouseLeave={() => setIsPen(false)}
                        >
                            <FontAwesomeIcon className='absolute top-1 right-2 text-white' icon={faEllipsis} onClick={(e) => {
                                e.stopPropagation();
                                setIsDelete(!isDelete);
                            }} />

                            {
                                isPen ? (
                                    <div className='flex flex-col'>
                                        <FontAwesomeIcon className='w-10 h-10 text-white m-auto' icon={faPen} />
                                        <p className='text-white'>Choose photo</p>
                                    </div>
                                ) : (
                                    <FontAwesomeIcon className='w-12 h-12 text-[#3E3E3E]' icon={faMusic} />
                                )
                            }
                        </div>
                        {
                            isDelete && (
                                <div className='absolute top-7 left-[22%] z-10 bg-[#1C1C1C] py-2 px-4 hover:bg-[#282828]'>
                                    <ul className=' cursor-pointer'>
                                        <li onClick={() => fileInputRef.current.value = null}>Remove photo</li>
                                    </ul>
                                </div>
                            )
                        }

                        <div className='flex flex-col ml-3 w-[60%]'>

                            <div className="relative mb-4">
                                <input
                                    id="name"
                                    className="block p-4 pt-6 w-full text-sm text-white bg-[#3E3E3E] rounded appearance-none focus:outline-none focus:ring-0"
                                    placeholder="Enter name"
                                    ref={nameInputRef}
                                    onFocus={() => setIsNameFocused(true)}
                                    onBlur={(e) => !e.target.value && setIsNameFocused(false)}
                                />
                                <label
                                    htmlFor="name"
                                    className={`absolute text-sm text-gray-400 duration-300 transform ${isNameFocused ? '-translate-y-4 scale-75 opacity-100' : 'translate-y-0 scale-100 opacity-0'} top-2 left-4 origin-[0] text-gray-200 font-bold`}
                                >
                                    Name
                                </label>
                            </div>

                            <div className="relative">
                                <input
                                    id="description"
                                    className="block p-4 pt-6 w-full text-sm text-white bg-[#3E3E3E] rounded appearance-none focus:outline-none focus:ring-0"
                                    placeholder="Add an optional description"
                                    ref={descriptionInputRef}
                                    onFocus={() => setIsDescriptionFocused(true)}
                                    onBlur={(e) => !e.target.value && setIsDescriptionFocused(false)}
                                />
                                <label
                                    htmlFor="description"
                                    className={`absolute text-sm text-gray-400 duration-300 transform ${isDescriptionFocused ? '-translate-y-4 scale-75 opacity-100' : 'translate-y-0 scale-100 opacity-0'} top-2 left-4 origin-[0] text-gray-200 font-bold`}
                                >
                                    Description
                                </label>
                            </div>
                            <div className='flex justify-end items-center'>
                                <button type='submit' className='text-black bg-white mt-[8%] w-24 h-10 rounded-3xl hover:w-28 transition-all duration-200'>
                                    {loading ? <ClipLoader color="#000" size={20} /> : "Save"}
                                </button>
                            </div>
                        </div>

                        <input
                            accept="image/.jpg, image/.jpeg, image/.png"
                            type="file"
                            ref={fileInputRef}
                            style={{ display: 'none' }}
                        />
                    </div>
                </div>
            </form>
        </>
    );
};

export default Popup;
