using Tanks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private Button _unpauseButton;
    [SerializeField]
    private Button _startMenuButton;
    private int i = 0;

    private void Start()
    {
        _unpauseButton.onClick.AddListener(ClosePauseMenu);
        _startMenuButton.onClick.AddListener(QuitGame);
    }
    private void OnEnable()
    {
        if (i == 0)
        {
            i++;
            return; }

            Time.timeScale = 0f;
            AudioManager.Instance.moveSource.mute = true;
            AudioManager.Instance.PlaySFX("Pause");
    }
    private void ClosePauseMenu()
    {
        AudioManager.Instance.moveSource.mute = false;
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    private void QuitGame()
    {
        Time.timeScale = 1f;
        AudioManager.Instance.musicSource.Play();
        AudioManager.Instance.moveSource.Stop();
        AudioManager.Instance.sfxSource.Stop();
        AudioManager.Instance.moveSource.mute = false;
        SceneManager.LoadScene("Menu");
    }

}
