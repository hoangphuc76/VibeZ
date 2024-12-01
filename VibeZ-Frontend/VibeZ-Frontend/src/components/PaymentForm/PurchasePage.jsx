import React, { useState, useEffect, useContext, useRef } from 'react';
import Navbar2 from '../Navbar2';
import { assets } from '../../assets/assets';
import PaymentService from '../../services/PaymentService';
import authService from '../../services/authService';
import { useLocation, useNavigate } from 'react-router-dom';
import { PlayerContext } from '../../context/PlayerContext';

const getCurrentDate = () => {
  const options = { year: 'numeric', month: 'long', day: 'numeric' };
  return new Date().toLocaleDateString('en-US', options);
};

const getCurrentDateString = () => {
  return new Date();
};
const getNextMonthDateString = () => {
  const currentDate = new Date();
  const nextMonthDate = new Date(currentDate);
  nextMonthDate.setMonth(currentDate.getMonth() + 1);

  if (nextMonthDate.getDate() !== currentDate.getDate()) {
    nextMonthDate.setDate(0);
  }

  return nextMonthDate;
};

const getNextMonthDate = () => {
  const currentDate = new Date();
  const nextMonthDate = new Date(currentDate);
  nextMonthDate.setMonth(currentDate.getMonth() + 1);

  if (nextMonthDate.getDate() !== currentDate.getDate()) {
    nextMonthDate.setDate(0);
  }

  return nextMonthDate.toLocaleDateString('en-US', { year: 'numeric', month: 'long', day: 'numeric' });
};

const generateRandomName = (length = 10) => {
  const characters = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
  let result = '';
  for (let i = 0; i < length; i++) {
    result += characters.charAt(Math.floor(Math.random() * characters.length));
  }
  return result;
};

const PurchasePage = () => {
  const [qrCodeUrl, setQrCodeUrl] = useState('');
  const [timer, setTimer] = useState(300);
  const [isCheckingPayment, setIsCheckingPayment] = useState(false);
  // const location = useLocation();
  // const myData = location.state || {};
  // const price = myData.planPrice?.split(" ")[0] || '';
  const [myData, setMyData] = useState({});
  const navigate = useNavigate();
  const [method, setMethod] = useState('');
  const { isLoading, setLoading } = useContext(PlayerContext);
  const intervalRef = useRef(null);
  const [randomName, setRandomName] = useState(generateRandomName());
  useEffect(() => {
    // Lấy `myData` từ `localStorage`
    const storedData = localStorage.getItem('myData');
    if (storedData) {
      setMyData(JSON.parse(storedData));
    }
  }, []);

  const price = myData.planPrice?.split(" ")[0] || '';
  console.log("Received myData in StudentVerify:", myData);
  // Hàm thực hiện thanh toán
  const pay = async () => {
    try {
      const userId = JSON.parse(localStorage.getItem('userId'));
      setLoading(true);
      const response1 = await authService.updatePremiumStatus(userId , 'Individual');
      const response2 = await PaymentService.createUserPackage(
        myData.packId,
        userId,
        myData.total,
        method,
        myData.planName,
        getCurrentDateString(),
        getNextMonthDateString()
      );

      if (response1 && response2) {
        setLoading(false);
      }
    } catch (error) {
      setLoading(false);
      console.error(error.message);
    }
  };

  // Hàm kiểm tra thanh toán
  const checkPaymentStatus = async () => {
    if (isCheckingPayment) return; // Kiểm soát chỉ gọi một lần
    setIsCheckingPayment(true);
    const response = await PaymentService.checkPaid(myData.total, randomName);
    if (response) {
      clearInterval(intervalRef.current); // Dừng interval khi thanh toán thành công
      alert("Paid successfully");
      await pay();
      navigate('/');
    }
    setIsCheckingPayment(false);
  };

  // Tạo QR code và đặt bộ đếm thời gian
  const handlePaymentSelection = (selectedMethod) => {
    alert(`Bạn đã chọn phương thức thanh toán: ${selectedMethod}`);
    setMethod(selectedMethod);

    if (selectedMethod === 'VietQR') {
      const qrUrl = PaymentService.generateQRCode(randomName, myData.total);
      setQrCodeUrl(qrUrl);
      setTimer(300); // Đặt lại bộ đếm thời gian
    } else {
      setQrCodeUrl('');
    }
  };

  // Đếm ngược thời gian
  useEffect(() => {
    if (qrCodeUrl) {
      intervalRef.current = setInterval(() => {
        setTimer((prevTimer) => {
          if (prevTimer <= 1) {
            clearInterval(intervalRef.current);
            setQrCodeUrl('');
            return 0;
          }
          return prevTimer - 1;
        });
      }, 1000);
    }

    return () => clearInterval(intervalRef.current);
  }, [qrCodeUrl]);

  // Kiểm tra thanh toán mỗi khi timer giảm
  useEffect(() => {
    if (qrCodeUrl && timer > 0) {
      checkPaymentStatus();
    }
  }, [timer]); // Kiểm tra mỗi lần timer thay đổi

  const formatTime = (seconds) => {
    const minutes = Math.floor(seconds / 60);
    const remainingSeconds = seconds % 60;
    return `${String(minutes).padStart(2, '0')}:${String(remainingSeconds).padStart(2, '0')}`;
  };

  return (
    <div className='overflow-scroll'>
      <div className='bg-black '>
        <Navbar2 />
      </div>
      <div className=" py-5">
        <div className="max-w-xl mx-auto px-4">
          {/* Header section */}
          <div className="flex justify-between items-center text-white mb-4">
            <h1 className="text-2xl font-bold">Your plan</h1>
            <button className="text-gray-400 hover:text-green-500 underline" onClick={() => navigate('/plan')}>
              Change plan
            </button>
          </div>

          {/* Main card */}
          <div className="bg-white rounded-3xl overflow-hidden shadow-lg">
            <div className="bg-pink-100 p-6">
              <div className="flex justify-between items-start">
                <div>
                  <h2 className="text-black text-xl font-bold mb-1">Premium {myData.planName}</h2>
                  <p className="text-gray-700">1 Premium account</p>
                </div>
                <div className="text-right">
                  <div className="text-black text-2xl font-bold">2 months</div>
                  <div className="text-gray-700">For {price}</div>
                </div>
              </div>
            </div>

            <div className="p-6">
              <div className="space-y-3 mb-6">
                <div className="flex justify-between items-center">
                  <span className="font-bold">Total</span>
                  <span>{myData.planPrice}</span>
                </div>
                <div className="flex justify-between items-center">
                  <span>Starting {getCurrentDate()}</span>
                  <span>{price} / month</span>
                </div>
              </div>
              <ul className="list-disc pl-5 space-y-2 text-sm">
                <li className='text-black'>Normal price is {price} / month</li>
                <li className='text-black'>You will next be billed on {getNextMonthDate()}</li>
                <li className="flex items-center gap-1 text-black">
                  <span>Cancel anytime.</span>
                  <a href="#" className="text-blue-600 hover:underline">Offer terms</a>
                  <span>apply.</span>
                </li>
              </ul>
            </div>
          </div>

          <div className="bg-white rounded-3xl mt-4 shadow-lg p-6">
            <div className="space-y-4">
              <label className="flex items-center p-4 border rounded-xl cursor-pointer hover:bg-gray-50">
                <input
                  type="radio"
                  name="payment"
                  className="mr-3 w-4 h-4"
                  onClick={() => handlePaymentSelection('MoMo wallet')}
                />
                <span className="flex-1">MoMo wallet</span>
                <img src={assets.momo} alt="MoMo" className="h-8" />
              </label>

              <label className="flex items-center p-4 border rounded-xl cursor-pointer hover:bg-gray-50">
                <input
                  type="radio"
                  name="payment"
                  className="mr-3 w-4 h-4"
                  onClick={() => handlePaymentSelection('VietQR')}
                />
                <span className="flex-1">VietQR</span>
                <img src={assets.VietQR} alt="VietQR" className="w-10 h-10" />
              </label>
            </div>
          </div>

          {qrCodeUrl && (
            <div className="mt-6 p-4 bg-white rounded-lg shadow-lg max-w-sm mx-auto text-center">
              <h2 className="text-xl font-bold text-gray-800 mb-2">QR Code for Payment</h2>
              <p className="text-gray-500 text-sm mb-4">Scan the QR code below to complete your payment</p>
              <div className="flex justify-center mb-4">
                <img src={qrCodeUrl} alt="QR Code for VietQR payment" className="max-h-100 max-w-100" />
              </div>
              <div className="text-gray-700">
                <p className="font-semibold">Time Remaining:</p>
                <p className="text-red-500 font-bold">{formatTime(timer)}</p>
              </div>
            </div>
          )}
        </div>
      </div>
    </div>
  );
};

export default PurchasePage;
