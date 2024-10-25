// src/services/albumService.js
import axios from 'axios';

const API_URL = 'https://localhost:7241/api/Album'; // Thay đổi theo địa chỉ backend

// Hàm lấy tất cả album
const getAllAlbums = async () => {
  try {
    const response = await axios.get(`${API_URL}/all`);
    return response.data; // Trả về dữ liệu nhận được từ API
  } catch (error) {
    console.error("Error fetching albums:", error.message || error);
    throw new Error(
      "Failed to fetch albums: " + 
      (error.response?.data?.message || error.message)
    );
  }
};

// Xuất các service

const getAlbumsById = async (id) => {
  try {
    const response = await axios.get(`${API_URL}/${id}`);
    return response.data; // Trả về dữ liệu nhận được từ API
  } catch (error) {
    console.error("Error fetching albums:", error.message || error);
    throw new Error(
      "Failed to fetch albums: " + 
      (error.response?.data?.message || error.message)
    );
  }
};

// Xuất các service

export default { getAllAlbums, getAlbumsById };
