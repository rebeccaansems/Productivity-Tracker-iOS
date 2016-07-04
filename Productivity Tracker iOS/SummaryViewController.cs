using Foundation;
using System;
using System.IO;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using UIKit;
using SQLite;

namespace Productivity_Tracker_iOS
{
    partial class SummaryViewController : UIViewController
    {

        public SummaryViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            Tuple<int, int>[] productivityHour = new Tuple<int, int>[24];
            int productiveDataPoints = 0, productivityLevelTotalHour = 0;

            var database = AppDelegate.db.Table<ProductiveData>();
            //fill 24 instance(hour) array with zero'd tuples 
            for (int i = 0; i < productivityHour.Length; i++)
            {
                productivityHour[i] = new Tuple<int, int>(0, 0);//(number of data points, productivity level)
            }

            int numberOfDataPoints = 0;
            foreach (var dataPoint in database)
            {
                productiveDataPoints = productivityHour[dataPoint.DateHour].Item1 + 1;
                productivityLevelTotalHour = productivityHour[dataPoint.DateHour].Item2 + dataPoint.ProdutivityLevel;
                productivityHour[dataPoint.DateHour] = new Tuple<int, int>(productiveDataPoints, productivityLevelTotalHour);
                numberOfDataPoints++;
            }

            if (numberOfDataPoints > 20)
            {
                Tuple<List<Tuple<int, float>>, List<Tuple<int, float>>> bestWorstProd =
                    CalculateBestWorstTimes(CalculateAverage(productivityHour));

                List<Tuple<int, float>> bestProductivityTimes = bestWorstProd.Item1;
                List<Tuple<int, float>> worstProductivityTimes = bestWorstProd.Item2;

                string summaryMostText = "";
                for (int i = 0; i < bestProductivityTimes.Count; i++)
                {
                    if (bestProductivityTimes[i].Item1 > 12)
                    {
                        summaryMostText += bestProductivityTimes[i].Item1 - 12 + "pm \n";
                    }
                    else if (bestProductivityTimes[i].Item1 == 0)
                    {
                        summaryMostText += 12 + "am \n";
                    }
                    else if (bestProductivityTimes[i].Item1 == 12)
                    {
                        summaryMostText += 12 + "pm \n";
                    }
                    else
                    {
                        summaryMostText += bestProductivityTimes[i].Item1 + "am \n";
                    }
                }

                string summaryWorstText = "";
                for (int i = 0; i < worstProductivityTimes.Count; i++)
                {
                    if (worstProductivityTimes[i].Item1 > 12)
                    {
                        summaryWorstText += worstProductivityTimes[i].Item1 - 12 + "pm \n";
                    }
                    else if (worstProductivityTimes[i].Item1 == 0)
                    {
                        summaryWorstText += 12 + "am \n";
                    }
                    else if (worstProductivityTimes[i].Item1 == 12)
                    {
                        summaryWorstText += 12 + "pm \n";
                    }
                    else
                    {
                        summaryWorstText += worstProductivityTimes[i].Item1 + "am \n";
                    }
                }
                t_SummaryMost.Text = summaryMostText;
                t_SummaryLeast.Text = summaryWorstText;
            }
            else
            {
                t_SummaryMost.Text = "To ensure accuracy, you have to have a minumum of 20 data points.";
                t_SummaryLeast.Text = "Currently you have " + numberOfDataPoints + " data point/s.";
            }
        }

        //calculate the average of an array of tuples
        float[] CalculateAverage(Tuple<int, int>[] prodHours)
        {
            float[] prodHoursAverage = new float[24];
            for (int i = 0; i < prodHours.Length; i++)
            {
                if (prodHours[i].Item1 != 0)
                {
                    prodHoursAverage[i] = (float)prodHours[i].Item2 / (float)prodHours[i].Item1;
                }
            }
            return prodHoursAverage;
        }

        //calculates the best and worst times for productivity based on averages
        Tuple<List<Tuple<int, float>>, List<Tuple<int, float>>> CalculateBestWorstTimes(float[] prodHoursAverages)
        {
            float max = 0;
            float min = 100;

            List<Tuple<int, float>> bestProdTimes = new List<Tuple<int, float>>();
            List<Tuple<int, float>> worstProdTimes = new List<Tuple<int, float>>();

            for (int i = 0; i < prodHoursAverages.Length; i++)
            {
                if (max < prodHoursAverages[i])
                {
                    max = prodHoursAverages[i];
                }

                if (min > prodHoursAverages[i] && prodHoursAverages[i] != 0)
                {
                    min = prodHoursAverages[i];
                }
            }

            for (int hour = 0; hour < prodHoursAverages.Length; hour++)
            {
                if (prodHoursAverages[hour] >= max - 0.5f && prodHoursAverages[hour] != 0)
                {
                    bestProdTimes.Add(new Tuple<int, float>(hour, prodHoursAverages[hour]));
                }

                if (prodHoursAverages[hour] <= min + 0.5f && prodHoursAverages[hour] != 0)
                {
                    worstProdTimes.Add(new Tuple<int, float>(hour, prodHoursAverages[hour]));
                }
            }
            Tuple<List<Tuple<int, float>>, List<Tuple<int, float>>> bestWorstProdTimes =
                new Tuple<List<Tuple<int, float>>, List<Tuple<int, float>>>(bestProdTimes, worstProdTimes);
            return bestWorstProdTimes;
        }
    }
}
