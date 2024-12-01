import React from "react";
import { useNavigate } from "react-router-dom";
import { useState, useEffect } from "react";
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faPlay, faPause } from "@fortawesome/free-solid-svg-icons";
import eventBus from "../services/eventBus";
const AlbumItem = ({ image, name, createBy ,id}) => {

    const navigate = useNavigate()
    const [isMouseOver, setMouseOver] = useState(false);




    return (
        <div>
            <div onClick={() => navigate(`/playlist/${id}`)} className="max-w-[220px] p-2 px-3 rounded cursor-pointer hover:bg-[#ffffff26]" onMouseOver={() => setMouseOver(true)} onMouseOut={() => setMouseOver(false)}>
                <div className="relative">
                    <img className="rounded" src={image} alt="" />
                    {isMouseOver && (
                        <div className="bg-green-600 w-14 h-14 rounded-full flex justify-center absolute items-center bottom-1 right-1 hover:bg-[#3BE477]"  >
                            <FontAwesomeIcon className="text-black text-[20px]" icon={faPlay} />
                        </div>
                    )
                    }

                </div>
                <p className="font-bold mt-2 mb-1">{name}</p>
                <p className="text-gray-400 text-[14px] hover:text-white hover:underline hover:decoration-1">{createBy}</p>
            </div>
        </div>
    )
}

export default AlbumItem;