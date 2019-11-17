namespace ActivationFunctions
{
    public class Step : ActivationFunction
    {
        public override double Impuls(double net)
        {
            if (net == 0.0)
                return 0.0;

            return net > 0.0 ? 1.0 : -1.0;
        }
    }
}
