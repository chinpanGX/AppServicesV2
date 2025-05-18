#nullable enable
using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;

namespace UniVerse.ComponentEx.UI.Button
{
    using Button = UnityEngine.UI.Button;
    using UI;
    using Audio;
    using Logger;

    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(CustomText))]
    public class CustomButton : MonoBehaviour
    {
        [SerializeField] private Button? button;
        [SerializeField] private CustomText? text;
        [SerializeField] private ButtonSeType buttonSeType = ButtonSeType.None;

        private bool isConstructed = false;

        // クリックのクールダウン時間
        private readonly TimeSpan cooldown = TimeSpan.FromSeconds(0.1f);
        private readonly ReactiveProperty<bool> interactable = new(true);

        private readonly Subject<Unit> onClickSubject = new();
        public Observable<Unit> OnClickAsObservable
        {
            get
            {
                if (button == null)
                {
                    LoggerEx.LogError("Button is null");
                    return Observable.Empty<Unit>();
                }

                if (!isConstructed)
                {
                    LoggerEx.LogError("Button is not constructed");
                    return Observable.Empty<Unit>();
                }

                return onClickSubject;
            }
        }

        public CustomText Text
        {
            get
            {
                if (text == null)
                {
                    LoggerEx.LogError("TextMeshProUGUI is null");
                    return null!;
                }
                return text;
            }
        }

        public void Construct(CancellationToken cancellationToken,
            CanvasGroup? viewCanvasGroup = null,
            IButtonSePlayer? buttonSePlayer = null)
        {
            if (button == null)
            {
                LoggerEx.LogError("Button is null");
                return;
            }

            interactable.Subscribe(value =>
                {
                    SetInteractableSafe(value);
                    viewCanvasGroup.SetInteractableSafe(value);
                }
            ).RegisterTo(cancellationToken);

            button.OnClickAsObservable()
                .Where(_ => interactable.Value)
                .SubscribeAwait(async (_, ct) => { await ClickActionAsync(buttonSePlayer, ct); }, AwaitOperation.Drop
                )
                .RegisterTo(cancellationToken);

            isConstructed = true;
        }

        private async UniTask ClickActionAsync(IButtonSePlayer? sePlayer, CancellationToken cancellationToken)
        {
            try
            {
                interactable.Value = false;
                sePlayer?.Play(buttonSeType);
                onClickSubject.OnNext(Unit.Default);
                await UniTask.Delay(cooldown, cancellationToken: cancellationToken);
            }
            finally
            {
                interactable.Value = true;
            }
        }

        public void SetInteractableSafe(bool value)
        {
            if (button != null)
                button.interactable = value;
        }

        public void SetTextSafe(string value)
        {
            if (text != null)
                text.SetTextSafe(value);
        }
    }
}