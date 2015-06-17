using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackingDefaceBUS.Utils
{
    public class BoyerMoore
    {
        private static int[] BuildBadCharTable(char[] needle, int needleLength)
        {
            int[] badShift = new int[needleLength];
            for (int i = 0; i < needleLength; i++)
            {
                badShift[i] = needle.Length;
            }
            int last = needle.Length - 1;
            for (int i = 0; i < last; i++)
            {
                badShift[(int)needle[i]] = last - i;
            }
            return badShift;
        }

        public static int boyerMooreHorsepool(String pattern, String text)
        {
            char[] needle = pattern.ToCharArray();
            int a = needle.Length;
            Console.WriteLine(a);
            char[] haystack = text.ToCharArray();

            if (needle.Length > haystack.Length)
            {
                return -1;
            }
            int[] badShift = BuildBadCharTable(needle, a);
            int offset = 0;
            int scan = 0;
            int last = needle.Length - 1;
            int maxoffset = haystack.Length - needle.Length;
            while (offset <= maxoffset)
            {
                for (scan = last; (needle[scan] == haystack[scan + offset]); scan--)
                {
                    if (scan == 0)
                    { //Match found
                        return offset;
                    }
                }
                offset += badShift[(int)haystack[offset + last]];
            }
            return -1;
        }
    }
}
