using System;

namespace Shapes {
    internal static class CalculationHelper {
        public static double GetArea(string type, int r, int w, int h) =>
            type switch {
                "Rectangle" => w * h,
                "Triangle" => w * h / 2d,
                "Circle" => Math.Pow(r, 2) * Math.PI,
                _ => throw new ArgumentException("Invalid type", nameof(type))
            };
    }
}
