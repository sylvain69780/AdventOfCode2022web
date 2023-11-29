using Domain.CalorieCounting;
using Domain.CampCleanup;
using Domain.NoSpaceLeftOnDevice;
using Domain.RockPaperScissors;
using Domain.RucksackReorganization;
using Domain.Sudoku;
using Domain.SupplyStacks;
using Domain.TreetopTreeHouse;
using Domain.TuningTrouble;
using Domain;
using Domain.RopeBridge;
using Domain.CathodeRayTube;
using Domain.MonkeyInTheMiddle;
using Domain.HillClimbingAlgorithm;
using Domain.DistressSignal;
using Domain.RegolithReservoir;
using Domain.BeaconExclusionZone;
using Domain.ProboscideaVolcanium;

namespace Blazor
{
    public static class MyConfigServiceCollectionExtensions
    {
        public static IServiceCollection AddPuzzles(this IServiceCollection services)
        {
            services.AddTransient<CalorieCountingService, CalorieCountingService>();
            services.AddTransient<IPuzzleStrategy<CalorieCountingModel>, CalorieCountingPart1Strategy>();
            services.AddTransient<IPuzzleStrategy<CalorieCountingModel>, CalorieCountingPart2Strategy>();

            services.AddTransient<RockPaperScissorsService, RockPaperScissorsService>();
            services.AddTransient<IPuzzleStrategy<RockPaperScissorsModel>, RockPaperScissorsPart1Strategy>();
            services.AddTransient<IPuzzleStrategy<RockPaperScissorsModel>, RockPaperScissorsPart2Strategy>();

            services.AddTransient<RucksackReorganizationService, RucksackReorganizationService>();
            services.AddTransient<IPuzzleStrategy<RucksackReorganizationModel>, RucksackReorganizationPart1Strategy>();
            services.AddTransient<IPuzzleStrategy<RucksackReorganizationModel>, RucksackReorganizationPart2Strategy>();

            services.AddTransient<CampCleanupService, CampCleanupService>();
            services.AddTransient<IPuzzleStrategy<CampCleanupModel>, CampCleanupPart1Strategy>();
            services.AddTransient<IPuzzleStrategy<CampCleanupModel>, CampCleanupPart2Strategy>();

            services.AddTransient<SupplyStacksService, SupplyStacksService>();
            services.AddTransient<IPuzzleStrategy<SupplyStacksModel>, SupplyStacksPart1Strategy>();
            services.AddTransient<IPuzzleStrategy<SupplyStacksModel>, SupplyStacksPart2Strategy>();

            services.AddTransient<TuningTroubleService, TuningTroubleService>();
            services.AddTransient<IPuzzleStrategy<TuningTroubleModel>, TuningTroublePart1Strategy>();
            services.AddTransient<IPuzzleStrategy<TuningTroubleModel>, TuningTroublePart2Strategy>();

            services.AddTransient<NoSpaceLeftOnDeviceService, NoSpaceLeftOnDeviceService>();
            services.AddTransient<IPuzzleStrategy<NoSpaceLeftOnDeviceModel>, NoSpaceLeftOnDevicePart1Strategy>();
            services.AddTransient<IPuzzleStrategy<NoSpaceLeftOnDeviceModel>, NoSpaceLeftOnDevicePart2Strategy>();

            services.AddTransient<TreetopTreeHouseService, TreetopTreeHouseService>();
            services.AddTransient<IPuzzleStrategy<TreetopTreeHouseModel>, TreetopTreeHousePart1Strategy>();
            services.AddTransient<IPuzzleStrategy<TreetopTreeHouseModel>, TreetopTreeHousePart2Strategy>();

            services.AddTransient<RopeBridgeService, RopeBridgeService>();
            services.AddTransient<IPuzzleStrategy<RopeBridgeModel>, RopeBridgePart1Strategy>();
            services.AddTransient<IPuzzleStrategy<RopeBridgeModel>, RopeBridgePart2Strategy>();

            services.AddTransient<CathodeRayTubeService, CathodeRayTubeService>();
            services.AddTransient<IPuzzleStrategy<CathodeRayTubeModel>, CathodeRayTubePart1Strategy>();
            services.AddTransient<IPuzzleStrategy<CathodeRayTubeModel>, CathodeRayTubePart2Strategy>();

            services.AddTransient<MonkeyInTheMiddleService, MonkeyInTheMiddleService>();
            services.AddTransient<IPuzzleStrategy<MonkeyInTheMiddleModel>, MonkeyInTheMiddlePart1Strategy>();
            services.AddTransient<IPuzzleStrategy<MonkeyInTheMiddleModel>, MonkeyInTheMiddlePart2Strategy>();

            services.AddTransient<HillClimbingAlgorithmService, HillClimbingAlgorithmService>();
            services.AddTransient<IPuzzleStrategy<HillClimbingAlgorithmModel>, HillClimbingAlgorithmPart1Strategy>();
            services.AddTransient<IPuzzleStrategy<HillClimbingAlgorithmModel>, HillClimbingAlgorithmPart2Strategy>();

            services.AddTransient<DistressSignalService, DistressSignalService>();
            services.AddTransient<IPuzzleStrategy<DistressSignalModel>, DistressSignalPart1Strategy>();
            services.AddTransient<IPuzzleStrategy<DistressSignalModel>, DistressSignalPart2Strategy>();
            services.AddTransient<IPuzzleStrategy<DistressSignalModel>, DistressSignalPart1JsonStrategy>();
            services.AddTransient<IPuzzleStrategy<DistressSignalModel>, DistressSignalPart2JsonStrategy>();

            services.AddTransient<SudokuService, SudokuService>();
            services.AddTransient<IPuzzleStrategy<SudokuModel>, SudokuStrategy>();

            services.AddTransient<RegolithReservoirService, RegolithReservoirService>();
            services.AddTransient<IPuzzleStrategy<RegolithReservoirModel>, RegolithReservoirPart1Strategy>();
            services.AddTransient<IPuzzleStrategy<RegolithReservoirModel>, RegolithReservoirPart2Strategy>();

            services.AddTransient<BeaconExclusionZoneService, BeaconExclusionZoneService>();
            services.AddTransient<IPuzzleStrategy<BeaconExclusionZoneModel>, BeaconExclusionZonePart1Strategy>();
            services.AddTransient<IPuzzleStrategy<BeaconExclusionZoneModel>, BeaconExclusionZonePart2Strategy>();

            services.AddTransient<ProboscideaVolcaniumService, ProboscideaVolcaniumService>();
            services.AddTransient<IPuzzleStrategy<ProboscideaVolcaniumModel>, ProboscideaVolcaniumPart1Strategy>();
            services.AddTransient<IPuzzleStrategy<ProboscideaVolcaniumModel>, ProboscideaVolcaniumPart2Strategy>();

            return services;
        }
    }
}
