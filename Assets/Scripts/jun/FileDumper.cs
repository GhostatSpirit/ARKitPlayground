using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FileDumper : MonoBehaviour {
    public string path;
    private TurretArranger TA;
    private List<GameObject> boxes;

    private class Turret
    {
        public int type;
        public Vector3 position;
    };
    // Use this for initialization
    void Start () {
        TA = GetComponent<TurretArranger>();
        boxes = TA.boxes;
        path = @"MyTest.txt";
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SaveToFile()
    {
        FileStream fs = new FileStream(path, FileMode.Truncate);

        StreamWriter sw = new StreamWriter(fs);

        string turretString = "";
        foreach (GameObject box in boxes)
        {
            //turretString = "";
            turretString = box.GetComponent<BasicInfo>().GetInfo();
            sw.WriteLine(turretString);

        }
        sw.Close();
        fs.Close();

    }

    public int LoadFromFile(ref int turretType, ref Vector3 position) {
        FileStream fs = new FileStream(path, FileMode.Open);
        string str;
        string tmp;
        StreamReader sr = new StreamReader(fs);
        while (!sr.EndOfStream) {
            str=sr.ReadLine();
            
            Debug.Log(str);
            int i = 0;
            //for (int j = 0; j < 4; j++) {
            tmp = readWordFromString(str, ref i);
            turretType = int.Parse(tmp);
            tmp = readWordFromString(str, ref i);
            position.x = float.Parse(tmp);
            tmp = readWordFromString(str, ref i);
            position.y = float.Parse(tmp);
            tmp = readWordFromString(str, ref i);
            position.z = float.Parse(tmp);
            Debug.Log(tmp);

        }
        //while (sr) {
        //    str=sr.readLine();
        //}
        sr.Close();
        fs.Close();

        return 0;
    }

    private string readWordFromString(string str, ref int index) {
        string word="";
        for ( ; index < str.Length; index++) {
            if (str[index] != ' ')
            {
                word += str[index];
            }
            else { index++; break; }
        }
        return word;
    }

    //void CreateFile(string path, string name, string infos)
    //{
    //    StreamWriter sw;//文件流信息
    //    FileInfo fileInfo = new FileInfo( name);
    //    if (!fileInfo.Exists)
    //    {
    //        sw = fileInfo.CreateText();
    //        sw = fileInfo.OpenWrite();
    //    }
    //    else
    //    {
    //        sw = fileInfo.AppendText();//如果此文件存在，则打开该文件
    //    }
    //    sw.WriteLine(infos);
    //    sw.Close();
    //    sw.Dispose();
    //}
}
