import React, { useState, useEffect } from 'react';
import { Table, Input, Spin } from 'antd';
import { Bar } from 'react-chartjs-2';
import { Chart as ChartJS, CategoryScale, LinearScale, BarElement, Title, Tooltip, Legend } from 'chart.js';
import { getAdminHome } from '../../services/adminService';

ChartJS.register(CategoryScale, LinearScale, BarElement, Title, Tooltip, Legend);

function AdminDisplayHome() {
  const [dataTotal, setDataTotal] = useState({});
  const [loading, setLoading] = useState(true);
  const [searchText, setSearchText] = useState('');
  const [filteredData, setFilteredData] = useState([]);
  const [chartData, setChartData] = useState(null);
  const [dataTable, setDataTable] = useState([]);

  const columns = [
    {
      title: 'No',
      dataIndex: '',
      render: (text, record, index) => index + 1,
    },
    {
      title: 'Artist Name',
      dataIndex: 'name', 
      width: '30%',
    },
    {
      title: 'Joining Date',
      dataIndex: 'dob',
      sorter: (a, b) => new Date(a.dob) - new Date(b.dob),
    },
    {
      title: 'Total Songs',
      dataIndex: 'totalSong',
      width: '40%',
    },
  ];

  const fetchData = async () => {
    try {
      const response = await getAdminHome();
      const { dataTotal, dataTable } = response;

      setDataTotal(dataTotal);

      setDataTable(dataTable);
      setFilteredData(dataTable);
    } catch (error) {
      console.error('Failed to fetch data:', error);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchData();
  }, []);

  useEffect(() => {
    if (dataTotal) {
      setChartData({
        labels: ['Categories'],
        datasets: [
          { label: 'Artists', data: [dataTotal.totalArtist || 0], backgroundColor: 'rgba(255, 99, 132, 0.6)' },
          { label: 'Albums', data: [dataTotal.totalAlbum || 0], backgroundColor: 'rgba(54, 162, 235, 0.6)' },
          { label: 'Songs', data: [dataTotal.totalSong || 0], backgroundColor: 'rgba(255, 206, 86, 0.6)' },
          { label: 'Playlists', data: [dataTotal.totalPlaylist || 0], backgroundColor: 'rgba(75, 192, 192, 0.6)' },
          { label: 'Users', data: [dataTotal.totalUser || 0], backgroundColor: 'rgba(153, 102, 255, 0.6)' },
        ],
      });
    }
  }, [dataTotal]);

  useEffect(() => {
    setFilteredData(
      dataTable.filter((item) => item.name.toLowerCase().includes(searchText.toLowerCase()))
    );
  }, [searchText, dataTable]);

  if (loading) {
    return (
      <div className="flex justify-center items-center h-screen">
        <Spin size="large" />
      </div>
    );
  }

  return (
    <>
      <div className="flex justify-between p-4">
        <div className="bg-gray-800 text-white p-4 flex flex-col items-center justify-center w-1/5 h-32 rounded-lg shadow-lg mx-2">
          <h3 className="text-2xl font-bold">{dataTotal.totalArtist || '-'}</h3>
          <p className="text-sm">Total Artists</p>
        </div>
        <div className="bg-gray-800 text-white p-4 flex flex-col items-center justify-center w-1/5 h-32 rounded-lg shadow-lg mx-2">
          <h3 className="text-2xl font-bold">{dataTotal.totalAlbum || '-'}</h3>
          <p className="text-sm">Total Albums</p>
        </div>
        <div className="bg-gray-800 text-white p-4 flex flex-col items-center justify-center w-1/5 h-32 rounded-lg shadow-lg mx-2">
          <h3 className="text-2xl font-bold">{dataTotal.totalSong || "-"}</h3>
          <p className="text-sm">Total Songs</p>
        </div>
        <div className="bg-gray-800 text-white p-4 flex flex-col items-center justify-center w-1/5 h-32 rounded-lg shadow-lg mx-2">
          <h3 className="text-2xl font-bold">{dataTotal.totalPlaylist || "-"}</h3>
          <p className="text-sm">Total Playlists</p>
        </div>
        <div className="bg-gray-800 text-white p-4 flex flex-col items-center justify-center w-1/5 h-32 rounded-lg shadow-lg mx-2">
          <h3 className="text-2xl font-bold">{dataTotal.totalUser || "-"}</h3>
          <p className="text-sm">Total Users</p>
        </div>
      </div>
      <div className="p-4">
        <div className="flex justify-between">
          <div className="w-1/2 pr-2">
            <Input
              placeholder="Search by name"
              value={searchText}
              onChange={(e) => setSearchText(e.target.value)}
              className="mb-4"
            />
            <Table columns={columns} dataSource={filteredData} rowKey={(record) => record.id} />
          </div>
          <div className="w-1/2 pl-2 flex justify-center items-center">
            {chartData && (
              <Bar
                data={chartData}
                options={{
                  responsive: true,
                  plugins: {
                    legend: { position: 'top' }
                  }
                }}
              />
            )}
          </div>
        </div>
      </div>
    </>
  );
}

export default AdminDisplayHome;
