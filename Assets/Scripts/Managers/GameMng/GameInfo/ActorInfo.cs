using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

/// <summary>
/// ActorInfo 클래스는 플레이어 또는 캐릭터의 체력 정보를 관리한다.
/// - 최대 체력, 추가 체력(누적 점수 기반), 현재 체력을 포함.
/// - 체력 계산, 데미지 처리, 체력 추가 등의 기능 제공.
/// 
/// 주요 역할:
/// 1. 캐릭터 초기화 및 체력 설정.
/// 2. 데미지 처리 및 게임 성공 여부 업데이트.
/// 3. 누적 점수 기반 추가 체력 계산.
/// 4. 현재 및 최대 체력 관리.
/// </summary>
public class ActorInfo
{
	public int m_HP = 0;        // 현재 체력
	public int m_MaxHP = 0;     // 기본 최대 체력
	public int m_ExtraHP = 0;   // 추가 체력 (누적 점수 기반)

	public bool IsShieldActive = false;  // 보호막 상태
	public bool IsInvincible = false;    // 무적 상태

	/// <summary>
	/// ActorInfo를 초기화하고 체력을 설정.
	/// </summary>
	/// <param name="nMaxHp">최대 체력</param>
	public void Initialize(int nMaxHp)
	{
		m_MaxHP = nMaxHp;
		m_HP = nMaxHp + m_ExtraHP; // 현재 체력 초기화
	}

	/// <summary>
	/// 체력을 추가하여 회복.
	/// </summary>
	/// <param name="nHP">추가할 체력</param>
	public void AddHP(int nHP)
	{
		m_HP = Mathf.Min(m_HP + nHP, m_MaxHP); // 최대 체력 초과 방지
	}

	/// <summary>
	/// 데미지를 추가하여 체력을 감소.
	/// </summary>
	/// <param name="nDamage">받는 데미지</param>
	public void AddDamage(int nDamage)
	{
		m_HP -= nDamage;
		if (m_HP <= 0)
		{
			m_HP = 0; // 체력 최소값 유지
		}
	}
}
