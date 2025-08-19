namespace MyPolynomial
{
    /// <summary>
    /// Test driver class to test all public methods of MyPolynomial class
    /// </summary>
    public class TestMyPolynomial
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("=== MyPolynomial Test Driver ===\n");

            // Test 1: Constructor and basic functionality
            Console.WriteLine("Test 1: Constructor and ToString()");
            double[] coeffs1 = [11, -4, 3]; // represents 3x^2 - 4x + 11
            MyPolynomial poly1 = new(coeffs1);
            Console.WriteLine($"Polynomial 1: {poly1}");
            Console.WriteLine($"Degree: {poly1.GetDegree()}");
            Console.WriteLine();

            // Test 2: Another polynomial
            Console.WriteLine("Test 2: Second polynomial");
            double[] coeffs2 = [1, 6, 8, 0, 6]; // represents 6x^4 + 8x^2 + 6x + 1
            MyPolynomial poly2 = new(coeffs2);
            Console.WriteLine($"Polynomial 2: {poly2}");
            Console.WriteLine($"Degree: {poly2.GetDegree()}");
            Console.WriteLine();

            // Test 3: Polynomial with zero coefficients
            Console.WriteLine("Test 3: Polynomial with zero coefficients");
            double[] coeffs3 = [0, 0, 5, 0, -2]; // represents -2x^4 + 5x^2
            MyPolynomial poly3 = new(coeffs3);
            Console.WriteLine($"Polynomial 3: {poly3}");
            Console.WriteLine();

            // Test 4: Constant polynomial
            Console.WriteLine("Test 4: Constant polynomial");
            double[] coeffs4 = [7]; // represents 7
            MyPolynomial poly4 = new(coeffs4);
            Console.WriteLine($"Polynomial 4: {poly4}");
            Console.WriteLine($"Degree: {poly4.GetDegree()}");
            Console.WriteLine();

            // Test 5: Evaluate method
            Console.WriteLine("Test 5: Evaluate method");
            Console.WriteLine($"Polynomial 1: {poly1}");
            double x = 2;
            double result = poly1.Evaluate(x);
            Console.WriteLine($"Evaluate at x = {x}: {result}");
            // Expected: 3(4) - 4(2) + 11 = 12 - 8 + 11 = 15

            x = 0;
            result = poly1.Evaluate(x);
            Console.WriteLine($"Evaluate at x = {x}: {result}");
            // Expected: 11

            x = -1;
            result = poly1.Evaluate(x);
            Console.WriteLine($"Evaluate at x = {x}: {result}");
            // Expected: 3(1) - 4(-1) + 11 = 3 + 4 + 11 = 18
            Console.WriteLine();

            // Test 6: Addition
            Console.WriteLine("Test 6: Addition");
            double[] coeffsA = [1, 2, 3]; // 3x^2 + 2x + 1
            double[] coeffsB = [4, 5]; // 5x + 4
            MyPolynomial polyA = new(coeffsA);
            MyPolynomial polyB = new(coeffsB);
            Console.WriteLine($"Polynomial A: {polyA}");
            Console.WriteLine($"Polynomial B: {polyB}");
            polyA.Add(polyB);
            Console.WriteLine($"A + B: {polyA}");
            // Expected: 3x^2 + 7x + 5
            Console.WriteLine();

            // Test 7: Multiplication
            Console.WriteLine("Test 7: Multiplication");
            double[] coeffsC = [1, 1]; // x + 1
            double[] coeffsD = [2, 3]; // 3x + 2
            MyPolynomial polyC = new(coeffsC);
            MyPolynomial polyD = new(coeffsD);
            Console.WriteLine($"Polynomial C: {polyC}");
            Console.WriteLine($"Polynomial D: {polyD}");
            string originalC = polyC.ToString();
            string originalD = polyD.ToString();
            polyC.Multiply(polyD);
            Console.WriteLine($"({originalC}) * ({originalD}) = {polyC}");
            // Expected: (x + 1)(3x + 2) = 3x^2 + 5x + 2
            Console.WriteLine();

            // Test 8: Complex multiplication
            Console.WriteLine("Test 8: Complex multiplication");
            double[] coeffsE = [-1, 2, 1]; // x^2 + 2x - 1
            double[] coeffsF = [1, -1, 1]; // x^2 - x + 1
            MyPolynomial polyE = new(coeffsE);
            MyPolynomial polyF = new(coeffsF);
            Console.WriteLine($"Polynomial E: {polyE}");
            Console.WriteLine($"Polynomial F: {polyF}");
            polyE.Multiply(polyF);
            Console.WriteLine($"E * F: {polyE}");
            Console.WriteLine();

            // Test 9: Zero polynomial
            Console.WriteLine("Test 9: Zero polynomial");
            double[] coeffsZero = [0, 0, 0];
            MyPolynomial polyZero = new(coeffsZero);
            Console.WriteLine($"Zero polynomial: {polyZero}");
            Console.WriteLine();

            // Test 10: Single term polynomial
            Console.WriteLine("Test 10: Single term polynomials");
            double[] coeffsX3 = [0, 0, 0, 1]; // x^3
            MyPolynomial polyX3 = new(coeffsX3);
            Console.WriteLine($"x^3 polynomial: {polyX3}");

            double[] coeffsNegX = [0, -1]; // -x
            MyPolynomial polyNegX = new(coeffsNegX);
            Console.WriteLine($"-x polynomial: {polyNegX}");
            Console.WriteLine();

            // Test: Polynomial with trailing zeros
            Console.WriteLine("Test: Trailing zeros");
            double[] coeffsTrailing = [1, 2, 3, 0, 0]; 
            MyPolynomial polyTrailing = new(coeffsTrailing);
            Console.WriteLine($"Polynomial [1,2,3,0,0]: {polyTrailing}");
            Console.WriteLine($"Expected degree: 2, Actual degree: {polyTrailing.GetDegree()}");
            Console.WriteLine();

            // Test: All zeros except constant term
            Console.WriteLine("Test: Only constant term");
            double[] coeffsConstantOnly = [5, 0, 0, 0]; 
            MyPolynomial polyConstantOnly = new(coeffsConstantOnly);
            Console.WriteLine($"Polynomial [5,0,0,0]: {polyConstantOnly}");
            Console.WriteLine($"Expected degree: 0, Actual degree: {polyConstantOnly.GetDegree()}");
            Console.WriteLine();

            // Test: Completely zero polynomial
            Console.WriteLine("Test: All zero coefficients");
            double[] coeffsAllZero = [0, 0, 0, 0]; 
            MyPolynomial polyAllZero = new(coeffsAllZero);
            Console.WriteLine($"Polynomial [0,0,0,0]: {polyAllZero}");
            Console.WriteLine($"Expected degree: 0, Actual degree: {polyAllZero.GetDegree()}");
            Console.WriteLine();

            Console.WriteLine("=== All tests completed ===");
            Console.ReadLine();
        }
    }
}
