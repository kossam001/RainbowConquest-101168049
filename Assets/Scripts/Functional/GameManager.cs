using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    [SerializeField] public List<Material> colours;
    public Dictionary<Color, Dictionary<int, GameObject>> teams;

    private int characterCount = 0;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        teams = new Dictionary<Color, Dictionary<int, GameObject>>();
        
        for (int i = 0; i < 3; i++)
        {
            teams.Add(colours[i].color, new Dictionary<int, GameObject>());
        }
    }

    public Material InitColour(GameObject character, CharacterData data)
    {
        Material randColour = colours[Random.Range(0, colours.Count)];
        teams[randColour.color].Add(characterCount, character);

        data.id = characterCount;
        characterCount++;

        return randColour;
    }

    public List<GameObject> GetEnemies(Material colour)
    {
        List<GameObject> enemies = new List<GameObject>();

        foreach (Material mat in colours)
        {
            if (mat.color != colour.color)
            {
                enemies.AddRange(teams[mat.color].Values);
            }
        }

        return enemies;
    }

    public void ChangeTeams(Material oldColour, Material newColour, CharacterData data, GameObject character)
    {
        teams[oldColour.color].Remove(data.id);
        teams[newColour.color].Add(data.id, character);
    }
}
