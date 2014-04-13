﻿using System;
using System.Windows.Forms;
using RandomNumberGenerator;
using widemeadows.ml.kohonen.metrics;
using widemeadows.ml.kohonen.neighborhoods;
using widemeadows.ml.kohonen.net;
using widemeadows.ml.kohonen.tests.rgbmesh;

namespace RGBMeshGUI
{
    class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var program = new Program();
            program.Run();
        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        private void Run()
        {
            const int width = 16;
            const int height = 16;
            const int count = width * height;

            const int totalIterations = 100;
            double baseRadius = Math.Sqrt(count);

            // prepare generator and randomized data set
            var generator = new StandardRng();
            var dataSet = new RgbDataSet(generator, count);

            // prepare factories
            var gridFactory = new Grid2DFactory(generator);
            var neuronFactory = new RandomWeightGenerator(3);

            // prepare the metric
            var metric = new EuclideanDistance();
            var bmuFinder = new BmuFinder(metric);

            // prepare the grid
            var grid = gridFactory.CreateGrid(width, height, neuronFactory);

            // prepare adjustment functions
            var radiusFunction = new RadiusExponentialShrink(totalIterations, baseRadius);
            var neighborhoodFunction = new GaussianNeighborhood();
            var learningRateFunction = new LearningRateExponentialShrink(totalIterations, 0.5);
            var weightAdjustment = new WeightAdjustment(radiusFunction, neighborhoodFunction, learningRateFunction);

            // iterate
            for (int i = 0; i < totalIterations; ++i)
            {
                // pick a datum and find the best matching unit on the grid
                var picked = dataSet.PickRandom(generator);
                var bmu = bmuFinder.FindBestMatchingUnit(grid, picked);

                // adjust all other neurons
                var trainingVectorWeights = picked.MapToWeights();
                foreach (var gridNeuron in grid)
                {
                    var distanceToBmu = metric.CalculateDistance(bmu.GridCoordinates, gridNeuron.GridCoordinates);

                    // calculate the new weights
                    var currentWeights = gridNeuron.Neuron.Weights;
                    var newWeights = weightAdjustment.CalculateNewWeights(i, trainingVectorWeights, currentWeights, distanceToBmu);

                    // update the neuron with the new weights
                    gridNeuron.Neuron.UpdateWeights(newWeights);
                }
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var form = new Main();
            form.SetGrid(grid);
            Application.Run(form);
        }
    }
}
