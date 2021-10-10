using NodeCanvas.Framework;
using ParadoxNotion;
using ParadoxNotion.Design;
using UnityEngine;

namespace SkillTree.NodeCanvas
{
    abstract public class STNode : Node 
    {
        sealed public override System.Type outConnectionType { get { return typeof(STConnection); } }
        public override bool allowAsPrime { get { return false; } }
        sealed public override bool canSelfConnect { get { return false; } }
        public override Alignment2x2 commentsAlignment { get { return Alignment2x2.Bottom; } }
        public override Alignment2x2 iconAlignment { get { return Alignment2x2.Default; } }
        public override int maxInConnections { get { return 10; } }
        public override int maxOutConnections { get { return 10; } }
    }
}