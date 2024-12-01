// src/services/albumService.js
import axios from "axios";

const API_URL = "https://localhost:7241/odata/Track";

export const changeStatus = async (id) => {
  try {
    const response = await axios.put(`https://localhost:7241/api/Admin/approve-track/${id}`);
    console.log(response.data);
    return;
  } catch (error) {
    console.error("Error changing status:", error.message || error);
    throw new Error(
      "Failed to change status: " +
        (error.response?.data?.message || error.message)
    );
  }
};

// Hàm lấy tên nghệ sĩ theo ID
const getAllTrackByAlbumId = async (id) => {
  try {
    const response = await axios.get(`${API_URL}/Album/${id}`);
    return response.data;
  } catch (error) {
    console.error("Error fetching track:", error.message || error);
    throw new Error(
      "Failed to fetch artist: " +
        (error.response?.data?.message || error.message)
    );
  }
};
const getTrackById = async (id) => {
  try {
    const response = await axios.get(`${API_URL}/${id}`);
    return response.data; // Trả về tên nghệ sĩ từ API
  } catch (error) {
    console.error("Error fetching track:", error.message || error);
    throw new Error(
      "Failed to fetch artist: " +
        (error.response?.data?.message || error.message)
    );
  }
};
const getAllTracks = async () => {
  try {
    const response = await axios.get(`${API_URL}/all`);
    return response.data; // Trả về tên nghệ sĩ từ API
  } catch (error) {
    console.error("Error fetching track:", error.message || error);
    throw new Error(
      "Failed to fetch artist: " +
        (error.response?.data?.message || error.message)
    );
  }
};

const UpdateListener = async (id) => {
  try {
    const response = await axios.put(`${API_URL}/UpdateListener/${id}`);

    // Kiểm tra nếu API trả về 204 No Content
    if (response.status === 204) {
      return "Update successful, no content returned"; // Có thể trả về một thông điệp
    }

    return response.data; // Trả về dữ liệu nếu có
  } catch (error) {
    console.error("Error fetching track:", error.message || error);
    throw new Error(
      "Failed to update listener: " +
        (error.response?.data?.message || error.message)
    );
  }
};
const createTrack = async (formData) => {
  try {
    // Make the POST request to the API to create the track
    const response = await axios.post(`${API_URL}/upload`, formData, {
      headers: {
        'Content-Type': 'multipart/form-data',
      },
    });

    // Return the response data (e.g., track information)
    return response.data;
  } catch (error) {
    console.error("Error creating track:", error.message || error);
    throw new Error(
      "Failed to create track: " + 
      (error.response?.data?.message || error.message)
    );
  }
};


const updateTrack = async (id, formData) => {
  try {

    const response = await axios.put(`${API_URL}/${id}`, formData, {
      headers: {
        'Content-Type': 'multipart/form-data',
      }
    });

    return response.data;
  } catch (error) {
    console.error("Error updating track:", error.message || error);
    throw new Error(
      "Failed to update track: " + (error.response?.data?.message || error.message)
    );
  }
};


const getRecentlyPlayedTracks = async () => {
  try {
    // Lấy danh sách id từ localStorage và lọc bỏ các id không hợp lệ
    const recentlyPlayed =
      JSON.parse(localStorage.getItem("RecentlyPlayed")) || [];
    const validIds = recentlyPlayed.filter(Boolean); // Loại bỏ null, undefined, và chuỗi rỗng

    // Nếu không có id hợp lệ, trả về mảng rỗng
    if (!validIds.length) {
      console.log("No valid track IDs found.");
      return [];
    }

    // Sử dụng POST request để gửi danh sách ID
    const response = await axios.post(`${API_URL}/Tracks`, validIds);

    // Kiểm tra phản hồi từ API và trả về dữ liệu
    return response.data || [];
  } catch (error) {
    console.error("Error fetching tracks:", error);
    return []; // Trả về mảng rỗng nếu gặp lỗi
  }
};
const fetchRecommendations = async (clickedTrackId) => {
  try {
    const recentlyPlayed =
      JSON.parse(localStorage.getItem("RecentlyPlayed")) || [];
    const validIds = recentlyPlayed.filter(Boolean); // Loại bỏ null, undefined, và chuỗi rỗng

    // Nếu không có id hợp lệ, trả về mảng rỗng
    if (!validIds.length) {
      console.log("No valid track IDs found.");
      return [];
    }

    const response = await axios.post(`${API_URL}/GetRecommendations`, {
      recentlyPlayedIds: validIds,
      clickedTrackId: clickedTrackId,
      topN: 10, // Số lượng bài hát gợi ý
    });

    return response.data || [];
  } catch (error) {
    console.error("Error fetching track recommendations:", error);
    setError("Failed to fetch recommendations");
  }
};

const deleteTrack = async (id) => {
  try {
    await axios.delete(`${API_URL}/${id}`);
    return "Track deleted successfully";
  } catch (error) {
    console.error("Error deleting track:", error.message || error);
    throw new Error(
      "Failed to delete track: " + (error.response?.data?.message || error.message)
    );
  }
};
const deleteTracksByAlbumId = async (id) => {
  try{
    await axios.delete(`${API_URL}/Album/${id}`);
    return "Track deleted successfully";
  }catch (error) {
    console.error("Error deleting track:", error.message || error);
    throw new Error(
      "Failed to delete track: " + (error.response?.data?.message || error.message)
    );
  }
};
const getTrackByArtistId = async (id) => {
  try {
    const response = await axios.get(`${API_URL}/Artist/${id}`);
    return response.data;  // Trả về tên nghệ sĩ từ API
  } catch (error) {
    console.error("Error fetching track:", error.message || error);
    throw new Error(
      "Failed to fetch artist: " +
        (error.response?.data?.message || error.message)
    );
  }
};
// Xuất các service
export default {
  getAllTrackByAlbumId,
  getTrackById,
  getAllTracks,
  getRecentlyPlayedTracks,
  fetchRecommendations,
  UpdateListener,
  deleteTrack,
  createTrack,
  updateTrack,
  getTrackByArtistId,
  deleteTracksByAlbumId,
  changeStatus
};
