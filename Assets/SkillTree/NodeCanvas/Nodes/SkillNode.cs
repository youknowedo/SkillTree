using NodeCanvas.Framework;
using ParadoxNotion;
using ParadoxNotion.Design;
using UnityEngine;


namespace SkillTree.NodeCanvas
{

    [Name("Skill")]
    [Description("A skill to be displayed on the Skill Tree")]
    public class SkillNode : STNode
    {
        [SerializeField]
        public Skill skill = new Skill();
    }
}