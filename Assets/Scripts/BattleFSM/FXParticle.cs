using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// FXParticle Ŭ������ ���� ��ƼŬ �ý����� �����ϴ� ��ƿ��Ƽ Ŭ�����̴�.
/// - ��ƼŬ�� ��� �� ������ ����.
/// - ��ƼŬ�� ����ִ� ���¸� �����ϰ�, �ڵ����� ��Ȱ��ȭ�ϰų� ����.
/// - �ɼǿ� ���� GameObject�� ��Ȱ��ȭ�ϰų� ������ ���� ����.
/// </summary>
public class FXParticle : MonoBehaviour
{
	public bool m_OnlyDeactivate = false; // true: ��ƼŬ ��Ȱ��ȭ, false: GameObject ����
	private bool m_bLoop = true;          // ��ƼŬ üũ ���� ���� ����

	/// <summary>
	/// ��ƼŬ�� Ȱ��ȭ�� �� ����Ǵ� �޼���.
	/// - IsAlive�� Ȯ���ϴ� �ڷ�ƾ�� �����Ѵ�.
	/// </summary>
	void OnEnable()
	{
		StartCoroutine(EnumFunc_CheckAlive(0.5f));
	}

	/// <summary>
	/// GameObject�� Ȱ��ȭ �Ǵ� ��Ȱ��ȭ�Ѵ�.
	/// </summary>
	/// <param name="bShow">Ȱ��ȭ ����</param>
	public void Show(bool bShow)
	{
		gameObject.SetActive(bShow);
	}

	/// <summary>
	/// ��ƼŬ�� ����Ѵ�.
	/// </summary>
	public void Play()
	{
		Stop(); // ���� ��� ���� �ʱ�ȭ
		Invoke("Callback_StartFX", 0.1f); // ������ �� ��� ����
	}

	/// <summary>
	/// ��ƼŬ ����� �����ϴ� �ݹ� �޼���.
	/// </summary>
	void Callback_StartFX()
	{
		Show(true); // GameObject�� Ȱ��ȭ
	}

	/// <summary>
	/// ��ƼŬ ����� �����Ѵ�.
	/// </summary>
	public void Stop()
	{
		m_bLoop = false; // ���� ����
		Show(false); // GameObject ��Ȱ��ȭ
	}

	/// <summary>
	/// ��ƼŬ�� ���¸� �ֱ������� üũ�ϴ� �ڷ�ƾ.
	/// - ��ƼŬ�� ��� ������ Ȯ��.
	/// - ����� ����Ǹ� GameObject�� ��Ȱ��ȭ�ϰų� ����.
	/// </summary>
	/// <param name="fDealy">üũ �ֱ�</param>
	/// <returns>IEnumerator</returns>
	IEnumerator EnumFunc_CheckAlive(float fDealy)
	{
		m_bLoop = true;

		while (m_bLoop)
		{
			yield return new WaitForSeconds(fDealy);

			// ��ƼŬ �ý����� ��� ������ Ȯ��
			if (!GetComponent<ParticleSystem>().IsAlive(true))
			{
				if (m_OnlyDeactivate)
				{
					this.gameObject.SetActive(false); // GameObject ��Ȱ��ȭ
				}
				else
				{
					GameObject.Destroy(this.gameObject); // GameObject ����
				}

				m_bLoop = false; // ���� ����
				break;
			}
		}
	}
}
