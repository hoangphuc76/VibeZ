// src/components/PremiumFeatures.js
import React from 'react';

function PremiumFeatures() {
  return (
    <div class="premium-features flex gap-10 justify-center mt-[3%]">
      <div className='flex items-center'> 
        <h2 class="text-white text-[25px] font-bold">All Premium plans include</h2>
      </div>
      <ul class="features-list">
        <li className='text-[16px] text-white font-semibold'>Ad-free music listening</li>
        <li className='text-[16px] text-white font-semibold' >Download to listen offline</li>
        <li className='text-[16px] text-white font-semibold' >Play songs in any order</li>
        <li className='text-[16px] text-white font-semibold' >High audio quality</li>
        <li className='text-[16px] text-white font-semibold' >Listen with friends in real time</li>
        <li className='text-[16px] text-white font-semibold' >Organize listening queue</li>
      </ul>
    </div>

  );
}

export default PremiumFeatures;
