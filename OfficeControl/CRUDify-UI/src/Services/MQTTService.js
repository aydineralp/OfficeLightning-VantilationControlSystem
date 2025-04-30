import React, { useEffect } from "react";
import mqtt from "mqtt";

const MQTT_BROKER_URL = "ws://192.168.0.108:1884";
const TOPIC = "SiskonSada/DeparkOfisPLC/#";

const MqttService = ({ onUpdate }) => {
  useEffect(() => {
    const client = mqtt.connect(MQTT_BROKER_URL);

    client.on("connect", () => {
      //console.log("MQTT bağlantısı başarılı!");
      client.subscribe(TOPIC, (err) => {
        //if (!err) console.log("Abone olundu!");
      });
    });

    client.on("message", (topic, payload) => {
      try {
        const message = JSON.parse(payload.toString());
        if (message.TagCode && message.Value !== undefined) {
          // Yeni MQTT verisini onUpdate ile yukarıya gönder
          onUpdate((prevData) => ({           
            ...prevData,
            
            [message.TagCode]: message, // TagCode üzerinden veriyi saklıyoruz            
          }));
        }
      } catch (error) {
        console.error("JSON Parse Hatası:", error);
      }
    });

    return () => {
      client.end();
    };
  }, [onUpdate]);

  return null;
};

export default MqttService;
