using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Song
{
    public string name { get; private set; }

    public AudioClip audio { get; private set; }


    private List<Difficulty> difficulties;

    

    public Song(AudioClip audio, List<Difficulty> difficulties, string name = "unknown")
    {
        this.audio = audio;
        this.difficulties = difficulties;
        this.name = name;
    }
    
    public bool HasAnyDifficulty()
    {
        return difficulties.Count > 0;
    }


    public Difficulty GetDifficulty(int difficultyNumber)
    {
        return difficulties[difficultyNumber];
    }

    public Difficulty.DifficultyInfo[] GetDifficultiesNamesAndPositions()
    {
        var diffData = new Difficulty.DifficultyInfo[difficulties.Count];
        for (int i = 0; i < difficulties.Count; i++)
        {
            diffData[i] = difficulties[i].GetDifficultyInfo(i);
        }
        return diffData;
    }

    

    public override string ToString()
    {
        string result = $"SongName = {name}\nDifficulties:\n";
        foreach (Difficulty difficulty in difficulties)
        {
            result += difficulty.name + "\n";
        }
        result += $"Number of difficulties: {difficulties.Count.ToString()}\n";
        return result;
    }
}

