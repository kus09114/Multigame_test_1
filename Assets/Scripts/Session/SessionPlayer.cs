using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Netcode.Components;

public class SessionPlayer : NetworkBehaviour
{
    [SerializeField] private float moveSpeed = 2;
    [SerializeField] private MeshRenderer meshRenderer = null;
    private CharacterController controller = null;
	private string _colorHex = "";
	private string _id = "";

	private void Awake()
	{
		controller = GetComponent<CharacterController>();
	}

	private void Update()
	{
		if(IsOwner)
		{
			Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
			controller.Move(new Vector3(moveInput.x, 0, moveInput.y) * Time.deltaTime * moveSpeed);
		}
	}

	[Rpc(SendTo.Everyone)]
	public void ApplyDataRpc(string id, string colorHex)
	{
		ColorUtility.TryParseHtmlString(colorHex, out Color color);
		meshRenderer.material.color = color;
		_colorHex = colorHex;
		_id = id;
	}

	public void ApplyDataRpc()
	{
		ApplyDataRpc(_id, _colorHex);
	}
}
