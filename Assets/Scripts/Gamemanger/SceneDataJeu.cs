using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SceneDataJeu : MonoBehaviour
{
  static public SceneDataJeu Singleton;

  [field: SerializeField] public Transform Limit_z { get; private set; }
  [field: SerializeField] public Transform Limit_x { get; private set; }
  [field: SerializeField] public Transform Limit_center { get; private set; }
  [field: SerializeField] public Vector3 DefaultSpawn { get; private set; }

  [field: SerializeField] public TMP_InputField m_ipInput;

  [Header("UI elements")]
  [field: SerializeField] private GameObject m_mainMenuUI;
  [field: SerializeField] private GameObject m_scoreUI;
  [field: SerializeField] private Button m_boutonStart_host;
  [field: SerializeField] private Button m_boutonStart_client;

  [Header("Prefab Objects")]
  [field: SerializeField] public GameObject PlayerPrefab { get; private set; }
  [field: SerializeField] public GameObject PuckPrefab { get; private set; }

  private void Awake()
  {
    if (Singleton == null) Singleton = this;
    else Destroy(gameObject);
    DisplayOptions();
  }

  private void StartHost()
  {
    NetworkManager.Singleton.StartHost();
    HideOptions();
  }

  private void StartClient()
  {
    NetworkManager.Singleton.StartClient();
    HideOptions();
  }

  private void DisplayOptions()
  {
#if UNITY_EDITOR
    Debug.Log("Displaying options");
#endif
    NetworkPlayer.Singleton.OnPlayerDisconnected -= DisplayOptions;
    m_boutonStart_host.onClick.AddListener(StartHost);
    m_boutonStart_client.onClick.AddListener(StartClient);

    m_ipInput.onEndEdit.AddListener(OnIpFieldEdited);

    m_ipInput.gameObject.SetActive(true);
    m_mainMenuUI.SetActive(true);
  }

  private void HideOptions()
  {
#if UNITY_EDITOR
    Debug.Log("Hiding options");
#endif
    NetworkPlayer.Singleton.OnPlayerDisconnected += DisplayOptions;

    m_boutonStart_host.onClick.RemoveListener(StartHost);
    m_boutonStart_client.onClick.RemoveListener(StartClient);

    m_ipInput.onEndEdit.RemoveAllListeners();

    m_ipInput.gameObject.SetActive(false);

    m_mainMenuUI.SetActive(false);
    m_scoreUI.SetActive(true);
  }

  private void OnIpFieldEdited(string input)
  {
    NetworkPlayer.Singleton.SetIP(input);
  }
}