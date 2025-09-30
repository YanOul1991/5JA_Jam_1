using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PowerupUiObject : MonoBehaviour
{
  private static readonly float s_uiUpdateRate = 30.0f;

  public bool IsActive { get; private set; }

  [field: SerializeField] private GameObject m_timeFill;
  [field: SerializeField] private GameObject m_icon;

  public void Initialize(Sprite icon)
  {
    IsActive = false;
    m_icon.GetComponent<Image>().sprite = icon;
  }

  public void StartTimer(float time)
  {
    if (IsActive) return;
    StartCoroutine(RoutineUpdateTimer(time));
  }
  
  private IEnumerator RoutineUpdateTimer(float time)
  {
    float steps = 1 / (time * s_uiUpdateRate);
    float framerate = 1 / s_uiUpdateRate;
    Image _img = m_timeFill.GetComponent<Image>();

    IsActive = true;
    _img.fillAmount = 1.0f;
    
    while (_img.fillAmount > 0)
    {
      m_timeFill.GetComponent<Image>().fillAmount -= steps;
      yield return new WaitForSeconds(framerate);
    }

    IsActive = false;
    gameObject.SetActive(false);
    yield break;

  }  
}
