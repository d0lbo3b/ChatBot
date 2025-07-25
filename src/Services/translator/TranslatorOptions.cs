﻿using System.Resources;
using ChatBot.shared;
using ChatBot.shared.Handlers;
using ChatBot.shared.interfaces;
using ChatBot.utils;

namespace ChatBot.Services.translator;

public class TranslatorOptions : Options {
    private SaveData? _saveData;

    protected override string Name => "translator";
    protected override string OptionsPath => Path.Combine(Directories.ServiceDirectory+Name, $"{Name}_opt.json");
    
    public override State ServiceState => _saveData!.ServiceState;
    public string ProjectId => _saveData!.ProjectId;
    public string Location => _saveData!.Location;
    public string GoogleToken => _saveData!.GoogleToken;
    public string VkToken => _saveData!.VkToken;
    public string TargetLanguage => _saveData!.TargetLanguage;
    public TranslationService TranslationService => _saveData!.TranslationService;


    public override bool TryLoad() {
        return JsonUtils.TryRead(OptionsPath, out _saveData);
    }

    public override void Load() {
        if (!JsonUtils.TryRead(OptionsPath, out _saveData!)) {
            ErrorHandler.LogErrorAndPrint(ErrorCode.SaveIssue);
            SetDefaults();
        }
    }

    public override void Save() {
        JsonUtils.WriteSafe(OptionsPath, Path.Combine(Directories.ServiceDirectory, Name), _saveData);
    }

    public override void SetDefaults() {
        _saveData = new();
        Save();
    }

    public override void SetState(State state) {
        _saveData!.ServiceState = state;
        Save();
    }

    public override State GetState() {
        return ServiceState;
    }

    public void SetProjectId(string projectId) {
        _saveData!.ProjectId = projectId;
        Save();
    }
    
    public void SetLocation(string location) {
        _saveData!.Location = location;
        Save();
    }
    
    public void SetGoogleToken(string token) {
        _saveData!.GoogleToken = token;
        Save();
    }
    
    public void SetVkToken(string token) {
        _saveData!.VkToken = token;
        Save();
    }

    public void SetTargetLanguage(string language) {
        _saveData!.TargetLanguage = language;
        Save();
    }

    public void SetTranslationService(TranslationService service) {
        _saveData!.TranslationService = service;
        Save();
    }
}