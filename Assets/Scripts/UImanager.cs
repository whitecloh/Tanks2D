using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Tanks
{
    public class UImanager : MonoBehaviour
    {
        [SerializeField]
        private GameObject _resultsImage;
        [SerializeField]
        private Text _resultsText;
        [SerializeField]
        private PauseMenu _pauseMenu;
        [SerializeField]
        private DeadAnimation _deadAnimation;

        public DeadAnimation GetDeadAnimation => _deadAnimation;

        public GameObject GetResultsImage => _resultsImage;
        public Text GetResultsText => _resultsText;
        private void Awake()
        {
            _pauseMenu.gameObject.SetActive(false);
        }
        private void Start()
        {
            Time.timeScale = 1;
            Camera.main.GetComponent<AudioListener>().enabled = true;
        }
        public void OnEndGame()
        {
            AudioManager.Instance.moveSource.mute=true;
            StartCoroutine(EndGame());
        }
        private IEnumerator EndGame()
        {
            Time.timeScale = 0;
            GetResultsImage.SetActive(true);
            AudioManager.Instance.PlaySFX("GameOver");
            yield return new WaitForSecondsRealtime(3f);
            LoadMenu();
            yield return null;
        }
        private void LoadMenu()
        {
            StopAllCoroutines();
            AudioManager.Instance.musicSource.Play();
            AudioManager.Instance.moveSource.Stop();
            AudioManager.Instance.moveSource.mute = false;
            SceneManager.LoadScene("Menu");
        }
    }
}
