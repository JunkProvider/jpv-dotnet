using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq.Expressions;
using SpaceConsole.ConsoleApp.Commands;
using SpaceConsole.ConsoleApp.Commands.Invoker;
using SpaceConsole.ConsoleApp.Model;
using SpaceConsole.ConsoleApp.Model.Celestials;
using SpaceConsole.ConsoleApp.Model.Items;
using SpaceConsole.ConsoleApp.Quantities;
using SpaceConsole.ConsoleApp.UserControls;
using SpaceConsole.ConsoleApp.ViewModel;
using Console = SpaceConsole.ConsoleApp.Rendering.Console;

namespace SpaceConsole.ConsoleApp
{
    public sealed class Program
    {
        private Console Console { get; } = new Console(120, 30, "Space Console Version 0.1.0");

        private IngameControl MainControl { get; }

        private World World { get; }

        private IngameModel ViewModel { get; }

        private CommandService CommandService { get; }

        private bool IsRunning { get; set; }

        public static void Main(string[] args)
        {
            new Program().Run();
        }

        private Program()
        {
            var asteroidRadius = 10.Kilometer();
            var moonRadius = 800.Kilometer();
            var rockPlanetRadius = 6500.Kilometer();
            var gasGiantRadius = 90000.Kilometer();
            var starRadius = 100 * gasGiantRadius;
            var blackHoleRadius = 100 * starRadius;

            var factory = new CelestialFactory();

            var universe = factory.Create(
                systemName: "Universe",
                name: "Universe Center",
                orbit: 0,
                radius: 1,
                density: 1,
                children: new []
                {
                    factory.Create(
                        systemName: "Milkiway",
                        name: "Black Hole",
                        orbit: Math.Pow(10, 9).AstronomicUnits(),
                        radius: blackHoleRadius,
                        density: 1000.TonsPerCubicMeter(),
                        children: new []
                        {
                            factory.Create(
                                name: "Sun",
                                orbit: Math.Pow(10, 6).AstronomicUnits(),
                                radius: 696342.Kilometer(),
                                density: 1.408.GramPerCubicCentimeter(),
                                children: new []
                                {
                                    factory.Create(
                                        name: "Mercury",
                                        orbit: 0.387098.AstronomicUnits(),
                                        radius: 2439.7.Kilometer(),
                                        density: 5.427.GramPerCubicCentimeter()),
                                    factory.Create(
                                        name: "Venus",
                                        orbit: 0.723332.AstronomicUnits(),
                                        radius: 6051.8.Kilometer(),
                                        density: 5.243.GramPerCubicCentimeter()),
                                    factory.Create(
                                        name: "Earth",
                                        orbit: 1.AstronomicUnits(),
                                        radius: 6371.Kilometer(),
                                        density: 5.514.GramPerCubicCentimeter(),
                                        children: new []
                                        {
                                            factory.Create(
                                                name: "Moon",
                                                orbit: 384399.Kilometer(),
                                                radius: 1737.1.Kilometer(),
                                                density: 3.344.GramPerCubicCentimeter())
                                        }),
                                    factory.Create(
                                        name: "Mars",
                                        orbit: 1.523679.AstronomicUnits(),
                                        radius: 3389.5.Kilometer(),
                                        density: 3.9335.GramPerCubicCentimeter()),
                                    factory.Create(
                                        name: "Jupiter",
                                        orbit: 5.2044.AstronomicUnits(),
                                        radius: 69911.Kilometer(),
                                        density: 1.326.GramPerCubicCentimeter()),
                                    factory.Create(
                                        name: "Saturn",
                                        orbit: 9.5826.AstronomicUnits(),
                                        radius: 58232.Kilometer(),
                                        density: 0.687.GramPerCubicCentimeter()),
                                    factory.Create(
                                        name: "Uranus",
                                        orbit: 19.2184.AstronomicUnits(),
                                        radius: 25362.Kilometer(),
                                        density: 1.27.GramPerCubicCentimeter()),
                                    factory.Create(
                                        name: "Neptune",
                                        orbit: 30.11.AstronomicUnits(),
                                        radius: 24622.Kilometer(),
                                        density: 1.638.GramPerCubicCentimeter()),
                                    factory.Create(
                                        name: "Pluto",
                                        orbit: 39.48.AstronomicUnits(),
                                        radius: 1188.3.Kilometer(),
                                        density: 1.854.GramPerCubicCentimeter())
                                })
                        })
                });

            var ship = new Ship
            {
                Crew = 3,
                PowerOutput = 100,
                PowerUsage = 80,
                Credits = 1000
            };

            World = new World {Ship = ship};

            var earth = universe.GetDescendent("Earth System");

            World.Stations.Add(CreateStation(earth));

            World.Ship.CurrentStation = World.Stations[0];

            foreach (var system in earth.GetSiblings())
            {
                World.Stations.Add(CreateStation(system));
            }

            ViewModel = new IngameModel(World);
            ViewModel.Update();

            CommandService = new CommandService(ViewModel);

            MainControl = new IngameControl(ViewModel);
        }

        private static Station CreateStation(ICelestialSystem system)
        {
            return new Station($"{system.CentralBody.Name} Station", system, 200000.Kilometer() + system.CentralBody.Radius * 100)
            {
                OfferedItems = new List<MarketplaceItem>
                {
                    new MarketplaceItem(new ItemStack(new LiquidElementItem(Elements.Hydrogen), 5000), new decimal(0.11)),
                    new MarketplaceItem(new ItemStack(new LiquidElementItem(Elements.Oxygen), 5000), new decimal(1.8)),
                },
                RequestedItems = new List<MarketplaceItem>
                {
                    new MarketplaceItem(new ItemStack(new LiquidElementItem(Elements.Hydrogen), 5000), new decimal(0.1)),
                    new MarketplaceItem(new ItemStack(new LiquidElementItem(Elements.Oxygen), 5000), new decimal(1.6))
                }
            };
        }

        public void Run()
        {
            IsRunning = true;

            Render();

            while (IsRunning)
            {
                if (!HandleInput(Console.ReadLine()))
                    continue;

                ViewModel.UserInput = string.Empty;
                Render();

            }
        }

        private void Render()
        {
            ViewModel.Update();
            MainControl.Update();
            MainControl.Meassure();
            MainControl.Arrange(new Size(Console.Width, Console.Height - 3));
            Console.Write(MainControl.Render(), TimeSpan.Zero/*.FromMilliseconds(20)*/, TimeSpan.Zero /*.FromMilliseconds(0.2)*/);
            Console.WriteLine(ViewModel.UserFeedback);
            Console.Write(" ");
        }

        private bool HandleInput(string line)
        {
            HandleCommand(line);
            return true;
        }

        private void HandleCommand(string input)
        {
            ViewModel.ClearMessage();

            if (string.IsNullOrWhiteSpace(input))
                return;

            var command = CommandParser.Parse(input);

            if (!CommandService.Invoke(command))
                ViewModel.SetMessage($"Unknown command {input}");
        }
    }
}
