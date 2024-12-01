import React, { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import albumService from "../../services/albumService"; // Adjust the import as needed

const EditAlbum = () => {
  const { id } = useParams();  // Retrieve the album ID from the URL params (to fetch the album)
  const navigate = useNavigate();

  const [albumData, setAlbumData] = useState({
    name: "",
    yy: "", // Year
    mm: "", // Month
    dd: "", // Day
    image: "", // Cover Image URL
    nation: "",
  });

  useEffect(() => {
    // Log the albumId to ensure it's correct
    console.log('Album ID:', id);

    const fetchAlbumData = async () => {
      try {
        console.log("Fetching album data...");
        const data = await albumService.getAlbumsById(id);  // Fetch album data using the correct ID
        console.log("Album data fetched:", data);

        // Ensure the releaseDate is parsed correctly
        const releaseDate = new Date(data.dateOfRelease); // Create a Date object from the string

        if (isNaN(releaseDate)) {
          throw new Error("Invalid date format");
        }

        // Set the album data with the correct values
        setAlbumData({
          name: data.name,
          yy: releaseDate.getFullYear(),
          mm: releaseDate.getMonth() + 1, // Month is 0-based, so add 1
          dd: releaseDate.getDate(),
          image: data.image,  // Use image URL
          nation: data.nation,
        });
      } catch (error) {
        console.error("Error fetching album data", error);
      }
    };

    fetchAlbumData();
  }, [id]);

  const handleSubmit = async (e) => {
    e.preventDefault();
    console.log("Submitting form with data:", albumData);
    
    try {
      const formData = new FormData();
      formData.append('name', albumData.name);
      formData.append('yy', albumData.yy);
      formData.append('mm', albumData.mm);
      formData.append('dd', albumData.dd);
      formData.append('nation', albumData.nation);

      if (albumData.image) {
        formData.append('image', albumData.image);  // If image is provided, append it
      }

      console.log("Sending form data:", formData);

      // Use the id from useParams and send it along with the form data
      await albumService.updateAlbum(id, formData);  
      alert("Album updated successfully!");
      navigate(`/artistdashboard/music/album/${id}`);  
    } catch (error) {
      console.error("Error updating album", error);
      alert("Failed to update album.");
    }
  };

  return (
    <div className="min-h-screen bg-gray-900 text-white p-8">
      <h1 className="text-3xl font-bold mb-8">Edit Album</h1>
      <form onSubmit={handleSubmit} className="space-y-6">
        <div>
          <label htmlFor="name" className="block text-lg font-medium mb-2">Album Name</label>
          <input
            type="text"
            id="name"
            value={albumData.name}
            onChange={(e) => setAlbumData({ ...albumData, name: e.target.value })}
            required
            className="w-full p-3 bg-gray-800 border border-gray-600 text-white rounded-md focus:outline-none focus:ring-2 focus:ring-green-500"
          />
        </div>
        <div>
          <label htmlFor="nation" className="block text-lg font-medium mb-2">Nation</label>
          <input
            type="text"
            id="nation"
            value={albumData.nation}
            onChange={(e) => setAlbumData({ ...albumData, nation: e.target.value })}
            required
            className="w-full p-3 bg-gray-800 border border-gray-600 text-white rounded-md focus:outline-none focus:ring-2 focus:ring-green-500"
          />
        </div>
        <div>
          <label htmlFor="releaseDate" className="block text-lg font-medium mb-2">Release Date</label>
          <div className="flex space-x-4">
            <input
              type="number"
              id="yy"
              value={albumData.yy}
              onChange={(e) => setAlbumData({ ...albumData, yy: e.target.value })}
              required
              placeholder="Year"
              className="w-1/3 p-3 bg-gray-800 border border-gray-600 text-white rounded-md focus:outline-none focus:ring-2 focus:ring-green-500"
            />
            <input
              type="number"
              id="mm"
              value={albumData.mm}
              onChange={(e) => setAlbumData({ ...albumData, mm: e.target.value })}
              required
              placeholder="Month"
              className="w-1/3 p-3 bg-gray-800 border border-gray-600 text-white rounded-md focus:outline-none focus:ring-2 focus:ring-green-500"
            />
            <input
              type="number"
              id="dd"
              value={albumData.dd}
              onChange={(e) => setAlbumData({ ...albumData, dd: e.target.value })}
              required
              placeholder="Day"
              className="w-1/3 p-3 bg-gray-800 border border-gray-600 text-white rounded-md focus:outline-none focus:ring-2 focus:ring-green-500"
            />
          </div>
        </div>
        <div>
          <label htmlFor="coverImage" className="block text-lg font-medium mb-2">Cover Image</label>
          {albumData.image && (
            <div className="mb-4">
              <img src={albumData.image} alt="Album Cover" className="w-32 h-32 object-cover" />
            </div>
          )}
          <input
            type="file"
            id="coverImage"
            onChange={(e) => setAlbumData({ ...albumData, image: e.target.files[0] })}
            className="w-full p-3 bg-gray-800 border border-gray-600 text-white rounded-md focus:outline-none focus:ring-2 focus:ring-green-500"
          />
        </div>
        <button type="submit" className="w-full py-3 bg-green-500 text-white rounded-md font-semibold hover:bg-green-600 transition-all">Update Album</button>
      </form>
    </div>
  );
};

export default EditAlbum;
