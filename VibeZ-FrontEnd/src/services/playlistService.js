import axios from "axios";
const BASE_URL = "https://localhost:7241/odata/Playlist";

const getTracksByPlaylistId = async (playlistId) => {
    try {
      const response = await axios.get(`${BASE_URL}/Tracks/${playlistId}`);
      return response.data; // Trả về dữ liệu playlists
    } catch (error) {
      console.error(
        `Error fetching playlists for TrackPlaylist: ${playlistId}`,
        error
      );
      throw error;
    }
  };// Thay đổi theo địa chỉ backend

  const getPlaylistById = async (playlistId) => {
    try {
      const response = await axios.get(`${BASE_URL}/${playlistId}`);
      return response.data; // Trả về dữ liệu playlists
    } catch (error) {
      console.error(
        `Error fetching playlists for Playlist: ${playlistId}`,
        error
      );
      throw error;
    }
  };// Thay đổi theo địa chỉ backend
  const createPlaylist = async (name, description, createBy, image, userId) => {
    if (!name || !createBy || !description) {
      console.error("Missing required fields");
      return;
    }
  
    try {
      const formData = new FormData();
      formData.append('name', name);
      formData.append('description', description);
      formData.append('createBy', createBy);
      formData.append('image', image);
      formData.append('userId', userId);
  
      const response = await axios.post(`${BASE_URL}/CreatePlaylist`, formData
      );
  
      return response.data;
    } catch (error) {
      console.error("Error creating playlist", error.response.data);
      throw error;
    }
  };
  
  const createLibrary_playlist = async (libId, playlistId) => {
    try {
      const formData = new FormData();
      formData.append('libId', libId);
      formData.append('playId', playlistId);

      const response = await axios.post(`https://localhost:7241/api/Library_Playlist/Create`, formData);
      if (response.status === 200) {
        return "Create successful"; // Có thể trả về một thông điệp
      }
      return response.data;
    } catch(error) {
      console.error("Error creating playlist", error.response.data);
      throw error;
    }
  }

  export default {
    getTracksByPlaylistId,
    getPlaylistById,
    createPlaylist,
    createLibrary_playlist
  };
  