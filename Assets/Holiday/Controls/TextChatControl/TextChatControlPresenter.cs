﻿using System.Diagnostics.CodeAnalysis;
using Cysharp.Threading.Tasks;
using Extreal.Core.StageNavigation;
using Extreal.Integration.Chat.Vivox;
using Extreal.SampleApp.Holiday.App;
using Extreal.SampleApp.Holiday.App.Common;
using Extreal.SampleApp.Holiday.App.Config;
using UniRx;

namespace Extreal.SampleApp.Holiday.Controls.TextChatControl
{
    public class TextChatControlPresenter : StagePresenterBase
    {
        private readonly VivoxClient vivoxClient;
        private readonly TextChatControlView textChatControlView;
        private readonly AppState appState;

        private TextChatChannel textChatChannel;

        public TextChatControlPresenter(
            StageNavigator<StageName, SceneName> stageNavigator,
            VivoxClient vivoxClient,
            TextChatControlView textChatControlView,
            AppState appState) : base(stageNavigator)
        {
            this.vivoxClient = vivoxClient;
            this.textChatControlView = textChatControlView;
            this.appState = appState;
        }

        [SuppressMessage("CodeCracker", "CC0020")]
        protected override void Initialize(StageNavigator<StageName, SceneName> stageNavigator, CompositeDisposable sceneDisposables) =>
            textChatControlView.OnSendButtonClicked
                .Subscribe(message => textChatChannel.SendMessage(message)).AddTo(sceneDisposables);

        protected override void OnStageEntered(StageName stageName, CompositeDisposable stageDisposables)
        {
            textChatChannel = new TextChatChannel(vivoxClient, $"HolidayTextChat{stageName}");
            stageDisposables.Add(textChatChannel);

            textChatChannel.OnConnected
                .Subscribe(appState.SetInText)
                .AddTo(stageDisposables);

            textChatChannel.OnMessageReceived
                .Subscribe(textChatControlView.ShowMessage)
                .AddTo(stageDisposables);

            textChatChannel.OnUnexpectedDisconnected
                .Subscribe(_ => appState.SetNotification("Unexpected disconnection from vivox server has occurred"))
                .AddTo(stageDisposables);

            textChatChannel.OnConnectFailed
                .Subscribe(_ => appState.SetNotification("Connection to vivox server is failed"))
                .AddTo(stageDisposables);

            textChatChannel.JoinAsync().Forget();
        }

        protected override void OnStageExiting(StageName stageName)
        {
            appState.SetInText(false);
            textChatChannel.Leave();
        }
    }
}
