using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;

public class SongLoader : MonoBehaviour
{
    private string songName;
    private string artist;

    private string folderPath;
    private string audioFilePath;

    private List<string> songInfoFiles;
    public bool IfSongLoaded { get => ifAduioLoaded && ifDataLoaded; }

    public Song ResultSong { get; private set; }

    private bool ifAduioLoaded;
    private bool ifDataLoaded;

    private List<Difficulty> difficulties;
    private AudioClip audioClip;

    public SongLoader()
    {
        songInfoFiles = new List<string>();
    }

    public void Initialize(string folderPath)
    {
        this.folderPath = folderPath;

        difficulties = new List<Difficulty>();

        var files = Directory.GetFiles(folderPath);

        SetAudioAndDataPaths(files);

        ifAduioLoaded = false;
        ifDataLoaded = false;
        print($"song loader initialized with path:\t{folderPath}");
    }

    private void SetAudioAndDataPaths(string[] files)
    {
        foreach (var file in files)
        {
            if (file.Contains(".meta"))
            {
                continue;
            }
            if (file.Contains(".mp3"))
            {
                audioFilePath = file;
            }
            if (file.Contains(".osu"))
            {
                songInfoFiles.Add(file);
            }
        }
    }

    public void StartLoadSong()
    {
        print("Song Load Started");
        StartCoroutine(LoadData());
        StartCoroutine(LoadAudio());
    }

    IEnumerator LoadData()
    {
        foreach (var diffFile in songInfoFiles)
        {
            string name;
            float[] difficultyInfo;
            Queue<HitObject> hits;

            using (var file = File.OpenText(diffFile))
            {
                name = GetDifficultyName(file);
                print($"working on {name} diff");
                difficultyInfo = GetDifficultyInfo(file);

                hits = GetHitObjects(file, DifficultyInterpretation.ApproachRateToNumber(difficultyInfo[3]));
            }
            print("diff got");
            Difficulty diff = new Difficulty(hits, name, difficultyInfo);
            difficulties.Add(diff);
        }

        yield return 1;
        ifDataLoaded = true;
        print("Song data loaded sucesfull");
    }

    private string GetDifficultyName(StreamReader file)
    {
        print("loading difficulty");
        string result = "no name";
        while (file.ReadLine() != "[Metadata]") ;
        while (file.Peek() != '[')
        {
            string currentString = file.ReadLine();
            if (currentString.Contains("Version"))
            {
                result = currentString.Substring(8);
                return result;
            }
        }
        return result;
    }

    private float[] GetDifficultyInfo(StreamReader file)
    {
        
        float[] difficultyInfo = new float[6];
        while (file.ReadLine() != "[Difficulty]") ;
        for (int i = 0; i < 6; i++)
        {
            string currentLine = file.ReadLine().Replace('.', ',');
            difficultyInfo[i] = float.Parse(currentLine.Split(':')[1]);
        }
        return difficultyInfo;
    }

    private Queue<HitObject> GetHitObjects(StreamReader file, float lifeTime)
    {
        Queue<HitObject> hits = new Queue<HitObject>();

        while (file.ReadLine() != "[HitObjects]") ;

        while (!file.EndOfStream)
        {
            string line = file.ReadLine();
            hits.Enqueue(CreateHitObject(line, lifeTime));
        }
        return hits;
    }

    private HitObject CreateHitObject(string line, float lifeTime)
    {
        //print("creating hit object");
        if (line.Contains('|'))
        {
            return CreateSlider(line, lifeTime);
        }
        else if (CountChars(line, ',') == 5)
        {
            return CreateSimpleHit(line, lifeTime);
        }
        else
        {
            return CreateSpinner(line, lifeTime);
        }
    }

    private HitObject CreateSimpleHit(string line, float lifeTime)
    {
        string[] data = line.Split(',');
        float x = float.Parse(data[0]);
        float y = float.Parse(data[1]);
        float time = float.Parse(data[2]) / 1000;
        HitObject hitObject = new HitCircle(x, y, lifeTime, time);
        return hitObject;
    }

    private HitObject CreateSlider(string line, float lifeTime)
    {
        string[] data = line.Split('|');
        int lastSectorNumber = 1;
        for (; lastSectorNumber < data.Length; lastSectorNumber++)
            if (CountChars(data[lastSectorNumber], ',') != 0)
                break;
        string[] firstSector = data[0].Split(',');
        string[] lastSector = data[lastSectorNumber].Split(',');
        int usefulSectorCount = lastSectorNumber + 1;

        float[,] hits = new float[usefulSectorCount, 2];
        float startTime = float.Parse(firstSector[2]) / 1000.0f;
        float sliderRepeatCount = float.Parse(lastSector[1]);
        data[0] = firstSector[0] + ':' + firstSector[1];
        data[lastSectorNumber] = lastSector[0];

        for (int i = 0; i < usefulSectorCount; i++)
        {
            string[] strCoordinates = data[i].Split(':');
            hits[i, 0] = float.Parse(strCoordinates[0]);
            hits[i, 1] = float.Parse(strCoordinates[1]);
        }

        return new Slider(lifeTime, startTime, sliderRepeatCount, Slider.SliderType.B, hits);
    }

    private HitObject CreateSpinner(string line, float lifeTime)
    {
        string[] data = line.Split(',');
        float x = float.Parse(data[0]);
        float y = float.Parse(data[1]);
        float startTime = float.Parse(data[2]) / 1000.0f;
        float endTime = float.Parse(data[5]) / 1000.0f;
        return new Spinner(x, y, lifeTime, startTime, endTime);
    }

    private IEnumerator LoadAudio()
    {
        string webPath = @"file://" + audioFilePath;
        //print($"Loading audio from {webPath}");
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(webPath, AudioType.MPEG))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.LogError(www.error);
            }
            else
            {
                audioClip = DownloadHandlerAudioClip.GetContent(www);
                //print("Song audio loaded sucesfull");
                ifAduioLoaded = true;
            }
        }
    }

    public Song GetSong()
    {
        if (IfSongLoaded)
        {
            Song song = new Song(audioClip, difficulties, songName);
            return song;
        }
        else
        {
            throw new System.Exception("Song isn't loaded yet");
        }
    }

    private int CountChars(string line, char symbol)
    {
        int count = 0;

        foreach (var letter in line)
        {
            if (letter == symbol)
            {
                count++;
            }
        }

        return count;
    }
}
