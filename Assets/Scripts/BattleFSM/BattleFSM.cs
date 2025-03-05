using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// BattleFSM (Finite State Machine) 클래스는 게임의 상태 전환을 관리하는 시스템이다.
/// 상태 전환은 Ready, Wave, Game, Result 네 가지로 구성된다.
/// 각 상태는 특정 진입, 업데이트, 종료 로직을 가지며, DelegateFunc를 통해 상태마다 실행할 수 있는 커스텀 동작을 추가할 수 있다.
/// 상태 전환의 흐름은 다음과 같다:
/// 1. 새로운 상태가 설정되면 현재 상태를 종료(OnExit)하고 새로운 상태로 변경.
/// 2. 새로운 상태의 초기화(OnEnter)를 호출.
/// 3. 현재 상태를 업데이트(OnUpdate).
/// 이 코드는 주로 전투나 웨이브 기반 게임의 상태 전환 로직에 적합하다.
/// </summary>
public class BattleFSM
{
	public delegate void DelegateFunc();

	// 상태를 정의하는 기본 클래스
	public class CState
	{
		public DelegateFunc m_OnEnterFunc = null;

		// 상태의 초기화 함수
		public virtual void Initialize(DelegateFunc func)
		{
			m_OnEnterFunc = new DelegateFunc(func);
		}

		// 상태 진입 시 호출
		public virtual void OnEnter() { }

		// 상태 업데이트 시 호출
		public virtual void OnUpdate() { }

		// 상태 종료 시 호출
		public virtual void OnExit() { }
	}

	// Ready 상태 클래스
	public class CReadyState : CState
	{
		public override void OnEnter()
		{
			// 상태 진입 시 DelegateFunc 실행
			if (m_OnEnterFunc != null)
				m_OnEnterFunc();
		}
	}

	// Wave 상태 클래스
	public class CWaveState : CState
	{
		public override void OnEnter()
		{
			if (m_OnEnterFunc != null)
				m_OnEnterFunc();
		}
	}

	// Game 상태 클래스
	public class CGameState : CState
	{
		public override void OnEnter()
		{
			if (m_OnEnterFunc != null)
				m_OnEnterFunc();
		}
	}

	// Result 상태 클래스
	public class CResultState : CState
	{
		public override void OnEnter()
		{
			if (m_OnEnterFunc != null)
				m_OnEnterFunc();
		}
	}

	private CState m_curState = null;  // 현재 상태
	private CState m_newState = null;  // 새로운 상태

	// 네 가지 상태의 인스턴스 생성
	private CState m_Ready = new CResultState();
	private CState m_Wave = new CWaveState();
	private CState m_Game = new CGameState();
	private CState m_Result = new CResultState();

	/// <summary>
	/// 각 상태를 초기화하며, DelegateFunc를 매핑한다.
	/// </summary>
	public void Initialize(DelegateFunc kReady, DelegateFunc kWave, DelegateFunc kGame, DelegateFunc kResult)
	{
		m_Ready.Initialize(kReady);
		m_Wave.Initialize(kWave);
		m_Game.Initialize(kGame);
		m_Result.Initialize(kResult);
	}

	/// <summary>
	/// 새로운 상태를 설정한다.
	/// </summary>
	public void SetState(CState kState)
	{
		m_newState = kState;
	}

	/// <summary>
	/// 상태 업데이트 로직:
	/// - 새로운 상태가 있으면 현재 상태를 종료하고 새 상태로 전환.
	/// - 새로운 상태의 OnEnter() 호출.
	/// - 현재 상태의 OnUpdate() 호출.
	/// </summary>
	public void OnUpdate()
	{
		if (m_newState != null)
		{
			if (m_curState != null)
			{
				m_curState.OnExit();
			}
			m_curState = m_newState;
			m_newState = null;
			m_curState.OnEnter();
		}

		if (m_curState != null)
		{
			m_curState.OnUpdate();
		}
	}

	// 상태 전환 메서드
	public void SetReadyState() => SetState(m_Ready);
	public void SetWaveState() => SetState(m_Wave);
	public void SetGameState() => SetState(m_Game);
	public void SetResultState() => SetState(m_Result);

	/// <summary>
	/// 현재 상태와 비교하여 동일 여부를 반환.
	/// </summary>
	public bool IsCurState(CState kState)
	{
		if (m_curState == null)
			return false;

		return (m_curState == kState);
	}

	/// <summary>
	/// 현재 상태를 반환.
	/// </summary>
	public CState GetCurState() => m_curState;

	/// <summary>
	/// 상태를 초기화(없음 상태)로 설정.
	/// </summary>
	public void SetNoneState()
	{
		m_newState = null;
		m_curState = null;
	}

	// 특정 상태 확인 메서드
	public bool IsGameState() => (m_curState == m_Game);
	public bool IsResultState() => (m_curState == m_Result);
	public bool IsNoneState() => (m_curState == null);
}
