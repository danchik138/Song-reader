using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class SongManager : MonoBehaviour
{

    public TMP_Text text1;

    public SpawnManager spawnManager;

    public string commonSongsFolderPath;

    public string[] songFolders;

    private SongLoader loader;
    GameObject loaderObject;
    void Start()
    {
        commonSongsFolderPath = Path.GetFullPath(Application.dataPath) + @"\Songs";
        print(commonSongsFolderPath);
        if (!Directory.Exists(commonSongsFolderPath))
        {
            Directory.CreateDirectory(commonSongsFolderPath);
        }

        songFolders = Directory.GetDirectories(commonSongsFolderPath);

        text1.text = Path.GetFullPath(songFolders[0]);

        loaderObject = new GameObject();
        loader = loaderObject.AddComponent<SongLoader>();
        loader.Initialize(songFolders[0]);
        loader.StartLoadSong();
    }

    private void Update()
    {
        if (loader.IfSongLoaded)
        {
            Song song = loader.GetSong();
            spawnManager.StartSong(song.audio, song.GetDifficulty(0).GetHitsQueue());
            gameObject.active = false;
        }
    }


}
