// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;


namespace a.Utilities
{
    public static class Utilities
    {
        public static bool IsRunningMocked { get; set; }
        public static Action<string> LoggerMethod { get; set; }
        public static Func<string> PauseMethod { get; set; }
        public static string ProjectPath { get; set; }

        static Utilities()
        {
            LoggerMethod = Console.WriteLine;
            PauseMethod = Console.ReadLine;
            ProjectPath = ".";
        }

        public static void Log(string message)
        {
            LoggerMethod.Invoke(message);
        }

        public static void Log(object obj)
        {
            if (obj != null)
            {
                LoggerMethod.Invoke(obj.ToString());
            }
            else
            {
                LoggerMethod.Invoke("(null)");
            }
        }

        public static void Log()
        {
            Utilities.Log(string.Empty);
        }

        public static string GetArmTemplate(string templateFileName)
        {
            var hostingPlanName = "hpBart-Bun-Test-App01";
            var webAppName = "wnBart-Bun-Test-App01";
            //pipeline path
            var armTemplateString = File.ReadAllText(Path.Combine(Utilities.ProjectPath, "_Bunek88_AzDOAssignment-test/Azure-net/Asset", templateFileName));
            //local path
            //var armTemplateString = File.ReadAllText(Path.Combine(Utilities.ProjectPath, "Asset", templateFileName));

            if (string.Equals("ArmTemplate.json", templateFileName, StringComparison.OrdinalIgnoreCase))
            {
                armTemplateString = armTemplateString.Replace("\"hostingPlanName\": {\r\n      \"type\": \"string\",\r\n      \"defaultValue\": \"\"",
                   "\"hostingPlanName\": {\r\n      \"type\": \"string\",\r\n      \"defaultValue\": \"" + hostingPlanName + "\"");
                armTemplateString = armTemplateString.Replace("\"webSiteName\": {\r\n      \"type\": \"string\",\r\n      \"defaultValue\": \"\"",
                    "\"webSiteName\": {\r\n      \"type\": \"string\",\r\n      \"defaultValue\": \"" + webAppName + "\"");
            }

            return armTemplateString;
        }

      

        public static string RandomResourceName(string prefix, int maxLen)
        {
            var namer = new ResourceNamer("");
            return namer.RandomName(prefix, maxLen);
        }
      

        public static async Task<List<T>> ToEnumerableAsync<T>(this IAsyncEnumerable<T> asyncEnumerable)
        {
            List<T> list = new List<T>();
            await foreach (T item in asyncEnumerable)
            {
                list.Add(item);
            }
            return list;
        }
    }
}
