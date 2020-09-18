using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class ConfigManager : MonoBehaviour
{
    public ParameterData data;

    private string file = "";

    public bool Save()
    {
        string json = JsonUtility.ToJson(data);
        return writeToFile(file, json);

    }

    public void Load()
    {
        data = new ParameterData();
        string json = ReadFromFile(file);
        JsonUtility.FromJsonOverwrite(json, data);
    }

    private bool writeToFile(string filename, string json)
    {

        string path = GetFilePath(filename);

        Debug.Log("Start saving config...");
        FileStream fileStream = new FileStream(path, FileMode.Create);
        using (StreamWriter writer = new StreamWriter(fileStream))
        {
            writer.Write(json);
        }
        Debug.Log("Saved here:" + GetFilePath(file));
        return true;
    }

    private string ReadFromFile(string filename)
    {
        string path = GetFilePath(filename);
        if (File.Exists(path))
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string json = reader.ReadToEnd();
                return json;
            }
        }
        else
        {
            Debug.LogWarning("File not found");
        }
        return "";
    }

    private string GetFilePath(string filename)
    {
        return Application.persistentDataPath + "/Config/" + filename;
    }

    public void SetFilename(string filename)
    {
        file = filename;
    }

    public void SetForward(KeyCode key)
    {
        data.forward = key;
    }

    public void SetBackward(KeyCode key)
    {
        data.backward = key;
    }
    public void SetLeft(KeyCode key)
    {
        data.left = key;
    }
    public void SetRight(KeyCode key)
    {
        data.right = key;
    }
    public void SetJump(KeyCode key)
    {
        data.jump = key;
    }
    public void SetInteract(KeyCode key)
    {
        data.interact = key;
    }

    public KeyCode GetForward()
    {
        return data.forward;
    }

    public KeyCode GetBackward()
    {
        return data.backward;
    }
    public KeyCode GetLeft()
    {
        return data.left;
    }
    public KeyCode GetRight()
    {
        return data.right;
    }
    public KeyCode GetJump()
    {
        return data.jump;
    }
    public KeyCode GetInteract()
    {
        return data.interact;
    }
}
