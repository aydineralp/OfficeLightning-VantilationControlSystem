import React, { useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import './Navbar.css';
import logo from '../assets/crudify.logo.png';
import { FaBars, FaTimes } from 'react-icons/fa';
 
const Navbar = ({ handleLogout }) => {
  const navigate = useNavigate();
  const [menuOpen, setMenuOpen] = useState(false);
 
  const toggleMenu = () => {
    setMenuOpen(!menuOpen);
  };
 
  const handleLogoutClick = () => {
    handleLogout();
    navigate('/');
  };
 
  return (
    <nav className="navbar">
      <div className="navbar-container">
        <Link to="/home" className="navbar-logo">
          <img src={logo} alt="Logo" className="navbar-image" />
        </Link>
        <div className="menu-icon" onClick={toggleMenu}>
          {menuOpen ? <FaTimes /> : <FaBars />}
        </div>
        <ul className={menuOpen ? "nav-menu active" : "nav-menu"}>
          <li className="nav-item">
            <Link to="/home" className="nav-links" onClick={toggleMenu}>
              Ana Sayfa
            </Link>
          </li>
          <li className="nav-item">
            <Link to="/ofisDogu" className="nav-links" onClick={toggleMenu}>
              Ofis Doğu
            </Link>
          </li>
          <li className="nav-item">
            <Link to="/ofisBati" className="nav-links" onClick={toggleMenu}>
              Ofis Batı
            </Link>
          </li>
          <li className="nav-item">
            <Link to="/sosyalAlan" className="nav-links" onClick={toggleMenu}>
              Sosyal Alan
            </Link>
          </li>
          <li className="nav-item">
            <Link to="/profil" className="nav-links" onClick={toggleMenu}>
              Profil
            </Link>
          </li>
          <li className="nav-item logout-item">
            <button className="logout-button" onClick={handleLogoutClick}>Çıkış Yap</button>
          </li>
        </ul>
      </div>
    </nav>
  );
};
 
export default Navbar;