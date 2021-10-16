using UnityEngine;
using NodeCanvas.Framework;
using ParadoxNotion;
using System.Linq;

namespace SkillTree.NodeCanvas
{
    [AddComponentMenu("NodeCanvas/UGS/Skill Tree Controller")]
    public class SkillTreeController : GraphOwner<SkillTreeGraph>
    {

        public int SkillPoints { get; private set; }

        public void AddSkillPoint(int points) => SkillPoints += points;

        public void RemoveSkillPoint(int points)
        {
            SkillPoints -= points;
            if (SkillPoints < 0)
                SkillPoints = 0;
        }

        public bool UnlockSkill(string skillName)
        {
            var skill = graph.allNodes.Where(n => n.name.Equals(skillName)).FirstOrDefault() as SkillNode;
            if (skill == null)
            {
                Debug.Log("Skill not found");
                return false;
            }
            if (skill.Unlocked.value)
            {
                Debug.Log("Skill already unlocked!");
                return false;
            }
            if (!skill.HasDependenciesUnlocked())
            {
                Debug.Log("Skill cannot be unlocked at this point!");
                return false;
            }
            if (skill.Cost.value > SkillPoints)
            {
                Debug.Log("Can't afford skill!");
                return false;
            }
            SkillPoints -= skill.Cost.value;
            skill.Unlocked = true;
            return true;
        }

        public bool HasUnlockedSkill(string skillName)
        {
            return (graph.allNodes.Where(n => n.name.Equals(skillName)).FirstOrDefault() as SkillNode)?.Unlocked.value ?? throw new System.Exception("Skill not found!");
        }

        
    }
}