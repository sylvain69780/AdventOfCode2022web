// See https://aka.ms/new-console-template for more information
using AdventOfCode2022web.Puzzles;


var puzzleInput = @"Valve AA has flow rate=0; tunnels lead to valves DD, II, BB
Valve BB has flow rate=13; tunnels lead to valves CC, AA
Valve CC has flow rate=2; tunnels lead to valves DD, BB
Valve DD has flow rate=20; tunnels lead to valves CC, AA, EE
Valve EE has flow rate=3; tunnels lead to valves FF, DD
Valve FF has flow rate=0; tunnels lead to valves EE, GG
Valve GG has flow rate=0; tunnels lead to valves FF, HH
Valve HH has flow rate=22; tunnel leads to valve GG
Valve II has flow rate=0; tunnels lead to valves AA, JJ
Valve JJ has flow rate=21; tunnel leads to valve II".Replace("\r","");

var puzzleInput2 = @"Valve PL has flow rate=4; tunnels lead to valves LI, GD, LB, IA, LZ
Valve LB has flow rate=0; tunnels lead to valves PL, VR
Valve QS has flow rate=0; tunnels lead to valves MG, YL
Valve RM has flow rate=17; tunnels lead to valves OQ, UN
Valve QM has flow rate=0; tunnels lead to valves RD, RO
Valve LI has flow rate=0; tunnels lead to valves AF, PL
Valve VR has flow rate=0; tunnels lead to valves YL, LB
Valve SJ has flow rate=0; tunnels lead to valves RO, TU
Valve PZ has flow rate=14; tunnels lead to valves KU, HE
Valve OQ has flow rate=0; tunnels lead to valves RM, OC
Valve YT has flow rate=0; tunnels lead to valves PX, IO
Valve TU has flow rate=5; tunnels lead to valves WS, GZ, MG, SJ, GD
Valve PC has flow rate=7; tunnels lead to valves RY, WK, OG, PD
Valve HE has flow rate=0; tunnels lead to valves PZ, OG
Valve IO has flow rate=20; tunnels lead to valves YT, TX
Valve OC has flow rate=19; tunnels lead to valves OQ, PD
Valve AA has flow rate=0; tunnels lead to valves NY, IA, WK, FU, NU
Valve UN has flow rate=0; tunnels lead to valves JY, RM
Valve NY has flow rate=0; tunnels lead to valves AA, WA
Valve HU has flow rate=0; tunnels lead to valves WA, RC
Valve GD has flow rate=0; tunnels lead to valves PL, TU
Valve WK has flow rate=0; tunnels lead to valves PC, AA
Valve RY has flow rate=0; tunnels lead to valves PV, PC
Valve GX has flow rate=0; tunnels lead to valves QX, YL
Valve RC has flow rate=0; tunnels lead to valves HU, RL
Valve TX has flow rate=0; tunnels lead to valves IO, WA
Valve PV has flow rate=12; tunnel leads to valve RY
Valve PP has flow rate=25; tunnel leads to valve KU
Valve RL has flow rate=9; tunnel leads to valve RC
Valve OG has flow rate=0; tunnels lead to valves PC, HE
Valve PD has flow rate=0; tunnels lead to valves OC, PC
Valve RO has flow rate=8; tunnels lead to valves SJ, QX, MO, QM
Valve QX has flow rate=0; tunnels lead to valves GX, RO
Valve WA has flow rate=6; tunnels lead to valves TX, AF, RG, HU, NY
Valve PX has flow rate=0; tunnels lead to valves YT, OE
Valve GZ has flow rate=0; tunnels lead to valves TU, FU
Valve RG has flow rate=0; tunnels lead to valves OE, WA
Valve MG has flow rate=0; tunnels lead to valves QS, TU
Valve AF has flow rate=0; tunnels lead to valves WA, LI
Valve WS has flow rate=0; tunnels lead to valves ND, TU
Valve OE has flow rate=18; tunnels lead to valves RG, PX
Valve YL has flow rate=3; tunnels lead to valves VR, GX, QS, NU
Valve ND has flow rate=0; tunnels lead to valves JY, WS
Valve FU has flow rate=0; tunnels lead to valves GZ, AA
Valve NU has flow rate=0; tunnels lead to valves YL, AA
Valve JY has flow rate=11; tunnels lead to valves UN, RD, ND
Valve IA has flow rate=0; tunnels lead to valves AA, PL
Valve KU has flow rate=0; tunnels lead to valves PZ, PP
Valve RD has flow rate=0; tunnels lead to valves JY, QM
Valve MO has flow rate=0; tunnels lead to valves RO, LZ
Valve LZ has flow rate=0; tunnels lead to valves PL, MO".Replace("\r", "");


var solver = new ProboscideaVolcanium();

Console.WriteLine(solver.SolveFirstPart(puzzleInput));

Console.WriteLine(solver.SolveFirstPart(puzzleInput2));

Console.WriteLine(solver.SolveSecondPart(puzzleInput));

Console.WriteLine(solver.SolveSecondPart(puzzleInput2));
