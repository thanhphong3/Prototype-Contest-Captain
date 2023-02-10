using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AutoBuild : MonoBehaviour
{
    public static void ShowCommand()
    {
        Debug.Log("Build Done Phong");
    }
    public static void PerformSwitchAndroid()
    {
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android, BuildTarget.Android);
    }

}
