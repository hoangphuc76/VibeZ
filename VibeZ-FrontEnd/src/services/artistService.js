// src/services/albumService.js
import axios from 'axios';

const API_URL = 'https://localhost:7241/api/Artist'; // Thay đổi theo địa chỉ backend

// Hàm lấy tên nghệ sĩ theo ID
const getNameArtistById = async (id) => {
  try {
    const response = await axios.get(`${API_URL}/${id}`);
    return response.data.name; // Trả về tên nghệ sĩ từ API
  } catch (error) {
    console.error("Error fetching artist name:", error.message || error);
    throw new Error(
      "Failed to fetch artist: " + 
      (error.response?.data?.message || error.message)
    );
  }
};

const getArtistById = async (id) => {
  try {
    const response = await axios.get(`${API_URL}/${id}`);
    return response.data; // Trả về tên nghệ sĩ từ API
  } catch (error) {
    console.error("Error fetching artist:", error.message || error);
    throw new Error(
      "Failed to fetch artist: " + 
      (error.response?.data?.message || error.message)
    );
  }
}
const getAllTrackByArtistId = async (id) => {
  try {
    const response = await axios.get(`${API_URL}/Tracks/${id}`);
    return response.data; // Trả về tên nghệ sĩ từ API
  } catch (error) {
    console.error("Error fetching artist:", error.message || error);
    throw new Error(
      "Failed to fetch artist: " + 
      (error.response?.data?.message || error.message)
    );
  }
}
// Xuất các service
export default { getNameArtistById, getArtistById, getAllTrackByArtistId };
