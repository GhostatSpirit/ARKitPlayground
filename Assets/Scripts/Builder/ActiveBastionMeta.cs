using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ActiveBastionMeta {
    private static string _fileName = "bastion00.json";
    private static BastionData _bastionData;
    private static int _targetSceneNum = 0;
	
    public static string fileName
    {
        get
        {
            return _fileName;
        }
        set
        {
            _fileName = value;
        }
    }

    public static BastionData bastionData
    {
        get
        {
            return _bastionData;
        }
        set
        {
            _bastionData = value;
        }
    }

    public static int targetSceneNum
    {
        get
        {
            return _targetSceneNum;
        }
        set
        {
            _targetSceneNum = value;
        }
    }
}
