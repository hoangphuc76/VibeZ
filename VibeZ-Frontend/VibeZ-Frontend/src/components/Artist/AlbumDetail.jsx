import React, { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom"; // Import useNavigate
import { assets } from "../../assets/assets";
import albumService from "../../services/albumService";
import trackService from "../../services/trackService";
import artistService from "../../services/artistService";
import { FiEdit, FiTrash, FiPlusCircle } from "react-icons/fi"; // Using react-icons for edit, delete, and add icons

const AlbumDetail = () => {
  const { id } = useParams();
  const navigate = useNavigate(); 
  const [albumData, setAlbumData] = useState({});
  const [artists, setArtists] = useState({});
  const [tracks, setTracks] = useState([]);

  useEffect(() => {
    const fetchAlbumData = async () => {
      try {
        const data = await albumService.getAlbumsById(id);
        setAlbumData(data);
        const trackList = await trackService.getAllTrackByAlbumId(id);
        setTracks(trackList);

        const artistPromises = data.map(async (album) => {
          const artistName = await artistService.getNameArtistById(album.artistId);
          return { [album.id]: artistName };
        });

        const artistResults = await Promise.all(artistPromises);
        const artistMap = Object.assign({}, ...artistResults);
        setArtists(artistMap);
      } catch (error) {
        console.error(error.message);
      }
    };

    fetchAlbumData();
  }, [id]);

  // Function to navigate to the CreateTrack page
  const handleAddTrackClick = () => {
    navigate(`/artistdashboard/music/album/${id}/create-track`); 
  };

  const handleEditTrackClick = (trackId) => {
    navigate(`/artistdashboard/music/album/${id}/edit-track/${trackId}`); // Navigate with trackId
  };

  const handleDeleteTrack = async (trackId) => {
    try {
      await trackService.deleteTrack(trackId); 
      setTracks(tracks.filter(track => track.trackId !== trackId)); // Update UI by removing the deleted track
      alert("Track deleted successfully!");
    } catch (error) {
      console.error("Error deleting track", error);
      alert("Failed to delete track.");
    }
  };  

  return (
    <div className="min-h-screen">
      {/* Header Section */}
      <div className="bg-black text-white px-4 py-10">
        <div className="flex flex-col gap-8 md:flex-row md:items-end">
          <img className="w-64 rounded-lg shadow-2xl" src={albumData.image} alt="Album Cover" />

          <div className="flex flex-col md:flex-grow">
            <p className="font-semibold text-gray-400">Album</p>
            <h2 className="text-5xl font-bold mb-4 md:text-7xl">{albumData.name}</h2>
            <h4 className="text-lg text-gray-300">{artists[albumData.id]}</h4>
            <p className="mt-1 text-gray-400">
              <img className="inline-block w-5 mr-1.5 rounded-full" src={assets.logo} alt="VibeZ logo" />
              <b>VibeZ</b> • 1,313,336 likes • <b>{tracks.length} songs</b>, about 2 hr 30 min
            </p>
          </div>

          {/* Add Track Icon (Now Clickable to Navigate) */}
          <div className="flex items-center justify-end w-full mt-4 md:mt-0">
            <button
              onClick={handleAddTrackClick} // Navigate when clicked
              className="flex items-center text-white text-lg hover:text-green-400 transition-all duration-200"
            >
              <FiPlusCircle className="mr-2" size={20} /> Add Track
            </button>
          </div>
        </div>
      </div>

      {/* Track List Section */}
      <div className="bg-white text-black px-4 py-10">
        <div className="flex justify-between items-center mb-4">
          <div className="grid grid-cols-4 gap-4 pl-2 w-full">
            <p className="font-semibold text-lg">No</p>
            <p className="ml-4 font-semibold text-lg">Title</p>
            <p className="hidden sm:block text-center font-semibold text-lg">Date Added</p>
            <p className="text-center font-semibold text-lg">Actions</p>
          </div>
        </div>

        <hr className="border-gray-300" />

        {/* Tracks */}
        {tracks.map((item, index) => (
          <div
            key={index}
            className="grid grid-cols-4 gap-4 p-4 items-center text-black hover:bg-gray-100 cursor-pointer transition-all"
          >
            {/* Track number */}
            <div className="flex items-center">
              <b className="mr-4 text-gray-500">{index + 1}</b>
            </div>

            {/* Track Image and Title */}
            <div className="flex items-center">
              <img className="inline w-12 mr-4 rounded-lg" src={item.image} alt="Album" />
              <span>{item.name}</span> {/* Track title */}
            </div>

            {/* Date Added */}
            <p className="hidden sm:block text-center">{item.createDate}</p>

            {/* Edit and Delete buttons */}
            <div className="flex justify-center space-x-4 text-black">
              <button 
              onClick={() => handleEditTrackClick(item.trackId)}
              className="hover:text-blue-500 ml-4">
                <FiEdit size={18} />
              </button>
              <button 
              onClick={() => handleDeleteTrack(item.trackId)}
              className="hover:text-red-500 ml-4">
                <FiTrash size={18} />
              </button>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
};

export default AlbumDetail;
