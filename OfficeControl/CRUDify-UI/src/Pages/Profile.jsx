import React, { useEffect, useState } from 'react';

const Profile = () => {
  const [user, setUser] = useState(null);

  useEffect(() => {
    // localStorage'daki verileri çek
    const userID = localStorage.getItem("userID");
    const name = localStorage.getItem("userFirstName");
    const surname = localStorage.getItem("userLastName");
    const email = localStorage.getItem("userEmail");

    if (userID) {
      setUser({
        userID,
        name,
        surname,
        email
      });
    }
  }, []);

  return (
    <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'center', height: '90vh', flexDirection: 'column' }}>
      <h1>Profil</h1>
      {user ? (
        <div>
          <p><strong>Ad Soyad:</strong> {user.name} {user.surname}</p>
          <p><strong>Email:</strong> {user.email}</p>
        </div>
      ) : (
        <p>Bilgiler yükleniyor...</p>
      )}
    </div>
  );
};

export default Profile;
