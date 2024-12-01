import React, { useState, useEffect } from 'react';
import { Table, Spin, Input, message } from 'antd';
import { Bar } from 'react-chartjs-2';
import axios from 'axios';
import {
  Chart as ChartJS,
  CategoryScale,
  LinearScale,
  BarElement,
  Title,
  Tooltip,
  Legend,
} from 'chart.js';

ChartJS.register(CategoryScale, LinearScale, BarElement, Title, Tooltip, Legend);

function AdminStatistics() {
  const [loading, setLoading] = useState(false);
  const [searchText, setSearchText] = useState('');
  const [dataTable, setDataTable] = useState([]);
  const [filteredData, setFilteredData] = useState([]);
  const [chartData, setChartData] = useState(null);
  const [dataTotal, setDataTotal] = useState({
    mini: 0,
    standard: 0,
    student: 0,
  });

  const columns = [
    {
      title: 'No',
      dataIndex: '',
      render: (text, record, index) => index + 1,
    },
    {
      title: 'User Name',
      dataIndex: 'name',
      key: 'name',
      width: '30%',
    },
    {
      title: 'Email',
      dataIndex: 'email',
      key: 'email',
    },
    {
      title: 'Total Amount (USD)',
      dataIndex: 'amount',
      key: 'amount',
      sorter: (a, b) => a.amount - b.amount,
    },
    {
      title: 'Type',
      dataIndex: 'typeOfPremium',
      key: 'typeOfPremium',
      filters: [
        { text: 'Mini Premium', value: 'Mini' },
        { text: 'Standard Premium', value: 'Standard' },
        { text: 'Student Premium', value: 'Student' },
      ],
      onFilter: (value, record) => record.typeOfPremium === value,
    },
  ];

  const fetchData = async () => {
    setLoading(true);
    try {
      const response = await axios.get('https://localhost:7241/api/Admin/admin-statistics'); 
      const { mini, standard, student, table } = response.data;
      
      setDataTotal({ mini, standard, student });
      setDataTable(table);
      setFilteredData(table);
    } catch (error) {
      message.error('Failed to load data');
      console.error(error);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchData();
  }, []);

  useEffect(() => {
    setFilteredData(
      dataTable.filter((item) =>
        item.name.toLowerCase().includes(searchText.toLowerCase())
      )
    );
  }, [searchText, dataTable]);

  useEffect(() => {
    setChartData({
      labels: ['Mini Premium', 'Standard Premium', 'Student Premium'],
      datasets: [
        {
          label: 'Total Amount (VND)',
          data: [dataTotal.mini, dataTotal.standard, dataTotal.student],
          backgroundColor: ['#4bc0c0', '#36a2eb', '#9966ff'],
        },
      ],
    });
  }, [dataTotal]);

  if (loading) {
    return (
      <div className="flex justify-center items-center h-screen">
        <Spin size="large" />
      </div>
    );
  }

  return (
    <>
      <div className="flex justify-around p-4">
        <div className="bg-gray-800 text-white p-4 flex flex-col items-center justify-center w-1/4 h-32 rounded-lg shadow-lg mx-2">
          <h3 className="text-2xl font-bold">${dataTotal.mini || '-'} VND</h3>
          <p className="text-sm">Mini Premium Amount</p>
        </div>
        <div className="bg-gray-800 text-white p-4 flex flex-col items-center justify-center w-1/4 h-32 rounded-lg shadow-lg mx-2">
          <h3 className="text-2xl font-bold">{dataTotal.standard || '-'} VND</h3>
          <p className="text-sm">Standard Premium Amount</p>
        </div>
        <div className="bg-gray-800 text-white p-4 flex flex-col items-center justify-center w-1/4 h-32 rounded-lg shadow-lg mx-2">
          <h3 className="text-2xl font-bold">${dataTotal.student || '-'} VND</h3>
          <p className="text-sm">Student Premium Amount</p>
        </div>
      </div>

      <div className="p-4">
        <div className="flex justify-between">
          <div className="w-1/2 pr-2">
            <Input
              placeholder="Search by username"
              value={searchText}
              onChange={(e) => setSearchText(e.target.value)}
              className="mb-4"
            />
            <Table
              columns={columns}
              dataSource={filteredData}
              rowKey={(record) => record.id}
              pagination={{ pageSize: 5 }}
            />
          </div>
          <div className="w-1/2 pl-2 flex justify-center items-center">
            {chartData && (
              <Bar
                data={chartData}
                options={{
                  responsive: true,
                  plugins: {
                    legend: { position: 'top' },
                    title: { display: true, text: 'Revenue by User Type' },
                  },
                }}
              />
            )}
          </div>
        </div>
      </div>
    </>
  );
}

export default AdminStatistics;