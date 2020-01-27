using System;
using System.Collections.Generic;
using System.IO;

namespace test1
{
    class Program
    {
        public static void Main(string[] args)
        {
            using (FileStream strm = File.Create(@"C:\Sandbox\StudyCaseProjects\caseStudySolution\test1\test1\WriteText.txt"));
            //TXT dosyasinin lokasyonunu guncellemelisiniz.  Program bazi yerlerde txt dosyasi uzerinden veri alip veriyor.

            List<int> numbers = new List<int>() {1,2,3,4,5,6,7};
            List<int> targets = new List<int>() {3,8,11,13};

            sum_up(numbers, targets);

            System.IO.StreamReader file =
                   new System.IO.StreamReader(@"C:\Sandbox\StudyCaseProjects\caseStudySolution\test1\test1\WriteText.txt") ;
          
            string myString = file.ReadToEnd();
            Console.WriteLine(myString);


            String[] listLines = myString.Split("\n");
            //Console.Write(listLines.Length);
            listLines[listLines.Length-1] = "";


            List<List<int>> file_read = new List<List<int>> { };
            for (int i = 0; i < listLines.Length; i++)
            {
                List<int> array = new List<int>();
                String[] listInts = listLines[i].Split('\t');
                for (int j = 0; j < listInts.Length; j++)
                {
                    if (listInts[j] != "")
                    {
                        array.Add(Convert.ToInt32(listInts[j]));
                    }
                }
                file_read.Add(array);
            }
            
            foreach (List<int> array in file_read)
            {
                foreach (int i in array)
                {
                    Console.Write(i + " ");
                }
                Console.WriteLine();
            }


            List<int> list1 = new List<int>() { 256, 306, 356 };
            List<int> list2 = new List<int>() { 277, 327, 377 };
            List<int> list3 = new List<int>() { 291, 341, 391 };
            List<List<int>> lists = new List<List<int>> { list1, list2, list3 };
            
            findCart(lists);

            //generatePermutations(lists, new List<int>(), 0, 0);


            /* var results = lists.CrossProduct();
             foreach (var resultList in results)
             {
                 Console.WriteLine("all permutations (" + string.Join(",", resultList) + ")");
             }
             */
        }

        private static void findCart(List<List<int>> lists)
        {
            for (int i = 0; i < lists[0].Count; i++)
                for (int j = 0; j < lists[1].Count; j++)
                    for (int k = 0; k < lists[2].Count; k++)
                        if (i != j && i != k && j != k)
                        {
                            Console.WriteLine("{" + lists[0][i] + ", " + lists[1][j] + ", " + lists[2][k] + "} ");
                            Console.WriteLine("total cost: " + (lists[0][i] + lists[1][j] + lists[2][k]));
                        }
        }

        
        private static void generatePermutations(List<List<int>> lists, List<int> result, int depth, int current)
        {
            if (depth == lists.Count)
            {
                result.Add(current);
                Console.WriteLine("all permutations (" + string.Join(",", result.ToArray()) + ")");
                return;
            }

            for (int i = 0; i < lists[depth].Count; i++)
            {
                    Console.WriteLine(lists[depth][i]);
                    generatePermutations(lists, result, depth + 1, current + lists[depth][i]);
            }
        }
        

        private static void sum_up(List<int> numbers, List<int> targets)
        {
            List<List<int>> lists = new List<List<int>> { };

            foreach (int target in targets)
                 sum_up_recursive(numbers, target, new List<int>());
        }
        
        

        private static void sum_up_recursive(List<int> numbers, int target, List<int> partial)
        {
            int total_sum = 0;
            int total_cost = 0;
            foreach (int x in partial)
            {
                total_cost += 50+x*7;
                total_sum += x; //simdiye kadar partial listedekileri topla
            }

            if (total_sum == target && partial.Count>1)
            {
                Console.Write("weight of the box " + target+" parts in the box(" + string.Join(",", partial.ToArray()) + ")"); //eger targete ulastiysa partial listeyi yazdir
                Console.WriteLine(" cost of the box: " + total_cost); //eger targete ulastiysa partial listeyi yazdir
                
                using (System.IO.StreamWriter file =
                    new System.IO.StreamWriter(@"C:\Sandbox\StudyCaseProjects\caseStudySolution\test1\test1\WriteText.txt", true))
                {
                    file.Write(target+"\t" + string.Join("\t", partial.ToArray()));
                    file.WriteLine("\t"+total_cost);
                }

            }
            if (total_sum >= target)
                return;

            for (int i = 0; i < numbers.Count; i++)
            {
                List<int> remaining = new List<int>();
                int n = numbers[i];
   
                remaining.Add(numbers[i]); //remaining liste: kullanabilecegimiz elemenlardan hala dahil edilmemis olanlar hangilerdir (kendisi)
                if (i+1 < numbers.Count) remaining.Add(numbers[i+1]); //remaining liste: kullanabilecegimiz elemenlardan hala dahil edilmemis olanlar hangilerdir (kendisinden 1 buyuk olan)
              

                List<int> partial_rec = new List<int>(partial);

                partial_rec.Add(n);
                sum_up_recursive(remaining, target, partial_rec);
            }

        }
    }
}
