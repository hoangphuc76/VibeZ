import React, { useState, useEffect } from 'react';
import { Button, Form, Input, DatePicker, message } from 'antd';
import { useNavigate } from 'react-router-dom';
import UploadSong from './SubComponent/UploadSong';
import albumService from '../../services/albumService';
import ArtistDashboardService from '../../services/artistDashboardService';
import ImageUpload from './SubComponent/ImageUpload';

const CreateAlbum = () => {
    const [name, setName] = useState('');
    const [nation, setNation] = useState('');
    const [releaseDate, setReleaseDate] = useState(null); 
    const [image, setImage] = useState(null);
    const [artist, setArtist] = useState();

    const navigate = useNavigate();

    useEffect(() => {
        const userId = JSON.parse(localStorage.getItem('userId'));
        const getArtist = async () => {
            try {
                const artistData = await ArtistDashboardService.getArtistByUserId(userId);
                setArtist(artistData);
            } catch (error) {
                console.error('Error fetching artist:', error);
            }
        };
        getArtist();
    }, []);

    const handleSubmit = async () => {
        try {
            if (!releaseDate) {
                throw new Error('Release date is required.');
            }
    
            const yy = releaseDate.year();
            const mm = (releaseDate.month() + 1).toString().padStart(2, '0');
            const dd = releaseDate.date().toString().padStart(2, '0');
    
            const formData = new FormData();
            formData.append('artistId', artist.id);
            formData.append('name', name);
            formData.append('yy', yy);
            formData.append('mm', mm);
            formData.append('dd', dd);
            formData.append('image', image);
            formData.append('nation', nation);
    
            await albumService.createAlbum(formData);
    
            message.success('Album created successfully!');
            resetForm();
            navigate('/artistdashboard/music');
        } catch (error) {
            message.error(`Failed to create album: ${error.message}`);
        }
    };

    const resetForm = () => {
        setName('');
        setNation('');
        setReleaseDate(null);
        setImage(null);
    };

    return (
        <div className="flex items-center justify-center min-h-screen bg-gray-100">
            <div className="w-full max-w-lg bg-white rounded-lg shadow-lg p-8">
                <h1 className="text-2xl font-semibold text-center text-gray-800 mb-6">Create Album</h1>
                <Form layout="vertical" onFinish={handleSubmit}>
                    <Form.Item label="Album Title" required>
                        <Input
                            placeholder="Enter album title"
                            value={name}
                            onChange={(e) => setName(e.target.value)}
                            className="border border-gray-300 rounded-lg px-4 py-2"
                        />
                    </Form.Item>
                    <Form.Item label="Release Date" required>
                        <DatePicker 
                            style={{ width: '100%' }}
                            onChange={(date) => setReleaseDate(date)} 
                            placeholder="Select release date"
                            className="w-full border border-gray-300 rounded-lg px-4 py-2"
                        />
                    </Form.Item>
                    <ImageUpload setImage={setImage} />
                    <Form.Item label="Nation" required>
                        <Input
                            placeholder="Enter nation"
                            value={nation}
                            onChange={(e) => setNation(e.target.value)}
                            className="border border-gray-300 rounded-lg px-4 py-2"
                        />
                    </Form.Item>
                    <Form.Item>
                        <Button type="primary" htmlType="submit" className="w-full bg-blue-500 hover:bg-blue-600 text-white rounded-lg px-4 py-2 mt-4">
                            Submit
                        </Button>
                    </Form.Item>
                </Form>
            </div>
        </div>
    );
};

export default CreateAlbum;
