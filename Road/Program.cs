using System;
using System.Collections.Generic;
using System.Linq;

namespace Road
{
    class Program
    {
        List<CityData> City = new List<CityData>();
        List<string> queries = new List<string>();
        int QueryNumber = 0;
        public void input()
        {
            try
            {
                QueryNumber = 0;
                City.Clear();
                queries.Clear();
                Console.Write("Please enter the data (e.g. A-B:5,B-C:3,QUERY(A-B),C-D:6,C-B:5,QUERY(C-D) ... ) : ");
                string cities = Console.ReadLine().ToUpper(); ;
                inputCast(cities);
                queryResult();
            }
            catch (Exception e)
            {
                Console.WriteLine("Wrong Type!");
                input();
            }
        }
        public void inputCast(string cities)
        {
            string[] cityCast = cities.Split(",");
            for (int i = 0; i < cityCast.Length; i++)
            {
                if (cityCast[i].StartsWith("QUERY") == false)
                {
                    roadCast(cityCast[i]);
                }
                if (cityCast[i].StartsWith("QUERY") == true)
                {
                    queryCast(cityCast[i]);
                }
            }
        }
        public void roadCast(string roadData)
        {
            try
            {
                string[] castRoad = roadData.Split(":");
                castRoad[0] = castRoad[0].Replace("-", "");
                if (castRoad[0].Length == 2)
                {
                    addRoad(castRoad);
                }
                else
                {
                    wrongType();
                }
            }
            catch
            {
                wrongType();
            }
        }
        public void queryCast(string queryData)
        {
            try
            {
                string[] castRoad = queryData.Split("(");
                castRoad[1] = castRoad[1].Substring(0, castRoad[1].Length - 1);
                castRoad[1] = castRoad[1].Replace("-", "");
                if (castRoad[1].Length == 2)
                {
                    addQuery(castRoad[1]);
                }
                else
                {
                    wrongType();
                }
            }
            catch
            {
                wrongType();
            }
        }
        public void addRoad(string[] road)
        {
            CityData c = new CityData();
            try
            {
                c.cities = road[0];
                c.distance = Convert.ToInt32(road[1]);
                c.queryNo = QueryNumber;
                City.Add(c);
            }
            catch
            {
                wrongType();
            }
        }
        public void addQuery(string Query)
        {
            queries.Add(Query);
            QueryNumber++;
        }
        public void wrongType()
        {
            throw new Exception();
        }
        public void queryResult()
        {
            string[] result = new string[2];
            for (int i = 0; i < queries.Count; i++)
            {
                var x = City.Where(s => s.cities == queries[i]).ToList();
                if (x.Count > 1)
                {
                    var z = x.Where(s => s.queryNo <= i).ToList();
                    var q = z.Max(s => s.queryNo);
                    var f = z.FindIndex(s => s.queryNo == q);
                    result[0] = z[f].cities;
                    result[1] = z[f].distance.ToString();
                    write(result);
                }
                else if (x.Count == 1)
                {
                    result[0] = x[0].cities;
                    result[1] = x[0].distance.ToString();
                    write(result);
                }
                else
                {
                    writeNull(queries[i]);
                }
            }
        }
        public void write(string[] writeResult)
        {
            Console.WriteLine(writeResult[0] + ":" + writeResult[1]);
        }
        public void writeNull(string value)
        {
            Console.WriteLine(value + ":-");
        }

        static void Main(string[] args)
        {
            Program p = new Program();
            p.input();
            Console.ReadLine();
        }
    }
}
