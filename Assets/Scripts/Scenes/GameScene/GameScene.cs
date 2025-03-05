using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// GameScene 클래스는 게임의 주요 동작을 관리하는 핵심 MonoBehaviour 스크립트이다.
/// - BattleFSM을 통해 Ready, Wave, Game, Result 상태를 제어.
/// - GameUI와 HubUI를 초기화하고 상태에 따라 업데이트.
/// - 게임 데이터 로드 및 저장 관리.
/// - Unity의 MonoBehaviour 이벤트 함수(Awake, Start, Update 등) 활용.
/// 
/// 주요 기능:
/// 1. Awake: Asset 및 게임 데이터를 로드하고 BattleFSM을 초기화.
/// 2. Start: 게임 매니저와 연결하여 초기 상태(ReadyState)를 설정.
/// 3. Update: BattleFSM 및 UI 상태를 주기적으로 업데이트.
/// 4. 상태 변경에 따른 초기화 콜백 (Ready, Wave, Game, Result).
/// 5. 애플리케이션 종료 시 게임 데이터 저장 처리.
/// </summary>
public class GameScene : MonoBehaviour
{
	[HideInInspector] public BattleFSM m_BattleFSM = null;  // 전투 상태 관리 FSM
	public GameUI m_GameUI = null;  // 게임 UI 관리
	public HubUI m_HubUI = null;    // HUD (Hub UI) 관리

	/// <summary>
	/// Awake는 가장 먼저 실행되는 MonoBehaviour 이벤트로, 초기화 작업을 수행한다.
	/// </summary>
	void Awake()
	{
		Screen.SetResolution(720, 1280, true);	// 화면 설정
		AssetMng.Inst.Initialize(); // 에셋 매니저 초기화

		GameMng.Inst.LoadFile();    // 게임 데이터 로드
		InitFSM();                  // FSM(상태 기계) 초기화
	}

	/// <summary>
	/// Start는 게임 실행 시 호출되며, 게임 매니저와 연결하고 초기 상태를 설정한다.
	/// </summary>
	void Start()
	{
		GameMng.Inst.Initialize();       // 게임 매니저 초기화
		GameMng.Inst.SetGameScene(this); // GameScene 등록
		Initialize();                    // 초기 상태(ReadyState) 설정
	}

	/// <summary>
	/// ReadyState를 초기화하여 게임 시작 준비를 완료한다.
	/// </summary>
	public void Initialize()
	{
		//m_BattleFSM.SetReadyState(); // Ready 상태로 설정
		m_BattleFSM.SetGameState();
	}

	/// <summary>
	/// BattleFSM(Finite State Machine)을 초기화한다.
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
	/// 매 프레임 업데이트 로직을 처리한다.
	/// - FSM 상태 업데이트.
	/// - 각 UI 업데이트.
	/// - GameState 상태에서 추가적인 게임 로직 업데이트.
	/// </summary>
	void Update()
	{
		if (m_BattleFSM != null)
		{
			m_BattleFSM.OnUpdate();  // FSM 상태 업데이트
			m_GameUI.OnUpdate();

			// Hub UI 업데이트
			//m_HubUI.m_HPBarUI.OnUpdate();
			//m_HubUI.m_StageUI.OnUpdate();

			// GameState 상태일 경우 추가 로직
			if (m_BattleFSM.IsGameState())
			{
				//GameMng.Inst.OnUpdate(Time.deltaTime);  // 게임 매니저 업데이트
				//m_HubUI.m_LifeTimeUI.OnUpdate();
				//m_HubUI.m_KeepTimeBarUI.OnUpdate();
			}
		}
	}
	// FSM 상태 진입 콜백 메서드
	public void Callback_ReadyEnter()
	{
		Initialize_ReadyState();
	}
	public void Callback_WaveEnter()
	{
		// Wave 상태 초기화 로직 (구현 필요)
	}
	public void Callback_GameEnter()
	{
		Initialize_GameState();
	}
	public void Callback_ResultEnter()
	{
		Initialize_ResultState();
	}

	// ReadyState 초기화 로직
	private void Initialize_ReadyState()
	{
		GameMng.Inst.InitStart();            // 게임 시작 준비
		m_HubUI.Initialize_ReadyState();     // Hub UI 초기화
		m_GameUI.Initialize_ReadyState();    // Game UI 초기화
	}

	// GameState 초기화 로직
	private void Initialize_GameState()
	{
		m_HubUI.Initialize_GameState();      // Hub UI 초기화
		m_GameUI.Initialize_GameState();     // Game UI 초기화
	}

	// ResultState 초기화 로직
	private void Initialize_ResultState()
	{
		m_HubUI.Initialize_ResultState();    // Hub UI 초기화
		m_GameUI.Initialize_ResultState();   // Game UI 초기화
	}

	/// <summary>
	/// 애플리케이션 종료 시 호출되며, 게임 데이터를 저장한다.
	/// </summary>
	private void OnApplicationQuit()
	{
		Debug.Log("[GameScene] App Quit......");
		try
		{
			GameMng.Inst.SaveFile(); // 게임 데이터 저장
		}
		catch (System.Exception e)
		{
			Debug.Log("[_Error_Quit]" + e.ToString()); // 오류 로깅
		}
	}
}
