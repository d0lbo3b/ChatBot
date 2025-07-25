﻿using Newtonsoft.Json;

namespace ChatBot.utils.Twitch.Helix.Data;

public class RewardData {
    [JsonProperty("id")]
    public string Id { get; set; }
    
    [JsonProperty("title")]
    public string Title { get; set; }
    
    [JsonProperty("cost")]
    public int Cost { get; set; }
    
    
    public RewardData(string id, string title, int cost) {
        Id = id;
        Title = title;
        Cost = cost;
    }
}