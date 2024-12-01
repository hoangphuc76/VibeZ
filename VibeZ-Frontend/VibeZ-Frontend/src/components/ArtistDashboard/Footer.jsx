import React from 'react';
import {Link } from 'react-router-dom';
import './HeaderFooter.css';

const Footer = () => {
    return (
        <footer>
        <div className="footer-container">
          <div className="footer-section">
            <h4>Contact Us</h4>
            <p>Email: support@example.com</p>
            <p>Phone: +84 123 456 789</p>
          </div>
          <div className="footer-section">
            <h4>VIBEZ</h4>
            <p>Explore your favorite music anytime, anywhere.</p>
            <p>
                    <a href="https://www.facebook.com" target="_blank" rel="noopener noreferrer">Facebook</a> |
                    <a href="https://www.twitter.com" target="_blank" rel="noopener noreferrer">Twitter</a> |
                    <a href="https://www.instagram.com" target="_blank" rel="noopener noreferrer">Instagram</a>
                </p>
          </div>
        </div>
        <div className="copyright">© 2024 Music App. All Rights Reserved.</div>
      </footer>
    )
}
export default Footer;