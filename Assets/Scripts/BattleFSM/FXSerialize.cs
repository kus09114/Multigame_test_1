using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * FXSerialize Ŭ������ ���� ���� ��ƼŬ �ý����� ���������� ����ϴ� ����� �����Ѵ�.
 * - ��ƼŬ�� ��µǱ� �������� ���� �ð��� �����ϰ� �����ȴ� (�ּҰ� ~ �ִ밪 ����).
 * - ���� ���� �ð��� ������ �ڵ����� ��ƼŬ�� �����ϰ� ����� Auto Hide ����� ���Եȴ�.
 * - Auto Play ����� ���� Start �� �ڵ����� ��ƼŬ�� ��� ����.
 */

public class FXSerialize : MonoBehaviour
{
	public const int DMAX_COUNT = 1000; // �ִ� ��ƼŬ ī��Ʈ (�ʱ�ȭ��)
	[SerializeField] ParticleSystem[] m_Particles = null;   // ��ƼŬ ����Ʈ
	[SerializeField] float m_KeepTime = 3.0f;               // ��ƼŬ ���� �ð�
	[SerializeField] float m_NextPlayDelayMin = 0.1f;       // ��ƼŬ �� ��� ������ �ּҰ�
	[SerializeField] float m_NextPlayDelayMax = 0.3f;       // ��ƼŬ �� ��� ������ �ִ밪
	[SerializeField] bool m_AutoHide = false;               // ���� �ð� ���� �ڵ����� �����
	[SerializeField] bool m_AutoPlay = false;               // ���� �� �ڵ� ��� ����

	private float m_DurationTime = 0.0f; // ���� ����� �ð�
	private int m_nCount = DMAX_COUNT;  // ���� ����� ��ƼŬ ī��Ʈ

	/// <summary>
	/// Start �޼���� MonoBehaviour�� �⺻ �̺�Ʈ��, AutoPlay�� Ȱ��ȭ�Ǿ� ���� ��� �ڵ����� ����� �����Ѵ�.
	/// </summary>
	void Start()
	{
		if (m_AutoPlay)
			StartPlay();
	}

	/// <summary>
	/// GameObject�� Ȱ��ȭ �Ǵ� ��Ȱ��ȭ�Ѵ�.
	/// </summary>
	/// <param name="bShow">Ȱ��ȭ ����</param>
	public void Show(bool bShow)
	{
		if (gameObject != null)
			gameObject.SetActive(bShow);
	}

	/// <summary>
	/// ��ƼŬ ����Ʈ�� Ȱ��ȭ �Ǵ� ��Ȱ��ȭ�Ѵ�.
	/// </summary>
	/// <param name="bShow">Ȱ��ȭ ����</param>
	public void ShowParticles(bool bShow)
	{
		for (int i = 0; i < m_Particles.Length; i++)
		{
			m_Particles[i].gameObject.SetActive(bShow);
		}
	}

	/// <summary>
	/// ��ƼŬ�� ���� ��� ������ Ȯ���Ѵ�.
	/// </summary>
	/// <returns>��� ����</returns>
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
	/// �ܺο��� ������ ��ƼŬ ����� ȣ���Ѵ�.
	/// </summary>
	public void Play()
	{
		Stop();  // ���� ��� ���� ��ƼŬ ����
		Show(true);
		StartPlay(); // ���ο� ��� ����
	}

	/// <summary>
	/// ��ƼŬ ����� �����Ѵ�.
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
	/// ��ƼŬ�� ���������� ����ϴ� �ڷ�ƾ.
	/// </summary>
	IEnumerator EnumFunc_Play()
	{
		float fDelay = m_NextPlayDelayMin;
		m_nCount = 0;

		while (m_nCount < m_Particles.Length)
		{
			ParticleSystem kParticle = m_Particles[m_nCount];
			kParticle.gameObject.SetActive(true); // Play On Awake�� üũ�Ǿ� �ִٰ� ����

			if (!kParticle.main.playOnAwake)
				kParticle.Play();

			m_nCount++;

			// ���� ������ ���
			int min = (int)(m_NextPlayDelayMin * 100);
			int max = (int)(m_NextPlayDelayMax * 100);
			int value = Random.Range(min, max);
			fDelay = value * 0.01f;

			yield return new WaitForSeconds(fDelay);
		}

		yield return null;
	}

	/// <summary>
	/// AutoHide ������ �����ϴ� �ݹ� �Լ�.
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
	/// ��� ��ƼŬ�� �����ϰ� ��Ȱ��ȭ�Ѵ�.
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
