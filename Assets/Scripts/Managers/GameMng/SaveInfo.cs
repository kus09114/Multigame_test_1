using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

/// <summary>
/// SaveInfo 클래스는 게임의 저장 및 로드 기능을 관리한다.
/// - 각 스테이지와 점수를 저장하고 불러오는 역할.
/// - 파일 입출력을 사용하여 데이터를 바이너리 형식으로 저장.
/// - 누적 점수, 최고 점수, 마지막 플레이한 스테이지 관리.
/// 
/// 주요 기능:
/// 1. 게임 진행 데이터를 파일에 저장 및 파일에서 로드.
/// 2. 스테이지별 점수를 관리하고, 최고 점수와 누적 점수를 갱신.
/// 3. 예외 상황에서 파일이 없거나 손상된 경우 예외 처리.
/// </summary>
public class SStage
{
	public int m_nStage = 0; // 스테이지 번호
	public int m_nScore = 0; // 해당 스테이지의 점수

	public SStage(int stage, int score)
	{
		m_nStage = stage;
		m_nScore = score;
	}
}

public class SaveInfo : MonoBehaviour
{
	public List<SStage> m_Stages = new List<SStage>(); // 각 스테이지와 점수를 저장하는 리스트

	public int m_LastStage = 1; // 마지막으로 플레이한 스테이지
	public int m_AccumulateScore = 0; // 누적 점수
	public int m_HighestScore = 0; // 최고 점수

	/// <summary>
	/// 게임 데이터를 바이너리 파일 형식으로 저장한다.
	/// </summary>
	public void SaveFila()
	{
		FileStream fs = new FileStream("stage.data", FileMode.Create, FileAccess.Write);
		BinaryWriter bw = new BinaryWriter(fs);

		// 기본 데이터 저장
		bw.Write(m_LastStage);
		bw.Write(m_AccumulateScore);
		bw.Write(m_HighestScore);

		// 스테이지 데이터 저장
		bw.Write(m_Stages.Count);
		for (int i = 0; i < m_Stages.Count; i++)
		{
			SStage kStage = m_Stages[i];
			bw.Write(kStage.m_nStage);
			bw.Write(kStage.m_nScore);
		}

		// 파일 닫기
		fs.Close();
		bw.Close();
	}

	/// <summary>
	/// 게임 데이터를 바이너리 파일 형식으로 로드한다.
	/// </summary>
	public void LoadFile()
	{
		m_Stages.Clear();
		try
		{
			FileStream fs = new FileStream("stage.data", FileMode.Open, FileAccess.Read);
			BinaryReader br = new BinaryReader(fs);

			// 기본 데이터 로드
			m_LastStage = br.ReadInt32();
			m_AccumulateScore = br.ReadInt32();
			m_HighestScore = br.ReadInt32();

			// 스테이지 데이터 로드
			int count = br.ReadInt32();
			for (int i = 0; i < count; i++)
			{
				int stage = br.ReadInt32();
				int score = br.ReadInt32();

				SStage kStage = new SStage(stage, score);
				m_Stages.Add(kStage);
			}

			// 파일 닫기
			fs.Close();
			br.Close();
		}
		catch (Exception e)
		{
			Debug.Log(e.ToString()); // 예외 발생 시 로그 출력
		}
	}

	/// <summary>
	/// 마지막으로 플레이한 스테이지를 설정한다.
	/// </summary>
	public void SetStage(int nStage)
	{
		m_LastStage = nStage;
	}

	/// <summary>
	/// 마지막 스테이지를 갱신한다.
	/// </summary>
	public void SetLastStage(int nStage)
	{
		m_LastStage = nStage;
	}

	/// <summary>
	/// 최고 점수를 갱신한다.
	/// </summary>
	public void SetHighestScore(int score)
	{
		m_HighestScore = (score > m_HighestScore) ? score : m_HighestScore;
	}

	/// <summary>
	/// 누적 점수를 추가한다.
	/// </summary>
	public void AddAccumulateScore(int score)
	{
		m_AccumulateScore += score;
	}

	/// <summary>
	/// 특정 스테이지의 점수를 설정하고 최고 점수 및 누적 점수를 갱신한다.
	/// </summary>
	public void SetStageScore(int nStage, int nScore)
	{
		SetHighestScore(nScore); // 최고 점수 갱신
		AddAccumulateScore(nScore); // 누적 점수 갱신

		if (nStage > m_Stages.Count)
		{
			// 새로운 스테이지 추가
			SStage kStage = new SStage(nStage, nScore);
			m_Stages.Add(kStage);
		}
		else
		{
			// 기존 스테이지 점수 갱신
			SStage kStage = m_Stages[nStage - 1];
			kStage.m_nScore = nScore;

			if (kStage.m_nStage != nStage)
			{
				Debug.LogError("kStage.m_nStage != nStage");
			}
		}
	}
}
