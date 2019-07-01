using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorProject
{
    class Program
    {
        static void Main(string[] args)
        {
            var result = Calculator(new string[] { "1", "+", "2", "*", "3", "*", "4", "+", "5"}); 
        }

        /// <summary>
        /// July 1, 2019
        /// Assuming that input are correct, calculate the numbers with operator +, *. 
        /// Requirement: memory O(1)
        /// The optimal solution is to write O(N) time complexity, and use
        /// iterative solution to solve it. 
        /// Work on test cases given by the interviewer - hints are given
        /// 1 + 2 + 3*...
        /// ----- addition calculation
        /// 1 + 2*3*4*5*6*7+ 8
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        public static int Calculator(string[] numbers)
        {
            if (numbers == null || numbers.Length == 0)
                return 0;

            var length = numbers.Length;
            if (length == 1)
                return Convert.ToInt32(numbers[0]);

            // assuming that numbers' length >= 3
            int sum = Convert.ToInt32(numbers[0]);
            int index = 1; 
            while(index < length)
            {
                var op = numbers[index];
                var isAddition = op.CompareTo("+") == 0; 

                var number1 = Convert.ToInt32(numbers[index + 1]);

                // first case + 2 + 3
                var firstCase = isAddition &&
                    ((index + 2) == length || numbers[index + 2].CompareTo("+") == 0);

                // second case
                // direct: 1 * 3 * 4 or 1 * 3 + 
                var secondCaseDirect = !isAddition;

                if (firstCase)
                {   // case: 1 + 2 + 3, sum = 1, number1 = 2, 1 + 2 should be next one
                    sum += number1;
                    index += 2;
                }                
                else
                {
                    if (secondCaseDirect)
                    {
                        sum = sum * number1;
                        index += 2;
                    }
                    else
                    {
                        // case: 1 + 2 * 3 * 4 * 5 + 6, it includes case: 1 * 2 + 3
                        var rightOperand = number1;
                        index += 2; //caught by online judge

                        // *3
                        while ((index + 2) <= length && numbers[index] == "*")
                        {
                            rightOperand *= Convert.ToInt32(numbers[index + 1]);
                            index += 2;
                        }

                        sum += rightOperand;
                    }
                }
            }

            return sum; 
        }
    }
}
