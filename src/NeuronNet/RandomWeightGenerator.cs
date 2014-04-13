﻿using System;
using widemeadows.ml.kohonen.model;
using widemeadows.ml.kohonen.net;

namespace RGBMesh
{
    /// <summary>
    /// Class ColorRandomizer.
    /// </summary>
    public sealed class RandomWeightGenerator : IRandomNeuronFactory
    {
        /// <summary>
        /// The _dimensions
        /// </summary>
        private readonly int _dimensions;

        /// <summary>
        /// Initializes a new instance of the <see cref="RandomWeightGenerator"/> class.
        /// </summary>
        /// <param name="dimensions">The dimensions.</param>
        public RandomWeightGenerator(int dimensions)
        {
            if (dimensions <= 0) throw new ArgumentOutOfRangeException("dimensions", dimensions, "Number of dimensions must be natural.");
            _dimensions = dimensions;
        }

        /// <summary>
        /// Creates the random.
        /// </summary>
        /// <param name="generator">The generator.</param>
        /// <returns>INeuron.</returns>
        public INeuron CreateRandom(IRandomNumber generator)
        {
            var dimensions = _dimensions;

            // create weights
            var weigths = new WeightVector(dimensions);
            for (int i = 0; i < dimensions; ++i)
            {
                weigths[i] = generator.GetDouble(0, 1);
            }

            // create neuron
            var neuron = new Neuron(weigths);
            return neuron;
        }
    }
}
