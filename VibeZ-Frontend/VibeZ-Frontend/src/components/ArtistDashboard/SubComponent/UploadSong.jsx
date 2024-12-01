import React from 'react';
import { UploadOutlined } from '@ant-design/icons';
import { Button, message, Upload } from 'antd';

const UploadSong = ({ setImage, setPath, setTrackLRC, setSongInfoImg }) => {
  const imageUploadProps = {
    name: 'image',
    beforeUpload: (file) => {
      console.log('Image file selected:', file);
      if (!file.type.startsWith('image/')) {
        message.error('You can only upload image files!');
        return false;
      }
      setImage(file);
      return false;
    },
  };

  const audioUploadProps = {
    name: 'file',
    beforeUpload: (file) => {
      console.log('Audio file selected:', file);
      setPath(file);
      return false;
    },
  };

  const trackLRCUploadProps = {
    name: 'trackLRC',
    beforeUpload: (file) => {
      console.log('Track LRC file selected:', file);
      setTrackLRC(file);
      return false;
    },
  };

  const songInfoImgUploadProps = {
    name: 'songInfoImg',
    beforeUpload: (file) => {
      console.log('Song Info Image file selected:', file);
      setSongInfoImg(file);
      return false;
    },
  };

  return (
    <div className="upload-songs">
      <h3>Upload Song Image</h3>
      <Upload {...imageUploadProps}>
        <Button icon={<UploadOutlined />} className="upload-button">
          Upload Image
        </Button>
      </Upload>

      <h3>Upload MP3 File</h3>
      <Upload {...audioUploadProps}>
        <Button icon={<UploadOutlined />} className="upload-button">
          Upload MP3
        </Button>
      </Upload>

      <h3>Upload Track LRC</h3>
      <Upload {...trackLRCUploadProps}>
        <Button icon={<UploadOutlined />} className="upload-button">
          Upload LRC
        </Button>
      </Upload>

      <h3>Upload Additional Song Info Image</h3>
      <Upload {...songInfoImgUploadProps}>
        <Button icon={<UploadOutlined />} className="upload-button">
          Upload Info Image
        </Button>
      </Upload>
    </div>
  );
};

export default UploadSong;
