import React from "react";

const Tag = ({ tagCode, handleToogleLight, isActive, style }) => {
  return (
    <div style={{ height: "60px", width: "100px" }}>
      <button
        type="submit"
        style={{ ...style, backgroundColor: isActive ? "green" : "red" }}
        onClick={(e) => {
          e.preventDefault(); // Sayfa yenilemesini engelle
          handleToogleLight(); // handleToogleLight fonksiyonunu burada çağır
        }}
      >
        {tagCode}
      </button>
    </div>
  );
};
export default Tag;
