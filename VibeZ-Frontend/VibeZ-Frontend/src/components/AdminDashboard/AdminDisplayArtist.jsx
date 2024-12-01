import React, { useEffect, useState } from 'react';
import { InputNumber, Table, Spin, message, Modal } from 'antd';
import { Input } from 'antd';
import deleteIcon from '../../assets/delete-icon.svg';
import { getArtistData } from '../../services/adminService';
import { deleteArtistApi } from '../../services/artistService';
const { Search } = Input;

function AdminDisplayArtist() {
  const [pageSize, setPageSize] = useState(5);
  const [filteredData, setFilteredData] = useState([]);
  const [dataTable, setDataTable] = useState([]);
  const [loading, setLoading] = useState(true);

  const columns = [
    {
      title: 'No',
      render: (text, record, index) => index + 1,
      width: '5%',
    },
    {
      title: 'Profile',
      dataIndex: 'image',
      render: (text) => (
        <img src={text || 'default-profile.png'} alt="Profile" className="w-8 h-8 rounded-full" />
      ),
      width: '10%',
    },
    {
      title: 'Writer Name',
      dataIndex: 'name',
      sorter: (a, b) => a.name.localeCompare(b.name),
      width: '20%',
    },
    {
      title: 'Email',
      dataIndex: 'email',
      width: '25%',
    },
    {
      title: 'Date Of Birth',
      dataIndex: 'dob',
      width: '15%',
    },
    {
      title: 'Song',
      dataIndex: 'totalSong',
      width: '15%',
    },
    {
      title: 'Album',
      dataIndex: 'totalAlbum',
      width: '15%',
    },
    {
      title: 'Action',
      dataIndex: 'action',
      render: (text, record) => (
        <div className="flex gap-2">
          <button 
            className="px-2 py-1 text-white rounded hover:bg-red-600" 
            onClick={() => confirmDeleteArtist(record.id)}
          >
            <img src={deleteIcon} alt="Delete" />
          </button>
        </div>
      ),
      width: '10%',
    },
  ];

  const confirmDeleteArtist = (id) => {
    Modal.confirm({
      title: 'Are you sure you want to delete this artist?',
      onOk: () => deleteArtist(id),
      okText: 'Yes',
      cancelText: 'No',
    });
  };

  const deleteArtist = async (id) => {
    try {
      console.log(`Deleting artist with id: ${id}`);
      await deleteArtistApi(id);
      message.success('Artist deleted successfully');
      fetchData();
    } catch (error) {
      console.error(error);
      message.error('Failed to delete artist');
    }
  };

  const onSearch = (value) => {
    const filtered = dataTable.filter(item => 
      item.name.toLowerCase().includes(value.toLowerCase())
    );
    setFilteredData(filtered);
  };

  const onChange = (value) => {
    setPageSize(value);
  };

  const fetchData = async () => {
    setLoading(true); 
    try {
      const response = await getArtistData(); 
      setDataTable(response); 
      setFilteredData(response); 
    } catch (error) {
      console.error(error);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchData();
  }, []);

  return (
    <div>
      <div className="p-5 bg-gray-100 rounded-lg shadow-md">
        <p className="text-lg font-semibold text-gray-800 mb-3 font-extrabold">Writer List</p>
        <div className="flex items-center justify-between">
          <div className="flex items-center">
            <span className="mr-2">Show</span>
            <InputNumber
              min={1}
              max={20}
              defaultValue={5}
              onChange={onChange}
              className="border border-gray-300 w-12 rounded"
            />
            <span className="ml-2">entries</span>
          </div>
          <Search
            placeholder="Search here"
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
            rowKey={(record) => record.id}
          />
        )}
      </div>
    </div>
  );
}

export default AdminDisplayArtist;
