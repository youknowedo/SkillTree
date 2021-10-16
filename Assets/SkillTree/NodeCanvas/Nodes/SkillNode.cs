using NodeCanvas.Framework;
using ParadoxNotion;
using ParadoxNotion.Design;
using System;
using System.Linq;
using UnityEngine;


namespace SkillTree.NodeCanvas
{

    [Serializable]
    [Name("Skill")]
    [Description("An unlockable skill by the player that adds a modifier to the gameplay")]
    public class SkillNode : STNode
    {

        public BBParameter<string> Description;

        public BBParameter<int> Cost;

        public BBParameter<bool> Unlocked;

        public BBParameter<bool> RequiresAllParentsUnlocked;

        internal bool HasDependenciesUnlocked()
        {
            // Either we need all direct parents to be unlocked or just one
            return RequiresAllParentsUnlocked.value && GetParentNodes().ToList().All(n => (n as SkillNode).Unlocked.value)
                    || !RequiresAllParentsUnlocked.value && GetParentNodes().ToList().Any(n => (n as SkillNode).Unlocked.value);
        }
    }
}