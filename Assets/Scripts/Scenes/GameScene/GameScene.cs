using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// GameScene Ŭ������ ������ �ֿ� ������ �����ϴ� �ٽ� MonoBehaviour ��ũ��Ʈ�̴�.
/// - BattleFSM�� ���� Ready, Wave, Game, Result ���¸� ����.
/// - GameUI�� HubUI�� �ʱ�ȭ�ϰ� ���¿� ���� ������Ʈ.
/// - ���� ������ �ε� �� ���� ����.
/// - Unity�� MonoBehaviour �̺�Ʈ �Լ�(Awake, Start, Update ��) Ȱ��.
/// 
/// �ֿ� ���:
/// 1. Awake: Asset �� ���� �����͸� �ε��ϰ� BattleFSM�� �ʱ�ȭ.
/// 2. Start: ���� �Ŵ����� �����Ͽ� �ʱ� ����(ReadyState)�� ����.
/// 3. Update: BattleFSM �� UI ���¸� �ֱ������� ������Ʈ.
/// 4. ���� ���濡 ���� �ʱ�ȭ �ݹ� (Ready, Wave, Game, Result).
/// 5. ���ø����̼� ���� �� ���� ������ ���� ó��.
/// </summary>
public class GameScene : MonoBehaviour
{
	[HideInInspector] public BattleFSM m_BattleFSM = null;  // ���� ���� ���� FSM
	public GameUI m_GameUI = null;  // ���� UI ����
	public HubUI m_HubUI = null;    // HUD (Hub UI) ����

	/// <summary>
	/// Awake�� ���� ���� ����Ǵ� MonoBehaviour �̺�Ʈ��, �ʱ�ȭ �۾��� �����Ѵ�.
	/// </summary>
	void Awake()
	{
		Screen.SetResolution(720, 1280, true);	// ȭ�� ����
		AssetMng.Inst.Initialize(); // ���� �Ŵ��� �ʱ�ȭ

		GameMng.Inst.LoadFile();    // ���� ������ �ε�
		InitFSM();                  // FSM(���� ���) �ʱ�ȭ
	}

	/// <summary>
	/// Start�� ���� ���� �� ȣ��Ǹ�, ���� �Ŵ����� �����ϰ� �ʱ� ���¸� �����Ѵ�.
	/// </summary>
	void Start()
	{
		GameMng.Inst.Initialize();       // ���� �Ŵ��� �ʱ�ȭ
		GameMng.Inst.SetGameScene(this); // GameScene ���
		Initialize();                    // �ʱ� ����(ReadyState) ����
	}

	/// <summary>
	/// ReadyState�� �ʱ�ȭ�Ͽ� ���� ���� �غ� �Ϸ��Ѵ�.
	/// </summary>
	public void Initialize()
	{
		//m_BattleFSM.SetReadyState(); // Ready ���·� ����
		m_BattleFSM.SetGameState();
	}

	/// <summary>
	/// BattleFSM(Finite State Machine)�� �ʱ�ȭ�Ѵ�.
	/// </summary>
	public void InitFSM()
	{
		if (m_BattleFSM == null)
		{
			m_BattleFSM = new BattleFSM();
			m_BattleFSM.Initialize(Callback_ReadyEnter, Callback_WaveEnter, Callback_GameEnter, Callback_ResultEnter);
		}
	}

	/// <summary>
	/// �� ������ ������Ʈ ������ ó���Ѵ�.
	/// - FSM ���� ������Ʈ.
	/// - �� UI ������Ʈ.
	/// - GameState ���¿��� �߰����� ���� ���� ������Ʈ.
	/// </summary>
	void Update()
	{
		if (m_BattleFSM != null)
		{
			m_BattleFSM.OnUpdate();  // FSM ���� ������Ʈ
			m_GameUI.OnUpdate();

			// Hub UI ������Ʈ
			//m_HubUI.m_HPBarUI.OnUpdate();
			//m_HubUI.m_StageUI.OnUpdate();

			// GameState ������ ��� �߰� ����
			if (m_BattleFSM.IsGameState())
			{
				//GameMng.Inst.OnUpdate(Time.deltaTime);  // ���� �Ŵ��� ������Ʈ
				//m_HubUI.m_LifeTimeUI.OnUpdate();
				//m_HubUI.m_KeepTimeBarUI.OnUpdate();
			}
		}
	}
	// FSM ���� ���� �ݹ� �޼���
	public void Callback_ReadyEnter()
	{
		Initialize_ReadyState();
	}
	public void Callback_WaveEnter()
	{
		// Wave ���� �ʱ�ȭ ���� (���� �ʿ�)
	}
	public void Callback_GameEnter()
	{
		Initialize_GameState();
	}
	public void Callback_ResultEnter()
	{
		Initialize_ResultState();
	}

	// ReadyState �ʱ�ȭ ����
	private void Initialize_ReadyState()
	{
		GameMng.Inst.InitStart();            // ���� ���� �غ�
		m_HubUI.Initialize_ReadyState();     // Hub UI �ʱ�ȭ
		m_GameUI.Initialize_ReadyState();    // Game UI �ʱ�ȭ
	}

	// GameState �ʱ�ȭ ����
	private void Initialize_GameState()
	{
		m_HubUI.Initialize_GameState();      // Hub UI �ʱ�ȭ
		m_GameUI.Initialize_GameState();     // Game UI �ʱ�ȭ
	}

	// ResultState �ʱ�ȭ ����
	private void Initialize_ResultState()
	{
		m_HubUI.Initialize_ResultState();    // Hub UI �ʱ�ȭ
		m_GameUI.Initialize_ResultState();   // Game UI �ʱ�ȭ
	}

	/// <summary>
	/// ���ø����̼� ���� �� ȣ��Ǹ�, ���� �����͸� �����Ѵ�.
	/// </summary>
	private void OnApplicationQuit()
	{
		Debug.Log("[GameScene] App Quit......");
		try
		{
			GameMng.Inst.SaveFile(); // ���� ������ ����
		}
		catch (System.Exception e)
		{
			Debug.Log("[_Error_Quit]" + e.ToString()); // ���� �α�
		}
	}
}
