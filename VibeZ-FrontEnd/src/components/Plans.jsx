import React from 'react'
import './PlansComponent.css'
import 'bootstrap/dist/css/bootstrap.min.css';
import Navbar from './Navbar2';
import { assets } from '../assets/assets';

const Plans = () => {
    return (
        <>
            <div className="con bg-black text-white font-sans">
                <Navbar/>
                <div className="container-fluid body">
                    <div className="container mt-5 header">
                        {/* Header */}
                        <div className="plans-header text-center">
                            <h1 className='text-[40px] mb-2'>Affordable plans for any situation</h1>
                            <p className='text-[18px]'>Choose a Premium plan and listen to ad-free music without limits on your phone, speaker, and other
                                devices. Pay in various ways. Cancel anytime.</p>
                            <div className="payment-methods mt-[2%] flex justify-center">
                                <img src={assets.vnpay} alt="Vnpay" />
                                <img src={assets.momo} alt="Momo" />
                                {/* Add more payment logos as needed */}
                            </div>
                        </div>

                        {/* Premium Features */}
                        <div className="row premium-features text-center mt-[5%]">
                            <h2 className="col-md-7 text-[25px] mt-[3%]">All Premium plans include</h2>
                            <ul className="col-md-5 list-unstyled features-list text-start">
                                <li className='text-[16px]'>Ad-free music listening</li>
                                <li className='text-[16px]'>Download to listen offline</li>
                                <li className='text-[16px]'>Play songs in any order</li>
                                <li className='text-[16px]'>High audio quality</li>
                                <li className='text-[16px]'>Listen with friends in real time</li>
                                <li className='text-[16px]'>Organize listening queue</li>
                            </ul>
                        </div>

                        {/* Plans Section */}
                        <div className="row plans-section mt-[5%]">
                            {/* Mini Plan */}
                            <div className="col-lg-4 col-md-4 col-sm-12 ">
                                <div className="plan-card bg-[#242424] p-[15px]">
                                    <div className=" plan mini-plan">
                                        <h3>Mini</h3>
                                        <p>₫10,500 for 1 week</p>
                                        <hr />
                                        <ul className="list-unstyled">
                                            <li>1 mobile-only Premium account</li>
                                            <li>Offline listening of up to 30 songs on 1 device</li>
                                            <li>One-time payment</li>
                                            <li>Basic audio quality</li>
                                        </ul>
                                    </div>
                                    <button className="btn btn-plan btn-mini">Get Premium Mini</button>
                                    <button className="btn btn-secondary btn-plan">One-time payment</button>
                                </div>
                            </div>

                            {/* Individual Plan */}
                            <div className="col-lg-4 col-md-4 col-sm-12 ">
                                <div className="plan-card bg-[#242424] p-[15px]">
                                    <div className=" plan individual-plan">
                                        <h3>Individual</h3>
                                        <p>₫59,000 for 2 months</p>
                                        <hr />
                                        <ul className="list-unstyled">
                                            <li>1 Premium account</li>
                                            <li>Cancel anytime</li>
                                            <li>Subscribe or one-time payment</li>
                                        </ul>
                                    </div>
                                    <button className="btn btn-plan btn-individual">Get Premium Mini</button>
                                    <button className="btn btn-secondary btn-plan">One-time payment</button>
                                </div>
                            </div>

                            {/* Student Plan */}
                            <div className="col-lg-4 col-md-4 col-sm-12 ">
                                <div className="plan-card bg-[#242424] p-[15px]">
                                    <div className=" plan student-plan">
                                        <h3>Student</h3>
                                        <p>₫29,500 for 2 months</p>
                                        <hr />
                                        <ul className="list-unstyled">
                                            <li>1 verified Premium account</li>
                                            <li>Discount for eligible students</li>
                                            <li>Cancel anytime</li>
                                            <li>Subscribe or one-time payment</li>
                                        </ul>
                                    </div>
                                    <button className="btn btn-plan btn-student">Get Premium Student</button>
                                    <button className="btn btn-secondary btn-plan">One-time payment</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </>
    )
}

export default Plans
