import React, { useEffect, useRef } from 'react';
import { Chart, registerables } from 'chart.js';
Chart.register(...registerables);

const ChartComponent = ({ chartData }) => {
    const chartRef = useRef(null);
    const chartInstance = useRef(null); // Dùng để lưu trữ instance của biểu đồ

    useEffect(() => {
        const ctx = chartRef.current.getContext('2d');

        // Kiểm tra và hủy instance cũ trước khi tạo instance mới
        if (chartInstance.current) {
            chartInstance.current.destroy();
        }

        // Tạo một instance mới của Chart và lưu vào chartInstance.current
        chartInstance.current = new Chart(ctx, {
            type: 'line',
            data: {
                labels: chartData.labels,
                datasets: [
                    {
                        label: 'User',
                        data: chartData.men,
                        backgroundColor: 'rgba(75, 192, 192, 0.2)',
                        borderColor: 'rgba(75, 192, 192, 1)',
                    },
                ],
            },
            options: {
                scales: {
                    y: {
                        beginAtZero: true,
                    },
                },
            },
        });

        // Hủy instance khi component bị unmount
        return () => {
            if (chartInstance.current) {
                chartInstance.current.destroy();
            }
        };
    }, [chartData]);

    return (
        <div className="chart">
            <h3>New listeners by month</h3>
            <canvas ref={chartRef}></canvas>
        </div>
    );
};

export default ChartComponent;
