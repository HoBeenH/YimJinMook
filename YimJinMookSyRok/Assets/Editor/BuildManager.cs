#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using Sirenix.Utilities;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CreateAssetMenu(fileName = "BuildManager", menuName = "Build")]
    public class BuildManager : ScriptableObject
    {
        private static BuildManager instance;
        public BuildManager() => instance = this;
        [SerializeField] private List<string> FileNames;

        [MenuItem("Build/Window",false,1)]
        public static void Build()
        {
            if (instance.FileNames.IsNullOrEmpty())
            { Debug.LogAssertion(instance.FileNames.ToString()); return; }

            var scenes = new string[instance.FileNames.Count];
            for (int i = 0; i < scenes.Length; i++)
            {
                scenes[i] = $"Assets\\Scenes\\{instance.FileNames[i]}.unity";
            }
            var flags = BuildOptions.Development;
            var savePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "\\TestBuild\\Test.exe");
            BuildPipeline.BuildPlayer(scenes, savePath, BuildTarget.StandaloneWindows, flags);
        }
    }
}
#endif

