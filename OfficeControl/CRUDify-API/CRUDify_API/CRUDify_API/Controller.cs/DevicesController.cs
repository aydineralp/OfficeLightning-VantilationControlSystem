using Microsoft.AspNetCore.Mvc;
using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using InstrumentDataCollectorServer;
using System;
using System.Threading.Tasks;
using CRUDify_API.Repositories.Abstract;
using CRUDify_API.Entities;

namespace CRUDify_API.Controller.cs
{
    [ApiController]
    [Route("api/light")]
    public class DevicesController : ControllerBase
    {
        private readonly DataCollector.DataCollectorClient _grpcClient;
        private readonly IUserRoomRepository _userRoomRepository;
        private readonly ILogRepository _logRepository;

        public DevicesController(IUserRoomRepository userRoomRepository, ILogRepository logRepository)
        {
            _userRoomRepository = userRoomRepository;
            var channel = GrpcChannel.ForAddress("http://192.168.0.21:5295");
            _grpcClient = new DataCollector.DataCollectorClient(channel);
            _logRepository = logRepository;
        }


        /// Işığı açma/kapatma

        [HttpPost("turnon")]
        public async Task<IActionResult> TurnOnLight(string tagCode, Guid userID,Guid roomID)
        {
            try
            {
                var grpcRequest = new WriteValue
                {
                    InstrumentCode = "DeparkOfisPLC",
                    TagCode = tagCode,
                    Value = new Value { BoolValue = true }
                };
                var userAccessResult = _userRoomRepository.CheckUserRoomByCompositPK(userID, roomID);
                if (userAccessResult)
                {
                    try
                    {
                        await _grpcClient.WriteTagValueAsync(grpcRequest);
                        await _logRepository.LogUserActivity(userID, "Turn On", tagCode);
                        return Ok(new { Message = $"Işık '{tagCode}' açıldı" });
                    }
                    catch (Exception ex)
                    {
                        return BadRequest("Error: " + ex.Message);
                    }
                }
                else
                {
                    return BadRequest("User yetkisi bu oda için bulunamadı.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = ex.Message });
            }
        }

        [HttpPost("turnoff")]
        public async Task<IActionResult> TurnOffLight(string tagCode,Guid userID,Guid roomID)
        {
            try
            {
                var grpcRequest = new WriteValue
                {
                    InstrumentCode = "DeparkOfisPLC",
                    TagCode = tagCode,
                    Value = new Value { BoolValue = false }
                };

                var userAccessResult = _userRoomRepository.CheckUserRoomByCompositPK(userID, roomID);
                if (userAccessResult)
                {
                    try
                    {
                        await _grpcClient.WriteTagValueAsync(grpcRequest);
                        await _logRepository.LogUserActivity(userID, "Turn Off", tagCode);
                        return Ok(new { Message = $"Işık '{tagCode}' kapandı" });
                    }
                    catch(Exception ex)
                    {
                        return BadRequest("Error: " + ex.Message);
                    }
                }
                else
                {
                    return BadRequest("User yetkisi bu oda için bulunamadı.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = ex.Message });
            }
        }
    }

    
}
