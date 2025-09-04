using NumSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace paper_rock_scissors.Class.Game
{
    public static class Decision
    {
        static string[] Signs = { "paper", "rock", "scissors" };
        static List<string> LatestSign = new();
        static NDArray? A;
        public static string ChooseSign(string playerChoice)
        {
            Debug.WriteLine(LatestSign.Count);
            if (LatestSign.Count < 15)
            {
                Random random = new Random();
                int index = random.Next(Signs.Length);
                return Signs[index];
            }
            if (A == null)
            {
                Debug.WriteLine("it's good");
                (float prock, float ppaper, float pscissors) = CalculPourcent("paper");
                (float rrock, float rpaper, float rscissors) = CalculPourcent("rock");
                (float srock, float spaper, float sscissors) = CalculPourcent("scissors");

                A = np.array(new float[,]
                {
                    {ppaper, prock, pscissors},
                    {rpaper, rrock, rscissors},
                    {spaper, srock, sscissors},
                });
            }
            NDArray pi0 = Signs.Select((sign) => 
            {
                return sign == LatestSign.Last() ? 1 : 0;
            }).ToArray();
            
            NDArray pourcents = np.dot(pi0.reshape(1,3), A);
            int lastIndex = 0;
            for(int i = 0; i < 3; ++i)
            {
                if ((double)pourcents.GetAtIndex(i) > (double)pourcents.GetAtIndex(lastIndex))
                {
                    lastIndex = i;
                }
            }
            if (Signs[lastIndex] == "scissors")
            {
                return "rock";
            }
            else if (Signs[lastIndex] == "rock")
            {
                return "paper";
            }
            else
            {
                return "scissors";
            }
        }
        public static void AddResultToSigns(string playerChoice)
        {
            LatestSign.Add(playerChoice);
        }
        public static void ResetSign()
        {
            LatestSign.Clear();
            A = null;
        }
        private static (float, float, float) CalculPourcent(string currentSign)
        {
            var indexs = LatestSign.Select((sign, signIndex) =>
            {
                if (sign == currentSign)
                {
                    return signIndex;
                }
                return -1;
            }).Where((signIndex) => signIndex != -1).ToList();
            var nextIndexs = indexs.Select((signIndex) =>
            {
                if (signIndex + 1 < LatestSign.Count)
                {
                    return signIndex + 1;
                }
                return -1;
            }).Where((signIndex) => signIndex != -1).ToList();
            float rock = 0;
            float paper = 0;
            float scissors = 0;
            foreach (var index in nextIndexs)
            {
                switch (LatestSign[index])
                {
                    case "paper":
                        ++paper;
                        break;
                    case "rock":
                        ++rock;
                        break;
                    case "scissors":
                        ++scissors;
                        break;
                }
            }
            int allPourcent = nextIndexs.Count;
            rock = rock == 0 ? 0 : rock / allPourcent;
            paper = paper == 0 ? 0 : paper / allPourcent;
            scissors = scissors == 0 ? 0 : scissors / allPourcent;
            return (rock, paper, scissors);
        }
    }
}
