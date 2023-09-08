using AdventOfCode2022web;
using AdventOfCode2022web.Puzzles;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddSingleton(sp => new PuzzleHelper());

builder.Services.AddScoped(sp => new PuzzleSolutionViewModel<CalorieCounting>(new CalorieCounting()));
builder.Services.AddScoped(sp => new PuzzleBasicSolutionViewModel<RockPaperScissors>(new RockPaperScissors()));
builder.Services.AddScoped(sp => new PuzzleBasicSolutionViewModel<RucksackReorganization>(new RucksackReorganization()));
builder.Services.AddScoped(sp => new PuzzleSolutionViewModel<CampCleanup>(new CampCleanup()));
builder.Services.AddScoped(sp => new PuzzleBasicSolutionViewModel<SupplyStacks>(new SupplyStacks()));
builder.Services.AddScoped(sp => new PuzzleBasicSolutionViewModel<TuningTrouble>(new TuningTrouble()));
builder.Services.AddScoped(sp => new PuzzleBasicSolutionViewModel<NoSpaceLeftOnDevice>(new NoSpaceLeftOnDevice()));
builder.Services.AddScoped(sp => new PuzzleBasicSolutionViewModel<TreetopTreeHouse>(new TreetopTreeHouse()));
builder.Services.AddScoped(sp => new PuzzleSolutionViewModel<RopeBridge>(new RopeBridge()));
builder.Services.AddScoped(sp => new PuzzleSolutionViewModel<CathodeRayTube>(new CathodeRayTube()));
builder.Services.AddScoped(sp => new PuzzleBasicSolutionViewModel<MonkeyInTheMiddle>(new MonkeyInTheMiddle()));
builder.Services.AddScoped(sp => new PuzzleBasicSolutionViewModel<HillClimbingAlgorithm>(new HillClimbingAlgorithm()));
builder.Services.AddScoped(sp => new PuzzleBasicSolutionViewModel<DistressSignalUsingJson>(new DistressSignalUsingJson()));
builder.Services.AddScoped(sp => new PuzzleSolutionViewModel<RegolithReservoir>(new RegolithReservoir()));
builder.Services.AddScoped(sp => new PuzzleSolutionViewModel<BeaconExclusionZone>(new BeaconExclusionZone()));
builder.Services.AddScoped(sp => new PuzzleBasicSolutionViewModel<ProboscideaVolcanium>(new ProboscideaVolcanium()));
builder.Services.AddScoped(sp => new PuzzleBasicSolutionViewModel<PyroclasticFlow>(new PyroclasticFlow()));
builder.Services.AddScoped(sp => new PuzzleSolutionViewModel<BoilingBoulders>(new BoilingBoulders()));
builder.Services.AddScoped(sp => new PuzzleSolutionViewModel<AdventOfCode2022.PuzzleSolutions.NotEnoughMinerals.NotEnoughMineralsSolution>(new AdventOfCode2022.PuzzleSolutions.NotEnoughMinerals.NotEnoughMineralsSolution()));
builder.Services.AddScoped(sp => new PuzzleBasicSolutionViewModel<GrovePositioningSystem>(new GrovePositioningSystem()));
builder.Services.AddScoped(sp => new PuzzleBasicSolutionViewModel<MonkeyMath>(new MonkeyMath()));
builder.Services.AddScoped(sp => new PuzzleSolutionViewModel<MonkeyMap>(new MonkeyMap()));
builder.Services.AddScoped(sp => new PuzzleSolutionViewModel<FullOfHotAir>(new FullOfHotAir()));
builder.Services.AddScoped(sp => new PuzzleBasicSolutionViewModel<DistressSignal>(new DistressSignal()));
builder.Services.AddScoped(sp => new PuzzleSolutionViewModel<Sudoku>(new Sudoku()));

builder.Services.AddScoped(sp => new PuzzleSolutionViewModel<UnstableDiffusion>(new UnstableDiffusion()));
builder.Services.AddScoped(sp => new PuzzleSolutionViewModel<BlizzardBasin>(new BlizzardBasin()));

await builder.Build().RunAsync();
