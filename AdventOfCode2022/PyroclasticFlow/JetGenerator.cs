namespace Domain.PyroclasticFlow
{
    public class JetGenerator
    {
        public string JetPattern = "";
        public int Counter;
        public char FetchJetDirection()
        {
            var counter = Counter;
            Counter = (counter + 1) % JetPattern.Length;
            return JetPattern[counter];
        }
    }
}