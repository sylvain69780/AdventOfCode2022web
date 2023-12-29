using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aplenty
{
    public class AplentyPart2Strategy : IPuzzleStrategy<AplentyModel>
    {
        public string Name { get; set; } = "Part 2";

        class Pos
        {
            public string Workflow { get; set; } = "in";
            public int RuleIndex { get; set; } = 0;
            public int XMin { get; set; } = 1;
            public int XMax { get; set; } = 4000;
            public int MMin { get; set; } = 1;
            public int MMax { get; set; } = 4000;
            public int AMin { get; set; } = 1;
            public int AMax { get; set; } = 4000;
            public int SMin { get; set; } = 1;
            public int SMax { get; set; } = 4000;

            public Pos Clone()
            {
                return new Pos()
                {
                    Workflow = Workflow,
                    RuleIndex = RuleIndex,
                    XMin = XMin,
                    XMax = XMax,
                    MMin = MMin,
                    MMax = MMax,
                    AMin = AMin,
                    AMax = AMax,
                    SMin = SMin,
                    SMax = SMax,
                };
            }
        }



        //public IEnumerable<ProcessingProgressModel> GetStepsBAD(AplentyModel model, Func<ProcessingProgressModel> updateContext, Action<string> provideSolution)
        //{
        //    var workflows = model.Workflows!;
        //    var dfs = new Queue<Pos>();
        //    var solutions = new List<Pos>();
        //    foreach (var (name, rules) in workflows)
        //    {
        //        for (var i =0;i<rules.Count;i++)
        //        {
        //            var rule = rules[i];
        //            if (rule.applyRule == "A")
        //            {
        //                var pos = new Pos() { Workflow = name, RuleIndex = i };
        //                if ( i != rules.Count-1)
        //                {
        //                    ApplyConstraint(rule, pos);
        //                }
        //                dfs.Enqueue(pos);
        //            }
                        
        //        }
        //    }
        //    while ( dfs.Count > 0 )
        //    {
        //        var newQueue = new Queue<Pos>();
        //        while ( dfs.TryDequeue(out var pos))
        //        {
        //            if (pos.RuleIndex == 0)
        //                if (pos.Workflow == "in")
        //                    solutions.Add(pos);
        //                else
        //                {
        //                    foreach (var (name, rules) in workflows)
        //                    {
        //                        for (var i = 0; i < rules.Count; i++)
        //                        {
        //                            var rule = rules[i];
        //                            if (rule.applyRule == pos.Workflow)
        //                            {
        //                                var nextPos = pos.Clone();
        //                                nextPos.RuleIndex = i;
        //                                nextPos.Workflow = name;
        //                                if (i != rules.Count - 1)
        //                                {
        //                                    ApplyConstraint(rule, nextPos);
        //                                }
        //                                newQueue.Enqueue(nextPos);
        //                            }
        //                        }
        //                    }
        //                }
        //            else
        //            {
        //                var (name, rules) = workflows.Single(x => x.name == pos.Workflow);
        //                pos.RuleIndex--;
        //                var rule = rules[pos.RuleIndex];
        //                    ApplyConstraint(rule, pos);
        //                newQueue.Enqueue(pos);
        //            }
        //        }
        //        dfs = newQueue;
        //    }
        //    // integrate intervals
        //    var count = 0L;

        //    yield return updateContext();
        //    provideSolution(count.ToString());

        //}

        public IEnumerable<ProcessingProgressModel> GetSteps(AplentyModel model, Func<ProcessingProgressModel> updateContext, Action<string> provideSolution)
        {
            var workflows = model.Workflows!;
            var dfs = new Queue<Pos>();
            dfs.Enqueue(new Pos() { Workflow = "in", RuleIndex = 0 });
            var solutions = new List<Pos>();
            while (dfs.Count > 0 )
            {
                var newDfs = new Queue<Pos>();
                while ( dfs.TryDequeue(out var pos))
                {
                    var (name, rules) = workflows.Single(x => x.name == pos.Workflow);
                    var rule = rules[pos.RuleIndex];
                    if (pos.RuleIndex != rules.Count - 1)
                    {
                        var pos2 = pos.Clone();
                        pos2.RuleIndex = 0;
                        pos2.Workflow = rule.applyRule;
                        ApplyConstraint(rule, pos2);
                        if (rule.applyRule == "A")
                            solutions.Add(pos2);
                        else if ( rule.applyRule != "R")
                            newDfs.Enqueue(pos2);
                        ApplyAntiConstraint(rule, pos);
                        pos.RuleIndex++;
                        newDfs.Enqueue(pos);
                    } else if (rule.applyRule == "A")
                    {
                        solutions.Add(pos);
                        continue;
                    } else if (rule.applyRule != "R")
                    {
                        pos.RuleIndex = 0;
                        pos.Workflow = rule.applyRule;
                        newDfs.Enqueue(pos);
                    }

                }
                dfs = newDfs;
            }
            var solution = solutions.Select(x => 
                (long)Math.Max(0, x.XMax - x.XMin + 1) *
                (long)Math.Max(0, x.MMax - x.MMin + 1) *
                (long)Math.Max(0, x.AMax - x.AMin + 1) *
                (long)Math.Max(0, x.SMax - x.SMin + 1) 
                ).Sum();
            yield return updateContext();
            provideSolution(solution.ToString());
        }

        static void ApplyConstraint((string name, string oper, int amount, string applyRule) rule, Pos pos)
        {
            if (rule.oper == "<")
            {
                if (rule.name == "x")
                    pos.XMax = rule.amount - 1;
                else if (rule.name == "m")
                    pos.MMax = rule.amount - 1;
                else if (rule.name == "a")
                    pos.AMax = rule.amount - 1;
                else if (rule.name == "s")
                    pos.SMax = rule.amount - 1;
            }
            else if (rule.oper == ">")
            {
                if (rule.name == "x")
                    pos.XMin = rule.amount + 1;
                else if (rule.name == "m")
                    pos.MMin = rule.amount + 1;
                else if (rule.name == "a")
                    pos.AMin = rule.amount + 1;
                else if (rule.name == "s")
                    pos.SMin = rule.amount + 1;
            }
        }
        static void ApplyAntiConstraint((string name, string oper, int amount, string applyRule) rule, Pos pos)
        {
            if (rule.oper == ">")
            {
                if (rule.name == "x")
                    pos.XMax = rule.amount;
                else if (rule.name == "m")
                    pos.MMax = rule.amount;
                else if (rule.name == "a")
                    pos.AMax = rule.amount;
                else if (rule.name == "s")
                    pos.SMax = rule.amount;
            }
            else if (rule.oper == "<")
            {
                if (rule.name == "x")
                    pos.XMin = rule.amount;
                else if (rule.name == "m")
                    pos.MMin = rule.amount;
                else if (rule.name == "a")
                    pos.AMin = rule.amount;
                else if (rule.name == "s")
                    pos.SMin = rule.amount;
            }
        }

    }
}
