using System.Collections.Generic;
using TMPro;
using UnityEngine;

using Random = UnityEngine.Random;

public class GameManager : GAWGameManager
{
  // Serialized vars
  [Header("Game References")]
  [SerializeField] private GameObject gameContainer;
  [SerializeField] private GameObject finishContainer;
  [SerializeField] private TMP_Text finishText;

  [Header("Blacklist Area References")]
  [SerializeField] private GameObject rulePrefab;
  [SerializeField] private TMP_Text currentScoreText;
  [SerializeField] private Transform ruleListContainer;

  [Header("Character Area References")]
  [SerializeField] private TMP_Text characterNameText;
  [SerializeField] private TMP_Text characterAgeText;
  [SerializeField] private TMP_Text characterOccupationText;

  [Header("Spawn Settings")]
  [SerializeField] private List<SuspectProfile> suspectList = new List<SuspectProfile>();
  [SerializeField] private List<EyesAccessoryPair> eyesAccessoryList = new List<EyesAccessoryPair>();
  [SerializeField] private List<HeadAccessoryPair> headAccessoryList = new List<HeadAccessoryPair>();
  [SerializeField] private List<MouthAccessoryPair> mouthAccessoryList = new List<MouthAccessoryPair>();
  [SerializeField] private List<NeckAccessoryPair> neckAccessoryList = new List<NeckAccessoryPair>();
  [SerializeField] private List<PetsAccessoryPair> petsAccessoryList = new List<PetsAccessoryPair>();
  [SerializeField] private Transform spawnPoint;

  // Private vars
  private bool _isLying = false;

  private int _goalScore = 5;
  private int _currentScore = 0;

  private GameObject _currentSuspectInstance;
  private SuspectProfileData _currentSuspect;

  private List<IBlacklistRule> activeRules = new List<IBlacklistRule>();

  // Game Managers Hooks
  public override void OnGameLoad()
  {
    // switch (GameMaster.GetDifficulty())
    // {
    //   case GameMaster.Difficulty.VERY_EASY:
    //     _goalScore = 5;
    //     break;
    //   case GameMaster.Difficulty.EASY:
    //     _goalScore = 5;
    //     break;
    //   case GameMaster.Difficulty.NORMAL:
    //     _goalScore = 7;
    //     break;
    //   case GameMaster.Difficulty.HARD:
    //     _goalScore = 10;
    //     break;
    //   case GameMaster.Difficulty.VERY_HARD:
    //     _goalScore = 15;
    //     break;
    //   default:
    //     _goalScore = 10;
    //     break;
    // }

    SetupRules();
    UpdateScore();
  }

  public override void OnGameStart() { }

  public override void OnGameSucceeded()
  {
    RemoveCurrentSuspect();

    if (gameContainer != null) gameContainer.SetActive(false);

    if (finishContainer != null)
    {
      finishContainer.SetActive(true);
      finishText.text = "Nice party! Keep your job!";
    }
  }

  public override void OnGameFailed()
  {
    RemoveCurrentSuspect();

    if (gameContainer != null) gameContainer.SetActive(false);

    if (finishContainer != null)
    {
      finishContainer.SetActive(true);
      finishText.text = "This party sucks! You're FIRED!";
    }
  }

  // Unity Hooks
  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.A)) OnAllow();
    if (Input.GetKeyDown(KeyCode.D)) OnDeny();
  }

  // Private
  private void RemoveCurrentSuspect()
  {
    for (int i = spawnPoint.childCount - 1; i >= 0; i--)
      Destroy(spawnPoint.GetChild(i).gameObject);
  }

  private void GenerateSuspect()
  {
    SuspectProfileData previousSupect = _currentSuspect;
    bool keepSearching = true;

    while (keepSearching)
    {
      _isLying = false;

      // 1. Generate suspect different than current one
      SuspectProfile newSuspectProfile = suspectList[Random.Range(0, suspectList.Count)];
      if (previousSupect != null && newSuspectProfile.englishName == previousSupect.profileData.englishName) continue;

      // 2. Remove current suspect
      if (_currentSuspectInstance != null) RemoveCurrentSuspect();
      // 3. Instantiate current suspect
      InstantiateSuspect(newSuspectProfile);
      // 4. Add accessories
      InstantiateAccessories();
      // 5. Update UI
      UpdateSuspectID();
      // 6. Check if the suspect is lying
      _isLying = IsSuspectBanned();
      // if (_currentSuspect.suspectShapeType != _currentSuspect.claimedShape) _isLying = true;
      // if (_currentSuspect.suspectColor != _currentSuspect.claimedColor) _isLying = true;

      keepSearching = false;
    }
  }

  private void InstantiateSuspect(SuspectProfile profile)
  {
    _currentSuspect = new SuspectProfileData(profile);
    _currentSuspectInstance = Instantiate(_currentSuspect.profileData.prefab, spawnPoint.position + Vector3.forward, Quaternion.identity, spawnPoint);
  }

  private void InstantiateAccessories()
  {
    GameObject accessoryPrefab = null;
    // Eyes accessories
    if (_currentSuspect.eyesAccessory != EnumEyesAccessoryType.NONE)
    {
      accessoryPrefab = eyesAccessoryList.Find(accessory => accessory.type == _currentSuspect.eyesAccessory).accessoryObj;
      Instantiate(accessoryPrefab, spawnPoint.position, Quaternion.identity, spawnPoint);
    }
    // Head accessories
    if (_currentSuspect.headAccessory != EnumHeadAccessoryType.NONE)
    {
      accessoryPrefab = headAccessoryList.Find(accessory => accessory.type == _currentSuspect.headAccessory).accessoryObj;
      Instantiate(accessoryPrefab, spawnPoint.position, Quaternion.identity, spawnPoint);
    }
    // Mouth accessories
    if (_currentSuspect.mouthAccessory != EnumMouthAccessoryType.NONE)
    {
      accessoryPrefab = mouthAccessoryList.Find(accessory => accessory.type == _currentSuspect.mouthAccessory).accessoryObj;
      Instantiate(accessoryPrefab, spawnPoint.position, Quaternion.identity, spawnPoint);
    }
    // Neck accessories
    if (_currentSuspect.neckAccessory != EnumNeckAccessoryType.NONE)
    {
      accessoryPrefab = neckAccessoryList.Find(accessory => accessory.type == _currentSuspect.neckAccessory).accessoryObj;
      Instantiate(accessoryPrefab, spawnPoint.position, Quaternion.identity, spawnPoint);
    }
    // Pets accessories
    if (_currentSuspect.petsAccessory != EnumPetsAccessoryType.NONE)
    {
      accessoryPrefab = petsAccessoryList.Find(accessory => accessory.type == _currentSuspect.petsAccessory).accessoryObj;
      Instantiate(accessoryPrefab, spawnPoint.position, Quaternion.identity, spawnPoint);
    }
  }

  private void UpdateSuspectID()
  {
    characterNameText.text = _currentSuspect.activeName;
    characterAgeText.text = $"{_currentSuspect.age} years";
    characterOccupationText.text = GameUtils.GetOccupationText(_currentSuspect.occupation);
  }

  private void SetupRules()
  {
    activeRules.Clear(); // Just in case xdxdxddddd

    switch (GameMaster.GetDifficulty())
    {
      case GameMaster.Difficulty.VERY_EASY:
        SetupVeryEasyRules();
        break;
      case GameMaster.Difficulty.EASY:
        SetupEasyRules();
        break;
      case GameMaster.Difficulty.NORMAL:
        SetupNormalRules();
        break;
      case GameMaster.Difficulty.HARD:
        SetupHardRules();
        break;
      case GameMaster.Difficulty.VERY_HARD:
        SetupVeryHardRules();
        break;
      default:
        SetupEasyRules();
        break;
    }

    UpdateNopeListUI();
  }

  // ----- Difficulties -----
  private void SetupVeryEasyRules()
  {
    int ruleType = Random.Range(0, 3);

    switch (ruleType)
    {
      case 0:
        activeRules.Add(new BanShapeRule());
        break;
      case 1:
        activeRules.Add(new BanAgeRule());
        break;
      case 2:
        activeRules.Add(new BanOccupationRule());
        break;
      default:
        activeRules.Add(new BanShapeRule());
        break;
    }
  }

  private void SetupEasyRules() { }

  private void SetupNormalRules() { }

  private void SetupHardRules() { }

  private void SetupVeryHardRules() { }

  private void UpdateNopeListUI()
  {
    foreach (IBlacklistRule rule in activeRules)
    {
      GameObject ruleGO = Instantiate(rulePrefab, ruleListContainer.position, Quaternion.identity, ruleListContainer);
      ruleGO.GetComponent<RuleController>().UpdateRuleText(rule.GetRuleDescription());
    }
  }

  // ---------------------------

  public bool IsSuspectBanned()
  {
    foreach (IBlacklistRule rule in activeRules)
      if (rule.IsBanned(_currentSuspect))
        return true;
    return false;
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
