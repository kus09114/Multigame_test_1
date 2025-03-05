using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEditor.PackageManager;
using UnityEngine;

/// <summary>
/// GameInfo Ŭ������ ���� ���� ���� �� �����͸� �����Ѵ�.
/// - ���� �������� ����, �÷��̾� ����, ������ ���¸� ����.
/// - ���� �ʱ�ȭ, ���� ���, ������Ʈ ����, ������ ��� ó�� ���� ����.
/// 
/// �ֿ� ����:
/// 1. ���� ���������� �÷��̾� ���¸� �ʱ�ȭ.
/// 2. ������ �����͸� �ʱ�ȭ�ϰ� ������ ��� �� ȿ���� �ݿ�.
/// 3. ���� ��� �� ���� ���� �Ǵ�.
/// 4. ���� ���� �ð� ���� �� ���� ������Ʈ.
/// </summary>
public class GameInfo
{
	public int m_nStage = 1; // ���� ��������
	public bool m_bSuccess = false; // �������� ���� ����
	public float m_fDurationTime = 0; // ���� ������������ ����� �ð�

	public ActorInfo m_ActorInfo = new ActorInfo(); // �÷��̾� ����
	//public List<ItemInfo> m_ListItemInfo = new List<ItemInfo>(); // ������ ���� ����Ʈ
	public AssetStage m_AseetStage = new AssetStage(); // ���� �������� ������

	/// <summary>
	/// ���� ������ �ʱ�ȭ.
	/// - ������ �������� �����͸� �ε��Ͽ� �ʱ�ȭ.
	/// </summary>
	public void Initialize()
	{
		//SaveInfo kSaveInfo = GameMng.Inst.m_SaveInfo;
		//m_nStage = kSaveInfo.m_LastStage;
		//if (m_nStage <= 0)
		//	m_nStage = 1;

		//Initialize_Stage(m_nStage); // �������� �ʱ�ȭ
		//Initialize_Item(); // ������ �ʱ�ȭ
	}

	/// <summary>
	/// Ư�� �������� �����͸� �ʱ�ȭ.
	/// </summary>
	/// <param name="nStage">�������� ��ȣ</param>
	public void Initialize_Stage(int nStage)
	{
		//m_AseetStage = AssetMng.Inst.GetAssetStage(nStage);
		//m_ActorInfo.Initialize(m_AseetStage.m_nPlayerHP);
		//m_fDurationTime = 0;
	}

	/// <summary>
	/// ������ �����͸� �ʱ�ȭ.
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
	/// ���� ������ ���.
	/// </summary>
	/// <returns>���� ����</returns>
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
	/// �÷��̾�� �������� �߰�.
	/// </summary>
	public void AddDamage()
	{
		//m_ActorInfo.AddDamage(m_AseetStage.m_nBulletAttack);
	}

	/// <summary>
	/// �� ������ ������Ʈ ó��.
	/// </summary>
	/// <param name="fElasedTime">��� �ð�</param>
	public void OnUpdate(float fElasedTime)
	{
		m_fDurationTime += fElasedTime;
	}

	/// <summary>
	/// �������� ���� ���� Ȯ��.
	/// </summary>
	/// <returns>���� ����</returns>
	public bool IsSucces()
	{
		return m_bSuccess;
	}

	/// <summary>
	/// ���� �÷��̾��� HP�� ������ ��ȯ.
	/// </summary>
	/// <returns>���� HP ����</returns>
	//public float GetCurrentPlayerHP()
	//{
	//	float curHP = (float)m_ActorInfo.m_HP;
	//	int maxHP = m_ActorInfo.CalculateMaxHP();
	//	float fScore = curHP / maxHP;
	//	return fScore;
	//}

	/// <summary>
	/// ���� �������� ���� �ð��� ���.
	/// </summary>
	/// <returns>���� �ð�</returns>
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
	/// ������ ��� �� ó��.
	/// - ��: ü�� ȸ��, ��Ÿ ȿ�� ����.
	/// </summary>
	/// <param name="nItemId">������ ID</param>
	/// <returns>����� ������ ����</returns>
	//public ItemInfo ActionItem(int nItemId)
	//{
	//	ItemInfo kInfo = GetItemInfo(nItemId);
	//	if (kInfo.m_ItemType == (int)ItemInfo.Type.eHealing)
	//	{
	//		m_ActorInfo.m_HP += (int)kInfo.m_ItemValue; // ü�� ȸ��
	//	}
	//	if (kInfo.m_ItemType == (int)ItemInfo.Type.eExplose)
	//	{
	//		// ���� ȿ�� ó�� (���� ��� ����)
	//	}
	//	return kInfo;
	//}

	/// <summary>
	/// Ư�� ID�� ������ ������ �����´�.
	/// </summary>
	/// <param name="id">������ ID</param>
	/// <returns>������ ����</returns>
	//public ItemInfo GetItemInfo(int id)
	//{
	//	if (id > 0 && id <= m_ListItemInfo.Count)
	//	{
	//		return m_ListItemInfo[id - 1];
	//	}
	//	return null;
	//}
}
