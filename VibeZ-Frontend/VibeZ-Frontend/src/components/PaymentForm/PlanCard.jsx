import React from 'react';
import { useNavigate } from 'react-router-dom';

function PlanCard({ planName, planPrice, description, features, buttonText }) {
  const navigate = useNavigate();
  const handleNavigate = (packId, name, price, total) => {
    const myData = { packId: packId, planName: name ,planPrice: price, total: total };
    
    // Lưu `myData` vào `localStorage`
    localStorage.setItem('myData', JSON.stringify(myData));
    
    
    if (name === 'Student') {
      navigate('/student-verify');
    } else {
      navigate('/purchase');
    }
  }

  return (
    <div className="rounded-lg bg-[#242424] p-6 h-[550px] flex flex-col justify-between ">
      <div className="flex flex-wrap flex-col">

        {planName === 'Individual' && (
          <>
            <h3 className='text-[24px] md:text-[28px] lg:text-[30px] font-bold mb-3 text-[#FFD2D7]'>{planName}</h3>
            <p className="text-[12px] md:text-[14px] text-white font-bold">{planPrice}</p>
            {description && <p className="text-[10px] md:text-[12px] text-gray-400 font-bold">{description}</p>}
            <hr className="mt-5 text-white" />
            <ul className="mt-5 px-3 list-disc list-inside">
              {features.map((feature, index) => (
                <li key={index} className="text-[12px] text-white md:text-[14px]">{feature}</li>
              ))}
            </ul>
            <div className="flex justify-center">
              <button className="text-black font-bold bg-[#FFD2D7] rounded-full px-3 py-3 mt-5 text-[12px] md:text-[18px] w-full hover:scale-110 transition-transform"
                onClick={() => handleNavigate('f696c9b8-34fa-469a-ef0d-08dcfce904f0' ,planName ,planPrice, 59000)}
              >
                {buttonText}
              </button>
            </div>
            <p className="text-gray-400 mt-5 text-[12px] md:text-[14px]">59,000 ₫ for 2 months, then 59,000 ₫/month. Offer only applies if you have never used a Premium plan before.</p>
          </>
        )}

        {planName === 'Student' && (
          <>
            <h3 className='text-[24px] md:text-[28px] lg:text-[30px] font-bold mb-3 text-[#C4B1D4]'>{planName}</h3>
            <p className="text-[12px] md:text-[14px] text-white font-bold">{planPrice}</p>
            {description && <p className="text-[10px] md:text-[12px] text-gray-400">{description}</p>}
            <hr className="mt-5 text-white" />
            <ul className="mt-5 px-3 list-disc list-inside">
              {features.map((feature, index) => (
                <li key={index} className="text-[12px] text-white md:text-[14px]">{feature}</li>
              ))}
            </ul>
            <div className="flex justify-center">
              <button className="text-black font-bold bg-[#C4B1D4] rounded-full px-3 py-3 mt-5 text-[12px] md:text-[18px] w-full hover:scale-110 transition-transform"
                onClick={() => handleNavigate('17bacca0-9e7e-43dd-ef0e-08dcfce904f0' ,planName ,planPrice, 29500)}
              >
                {buttonText}
              </button>
            </div>
            <p className="text-gray-400 mt-5 text-[12px] md:text-[14px]">29,500 ₫ for 2 months, then 29,500 ₫/month. Offer is only available to students at accredited universities and colleges and applies only if you have never used a Premium plan before.</p>
          </>
        )}
      </div>
    </div>
  );
}

export default PlanCard;
