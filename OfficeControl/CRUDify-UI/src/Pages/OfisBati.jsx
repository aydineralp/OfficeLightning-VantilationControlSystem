import React, { useEffect, useState } from "react";
import ofisBatiPlan from "../assets/bgbati.jpg"; // Görseli içe aktar
import Tag from "../Components/TagComponent";
import MqttService from "../Services/MQTTService";
import {
  getRoomsByLocationName,
  getTagsByLocationName,
  turnOffLight,
  turnOnLight,
} from "../services/ApiService";
const OfisBati = () => {
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
    GetRoomsByLocationName("Ofis Bati");
    GetTagsByLocationName("Ofis Bati");
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
        src={ofisBatiPlan}
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
            handleToogleLight={() => handleToogleLight(tag)}
          />
        ))}
      </div>
    </div>
  );
};

export default OfisBati;
