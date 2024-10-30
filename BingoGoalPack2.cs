using Modding;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using BingoSync.CustomGoals;


namespace BingoGoalPack2 {
    public class BingoGoalPack2: Mod {
        new public string GetName() => "BingoGoalPack2";
        public override string GetVersion() => "1.0.0.0";
        public override int LoadPriority() => 8;

        public override void Initialize(Dictionary<string, Dictionary<string, GameObject>> preloadedObjects) {
            Assembly assembly = Assembly.GetExecutingAssembly();

            Dictionary<string, BingoGoal> myGoals = processEmbeddedJson(assembly, "Goals");
            GameMode mode = new("GoalPack2", myGoals);
            //BingoSync.Goals.AddGameMode(mode);
            BingoSync.Goals.RegisterGoalsForCustom("Goal Pack 2", myGoals);
        }

        private Dictionary<string, BingoGoal> processEmbeddedJson(Assembly assembly, string jsonName) {
            string resourceName = assembly.GetManifestResourceNames().Single(str => str.EndsWith("Squares." + jsonName + ".json"));
            Stream stream = assembly.GetManifestResourceStream(resourceName);
            return BingoSync.Goals.ProcessGoalsStream(stream);
        }
    }
}