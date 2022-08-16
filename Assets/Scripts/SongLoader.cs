using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;

public class SongLoader : MonoBehaviour
{
    private string infoFilePath;
    private string audioFilePath;
    private string songName;

    public bool IfSongLoaded { get => ifAduioLoaded && ifDataLoaded; }

    public Song ResultSong { get; private set; }

    private bool ifAduioLoaded;
    private bool ifDataLoaded;

    private Queue<HitObject> hits;
    private AudioClip audioClip;

    public void Initialize(string absoluteFolderPath, string songName = "Unknown", string mainSongFileName = @"./Song.prosu",
        string audioFileName = @"./Audio.mp3")
    {
        this.songName = songName;
        infoFilePath = Path.Combine(absoluteFolderPath, mainSongFileName);
        audioFilePath = Path.Combine(absoluteFolderPath, audioFileName);
        ifAduioLoaded = false;
        ifDataLoaded = false;
        print($"song loader initialized with:\n\tInfo = {infoFilePath}\n\tAudio = {audioFilePath}");
    }
    public void StartLoadSong()
    {
        StartCoroutine(LoadData());
        StartCoroutine(LoadAudio());
        print("Song Load Started");
    }
    IEnumerator LoadData()
    {
        hits = new Queue<HitObject>();

        using (var file = File.OpenText(infoFilePath))
        {
            while (file.ReadLine() != "[HitObjects]") ;
            while (!file.EndOfStream)
            {
                string line = file.ReadLine();
                hits.Enqueue(CreateHitObject(line));
            }
        }
        yield return 1;
        ifDataLoaded = true;
        print("Song data loaded sucesfull");
    }

    IEnumerator LoadAudio()
    {
        string webPath = @"file://" + audioFilePath;
        print($"Loading audio from {webPath}");
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
                print("Song audio loaded sucesfull");
                ifAduioLoaded = true;
            }
        }
    }


    HitObject CreateHitObject(string line)
    {
        string[] data = line.Split(',');
        float x = float.Parse(data[0]);
        float y = float.Parse(data[1]);
        float time = float.Parse(data[2]) / 1000;
        HitObject hitObject = new HitObject(x, y, time);
        //print(hitObject);
        return hitObject;
    }

    public Song GetSong()
    {
        if (IfSongLoaded)
        {
            Song song = new Song(audioClip, hits, songName);
            return song;
        }
        else
        {
            throw new System.Exception("Song isn't loaded yet");
        }
    }
}
