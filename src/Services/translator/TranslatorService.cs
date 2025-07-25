﻿using System.Net.Sockets;
using ChatBot.bot.interfaces;
using ChatBot.Services.interfaces;
using ChatBot.Services.logger;
using ChatBot.Services.Static;
using ChatBot.Services.translator.Google;
using ChatBot.Services.translator.Google.Data;
using ChatBot.Services.translator.VK;

namespace ChatBot.Services.translator;

public enum TranslationService {
    Google,
    Vk
}

public class TranslatorService : Service {
    private static readonly LoggerService _logger = (LoggerService)ServiceManager.GetService(ServiceName.Logger);
    private GoogleTranslateClient _gTranslateClient = null!;
    private VkTranslateClient _vkTranslateClient = null!;
    
    public override string Name => ServiceName.Translator;
    public override TranslatorOptions Options { get; } = new();


    public async Task<string?> Translate(string text, string? targetLang = null, string? sourceLang = null) {
        try {
            var targetLanguage = string.IsNullOrEmpty(targetLang) ? Options.TargetLanguage : targetLang;

            return Options.TranslationService switch {
                       TranslationService.Google => await GTranslate(text, targetLanguage),
                       TranslationService.Vk     => await VkTranslate(text, targetLanguage, sourceLang),
                       _                         => null
                   };

        } catch (Exception e) {
            _logger.Log(LogLevel.Error, $"Error while translating. {e.Message}");
            return null;
        }
    }
    
    private async Task<string?> GTranslate(string text, string targetLang) {
        try {
            var response = await _gTranslateClient.Translate([text], targetLang, logger: _logger);
            
            if (response is { Translations.Count: > 0 }) {
                return response.Translations[0].TranslatedText;
            }
            
            _logger.Log(LogLevel.Error, $"Failed to translate '{text}' to {targetLang}");
            return null;
        } catch (Exception) {
            return null;
        }
    }

    private async Task<string?> VkTranslate(string text, string targetLang, string? sourceLang = null) {
        try {
            string[]? response;
            if (!string.IsNullOrEmpty(sourceLang)) {
                response = await _vkTranslateClient.Translate(text, targetLang, sourceLang, logger: _logger);
            } else {
                response = await _vkTranslateClient.Translate(text, targetLang, logger: _logger);
            }
            
            if (response?.Length > 0) {
                return response[0];
            }
            
            _logger.Log(LogLevel.Error, $"Failed to translate '{text}' to {targetLang}");
            return null;
        } catch (Exception) {
            return null;
        }
    }

    public async Task<DetectedLanguage?> DetectLanguage(string text) {
        try {
            var response = await _gTranslateClient.DetectLanguage(text, _logger);
            
            if (response is { Languages.Count: > 0 }) {
                return response.Languages[0];
            }
            
            _logger.Log(LogLevel.Error, $"Failed to detect language of '{text}'");
            return null;
        } catch (Exception e) {
            _logger.Log(LogLevel.Error, $"Error while detecting a language. {e.Message}");
            return null;
        }
    }

    public string GetProjectId() {
        return Options.ProjectId;
    }

    public void SetProjectId(string projectId) {
        Options.SetProjectId(projectId);
        _gTranslateClient = new GoogleTranslateClient(Options.ProjectId, Options.Location, Options.GoogleToken);
    }

    public string GetLocation() {
        return Options.Location;
    }

    public void SetLocation(string location) {
        Options.SetLocation(location);
        _gTranslateClient = new GoogleTranslateClient(Options.ProjectId, Options.Location, Options.GoogleToken);
    }

    public string GetGoogleToken() {
        return Options.GoogleToken;
    }

    public void SetGoogleToken(string token) {
        Options.SetGoogleToken(token);
        _gTranslateClient = new GoogleTranslateClient(Options.ProjectId, Options.Location, Options.GoogleToken);
    }

    public void SetVkToken(string token) {
        Options.SetVkToken(token);
        _vkTranslateClient = new VkTranslateClient(Options.VkToken);
    }
    
    public string GetVkToken() {
        return Options.VkToken;
    }
    
    public string GetTargetLanguage() {
        return Options.TargetLanguage;
    }

    public void SetTargetLanguage(string language) {
        Options.SetTargetLanguage(language);
    }

    public TranslationService GetTranslationService() {
        return Options.TranslationService;
    }
    
    public int GetTranslationServiceAsInt() {
        return (int)Options.TranslationService;
    }

    public void TranslationServiceNext() {
        Options.SetTranslationService((TranslationService)(((int)Options.TranslationService+1)%Enum.GetValues(typeof(TranslationService)).Length));
    }
    
    public override void Init(Bot bot) {
        base.Init(bot);
        _gTranslateClient = new GoogleTranslateClient(Options.ProjectId, Options.Location, Options.GoogleToken);
        _vkTranslateClient = new VkTranslateClient(Options.VkToken);
    }
}