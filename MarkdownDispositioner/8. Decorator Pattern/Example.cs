using System;

namespace DecoratorPattern
{
    // Component Interface
    public interface ITesla
    {
        string GetDescription();
        double GetCost();
    }

    // Concrete Component
    public class ModelY : ITesla
    {
        public string GetDescription()
        {
            return "Tesla Model Y";
        }

        public double GetCost()
        {
            return 50000; // Base cost of Tesla Model Y
        }
    }

    // Decorator Abstract Class
    public abstract class TeslaDecorator : ITesla
    {
        protected ITesla _decoratedTesla;

        public TeslaDecorator(ITesla tesla)
        {
            _decoratedTesla = tesla;
        }

        public virtual string GetDescription()
        {
            return _decoratedTesla.GetDescription();
        }

        public virtual double GetCost()
        {
            return _decoratedTesla.GetCost();
        }
    }

    // Concrete Decorator for adding color
    public class ColorDecorator : TeslaDecorator
    {
        public ColorDecorator(ITesla tesla) : base(tesla) { }

        public override string GetDescription()
        {
            return base.GetDescription() + ", Color";
        }

        public override double GetCost()
        {
            return base.GetCost() + 1000; // Adding cost for color
        }
    }

    // Concrete Decorator for adding premium features
    public class PremiumDecorator : TeslaDecorator
    {
        private int _level;

        public PremiumDecorator(ITesla tesla, int level) : base(tesla)
        {
            _level = level;
        }

        public override string GetDescription()
        {
            return base.GetDescription() + $", Premium Level {_level}";
        }

        public override double GetCost()
        {
            return base.GetCost() + 2000 * _level; // Adding cost for premium features based on level
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Create a base Tesla Model Y
            ITesla myTesla = new ModelY();
            Console.WriteLine($"Description: {myTesla.GetDescription()}");
            Console.WriteLine($"Cost: {myTesla.GetCost()}");

            // Add color to the Tesla
            myTesla = new ColorDecorator(myTesla);
            Console.WriteLine($"Description after color: {myTesla.GetDescription()}");
            Console.WriteLine($"Cost after color: {myTesla.GetCost()}");

            // Add premium features to the Tesla
            myTesla = new PremiumDecorator(myTesla, 2); // Level 2 premium features
            Console.WriteLine($"Description after premium: {myTesla.GetDescription()}");
            Console.WriteLine($"Cost after premium: {myTesla.GetCost()}");

            Console.ReadLine();
        }
    }
}
