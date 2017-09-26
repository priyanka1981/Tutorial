using System;
namespace GrapgTutorial.Marbeles
{
    public class Marbles
    {
        private static readonly Random mRandom = new Random();
        public const int RED_MARBLE = 1;
        public const int BLUE_MARBLE = 2;
        public const int GREEN_MARBLE = 3;
        public const int ORANGE_MARBLE = 4;
        public Marbles()
        {
        }
		
        public static int PromptInt(string message)
		{
			int ret = -1;
			while (true)
			{
				Console.WriteLine(message);
				string str = Console.ReadLine();
				if (Int32.TryParse(str, out ret))
					break;
				else
					Console.WriteLine("'{0}' is invalid", str);
			}
			return ret;
		}


		public static int[] Solve(int red, int green, int blue, int orange, int count)
		{
            // TOOD: Return an array of integers of length [count]
            // each element in the array should contain a value from 1 to 4
            // the value represents a marble color (see constants above)
            // using the passed in values, you need to infer the probablility of each colored marble.
            // You should then *randomly* generate [count] number of marbles based on that probability

            // (i.e. if you were passed the values 10, 5, 5, 1 for the red, green, blue and orange parameters respectively
            // you should have approximately 10 red marbles for every 5 green and for every five blue marbles, and
            // there should 10 red marbles for approximately every orange marble you get)

            int[] color_array = new int[count];

            // creating expected probability array
            int[] expectedMarbles = new int[4];
            expectedMarbles[0] = red;
            expectedMarbles[1] = green;
            expectedMarbles[2] = blue;
            expectedMarbles[3] = orange;
            double[] expectedProbability = calculateRatio(expectedMarbles);

            // creating currentProbability array
            int[] currentMarbles = new int[4]{ 0,0,0,0 };
            // double[] currentProbability = new double[4] { 0,0,0,0 };

            // initialize the color array
            int nxt = nxtRandom(new int[]{0,0,0,0});
            color_array[0] = nxt;
            currentMarbles[nxt-1] = 1;
            // currentProbability[nxt] = 1;

            int total_counter = 1;
            while(total_counter < count)
            {
                nxt = rebalance(currentMarbles, expectedProbability);
                color_array[total_counter] = nxt;
                currentMarbles[nxt - 1] += 1;
                total_counter++;
            }

			return color_array;
		}

        public static void WriteOutStats(int[] results)
        {
            // TODO: output the total number of red, green, blue and orange marbles based on the array of results passed into you.
            // This array is the same array you generated in the Solve function above.

            int[] marbles = new int[4] { 0, 0, 0, 0 };
            foreach(int marble in results)
            {
                marbles[marble - 1] += 1;
            }

			int idx = 1;
			foreach (int marble in marbles)
			{
				switch (idx)
				{
					case RED_MARBLE:
						Console.WriteLine("RED: " + marble);
						break;
					case BLUE_MARBLE:
						Console.WriteLine("BLUE: " + marble);
						break;
					case GREEN_MARBLE:
						Console.WriteLine("GREEN: " + marble);
						break;
					case ORANGE_MARBLE:
						Console.WriteLine("ORANGE: " + marble);
						break;
				}

				idx++;

			}

        }

        public static int rebalance(int[] marbles, double[] expectedProbability) 
        {
            // double maxDev=0;
            double[] currentProbability = calculateRatio(marbles);

            int[] skipMarbles = new int[4] { 0, 0, 0, 0 };

            int counter = 0;
            // int counterMax = -1;
            while(counter < marbles.Length) 
            {
                if(currentProbability[counter] > expectedProbability[counter]) 
                {
                    skipMarbles[counter] = 1;
                }
                counter++;
            }

            // counterMax = counter;

            int nxtMarble = nxtRandom(skipMarbles);

            return nxtMarble;
        }

        public static double[] calculateRatio(int[] marbles)
        {
            double[] r = new double[4]{0.00, 0.00, 0.00, 0.00};
            int t = 0;
            
            for (int i = 0; i < marbles.Length; i++)
            {
                t += marbles[i];
            }

			for (int i = 0; i < marbles.Length; i++)
			{
                r[i] = (double)marbles[i] / (double)t;
			}

            return r;
        }

        public static int nxtRandom(int[] skipMarbles) 
        {
            int nxt = mRandom.Next(1, 5);
            while(skipMarbles[nxt-1] == 1) 
            {
                nxt = mRandom.Next(1, 5);
            }

            return nxt;
        }


    }
}
