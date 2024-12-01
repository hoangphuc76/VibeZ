import React, { useEffect, useState } from 'react';
import { InputNumber, Table, Modal, Spin, Input } from 'antd';
import { getArtistApproval, UpdateArtist, updateTrackToVerify, DeleteApproval, UpdateUserRole } from '../../services/adminService';
import trackService from '../../services/trackService';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faFile, faArrowUp } from '@fortawesome/free-solid-svg-icons';

const { Search } = Input;

function ArtistApprovalDisplay() {
  const [pageSize, setPageSize] = useState(5);
  const [filteredData, setFilteredData] = useState([]);
  const [dataTable, setDataTable] = useState([]);
  const [loading, setLoading] = useState(true);
  const [lyricsUrl, setLyricUrl] = useState('');
  const [lyrics, setLyrics] = useState('');
  const [approveData, setApproveData] = useState(null);

  // Fetch lyrics khi `lyricsUrl` thay đổi
  useEffect(() => {
    const fetchLyrics = async () => {
      try {
        const response = await fetch(lyricsUrl);
        const text = await response.text();

        const processedLyrics = text
          .replace(/\[.*?\]/g, '') // Xóa timestamp
          .split('\n')
          .map(line => line.trim())
          .filter(line => line.length > 0)
          .join('\n');
        setLyrics(processedLyrics);
      } catch (error) {
        console.error('Error fetching LRC file:', error);
      }
    };

    if (lyricsUrl) {
      fetchLyrics();
    }
  }, [lyricsUrl]);

  // Gọi `approveArtist` khi `lyrics` và `approveData` có sẵn
  useEffect(() => {
    if (lyrics && approveData) {
      const {id, name, genre, image, imgBackground, nation, email, userId, songName, songImg, minute, second, song, lyricLRC } = approveData;
      approveArtist(id, name, genre, image, imgBackground, nation, email, userId, songName, songImg, minute, second, song, lyricLRC);

      setApproveData(null); // Reset sau khi hoàn tất
      setLyrics(''); // Reset lyrics
    }
  }, [lyrics, approveData]);

  const columns = [
    {
      title: 'No',
      render: (_, __, index) => index + 1,
      width: '5%',
    },
    {
      title: 'Profile',
      dataIndex: 'image',
      render: (image) => (
        <img src={image} alt="Profile" className="w-8 h-8 rounded-full" />
      ),
      width: '10%',
    },
    {
      title: 'Writer Name',
      dataIndex: 'artistName',
      sorter: (a, b) => a.artistName.localeCompare(b.artistName),
      width: '10%',
    },
    {
      title: 'Nation',
      dataIndex: 'nation',
      width: '10%',
    },
    {
      title: 'Genre',
      dataIndex: 'genre',
      width: '10%',
    },
    {
      title: 'Song Name',
      dataIndex: 'songName',
      width: '10%',
    },
    {
      title: 'Song Image',
      dataIndex: 'songImg',
      render: (songImg) => (
        <img src={songImg} alt="Song" className="w-8 h-8 rounded-full" />
      ),
      width: '10%',
    },
    {
      title: 'LyricLRC',
      dataIndex: 'lyricLRC',
      render: (text) => (
        text ? (
          <a href={text} download target="_blank" rel="noopener noreferrer">
            <FontAwesomeIcon className="text-[20px]" icon={faFile} />
          </a>
        ) : (
          <span>Không có file</span>
        )
      ),
      width: '5%',
    },
    {
      title: 'Date Created',
      dataIndex: 'createDate',
      width: '20%',
    },
    {
      title: 'Track',
      dataIndex: 'song',
      render: (song) => (
        <audio controls>
          <source src={song} type="audio/mpeg" />
          Your browser does not support the audio element.
        </audio>
      ),
      width: '10%',
    },
    {
      title: 'Action',
      dataIndex: 'action',
      render: (_, record) => (
        <div className="flex gap-2">
          <button
            className="px-2 py-1 text-white bg-blue-500 rounded hover:bg-blue-600"
            onClick={() => {
              setLyricUrl(record.lyricLRC);
              setApproveData({
                id : record.id,
                name: record.artistName,
                genre: record.genre,
                image: record.image,
                imgBackground: record.imgBackground,
                nation: record.nation,
                email: record.email,
                userId: record.userId,
                minute: record.minute,
                second: record.section,
                songImg: record.songImg,
                song: record.song,
                songName: record.songName,
                lyricLRC: record.lyricLRC
              });
            }}
          >
            Allow
          </button>
          <button
            className="px-2 py-1 text-white bg-red-500 rounded hover:bg-red-600"
            onClick={() => confirmDeleteApproval(record.id)}
          >
            Delete
          </button>
        </div>
      ),
      width: '10%',
    },
  ];

  const approveArtist = async (id, name, genre, image, imgBackground, nation, email, userId, songName, songImg, minute, second, song, lyricLRC) => {
  
    Modal.confirm({
      title: 'Are you sure to approve this user as an artist?',
      onOk: async () => {
        try {
          const response = await UpdateArtist(name, genre, image, imgBackground, nation, email, userId);
          if (response) {
            var respone1 = await updateTrackToVerify(songName, lyrics, genre, 0, parseInt(minute, 10), parseInt(second, 10), response.id, song, songImg, lyricLRC);
            if (respone1) {
              await DeleteApproval(id);
              await UpdateUserRole(userId);
            }
          }
          fetchData();
        } catch (error) {
          console.error('Error approving artist:', error.message || error);
        }
      },
      okText: 'Yes',
      cancelText: 'No',
    });
  };
  

  const confirmDeleteApproval = (id) => {
    Modal.confirm({
      title: 'Are you sure you want to delete this track?',
      onOk: async () => {
        try {
          await DeleteApproval(id);
          fetchData();
        } catch (error) {
          console.error('Error deleting track:', error.message || error);
        }
      },
      okText: 'Yes',
      cancelText: 'No',
    });
  };

  const fetchData = async () => {
    setLoading(true);
    try {
      const response = await getArtistApproval();
      setDataTable(response);
      setFilteredData(response);
    } catch (error) {
      console.error('Error fetching data:', error.message || error);
    } finally {
      setLoading(false);
    }
  };

  const onSearch = (value) => {
    const filtered = dataTable.filter(item =>
      item.artistName.toLowerCase().includes(value.toLowerCase())
    );
    setFilteredData(filtered);
  };

  const onChange = (value) => {
    setPageSize(value);
  };

  useEffect(() => {
    fetchData();
  }, []);

  return (
    <div>
      <div className="p-5 bg-gray-100 rounded-lg shadow-md">
        <p className="text-lg font-semibold text-gray-800 mb-3 font-extrabold">Approval</p>
        <div className="flex items-center justify-between">
          <div className="flex items-center">
            <span className="mr-2">Show</span>
            <InputNumber
              min={1}
              max={15}
              value={pageSize}
              onChange={onChange}
              className="border border-gray-300 w-12 rounded"
            />
            <span className="ml-2">entries</span>
          </div>
          <Search
            placeholder="Search by writer name"
            allowClear
            onSearch={onSearch}
            className="w-52"
          />
        </div>
      </div>

      <div className="mt-5">
        {loading ? (
          <div className="flex justify-center items-center h-32">
            <Spin size="large" />
          </div>
        ) : (
          <Table
            columns={columns}
            dataSource={filteredData}
            pagination={{ pageSize }}
            rowKey={(record) => record.trackId}
          />
        )}
      </div>
    </div>
  );
}

export default ArtistApprovalDisplay;
