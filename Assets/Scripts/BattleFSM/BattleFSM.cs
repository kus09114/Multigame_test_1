using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// BattleFSM (Finite State Machine) Ŭ������ ������ ���� ��ȯ�� �����ϴ� �ý����̴�.
/// ���� ��ȯ�� Ready, Wave, Game, Result �� ������ �����ȴ�.
/// �� ���´� Ư�� ����, ������Ʈ, ���� ������ ������, DelegateFunc�� ���� ���¸��� ������ �� �ִ� Ŀ���� ������ �߰��� �� �ִ�.
/// ���� ��ȯ�� �帧�� ������ ����:
/// 1. ���ο� ���°� �����Ǹ� ���� ���¸� ����(OnExit)�ϰ� ���ο� ���·� ����.
/// 2. ���ο� ������ �ʱ�ȭ(OnEnter)�� ȣ��.
/// 3. ���� ���¸� ������Ʈ(OnUpdate).
/// �� �ڵ�� �ַ� ������ ���̺� ��� ������ ���� ��ȯ ������ �����ϴ�.
/// </summary>
public class BattleFSM
{
	public delegate void DelegateFunc();

	// ���¸� �����ϴ� �⺻ Ŭ����
	public class CState
	{
		public DelegateFunc m_OnEnterFunc = null;

		// ������ �ʱ�ȭ �Լ�
		public virtual void Initialize(DelegateFunc func)
		{
			m_OnEnterFunc = new DelegateFunc(func);
		}

		// ���� ���� �� ȣ��
		public virtual void OnEnter() { }

		// ���� ������Ʈ �� ȣ��
		public virtual void OnUpdate() { }

		// ���� ���� �� ȣ��
		public virtual void OnExit() { }
	}

	// Ready ���� Ŭ����
	public class CReadyState : CState
	{
		public override void OnEnter()
		{
			// ���� ���� �� DelegateFunc ����
			if (m_OnEnterFunc != null)
				m_OnEnterFunc();
		}
	}

	// Wave ���� Ŭ����
	public class CWaveState : CState
	{
		public override void OnEnter()
		{
			if (m_OnEnterFunc != null)
				m_OnEnterFunc();
		}
	}

	// Game ���� Ŭ����
	public class CGameState : CState
	{
		public override void OnEnter()
		{
			if (m_OnEnterFunc != null)
				m_OnEnterFunc();
		}
	}

	// Result ���� Ŭ����
	public class CResultState : CState
	{
		public override void OnEnter()
		{
			if (m_OnEnterFunc != null)
				m_OnEnterFunc();
		}
	}

	private CState m_curState = null;  // ���� ����
	private CState m_newState = null;  // ���ο� ����

	// �� ���� ������ �ν��Ͻ� ����
	private CState m_Ready = new CResultState();
	private CState m_Wave = new CWaveState();
	private CState m_Game = new CGameState();
	private CState m_Result = new CResultState();

	/// <summary>
	/// �� ���¸� �ʱ�ȭ�ϸ�, DelegateFunc�� �����Ѵ�.
	/// </summary>
	public void Initialize(DelegateFunc kReady, DelegateFunc kWave, DelegateFunc kGame, DelegateFunc kResult)
	{
		m_Ready.Initialize(kReady);
		m_Wave.Initialize(kWave);
		m_Game.Initialize(kGame);
		m_Result.Initialize(kResult);
	}

	/// <summary>
	/// ���ο� ���¸� �����Ѵ�.
	/// </summary>
	public void SetState(CState kState)
	{
		m_newState = kState;
	}

	/// <summary>
	/// ���� ������Ʈ ����:
	/// - ���ο� ���°� ������ ���� ���¸� �����ϰ� �� ���·� ��ȯ.
	/// - ���ο� ������ OnEnter() ȣ��.
	/// - ���� ������ OnUpdate() ȣ��.
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

	// ���� ��ȯ �޼���
	public void SetReadyState() => SetState(m_Ready);
	public void SetWaveState() => SetState(m_Wave);
	public void SetGameState() => SetState(m_Game);
	public void SetResultState() => SetState(m_Result);

	/// <summary>
	/// ���� ���¿� ���Ͽ� ���� ���θ� ��ȯ.
	/// </summary>
	public bool IsCurState(CState kState)
	{
		if (m_curState == null)
			return false;

		return (m_curState == kState);
	}

	/// <summary>
	/// ���� ���¸� ��ȯ.
	/// </summary>
	public CState GetCurState() => m_curState;

	/// <summary>
	/// ���¸� �ʱ�ȭ(���� ����)�� ����.
	/// </summary>
	public void SetNoneState()
	{
		m_newState = null;
		m_curState = null;
	}

	// Ư�� ���� Ȯ�� �޼���
	public bool IsGameState() => (m_curState == m_Game);
	public bool IsResultState() => (m_curState == m_Result);
	public bool IsNoneState() => (m_curState == null);
}
