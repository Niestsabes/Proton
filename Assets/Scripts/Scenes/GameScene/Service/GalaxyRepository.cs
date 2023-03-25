using UnityEngine;
using System.IO;

public class GalaxyRepository
{
    public GalaxySerializable GetById(int levelId)
    {
        string content = this.ReadFile(this.GetFilePath(levelId));
        GalaxySerializable galaxyData = JsonUtility.FromJson<GalaxySerializable>(content);
        return galaxyData;
    }

    private string GetFilePath(int levelId)
    {
        return Application.streamingAssetsPath + "/Galaxy/galaxy" + levelId.ToString("00") + ".json";
    }

    private string ReadFile(string path)
    {
        if (File.Exists(path))
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string content = reader.ReadToEnd();
                return content;
            }
        }
        throw new System.Exception("File not found: " + path);
    }
}