import axios from "axios";

const BASE_URL = "https://localhost:7241/api/Library"; // Thay đổi theo địa chỉ backend

const getPlaylistsByLibraryId = async (libraryId) => {
  try {
    const response = await axios.get(`${BASE_URL}/${libraryId}/playlists`);
    return response.data; // Trả về dữ liệu playlists
  } catch (error) {
    console.error(
      `Error fetching playlists for libraryId: ${libraryId}`,
      error
    );
    throw error;
  }
};

const getArtistsByLibraryId = async (libraryId) => {
  try {
    const response = await axios.get(`${BASE_URL}/${libraryId}/artists`);
    return response.data; // Trả về dữ liệu artists
  } catch (error) {
    console.error(`Error fetching artists for libraryId: ${libraryId}`, error);
    throw error;
  }
};

const getAlbumsByLibraryId = async (libraryId) => {
  try {
    const response = await axios.get(`${BASE_URL}/${libraryId}/albums`);
    return response.data; // Trả về dữ liệu albums
  } catch (error) {
    console.error(`Error fetching albums for libraryId: ${libraryId}`, error);
    throw error;
  }
};

const getLibraryByUserId = async (userId) => {
    try {
      const response = await axios.get(`${BASE_URL}/${userId}`);
      return response.data; // Trả về danh sách Libraries
    } catch (error) {
      console.error(`Error fetching libraries for userId: ${userId}`, error);
      throw error;
    }
  };

export default {
    getPlaylistsByLibraryId,
    getArtistsByLibraryId,
    getAlbumsByLibraryId,
    getLibraryByUserId
  };
  