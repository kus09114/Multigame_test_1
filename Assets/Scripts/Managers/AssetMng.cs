using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// AssetMng 클래스는 게임에서 사용할 자산(아이템, 스테이지 데이터)을 관리하는 싱글톤 클래스이다.
/// - AssetStage: 스테이지별 설정 데이터 (예: 총알 속도, 발사 딜레이, 플레이어 체력 등).
/// - AssetItem: 게임 내 아이템 데이터 (예: 아이템 타입, 프리팹 이름, 값, 설명 등).
/// 
/// 주요 기능:
/// 1. CSV 파일(item.csv, stage.csv)을 읽어 게임 자산을 초기화.
/// 2. 아이템 및 스테이지 데이터를 가져오거나 카운트를 반환.
/// 3. Singleton 패턴을 사용하여 전역 접근 제공.
/// </summary>
/// 

public class CAsset
{
	public int m_nId = 0;
}

/// <summary>
/// AssetItem은 아이템 데이터를 정의한다.
/// </summary>
public class AssetItem : CAsset
{
	public int m_nItemType = 0;     // 아이템 타입
	public string m_sPrefabName = ""; // 프리팹 이름
	public float m_fValue = 0;      // 아이템 값 (예: 공격력, 체력 증가 등)
	public string m_sDesc = "";     // 아이템 설명
	public float duration;          // 지속 시간 (0이면 즉시 발동)
}

/// <summary>
/// AssetStage는 스테이지 데이터를 정의한다.
/// </summary>
public class AssetStages : CAsset
{
	public float m_fFireDelayTime = 0;   // 총알 발사 지연시간
	public float m_fBulletSpeed = 0;     // 총알 속도
	public float m_fStageKeepTime = 0;   // 스테이지 유지시간
	public int m_nPlayerHP = 0;          // 플레이어 체력
	public int m_nBulletAttack = 0;      // 총알 공격력
	public float m_fItemAppearDelay = 0; // 아이템 생성 지연 시간
	public float m_fItemKeepTime = 0;    // 아이템 화면 유지 시간
	public int m_nTurretCount = 0;       // 터렛 개수
}

/// <summary>
/// AssetMng 클래스는 자산 데이터 초기화 및 관리 역할을 한다.
/// </summary>
public class AssetMng
{
	// Singleton 패턴 구현
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
	// 초기화 상태를 확인하는 속성
	public bool IsInstalled { get; set; }
	public List<AssetStages> m_AssetStages = new List<AssetStages>(); // 스테이지 데이터 목록
	public List<AssetItem> m_AssetItems = new List<AssetItem>();   // 아이템 데이터 목록

	/// <summary>
	/// 자산 데이터를 초기화한다.
	/// </summary>
	public void Initialize()
	{
		IsInstalled = true;
		Initialize_Item(); // 아이템 데이터 초기화
		Initialize_Stage(); // 스테이지 데이터 초기화
	}

	/// <summary>
	/// 아이템 데이터를 CSV 파일에서 로드하여 초기화한다.
	/// </summary>
	public void Initialize_Item()
	{
		//List<string[]> dataList = CSVParser.Load2("Assets/Resources/TableData/item.csv");

		//for (int i = 1; i < dataList.Count; i++) // 첫 번째 줄은 헤더로 간주
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
	/// 스테이지 데이터를 CSV 파일에서 로드하여 초기화한다.
	/// </summary>
	public void Initialize_Stage()
	{
		//List<string[]> dataList = CSVParser.Load2("Assets/Resources/TableData/stage.csv");

		//for (int i = 1; i < dataList.Count; i++) // 첫 번째 줄은 헤더로 간주
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
	/// 스테이지 데이터를 ID로 가져온다.
	/// </summary>
	/// <param name="nStage">스테이지 ID</param>
	/// <returns>해당 스테이지 데이터</returns>
	public AssetStages GetAssetStage(int nStage)
	{
		if (nStage > 0 && nStage <= m_AssetStages.Count)
		{
			return m_AssetStages[nStage - 1];
		}
		return null;
	}

	/// <summary>
	/// 아이템 데이터를 ID로 가져온다.
	/// </summary>
	/// <param name="id">아이템 ID</param>
	/// <returns>해당 아이템 데이터</returns>
	public AssetItem GetAssetItem(int id)
	{
		if (id > 0 && id <= m_AssetItems.Count)
		{
			return m_AssetItems[id - 1];
		}
		return null;
	}

	/// <summary>
	/// 아이템 데이터의 총 개수를 반환한다.
	/// </summary>
	/// <returns>아이템 개수</returns>
	public int GetAssetItemCount()
	{
		return m_AssetItems.Count;
	}
}
