import React, { useContext, useEffect, useState } from 'react';
import { Navigate } from 'react-router-dom';
import { LoginContext } from './context/LoginContext';

function ProtectedRoute({ allowedRoles, children }) {
  const [isLoggedIn, setIsLoggedIn] = useState(false);
  const [roles, setRoles] = useState(null); // Đặt giá trị ban đầu là null để dễ kiểm tra trạng thái tải

  useEffect(() => {
    const getRoles = async () => {
      const role = JSON.parse(localStorage.getItem('roles'));
      const isLoggedIn = JSON.parse(localStorage.getItem('logIn'));
      setIsLoggedIn(isLoggedIn);
      setRoles(role);
    };
    getRoles();
  }, []);

  if (isLoggedIn == null) {
    return <Navigate to="/" replace />;

  }
  if (roles != null) {
    if (!allowedRoles.includes(roles)) {
      console.log(roles);
      return <Navigate to="/" replace />;
    }

    // Nếu có quyền truy cập, render `children`
    return children;
  }
}


export default ProtectedRoute;
