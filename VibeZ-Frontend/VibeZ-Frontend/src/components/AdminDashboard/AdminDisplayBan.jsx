import React, { useEffect, useState } from 'react';
import { InputNumber, Table, Spin } from 'antd';
import { Input } from 'antd';
import { getAdminBan } from '../../services/adminService';

const { Search } = Input;

function AdminDisplayBan() {
  const [pageSize, setPageSize] = useState(5);
  const [filteredData, setFilteredData] = useState([]);
  const [dataTable, setDataTable] = useState([]);
  const [loading, setLoading] = useState(true);

  const columns = [
    {
      title: 'No',
      dataIndex: 'key',
      sorter: (a, b) => a.key - b.key,
      width: '5%',
    },
    {
      title: 'Profile',
      dataIndex: 'profile',
      render: (text) => <img src={text} alt="Profile" className="w-8 h-8 rounded-full" />,
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
      title: 'Action',
      dataIndex: 'action',
      render: (text, record) => (
        <div className="flex gap-2">
          <button className="px-2 py-1 text-white bg-blue-500 rounded hover:bg-blue-600" onClick={() => revokeUser(record.id)}>
            Revoke
          </button>
          <button className="px-2 py-1 text-white bg-red-500 rounded hover:bg-red-600" onClick={() => deleteUser(record.id)}>
            Delete
          </button>
        </div>
      ),
      width: '10%',
    },
  ];

  const fetchData = async () => {
    setLoading(true);
    try {
      const response = await getAdminBan();
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

  const revokeUser = (userId) => {
    console.log(`Revoked user with ID: ${userId}`);
  };

  const deleteUser = (userId) => {
    console.log(`Deleted user with ID: ${userId}`);
  };

  const onSearch = (value) => {
    const filtered = dataTable.filter(item => item.name.toLowerCase().includes(value.toLowerCase()));
    setFilteredData(filtered);
  };

  const onChange = (value) => {
    setPageSize(value);
  };

  return (
    <div>
      <div className="p-5 bg-gray-100 rounded-lg shadow-md">
        <p className="text-lg font-semibold text-gray-800 mb-3 font-extrabold">Banned</p>

        <div className="flex items-center justify-between">
          <div className="flex items-center">
            <span className="mr-2">Show</span>
            <InputNumber
              min={1}
              max={10}
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

export default AdminDisplayBan;
