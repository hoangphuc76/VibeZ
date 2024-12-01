import React from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faUsers, faUserPlus, faUserSlash, faPlayCircle, faSave, faHourglassHalf } from '@fortawesome/free-solid-svg-icons';

const StatsGrid = ({ listener, follower, unfollower, savedPlaylists }) => (
    <div className="stats-grid">
        {[
            { label: 'Listeners', value: listener, icon: faUsers },
            { label: 'New followers', value: follower, icon: faUserPlus },
            { label: 'Unfollows', value: unfollower, icon: faUserSlash },
            { label: 'Saved as playlist', value: savedPlaylists, icon: faSave },
        ].map((stat, index) => (
            <div key={index} className="stat-card" role="region" aria-label={stat.label}>
                <FontAwesomeIcon icon={stat.icon} className="stat-icon" />
                <div className="stat-number-label-container">
            <p className="stat-number">{stat.value}</p>
            <p className="stat-label">{stat.label}</p>
        </div>
            </div>
        ))}
    </div>
);

export default StatsGrid;
