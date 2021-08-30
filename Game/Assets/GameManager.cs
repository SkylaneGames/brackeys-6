using CoreSystems.Transition.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int _enemiesRemaining;

    [SerializeField]
    private GameObject _winText;

    [SerializeField]
    private GameObject _gameOverText;

    [SerializeField]
    private TMP_Text _enemiesLeftText;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        var enemies = FindObjectsOfType<UnitController>().Where(p => p.Faction == Faction.Team_2).ToList();
        _enemiesRemaining = enemies.Count;
        foreach (var unit in enemies)
        {
            unit.Destroyed += OnEnemyDestroyed;
        }

        SetEnemiesLeftText(_enemiesRemaining);

        var player = FindObjectOfType<MechController>();

        player.Destroyed += OnPlayerDestroyed;
    }

    private void OnPlayerDestroyed(object sender, System.EventArgs e)
    {
        GameOver();
    }

    private void OnEnemyDestroyed(object sender, System.EventArgs e)
    {
        _enemiesRemaining--;
        SetEnemiesLeftText(_enemiesRemaining);

        if (_enemiesRemaining <= 0)
        {
            Win();
        }
    }

    private void SetEnemiesLeftText(int enemiesLeft)
    {
        _enemiesLeftText.text = $"Enemies Remaining: {enemiesLeft}";
    }

    private void Win()
    {
        _winText.SetActive(true);
        StartCoroutine(GoToMenu());
    }

    private void GameOver()
    {
        _gameOverText.SetActive(true);
        StartCoroutine(GoToMenu());
    }

    private IEnumerator GoToMenu()
    {
        yield return new WaitForSeconds(2);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        LevelLoader.Instance.LoadLevel(Level.Menu);
    }
}
