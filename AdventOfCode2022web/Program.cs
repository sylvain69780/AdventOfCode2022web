using Microsoft.AspNetCore.Components.Web;
using Blazor;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Domain.BeaconExclusionZone;
using Domain.BlizzardBasin;
using Domain.BoilingBoulders;
using Domain.CalorieCounting;
using Domain.CampCleanup;
using Domain.CathodeRayTube;
using Domain.DistressSignal;
using Domain.FullOfHotAir;
using Domain.GrovePositioningSystem;
using Domain.HillClimbingAlgorithm;
using Domain.MonkeyInTheMiddle;
using Domain.MonkeyMap;
using Domain.MonkeyMath;
using Domain.NoSpaceLeftOnDevice;
using Domain.NotEnoughMinerals;
using Domain.ProboscideaVolcanium;
using Domain.PyroclasticFlow;
using Domain.RegolithReservoir;
using Domain.RockPaperScissors;
using Domain.RopeBridge;
using Domain.RucksackReorganization;
using Domain.Sudoku;
using Domain.SupplyStacks;
using Domain.TreetopTreeHouse;
using Domain.TuningTrouble;
using Domain.UnstableDiffusion;
using Domain;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddTransient<CalorieCountingService, CalorieCountingService>();
builder.Services.AddTransient<IPuzzleStrategy<CalorieCountingModel>, CalorieCountingPart1Strategy>();
builder.Services.AddTransient<IPuzzleStrategy<CalorieCountingModel>, CalorieCountingPart2Strategy>();

builder.Services.AddTransient<RockPaperScissorsService, RockPaperScissorsService>();
builder.Services.AddTransient<IPuzzleStrategy<RockPaperScissorsModel>, RockPaperScissorsPart1Strategy>();
builder.Services.AddTransient<IPuzzleStrategy<RockPaperScissorsModel>, RockPaperScissorsPart2Strategy>();

builder.Services.AddTransient<RucksackReorganizationService, RucksackReorganizationService>();
builder.Services.AddTransient<IPuzzleStrategy<RucksackReorganizationModel>, RucksackReorganizationPart1Strategy>();
builder.Services.AddTransient<IPuzzleStrategy<RucksackReorganizationModel>, RucksackReorganizationPart2Strategy>();

// https://dev.to/davidkroell/strategy-design-pattern-with-dependency-injection-7ba
// https://adamstorr.azurewebsites.net/blog/aspnetcore-and-the-strategy-pattern
// to remove
builder.Services.AddScoped(sp => new BlizzardBasinSolution());
builder.Services.AddScoped(sp => new BeaconExclusionZoneSolution());
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
