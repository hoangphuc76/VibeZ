
import React, { useEffect } from 'react';

function QRCode({ bankID, accountNo, courses }) {
  useEffect(() => {
    const btns = document.querySelectorAll(".btn-plan");
    const paidContent = document.getElementById("paid_content");
    const paidPrice = document.getElementById("paid_price");
    const courseQrImg = document.querySelector(".course_qr_img");

    btns.forEach((item, index) => {
      item.addEventListener("click", () => {
        const QR = `https://img.vietqr.io/image/${bankID}-${accountNo}-compact2.png?amount=${courses[index].price}&addInfo=${courses[index].coursesID}1`;
        courseQrImg.src = QR;
        paidContent.innerHTML = `${courses[index].coursesID}1`;
        paidPrice.innerHTML = `${courses[index].paidPrice}`;
      });
    });
  }, [bankID, accountNo, courses]);

  return (
    <div className="qr-code">
      <img className="course_qr_img" src="./assets/qr-code.png" alt="QR Code" />
      <p>Nội dung chuyển khoản: <span id="paid_content">NDCK</span></p>
      <p>Số tiền: <span id="paid_price"></span></p>
    </div>
  );
}

export default QRCode;
