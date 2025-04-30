import axios from 'axios';
 
const API_BASE_URL = 'https://localhost:7120';
 
const api = axios.create({
    baseURL: API_BASE_URL,
    headers: {
    'Content-Type': 'application/json'
  }}
);
 
 
  export const loginWithEmailAndPassword = async (email, password) => {
    try {
      const response = await api.get('/api/Auths/checkuserbyemail', {
        params: { email, password }
      });
      return response.data;
    } catch (error) {
      throw error.response?.data || "Sunucu hatası!";
    }
  };
 
  export const getAllTags = async () => {
    try {
      const response = await api.get('/api/tags/getalltagdetails');
      return response.data;
    } catch (error) {
      throw error.response?.data || "Tag'ler yüklenemedi!";
    }
  };
 
  export const turnOnLight = async (tagCode, userID, roomID) => {
    try {
      const response = await api.post('/api/light/turnon', null, {
        params: { tagCode, userID, roomID }
      });
      return response.data;
    } catch (error) {
      throw error.response?.data || "Işık açma işlemi başarısız!";
    }
  };
 
  export const turnOffLight = async (tagCode, userID, roomID) => {
    try {
      const response = await api.post('/api/light/turnoff', null, {
        params: { tagCode, userID, roomID }
      });
      return response.data;
    } catch (error) {
      throw error.response?.data || "Işık kapatma işlemi başarısız!";
    }
  };
  export const getRoomsByLocationName = async (locationName) => {
    try{
        const response = await api.get('/api/Rooms/getroomsbylocationname', {
            params: {locationName}
        });
        return response.data;
    } catch(error) {
        throw error.response?.data || "Odalar getirilirken bir hata oluştu"
    }
  };
  export const getTagsByRoomName = async(roomName) => {
    try{
        const response = await api.get('/api/Tags/gettagsbyroomname',{
        params : {roomName}
    });
    return response.data;
    } catch(error) {
        throw error.response?.data || "Tagler getirilirken bir hata oluştu"
    }
  } 
  export const getAllLocations = async() => {
    try{
        const response = await api.get('/api/Locations/getalllocations');
        return response.data;
    }
    catch(error) {
        throw error.response?.data || "Lokasyonlar getirilirken bir hata oluştu"
    }
  }
  export const getTagsByLocationName = async(locationName) => {
    try{
        const response = await api.get('/api/Tags/gettagsbylocationname',{
        params : {locationName}
    });
    return response.data;
    } catch(error) {
        throw error.response?.data || "Tagler getirilirken bir hata oluştu"
    }
  } 
 
  export default api;
 
 

 
 