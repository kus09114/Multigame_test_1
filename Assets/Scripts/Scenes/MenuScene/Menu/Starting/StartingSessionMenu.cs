using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.Services.Lobbies.Models;
using UnityEngine.SceneManagement;

public class StartingSessionMenu : Panel
{
    [SerializeField] private TextMeshProUGUI statusText = null;
    [SerializeField] private Image progressImage = null;

    private bool allowSceneActivation = false;
    public bool isConfirmed
    {
        get
        {
            return allowSceneActivation;
        }
    }

    private bool loading = false;
    public bool isLoading
    {
        get
        {
            return loading;
        }
    }

    public void StartGameByLobby(Lobby lobby, bool waitForConfirmation)
    {
        Open();
        statusText.text = "Loading the game, please wait ...";

        string map = lobby.Data["map"].Value;
        string sceneName = "SessionMap01";
        // ToDo: Set scene name based on map value

        string mode = lobby.Data["mode"].Value;

        allowSceneActivation = !waitForConfirmation;
        if(loading == false)
        {
			loading = true;
            StartCoroutine(LoadAsyncScene(sceneName));
        }
	}

    public void StartGameConfirm()
    {
        allowSceneActivation = true;
    }

	public override void Open()
	{
        progressImage.fillAmount = 0;
		base.Open();
	}

	public override void Close()
	{
		base.Close();
        loading = false;
        allowSceneActivation = false;
	}

	private IEnumerator LoadAsyncScene(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        operation.allowSceneActivation = false;

        while (operation.isDone == false)
        {
            progressImage.fillAmount = operation.progress;
            if(operation.progress >= 0.9f)
            {
                operation.allowSceneActivation = allowSceneActivation;
            }
            yield return null;
        }
    }
}
