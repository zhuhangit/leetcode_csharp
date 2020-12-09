using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode
{
    class Program
    {
        static void Main(string[] args)
        {
            var test = leetcode.TwoSum(new int[] { 1, 2, 3, 3, 5 }, 8);
        }
    }

    public static class leetcode
    {

        /// <summary>
        /// 1. 两数之和
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static int[] TwoSum(int[] nums, int target)
        {
            int first = -1, next = -1;

            for (var _first = 0; _first < nums.Length; _first++)
            {
                var _firstValue = nums[_first];
                for (var _next = nums.Length - 1; _next >= 0; _next--)
                {
                    if (_first == _next)
                        continue;
                    var _nextValue = nums[_next];
                    if (_firstValue + _nextValue == target)
                    {
                        first = _first;
                        next = _next;
                        break;
                    }
                }
                if (first >= 0)
                    break;
            }
            return first < 0 ? null : new int[] { first, next };
        }
    }
}
