using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using dotless.Core.Plugins;
using dotless.Core.Parser.Tree;
using System.Drawing;


namespace LiteReg.Web.Infrastructure
{
    public class LessImportPlugin : VisitorPlugin
    {
        private class IgnoreCommentMarker
        {
            public int Start {get;set;}
            public int End { get; set; }
        }

        public override VisitorPluginType AppliesTo
        {
            get { return VisitorPluginType.AfterEvaluation; }
        }

        public override dotless.Core.Parser.Infrastructure.Nodes.Node Execute(dotless.Core.Parser.Infrastructure.Nodes.Node node, out bool visitDeeper)
        {
            visitDeeper = false;
            Root tree = node as Root;
            Comment c = null;
            List<IgnoreCommentMarker> markers = new List<IgnoreCommentMarker>();
            IgnoreCommentMarker marker = null;

            if (tree != null && tree.Rules != null)
            {
                for (int ruleCount = 0; ruleCount < tree.Rules.Count; ruleCount++)
                {
                    c = tree.Rules[ruleCount] as Comment;
                    if (c != null)
                    {
                        if (c.Value == "// start-ignore")
                        {
                            marker = new IgnoreCommentMarker();
                            marker.Start = ruleCount;
                        }
                        if (c.Value == "// end-ignore")
                        {
                            if (marker != null)
                            {
                                marker.End = ruleCount;
                                markers.Add(marker);
                                marker = null;
                            }
                        }
                    }
                }

                foreach (var m in markers)
                {
                    for (int ruleCount = m.Start; ruleCount <= m.End; ruleCount++)
                    {
                        tree.Rules[ruleCount].ForceIgnoreOutput = true;
                    }
                }
            }

            return node;
        }


    }
}