import { useEffect, useRef, useState } from 'react';
import { Group, Stack, Text, Image, Progress, Button } from '@mantine/core';
import { Dropzone, IMAGE_MIME_TYPE } from '@mantine/dropzone';
import { createWorker } from 'tesseract.js';
import { useRouter } from 'next/router';
const Home = () => {
  const [imageData, setImageData] = useState<null | string>(null);
  const [progress, setProgress] = useState(0);
  const [progressLabel, setProgressLabel] = useState('idle');
  const [ocrResult, setOcrResult] = useState('');
  const workerRef = useRef<Tesseract.Worker | null>(null);
  const router = useRouter();


  useEffect(() => {

    // Lấy dữ liệu `myData` từ query string và kiểm tra bằng console.log
    // const queryData = Object.fromEntries(new URLSearchParams(window.location.search).entries());
    // console.log("Received myData in Next.js (from query string):", queryData);
    
    workerRef.current = createWorker({
      logger: message => {
        if ('progress' in message) {
          setProgress(message.progress);
          setProgressLabel(message.progress === 1 ? 'Done' : message.status);
        }
      }
    });
    return () => {
      workerRef.current?.terminate();
      workerRef.current = null;
    };
  }, []);

  const loadFile = (file: File) => {
    const reader = new FileReader();
    reader.onloadend = () => {
      const imageDataUri = reader.result;
      setImageData(imageDataUri as string);
    };
    reader.readAsDataURL(file);
  };

  const handleExtract = async () => {
    setProgress(0);
    setProgressLabel('starting');

    const worker = workerRef.current!;
    await worker.load();
    await worker.loadLanguage('eng+vie');
    await worker.initialize('eng+vie');
    
    const response = await worker.recognize(imageData!);
    const ocrText = response.data.text;
    setOcrResult(ocrText);

    // Kiểm tra từ khóa "student hoặc "Student" trong kết quả OCR
    if (ocrText.toLowerCase().includes('student') || 
        ocrText.toLowerCase().includes('sinh viên') ||
        ocrText.toLowerCase().includes('university') || 
        ocrText.toLowerCase().includes('đại học')){
      alert("You are Student");

      //  const myData = { 
      //   packId: '17bacca0-9e7e-43dd-ef0e-08dcfce904f0', 
      //   planName: 'Student', 
      //   planPrice: '29,500₫', 
      //   total: '29500' 
      // };
      // Lưu myData vào localStorage
      // localStorage.setItem('myData', JSON.stringify(myData));

      
      // Điều hướng tới PurchasePage
      window.location.href = 'http://localhost:5173/purchase';
    } else {
      alert("Sorry bro, you are not Student. Can you verify again?");
      window.location.href = 'http://localhost:5173/student-verify';
    }
  };

  return (
    <Group align='initial' style={{ padding: '10px' }}>
      <Stack style={{ flex: '1' }}>
        <Dropzone
          onDrop={(files) => loadFile(files[0])}
          accept={IMAGE_MIME_TYPE}
          multiple={false}
        >
          {() => (
            <Text size="xl" inline>
              Drag image here or click to select file
            </Text>
          )}
        </Dropzone>
        {!!imageData && <Image src={imageData} style={{ width: '100%' }} />}
      </Stack>

      <Stack style={{ flex: '1' }}>
        <Button disabled={!imageData || !workerRef.current} onClick={handleExtract}>Extract</Button>
        <Text>{progressLabel.toUpperCase()}</Text>
        <Progress value={progress * 100} />

        {!!ocrResult && (
          <Stack>
            <Text size='xl'>RESULT</Text>
            <Text style={{ fontFamily: 'monospace', background: 'black', padding: '10px' }}>{ocrResult}</Text>
          </Stack>
        )}
      </Stack>
    </Group>
  );
};

export default Home;
