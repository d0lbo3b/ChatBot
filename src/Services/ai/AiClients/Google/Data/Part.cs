﻿using Newtonsoft.Json;

namespace ChatBot.Services.ai.AiClients.Google.Data;

public class Part {
    [JsonProperty("text")]
    public string Text { get; set; } = null!;
}