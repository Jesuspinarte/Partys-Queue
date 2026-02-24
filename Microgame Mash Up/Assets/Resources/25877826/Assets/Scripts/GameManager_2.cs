using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager_2 : GAWGameManager
{
  // Serialized vars
  [Header("Game Object References")]
  [SerializeField] private GameObject gameContainer;
  [SerializeField] private GameObject finishContainer;

  [Header("Game Text References")]
  [SerializeField] private TMP_Text finishText;
  [SerializeField] private TMP_Text currentScoreText;

  [Header("Current Suspect References")]
  [SerializeField] private TMP_Text suspectNameText;

  [Header("Target Passport References")]
  [SerializeField] private TMP_Text targetSuspectNameText;
  [SerializeField] private TMP_Text targetSuspectShapeText;
  [SerializeField] private TMP_Text targetSuspectColorText;
  [SerializeField] private TMP_Text targetSuspectMeterText;

  [Header("Spawn Settings")]
  [SerializeField] private List<SuspectProfile> suspectList = new List<SuspectProfile>();
  [SerializeField] private Transform spawnPoint;

  // Private vars
  private int _goalScore = 10;
  private int _currentScore = 0;
  private bool _matchesTargetSuspect = false;
  private SuspectProfile _targetSuspect;
  private SuspectProfile _currentSuspect;
  private GameObject _targetSuspectInstance;
  private GameObject _currentSuspectInstance;

  private int _targetSuspectMeter = 1; // How many characteristics to check

  // Game Managers Hooks
  public override void OnGameLoad()
  {
    switch (GameMaster.GetDifficulty())
    {
      case GameMaster.Difficulty.VERY_EASY:
        _goalScore = 5;
        _targetSuspectMeter = 1;
        break;
      case GameMaster.Difficulty.EASY:
        _goalScore = 6;
        _targetSuspectMeter = 1;
        break;
      case GameMaster.Difficulty.NORMAL:
        _goalScore = 7;
        _targetSuspectMeter = 2;
        break;
      case GameMaster.Difficulty.HARD:
        _goalScore = 8;
        _targetSuspectMeter = 3;
        break;
      case GameMaster.Difficulty.VERY_HARD:
        _goalScore = 10;
        _targetSuspectMeter = 3;
        break;
      default:
        _goalScore = 10;
        _targetSuspectMeter = 1;
        break;
    }

    UpdateScore();
    SetSuspect();
  }

  public override void OnGameStart()
  {

  }

  public override void OnGameSucceeded()
  {
    if (_currentSuspectInstance != null) Destroy(_currentSuspectInstance);
    if (_targetSuspectInstance != null) Destroy(_targetSuspectInstance);
    if (gameContainer != null) gameContainer.SetActive(false);

    if (finishContainer != null)
    {
      finishContainer.SetActive(true);
      finishText.text = "YOU ARE ON SHAPE!";
    }
  }

  public override void OnGameFailed()
  {
    if (_currentSuspectInstance != null) Destroy(_currentSuspectInstance);
    if (_targetSuspectInstance != null) Destroy(_targetSuspectInstance);
    if (gameContainer != null) gameContainer.SetActive(false);

    if (finishContainer != null)
    {
      finishContainer.SetActive(true);
      finishText.text = "YOU ARE OUT OF SHAPE!";
    }
  }

  // Unity Hooks
  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.A)) OnAllow();
    if (Input.GetKeyDown(KeyCode.D)) OnDeny();
  }

  // Private
  private (SuspectProfile suspect, GameObject suspectInstace) CreateSuspect()
  {
    return (
      suspect: suspectList[Random.Range(0, suspectList.Count)],
      suspectInstace: _currentSuspectInstance = Instantiate(_currentSuspect.prefab, spawnPoint.position, Quaternion.identity)
    );
  }

  private void SetSuspect()
  {
    (SuspectProfile suspect, GameObject suspectInstace) = CreateSuspect();
    _targetSuspect = suspect;
    _targetSuspectInstance = suspectInstace;

    suspectNameText.text = _currentSuspect.suspectName;
    targetSuspectColorText.text = _currentSuspect.claimedColor.ToString();
    targetSuspectShapeText.text = _currentSuspect.claimedShape.ToString();

    targetSuspectMeterText.text = $"Check for {_targetSuspectMeter} characteristic(s)";
  }

  private void GenerateSuspect()
  {
    SuspectProfile previousSupect = _currentSuspect;
    bool keepSearching = true;

    while (keepSearching)
    {
      int suspectMeter = 0;
      _matchesTargetSuspect = false;
      if (_currentSuspectInstance != null) Destroy(_currentSuspectInstance);

      (SuspectProfile suspect, GameObject suspectInstace) = CreateSuspect();

      _currentSuspect = suspect;
      _currentSuspectInstance = suspectInstace;

      if (_currentSuspect.suspectShape == _targetSuspect.suspectShape) ++suspectMeter;
      if (_currentSuspect.suspectColor == _targetSuspect.suspectColor) ++suspectMeter;
      if (_currentSuspect.name == _targetSuspect.name) ++suspectMeter;

      if (suspectMeter >= _targetSuspectMeter) _matchesTargetSuspect = true;

      if (previousSupect == null) keepSearching = false;
      else if (previousSupect.name != _currentSuspect.name) keepSearching = false;
    }
  }

  private void UpdateScore()
  {
    if (_currentScore < 0) _currentScore = 0;
    else if (_currentScore >= _goalScore) GameMaster.GameSucceeded();

    if (_currentScore < _goalScore) GenerateSuspect();
    currentScoreText.text = $"{_currentScore} / {_goalScore}";
  }

  // Public
  public void OnAllow()
  {
    if (GameMaster.GetGameState() == Game.State.RUNNING)
    {
      if (_matchesTargetSuspect) _currentScore -= 1;
      else _currentScore += 1;

      UpdateScore();
    }
  }

  public void OnDeny()
  {
    if (GameMaster.GetGameState() == Game.State.RUNNING)
    {
      if (_matchesTargetSuspect) _currentScore += 1;
      else _currentScore -= 1;

      UpdateScore();
    }
  }
}
