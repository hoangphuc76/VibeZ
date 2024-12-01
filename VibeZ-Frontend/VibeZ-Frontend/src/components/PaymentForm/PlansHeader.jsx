// src/components/PlansHeader.js
import React from 'react';
import { assets } from '../../assets/assets';
function PlansHeader() {
  return (
    <div className="plans-header text-center mt-[5%] text-white flex flex-col gap-2 items-center">
      <h1 className='text-[30px] font-bold'>Affordable plans for any situation</h1>
      <p>Choose a Premium plan and listen to ad-free music without limits on your phone, speaker, and other devices. Pay in various ways. Cancel anytime.</p>
      <div className="w-10 h-10 flex mt-[2%] gap-3 ">
        <img className='rounded' src={assets.vnpay} alt="Vnpay" />
        <img className='rounded' src={assets.momo} alt="Momo" />
        <img className='rounded' src={assets.VietQR} alt="VietQR" />
      </div>
    </div>
  );
}

export default PlansHeader;
