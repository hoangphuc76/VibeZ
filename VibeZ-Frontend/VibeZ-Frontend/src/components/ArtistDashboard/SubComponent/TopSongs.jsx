import React from 'react';

const TopSongs = ({ songs }) => (
    <div className="top-songs">
        <h3>Top Songs People Are Listening To</h3>
        <div id="song-list" className="song-list" role="list">
            {songs.map((song, index) => (
                <div key={index} className="song-item" role="listitem">
                    <strong>{song.name}</strong> - {song.listener} listeners
                </div>
            ))}
        </div>
    </div>
);

export default TopSongs;
