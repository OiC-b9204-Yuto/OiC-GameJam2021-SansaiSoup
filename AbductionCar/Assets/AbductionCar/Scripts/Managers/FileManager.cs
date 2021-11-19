﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AbductionCar.Managers
{
    public static class FileManager
    {
        public static void Save(string fileName, string data)
        {
            string path = Application.dataPath + "/" + fileName;
            using (StreamWriter sw =
                new StreamWriter(path, false, Encoding.UTF8))
            {
                sw.WriteLine(data);
            }
        }

        public static string Load(string fileName)
        {
            string readStr = "";
            string path = Application.dataPath + "/" + fileName;
            using (StreamReader sr =
                new StreamReader(path, Encoding.UTF8))
            {
                readStr = sr.ReadToEnd();
            }
            return readStr;
        }
    }
}
