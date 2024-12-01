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

  
  const AddTrackToPlaylist = async (playlistId, trackId) => {
    try {
      const response = await axios.post(`https://localhost:7241/api/TrackPlaylist`, null, {
        params: {
          playlistId: playlistId,
          trackId: trackId
        }
      });
      return response.status === 200 ? "Ok" : null;
    } catch (error) {
      console.error("Error creating playlist_track relationship:", error.message || error);
      throw new Error(
        "Failed to create playlist_track relationship: " + 
        (error.response?.data?.message || error.message)
      );
    }
  }

  const DeleteTrackFromPlaylist = async (playlistId, trackId) => {
    try {
      const response = await axios.delete(`https://localhost:7241/api/TrackPlaylist/${playlistId}/${trackId}`);
      return response.status === 200 ? "Ok" : null;
    } catch (error) {
      console.error("Error deleting playlist_track relationship:", error.message || error);
      throw new Error(
        "Failed to delete playlist_track relationship: " + 
        (error.response?.data?.message || error.message)
      );
    }
  }

  const deletePlaylist = async (playlistId) => {
    if (!playlistId) {
      console.error("Playlist ID is required to delete a playlist");
      return;
    }
  
    try {
      // Gọi API DELETE để xóa playlist với ID cụ thể
      const response = await axios.delete(`${BASE_URL}/${playlistId}`);
      
      // if (response.status === 204) { // 204: No Content, nghĩa là xóa thành công
      //   console.log(`Playlist with ID ${playlistId} has been deleted successfully.`);
      //   return "Playlist deleted successfully";
      // }
  
      return response.status; // Trả về dữ liệu response nếu có
    } catch (error) {
      console.error(`Error deleting playlist with ID ${playlistId}`, error.response?.data || error.message);
      throw error;
    }
  };
  

  export default {
    getTracksByPlaylistId,
    getPlaylistById,
    createPlaylist,
    createLibrary_playlist,
    deletePlaylist, 
    AddTrackToPlaylist,
    DeleteTrackFromPlaylist
  };
  