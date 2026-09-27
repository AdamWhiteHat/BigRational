using System;
using System.Numerics;
using ExtendedNumerics;
using NUnit.Framework;

namespace TestBigRational
{
	[TestFixture(Category = "Core")]
	public class TestMixedFraction
	{
		public TestContext TestContext { get { return m_testContext; } set { m_testContext = value; } }
		private TestContext m_testContext;

		[Test]
		public void TestConstruction()
		{
			MixedFraction result1 = new MixedFraction(BigInteger.Zero, new BigRational(182, 26));
			MixedFraction result1_2 = new MixedFraction(new BigRational(182, 26));
			MixedFraction result2 = new MixedFraction(BigInteger.Zero, new BigRational(-7, 5));

			MixedFraction expected1 = new MixedFraction(7);
			MixedFraction expected2 = new MixedFraction(-1, 2, 5);

			Assert.AreEqual(expected1, result1);
			Assert.AreEqual(expected1, result1_2);
			Assert.AreEqual(expected2, result2);
		}

		[Test]
		public void TestExpandImproperFraction()
		{
			MixedFraction threeAndOneThird = new MixedFraction(3, 1, 3);
			MixedFraction oneEightyTwoTwentySixths = new MixedFraction(new BigRational(182, 26));
			MixedFraction negativeThreeAndOneSeventh = new MixedFraction(-3, 1, 7);
			MixedFraction seven = new MixedFraction(7);

			BigRational expected313 = new BigRational(10, 3);
			BigRational expected18226 = new BigRational(91, 13);
			BigRational expectedNeg317 = new BigRational(-22, 7);
			BigRational expected7over1 = new BigRational(7, 1);

			BigRational result1 = threeAndOneThird.GetImproperFraction();
			BigRational result2 = oneEightyTwoTwentySixths.GetImproperFraction();
			BigRational result3 = negativeThreeAndOneSeventh.GetImproperFraction();
			BigRational result7 = seven.GetImproperFraction();


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
			MixedFraction c108 = new MixedFraction(108);
			MixedFraction c815 = new MixedFraction(815);
			MixedFraction c1500 = new MixedFraction(1500);
			MixedFraction convergencePoint = new MixedFraction(5);

			// Seed the initial values
			MixedFraction X0 = new MixedFraction(4);
			MixedFraction X1 = new MixedFraction(new BigRational(17, 4));
			MixedFraction Xprevious = X0;
			MixedFraction Xn = X1;

			// Get the current distance to the convergence point, this should be constantly
			// decreasing with each iteration
			MixedFraction distanceToConvergence = MixedFraction.Subtract(convergencePoint, X1);

			int count = 1;
			for (int i = 1; i < n; ++i)
			{
				MixedFraction Xnext = c108 - (c815 - c1500 / Xprevious) / Xn;
				MixedFraction nextDistanceToConvergence = MixedFraction.Subtract(convergencePoint, Xnext);
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
			MixedFraction testA1 = new MixedFraction(0, 1, 31);
			MixedFraction testA2 = new MixedFraction(0, 2, 31);

			Assert.AreNotEqual(testA1.GetHashCode(), testA2.GetHashCode());
		}

		[Test]
		public void TestCompare()
		{
			MixedFraction toCompareAgainst = new MixedFraction(0, 3, 5);

			MixedFraction same = new MixedFraction(0, 6, 10);
			MixedFraction larger = new MixedFraction(0, 61, 100);
			MixedFraction smaller = new MixedFraction(0, 59, 100);
			MixedFraction negative = new MixedFraction(0, -3, 5);

			int expected_Same = 0;
			int expected_Larger = -1;
			int expected_Smaller = 1;
			int expected_Negative = 1;

			int actual_Same = MixedFraction.Compare(toCompareAgainst, same);
			int actual_Larger = MixedFraction.Compare(toCompareAgainst, larger);
			int actual_Smaller = MixedFraction.Compare(toCompareAgainst, smaller);
			int actual_Negative = MixedFraction.Compare(toCompareAgainst, negative);

			Assert.AreEqual(expected_Same, actual_Same, $"Same: BigRational.Compare({toCompareAgainst}, {same}) == {expected_Same} (expected) ; Actual: {actual_Same}");
			Assert.AreEqual(expected_Larger, actual_Larger, $"Larger: BigRational.Compare({toCompareAgainst}, {larger}) == {expected_Larger} (expected) ; Actual: {actual_Larger}");
			Assert.AreEqual(expected_Smaller, actual_Smaller, $"Smaller: BigRational.Compare({toCompareAgainst}, {smaller}) == {expected_Smaller} (expected) ; Actual: {actual_Smaller}");
			Assert.AreEqual(expected_Negative, actual_Negative, $"Negative: BigRational.Compare({toCompareAgainst}, {negative}) == {expected_Negative} (expected) ; Actual: {actual_Negative}");
		}
	}
}
