using System;
using System.Numerics;
using ExtendedNumerics;
using NUnit.Framework;

namespace TestBigRational
{
	[TestFixture(Category = "Arithmetic")]
	public class TestMixedFractionArithmetic
	{
		public TestContext TestContext { get { return m_testContext; } set { m_testContext = value; } }
		private TestContext m_testContext;


		[Test]
		public void TestNormalizeSign001()
		{
			MixedFraction value1 = new MixedFraction(0, new BigRational(5, -2));
			MixedFraction value2 = new MixedFraction(0, new BigRational(-5, 2));
			MixedFraction value3 = new MixedFraction(0, new BigRational(-5, -2));
			MixedFraction value4 = new MixedFraction(0, new BigRational(0, -2));

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
			MixedFraction value1 = new MixedFraction(0, new BigRational(2, -1));
			MixedFraction value2 = new MixedFraction(0, -2, 1);
			MixedFraction value3 = new MixedFraction(-2, -1, -1);
			MixedFraction value4 = new MixedFraction(-2, 1, -1);
			MixedFraction value5 = new MixedFraction(-2, -1, 1);
			MixedFraction value6 = new MixedFraction(0, new BigRational(-2, -1));
			MixedFraction value7 = new MixedFraction(0, new BigRational(0, -1));

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
			MixedFraction value = new MixedFraction(0.000001);
			MixedFraction result1 = -value;
			MixedFraction result2 = -(value);
			MixedFraction result3 = MixedFraction.Negate(value);

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
			MixedFraction result = new MixedFraction(0, -1, 1000000);

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
			MixedFraction value1 = new MixedFraction(-1, 3);
			MixedFraction result1 = MixedFraction.Negate(value1);

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
			MixedFraction value_bigRational = new MixedFraction(0, BigRational.Zero);
			BigRational value_Fraction = BigRational.Zero;

			MixedFraction result_bigRational = MixedFraction.Negate(value_bigRational);
			BigRational result_Fraction = BigRational.Negate(value_Fraction);


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
					MixedFraction result = new MixedFraction(0, 1, 0);
					TestContext.WriteLine($"{result}");
				});
		}

		[Test]
		public void TestAddition()
		{
			// 3/2 + 10/8 == 201/2
			MixedFraction threeHalfs = new MixedFraction(BigInteger.Zero, new BigRational(3, 2));
			MixedFraction tenEighths = new MixedFraction(BigInteger.Zero, new BigRational(10, 8));
			MixedFraction expected1 = MixedFraction.Reduce(new MixedFraction(BigInteger.Zero, new BigRational(11, 4)));

			// 1/100 + 1/2 == 11/4
			MixedFraction oneHundred = new MixedFraction(100);
			MixedFraction oneHalf = new MixedFraction(BigInteger.Zero, new BigRational(1, 2));
			MixedFraction expected2 = MixedFraction.Reduce(new MixedFraction(BigInteger.Zero, new BigRational(201, 2)));

			// 1/3 + 1/5 == 1/2 + 1/30 == 8/15
			MixedFraction oneThird = new MixedFraction(0, 1, 3);
			MixedFraction oneFifth = new MixedFraction(0, new BigRational(1, 5));
			MixedFraction oneThirtieth = new MixedFraction(0, new BigRational(1, 30));
			MixedFraction expected3and4 = MixedFraction.Reduce(new MixedFraction(BigInteger.Zero, new BigRational(8, 15)));

			// Calculate
			MixedFraction result1 = MixedFraction.Add(threeHalfs, tenEighths);
			MixedFraction result2 = MixedFraction.Add(oneHundred, oneHalf);
			MixedFraction result3 = MixedFraction.Add(oneThird, oneFifth);
			MixedFraction result4 = MixedFraction.Add(oneHalf, oneThirtieth);

			// Assert
			Assert.AreEqual(expected1, result1);
			Assert.AreEqual(expected2, result2);
			Assert.AreEqual(expected3and4, result3);
			Assert.AreEqual(expected3and4, result4);
		}

		[Test]
		public void TestAddingNegative()
		{
			var low = new MixedFraction(-7);
			var high = new MixedFraction(-6);

			MixedFraction sumLowHigh = (low + high);
			MixedFraction sumHighLow = (high + low);
			MixedFraction subtractLowHigh = (low - high);
			MixedFraction subtractHightLow = (high - low);

			Assert.AreEqual(new MixedFraction(-13), sumLowHigh, $"{low} + {high} = {sumLowHigh}");
			Assert.AreEqual(new MixedFraction(-13), sumHighLow, $"{high} + {low} = {sumHighLow}");
			Assert.AreEqual(new MixedFraction(-1), subtractLowHigh, $"{low} + {high} = {subtractLowHigh}");
			Assert.AreEqual(new MixedFraction(1), subtractHightLow, $"{high} + {low} = {subtractHightLow}");
		}

		[Test]
		public void TestSubtraction()
		{
			MixedFraction sevenTwoths = new MixedFraction(3, 1, 2);
			MixedFraction sevenFifths = new MixedFraction(1, 2, 5);

			MixedFraction expected = new MixedFraction(2, 1, 10);
			MixedFraction result = MixedFraction.Subtract(sevenTwoths, sevenFifths);

			Assert.AreEqual(expected, result);
		}

		[Test]
		public void TestMultiplication()
		{
			MixedFraction sevenTwoths = new MixedFraction(BigInteger.Zero, new BigRational(7, 2));
			MixedFraction sevenFifths = new MixedFraction(BigInteger.Zero, new BigRational(7, 5));

			MixedFraction expected = MixedFraction.Reduce(new MixedFraction(BigInteger.Zero, 49, 10));
			MixedFraction result = MixedFraction.Multiply(sevenTwoths, sevenFifths);

			Assert.AreEqual(expected, result);
		}

		[Test]
		public void TestDivisionNegative()
		{
			MixedFraction expected = MixedFraction.One;
			MixedFraction result = MixedFraction.Divide(MixedFraction.MinusOne, MixedFraction.MinusOne);

			Assert.AreEqual(expected, result);
		}

		[Test]
		public void TestDivision()
		{
			MixedFraction sevenTwoths = new MixedFraction(BigInteger.Zero, new BigRational(7, 2));
			MixedFraction sevenFifths = new MixedFraction(BigInteger.Zero, new BigRational(7, 5));

			MixedFraction expected = MixedFraction.Reduce(new MixedFraction(BigInteger.Zero, 5, 2));
			MixedFraction result = MixedFraction.Divide(sevenTwoths, sevenFifths);

			Assert.AreEqual(expected, result);
		}

		[Test]
		public void TestPow()
		{
			MixedFraction nineFifths = new MixedFraction(1, 4, 5);

			MixedFraction expected = new MixedFraction(3, 6, 25);
			MixedFraction result = MixedFraction.Pow(nineFifths, 2);

			Assert.AreEqual(expected, result);
		}

		[Test]
		public void TestSqrt001()
		{
			MixedFraction fourNinths = new MixedFraction(0, 4, 9); // sqrt(4/9) == 2/3

			MixedFraction expected = new MixedFraction(0, 2, 3);

			MixedFraction result = MixedFraction.Sqrt(fourNinths);

			Assert.AreEqual(expected, result);
		}

		[Test]
		public void TestSqrt002()
		{
			double n = 160000000000d;
			double d = 249.999d;
			double q = n / d;
			double expected = Math.Sqrt(q); // 25298.271877941387183714739055

			MixedFraction a = new MixedFraction(BigInteger.Parse("160000000000"));
			MixedFraction b = new MixedFraction(BigInteger.Parse("249"), new BigRational(0.999));
			MixedFraction c = MixedFraction.Divide(a, b);
			MixedFraction result = MixedFraction.Sqrt(c);

			TestContext.WriteLine($"Expected: {expected}");
			TestContext.WriteLine($"Actual: {(double)result}");
			TestContext.WriteLine($"Actual: {(decimal)result}");
			TestContext.WriteLine($"Actual: {result}");

			Assert.AreEqual(expected, (double)result, 0.000000000002d);
		}

		[Test]
		public void TestNthRoot()
		{
			MixedFraction twentyTwoOverThreeOneTwoFive = new MixedFraction(0, 32, 3125); // 5#(32/3125) = 2/5

			MixedFraction expected = new MixedFraction(0, 2, 5);

			MixedFraction result = MixedFraction.NthRoot(twentyTwoOverThreeOneTwoFive, 5);

			Assert.AreEqual(expected, result);
		}

		[Test]
		public void TestMidPoint()
		{
			var low = (MixedFraction)(-7);
			var high = (MixedFraction)(-6);

			// Take the mid point of the above 2 numbers
			MixedFraction sum = (low + high);
			MixedFraction mid = sum / (MixedFraction)2;

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
