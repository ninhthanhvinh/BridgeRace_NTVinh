using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private int currentLevel = 0;
    private int currentComponent = 0;
    public int CurrentLevel
    {
        get
        {
            return currentLevel;
        }
        set
        {
            currentLevel = value;
        }
    }
    public int CurrentComponent
    {
        get
        {
            return currentComponent;
        }
        set
        {
            currentComponent = value;
        }
    }

    [SerializeField] List<Level> levels;
    [SerializeField] List<Enemy> characters;
    [SerializeField] Transform player;

    public UnityEvent OnLevelEnd;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("GameManager already exists");
            Destroy(this);
        }

        Time.timeScale = 0;

    }

    public Transform GetGoalDestination(int level)
    {
        var randomIndex = Random.Range(0, levels[level].initGoal.Count());
        return levels[level].initGoal[randomIndex];
    }

    public BrickSpawner GetBrickSpawner(int level)
    {
        return levels[level].spawner[0];
    }

    public void LoadLevel(int level)
    {
        ResetLevel();
        if (level >= levels.Count)
        {
            LoadLevel(0);
            return;
        }
        Time.timeScale = 1;
        currentLevel = level;
        levels[level].levelObject.SetActive(true);
        var spawner = GetBrickSpawner(level);
        for (int i = 0; i < characters.Count; i++)
        {
            characters[i].gameObject.SetActive(true);
            characters[i].BrickSpawner = spawner;
            characters[i].GetComponent<BrickCarrier>().Spawner = spawner;
            characters[i].GoalPosition = GetGoalDestination(level).position;
            characters[i].transform.position = levels[level].spawnPositions[i].position;
            characters[i].GetComponent<NavMeshAgent>().enabled = true;
        }

        // Vì trong list player nằm ở vị trí cuối cùng nên ta sẽ lấy vị trí spawn của player là vị trí cuối cùng trong list spawnPositions,
        // sau characters.Count là số enemy có trong list characters
        player.gameObject.SetActive(true);
        player.position = levels[level].spawnPositions[characters.Count].position;
        player.GetComponent<BrickCarrier>().Spawner = spawner;
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }

    public void Replay()
    {
        ResetLevel();
        LoadLevel(currentLevel);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void NextLevel()
    {
        ResetLevel();
        LoadLevel(currentLevel + 1);
    }

    public void ResetLevel()
    {
        levels[currentLevel].levelObject.SetActive(false);
        foreach (var character in characters)
        {
            character.GetComponent<NavMeshAgent>().enabled = false;
            character.gameObject.SetActive(false);
        }

        player.gameObject.SetActive(false);
    }
}
