import React, { useState, useEffect } from 'react';
import axios from 'axios';


const API_Key = "AK_CS.669518e09a1611ef95d2df3b370b378f.6tJiVjFtrxQvampRGaETywBVnOXYzMANWcr8vFn9vmRKzZ9dkVkOwmymHGtAbPRjL91TLE1D";
const API_Get_Paid = "https://oauth.casso.vn/v2/transactions";

const MY_BANK = {
    BANK_ID: "MB",
    ACCOUNT_NO: "3070110298555",

}
// Hàm tạo URL QR Code
const generateQRCode = (courseID, coursePrice) => {
    const paidContent = `${courseID}`;
    const paidPrice = `${coursePrice}`;
    const QR = `https://img.vietqr.io/image/${MY_BANK.BANK_ID}-${MY_BANK.ACCOUNT_NO}-compact2.png?amount=${paidPrice}&addInfo=${paidContent}`;
    return QR;
};

// Hàm kiểm tra thanh toán
const checkPaid = async (price, content) => {
    try {
        const response = await fetch(
            "https://script.google.com/macros/s/AKfycbytxcFyPpCyX3KH64bomNQw8ZMS59LDftAR5C3wcPmLywNe95lNi9oJU_0bHjksa3qY/exec"
        );
        const data = await response.json();
        console.log("Fetched data:", data);
        // Kiểm tra dữ liệu có tồn tại hay không
        if (data && data.data && data.data.length > 0) {
            const lastPaid = data.data[data.data.length - 1];
            const lastPrice = lastPaid["Giá trị"];
            const lastContent = lastPaid["Mô tả"];
            const lastTimeStr = lastPaid["Ngày diễn ra"];
            const lastTime = new Date(lastTimeStr);
            const currentTime = new Date();

            // Kiểm tra giá trị và nội dung của giao dịch cuối
            if (parseFloat(lastPrice) >= parseFloat(price) &&
                lastContent.includes(content) &&
                (currentTime - lastTime) < 5 * 60 * 1000) // Kiểm tra nếu thời gian giao dịch trong vòng 5 phút
            {
                return true;
            } else {
                console.log("Waiting for the correct payment...");
                return false;
            }
        } else {
            console.log("No payment data available.");
            return false;
        }
    } catch (error) {
        console.error("Error checking payment status:", error.message);
        return false;
    }
};
const createUserPackage = async (packId, userId, total, paymentMethod, typeOfPremium, startDate, endDate) => {
    try {
      // Gọi API với phương thức POST và các tham số cần thiết
      const response = await axios.post(`https://localhost:7241/api/UPackage/create`, null, {
        params: {
          packId,
          userId,
          total,
          paymentMethod,
          typeOfPremium,
          startDate: startDate.toISOString().split('T')[0], // Định dạng DateOnly thành yyyy-MM-dd
          endDate: endDate.toISOString().split('T')[0],
        },
      });
  
      // Kiểm tra và trả về kết quả từ response
      return response.status === 200 ? response.data.message : null;
    } catch (error) {
      console.error("Error creating user package:", error.message || error);
      throw new Error(
        "Failed to create user package: " + 
        (error.response?.data?.message || error.message)
      );
    }
  };
  

export default {
    generateQRCode, checkPaid, createUserPackage
};
