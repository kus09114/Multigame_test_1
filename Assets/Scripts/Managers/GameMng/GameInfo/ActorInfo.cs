using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

/// <summary>
/// ActorInfo Ŭ������ �÷��̾� �Ǵ� ĳ������ ü�� ������ �����Ѵ�.
/// - �ִ� ü��, �߰� ü��(���� ���� ���), ���� ü���� ����.
/// - ü�� ���, ������ ó��, ü�� �߰� ���� ��� ����.
/// 
/// �ֿ� ����:
/// 1. ĳ���� �ʱ�ȭ �� ü�� ����.
/// 2. ������ ó�� �� ���� ���� ���� ������Ʈ.
/// 3. ���� ���� ��� �߰� ü�� ���.
/// 4. ���� �� �ִ� ü�� ����.
/// </summary>
public class ActorInfo
{
	public int m_HP = 0;        // ���� ü��
	public int m_MaxHP = 0;     // �⺻ �ִ� ü��
	public int m_ExtraHP = 0;   // �߰� ü�� (���� ���� ���)

	public bool IsShieldActive = false;  // ��ȣ�� ����
	public bool IsInvincible = false;    // ���� ����

	/// <summary>
	/// ActorInfo�� �ʱ�ȭ�ϰ� ü���� ����.
	/// </summary>
	/// <param name="nMaxHp">�ִ� ü��</param>
	public void Initialize(int nMaxHp)
	{
		m_MaxHP = nMaxHp;
		m_HP = nMaxHp + m_ExtraHP; // ���� ü�� �ʱ�ȭ
	}

	/// <summary>
	/// ü���� �߰��Ͽ� ȸ��.
	/// </summary>
	/// <param name="nHP">�߰��� ü��</param>
	public void AddHP(int nHP)
	{
		m_HP = Mathf.Min(m_HP + nHP, m_MaxHP); // �ִ� ü�� �ʰ� ����
	}

	/// <summary>
	/// �������� �߰��Ͽ� ü���� ����.
	/// </summary>
	/// <param name="nDamage">�޴� ������</param>
	public void AddDamage(int nDamage)
	{
		m_HP -= nDamage;
		if (m_HP <= 0)
		{
			m_HP = 0; // ü�� �ּҰ� ����
		}
	}
}
