import React, { useEffect, useState } from 'react';
import { InputNumber, Table, Modal, Spin, Input } from 'antd';
import { getAdminApproval } from '../../services/adminService';
import trackService from '../../services/trackService';
import { data } from 'autoprefixer';

const { Search } = Input;

function AdminDisplayApproval() {
  const [pageSize, setPageSize] = useState(5);
  const [filteredData, setFilteredData] = useState([]);
  const [dataTable, setDataTable] = useState([]);
  const [loading, setLoading] = useState(true);

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
      width: '5%',
    },
    {
      title: 'Writer Name',
      dataIndex: 'writerName',
      sorter: (a, b) => a.writerName.localeCompare(b.writerName),
      width: '15%',
    },
    {
      title: 'Album Name',
      dataIndex: 'albumName',
      width: '15%',
    },
    {
      title: 'Song Name',
      dataIndex: 'songName',
      width: '15%',
    },
    {
      title: 'Date Created',
      dataIndex: 'dateCreated',
      width: '15%',
    },
    {
      title: 'Track',
      dataIndex: 'path',
      render: (path) => (
        <audio controls>
          <source src={path} type="audio/mpeg" />
          Your browser does not support the audio element.
        </audio>
      ),
      width: '25%',
    },
    {
      title: 'Action',
      dataIndex: 'action',
      render: (_, record) => (
        <div className="flex gap-2">
          <button
            className="px-2 py-1 text-white bg-blue-500 rounded hover:bg-blue-600"
            onClick={() => approveTrack(record.trackId)}
          >
            Allow
          </button>
          <button
            className="px-2 py-1 text-white bg-red-500 rounded hover:bg-red-600"
            onClick={() => confirmDeleteTrack(record.trackId)}
          >
            Delete
          </button>
        </div>
      ),
      width: '10%',
    },
  ];

  const confirmDeleteTrack = (id) => {
    Modal.confirm({
      title: 'Are you sure you want to delete this track?',
      onOk: async () => {
        try {
          await trackService.deleteTrack(id);
          fetchData();
        } catch (error) {
          console.error('Error deleting track:', error.message || error);
        }
      },
      okText: 'Yes',
      cancelText: 'No',
    });
  };

  const approveTrack = (id) => {
    Modal.confirm({
      title: 'Are you sure to approve this track?',
      onOk: async () => {
        try {
          await trackService.changeStatus(id);
          fetchData();
        } catch (error) {
          console.error('Error approving track:', error.message || error);
        }
      },
      okText: 'Yes',
      cancelText: 'No',
    });
  };

  const fetchData = async () => {
    setLoading(true);
    try {
      const response = await getAdminApproval();
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
      item.writerName.toLowerCase().includes(value.toLowerCase())
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
              value={pageSize} // Use value instead of defaultValue
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

export default AdminDisplayApproval;
