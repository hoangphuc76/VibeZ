import React, { useEffect, useState } from 'react';
import StatsGrid from './SubComponent/StatsGrid';
import ChartComponent from './SubComponent/ChartComponent';
import TopSongs from './SubComponent/TopSongs';
import UploadSong from './SubComponent/UploadSong';
import CreateAlbum from './CreateAlbum';
import ArtistDashboardService from '../../services/artistDashboardService';
import './styles.css'

const Dashboard = () => {
    const [listener, setListener] = useState();
    const [follower, setFollower] = useState();
    const [unfollower, setUnfollower] = useState();
    const [savedPlaylist, setSavedPlaylist] = useState();
    const [startDate, setStartDate] = useState("");
    const [endDate, setEndDate] = useState(new Date().toISOString().split('T')[0]); // Ngày hiện tại
    const [customDate, setCustomDate] = useState(false);
    const [artist, setArtist] = useState();
    const [songs, setSongs] = useState([]);

    useEffect(() => {
        const fetchData = async () => {
            try {
                const userId = JSON.parse(localStorage.getItem('userId'));
                const artist = await ArtistDashboardService.getArtistByUserId(userId);
                setArtist(artist);
                const todayFormatted = new Date();
                const lt = await ArtistDashboardService.getTotalListener(artist.id, todayFormatted, todayFormatted);
                console.log(lt);
                setListener(lt);
                const fl = await ArtistDashboardService.getTotalFollow(artist.id, todayFormatted, todayFormatted);
                setFollower(fl);
                const unFl = await ArtistDashboardService.getTotalUnFollow(artist.id, todayFormatted, todayFormatted);
                setUnfollower(unFl);
                const sv = await ArtistDashboardService.getTotalSavedPlaylist(artist.id, todayFormatted, todayFormatted);
                setSavedPlaylist(sv);
                const topSongs = await ArtistDashboardService.getTopSongs(artist.id, todayFormatted, todayFormatted);
                setSongs(topSongs);
            } catch (error) {
                console.error(error.message);
            }
        }
        fetchData();
    }, [])


    const handleSelectChange = async (e) => {
        const selectedValue = e.target.value;
        const today = new Date();
        let calculatedStartDate;

        if (selectedValue === "24h") {
            calculatedStartDate = new Date(Date.now() - 24 * 60 * 60 * 1000);
        } else if (selectedValue === "7d") {
            calculatedStartDate = new Date(Date.now() - 7 * 24 * 60 * 60 * 1000);
        } else if (selectedValue === "28d") {
            calculatedStartDate = new Date(Date.now() - 28 * 24 * 60 * 60 * 1000);
        }

        try {
            const lt = await ArtistDashboardService.getTotalListener(artist.id, calculatedStartDate, today);
            setListener(lt);
            const fl = await ArtistDashboardService.getTotalFollow(artist.id, calculatedStartDate, today);
            setFollower(fl);
            const unFl = await ArtistDashboardService.getTotalUnFollow(artist.id, calculatedStartDate, today);
            setUnfollower(unFl);
            const sv = await ArtistDashboardService.getTotalSavedPlaylist(artist.id, calculatedStartDate, today);
            setSavedPlaylist(sv);
            const topSongs = await ArtistDashboardService.getTopSongs(artist.id, calculatedStartDate, today);
            setSongs(topSongs);
        } catch (error) {
            console.error("Error fetching data:", error);
        }
    };

    const [data, setData] = useState({
        listeners: listener,
        newFollowers: follower,
        unfollows: unfollower,
        savedPlaylists: savedPlaylist,
        topSongs: [
            { title: 'Song A', listeners: 25 },
            { title: 'Song B', listeners: 23 },
            { title: 'Song C', listeners: 20 },
            { title: 'Song D', listeners: 20 },
            { title: 'Song E', listeners: 18 },
            { title: 'Song F', listeners: 17 },
            { title: 'Song G', listeners: 15 },
        ],
        chartData: {
            labels: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
            men: [12, 19, 3, 5, 2, 3, 9, 10, 13, 7, 12, 6],
        },
    });

    return (
        <div className="dashboard-container">
            <div className="dashboard-header">
                <h2>Dashboard</h2>
                <div className="header-controls !items-center !flex ">
                    <div>
                        <select className='time-frame' onChange={handleSelectChange}>
                            <option value="24h">Last 24 hours</option>
                            <option value="7d">Last 7 days</option>
                            <option value="28d">Last 28 days</option>
                        </select>
                    </div>
                    <div style={{ marginTop: '10px' }} >
                        <label>
                            Start Date:
                            <input
                                type="date"
                                value={startDate}
                                onChange={(e) => setStartDate(e.target.value)}
                            />
                        </label>
                        <label style={{ marginLeft: '10px' }}>
                            End Date:
                            <input
                                type="date"
                                value={endDate}
                                onChange={(e) => setEndDate(e.target.value)}
                            />
                        </label>
                    </div>
                    <button className='button'>Download as CSV</button>
                </div>
            </div>
            <StatsGrid listener={listener} follower={follower} unfollower={unfollower} savedPlaylists={savedPlaylist} />
            <div className="charts">
                <ChartComponent chartData={data.chartData} />
                <TopSongs songs={songs} />
            </div>
        </div>
    );
};

export default Dashboard;
