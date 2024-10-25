import React, { useContext } from 'react';
// Assets Connection
import { albumsData } from '../assets/assets';
// Component Connection
import Navbar from './Navbar'
import AlbumItem from './AlbumItem';
import SongItem from './SongItem';
import ArtistItem from './ArtistItem';
import axios from 'axios';
import { LoginContext } from '../context/LoginContext';
import { useState } from 'react';
import { useEffect } from 'react';
import albumService from '../services/albumService';
import artistService from '../services/artistService';
import trackService from '../services/trackService';

const DisplayHome = () => {
  // const getTrack= () => {
  //   axios
  //   .get("")
  // }
  const { isLoggedIn } = useContext(LoginContext); // Lấy isLoggedIn và user từ context
  const [albums, setAlbums] = useState([]);
  const [artists, setArtists] = useState({}); // State lưu trữ tên nghệ sĩ
  const [songsData, setSongsData] = useState([]);

  useEffect(() => {
    const fetchAlbums = async () => {
      try {
        const data = await albumService.getAllAlbums(); // Gọi API lấy dữ liệu album
        setAlbums(data); // Lưu dữ liệu vào state albums
  
        // Lấy tên nghệ sĩ cho mỗi album
        const artistPromises = data.map(async (album) => {
          const artistName = await artistService.getNameArtistById(album.artistId);
          return { [album.id]: artistName };
        });
  
        const artistResults = await Promise.all(artistPromises);
        const artistMap = Object.assign({}, ...artistResults); // Tạo object từ kết quả
        setArtists(artistMap);
      } catch (error) {
        console.error(error.message); // Lưu lỗi nếu xảy ra
      }
    };
  
    const fetchRecentlySong = async () => {
      try {
        const data = await trackService.getRecentlyPlayedTracks(); 
        setSongsData(data); 
      } catch (error) {
        console.error(error.message); // Lưu lỗi nếu xảy ra
      }
    };
  
    // Gọi cả hai hàm fetch song song
    fetchAlbums();
    fetchRecentlySong();
  }, []);
 

  return (
    <>
      {
        isLoggedIn && (<div className="mb-4">
          <h1 className="my-5 font-bold text-2xl">Popular Album</h1>
          <div className="flex overflow-auto">
            {albums.map((item, index) => (
              <AlbumItem key={index} name={item.name} release={item.dateOfRelease} artist={artists[item.id]} image={item.image} id={item.id} />
            ))}
          </div>
        </div>)
      }

    {
      isLoggedIn && songsData.length > 0 && (
        <div className="mb-4">
        <h1 className="my-5 font-bold text-2xl">Recently Played</h1>
        <div className="flex overflow-auto">
          {songsData.map((item, index) => (
            <SongItem key={index} name={item.name} artistName={item.artistName} id={item.trackId} image={item.image} />
          ))}
        </div>
      </div>
      )
    }

      <div className="mb-4">
        <h1 className="my-5 font-bold text-2xl">Your Favorite Artists</h1>
        <div className="flex overflow-auto">
          {albumsData.map((item, index) => (
            <ArtistItem key={index} name={item.name} desc={item.desc} id={item.id} image={item.image} />
          ))}
        </div>
      </div>
      {/* <div className="mb-4">
        <h1 className="my-5 font-bold text-2xl">Recently Played</h1>
        <div className="flex overflow-auto">
          {songsData.map((item, index) => (
            <SongItem key={index} name={item.name} desc={item.desc} id={item.id} image={item.image} />
          ))}
        </div>
      </div> */}
    </>
  )
}

export default DisplayHome;