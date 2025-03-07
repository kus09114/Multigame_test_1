using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;
using Unity.Services.Authentication;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;

public class LobbyPlayerItem : MonoBehaviour
{
	[SerializeField] public TextMeshProUGUI nameText = null;
	[SerializeField] public TextMeshProUGUI roleText = null;
	[SerializeField] public TextMeshProUGUI statusText = null;
	[SerializeField] private Button kickButton = null;

	private string lobbyId = "";
	private Player player = null;

	private void Start()
	{
		kickButton.onClick.AddListener(Kick);
	}

	public void Initialize(Player player, string lobbyId, string hostId)
	{
		this.player = player;
		this.lobbyId = lobbyId;
		nameText.text = player.Data["name"].Value;
		roleText.text = player.Id == hostId ? "Host" : "Member";
		bool isReady = player.Data["ready"].Value == "1";
		statusText.text = isReady ? "Ready" : "Not Ready";
		kickButton.gameObject.SetActive(player.Id != hostId && AuthenticationService.Instance.PlayerId == hostId);
	}

	private async void Kick()
	{
		kickButton.interactable = false;
		try
		{
			await LobbyService.Instance.RemovePlayerAsync(lobbyId, player.Id);
			Destroy(gameObject);
		}
		catch (Exception exception)
		{
			Debug.Log(exception.Message);
		}
		kickButton.interactable = true;
	}
}
