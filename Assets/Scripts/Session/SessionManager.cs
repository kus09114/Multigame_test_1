using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Netcode;
using Unity.Netcode.Components;
using Unity.Services.Authentication;
using Unity.Services.CloudSave;
using Unity.Services.CloudSave.Models.Data.Player;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using Unity.Mathematics;

public class SessionManager : NetworkBehaviour
{
	[SerializeField] private NetworkPrefabsList charactersPrefab = null;

	public static Role role = Role.Client;
	public static string joinCode = "";
	public static string lobbyID = "";

	public enum Role
	{
		Client = 1, Host = 2, Server = 3
	}

	private static SessionManager singleton = null;
	public static SessionManager Singleton
	{
		get
		{
			if(singleton == null)
			{
				singleton = FindFirstObjectByType<SessionManager>();
				singleton.Initialize();
			}
			return singleton;
		}
	}

    private bool initialized = false;

	private void Initialize()
	{
		if (initialized) { return; }
		initialized = true;
	}

	public override void OnDestroy()
	{
        if (singleton == this)
        {
			singleton = null;
        }
        base.OnDestroy();
	}

	private void Start() // Do not do this in Awake
	{
		Initialize();
		NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
		if (role == Role.Client)
		{
			NetworkManager.Singleton.StartClient();
		}
		else if (role == Role.Host)
		{
			NetworkManager.Singleton.StartHost();
			if (string.IsNullOrEmpty(joinCode) == false && string.IsNullOrEmpty(lobbyID) == false)
			{
				SetLobbyJoinCode(joinCode);
			}
		}
		else
		{
			NetworkManager.Singleton.StartServer();
		}
	}

	private void OnClientConnected(ulong id)
	{
		if(NetworkManager.Singleton.IsServer)
		{
			RpcParams rpcParams = NetworkManager.Singleton.RpcTarget.Single(id, RpcTargetUse.Temp);
			InitializeRpc(rpcParams);
		}
    }

	[Rpc(SendTo.SpecifiedInParams)]
	private void InitializeRpc(RpcParams rpcParams)
	{
		InitializeClient();
	}

	private async void InitializeClient()
	{
		int character = 0;
		string color = "";
		try
		{
			var playerData = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string> { "character" }, new LoadOptions(new PublicReadAccessClassOptions()));
			if(playerData.TryGetValue("character", out var characterData))
			{
				var data = characterData.Value.GetAs<Dictionary<string, object>>();
				character = int.Parse(data["type"].ToString());
				color = data["color"].ToString();
			}
		}
		catch(Exception exception)
		{
			Debug.Log(exception.Message);
		}
		InstantiateCharacterRpc(character, AuthenticationService.Instance.PlayerId, color);
	}

	[Rpc(SendTo.Server, RequireOwnership = false)]
	private void InstantiateCharacterRpc(int character, string id, string color, RpcParams rpcParams = default)
	{
		Vector3 position = SessionSpawnPoints.Singleton.GetSpawnPositionOrdered();
		var prefab = charactersPrefab.PrefabList[character].Prefab.GetComponent<NetworkObject>();
		var networkObject = NetworkManager.Singleton.SpawnManager.InstantiateAndSpawn(prefab, rpcParams.Receive.SenderClientId, true, true, false, position, quaternion.identity);
		SessionPlayer player = networkObject.GetComponent<SessionPlayer>();
		player.ApplyDataRpc(id, color);

		SessionPlayer[] players = FindObjectsByType<SessionPlayer>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
		if(player != null)
		{
			for(int i = 0; i < players.Length; i++)
			{
				if(players[i] != null)
				{
					players[i].ApplyDataRpc();
				}
			}
		}
	}

	private async void SetLobbyJoinCode(string code)
	{
		try
		{
			UpdateLobbyOptions options = new UpdateLobbyOptions();
			options.Data = new Dictionary<string, DataObject>();
			options.Data.Add("join_code", new DataObject(visibility: DataObject.VisibilityOptions.Public, value: code));
			var lobby = await LobbyService.Instance.UpdateLobbyAsync(lobbyID, options);
		}
		catch (Exception exception)
		{
			Debug.Log(exception.Message);
		}
	}
}
