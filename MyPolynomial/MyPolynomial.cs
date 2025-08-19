using System.Text;

namespace MyPolynomial
{
    /// <summary>
    /// Represents a polynomial with coefficients stored in an array
    /// </summary>
    public class MyPolynomial
    {
        private double[] _coeffs;

        /// <summary>
        /// Constructor that initializes the polynomial with given coefficients
        /// </summary>
        /// <param name="coeffs">Array of coefficients where index 0 represents a0, index 1 represents a1, etc.</param>
        public MyPolynomial(double[] coeffs)
        {
            if (coeffs == null || coeffs.Length == 0)
                throw new ArgumentException("Coefficients array cannot be null or empty");

            _coeffs = new double[coeffs.Length];
            Array.Copy(coeffs, _coeffs, coeffs.Length);
        }

        /// <summary>
        /// Returns the degree of the polynomial
        /// </summary>
        /// <returns>The degree (highest power) of the polynomial</returns>
        public int GetDegree()
        {
            for (int i = _coeffs.Length - 1; i >= 0; i--)
            {
                if (_coeffs[i] != 0)
                {
                    return i;
                }
            }
            return 0;
        }

        /// <summary>
        /// Returns a string representation of the polynomial
        /// </summary>
        /// <returns>String formatted as "anx^n + an-1x^(n-1) + ... + a2x^2 + a1x + a0"</returns>
        public override string ToString()
        {
            StringBuilder sb = new();
            bool isFirstTerm = true;

            // Start from the highest degree and work backwards
            for (int i = _coeffs.Length - 1; i >= 0; i--)
            {
                double coeff = _coeffs[i];

                // Skip zero coefficients
                if (coeff == 0)
                    continue;

                // Add sign (+ or -)
                if (!isFirstTerm)
                {
                    sb.Append(coeff > 0 ? " + " : " - ");
                    coeff = Math.Abs(coeff);
                }
                else if (coeff < 0)
                {
                    sb.Append('-');
                    coeff = Math.Abs(coeff);
                }

                // Add coefficient
                if (coeff != 1 || i == 0)
                {
                    sb.Append(coeff);
                }

                // Add variable and power
                if (i > 0)
                {
                    sb.Append('x');
                    if (i > 1)
                    {
                        sb.Append('^').Append(i);
                    }
                }

                isFirstTerm = false;
            }

            // If all coefficients are zero
            if (sb.Length == 0)
                return "0";

            return sb.ToString();
        }

        /// <summary>
        /// Evaluates the polynomial for a given value of x
        /// </summary>
        /// <param name="x">The value to substitute for x</param>
        /// <returns>The result of evaluating the polynomial at x</returns>
        public double Evaluate(double x)
        {
            double result = 0;
            double xPower = 1; // x^0 = 1

            for (int i = 0; i < _coeffs.Length; i++)
            {
                result += _coeffs[i] * xPower;
                xPower *= x; // Increment the power of x
            }

            return result;
        }

        /// <summary>
        /// Adds another polynomial to this polynomial
        /// </summary>
        /// <param name="another">The polynomial to add</param>
        /// <returns>This instance with the result of the addition</returns>
        public MyPolynomial Add(MyPolynomial another)
        {
            ArgumentNullException.ThrowIfNull(another);

            int maxDegree = Math.Max(GetDegree(), another.GetDegree());
            double[] newCoeffs = new double[maxDegree + 1];

            // Add coefficients from this polynomial
            for (int i = 0; i < _coeffs.Length; i++)
            {
                newCoeffs[i] += _coeffs[i];
            }

            // Add coefficients from the other polynomial
            for (int i = 0; i < another._coeffs.Length; i++)
            {
                newCoeffs[i] += another._coeffs[i];
            }

            // Update this polynomial's coefficients
            _coeffs = newCoeffs;
            return this;
        }

        /// <summary>
        /// Multiplies this polynomial by another polynomial
        /// </summary>
        /// <param name="another">The polynomial to multiply by</param>
        /// <returns>This instance with the result of the multiplication</returns>
        public MyPolynomial Multiply(MyPolynomial another)
        {
            ArgumentNullException.ThrowIfNull(another);

            int newDegree = GetDegree() + another.GetDegree();
            double[] newCoeffs = new double[newDegree + 1];

            // Multiply each term of this polynomial with each term of the other
            for (int i = 0; i < _coeffs.Length; i++)
            {
                for (int j = 0; j < another._coeffs.Length; j++)
                {
                    newCoeffs[i + j] += _coeffs[i] * another._coeffs[j];
                }
            }

            // Update this polynomial's coefficients
            _coeffs = newCoeffs;
            return this;
        }
    }
}
