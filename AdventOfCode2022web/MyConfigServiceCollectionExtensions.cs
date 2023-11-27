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

            services.AddTransient<SudokuService, SudokuService>();
            services.AddTransient<IPuzzleStrategy<SudokuModel>, SudokuStrategy>();

            return services;
        }
    }
}
