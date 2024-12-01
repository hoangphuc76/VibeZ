import { createContext, useEffect, useState } from "react";
import { songsData } from "../assets/assets";
import authService from "../services/authService";
export const LoginContext = createContext();

const LoginContextProvider = (props) => {
    const [isLoggedIn, setIsLoggedIn] = useState(false);
    const [user, setUser] = useState(null);
    const [userInfo, setUserInfo] = useState();
    const [username, setusername] = useState(null);
    const [userId, setUserId] = useState(null);
    const [isChange, setChange] = useState(false);
    const [Info, setInfo] = useState(false);
    const [loading, setLoading] = useState(true); // Add loading state

    useEffect(() => {
        // Kiểm tra token trong localStorage khi ứng dụng khởi động
        const token = localStorage.getItem('jwtToken');
        const storedUser = localStorage.getItem('username');
        const storeId = JSON.parse(localStorage.getItem('userId'));
        setUserId(storeId);

        if (token && storedUser && storeId) {
            setIsLoggedIn(true);
            setUser(JSON.parse(storedUser).charAt(0).toUpperCase());
            console.log('User logged in:', storeId);
        } else {
            setIsLoggedIn(false);
            setUser(null);
        }

        setLoading(false);
    }, []);
    
    const googleLogin = async (res) => {
        try {
            const response = await authService.responseGoogle(res);
            setIsLoggedIn(true);
            var roles = response.user.role
            console.log(response.user.role);
            setUserInfo(roles);
            localStorage.setItem('logIn', JSON.stringify(true));
            localStorage.setItem('roles', JSON.stringify(roles));
            setUser(response.user.userName.charAt(0).toUpperCase());
            localStorage.setItem('jwtToken', response.token);
        } catch (error) {
            console.error("Login failed:", error.message);
            alert(error.message);
        }
    };

    const login = async (username, password) => {
        try {
            const response = await authService.authServices(username, password);
            setIsLoggedIn(true);
            var roles = response.user.role
            console.log(roles);
            setUserInfo(roles);
            localStorage.setItem('logIn', JSON.stringify(true));
            setusername(response.user.name);
            setUserInfo(roles);
            localStorage.setItem('roles', JSON.stringify(roles));

            // Cập nhật thông tin người dùng    
            setUser(response.user.userName.charAt(0).toUpperCase());
            localStorage.setItem('jwtToken', response.token);
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
    const signUp = async (name, username, email, password) => {
        try {
            await authService.registerService(name, username, email, password);
        } catch (error) {
            console.error("Registration failed:", error.message);
            throw new Error("Registration failed: " + error.message);
        }
    };

    const logout = () => {
        localStorage.removeItem('jwtToken');
        localStorage.removeItem('username');
        localStorage.removeItem('userId');
        localStorage.removeItem('premium');
        localStorage.removeItem('libId');
        localStorage.removeItem('user');
        localStorage.removeItem('roles');
        localStorage.removeItem('logIn');



        setIsLoggedIn(false);
        setUser(null);
        setUserId(null);
    };

    const contextValue = {
        login,
        logout,
        isLoggedIn,
        user,
        userId,
        username,
        isChange,
        setChange,
        setInfo,
        Info,
        signUp,
        googleLogin,
        loading,
        userInfo
    };

    return (
        <LoginContext.Provider value={contextValue}>
            {props.children}
        </LoginContext.Provider>
    );
};

export default LoginContextProvider;
