import { createContext, useEffect, useRef, useState } from "react";
import authService from "../services/authService";
import { songsData } from "../assets/assets";

export const LoginContext = createContext();

const LoginContextProvider = (props) => {

    const [isLoggedIn, setIsLoggedIn] = useState(false);
    const [user, setUser] = useState(null);
    const [username, setusername] = useState(null);
    const [userId, setUserId] = useState(null);
    const [isChange, setChange] = useState(false);
    useEffect(() => {
        // Kiểm tra token trong localStorage khi ứng dụng khởi động
        const token = localStorage.getItem('jwtToken');
        const storedUser = localStorage.getItem('username');
        const storeId = JSON.parse(localStorage.getItem('userId')); // Loại bỏ dấu ngoặc kép nếu có
        setUserId(storeId);


        if (token && storedUser && storeId) {
            setIsLoggedIn(true);
            setUser(JSON.parse(storedUser).charAt(0).toUpperCase());
            console.log('User logged in:', storeId);
        }
    }, []);

    const login = async (username, password) => {
        try {
            const response = await authService(username, password);
            setIsLoggedIn(true);
            setusername(response.username);
            setUser(response.username.charAt(0).toUpperCase()); // Cập nhật thông tin người dùng chỉ với chữ cái đầu tiên

            // Cập nhật thông tin người dùng    
        } catch (error) {
            console.error("Login failed:", error.message);
            throw new Error("Login failed: " + error.message);
        }
    };

    // Theo dõi sự thay đổi của isLoggedIn
    useEffect(() => {
        console.log('Login status changed:', isLoggedIn, user, userId);
    }, [isLoggedIn, user]); // Ghi log khi isLoggedIn hoặc user thay đổi

    // Đăng xuất người dùng
    const logout = () => {
        localStorage.removeItem('jwtToken');
        localStorage.removeItem('username');
        localStorage.removeItem('userId');
        setIsLoggedIn(false);
        setUser(null);
        setUserId(null);
    };

    const contextValue = {
        login, logout,
        isLoggedIn, user, userId, username, isChange, setChange
    };

    return (
        <LoginContext.Provider value={contextValue}>
            {props.children}
        </LoginContext.Provider>
    );
};

export default LoginContextProvider;
