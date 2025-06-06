﻿using ChatBot.shared.interfaces;
using Newtonsoft.Json;

namespace ChatBot.Services.text_generator;

public class SaveData {
    [JsonProperty(PropertyName = "service_state")]
    public State ServiceState { get; set; } 
    [JsonProperty(PropertyName = "context_size")]
    public int ContextSize { get; set; }
    [JsonProperty(PropertyName = "max_length")]
    public int MaxLength { get; set; }
    [JsonProperty(PropertyName = "model")]
    public Dictionary<string, Dictionary<string, int>> Model { get; private set; }


    [JsonConstructor]
    public SaveData(
        [JsonProperty(PropertyName = "service_state")] State serviceState,
        [JsonProperty(PropertyName = "context_size")] int contextSize,
        [JsonProperty(PropertyName = "max_length")] int maxLength,
        [JsonProperty(PropertyName = "model")] Dictionary<string, Dictionary<string, int>> model
        ) {
        ServiceState = serviceState;
        ContextSize = contextSize;
        MaxLength = maxLength;
        Model = model;
    }
}