import axios from 'axios';

const ShazamAPI = {
  apiKey: '88451cba58msh52f997290c67bfbp1a7c91jsnb8f6d780e887', // Thay bằng API Key của bạn
  host: 'shazam-api6.p.rapidapi.com',
};

// Hàm gửi yêu cầu nhận diện bài hát qua Shazam API
const identifySong = async (audioFile) => {
  try {
    // Kiểm tra tính hợp lệ của file trước khi gửi
    if (!audioFile || !(audioFile instanceof File)) {
      throw new Error("Audio file không hợp lệ hoặc chưa được chọn.");
    }

    const formData = new FormData();
    formData.append('file', audioFile); // Thêm file audio vào FormData

    // Gửi yêu cầu POST đến Shazam API
    const response = await axios.post(`https://${ShazamAPI.host}/shazam/recognize/`, formData, {
      headers: {
        'Accept': 'application/json',
        'Content-Type': ' multipart/form-data',
        'X-RapidAPI-Key': ShazamAPI.apiKey,
        'X-RapidAPI-Host': ShazamAPI.host,
      },
    });

    // Kiểm tra và xử lý kết quả trả về
    if (response.data && response.data.track) {
      console.log('Thông tin bài hát nhận diện:', response.data.track);
      return response.data.track;
    } else {
      console.log('Không thể nhận diện bài hát');
      return null;
    }
  } catch (error) {
    console.error('Lỗi khi gửi yêu cầu:', error);
    throw error;
  }
};

export default {
  identifySong,
};
