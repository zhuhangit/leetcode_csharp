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
            var test = leetcode.Convert6("LEETCODEISHIRING", 2);

            Console.WriteLine(JsonConvert.SerializeObject(test));
            Console.Read();
        }
    }

    /// <summary>
    /// leetcode刷题
    /// 存在很大优化空间：记录排名难看的题目；后续会考虑优化，不过大多还没思路；
    /// </summary>
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

        /// <summary>
        /// 3. 无重复字符的最长子串
        /// 注意：验证失败剔除索引及以前字符即可；循环需要验证最后一次结果
        /// 存在很大优化空间：内存消耗：26.6 MB, 在所有 C# 提交中击败了5.07%的用户
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int LengthOfLongestSubstring(string s)
        {
            int relt = 0;
            StringBuilder sb = new StringBuilder();

            if (s != null)
                for (int i = 0; i < s.Length; i++)
                {
                    string str = s[i].ToString();
                    if (i == 0)
                    {
                        relt = 1;
                        sb.Append(str);
                        continue;
                    }
                    int index = sb.ToString().IndexOf(str);
                    if (index > -1)
                    {
                        relt = sb.Length > relt ? sb.Length : relt;
                        var sbNew = sb.ToString().Substring(index + 1);
                        sb.Clear();
                        sb.Append(sbNew);
                    }
                    sb.Append(str);
                }
            //最后一次验证
            relt = sb.Length > relt ? sb.Length : relt;
            return relt;
        }

        /// <summary>
        /// 4. 寻找两个正序数组的中位数
        /// 存在很大优化空间：执行用时： 800 ms , 在所有 C# 提交中击败了 5.72% 的用户
        /// 思考：没有好的思路，只能合并排序
        /// </summary>
        /// <param name="nums1"></param>
        /// <param name="nums2"></param>
        /// <returns></returns>
        public static double FindMedianSortedArrays(int[] nums1, int[] nums2)
        {
            double relt = 0;
            var nums3 = new int[] { };
            nums1 = nums1 ?? new int[] { };
            nums2 = nums2 ?? new int[] { };

            if (nums1.Length == 0 || nums2.Length == 0)
            {
                nums3 = nums1.Length == 0 ? nums2 : nums1;
            }
            else
            {
                var lt = new List<int>();//number
                foreach (var item in nums1)
                    lt.Add(item);
                foreach (var item in nums2)
                    lt.Add(item);
                nums3 = lt.ToArray();
            }
            var sz3 = GetMiddleNumber(nums3);
            if (sz3.Length == 1)
                relt = sz3[0] / 1.0;
            else if (sz3.Length >= 2)
                relt = (sz3[0] + sz3[1]) / 2.0;
            return relt;
        }
        /// <summary>
        /// 获取数组中位数
        /// </summary>
        private static int[] GetMiddleNumber(int[] nums1)
        {
            int root = 0;
            var dic = new Dictionary<int, bool>();//index,delete
            if (nums1.Length > 2)
                while (root < (int)(nums1.Length / 2.0 + 0.5) - 1)
                {
                    int max = 0, min = 0;
                    int maxIndex = -1, minIndex = -1;
                    for (int i = 0; i < nums1.Length; i++)
                    {
                        if (root == 0)
                            dic.Add(i, false);  //初始化
                        else if (dic[i])
                            continue;           //跳过已删除数据

                        int number = nums1[i];
                        if (maxIndex == -1)
                        {
                            max = number;
                            min = number;
                            maxIndex = i;
                            minIndex = i;
                            continue;
                        }
                        if (number > max)
                        {
                            max = number;
                            maxIndex = i;
                        }
                        else if (number < min)
                        {
                            min = number;
                            minIndex = i;
                        }
                    }
                    //记录历史最大、最小
                    if (maxIndex != -1)
                        dic[maxIndex] = true;
                    if (minIndex != -1)
                        dic[minIndex] = true;
                    //初始化变量
                    max = 0;
                    min = 0;
                    maxIndex = -1;
                    minIndex = -1;
                    root++;
                }
            else
                return nums1;
            var lt = new List<int>();//number
            foreach (var item in dic)
            {
                int index = item.Key;
                if (!item.Value)
                    lt.Add(nums1[index]);
            }
            var relt = lt.ToArray();
            return relt;
        }

        /// <summary>
        /// 5. 最长回文子串
        /// 思路：无非两种，单数回文，双数回文
        /// 注意：循环中的临界值,采用索引时注意范围限定
        /// 教训：这道题踩坑太多，全是边界问题，提交10次通过。。。
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string LongestPalindrome(string s)
        {
            string relt = string.Empty;
            s = s ?? "";

            if (s.Length <= 2)
            {
                if (s.Length == 0)
                    relt = "";
                else if (s.Length == 2 && s[0] != s[1])
                    relt = s[0].ToString();
                else
                    relt = s;
                return relt;
            }

            var DoubleHW = new List<int>();//是否存在双数回文
            //单数回文
            for (int i = 1; i < s.Length - 1; i++)
            {
                char before = s[i - 1];
                char next = s[i + 1];
                char current = s[i];

                if (before == current)
                    DoubleHW.Add(i - 1);
                //添加最后一次回文
                if (i == s.Length - 2 && next == current)
                    DoubleHW.Add(i);
                if (before != next)
                    continue;
                int isPassLength = 3;
                for (int j = 1; j < i && i + 1 + j < s.Length; j++)
                {
                    char beforeJ = s[i - 1 - j];
                    char nextJ = s[i + 1 + j];
                    if (beforeJ != nextJ)
                        break;
                    isPassLength += 2;
                }
                if (isPassLength > relt.Length)
                    relt = s.Substring(i - (isPassLength - 1) / 2, isPassLength);
            }
            //双数回文
            if (DoubleHW.Count > 0)
                foreach (var i in DoubleHW)
                {
                    int isPassLength = 2;
                    for (int j = 1; j < i + 1 && i + 1 + j < s.Length; j++)
                    {
                        char beforeJ = s[i - j];
                        char nextJ = s[i + 1 + j];
                        if (beforeJ != nextJ)
                            break;
                        isPassLength += 2;
                    }
                    if (isPassLength > relt.Length)
                        relt = s.Substring(i + 1 - isPassLength / 2, isPassLength);
                }
            //未找到回文字符，默认取第一个
            if (relt.Length == 0)
                relt = s[0].ToString();
            return relt;
        }

        /// <summary>
        /// 6. Z 字形变换
        /// 存在很大优化空间：执行用时：560 ms, 在所有 C# 提交中击败了5.22%的用户
        /// </summary>
        /// <param name="s"></param>
        /// <param name="numRows"></param>
        /// <returns></returns>
        public static string Convert6(string s, int numRows)
        {
            string relt = s ?? "";
            if (relt.Length <= numRows || numRows < 2)
                return relt;//不需要转换

            int fullLength = numRows * 2 - 2;   //单次长度
            int fullNum = relt.Length / fullLength;     //完整次数
            int remainder = relt.Length % fullLength;   //余数
            int colsLength = fullNum * (numRows - 1);   //单行最长
            if (remainder != 0)
                colsLength += (remainder <= numRows ? 1 : remainder - numRows + 1);

            //初始化
            var list = new List<char[]>();
            for (int i = 0; i < numRows; i++)
                list.Add(new char[colsLength]);
            //填充
            var colIndex = 0;
            for (int i = 1; i <= relt.Length; i++)
            {
                var cr = relt[i - 1];
                int fullNumN = i / fullLength;     //完整次数
                int remainderN = i % fullLength;   //余数
                //竖线，奇偶次顺序不同
                if (remainderN > 0 && remainderN <= numRows)
                {
                    list[remainderN - 1][colIndex] = cr;
                    if (remainderN == numRows)
                        colIndex++;  //next
                }
                //折线
                else
                {
                    remainderN = remainderN == 0 ? fullLength : remainderN;
                    list[(numRows - 1) - (remainderN - numRows)][colIndex] = cr;
                    colIndex++;  //next
                }
            }
            //读取
            var sb = new StringBuilder();
            for (int j = 0; j < numRows; j++)
                for (int i = 0; i < colsLength; i++)
                {
                    var str = list[j][i].ToString();
                    if (str!= "\0")
                        sb.Append(str);
                }
            relt = sb.ToString();
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
