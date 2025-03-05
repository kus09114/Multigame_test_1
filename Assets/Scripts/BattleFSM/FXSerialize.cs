using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * FXSerialize 클래스는 여러 개의 파티클 시스템을 순차적으로 출력하는 기능을 제공한다.
 * - 파티클이 출력되기 전까지의 지연 시간이 랜덤하게 설정된다 (최소값 ~ 최대값 사이).
 * - 일정 유지 시간이 지나면 자동으로 파티클을 정지하고 숨기는 Auto Hide 기능이 포함된다.
 * - Auto Play 기능을 통해 Start 시 자동으로 파티클을 재생 가능.
 */

public class FXSerialize : MonoBehaviour
{
	public const int DMAX_COUNT = 1000; // 최대 파티클 카운트 (초기화용)
	[SerializeField] ParticleSystem[] m_Particles = null;   // 파티클 리스트
	[SerializeField] float m_KeepTime = 3.0f;               // 파티클 유지 시간
	[SerializeField] float m_NextPlayDelayMin = 0.1f;       // 파티클 간 출력 간격의 최소값
	[SerializeField] float m_NextPlayDelayMax = 0.3f;       // 파티클 간 출력 간격의 최대값
	[SerializeField] bool m_AutoHide = false;               // 유지 시간 이후 자동으로 숨기기
	[SerializeField] bool m_AutoPlay = false;               // 시작 시 자동 재생 여부

	private float m_DurationTime = 0.0f; // 현재 재생된 시간
	private int m_nCount = DMAX_COUNT;  // 현재 재생된 파티클 카운트

	/// <summary>
	/// Start 메서드는 MonoBehaviour의 기본 이벤트로, AutoPlay가 활성화되어 있을 경우 자동으로 재생을 시작한다.
	/// </summary>
	void Start()
	{
		if (m_AutoPlay)
			StartPlay();
	}

	/// <summary>
	/// GameObject를 활성화 또는 비활성화한다.
	/// </summary>
	/// <param name="bShow">활성화 여부</param>
	public void Show(bool bShow)
	{
		if (gameObject != null)
			gameObject.SetActive(bShow);
	}

	/// <summary>
	/// 파티클 리스트를 활성화 또는 비활성화한다.
	/// </summary>
	/// <param name="bShow">활성화 여부</param>
	public void ShowParticles(bool bShow)
	{
		for (int i = 0; i < m_Particles.Length; i++)
		{
			m_Particles[i].gameObject.SetActive(bShow);
		}
	}

	/// <summary>
	/// 파티클이 현재 재생 중인지 확인한다.
	/// </summary>
	/// <returns>재생 여부</returns>
	public bool IsPlayFX()
	{
		if (Time.time - m_DurationTime > m_KeepTime)
			return false;

		for (int i = 0; i < m_Particles.Length; i++)
		{
			if (m_Particles[i].IsAlive())
				return true;
		}
		return false;
	}

	/// <summary>
	/// 외부에서 강제로 파티클 재생을 호출한다.
	/// </summary>
	public void Play()
	{
		Stop();  // 기존 재생 중인 파티클 정지
		Show(true);
		StartPlay(); // 새로운 재생 시작
	}

	/// <summary>
	/// 파티클 재생을 시작한다.
	/// </summary>
	private void StartPlay()
	{
		m_DurationTime = Time.time;
		StartCoroutine("EnumFunc_Play");

		if (m_AutoHide)
		{
			Invoke("CBI_AutoHide", m_KeepTime);
		}
	}

	/// <summary>
	/// 파티클을 순차적으로 출력하는 코루틴.
	/// </summary>
	IEnumerator EnumFunc_Play()
	{
		float fDelay = m_NextPlayDelayMin;
		m_nCount = 0;

		while (m_nCount < m_Particles.Length)
		{
			ParticleSystem kParticle = m_Particles[m_nCount];
			kParticle.gameObject.SetActive(true); // Play On Awake가 체크되어 있다고 가정

			if (!kParticle.main.playOnAwake)
				kParticle.Play();

			m_nCount++;

			// 랜덤 딜레이 계산
			int min = (int)(m_NextPlayDelayMin * 100);
			int max = (int)(m_NextPlayDelayMax * 100);
			int value = Random.Range(min, max);
			fDelay = value * 0.01f;

			yield return new WaitForSeconds(fDelay);
		}

		yield return null;
	}

	/// <summary>
	/// AutoHide 동작을 수행하는 콜백 함수.
	/// </summary>
	void CBI_AutoHide()
	{
		if (!IsPlayFX())
		{
			Stop();
			Debug.Log("Stop Play...");
		}
	}

	/// <summary>
	/// 모든 파티클을 정지하고 비활성화한다.
	/// </summary>
	public void Stop()
	{
		for (int i = 0; i < m_Particles.Length; i++)
		{
			m_Particles[i].Stop();
			m_Particles[i].gameObject.SetActive(false);
		}
		m_nCount = DMAX_COUNT;
		Show(false);
	}
}
