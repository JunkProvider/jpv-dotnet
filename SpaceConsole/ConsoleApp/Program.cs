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
                                        orbit: 0.3.AstronomicUnits(),
                                        radius: rockPlanetRadius / 5,
                                        density: 3.TonsPerCubicMeter()),
                                    factory.Create(
                                        name: "Venus",
                                        orbit: 0.6.AstronomicUnits(),
                                        radius: rockPlanetRadius * 1.1,
                                        density: 5.TonsPerCubicMeter()),
                                    factory.Create(
                                        name: "Earth",
                                        orbit: 1.AstronomicUnits(),
                                        radius: 6371.Kilometer(),
                                        density: 5.514.GramPerCubicCentimeter(),
                                        children: new []
                                        {
                                            factory.Create(
                                                name: "Moon",
                                                orbit: rockPlanetRadius * 100,
                                                radius: moonRadius,
                                                density: 2.5.TonsPerCubicMeter())
                                        }),
                                    factory.Create(
                                        name: "Mars",
                                        orbit: 1.523679.AstronomicUnits(),
                                        radius: 3389.5.Kilometer(),
                                        density: 3.9335.GramPerCubicCentimeter()),
                                    factory.Create(
                                        name: "Jupiter",
                                        orbit: 4.AstronomicUnits(),
                                        radius: gasGiantRadius,
                                        density: 2.TonsPerCubicMeter())
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
            ship.CargoBay.Add(new ItemStack(new LiquidElementItem(Elements.Oxygen), 150));
            ship.CargoBay.Add(new ItemStack(new BakedBeans(), 20));

            World = new World {Ship = ship};

            World.Stations.Add(new Station("Earth Station", universe.GetDescendent("Earth System"), 300e3.Kilometer())
            {
                OfferedItems = new List<MarketplaceItem>
                {
                    new MarketplaceItem(new ItemStack(new LiquidElementItem(Elements.Hydrogen), 5000), new decimal(0.1)),
                    new MarketplaceItem(new ItemStack(new BakedBeans(), 40), new decimal(2.5))
                },
                RequestedItems = new List<MarketplaceItem>
                {
                    new MarketplaceItem(new ItemStack(new LiquidElementItem(Elements.Oxygen), 5000), new decimal(0.1))
                }
            });
            /* World.Stations.Add(new Station("Bob's Workshop", new Vector(1.85.AstronomicUnits(), 0))
            {
                RequestedItems = new List<MarketplaceItem>
                {
                    new MarketplaceItem(new ItemStack(new BakedBeans(), 100), new decimal(3.1))
                }
            });
            World.Stations.Add(new Station("Europa Hydroponics", new Vector(5.0.AstronomicUnits(), 5.0.AstronomicUnits()))); */
            World.Ship.CurrentStation = World.Stations[0];

            World.Stations.Add(new Station("Mars Station", universe.GetDescendent("Mars System"), 300e3.Kilometer()));

            ViewModel = new IngameModel(World);
            ViewModel.Update();

            CommandService = new CommandService(ViewModel);

            MainControl = new IngameControl(ViewModel);
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
