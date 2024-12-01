import React from 'react';
import PlansHeader from './PlansHeader';
import PremiumFeatures from './PremiumFeatures';
import PlanCard from './PlanCard';
import QRCode from './QRCode';
import Navbar from '../../components/Navbar2';

function PremiumPage() {
  // Sample data for each plan card
  const plans = [

    {
      planName: "Individual",
      planPrice: "₫59,000 for 2 months",
      description: "₫59,000 / month after",
      features: [
        "1 Premium account",
        "Cancel anytime",
        "Subscribe or one-time payment",
      ],
      buttonText: "Get Premium Individual",
    },
    {
      planName: "Student",
      planPrice: "₫29,500 / month",
      description: "",
      features: [
        "1 verified Premium account",
        "Discount for eligible students",
        "Cancel anytime",
        "Subscribe or one-time payment",
      ],
      buttonText: "Get Premium Student",
    },
  ];

  // Sample QR code data
  const qrData = {
    BANK_ID: "MB",
    ACCOUNT_NO: "0983028278",
    courses: [
      { coursesID: "MiniPlan", price: 10500, paidPrice: "₫10,500" },
      { coursesID: "IndividualPlan", price: 59000, paidPrice: "₫59,000" },
      { coursesID: "StudentPlan", price: 29500, paidPrice: "₫29,500" },
    ],
  };

  return (
    <div className="bg-[#121212] min-h-screen w-full overflow-auto">
      <Navbar />
      <PlansHeader />
      <PremiumFeatures />
      
      <div className="grid grid-cols-1 sm:grid-cols-1 md:grid-cols-2 lg:grid-cols-2 gap-4 w-full max-w-[900px] px-4 mx-auto mt-20">
        {plans.map((plan, index) => (
          <PlanCard
            key={index}
            planName={plan.planName}
            planPrice={plan.planPrice}
            description={plan.description}
            features={plan.features}
            buttonText={plan.buttonText}
          />
        ))}
      </div>
      
      <div className="mt-10">
        {/* Uncomment to enable QRCode component */}
        {/* <QRCode bankID={qrData.BANK_ID} accountNo={qrData.ACCOUNT_NO} courses={qrData.courses} /> */}
      </div>
    </div>
  );
}

export default PremiumPage;
