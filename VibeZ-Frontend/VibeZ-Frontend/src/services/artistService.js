import axios from 'axios';

const API_URL = 'https://localhost:7241/api/Artist';

export const deleteArtistApi = async (id) => {
  console.log(`Deleting artist with id: ${id}`);
  try {
    const response = await axios.delete(`${API_URL}/${id}`);
    return response.data; 
  } catch (error) {
    console.error("Error deleting artist:", error.message || error);
    throw new Error("Failed to delete artist: " + (error.response?.data?.message || error.message));
  }
};

const getNameArtistById = async (id) => {
  try {
    const response = await axios.get(`${API_URL}/${id}`);
    return response.data.name;
  } catch (error) {
    console.error("Error fetching artist name:", error.message || error);
    throw new Error("Failed to fetch artist: " + (error.response?.data?.message || error.message));
  }
};

const getArtistById = async (id) => {
  try {
    const response = await axios.get(`${API_URL}/${id}`);
    return response.data; 
  } catch (error) {
    console.error("Error fetching artist:", error.message || error);
    throw new Error("Failed to fetch artist: " + (error.response?.data?.message || error.message));
  }
};

const getAllTrackByArtistId = async (id) => {
  try {
    const response = await axios.get(`${API_URL}/Tracks/${id}`);
    return response.data; 
  } catch (error) {
    console.error("Error fetching tracks for artist:", error.message || error);
    throw new Error("Failed to fetch artist tracks: " + (error.response?.data?.message || error.message));
  }
};

const unfollowArtist = async (artistId, libraryId) => {
  if (!artistId || !libraryId) {
    console.error("Artist ID and Library ID are required to unfollow an artist");
    return;
  }
  try {
    const response = await axios.delete(`https://localhost:7241/api/Library_Artist/${libraryId}/${artistId}`);
    return response.status; 
  } catch (error) {
    console.error(`Error unfollowing artist with ID ${artistId}:`, error.response?.data || error.message);
    throw new Error("Failed to unfollow artist: " + (error.response?.data?.message || error.message));
  }
};

const suggestArtists = async () => {
  try {
    const recentlyPlayed = JSON.parse(localStorage.getItem("RecentlyPlayed")) || [];
    const validIds = recentlyPlayed.filter(Boolean);
    if (!validIds.length) {
      console.log("No valid IDs found for recently played artists.");
      return [];
    }
    const response = await axios.post(`${API_URL}/SuggestArtist`, validIds, {
      headers: { 'Content-Type': 'application/json' }
    });
    return response.data;
  } catch (error) {
    console.error("Error suggesting artists:", error.message || error);
    throw new Error("Failed to suggest artists: " + (error.response?.data?.message || error.message));
  }
};

const followArtist = async (libId, artistId) => {
  try {
    const formData = new FormData();
    formData.append('libId', libId);
    formData.append('artistId', artistId);
    const response = await axios.post(`https://localhost:7241/api/Library_Artist/Follow`, formData);
    return response.status === 200 ? "Ok" : null;
  } catch (error) {
    console.error("Error following artist:", error.message || error);
    throw new Error("Failed to create Library_Artist relationship: " + (error.response?.data?.message || error.message));
  }
};
const follow = async (userId, artistId) => {
  try {
    const formData = new FormData();
    formData.append('userId', userId);
    formData.append('artistId', artistId);

    const response = await axios.post(`https://localhost:7241/Follow`, formData);
    return response.status === 201 ? "Created" : null;
  } catch (error) {
    console.error("Error following artist:", error.message || error);
    throw new Error("Failed to create Follow relationship: " + (error.response?.data?.message || error.message));
  }
};
const unfollow = async (userId, artistId) => {
  try {
    const response = await axios.put(`https://localhost:7241/Unfollow/${userId}/${artistId}`);
    return response.status === 204 ? "Unfollowed" : null;
  } catch (error) {
    console.error("Error unfollowing artist:", error.message || error);
    throw new Error("Failed to update Follow relationship to unfollow: " + (error.response?.data || error.message));
  }
};

const getFollowArtist = async (libId, artistId) => {
    const response = await axios.get(`https://localhost:7241/api/Library_Artist/${libId}/${artistId}`);
    return response.status === 200 ? "Ok" : null;
  
};

export default {
  deleteArtistApi,
  getNameArtistById,
  getArtistById,
  getAllTrackByArtistId,
  unfollowArtist,
  suggestArtists,
  followArtist,
  getFollowArtist,
  follow,
  unfollow
};
