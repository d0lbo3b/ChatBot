﻿using ChatBot.shared.Handlers;
using TwitchLib.Client.Interfaces;

namespace ChatBot.CLI.CliNodes.Client;

public delegate void ClientHandler(ITwitchClient? client, string channel);

public class CliNodeClient : CliNode {
    private readonly ClientHandler _clientHandler;
    private ITwitchClient? _client = null!;
    private string _channel = null!;
    
    protected override string Text { get; }

    
    public CliNodeClient(string text, ClientHandler clientHandler, CliState state) {
        Text = text;
        _clientHandler = clientHandler;
    }
    
    public override void Activate(CliState state) {
        var err = state.Data.Bot.TryGetClient(out _client);
        if (ErrorHandler.LogErrorAndPrint(err)) {
            return;
        }
        
        _channel = state.Data.Bot.Options.GetChannel();
        _clientHandler.Invoke(_client, _channel);
    }
}