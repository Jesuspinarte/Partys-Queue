using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : GAWGameManager
{
  // Serialized vars
  [Header("Reference Settings")]
  [SerializeField] private TMP_Text suspectNameText;
  [SerializeField] private TMP_Text currentScoreText;
  [SerializeField] private TMP_Text shapeDescriptionText;
  [SerializeField] private TMP_Text colorDescriptionText;

  [Header("Spawn Settings")]
  [SerializeField] private List<SuspectProfile> suspectList = new List<SuspectProfile>();
  [SerializeField] private Transform spawnPoint;

  // Private vars
  private SuspectProfile _currentSuspect;
  private GameObject _currentSuspectInstance;
  private bool _isLying = false;
  private int _currentScore = 0;
  private int _goalScore = 5;

  // Game Managers Hooks
  public override void OnGameLoad()
  {
    UpdateScore();
  }

  public override void OnGameStart()
  {

  }

  public override void OnGameSucceeded()
  {

  }

  public override void OnGameFailed()
  {

  }

  // Unity Hooks
  private void Update()
  {
    if (Input.GetKeyDown("A")) OnAllow();
    if (Input.GetKeyDown("D")) OnDeny();
  }

  // Private
  private void GenerateSuspect()
  {
    if (_currentSuspectInstance != null) Destroy(_currentSuspectInstance);

    _currentSuspect = suspectList[Random.Range(0, suspectList.Count)];
    _currentSuspectInstance = Instantiate(_currentSuspect.prefab, spawnPoint.position, Quaternion.identity);

    if (_currentSuspect.suspectShape != _currentSuspect.claimedShape) _isLying = true;
    if (_currentSuspect.suspectColor != _currentSuspect.claimedColor) _isLying = true;

    suspectNameText.text = _currentSuspect.suspectName;
    colorDescriptionText.text = _currentSuspect.claimedColor.ToString();
    shapeDescriptionText.text = _currentSuspect.claimedShape.ToString();
  }

  private void UpdateScore()
  {
    if (_currentScore < 0) _currentScore = 0;
    else GameMaster.GameSucceeded();

    currentScoreText.text = $"{_currentScore} / {_goalScore}";
    GenerateSuspect();
  }

  // Public
  public void OnAllow()
  {
    if (_isLying) _currentScore -= 1;
    else _currentScore += 1;

    UpdateScore();
  }

  public void OnDeny()
  {
    if (_isLying) _currentScore += 1;
    else _currentScore -= 1;

    UpdateScore();
  }
}
