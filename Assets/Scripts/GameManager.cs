using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }
	private PlayerController _player;
	public PlayerController Player => _player;
	private UIManager _uiManager;
	public UIManager UIManager => _uiManager;
	private bool _fadeOutToBlack = false;
	private bool _isQuitting;
	private CameraManager _cameraManager;
	public CameraManager CameraManager => _cameraManager;
	private LevelSpawnTrack _spawner;
	public LevelSpawnTrack spawner => _spawner;

	public bool FadeOutToBlack
	{
		get => _fadeOutToBlack;
		set => _fadeOutToBlack = value;
	}

	private void OnEnable()
	{
		SceneManager.sceneLoaded += OnLevelFinishedLoadingScene;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
	}

	private void OnDisable()
	{
		SceneManager.sceneLoaded -= OnLevelFinishedLoadingScene;
	}

	//this function is activated every time a scene is loaded
	private void OnLevelFinishedLoadingScene(Scene scene, LoadSceneMode mode)
	{
		Setup();
		if (_fadeOutToBlack)
		{
			UIManager.FadeToBlack(false);
			_fadeOutToBlack = false;
		}
	}

	private void Setup()
	{
		//alternative way to get elements. cons : if there is no element with such tag it creates an error
		//_player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
		_player = FindObjectOfType<PlayerController>();
		_uiManager = FindObjectOfType<UIManager>();
		_cameraManager = FindObjectOfType<CameraManager>();
		_spawner = FindObjectOfType<LevelSpawnTrack>();
	}

	private void Awake()
	{
		if (Instance != null && Instance != this)
		{
			Destroy(gameObject);
		}
		else
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}

		Setup();
	}

	public void LoadLevel(string nameLevel)
	{
		SceneManager.LoadScene(nameLevel);
	}

	public void LoadLevelFadeInAndOut(string nameLevel)
	{
		UIManager.FadeToBlack(true);
		_fadeOutToBlack = true;
		StartCoroutine(LoadingLevel(nameLevel));
	}

	public void LoadLevel(string nameLevel, bool fadeInToBlack, bool fadeOutToBlack)
	{
		if (fadeInToBlack)
		{
			_uiManager.FadeToBlack(true);
			StartCoroutine(LoadingLevel(nameLevel));
		}
		else
		{
			LoadLevel(nameLevel);
		}

		_fadeOutToBlack = fadeOutToBlack;
	}

	private IEnumerator LoadingLevel(string nameLevel)
	{
		while (UIManager.IsFadingToBlack)
		{
			yield return null;
		}

		LoadLevel(nameLevel);
	}

	public void QuitGame()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
	}
}