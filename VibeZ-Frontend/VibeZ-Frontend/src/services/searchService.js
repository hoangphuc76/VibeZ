import axios from 'axios';

const API_URL = 'https://localhost:7241/api/ElasticSearch'; // Thay đổi theo địa chỉ backend

const search = async (query) => {
    try {
        // Thực hiện gọi API với phương thức GET, truyền các tham số query và fieldName
        const response = await axios.get(`${API_URL}/search`, {
            params: { query, fieldName: 'lyrics' }
        });

        // Kiểm tra nếu có dữ liệu trả về
        if (response.data) {
            console.log('Search successful');
            return response.data; // Trả về kết quả tìm kiếm
        } else {
            console.log("Search failed, no data returned.");
            throw new Error("Search failed: no data returned."); // Ném lỗi nếu không có kết quả
        }
    } catch (error) {
        console.error("Search error:", error.message || error); // In ra lỗi
        throw new Error("Search failed: " + (error.response?.data?.message || error.message)); // Ném lỗi với thông điệp rõ ràng
    }
};

export default {
  search
  };
