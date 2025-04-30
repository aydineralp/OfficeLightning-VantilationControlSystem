import React from "react";
import ofisDoguPlan from "../assets/bgdogu.jpg"; // Görseli içe aktar
import { useState, useEffect } from "react";
import Tag from "../Components/TagComponent";
import MqttService from "../Services/MQTTService";
import {
  getRoomsByLocationName,
  getTagsByLocationName,
  turnOffLight,
  turnOnLight,
} from "../services/ApiService";

const OfisDogu = () => {
  const [rooms, setRooms] = useState([]);
  const [tags, setTags] = useState([]);
  const [mqttTagData, setMqttTagData] = useState({});
  useEffect(() => {
    async function GetRoomsByLocationName(locationName) {
      try {
        const responseData = await getRoomsByLocationName(locationName);
        setRooms(responseData.data);
        console.log(responseData);
      } catch (error) {
        console.error("Odalar yüklenemedi");
      }
    }
    async function GetTagsByLocationName(locationName) {
      try {
        const responseData = await getTagsByLocationName(locationName);
        setTags(responseData.data.result);
        console.log(responseData.data.result);
      } catch (error) {
        console.error("Tagler Yüklenmedi");
      }
    }
    GetRoomsByLocationName("Ofis Dogu");
    GetTagsByLocationName("Ofis Dogu");
  }, []);

  async function handleToogleLight(tag) {
    if (tag.isActive) {
      await turnOffLight(
        tag.tagCode,
        localStorage.getItem("userID"),
        tag.roomId
      );
    } else if (!tag.isActive) {
      await turnOnLight(
        tag.tagCode,
        localStorage.getItem("userID"),
        tag.roomId
      );
    }
  }
  const mergedTags = tags.map((tag) => ({
    ...tag,
    isActive: Boolean(mqttTagData[tag.tagCode]?.Value), // MQTT verisinde varsa, aktif olup olmadığını belirle
  }));
  return (
    <div>
      <MqttService onUpdate={setMqttTagData} />
      <img
        src={ofisDoguPlan}
        alt="Ofis Batı Planı"
        style={{
          zIndex: -1,
          maxWidth: "100%",
          height: "auto",
          marginTop: "20px",
          position: "absolute",
        }}
      />

      <div>
        {mergedTags.map((tag, index) => (
          <Tag
            key={index}
            tagCode={tag.tagCode}
            isActive={tag.isActive}
            handleToogleLight={() => handleToogleLight(tag)} // Fonksiyonu burada çağırmıyoruz, sadece referansını veriyoruz
          />
        ))}
      </div>
    </div>
  );
};

export default OfisDogu;
