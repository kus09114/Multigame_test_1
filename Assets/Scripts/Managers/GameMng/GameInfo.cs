using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEditor.PackageManager;
using UnityEngine;

/// <summary>
/// GameInfo 클래스는 현재 게임 상태 및 데이터를 관리한다.
/// - 현재 스테이지 정보, 플레이어 상태, 아이템 상태를 포함.
/// - 게임 초기화, 점수 계산, 업데이트 로직, 아이템 사용 처리 등을 수행.
/// 
/// 주요 역할:
/// 1. 현재 스테이지와 플레이어 상태를 초기화.
/// 2. 아이템 데이터를 초기화하고 아이템 사용 시 효과를 반영.
/// 3. 점수 계산 및 성공 여부 판단.
/// 4. 게임 진행 시간 관리 및 상태 업데이트.
/// </summary>
public class GameInfo
{
	public int m_nStage = 1; // 현재 스테이지
	public bool m_bSuccess = false; // 스테이지 성공 여부
	public float m_fDurationTime = 0; // 현재 스테이지에서 경과된 시간

	public ActorInfo m_ActorInfo = new ActorInfo(); // 플레이어 정보
	//public List<ItemInfo> m_ListItemInfo = new List<ItemInfo>(); // 아이템 정보 리스트
	public AssetStage m_AseetStage = new AssetStage(); // 현재 스테이지 데이터

	/// <summary>
	/// 게임 정보를 초기화.
	/// - 마지막 스테이지 데이터를 로드하여 초기화.
	/// </summary>
	public void Initialize()
	{
		//SaveInfo kSaveInfo = GameMng.Inst.m_SaveInfo;
		//m_nStage = kSaveInfo.m_LastStage;
		//if (m_nStage <= 0)
		//	m_nStage = 1;

		//Initialize_Stage(m_nStage); // 스테이지 초기화
		//Initialize_Item(); // 아이템 초기화
	}

	/// <summary>
	/// 특정 스테이지 데이터를 초기화.
	/// </summary>
	/// <param name="nStage">스테이지 번호</param>
	public void Initialize_Stage(int nStage)
	{
		//m_AseetStage = AssetMng.Inst.GetAssetStage(nStage);
		//m_ActorInfo.Initialize(m_AseetStage.m_nPlayerHP);
		//m_fDurationTime = 0;
	}

	/// <summary>
	/// 아이템 데이터를 초기화.
	/// </summary>
	public void Initialize_Item()
	{
		//for (int i = 0; i < AssetMng.Inst.m_AssetItems.Count; i++)
		//{
		//	AssetItem kAss = AssetMng.Inst.m_AssetItems[i];
		//	ItemInfo kInfo = new ItemInfo();
		//	kInfo.Initialize(kAss.m_nItemType, kAss.m_fValue);
		//	m_ListItemInfo.Add(kInfo);
		//}
	}

	/// <summary>
	/// 현재 점수를 계산.
	/// </summary>
	/// <returns>계산된 점수</returns>
	//public int CaluclateScore()
	//{
	//	int curHP = m_ActorInfo.m_HP;
	//	int maxHP = m_ActorInfo.CalculateMaxHP();
	//	int nScore = (int)(((float)curHP / maxHP) * Config.DMAX_SCORE);
	//	if (nScore < Config.DMIN_SCORE)
	//		nScore = Config.DMIN_SCORE;
	//	return nScore;
	//}

	/// <summary>
	/// 플레이어에게 데미지를 추가.
	/// </summary>
	public void AddDamage()
	{
		//m_ActorInfo.AddDamage(m_AseetStage.m_nBulletAttack);
	}

	/// <summary>
	/// 매 프레임 업데이트 처리.
	/// </summary>
	/// <param name="fElasedTime">경과 시간</param>
	public void OnUpdate(float fElasedTime)
	{
		m_fDurationTime += fElasedTime;
	}

	/// <summary>
	/// 스테이지 성공 여부 확인.
	/// </summary>
	/// <returns>성공 여부</returns>
	public bool IsSucces()
	{
		return m_bSuccess;
	}

	/// <summary>
	/// 현재 플레이어의 HP를 비율로 반환.
	/// </summary>
	/// <returns>현재 HP 비율</returns>
	//public float GetCurrentPlayerHP()
	//{
	//	float curHP = (float)m_ActorInfo.m_HP;
	//	int maxHP = m_ActorInfo.CalculateMaxHP();
	//	float fScore = curHP / maxHP;
	//	return fScore;
	//}

	/// <summary>
	/// 현재 스테이지 남은 시간을 계산.
	/// </summary>
	/// <returns>남은 시간</returns>
	//public float GetCurrentKeepTime()
	//{
	//	float curtime = m_AseetStage.m_fStageKeepTime - m_fDurationTime;
	//	if (curtime <= 0)
	//	{
	//		curtime = 0;
	//		m_bSuccess = true;
	//	}
	//	return curtime;
	//}

	/// <summary>
	/// 아이템 사용 시 처리.
	/// - 예: 체력 회복, 기타 효과 적용.
	/// </summary>
	/// <param name="nItemId">아이템 ID</param>
	/// <returns>사용한 아이템 정보</returns>
	//public ItemInfo ActionItem(int nItemId)
	//{
	//	ItemInfo kInfo = GetItemInfo(nItemId);
	//	if (kInfo.m_ItemType == (int)ItemInfo.Type.eHealing)
	//	{
	//		m_ActorInfo.m_HP += (int)kInfo.m_ItemValue; // 체력 회복
	//	}
	//	if (kInfo.m_ItemType == (int)ItemInfo.Type.eExplose)
	//	{
	//		// 폭발 효과 처리 (현재 비어 있음)
	//	}
	//	return kInfo;
	//}

	/// <summary>
	/// 특정 ID의 아이템 정보를 가져온다.
	/// </summary>
	/// <param name="id">아이템 ID</param>
	/// <returns>아이템 정보</returns>
	//public ItemInfo GetItemInfo(int id)
	//{
	//	if (id > 0 && id <= m_ListItemInfo.Count)
	//	{
	//		return m_ListItemInfo[id - 1];
	//	}
	//	return null;
	//}
}
