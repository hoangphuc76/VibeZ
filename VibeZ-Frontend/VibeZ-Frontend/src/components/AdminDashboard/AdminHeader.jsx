import React, { useContext, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { DownOutlined, SettingOutlined } from '@ant-design/icons';
import { Dropdown } from 'antd';
import noti_icon from '../../assets/bell.png';
import { LoginContext } from '../../context/LoginContext';

const items = [
    {
        key: '1',
        label: 'My Account',
        disabled: true,
    },
    {
        type: 'divider',
    },
    {
        key: '2',
        label: 'Profile',
        extra: '⌘P',
    },
    {
        key: '3',
        label: 'Billing',
        extra: '⌘B',
    },
    {
        key: '4',
        label: 'Settings',
        icon: <SettingOutlined />,
        extra: '⌘S',
    },
    {
        key: '5',
        label: 'Logout',
    },
];

function AdminHeader() {
    const { user, isLoggedIn, loading } = useContext(LoginContext);
    const navigate = useNavigate();
    const userIcon = user && user.name ? user.name.charAt(0).toUpperCase() : '';

    useEffect(() => {
        if (!loading && !isLoggedIn) {
            navigate('/');
        }
    }, [isLoggedIn, loading, navigate]);

    if (loading) return null;

    return (
        <div className="flex justify-end items-center bg-black text-white p-2 ">
            <div className="relative mr-4">
                <img
                    src={noti_icon}
                    alt="Notifications"
                    className="w-6 h-6 cursor-pointer"
                />
            </div>

            <div className="relative">
                <Dropdown
                    menu={{
                        items,
                    }}
                >
                    <div className="flex items-center">
                        <button
                            className="bg-white text-customGreen rounded-full w-10 h-10 flex items-center justify-center cursor-pointer">
                            {userIcon}
                        </button>
                        <DownOutlined className="ml-2" />
                    </div>
                </Dropdown>
            </div>
        </div>
    );
}

export default AdminHeader;
