using Romeno.Utils;
using UnityEditor;


namespace Romeno.WizardTest
{
    public class GameManager : StrictSingleton<GameManager>
    {
        protected override void Setup()
        {
            
        }

        public void StartGame()
        {
            PoolManager.I.Init();

            UIManager.I.ShowWidget<PreStartScreen>();
            CameraManager.I.ActivateOverviewCamera();
        }

        public void Quit()
        {
#if UNITY_EDITOR
            if (EditorApplication.isPlaying)
            {
                EditorApplication.isPlaying = false;
            }
#else 
            Application.Quit();
#endif
        }

        public void StartRound()
        {
            UIManager.I.ShowWidget<HUD>();
            UIManager.I.ShowWidget<CountdownWidget>().StartCountdown(DB.I.RoundSettings.StartRoundCountdownTime);
            UIManager.I.HideWidget<PreStartScreen>();
            
            RoundManager.I.StartRound();
        }

        public void RestartRound()
        {
            UIManager.I.ShowWidget<HUD>();
            UIManager.I.ShowWidget<CountdownWidget>().StartCountdown(DB.I.RoundSettings.StartRoundCountdownTime);
            UIManager.I.HideWidget<RoundResultScreen>();
            
            RoundManager.I.RestartRound();
        }
    }
}