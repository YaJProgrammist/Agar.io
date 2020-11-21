using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenuController : MonoBehaviour
{
    [SerializeField]
    GameObject startMenu;

    [SerializeField]
    InputField nameField;

    [SerializeField]
    Button playButton;

    [SerializeField]
    Text errorField;

    private void Awake()
    {
        nameField.gameObject.SetActive(true);
        playButton.gameObject.SetActive(true);
        errorField.gameObject.SetActive(false);

        playButton.onClick.AddListener(() => {
            if (nameField.text != "")
            {
                PlayerManager.Instance.currentPlayerName = nameField.text;
                GameManager.Instance.SetConnection();
                UIManager.Instance.ShowWaitingWindow();
                errorField.gameObject.SetActive(false);
                Close();

            } else
            {
                errorField.gameObject.SetActive(true);
            }
        });
    }

    public void Open()
    {
        startMenu.SetActive(true);
    }

    public void Close()
    {
        startMenu.SetActive(false);
    }
}
