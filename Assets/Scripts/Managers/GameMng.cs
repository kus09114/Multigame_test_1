using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// GameMng 클래스는 싱글톤(Singleton) 패턴을 기반으로 게임의 전역 데이터를 관리하는 핵심 관리 클래스이다.
/// - 게임의 초기화 및 시작 설정.
/// - GameScene과의 연결 및 접근.
/// - 게임 정보(GameInfo)와 저장 정보(SaveInfo) 관리.
/// - 매 프레임 업데이트 수행.
/// - 게임 데이터를 파일로 저장 및 로드.
/// 
/// 주요 역할:
/// 1. 게임의 전역 상태를 관리.
/// 2. GameScene에 접근하여 UI와 상태 전환 관리.
/// 3. SaveInfo를 통해 게임 데이터를 파일에 저장하거나 불러오기.
/// 4. 싱글톤 패턴을 사용하여 전역에서 접근 가능.
/// </summary>
public class GameMng
{
	// 싱글톤 인스턴스
	static GameMng _instance = null;
	public static GameMng Inst
	{
		get
		{
			// 인스턴스가 없으면 새로 생성
			if (_instance == null)
				_instance = new GameMng();

			return _instance;
		}
	}

	// 게임 정보와 저장 정보 객체
	public GameInfo m_GameInfo = new GameInfo(); // 현재 게임 상태 및 진행 정보
	//public SaveInfo m_SaveInfo = new SaveInfo(); // 게임 저장 및 로드 관련 데이터

	private GameScene m_GameScene = null; // 현재 활성화된 게임 씬

	/// <summary>
	/// 게임 매니저 초기화.
	/// - 백그라운드에서 애플리케이션을 유지하도록 설정.
	/// </summary>
	public void Initialize()
	{
		Application.runInBackground = true; // 백그라운드 실행 허용
	}

	/// <summary>
	/// 게임 시작 시 초기화 로직.
	/// - GameInfo 초기화.
	/// </summary>
	public void InitStart()
	{
		//m_GameInfo.Initialize();
	}

	/// <summary>
	/// GameScene 설정.
	/// </summary>
	/// <param name="kGameScene">현재 활성화된 GameScene 객체</param>
	public void SetGameScene(GameScene kGameScene)
	{
		m_GameScene = kGameScene;
	}

	/// <summary>
	/// 현재 GameScene을 반환.
	/// </summary>
	public GameScene GetGameScene() { return m_GameScene; }

	/// <summary>
	/// 매 프레임 게임 정보 업데이트.
	/// </summary>
	/// <param name="fElasedTime">경과 시간 (Time.deltaTime)</param>
	public void OnUpdate(float fElasedTime)
	{
		//m_GameInfo.OnUpdate(fElasedTime);
	}

	/// <summary>
	/// 게임 데이터를 저장.
	/// </summary>
	public void SaveFile()
	{
		//m_SaveInfo.SaveFila();
	}

	/// <summary>
	/// 게임 데이터를 로드.
	/// </summary>
	public void LoadFile()
	{
		//m_SaveInfo.LoadFile();
	}
}
