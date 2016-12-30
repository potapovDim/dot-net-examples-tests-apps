using System;
using someNewNameSpace;
namespace InitialProgram
{
  public class CustomMethods
  {
    public double CalculateSum(double a, double b)
    {
      return a + b;
    }
    public double CalculateDiff(double a, double b)
    {
      return a - b;
    }
  }
  public class Program
  {
    public static void Main(string[] args)
    {

      var calcul = new TestClass();
      double dododdoodod = calcul.PLUSS(10,12);
      Console.WriteLine(dododdoodod);
      Console.ReadKey();
    }
  }
}
