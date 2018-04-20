using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ActiveBastionMeta {
    private static string _fileName = "bastion00.json";
    private static BastionData _bastionData;
	
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
}
