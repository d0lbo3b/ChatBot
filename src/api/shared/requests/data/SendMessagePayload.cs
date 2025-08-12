﻿using Newtonsoft.Json;

namespace ChatBot.api.shared.requests.data;

public class SendMessagePayload {
    [JsonProperty("broadcaster_id")]
    public string BroadcasterId { get; }
    
    [JsonProperty("sender_id")]
    public string SenderId { get; }
    
    [JsonProperty("message")]
    public string Message { get; }


    public SendMessagePayload(
        string broadcasterId,
        string senderId,
        string message) {
        BroadcasterId = broadcasterId;
        SenderId = senderId;
        Message = message;
    }
}