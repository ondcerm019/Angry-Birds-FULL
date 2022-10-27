using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    [SerializeField] string _nextLevelName;

    Monster[] _monsters;

    void OnEnable()
    {
        _monsters = FindObjectsOfType<Monster>();
    }


    // Update is called once per frame
    void Update()
    {
        if (MonstersAreAllDead())
        {
            StartCoroutine(GoToNextLevel());
        }
    }

    bool MonstersAreAllDead()
    {
        foreach (var monster in _monsters)
        {
            if (!monster.HasDied)
            {
                return false;
            }
        }

        return true;
    }

    IEnumerator GoToNextLevel()
    {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene(_nextLevelName);
    }
}
