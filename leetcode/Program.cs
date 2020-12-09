using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
            // var test = leetcode.TwoSum(new int[] { 1, 2, 3, 3, 5 }, 8);

            var l1 = new ListNode { val = 2, next = new ListNode { val = 4, next = new ListNode { val = 3, next = null } } };
            var l2 = new ListNode { val = 5, next = new ListNode { val = 6, next = new ListNode { val = 4, next = null } } };
            var test = leetcode.AddTwoNumbers(l1, l2);

            Console.WriteLine(JsonConvert.SerializeObject(test));
            Console.Read();
        }
    }

    public static class leetcode
    {

        /// <summary>
        /// 1. 两数之和 
        /// 优化：剔除无效循环（ab|ba类型、最后一组）
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
                if (_first + 1 >= nums.Length)
                    break;
                for (var _next = _first + 1; _next < nums.Length; _next++)
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


        /// <summary>
        /// 2. 两数相加
        /// 注意：需要使用单位字符计算（存在超大数值）
        /// 存在很大优化空间：148 ms, 在所有 C# 提交中击败了9.67%的用户
        /// </summary>
        /// <param name="l1"></param>
        /// <param name="l2"></param>
        /// <returns></returns>
        public static ListNode AddTwoNumbers(ListNode l1, ListNode l2)
        {
            ListNode relt = null;
            string str1 = "0", str2 = "0", str3 = "0";

            //字符转int
            int ConvertCharToInt(char cr) => cr == '\0' ? 0 : Convert.ToInt32(cr.ToString());

            //链表查找
            string GetLNstr(ListNode ln) => ln.next == null ? ln.val.ToString() : GetLNstr(ln.next) + ln.val.ToString();
            str1 = GetLNstr(l1);
            str2 = GetLNstr(l2);

            //求和
            int maxLength = 0, aLengthDF = 0, bLengthDF = 0; ;
            if (str1.Length > str2.Length)
            {
                maxLength = str1.Length;
                bLengthDF = str1.Length - str2.Length;
            }
            else
            {
                maxLength = str2.Length;
                aLengthDF = str2.Length - str1.Length;
            }
            var sz = new char[maxLength];
            char szAdd = '\0';
            for (int i = maxLength - 1; i >= 0; i--)
            {
                int value = ConvertCharToInt(sz[i]);
                int valueA = i - aLengthDF < 0 ? 0 : ConvertCharToInt(str1[i - aLengthDF]);
                int valueB = i - bLengthDF < 0 ? 0 : ConvertCharToInt(str2[i - bLengthDF]);
                string unitValue = (value + valueA + valueB).ToString();
                if (unitValue.Length > 1)
                {
                    sz[i] = unitValue[1];
                    if (i == 0)
                        szAdd = unitValue[0];
                    else
                        sz[i - 1] = unitValue[0];
                }
                else
                {
                    sz[i] = unitValue[0];
                }
            }
            str3 = (szAdd == '\0' ? "" : szAdd.ToString()) + string.Join("", sz);

            //值转换str
            ListNode lnNew = null;
            for (int i = str3.Length - 1; i >= 0; i--)
            {
                var _lnNew = new ListNode();
                int valNew = ConvertCharToInt(str3[i]);
                if (relt == null)
                {
                    relt = new ListNode { val = valNew, next = i == 0 ? null : _lnNew };
                }
                else
                {
                    lnNew.val = valNew;
                    lnNew.next = i == 0 ? null : _lnNew;
                }
                lnNew = _lnNew;
            }
            return relt;
        }
    }

    // Definition for singly-linked list.
    public class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int val = 0, ListNode next = null)
        {
            this.val = val;
            this.next = next;
        }
    }
}
