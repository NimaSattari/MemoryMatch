using System;
using System.Collections.Generic;
using System.Linq;

public class RandomNumberArrayGenerator
{
    public List<int> Generate(int howManyNumbers, int maxNumb)
    {
        List<int> arr = new List<int>(howManyNumbers);
        System.Random rnd = new System.Random();
        int tmp;
        for (int i = 0; i < howManyNumbers; i++)
        {
            tmp = rnd.Next(maxNumb);
            while (IsDuplicate(tmp, arr))
            {
                tmp = rnd.Next(maxNumb);
            }
            arr.Add(tmp);
        }
        foreach (int i in arr)
        {
            UnityEngine.Debug.Log(i);
        }
        return arr;
    }

    private bool IsDuplicate(int tmp, List<int> arr)
    {
        foreach (var item in arr)
        {
            if (item == tmp)
            {
                return true;
            }
        }
        return false;
    }

    public List<int> GenerateRandomIntegersWithoutRepeat(int count, int low, int high)
    {
        var random = new System.Random();
        var numbers = Enumerable.Range(low, high).ToList();
        var result = new List<int>();

        while (result.Count < count)
        {
            var index = random.Next(numbers.Count);
            result.Add(numbers[index]);
            numbers.RemoveAt(index);
        }
        return result;
    }
}