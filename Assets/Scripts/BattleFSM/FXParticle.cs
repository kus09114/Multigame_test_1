using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// FXParticle 클래스는 개별 파티클 시스템을 관리하는 유틸리티 클래스이다.
/// - 파티클의 재생 및 정지를 제어.
/// - 파티클이 살아있는 상태를 감지하고, 자동으로 비활성화하거나 삭제.
/// - 옵션에 따라 GameObject를 비활성화하거나 완전히 제거 가능.
/// </summary>
public class FXParticle : MonoBehaviour
{
	public bool m_OnlyDeactivate = false; // true: 파티클 비활성화, false: GameObject 제거
	private bool m_bLoop = true;          // 파티클 체크 루프 실행 여부

	/// <summary>
	/// 파티클이 활성화될 때 실행되는 메서드.
	/// - IsAlive를 확인하는 코루틴을 시작한다.
	/// </summary>
	void OnEnable()
	{
		StartCoroutine(EnumFunc_CheckAlive(0.5f));
	}

	/// <summary>
	/// GameObject를 활성화 또는 비활성화한다.
	/// </summary>
	/// <param name="bShow">활성화 여부</param>
	public void Show(bool bShow)
	{
		gameObject.SetActive(bShow);
	}

	/// <summary>
	/// 파티클을 재생한다.
	/// </summary>
	public void Play()
	{
		Stop(); // 기존 재생 상태 초기화
		Invoke("Callback_StartFX", 0.1f); // 딜레이 후 재생 시작
	}

	/// <summary>
	/// 파티클 재생을 시작하는 콜백 메서드.
	/// </summary>
	void Callback_StartFX()
	{
		Show(true); // GameObject를 활성화
	}

	/// <summary>
	/// 파티클 재생을 중지한다.
	/// </summary>
	public void Stop()
	{
		m_bLoop = false; // 루프 종료
		Show(false); // GameObject 비활성화
	}

	/// <summary>
	/// 파티클의 상태를 주기적으로 체크하는 코루틴.
	/// - 파티클이 재생 중인지 확인.
	/// - 재생이 종료되면 GameObject를 비활성화하거나 삭제.
	/// </summary>
	/// <param name="fDealy">체크 주기</param>
	/// <returns>IEnumerator</returns>
	IEnumerator EnumFunc_CheckAlive(float fDealy)
	{
		m_bLoop = true;

		while (m_bLoop)
		{
			yield return new WaitForSeconds(fDealy);

			// 파티클 시스템이 재생 중인지 확인
			if (!GetComponent<ParticleSystem>().IsAlive(true))
			{
				if (m_OnlyDeactivate)
				{
					this.gameObject.SetActive(false); // GameObject 비활성화
				}
				else
				{
					GameObject.Destroy(this.gameObject); // GameObject 제거
				}

				m_bLoop = false; // 루프 종료
				break;
			}
		}
	}
}
