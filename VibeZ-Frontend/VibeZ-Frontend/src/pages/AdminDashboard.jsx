import React from 'react';
import SidebarAdmin from '../components/AdminDashboard/SidebarAdmin';
import AdminHeader from '../components/AdminDashboard/AdminHeader';
import { Outlet } from 'react-router-dom';
const AdminDashboard = () => {
    return (
        <div className="flex flex-col h-screen">
            <div className="flex flex-row">
                <SidebarAdmin/>
                <div className="flex-1">
                    <AdminHeader/>
                    <Outlet/>
                </div>
            </div>
        </div>
    );
};

export default AdminDashboard;
