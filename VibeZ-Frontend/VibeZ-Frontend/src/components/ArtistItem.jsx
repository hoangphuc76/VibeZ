import React from "react";
import { useNavigate } from "react-router-dom";

const ArtistItem = ({image, name, desc, id}) => {

   const navigate = useNavigate()

  return (
    <div>
      <div onClick={() => navigate(`/artist/${id}`)} className="max-w-[170px] p-2 px-3 rounded cursor-pointer hover:bg-[#ffffff26]">
        <img className="rounded-full" src={image} alt="" />
        <p className="font-bold mt-2 mb-1">{name}</p>
        <p className="text-slate-200 text-sm">{desc}</p>
      </div>
    </div>
  )
}

export default ArtistItem;