// src/services/authService.js
import axios from 'axios';

const API_URL = 'https://localhost:7241/odata'; // Thay đổi theo địa chỉ backend

const authService = async (username, password) => {
    try {
        const response = await axios.post(`${API_URL}/login`, { username, password });

        if (response.data?.token) {
            localStorage.setItem('jwtToken', response.data.token);
            localStorage.setItem('username', JSON.stringify(response.data.username));
            localStorage.setItem('userId', JSON.stringify(response.data.id))
            console.log('Login successful');
            return response.data; // Trả về thông tin người dùng
        } else {
            console.log("Login failed, no token returned.");
            throw new Error("Login failed: no token returned."); // Ném lỗi nếu không có token
        }
    } catch (error) {
        console.error("Login error:", error.message || error); // In ra lỗi
        throw new Error("Login failed: " + (error.response?.data?.message || error.message)); // Ném lỗi với thông điệp rõ ràng
    }
};

export default authService;
