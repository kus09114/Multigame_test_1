using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// GameMng Ŭ������ �̱���(Singleton) ������ ������� ������ ���� �����͸� �����ϴ� �ٽ� ���� Ŭ�����̴�.
/// - ������ �ʱ�ȭ �� ���� ����.
/// - GameScene���� ���� �� ����.
/// - ���� ����(GameInfo)�� ���� ����(SaveInfo) ����.
/// - �� ������ ������Ʈ ����.
/// - ���� �����͸� ���Ϸ� ���� �� �ε�.
/// 
/// �ֿ� ����:
/// 1. ������ ���� ���¸� ����.
/// 2. GameScene�� �����Ͽ� UI�� ���� ��ȯ ����.
/// 3. SaveInfo�� ���� ���� �����͸� ���Ͽ� �����ϰų� �ҷ�����.
/// 4. �̱��� ������ ����Ͽ� �������� ���� ����.
/// </summary>
public class GameMng
{
	// �̱��� �ν��Ͻ�
	static GameMng _instance = null;
	public static GameMng Inst
	{
		get
		{
			// �ν��Ͻ��� ������ ���� ����
			if (_instance == null)
				_instance = new GameMng();

			return _instance;
		}
	}

	// ���� ������ ���� ���� ��ü
	public GameInfo m_GameInfo = new GameInfo(); // ���� ���� ���� �� ���� ����
	//public SaveInfo m_SaveInfo = new SaveInfo(); // ���� ���� �� �ε� ���� ������

	private GameScene m_GameScene = null; // ���� Ȱ��ȭ�� ���� ��

	/// <summary>
	/// ���� �Ŵ��� �ʱ�ȭ.
	/// - ��׶��忡�� ���ø����̼��� �����ϵ��� ����.
	/// </summary>
	public void Initialize()
	{
		Application.runInBackground = true; // ��׶��� ���� ���
	}

	/// <summary>
	/// ���� ���� �� �ʱ�ȭ ����.
	/// - GameInfo �ʱ�ȭ.
	/// </summary>
	public void InitStart()
	{
		//m_GameInfo.Initialize();
	}

	/// <summary>
	/// GameScene ����.
	/// </summary>
	/// <param name="kGameScene">���� Ȱ��ȭ�� GameScene ��ü</param>
	public void SetGameScene(GameScene kGameScene)
	{
		m_GameScene = kGameScene;
	}

	/// <summary>
	/// ���� GameScene�� ��ȯ.
	/// </summary>
	public GameScene GetGameScene() { return m_GameScene; }

	/// <summary>
	/// �� ������ ���� ���� ������Ʈ.
	/// </summary>
	/// <param name="fElasedTime">��� �ð� (Time.deltaTime)</param>
	public void OnUpdate(float fElasedTime)
	{
		//m_GameInfo.OnUpdate(fElasedTime);
	}

	/// <summary>
	/// ���� �����͸� ����.
	/// </summary>
	public void SaveFile()
	{
		//m_SaveInfo.SaveFila();
	}

	/// <summary>
	/// ���� �����͸� �ε�.
	/// </summary>
	public void LoadFile()
	{
		//m_SaveInfo.LoadFile();
	}
}
