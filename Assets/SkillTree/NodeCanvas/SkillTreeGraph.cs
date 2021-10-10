using UnityEngine;
using NodeCanvas.Framework;
using ParadoxNotion;

namespace SkillTree.NodeCanvas
{
    [GraphInfo(
        packageName = "SkillTree",
        docsURL = "http://untitled-gaming.com/",
        resourcesURL = "http://untitled-gaming.com/",
        forumsURL = "http://untitled-gaming.com/"
    )]
    [CreateAssetMenu(menuName = "UGS/Skill Tree Asset")]
    public class SkillTreeGraph : Graph
    {    
        public override System.Type baseNodeType => typeof(STNode);
        public override bool requiresAgent => true;
        public override bool requiresPrimeNode => true;
        public override bool isTree => true;
        public override bool allowBlackboardOverrides => true;
        sealed public override bool canAcceptVariableDrops => false;
    }

}