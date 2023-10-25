using System;
using System.Collections.Generic;
using System.Linq;

public class RandomNumberArrayGenerator
{
    /// <summary>
    /// returns list of <paramref howManyNumbers="howManyNumbers"/> integers from 0 to <paramref maxNumb="maxNumb"/>.
    /// </summary>
    public List<int> GenerateRandomIntegersWithoutRepeat(int howManyNumbers, int maxNumb)
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

    /// <summary>
    /// returns list of <paramref howManyNumbers="howManyNumbers"/> integers from <paramref low="low"/> to <paramref high="high"/>.
    /// </summary>
    public List<int> GenerateRandomIntegersBetweenTwoNumberWithoutRepeat(int howManyNumbers, int low, int high)
    {
        var random = new System.Random();
        var numbers = Enumerable.Range(low, high).ToList();
        var result = new List<int>();

        while (result.Count < howManyNumbers)
        {
            var index = random.Next(numbers.Count);
            result.Add(numbers[index]);
            numbers.RemoveAt(index);
        }
        return result;
    }

    /// <summary>
    /// checks if <paramref numb="numb"/> is repeated in <paramref integerList="integerList"/>.
    /// </summary>
    private bool IsDuplicate(int numb, List<int> integerList)
    {
        foreach (var item in integerList)
        {
            if (item == numb)
            {
                return true;
            }
        }
        return false;
    }

}