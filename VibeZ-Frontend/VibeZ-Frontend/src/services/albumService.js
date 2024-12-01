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

const createAlbum = async (formData) => {
  try {
      const response = await axios.post(`${API_URL}/CreateAlbum`, formData, {
          headers: {
              'Content-Type': 'multipart/form-data',
          },
      });
      return response.data;
  } catch (error) {
      console.error("Error creating album:", error.message || error);
      throw new Error(
          "Failed to create album: " + 
          (error.response?.data?.message || error.message)
      );
  }
};
const updateAlbum = async (id, formData) => {
  try {
    const response = await axios.put(`${API_URL}/${id}`, formData, {
      headers: {
        'Content-Type': 'multipart/form-data',
      },
    });
    return response.data; 
  } catch (error) {
    console.error("Error updating album:", error.message || error);
    throw new Error(
      "Failed to update album: " + 
      (error.response?.data?.message || error.message)
    );
  }
};
const deleteAlbum = async (id) => {
  try {
    await axios.delete(`${API_URL}/${id}`);
    return; // Return nothing if the deletion is successful
  } catch (error) {
    console.error("Error deleting album:", error.message || error);
    throw new Error(
      "Failed to delete album: " + 
      (error.response?.data?.message || error.message)
    );
  }
};


const getAlbumsByArtistId = async (artistId) => {
  try {
    const response = await axios.get(`${API_URL}/${artistId}/all`);
    return response.data;
  }catch (error) {
    console.error("Error fetching albums:", error.message || error);
    throw new Error(
      "Failed to fetch albums: " + 
      (error.response?.data?.message || error.message)
    );
  }
};
// Xuất các service

export default { getAllAlbums, getAlbumsById, createAlbum, updateAlbum, deleteAlbum,getAlbumsByArtistId };
