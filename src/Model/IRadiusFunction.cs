﻿namespace widemeadows.ml.kohonen.model
{
    /// <summary>
    /// Interface IRadiusFunction
    /// </summary>
    public interface IRadiusFunction
    {
        /// <summary>
        /// Calculates the radius.
        /// </summary>
        /// <param name="iteration">The iteration.</param>
        /// <returns>System.Double.</returns>
        double CalculateRadius(int iteration);
    }
}