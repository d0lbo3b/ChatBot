﻿using ChatBot.shared.interfaces;
using Newtonsoft.Json;

namespace ChatBot.Services.demon_list;

public class SaveData {
    [JsonProperty(PropertyName = "service_state")]
    public State ServiceState { get; set; }
    
    
    public SaveData() {}
    
    [JsonConstructor]
    public SaveData(
        [JsonProperty(PropertyName = "service_state")] State serviceState) {
        ServiceState = serviceState;
    }
}