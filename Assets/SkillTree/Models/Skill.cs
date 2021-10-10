using UnityEngine;

namespace SkillTree
{
    [System.Serializable]
    public class Skill
    {
        [SerializeField]
        public int price = 0;

        [HideInInspector]
        public bool unlocked;

        [HideInInspector]
        public bool unlockable;
    }
}