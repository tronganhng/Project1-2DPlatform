using UnityEngine;
using System.IO;  // dung de thao tac vs file
using System.Runtime.Serialization.Formatters.Binary; // truy cap nhi phan
public static class SaveSystem
{
    public static void SavePlayer(IteamCollect player)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/player.fun"; // noi luu file
        FileStream file = new FileStream(path, FileMode.Create);  // tao file co duong dan path

        PlayerData playerdata = new PlayerData(player);  // tao data cua player

        formatter.Serialize(file, playerdata);  // ghi du lieu vao 'file' co data la 'playerdata'

        file.Close(); // dong file
    }

    public static PlayerData LoadData()
    {
        string path = Application.persistentDataPath + "/player.fun"; // noi luu file
        if(File.Exists(path))  // kiem tra file ton tai
        {
            BinaryFormatter formatter = new BinaryFormatter(); 
            FileStream file = new FileStream(path, FileMode.Open);  // mo file da luu
            PlayerData data = formatter.Deserialize(file) as PlayerData;  // doc file
            Debug.Log(path);
            file.Close();
            return data;
        }
        else
        {
            return null;
        }
    }
}
