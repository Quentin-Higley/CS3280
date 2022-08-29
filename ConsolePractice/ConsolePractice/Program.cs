// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
Console.Write("Enter the first number");
int intNum1 = Int16.Parse( Console.ReadLine() );
Console.Write("Enter the second number");
int intNum2 = Int16.Parse( Console.ReadLine() );

string[] equal = new string[] { "not equal to", "equal to"};
string[] great = new string[] { "is greater than", "is not greater than" };
string[] lsser = new string[] { "is lesser than", "is not lesser than" };

int lessThan = (int) intNum1 < intNum2 ? 0 : 1;
int greaterThan = (int)intNum1 > intNum2 ? 0 : 1;
int equalTo = (int)intNum1 == intNum2 ? 1 : 0;

Console.WriteLine(String.Format( "{1} + {2} = {0}", intNum1 + intNum2, intNum1, intNum2) );
Console.WriteLine(String.Format( "{1} - {2} = {0}", intNum1 - intNum2, intNum1, intNum2) );
Console.WriteLine(String.Format( "{1} * {2} = {0}", intNum1 * intNum2, intNum1, intNum2) );
Console.WriteLine(String.Format( "{1} / {2} = {0}", intNum1 / intNum2, intNum1, intNum2) );
Console.WriteLine(String.Format( "{1} % {2} = {0}", intNum1 % intNum2, intNum1, intNum2) );

Console.WriteLine(String.Format( "{1} {0} {2}", lsser[lessThan], intNum1, intNum2) );
Console.WriteLine(String.Format( "{1} {0} {2}", great[greaterThan], intNum1, intNum2) );
Console.WriteLine(String.Format( "{1} {0} {2}", equal[equalTo], intNum1, intNum2) );