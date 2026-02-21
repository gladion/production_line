using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace _Sim.Scripts
{
    // Not loving this implementation, but good enough for now
    public class LogsManager : MonoBehaviour
    {
        public static LogsManager Instance;
        private List<string> _logList = new List<string>();
        private int _logCounter = 1;

        private string _folderPath;
        private string _filePath;


        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(this);
                return;
            }

            Instance = this;
            createFolder();
        }

        public void AddLog(int count)
        {
            _logList.Add(DateTime.Now.ToString("HH:mm:ss.fff") + " " + count);
        }
       
        public void WriteLog()
        {
            _filePath = Path.Combine(_folderPath, $"ProductionLineLog{_logCounter}.txt");

            try
            {
                if ( File.Exists(_filePath) )
                {
                    File.Delete(_filePath);
                }
            }
            catch (IOException e)
            {
                Debug.LogError("Error delete file: " + e.Message);
            }
            
            try
            {
                File.WriteAllLines(_filePath, _logList.ToArray());
            }
            catch (IOException e)
            {
                Debug.LogError("Error write to file: " + e.Message);
            }

            _logList.Clear();
            _logCounter++;
        }

        private void createFolder()
        {
            _folderPath = Path.Combine(Application.dataPath, "SaveData");

         
            if (!Directory.Exists(_folderPath))
            {
                Directory.CreateDirectory(_folderPath);
            }

        }
    }
}