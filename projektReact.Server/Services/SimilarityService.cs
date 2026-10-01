namespace projektReact.Server.Services
{
    public class SimilarityService
    {

        public static double GetJaroWinklerDistance(string s1, string s2)
        {
            if (string.IsNullOrEmpty(s1) || string.IsNullOrEmpty(s2))
            {
                return 0.0;
            }

            int m = 0;
            int t = 0;
            int maxDistance = Math.Max(s1.Length, s2.Length) / 2 - 1;

            bool[] s1Matches = new bool[s1.Length];
            bool[] s2Matches = new bool[s2.Length];

            for (int i = 0; i < s1.Length; i++)
            {
                int start = Math.Max(0, i - maxDistance);
                int end = Math.Min(i + maxDistance + 1, s2.Length);

                for (int j = start; j < end; j++)
                {
                    if (!s2Matches[j] && s1[i] == s2[j])
                    {
                        s1Matches[i] = true;
                        s2Matches[j] = true;
                        m++;
                        break;
                    }
                }
            }

            if (m == 0)
            {
                return 0.0;
            }

            int k = 0;
            for (int i = 0; i < s1.Length; i++)
            {
                if (s1Matches[i])
                {
                    while (!s2Matches[k])
                    {
                        k++;
                    }

                    if (s1[i] != s2[k])
                    {
                        t++;
                    }

                    k++;
                }
            }
            t /= 2;
            double jaro = ((double)m / s1.Length + (double)m / s2.Length + (double)(m - t) / m) / 3.0;
            const double prefixScalingFactor = 0.1;
            int prefixLength = 0;

            for (int i = 0; i < Math.Min(4, Math.Min(s1.Length, s2.Length)); i++)
            {
                if (s1[i] == s2[i])
                {
                    prefixLength++;
                }
                else
                {
                    break;
                }
            }
            return jaro + prefixScalingFactor * prefixLength * (1 - jaro);
        }
    }
}
