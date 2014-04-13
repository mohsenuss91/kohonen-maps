﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using widemeadows.ml.kohonen.model;

namespace widemeadows.ml.kohonen.metrics
{
    /// <summary>
    /// Class EuclideanDistance.
    /// </summary>
    [Export(typeof(IMetric))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    [IdMetadataAttribute("6C46F6A3-CE3B-4CA1-993C-6DFE321392EE", "Euclidean Distance", "1.0.0.0")]
    public sealed class EuclideanDistance : IMetric
    {
        /// <summary>
        /// The squared euclidean distance
        /// </summary>
        private readonly SquaredEuclideanDistance _squaredEuclidean = new SquaredEuclideanDistance();

        /// <summary>
        /// Calculates the distance between two weight vectors.
        /// </summary>
        /// <param name="a">A.</param>
        /// <param name="b">The b.</param>
        /// <returns>System.Double.</returns>
        /// <exception cref="System.ArgumentException">Lengths of weight vectors differ.</exception>
        public double CalculateDistance(IWeights a, IWeights b)
        {
            return CalculateDistance(a.AsReadOnlyList, b.AsReadOnlyList);
        }

        /// <summary>
        /// Calculates the distance between two weight vectors.
        /// </summary>
        /// <param name="a">A.</param>
        /// <param name="b">The b.</param>
        /// <returns>System.Double.</returns>
        /// <exception cref="System.ArgumentException">Lengths of weight vectors differ.</exception>
        public double CalculateDistance(IReadOnlyList<double> a, IReadOnlyList<double> b)
        {
            var length = a.Count;
            if (length != b.Count) throw new ArgumentException("Lengths of weight vectors differ.");

            // since this is regular euclidean, draw the square root
            return Math.Sqrt(_squaredEuclidean.CalculateDistance(a, b));
        }
    }
}