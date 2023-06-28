using System;
using System.Numerics;
using ExtendedNumerics;
using NUnit.Framework;

namespace TestBigRational
{
	[TestFixture(Category = "Arithmetic")]
	public class TestBigRationalArithmetic
	{
		public TestContext TestContext { get { return m_testContext; } set { m_testContext = value; } }
		private TestContext m_testContext;


		[Test]
		public void TestNormalizeSign001()
		{
			BigRational value1 = new BigRational(0, new Fraction(5, -2));
			BigRational value2 = new BigRational(0, new Fraction(-5, 2));
			BigRational value3 = new BigRational(0, new Fraction(-5, -2));
			BigRational value4 = new BigRational(0, new Fraction(0, -2));

			int actual1 = value1.Sign;
			int actual2 = value2.Sign;
			int actual3 = value3.Sign;
			int actual4 = value4.Sign;

			int expected1 = -1;
			int expected2 = -1;
			int expected3 = 1;
			int expected4 = 0;

			Assert.AreEqual(expected1, actual1, "#1");
			Assert.AreEqual(expected2, actual2, "#2");
			Assert.AreEqual(expected3, actual3, "#3");
			Assert.AreEqual(expected4, actual4, "#4");
		}

		[Test]
		public void TestNormalizeSign002()
		{
			BigRational value1 = new BigRational(0, new Fraction(2, -1));
			BigRational value2 = new BigRational(0, -2, 1);
			BigRational value3 = new BigRational(-2, -1, -1);
			BigRational value4 = new BigRational(-2, 1, -1);
			BigRational value5 = new BigRational(-2, -1, 1);
			BigRational value6 = new BigRational(0, new Fraction(-2, -1));
			BigRational value7 = new BigRational(0, new Fraction(0, -1));

			int actual1 = value1.Sign;
			int actual2 = value2.Sign;
			int actual3 = value3.Sign;
			int actual4 = value4.Sign;
			int actual5 = value5.Sign;
			int actual6 = value6.Sign;
			int actual7 = value7.Sign;

			int expected1 = -1;
			int expected2 = -1;
			int expected3 = -1;
			int expected4 = -1;
			int expected5 = -1;
			int expected6 = 1;
			int expected7 = 0;

			TestContext.WriteLine($"#3: {value3.WholePart} + {value3.FractionalPart.Numerator} / {value3.FractionalPart.Denominator} = {value3}");
			TestContext.WriteLine($"#4: {value4.WholePart} + {value4.FractionalPart.Numerator} / {value4.FractionalPart.Denominator} = {value4}");
			TestContext.WriteLine($"#5: {value5.WholePart} + {value5.FractionalPart.Numerator} / {value5.FractionalPart.Denominator} = {value5}");

			Assert.AreEqual(expected1, actual1, "#1");
			Assert.AreEqual(expected2, actual2, "#2");
			Assert.AreEqual(expected3, actual3, "#3");
			Assert.AreEqual(expected4, actual4, "#4");
			Assert.AreEqual(expected5, actual5, "#5");
			Assert.AreEqual(expected6, actual6, "#6");
			Assert.AreEqual(expected7, actual7, "#7");
		}

		[Test]
		public void TestNegation001()
		{
			BigRational value = new BigRational(0.000001);
			BigRational result1 = -value;
			BigRational result2 = -(value);
			BigRational result3 = BigRational.Negate(value);

			TestContext.WriteLine($"{value} :");
			TestContext.WriteLine($"Sign: {value.Sign}");
			TestContext.WriteLine($"{value.WholePart}");
			TestContext.WriteLine($"{value.FractionalPart}");
			TestContext.WriteLine($"{value.FractionalPart.Numerator} / {value.FractionalPart.Denominator}");

			TestContext.WriteLine();
			TestContext.WriteLine("After negation:");

			TestContext.WriteLine($"{result1} :");
			TestContext.WriteLine($"Sign: {result1.Sign}");
			TestContext.WriteLine($"{result1.WholePart}");
			TestContext.WriteLine($"{result1.FractionalPart}");
			TestContext.WriteLine($"{result1.FractionalPart.Numerator} / {result1.FractionalPart.Denominator}");

			TestContext.WriteLine();
			TestContext.WriteLine("After negation:");

			TestContext.WriteLine($"{result2} :");
			TestContext.WriteLine($"Sign: {result2.Sign}");
			TestContext.WriteLine($"{result2.WholePart}");
			TestContext.WriteLine($"{result2.FractionalPart}");
			TestContext.WriteLine($"{result2.FractionalPart.Numerator} / {result2.FractionalPart.Denominator}");

			TestContext.WriteLine();
			TestContext.WriteLine("After negation:");

			TestContext.WriteLine($"{result3} :");
			TestContext.WriteLine($"Sign: {result3.Sign}");
			TestContext.WriteLine($"{result3.WholePart}");
			TestContext.WriteLine($"{result3.FractionalPart}");
			TestContext.WriteLine($"{result3.FractionalPart.Numerator} / {result3.FractionalPart.Denominator}");

			int expected = -1;

			Assert.AreEqual(1, value.Sign, "value");
			Assert.AreEqual(expected, result1.Sign, "result1");
			Assert.AreEqual(expected, result2.Sign, "result2");
			Assert.AreEqual(expected, result3.Sign, "result3");
		}

		[Test]
		public void TestNegation002()
		{
			BigRational result = new BigRational(0, -1, 1000000);

			TestContext.WriteLine($"{result} :");
			TestContext.WriteLine($"Sign: {result.Sign}");
			TestContext.WriteLine($"{result.WholePart}");
			TestContext.WriteLine($"{result.FractionalPart}");
			TestContext.WriteLine($"{result.FractionalPart.Numerator} / {result.FractionalPart.Denominator}");

			TestContext.WriteLine();

			int expected = -1;
			int actual = result.Sign;

			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void TestNegation003()
		{
			BigRational value1 = new BigRational(-1, 3);
			BigRational result1 = BigRational.Negate(value1);

			TestContext.WriteLine($"{value1} :");
			TestContext.WriteLine($"Sign: {value1.Sign}");
			TestContext.WriteLine($"{value1.WholePart}");
			TestContext.WriteLine($"{value1.FractionalPart}");
			TestContext.WriteLine($"{value1.FractionalPart.Numerator} / {value1.FractionalPart.Denominator}");

			TestContext.WriteLine();
			TestContext.WriteLine("After negation:");

			TestContext.WriteLine($"{result1} :");
			TestContext.WriteLine($"Sign: {result1.Sign}");
			TestContext.WriteLine($"{result1.WholePart}");
			TestContext.WriteLine($"{result1.FractionalPart}");
			TestContext.WriteLine($"{result1.FractionalPart.Numerator} / {result1.FractionalPart.Denominator}");

			int expected1 = 1;
			int actual1 = result1.Sign;

			Assert.AreEqual(expected1, actual1, "#1: 1/3");
		}

		[Test]
		public void TestNegation004()
		{
			BigRational value_bigRational = new BigRational(0, Fraction.Zero);
			Fraction value_Fraction = Fraction.Zero;

			BigRational result_bigRational = BigRational.Negate(value_bigRational);
			Fraction result_Fraction = Fraction.Negate(value_Fraction);


			int expected_bigRational = 0;
			int expected_fraction = 0;

			int actual_bigRational = result_bigRational.Sign;
			int actual_fraction = result_Fraction.Sign;

			Assert.AreEqual(expected_bigRational, actual_bigRational, "bigRational");
			Assert.AreEqual(expected_fraction, actual_fraction, "fraction");
		}

		[Test]
		public void TestDivideByZero()
		{
			Assert.Throws(typeof(DivideByZeroException),
				() =>
				{
					BigRational result = new BigRational(0, 1, 0);
					TestContext.WriteLine($"{result}");
				});
		}

		[Test]
		public void TestAddition()
		{
			// 3/2 + 10/8 == 201/2
			BigRational threeHalfs = new BigRational(BigInteger.Zero, new Fraction(3, 2));
			BigRational tenEighths = new BigRational(BigInteger.Zero, new Fraction(10, 8));
			BigRational expected1 = BigRational.Reduce(new BigRational(BigInteger.Zero, new Fraction(11, 4)));

			// 1/100 + 1/2 == 11/4
			BigRational oneHundred = new BigRational(100);
			BigRational oneHalf = new BigRational(BigInteger.Zero, new Fraction(1, 2));
			BigRational expected2 = BigRational.Reduce(new BigRational(BigInteger.Zero, new Fraction(201, 2)));

			// 1/3 + 1/5 == 1/2 + 1/30 == 8/15
			BigRational oneThird = new BigRational(0, 1, 3);
			BigRational oneFifth = new BigRational(0, new Fraction(1, 5));
			BigRational oneThirtieth = new BigRational(0, new Fraction(1, 30));
			BigRational expected3and4 = BigRational.Reduce(new BigRational(BigInteger.Zero, new Fraction(8, 15)));

			// Calculate
			BigRational result1 = BigRational.Add(threeHalfs, tenEighths);
			BigRational result2 = BigRational.Add(oneHundred, oneHalf);
			BigRational result3 = BigRational.Add(oneThird, oneFifth);
			BigRational result4 = BigRational.Add(oneHalf, oneThirtieth);

			// Assert
			Assert.AreEqual(expected1, result1);
			Assert.AreEqual(expected2, result2);
			Assert.AreEqual(expected3and4, result3);
			Assert.AreEqual(expected3and4, result4);
		}

		[Test]
		public void TestAddingNegative()
		{
			var low = new BigRational(-7);
			var high = new BigRational(-6);

			BigRational sumLowHigh = (low + high);
			BigRational sumHighLow = (high + low);
			BigRational subtractLowHigh = (low - high);
			BigRational subtractHightLow = (high - low);

			Assert.AreEqual(new BigRational(-13), sumLowHigh, $"{low} + {high} = {sumLowHigh}");
			Assert.AreEqual(new BigRational(-13), sumHighLow, $"{high} + {low} = {sumHighLow}");
			Assert.AreEqual(new BigRational(-1), subtractLowHigh, $"{low} + {high} = {subtractLowHigh}");
			Assert.AreEqual(new BigRational(1), subtractHightLow, $"{high} + {low} = {subtractHightLow}");
		}

		[Test]
		public void TestSubtraction()
		{
			BigRational sevenTwoths = new BigRational(3, 1, 2);
			BigRational sevenFifths = new BigRational(1, 2, 5);

			BigRational expected = new BigRational(2, 1, 10);
			BigRational result = BigRational.Subtract(sevenTwoths, sevenFifths);

			Assert.AreEqual(expected, result);
		}

		[Test]
		public void TestMultiplication()
		{
			BigRational sevenTwoths = new BigRational(BigInteger.Zero, new Fraction(7, 2));
			BigRational sevenFifths = new BigRational(BigInteger.Zero, new Fraction(7, 5));

			BigRational expected = BigRational.Reduce(new BigRational(BigInteger.Zero, 49, 10));
			BigRational result = BigRational.Multiply(sevenTwoths, sevenFifths);

			Assert.AreEqual(expected, result);
		}

		[Test]
		public void TestDivisionNegative()
		{
			BigRational expected = BigRational.One;
			BigRational result = BigRational.Divide(BigRational.MinusOne, BigRational.MinusOne);

			Assert.AreEqual(expected, result);
		}

		[Test]
		public void TestDivision()
		{
			BigRational sevenTwoths = new BigRational(BigInteger.Zero, new Fraction(7, 2));
			BigRational sevenFifths = new BigRational(BigInteger.Zero, new Fraction(7, 5));

			BigRational expected = BigRational.Reduce(new BigRational(BigInteger.Zero, 5, 2));
			BigRational result = BigRational.Divide(sevenTwoths, sevenFifths);

			Assert.AreEqual(expected, result);
		}

		[Test]
		public void TestPow()
		{
			BigRational nineFifths = new BigRational(1, 4, 5);

			BigRational expected = new BigRational(3, 6, 25);
			BigRational result = BigRational.Pow(nineFifths, 2);

			Assert.AreEqual(expected, result);
		}

		[Test]
		public void TestSqrt001()
		{
			BigRational fourNinths = new BigRational(0, 4, 9); // sqrt(4/9) == 2/3

			BigRational expected = new BigRational(0, 2, 3);

			BigRational result = BigRational.Sqrt(fourNinths);

			Assert.AreEqual(expected, result);
		}

		[Test]
		public void TestSqrt002()
		{
			double n = 160000000000d;
			double d = 249.999d;
			double q = n / d;
			double expected = Math.Sqrt(q); // 25298.271877941387183714739055

			BigRational a = new BigRational(BigInteger.Parse("160000000000"));
			BigRational b = new BigRational(BigInteger.Parse("249"), new Fraction(0.999));
			BigRational c = BigRational.Divide(a, b);
			BigRational result = BigRational.Sqrt(c);

			TestContext.WriteLine($"Expected: {expected}");
			TestContext.WriteLine($"Actual: {(double)result}");
			TestContext.WriteLine($"Actual: {(decimal)result}");
			TestContext.WriteLine($"Actual: {result}");

			Assert.AreEqual(expected, (double)result, 0.000000000002d);
		}

		[Test]
		public void TestNthRoot()
		{
			BigRational twentyTwoOverThreeOneTwoFive = new BigRational(0, 32, 3125); // 5#(32/3125) = 2/5

			BigRational expected = new BigRational(0, 2, 5);

			BigRational result = BigRational.NthRoot(twentyTwoOverThreeOneTwoFive, 5);

			Assert.AreEqual(expected, result);
		}

		[Test]
		public void TestMidPoint()
		{
			var low = (BigRational)(-7);
			var high = (BigRational)(-6);

			// Take the mid point of the above 2 numbers
			BigRational sum = (low + high);
			BigRational mid = sum / (BigRational)2;

			double expectedSum = -13d;
			double actualSum = (double)sum;
			Assert.AreEqual(expectedSum, actualSum, $"{low} + {high} = {sum}");

			double expectedMid = -6.5d;
			double actualMid = (double)mid;
			Assert.AreEqual(expectedMid, actualMid, $"({low} + {high})/2 = mid");

			TestContext.WriteLine($"{low} < {mid} < {high}");

			Assert.IsTrue(mid > low, "mid > low");
			Assert.IsTrue(mid < high, "mid < high");
		}
	}
}
