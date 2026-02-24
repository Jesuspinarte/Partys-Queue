using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : GAWGameManager
{
  // Serialized vars
  [Header("Game References")]
  [SerializeField] private GameObject gameContainer;
  [SerializeField] private GameObject finishContainer;
  [SerializeField] private TMP_Text finishText;

  [Header("Suspect Area References")]
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
  private int _goalScore = 10;

  // Game Managers Hooks
  public override void OnGameLoad()
  {
    switch (GameMaster.GetDifficulty())
    {
      case GameMaster.Difficulty.VERY_EASY:
        _goalScore = 5;
        break;
      case GameMaster.Difficulty.EASY:
        _goalScore = 5;
        break;
      case GameMaster.Difficulty.NORMAL:
        _goalScore = 7;
        break;
      case GameMaster.Difficulty.HARD:
        _goalScore = 10;
        break;
      case GameMaster.Difficulty.VERY_HARD:
        _goalScore = 15;
        break;
      default:
        _goalScore = 10;
        break;
    }

    UpdateScore();
  }

  public override void OnGameStart()
  {

  }

  public override void OnGameSucceeded()
  {
    if (_currentSuspectInstance != null) Destroy(_currentSuspectInstance);
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
  private void GenerateSuspect()
  {
    SuspectProfile previousSupect = _currentSuspect;
    bool keepSearching = true;

    while (keepSearching)
    {
      _isLying = false;
      if (_currentSuspectInstance != null) Destroy(_currentSuspectInstance);

      _currentSuspect = suspectList[Random.Range(0, suspectList.Count)];
      _currentSuspectInstance = Instantiate(_currentSuspect.prefab, spawnPoint.position, Quaternion.identity);

      if (_currentSuspect.suspectShape != _currentSuspect.claimedShape) _isLying = true;
      if (_currentSuspect.suspectColor != _currentSuspect.claimedColor) _isLying = true;

      suspectNameText.text = _currentSuspect.suspectName;
      colorDescriptionText.text = _currentSuspect.claimedColor.ToString();
      shapeDescriptionText.text = _currentSuspect.claimedShape.ToString();

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
      if (_isLying) _currentScore -= 1;
      else _currentScore += 1;

      UpdateScore();
    }
  }

  public void OnDeny()
  {
    if (GameMaster.GetGameState() == Game.State.RUNNING)
    {
      if (_isLying) _currentScore += 1;
      else _currentScore -= 1;

      UpdateScore();
    }
  }
}
