import React, { useState, useEffect, useContext } from 'react';
import ArtistItem from "../components/ArtistItem";
import AlbumItem from '../components/AlbumItem';
import PlaylistItem from "../components/PlaylistItem";
import libraryService from '../services/libraryService';
import { Dropdown, Menu } from 'antd';
import { PlayerContext } from '../context/PlayerContext';
import { useNavigate } from 'react-router-dom';
import SongItemInSearch from './SongItemInSearch';
import SongItem from './SongItem';

const DisplaySearch = () => {
    const navigate = useNavigate();
    const [activeView, setActiveView] = useState('all');
    const { playWithId } = useContext(PlayerContext);
    const { result } = useContext(PlayerContext);


    const hasSearchResults = () => {
        return (
            result.topResult ||
            (result.playlists && result.playlists.length > 0) ||
            (result.albums && result.albums.length > 0) ||
            (result.artists && result.artists.length > 0) ||
            (result.tracks && result.tracks.length > 0)
        );
    };


    useEffect(() => {
        console.log("track", JSON.stringify(result.tracks, null, 2));
        console.log("artist", JSON.stringify(result.artists, null, 2));
        console.log("playlist", JSON.stringify(result.playlists, null, 2));
        console.log("album", JSON.stringify(result.albums, null, 2));
        console.log("top result", JSON.stringify(result.topResult, null, 2));
    }, [result]);


    const handleViewChange = (view) => setActiveView(view);

    const handleClick = (topResult) => {
        if (topResult.genre) {
            navigate(`/artist/${topResult.id}`);
        } else if (topResult.dateOfRelease) {
            navigate(`/album/${topResult.id}`);
        } else if (topResult.createBy) {
            navigate(`/playlist/${topResult.playlistId}`);
        } else {
            playWithId(topResult.trackId);
        }
    };

    const renderTopResult = () => {
        const { topResult } = result;
        if (!topResult) return null;

        return (
            <div className='p-6 bg-[#1A1A1A] rounded hover:bg-[#272727] cursor-pointer' onClick={() => handleClick(topResult)}>
                {topResult.genre ? "Artist" && (
                    <>
                        <img className='max-w-[170px] rounded-full' src={topResult.image} alt={topResult.name || "No Name"} />
                        <h1 className='font-bold mt-3 text-[30px]'>{topResult.name}</h1>
                        <p className='mt-2 text-gray-400'>
                            Artist
                        </p>
                    </>
                ) : (
                    <>
                        <img className='max-w-[170px] rounded' src={topResult.image} alt={topResult.name || "No Name"} />
                        <h1 className='font-bold mt-3 text-[30px]'>{topResult.name}</h1>
                        <p className='mt-2 text-gray-400'>
                            {topResult.lyrics ? "Song" : topResult.dateOfRelease ? "Album" : topResult.createBy ? "Playlist" : ""}
                        </p>
                    </>

                )
                }

            </div>
        );
    };

    const renderTracks = () => {
        return (
            result.tracks && result.tracks.length > 0 && (
                <div className='w-[60%]'>
                    <h1 className="mt-5 mb-2 font-bold text-2xl">Songs</h1>
                    {result.tracks.slice(0, 4).map((item, index) => (
                        <SongItemInSearch // Thêm key cho mỗi phần tử
                            index={index}
                            name={item.name}
                            image={item.image}
                            artistName={item.artistName}
                            id={item.trackId}
                            time={item.time}
                        />
                    ))}
                </div>
            )
        );
    };


    return (
        <>
            {hasSearchResults() && (
                <div className='h-full flex-1 m-2 px-6 pt-4 rounded bg-[#121212] text-white overflow-auto'>
                    <div className="flex gap-2">
                        {['all', 'songs', 'playlists', 'albums', 'artists', 'profiles'].map((view) => (
                            <button
                                key={view}
                                className={`py-2 px-3 rounded-full hover:bg-white hover:text-black text-[14px] ${activeView === view ? 'bg-white text-black' : 'bg-[#333333]'}`}
                                onClick={() => handleViewChange(view)}
                            >
                                {view.charAt(0).toUpperCase() + view.slice(1)}
                            </button>
                        ))}
                    </div>

                    {activeView === 'all' && (
                        <div className='flex flex-col'>
                            <div className='flex w-full gap-6'>
                                <div className='w-[35%]'>
                                    <h1 className="mt-5 mb-2 font-bold text-2xl">Top Result</h1>
                                    {renderTopResult()}
                                </div>
                                {renderTracks()}
                            </div>
                            <div>
                                {result.artists.length > 0 && (
                                    <>
                                        <h1 className="my-5 font-bold text-2xl">Artists</h1>
                                        {result.artists.map((item, index) => (
                                            <ArtistItem key={index} name={item.name} desc={item.genre} id={item.id} image={item.image} />
                                        ))}
                                    </>
                                )}
                                {result.albums.length > 0 && (
                                    <>
                                        <h1 className="my-5 font-bold text-2xl">Albums</h1>
                                        {result.albums.map((item, index) => (
                                            <AlbumItem key={index} name={item.name} release={item.dateOfRelease} id={item.id} image={item.image} artist={item.artistName} />
                                        ))}
                                    </>
                                )}
                                {result.playlists.length > 0 && (
                                    <>
                                        <h1 className="my-5 font-bold text-2xl">Playlists</h1>
                                        {result.playlists.map((item, index) => (
                                            <PlaylistItem key={index} name={item.name} createBy={item.createBy} id={item.id} image={item.image} />
                                        ))}
                                    </>
                                )}
                            </div>
                        </div>
                    )}
                    {activeView === 'songs' && (
                        <>
                            {result.tracks.length > 0 ? (
                                <div className="mb-4 mt-4">
                                    <div className="flex flex-wrap">
                                        {result.tracks.map((item, index) => (
                                            <SongItem key={index} name={item.name} artistName={item.artistName} id={item.trackId} image={item.image} />
                                        ))}
                                    </div>
                                </div>
                            ) : (
                                <p className='my-5 font-bold text-2xl'>No Song available</p>
                            )
                            }
                        </>
                    )
                    }
                    {activeView === 'playlists' && (
                        <>
                            {result.playlists.length > 0 ? (
                                <div className="mb-4 mt-4">
                                    <div className="flex overflow-auto">
                                        {result.playlists.map((item, index) => (
                                            <PlaylistItem name={item.name} createBy={item.createBy} id={item.playlistId} image={item.image} />
                                        ))}
                                    </div>
                                </div>
                            ) : (
                                <p className='my-5 font-bold text-2xl'>No playlist available</p>
                            )
                            }
                        </>

                    )
                    }
                    {activeView === 'albums' && (
                        <>
                            {result.albums.length > 0 ? (
                                <div className="mb-4 mt-4">
                                    <div className="flex overflow-auto">
                                        {result.albums.map((item, index) => (
                                            <AlbumItem key={index} name={item.name} release={item.dateOfRelease} artist={artists[item.id]} image={item.image} id={item.id} />
                                        ))}
                                    </div>
                                </div>
                            ) : (
                                <p className='my-5 font-bold text-2xl'>No album available</p>
                            )
                            }
                        </>
                    )
                    }
                    {activeView === 'artists' && (
                        <>
                            {result.artists.length > 0 ? (
                                <div className="mb-4 mt-4">
                                    <div className="flex overflow-auto">
                                        {result.artists.map((item, index) => (
                                            <ArtistItem key={index} name={item.name} desc={item.genre} id={item.id} image={item.image} />
                                        ))}
                                    </div>
                                </div>
                            ) : (
                                <p className='my-5 font-bold text-2xl'>No artist available</p>

                            )
                            }
                        </>
                    )
                    }
                </div>
            )

            }

        </>
    );
};

export default DisplaySearch;
