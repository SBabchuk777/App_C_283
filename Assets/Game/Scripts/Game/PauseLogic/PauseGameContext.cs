using DG.Tweening;
using Game.Mephistoss.PanelMachine.Scripts;
using Prototype.SceneLoaderCore.Helpers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Scripts.Game.ExitGameLogic
{
    public class PauseGameContext : MonoBehaviour
    {
        [SerializeField] private PanelMachine _panelMachine;

        [SerializeField] private PanelBase _pauseGamePanel;
        [SerializeField] private PanelBase _exitGamePanel;
        [SerializeField] private PanelBase _restartGamePanel;

        public void AddPause()
        {
            _panelMachine.AddPanel(_pauseGamePanel);

            DisableTimeScale();
        }

        public void ContinueGame()
        {
            EnableTimeScale();

            _panelMachine.CloseLastPanel();
        }

        public void AddRestart() => 
            CloseLastAndOpenNew(_restartGamePanel);

        public void AddExit() => 
            CloseLastAndOpenNew(_exitGamePanel);

        public void ReturnToPausePanel() => 
            CloseLastAndOpenNew(_pauseGamePanel);

        public void CloseLastAndOpenNew(PanelBase newPanel)
        {
            _panelMachine.CloseLastPanel();
            _panelMachine.AddPanel(newPanel);
        }

        public void RestartGame()
        {
            EnableTimeScale();

            DOTween.KillAll();
            SceneLoader.Instance.SwitchToScene(SceneManager.GetActiveScene().name);
        }

        public void GoToMenu()
        {
            EnableTimeScale();

            DOTween.KillAll();
            SceneLoader.Instance.SwitchToScene("Menu");
        }

        private void DisableTimeScale()
        {
            Time.timeScale = 0;
            DOTween.timeScale = 0;
        }

        private void EnableTimeScale()
        {
            Time.timeScale = 1;
            DOTween.timeScale = 1;
        }
    }
}