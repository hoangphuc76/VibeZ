import React, { useState, useEffect, useRef } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import trackService from '../../services/trackService';
import albumService from '../../services/albumService';
import ArtistDashboardService from '../../services/artistDashboardService';
import { FiEdit, FiTrash } from 'react-icons/fi';
import CreateAlbum from './CreateAlbum';
import './music.css';

const MusicTab = () => {
  const { albumId } = useParams();
  const navigate = useNavigate();
  const [activeTab, setActiveTab] = useState(() => {
    const savedTab = localStorage.getItem('activeTab');
    return savedTab ? savedTab : 'songs';
  });
  const [tracks, setTracks] = useState([]);
  const [albums, setAlbums] = useState([]);
  const [loading, setLoading] = useState(false);
  const [loadingAlbums, setLoadingAlbums] = useState(false);
  const [errorTracks, setErrorTracks] = useState(null);
  const [errorAlbums, setErrorAlbums] = useState(null);
  const [isPlaying, setIsPlaying] = useState(null);
  const [currentTrack, setCurrentTrack] = useState(null);
  const audioRef = useRef(new Audio());
  const [artist, setArtist] = useState();

  const showTab = (tab) => {
    setActiveTab(tab);
    localStorage.setItem('activeTab', tab);
  };

  const formatDate = (dateString) => {
    const date = new Date(dateString);
    const day = String(date.getDate()).padStart(2, '0');
    const month = String(date.getMonth() + 1).padStart(2, '0');
    const year = date.getFullYear();
    return `${day}-${month}-${year}`;
  };

  useEffect(() => {
    const userId = JSON.parse(localStorage.getItem('userId'));
    const getArtist = async () => {
      try {
        const artistData = await ArtistDashboardService.getArtistByUserId(userId);
        console.log("Fetched artist:", artistData);
        setArtist(artistData);
      } catch (error) {
        console.error('Error fetching artist:', error);
      }
    };
    getArtist();
  }, []);

  useEffect(() => {
    if (!artist?.id) return;
    const fetchAlbums = async () => {
      setLoadingAlbums(true);
      try {
        const data = await albumService.getAlbumsByArtistId(artist.id);
        console.log("Fetched albums:", data);
        setAlbums(data);
      } catch (error) {
        setErrorAlbums("Error fetching albums");
        console.error("Error fetching albums:", error);
      } finally {
        setLoadingAlbums(false);
      }
    };

    const fetchTracks = async () => {
      setLoading(true);
      try {
        const data = await trackService.getTrackByArtistId(artist.id);
        console.log("Fetched tracks:", data);
        setTracks(data);
      } catch (error) {
        setErrorTracks("Error fetching tracks");
        console.error("Error fetching tracks:", error);
      } finally {
        setLoading(false);
      }
      //}
    };

    fetchAlbums();
    fetchTracks();
  }, [artist]);

  const handlePlay = (track) => {
    if (currentTrack?.trackId !== track.trackId) {
      audioRef.current.src = track.path;
      audioRef.current.play();
      setCurrentTrack(track);
      setIsPlaying(true);
    } else {
      if (isPlaying) {
        audioRef.current.pause();
      } else {
        audioRef.current.play();
      }
      setIsPlaying(!isPlaying);
    }
  };

  useEffect(() => {
    return () => {
      audioRef.current.pause();
      audioRef.current.src = "";
    };
  }, []);

  const handleCreateAlbum = () => {
    navigate('/artistdashboard/music/album/create');
  };

  const handleEditAlbum = (albumId) => {
    navigate(`/artistdashboard/music/album/${albumId}/edit`);
  };
  const handleDeleteAlbum = async (albumId) => {
    try {
      await trackService.deleteTracksByAlbumId(albumId);
      await albumService.deleteAlbum(albumId);
      setAlbums(albums.filter(album => album.id !== albumId));
      alert("Album deleted successfully!");
    } catch (error) {
      console.error("Error deleting album", error);
      alert("Failed to delete album.");
    };
  };

  return (
    <div className="app-container">
      <main id="music-section">
        <h2>Music</h2>
        <div className="tabs">
          <button onClick={() => showTab('songs')}>Songs</button>
          <button onClick={() => showTab('albums')}>Albums</button>
          <button onClick={() => showTab('playlists')}>Playlists</button>
          <button onClick={() => showTab('upcoming')}>Upcoming</button>
        </div>

        {/* Songs Tab */}
        {activeTab === 'songs' && (
          <div id="songs" className="tab-content p-6 bg-gray-100 rounded-lg shadow-md">
            <div className="search-bar flex items-center gap-4 mb-4">
              {/* Search Input */}
              <input
                type="text"
                placeholder="Search"
                className="w-full py-2 px-4 rounded-lg border border-gray-300 focus:outline-none focus:ring-2 focus:ring-blue-500"
              />

              {/* Filter Select */}
              <select
                className="py-2 px-4 rounded-lg border border-gray-300 bg-white focus:outline-none focus:ring-2 focus:ring-blue-500"
              >
                <option>Last 24 hours</option>
                <option>Last 7 days</option>
                <option>Last 28 days</option>
              </select>
            </div>
            <div className="table-container">
              {loading && <p>Loading songs...</p>}
              {errorTracks && <p>Error: {errorTracks}</p>}
              <table>
                <thead>
                  <tr>
                    <th>No</th>
                    <th>Title</th>
                    <th>Genre</th>
                    <th>Listeners</th>
                    <th>First Added</th>
                    <th>Track</th>
                  </tr>
                </thead>
                <tbody id="songs-data">
                  {tracks.filter(track => !track.pendingApproval).map((track, index) => (
                    <tr
                      key={track.trackId}
                      className={currentTrack?.trackId === track.trackId ? 'playing' : ''}
                      onClick={() => handlePlay(track)}
                    >
                      <td>{index + 1}</td>
                      <td>
                        <div style={{ display: 'flex', alignItems: 'center' }}>
                          <img src={track.image} alt={track.name} style={{ width: '50px', height: '50px', marginRight: '10px' }} />
                          <span>{track.name || 'Unknown Title'}</span>
                        </div>
                      </td>
                      <td>{track.genre || 'Unknown Genre'}</td>
                      <td>{track.listener || 0}</td>
                      <td>{track.createDate ? formatDate(track.createDate) : 'N/A'}</td>
                      <td>
                        <audio className="audio-player" controls style={{ width: "100%" }}>
                          <source src={track.path} type="audio/mpeg" />
                          Your browser does not support the audio element.
                        </audio>
                      </td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>
          </div>
        )}

        {/* Albums Tab */}
        {activeTab === 'albums' && (
          <div id="albums" className="tab-content p-6 bg-gray-100 rounded-lg shadow-md">
            <div className="search-bar flex items-center gap-4 mb-4">
              <input
                type="text"
                placeholder="Search"
                className="w-full py-2 px-4 rounded-lg border border-gray-300 focus:outline-none focus:ring-2 focus:ring-blue-500"
              />

              <select
                className="py-2 px-4 rounded-lg border border-gray-300 bg-white focus:outline-none focus:ring-2 focus:ring-blue-500"
              >
                <option>Last 24 hours</option>
                <option>Last 7 days</option>
                <option>Last 28 days</option>
              </select>

              <button
                onClick={handleCreateAlbum}
                className="bg-blue-500 text-white py-2 px-4 rounded-lg hover:bg-blue-600 flex items-center shadow-md transition-colors duration-200"
              >
                Create Album
              </button>
            </div>

            <div className="table-container">
              {loadingAlbums && <p>Loading albums...</p>}
              {errorAlbums && <p>Error: {errorAlbums}</p>}
              <table>
                <thead>
                  <tr>
                    <th>No</th>
                    <th>Title</th>
                    <th>Release Date</th>
                    <th>Nation</th>
                    <th>Actions</th>
                  </tr>
                </thead>
                <tbody id="albums-data">
                  {albums.map((album, index) => (
                    <tr
                      key={album.id}
                      onClick={() => navigate(`/artistdashboard/music/album/${album.id}`)}
                      style={{ cursor: 'pointer' }}
                    >
                      <td>{index + 1}</td>
                      <td>
                        <div style={{ display: 'flex', alignItems: 'center' }}>
                          <img src={album.image} alt={album.name} style={{ width: '50px', height: '50px', marginRight: '10px' }} />
                          <span>{album.name || 'Unknown Title'}</span>
                        </div>
                      </td>
                      <td>{album.dateOfRelease ? formatDate(album.dateOfRelease) : 'N/A'}</td>
                      <td>{album.nation || 'Unknown Nation'}</td>
                      <td>
                        <div style={{ display: 'flex', alignItems: 'center', gap: '1rem' }}>
                          <FiEdit
                            className="icon edit-icon"
                            onClick={(e) => {
                              e.stopPropagation();
                              handleEditAlbum(album.id);
                            }}
                            style={{
                              cursor: 'pointer',
                            }}
                          />
                          <FiTrash
                            className="icon delete-icon"
                            onClick={(e) => {
                              e.stopPropagation();
                              handleDeleteAlbum(album.id);
                            }}
                            style={{
                              cursor: 'pointer',
                            }}
                          />
                        </div>
                      </td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>
          </div>
        )}
      </main>
    </div>
  );
};

export default MusicTab;
