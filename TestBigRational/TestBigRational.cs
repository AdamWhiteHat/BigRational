using System;
using System.Numerics;
using ExtendedNumerics;
using NUnit.Framework;

namespace TestBigRational
{
	[TestFixture(Category = "Core")]
	public class TestBigRational
	{
		public TestContext TestContext { get { return m_testContext; } set { m_testContext = value; } }
		private TestContext m_testContext;

		[Test]
		public void TestConstruction()
		{
			BigRational result1 = new BigRational(BigInteger.Zero, new Fraction(182, 26));
			BigRational result1_2 = new BigRational(new Fraction(182, 26));
			BigRational result2 = new BigRational(BigInteger.Zero, new Fraction(-7, 5));

			BigRational expected1 = new BigRational(7);
			BigRational expected2 = new BigRational(-1, 2, 5);

			Assert.AreEqual(expected1, result1);
			Assert.AreEqual(expected1, result1_2);
			Assert.AreEqual(expected2, result2);
		}

		[Test]
		public void TestExpandImproperFraction()
		{
			BigRational threeAndOneThird = new BigRational(3, 1, 3);
			BigRational oneEightyTwoTwentySixths = new BigRational(new Fraction(182, 26));
			BigRational negativeThreeAndOneSeventh = new BigRational(-3, 1, 7);
			BigRational seven = new BigRational(7);

			Fraction expected313 = new Fraction(10, 3);
			Fraction expected18226 = new Fraction(91, 13);
			Fraction expectedNeg317 = new Fraction(-22, 7);
			Fraction expected7over1 = new Fraction(7, 1);

			Fraction result1 = threeAndOneThird.GetImproperFraction();
			Fraction result2 = oneEightyTwoTwentySixths.GetImproperFraction();
			Fraction result3 = negativeThreeAndOneSeventh.GetImproperFraction();
			Fraction result7 = seven.GetImproperFraction();


			Assert.AreEqual(expected313, result1);
			Assert.AreEqual(expected18226, result2);
			Assert.AreEqual(expectedNeg317, result3);
			Assert.AreEqual(expected7over1, result7);
		}

		[Test]
		public void TestMullersRecurrenceConvergesOnFive()
		{
			// Set an upper limit to the number of iterations to be tried
			int n = 100;

			// Precreate some constants to use in the calculations
			BigRational c108 = new BigRational(108);
			BigRational c815 = new BigRational(815);
			BigRational c1500 = new BigRational(1500);
			BigRational convergencePoint = new BigRational(5);

			// Seed the initial values
			BigRational X0 = new BigRational(4);
			BigRational X1 = new BigRational(new Fraction(17, 4));
			BigRational Xprevious = X0;
			BigRational Xn = X1;

			// Get the current distance to the convergence point, this should be constantly
			// decreasing with each iteration
			BigRational distanceToConvergence = BigRational.Subtract(convergencePoint, X1);

			int count = 1;
			for (int i = 1; i < n; ++i)
			{
				BigRational Xnext = c108 - (c815 - c1500 / Xprevious) / Xn;
				BigRational nextDistanceToConvergence = BigRational.Subtract(convergencePoint, Xnext);
				Assert.IsTrue(nextDistanceToConvergence < distanceToConvergence);

				Xprevious = Xn;
				Xn = Xnext;
				distanceToConvergence = nextDistanceToConvergence;
				if ((double)Xn == 5d)
					break;
				++count;
			}
			Assert.AreEqual((double)Xn, 5d);
			Assert.IsTrue(count == 70);
		}

		[Test]
		public void TestGetHashCode()
		{
			BigRational testA1 = new BigRational(0, 1, 31);
			BigRational testA2 = new BigRational(0, 2, 31);

			Assert.AreNotEqual(testA1.GetHashCode(), testA2.GetHashCode());
		}

		[Test]
		public void TestCompare()
		{
			BigRational toCompareAgainst = new BigRational(0, 3, 5);

			BigRational same = new BigRational(0, 6, 10);
			BigRational larger = new BigRational(0, 61, 100);
			BigRational smaller = new BigRational(0, 59, 100);
			BigRational negative = new BigRational(0, -3, 5);

			int expected_Same = 0;
			int expected_Larger = -1;
			int expected_Smaller = 1;
			int expected_Negative = 1;

			int result_Same = BigRational.Compare(toCompareAgainst, same);
			int result_Larger = BigRational.Compare(toCompareAgainst, larger);
			int result_Smaller = BigRational.Compare(toCompareAgainst, smaller);
			int resultl_Negative = BigRational.Compare(toCompareAgainst, negative);

			Assert.AreEqual(expected_Same, result_Same);
			Assert.AreEqual(expected_Larger, result_Larger);
			Assert.AreEqual(expected_Smaller, result_Smaller);
			Assert.AreEqual(expected_Negative, resultl_Negative);
		}
	}
}
