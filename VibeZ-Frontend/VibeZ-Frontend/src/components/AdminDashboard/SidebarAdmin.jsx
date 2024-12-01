import React, { useContext, useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import logo from '../../assets/vibez_logo.svg';
import home_admin from '../../assets/home-admin.svg';
import home_admin1 from '../../assets/home-admin1.svg';
import { LoginContext } from '../../context/LoginContext';

function SidebarAdmin() {
    const [hoveredItem, setHoveredItem] = useState(null);
    const navigate = useNavigate();

    const menuItems = [
        { name: 'Home', path: '/admin/home', icon: [home_admin, home_admin1] },
        { name: 'Artist', path: '/admin/artist' },
        { name: 'Ban', path: '/admin/ban' },
        { name: 'Approval', path: '/admin/approval' },
        { name: 'Artist Approval', path: '/admin/artistApproval'},
        { name: 'Statistics', path: '/admin/statistics'},

    ];

    return (
        <div className="w-64 h-screen bg-black text-white flex flex-col items-center p-4 shadow-lg">
            <div className="flex items-center h-16 mb-4">
                <img src={logo} alt="logo" className="mr-2" />
                <h2 className="text-xl font-bold cursor-pointer" onClick={() => navigate('/')}>For Admin </h2>
            </div>
            {menuItems.map((item, index) => (
                <Link
                    key={index}
                    to={item.path}
                    className="flex items-center gap-3 w-full h-20 p-4 hover:bg-gray-500 rounded-md cursor-pointer justify-center"
                    onMouseEnter={() => item.icon && setHoveredItem(item.name)}
                    onMouseLeave={() => setHoveredItem(null)}
                >
                    {item.icon ? (
                        <img
                            src={hoveredItem === item.name ? item.icon[0] : item.icon[1]}
                            alt={`${item.name} Logo`}
                            className="w-8 h-8"
                        />
                    ) : null}
                    <span>{item.name}</span>
                </Link>
            ))}
        </div>
    );
}

export default SidebarAdmin;
