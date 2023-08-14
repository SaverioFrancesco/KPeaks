using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


namespace KpeaksConsole.MinorityChainList
{
    public class MinorityChainList
    {
        [StructLayout(LayoutKind.Sequential)]
        unsafe public struct IndexNode {


            public int Index = -1;
            public IndexNode* _next { get; set; } = null;
            public IndexNode(int index) { Index = index; _next = null; }

        }

        private readonly long[] _inputArray;
        private readonly long _maxValue;
        private readonly IndexNode[] CountingList;
        private readonly IndexNode[] _LastNodes;
        private readonly int[] CountingLEqArray;
        private readonly int[] CountingLessArray;


        public MinorityChainList(long[] inputArray)
        {
            _inputArray = inputArray;
            _maxValue = inputArray.Max();

            _LastNodes = new IndexNode[_maxValue];
            CountingList = new IndexNode[_maxValue];
            CountingLessArray = new int[_maxValue];
            CountingLEqArray = new int[_maxValue];

            Initialize();
        }

        unsafe private void Initialize()
        {

            for (int i = 0; i < _inputArray.Length; i++)
            {
                CountingLEqArray[_inputArray[i]]++;

                var firstNode = CountingList[_inputArray[i]];
                if(firstNode.Equals(null))
                {
                    IndexNode newFirstNode = new(i)
                    {
                        _next = null
                    };
                    CountingList[_inputArray[i]] = newFirstNode;
                    _LastNodes[_inputArray[i]] = newFirstNode;
                }
                IndexNode newNode = new(i)
                {
                    _next = &firstNode
                };
                CountingList[_inputArray[i]] = newNode;
            }
            
            for (int i =0; i < CountingLEqArray.Length-1; i++)
                CountingLEqArray[i + 1] += CountingLEqArray[i];

            // Can be simplified.
            for (int i = 0; i < CountingLEqArray.Length - 2; i++)
                CountingLessArray[i + 1] += CountingLEqArray[i];
            


            for (int i = CountingList.Length-1; i>0; i--)
            {
                int j = i-1;
                while (j>0 && CountingList[_inputArray[j]].Equals(null))
                {
                    j--;
                }
                if (!CountingList[_inputArray[j]].Equals(null)) // maybe unnecessary.
                {
                    var a = CountingList[_inputArray[j]];
                    _LastNodes[i]._next = &a;
                }
            }


            }

        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string? ToString()
        {
            return base.ToString();
        }
    }
}
