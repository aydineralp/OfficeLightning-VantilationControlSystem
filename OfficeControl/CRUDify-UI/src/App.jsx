import React, { useState, useEffect } from "react";
import {
  BrowserRouter as Router,
  Routes,
  Route,
  Navigate,
  data,
} from "react-router-dom";
import "./App.css";
import Navbar from "./Components/Navbar";
import HomePage from "./Pages/HomePage";
import OfisBati from "./Pages/OfisBati";
import OfisDogu from "./Pages/OfisDogu";
import SosyalAlan from "./Pages/SosyalAlan";
import Profile from "./Pages/Profile";

import LoginRegister from "./Components/LoginRegister";

function App() {
  const [loggedIn, setLoggedIn] = useState(false);

  // Sayfa yüklendiğinde localStorage'dan giriş durumunu kontrol eder
  // useEffect(() => {
  //   const isLoggedIn = localStorage.getItem('isLoggedIn') === 'true';
  //   setLoggedIn(isLoggedIn);
  // }, []);

  // // Çıkış işlemi için fonksiyon
  const handleLogout = () => {
    setLoggedIn(false);
    localStorage.removeItem("isLoggedIn");
  };

  return (
    <>
      {/* // <Router>
    //   {loggedIn && <Navbar setLoggedIn={setLoggedIn} handleLogout={handleLogout} />}
    //   <Routes>
    //     <Route path="/" element={loggedIn ? <Navigate to="/home" /> : <LoginRegister setLoggedIn={setLoggedIn} />} />
    //     <Route path="/home" element={loggedIn ? <Home /> : <Navigate to="/" />} />
    //     <Route path="/ofisDogu" element={loggedIn ? <OfisDogu /> : <Navigate to="/" />} />
    //     <Route path="/ofisBati" element={loggedIn ? <OfisBati /> : <Navigate to="/" />} />
    //     <Route path="/sosyalAlan" element={loggedIn ? <SosyalAlan /> : <Navigate to="/" />} />
    //     <Route path="/profil" element={loggedIn ? <Profil /> : <Navigate to="/" />} />
    //   </Routes>
    // </Router> */}

      <Router>
        <Navbar setLoggedIn={setLoggedIn} handleLogout={handleLogout} />
        <Routes>
          <Route
            path="/"
            element={
              loggedIn ? (
                <Navigate to="/home" />
              ) : (
                <LoginRegister setLoggedIn={setLoggedIn} />
              )
            }
          />
          <Route path="/" element={<HomePage></HomePage>}></Route>
          <Route path="/ofisbati" element={<OfisBati></OfisBati>}></Route>
          <Route path="/ofisdogu" element={<OfisDogu></OfisDogu>}></Route>
          <Route path="/sosyalalan" element={<SosyalAlan></SosyalAlan>}></Route>
          <Route path="/profil" element={<Profile></Profile>}></Route>

        </Routes>
      </Router>
    </>
  );
}

export default App;
