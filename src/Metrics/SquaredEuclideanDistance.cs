﻿using System;
using System.ComponentModel.Composition;
using widemeadows.ml.kohonen.model;

namespace widemeadows.ml.kohonen.metrics
{
    /// <summary>
    /// Class SquaredEuclideanDistance.
    /// </summary>
    [Export(typeof(IMetric))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    [IdMetadataAttribute("E4923798-678D-48DD-81E1-1C5D01DD7B79", "Squared Euclidean CalculateDistance", "1.0.0.0")]
    public sealed class SquaredEuclideanDistance : IMetric
    {
        /// <summary>
        /// Calculates the distance between two weight vectors.
        /// </summary>
        /// <param name="a">A.</param>
        /// <param name="b">The b.</param>
        /// <returns>System.Double.</returns>
        /// <exception cref="System.ArgumentException">Lengths of weight vectors differ.</exception>
        public double CalculateDistance(IWeights a, IWeights b)
        {
            var length = a.Length;
            if (length != b.Length) throw new ArgumentException("Lengths of weight vectors differ.");

            // calculate sum of squared differences
            double distance = 0;
            for (int i = 0; i < length; ++i)
            {
                var difference = a[i] - b[i];
                distance += difference*difference;
            }

            // since this is squared euclidean, no square root is taken
            return distance;
        }
    }
}
