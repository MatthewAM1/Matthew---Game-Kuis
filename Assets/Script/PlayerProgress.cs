using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[CreateAssetMenu(
    fileName = "Player Progress",
    menuName = "Game Kuis/Player Progress")]
public class PlayerProgress : ScriptableObject
{
    [System.Serializable]
    public struct MainData
    {
        public int koin;
        public Dictionary<string, int> progressLevel;
    }

    [SerializeField]
    private string _filename = "contoh.txt";

    public MainData progressData = new MainData();

    public void SimpanProgress()
    {
        // Sampel data
        progressData.koin = 200;
        if (progressData.progressLevel == null)
            progressData.progressLevel = new();
        progressData.progressLevel.Add("Level Pack 1", 3);
        progressData.progressLevel.Add("Level Pack 3", 5);

        // Informasi penyimpanan data
        string directory = Application.dataPath + "/Temporary/";
        string path = directory + _filename;

        // Membuat directory temporary
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
            Debug.Log("Directory has been created: " + directory);
        }

        // Membuat file baru
        if (!File.Exists(path))
        {
            File.Create(path).Dispose();
            Debug.Log("File created: " + path);
        }

        // Menyimpan data ke dalam file menggunakan binari formatter
        var fileStream = File.Open(path, FileMode.Open);
        var formatter = new BinaryFormatter();

        fileStream.Flush();
        formatter.Serialize(fileStream, progressData);

        // Putuskan aliran memori dengan file
        fileStream.Dispose();

        //========================================================
        //// Menyimpan data ke dalam file menggunakan binari writer
        //var writer = new BinaryWriter(fileStream);

        //writer.Write(progressData.koin);
        //foreach (var i in progressData.progressLevel)
        //{
        //    writer.Write(i.Key);
        //    writer.Write(i.Value);
        //}

        //// Putuskan aliran memori dengan file
        //writer.Dispose();

        //========================================================
        // Menyimpan data ke dalam file
        //var kontenData = $"{progressData.koin}\n";

        //foreach (var i in progressData.progressLevel)
        //{
        //    kontenData += $"{i.Key} {i.Value}\n";
        //}

        //File.WriteAllText(path, kontenData);

        Debug.Log("Data saved to file: " + path);
    }

    public bool MuatProgress()
    {
        // Informasi penyimpanan data
        string directory = Application.dataPath + "/Temporary/";
        string path = directory + _filename;

        try
        {
            // Menyimpan data ke dalam file menggunakan binari formatter
            var fileStream = File.Open(path, FileMode.Open);
            var formatter = new BinaryFormatter();

            progressData = (MainData)formatter.Deserialize(fileStream);

            // Putuskan aliran memori dengan file
            fileStream.Dispose();

            Debug.Log($"{progressData.koin}; {progressData.progressLevel.Count}");

            return true;
        }

        catch (System.Exception e)
        {
            Debug.Log($"Error: Terjadi kesalahan saat memuat progress\n{e.Message}");

            return false;
        }
        
    }
}
