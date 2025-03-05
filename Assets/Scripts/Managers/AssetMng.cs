using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// AssetMng Ŭ������ ���ӿ��� ����� �ڻ�(������, �������� ������)�� �����ϴ� �̱��� Ŭ�����̴�.
/// - AssetStage: ���������� ���� ������ (��: �Ѿ� �ӵ�, �߻� ������, �÷��̾� ü�� ��).
/// - AssetItem: ���� �� ������ ������ (��: ������ Ÿ��, ������ �̸�, ��, ���� ��).
/// 
/// �ֿ� ���:
/// 1. CSV ����(item.csv, stage.csv)�� �о� ���� �ڻ��� �ʱ�ȭ.
/// 2. ������ �� �������� �����͸� �������ų� ī��Ʈ�� ��ȯ.
/// 3. Singleton ������ ����Ͽ� ���� ���� ����.
/// </summary>
/// 

public class CAsset
{
	public int m_nId = 0;
}

/// <summary>
/// AssetItem�� ������ �����͸� �����Ѵ�.
/// </summary>
public class AssetItem : CAsset
{
	public int m_nItemType = 0;     // ������ Ÿ��
	public string m_sPrefabName = ""; // ������ �̸�
	public float m_fValue = 0;      // ������ �� (��: ���ݷ�, ü�� ���� ��)
	public string m_sDesc = "";     // ������ ����
	public float duration;          // ���� �ð� (0�̸� ��� �ߵ�)
}

/// <summary>
/// AssetStage�� �������� �����͸� �����Ѵ�.
/// </summary>
public class AssetStages : CAsset
{
	public float m_fFireDelayTime = 0;   // �Ѿ� �߻� �����ð�
	public float m_fBulletSpeed = 0;     // �Ѿ� �ӵ�
	public float m_fStageKeepTime = 0;   // �������� �����ð�
	public int m_nPlayerHP = 0;          // �÷��̾� ü��
	public int m_nBulletAttack = 0;      // �Ѿ� ���ݷ�
	public float m_fItemAppearDelay = 0; // ������ ���� ���� �ð�
	public float m_fItemKeepTime = 0;    // ������ ȭ�� ���� �ð�
	public int m_nTurretCount = 0;       // �ͷ� ����
}

/// <summary>
/// AssetMng Ŭ������ �ڻ� ������ �ʱ�ȭ �� ���� ������ �Ѵ�.
/// </summary>
public class AssetMng
{
	// Singleton ���� ����
	static AssetMng _instance = null;
	public static AssetMng Inst
	{
		get
		{
			if (_instance == null)
				_instance = new AssetMng();

			return _instance;
		}
	}

	private AssetMng()
	{
		IsInstalled = false;
	}
	//------------------------------------------------------
	// �ʱ�ȭ ���¸� Ȯ���ϴ� �Ӽ�
	public bool IsInstalled { get; set; }
	public List<AssetStages> m_AssetStages = new List<AssetStages>(); // �������� ������ ���
	public List<AssetItem> m_AssetItems = new List<AssetItem>();   // ������ ������ ���

	/// <summary>
	/// �ڻ� �����͸� �ʱ�ȭ�Ѵ�.
	/// </summary>
	public void Initialize()
	{
		IsInstalled = true;
		Initialize_Item(); // ������ ������ �ʱ�ȭ
		Initialize_Stage(); // �������� ������ �ʱ�ȭ
	}

	/// <summary>
	/// ������ �����͸� CSV ���Ͽ��� �ε��Ͽ� �ʱ�ȭ�Ѵ�.
	/// </summary>
	public void Initialize_Item()
	{
		//List<string[]> dataList = CSVParser.Load2("Assets/Resources/TableData/item.csv");

		//for (int i = 1; i < dataList.Count; i++) // ù ��° ���� ����� ����
		//{
		//	string[] data = dataList[i];

		//	AssetItem item = new AssetItem();
		//	item.m_nId = int.Parse(data[0]);
		//	item.m_nItemType = int.Parse(data[1]);
		//	item.m_sPrefabName = data[2];
		//	item.m_fValue = float.Parse(data[3]);
		//	item.m_sDesc = data[4];

		//	m_AssetItems.Add(item);
		//}
	}

	/// <summary>
	/// �������� �����͸� CSV ���Ͽ��� �ε��Ͽ� �ʱ�ȭ�Ѵ�.
	/// </summary>
	public void Initialize_Stage()
	{
		//List<string[]> dataList = CSVParser.Load2("Assets/Resources/TableData/stage.csv");

		//for (int i = 1; i < dataList.Count; i++) // ù ��° ���� ����� ����
		//{
		//	string[] data = dataList[i];

		//	AssetStages stage = new AssetStages();
		//	stage.m_nId = int.Parse(data[0]);
		//	stage.m_fFireDelayTime = float.Parse(data[1]);
		//	stage.m_fBulletSpeed = float.Parse(data[2]);
		//	stage.m_fStageKeepTime = float.Parse(data[3]);
		//	stage.m_nPlayerHP = int.Parse(data[4]);
		//	stage.m_nBulletAttack = int.Parse(data[5]);
		//	stage.m_fItemAppearDelay = float.Parse(data[6]);
		//	stage.m_fItemKeepTime = float.Parse(data[7]);
		//	stage.m_nTurretCount = int.Parse(data[8]);

		//	m_AssetStages.Add(stage);
		//}
	}

	/// <summary>
	/// �������� �����͸� ID�� �����´�.
	/// </summary>
	/// <param name="nStage">�������� ID</param>
	/// <returns>�ش� �������� ������</returns>
	public AssetStages GetAssetStage(int nStage)
	{
		if (nStage > 0 && nStage <= m_AssetStages.Count)
		{
			return m_AssetStages[nStage - 1];
		}
		return null;
	}

	/// <summary>
	/// ������ �����͸� ID�� �����´�.
	/// </summary>
	/// <param name="id">������ ID</param>
	/// <returns>�ش� ������ ������</returns>
	public AssetItem GetAssetItem(int id)
	{
		if (id > 0 && id <= m_AssetItems.Count)
		{
			return m_AssetItems[id - 1];
		}
		return null;
	}

	/// <summary>
	/// ������ �������� �� ������ ��ȯ�Ѵ�.
	/// </summary>
	/// <returns>������ ����</returns>
	public int GetAssetItemCount()
	{
		return m_AssetItems.Count;
	}
}
