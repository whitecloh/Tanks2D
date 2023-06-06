using System.Collections;
using Tanks;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    [SerializeField]
    private Button _startButton;
    [SerializeField]
    private Button _quitButton;
    [SerializeField]
    private Image _backgroundImage;
    [SerializeField]
    private float _bpm;

    private void Start()
    {
        _startButton.onClick.AddListener(GameStart);
        _quitButton.onClick.AddListener(GameQuit);
        StartCoroutine(ChangeImageColor());
    }
    IEnumerator ChangeImageColor()
    {
        _backgroundImage.GetComponent<Image>().color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        _backgroundImage.transform.localEulerAngles += new Vector3(0, 0, 30f);
        yield return new WaitForSecondsRealtime(_bpm);
        StartCoroutine(ChangeImageColor());
    }
    private void GameStart()
    {
        AudioManager.Instance.musicSource.Stop();
        StopAllCoroutines();
        SceneManager.LoadScene("GameScene");
    }
    private void GameQuit()
    {
#if UNITY_EDITOR
        EditorApplication.isPaused = true;
#elif !UNITY_EDITOR && UNITY_STANDALONE_WIN
        Application.Quit();
#endif
    }
}
