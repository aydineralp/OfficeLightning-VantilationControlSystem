import React, { useState } from "react";
import { useNavigate } from "react-router-dom";import { FaUser, FaLock, FaEnvelope } from "react-icons/fa";
import { loginWithEmailAndPassword } from "../services/ApiService";
import "./LoginRegister.css";

const LoginRegister = () => {
  const [action, setAction] = useState("");
 
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [message, setMessage] = useState(null);
  const [isSuccess, setIsSuccess] = useState(false);
  const [currentUser,setCurrentUser] = useState({});
  const navigate = useNavigate();

  async function Login(email, password) {
    try {
      const responseResult = await loginWithEmailAndPassword(email, password);
      
      if (responseResult.success) {
        // Kullanıcı bilgilerini localStorage'a kaydet
        localStorage.setItem("userID", responseResult._user.userId);
        localStorage.setItem("userFirstName", responseResult._user.name);
        localStorage.setItem("userLastName", responseResult._user.surName);
        localStorage.setItem("userEmail", responseResult._user.email);

        // Başarıyla giriş yaptıktan sonra profile yönlendir
        navigate("/profil");
      } else {
        setMessage(responseResult.message);
      }
    } catch (error) {
      setMessage("Giriş sırasında bir hata oluştu!");
    }
  }

  return (
    <div>
      <div className="form-box login">
        <form onSubmit={(e) => { e.preventDefault(); Login(email, password); }}>
          <h1>Login</h1>
          <div className="input-box">
            <input
              type="email"
              placeholder="Email"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              required
            />
            <FaUser className="icon" />
          </div>
          <div className="input-box">
            <input
              type="password"
              placeholder="Password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              required
            />
            <FaLock className="icon" />
          </div>
          <button type="submit">Login</button>

          {message && <p style={{ color: "red", fontWeight: "bold" }}>{message}</p>}
        </form>
      </div>
    </div>
  );
};

export default LoginRegister;
