using AdventOfCode2022web;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
 using AdventOfCode2022Solutions.PuzzleSolutions;
 using AdventOfCode2022Solutions.PuzzleSolutions.BeaconExclusionZone;
 using AdventOfCode2022Solutions.PuzzleSolutions.BlizzardBasin;
 using AdventOfCode2022Solutions.PuzzleSolutions.BoilingBoulders;
 using AdventOfCode2022Solutions.PuzzleSolutions.CalorieCounting;
 using AdventOfCode2022Solutions.PuzzleSolutions.CampCleanup;
 using AdventOfCode2022Solutions.PuzzleSolutions.CathodeRayTube;
 using AdventOfCode2022Solutions.PuzzleSolutions.DistressSignal;
 using AdventOfCode2022Solutions.PuzzleSolutions.FullOfHotAir;
 using AdventOfCode2022Solutions.PuzzleSolutions.GrovePositioningSystem;
 using AdventOfCode2022Solutions.PuzzleSolutions.HillClimbingAlgorithm;
 using AdventOfCode2022Solutions.PuzzleSolutions.MonkeyInTheMiddle;
 using AdventOfCode2022Solutions.PuzzleSolutions.MonkeyMap;
 using AdventOfCode2022Solutions.PuzzleSolutions.MonkeyMath;
 using AdventOfCode2022Solutions.PuzzleSolutions.NoSpaceLeftOnDevice;
 using AdventOfCode2022Solutions.PuzzleSolutions.NotEnoughMinerals;
 using AdventOfCode2022Solutions.PuzzleSolutions.ProboscideaVolcanium;
 using AdventOfCode2022Solutions.PuzzleSolutions.PyroclasticFlow;
 using AdventOfCode2022Solutions.PuzzleSolutions.RegolithReservoir;
 using AdventOfCode2022Solutions.PuzzleSolutions.RockPaperScissors;
 using AdventOfCode2022Solutions.PuzzleSolutions.RopeBridge;
 using AdventOfCode2022Solutions.PuzzleSolutions.RucksackReorganization;
 using AdventOfCode2022Solutions.PuzzleSolutions.Sudoku;
 using AdventOfCode2022Solutions.PuzzleSolutions.SupplyStacks;
 using AdventOfCode2022Solutions.PuzzleSolutions.TreetopTreeHouse;
 using AdventOfCode2022Solutions.PuzzleSolutions.TuningTrouble;
 using AdventOfCode2022Solutions.PuzzleSolutions.UnstableDiffusion;
var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddSingleton(sp => new PuzzleHelper());

//builder.Services.AddScoped(sp => new PuzzleSolutionViewModel<CalorieCountingSolution>(new CalorieCountingSolution()));
//builder.Services.AddScoped(sp => new PuzzleBasicSolutionViewModel<RockPaperScissorsSolution>(new RockPaperScissorsSolution()));
//builder.Services.AddScoped(sp => new PuzzleBasicSolutionViewModel<RucksackReorganizationSolution>(new RucksackReorganizationSolution()));
//builder.Services.AddScoped(sp => new PuzzleSolutionViewModel<CampCleanupSolution>(new CampCleanupSolution()));
//builder.Services.AddScoped(sp => new PuzzleBasicSolutionViewModel<SupplyStacksSolution>(new SupplyStacksSolution()));
//builder.Services.AddScoped(sp => new PuzzleBasicSolutionViewModel<TuningTroubleSolution>(new TuningTroubleSolution()));
//builder.Services.AddScoped(sp => new PuzzleBasicSolutionViewModel<NoSpaceLeftOnDeviceSolution>(new NoSpaceLeftOnDeviceSolution()));
//builder.Services.AddScoped(sp => new PuzzleBasicSolutionViewModel<TreetopTreeHouseSolution>(new TreetopTreeHouseSolution()));
//builder.Services.AddScoped(sp => new PuzzleSolutionViewModel<RopeBridgeSolution>(new RopeBridgeSolution()));
//builder.Services.AddScoped(sp => new PuzzleSolutionViewModel<CathodeRayTubeSolution>(new CathodeRayTubeSolution()));
//builder.Services.AddScoped(sp => new PuzzleBasicSolutionViewModel<MonkeyInTheMiddleSolution>(new MonkeyInTheMiddleSolution()));
//builder.Services.AddScoped(sp => new PuzzleBasicSolutionViewModel<HillClimbingAlgorithmSolution>(new HillClimbingAlgorithmSolution()));
//builder.Services.AddScoped(sp => new PuzzleBasicSolutionViewModel<DistressSignalUsingJsonSolution>(new DistressSignalUsingJsonSolution()));
//builder.Services.AddScoped(sp => new PuzzleSolutionViewModel<RegolithReservoirSolution>(new RegolithReservoirSolution()));
//builder.Services.AddScoped(sp => new PuzzleSolutionViewModel<BeaconExclusionZoneSolution>(new BeaconExclusionZoneSolution()));
//builder.Services.AddScoped(sp => new PuzzleBasicSolutionViewModel<ProboscideaVolcaniumSolution>(new ProboscideaVolcaniumSolution()));
//builder.Services.AddScoped(sp => new PuzzleBasicSolutionViewModel<PyroclasticFlowSolution>(new PyroclasticFlowSolution()));
//builder.Services.AddScoped(sp => new PuzzleSolutionViewModel<BoilingBouldersSolution>(new BoilingBouldersSolution()));
//builder.Services.AddScoped(sp => new PuzzleSolutionViewModel<NotEnoughMineralsSolution>(new NotEnoughMineralsSolution()));
//builder.Services.AddScoped(sp => new PuzzleBasicSolutionViewModel<GrovePositioningSystemSolution>(new GrovePositioningSystemSolution()));
//builder.Services.AddScoped(sp => new PuzzleBasicSolutionViewModel<MonkeyMathSolution>(new MonkeyMathSolution()));
//builder.Services.AddScoped(sp => new PuzzleSolutionViewModel<MonkeyMapSolution>(new MonkeyMapSolution()));
//builder.Services.AddScoped(sp => new PuzzleSolutionViewModel<FullOfHotAirSolution>(new FullOfHotAirSolution()));
//builder.Services.AddScoped(sp => new PuzzleBasicSolutionViewModel<DistressSignalSolution>(new DistressSignalSolution()));
//builder.Services.AddScoped(sp => new PuzzleSolutionViewModel<SudokuSolution>(new SudokuSolution()));

//builder.Services.AddScoped(sp => new PuzzleSolutionViewModel<UnstableDiffusionSolution>(new UnstableDiffusionSolution()));
//builder.Services.AddScoped(sp => new PuzzleSolutionViewModel<BlizzardBasinSolution>(new BlizzardBasinSolution()));

builder.Services.AddScoped(sp => new BlizzardBasinSolution());
builder.Services.AddScoped(sp => new BeaconExclusionZoneSolution());
builder.Services.AddScoped(sp => new CalorieCountingSolution());
builder.Services.AddScoped(sp => new RockPaperScissorsSolution());
builder.Services.AddScoped(sp => new RucksackReorganizationSolution());
builder.Services.AddScoped(sp => new CampCleanupSolution());
builder.Services.AddScoped(sp => new SupplyStacksSolution());
builder.Services.AddScoped(sp => new TuningTroubleSolution());
builder.Services.AddScoped(sp => new NoSpaceLeftOnDeviceSolution());
builder.Services.AddScoped(sp => new TreetopTreeHouseSolution());
builder.Services.AddScoped(sp => new RopeBridgeSolution());
builder.Services.AddScoped(sp => new CathodeRayTubeSolution());
builder.Services.AddScoped(sp => new MonkeyInTheMiddleSolution());
builder.Services.AddScoped(sp => new HillClimbingAlgorithmSolution());
builder.Services.AddScoped(sp => new DistressSignalUsingJsonSolution());
builder.Services.AddScoped(sp => new RegolithReservoirSolution());
builder.Services.AddScoped(sp => new BeaconExclusionZoneSolution());
builder.Services.AddScoped(sp => new ProboscideaVolcaniumSolution());
builder.Services.AddScoped(sp => new PyroclasticFlowSolution());
builder.Services.AddScoped(sp => new BoilingBouldersSolution());
builder.Services.AddScoped(sp => new NotEnoughMineralsSolution());
builder.Services.AddScoped(sp => new GrovePositioningSystemSolution());
builder.Services.AddScoped(sp => new MonkeyMathSolution());
builder.Services.AddScoped(sp => new MonkeyMapSolution());
builder.Services.AddScoped(sp => new FullOfHotAirSolution());
builder.Services.AddScoped(sp => new DistressSignalSolution());
builder.Services.AddScoped(sp => new SudokuSolution());



await builder.Build().RunAsync();
