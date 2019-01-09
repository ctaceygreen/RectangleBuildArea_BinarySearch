using System;
using System.Collections.Generic;
// you can also use other imports, for example:
// using System.Collections.Generic;

// you can write to stdout for debugging purposes, e.g.
// Console.WriteLine("this is a debug message");

class Solution
{
    public int solution(int[] A, int X)
    {
        // write your code in C# 6.0 with .NET 4.5 (Mono)

        // Contruct a count array of edge lengths 
        Dictionary<int, long> ACount = new Dictionary<int, long>();
        foreach(var a in A)
        {
            long value = 0;
            if (ACount.TryGetValue(a, out value))
            {
                ACount[a] = value + 1;
            }
            else
            {
                ACount.Add(a, value + 1);
            }
        }

        //If count of a letter is greater than 2, then add to suitable edges. If count is greater than 4 then we could create a square pen, so check if that works straight off.
        List<int> suitableEdges = new List<int>();
        long totalPossibleRectangles = 0;
        foreach(var keyPair in ACount)
        {
            if(keyPair.Value >= 2)
            {
                suitableEdges.Add(keyPair.Key);
                if(keyPair.Value >=4)
                {
                    if(keyPair.Key * keyPair.Key >= X)
                    {
                        //Then square is possible rectangle so add 1 to that
                        totalPossibleRectangles++;
                    }
                }
            }
        }
        //Need to sort suitable edges first
        suitableEdges.Sort();


        //Binary search for each suitable edge. We want to find the smallest other edge which creates a rectangle big enough.
        //Then we know that all edges greater than that are also possible

        for(int i = 0; i < suitableEdges.Count; i++)
        {
            int begin = i + 1;
            int end = suitableEdges.Count - 1;
            while(begin <= end)
            {
                int mid = (begin + end) / 2;
                if(suitableEdges[i] * suitableEdges[mid] >= X)
                {
                    //Then possible rectangle. Not necessarily the smallest though, so move the end backwards to see.
                    end = mid - 1;
                }
                else
                {
                    //Not big enough so move begin up so search for larger
                    begin = mid + 1;
                }
            }
            //We now know that end is the smallest suitable edge that combines with this edge to make a rectangle. Therefore add to the count by all of the edges greater than mid
            totalPossibleRectangles += suitableEdges.Count - (end + 1);
            if(totalPossibleRectangles > 1000000000)
            {
                return -1;
            }
        }

        return (int)totalPossibleRectangles;
        
    }
}