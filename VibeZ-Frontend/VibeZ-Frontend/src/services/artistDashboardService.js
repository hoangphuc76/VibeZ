import axios from 'axios';

const API_BASE_URL = 'https://localhost:7241/api/ArtistDashboard';

const formatDate = (date) => date.toISOString().split('T')[0]; // Chuyển Date thành chuỗi YYYY-MM-DD

// Hàm gọi API GetTotalFollow
const getTotalFollow = async (artistId, startDate, endDate) => {
  try {
    const response = await axios.get(`${API_BASE_URL}/TotalFollow`, {
      params: { artistId, startDate: formatDate(startDate), endDate: formatDate(endDate) },
    });
    return response.data;
  } catch (error) {
    console.error("Error fetching total follow:", error.message || error);
    throw new Error("Failed to fetch total follow: " + (error.response?.data || error.message));
  }
};

// Hàm gọi API GetTotalUnFollow
const getTotalUnFollow = async (artistId, startDate, endDate) => {
  try {
    const response = await axios.get(`${API_BASE_URL}/TotalUnfollow`, {
      params: { artistId, startDate: formatDate(startDate), endDate: formatDate(endDate) },
    });
    return response.data;
  } catch (error) {
    console.error("Error fetching total unfollow:", error.message || error);
    throw new Error("Failed to fetch total unfollow: " + (error.response?.data || error.message));
  }
};

// Hàm gọi API GetTotalListener
const getTotalListener = async (artistId, startDate, endDate) => {
  try {
    const response = await axios.get(`${API_BASE_URL}/TotaListener`, {
      params: { artistId, startDate: formatDate(startDate), endDate: formatDate(endDate) },
    });
    return response.data;
  } catch (error) {
    console.error("Error fetching total listener:", error.message || error);
    throw new Error("Failed to fetch total listener: " + (error.response?.data || error.message));
  }
};
const getArtistByUserId = async (userId) => {
   try {
      const response = await axios.get(`https://localhost:7241/Artist/${userId}`);
      return response.data;
   } catch (error) {
    console.error(error.message);
   }
}
// Hàm gọi API GetTotalSavedPlaylist
const getTotalSavedPlaylist = async (artistId, startDate, endDate) => {
  try {
    const response = await axios.get(`${API_BASE_URL}/TotaSavePlaylist`, {
      params: { artistId, startDate: formatDate(startDate), endDate: formatDate(endDate) },
    });
    return response.data;
  } catch (error) {
    console.error("Error fetching total saved playlist:", error.message || error);
    throw new Error("Failed to fetch total saved playlist: " + (error.response?.data || error.message));
  }
};

// Hàm gọi API GetTopSongs
const getTopSongs = async (artistId, startDate, endDate) => {
  try {
    const response = await axios.get(`${API_BASE_URL}/TopSong`, {
      params: { artistId, startDate: formatDate(startDate), endDate: formatDate(endDate) },
    });
    return response.data;
  } catch (error) {
    console.error("Error fetching top songs:", error.message || error);
    throw new Error("Failed to fetch top songs: " + (error.response?.data || error.message));
  }
};

const addArtistPending = async (formData) => {
  try {
   
      const response = await axios.post(`https://localhost:7241/api/ArtistPending`, formData, {
          headers: {
              "Content-Type": "multipart/form-data"
          }
      });

      console.log("Artist pending added successfully:", response.data);
      return response.data;
  } catch (error) {
      console.error("Error adding artist pending record:", error.message || error);
      throw new Error(
          "Failed to add artist pending record: " +
          (error.response?.data?.message || error.message)
      );
  }
};


export default {
    getTotalFollow,
    getTotalSavedPlaylist,
    getTopSongs,
    getTotalListener,
    getTotalUnFollow, 
    getArtistByUserId,
    addArtistPending
}