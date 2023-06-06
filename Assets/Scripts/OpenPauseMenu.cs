using Tanks;
using UnityEngine;

public class OpenPauseMenu : MonoBehaviour
{
    [SerializeField]
    private PauseMenu _menu;

    void Update()
    {
        if (_menu.gameObject.activeSelf == false) OnPause();
    }

    public void OnPause()
    {
        var key = Input.GetKeyDown(KeyCode.Escape);

        if (key)
        {
            _menu.gameObject.SetActive(true);
        }
    }
}
