using System;

namespace DesignPatterns
{
    // Strategy Pattern

    public interface IBurger
    {
        void Prepare();
    }

    public class ChickenBurger : IBurger
    {
        public void Prepare()
        {
            Console.WriteLine("Preparing Chicken Burger");
        }
    }

    public class BeefBurger : IBurger
    {
        public void Prepare()
        {
            Console.WriteLine("Preparing Beef Burger");
        }
    }

    public class VeggieBurger : IBurger
    {
        public void Prepare()
        {
            Console.WriteLine("Preparing Veggie Burger");
        }
    }

    public class BurgerContext
    {
        private IBurger _burger;

        public void SetBurger(IBurger burger)
        {
            _burger = burger;
        }

        public void PrepareBurger()
        {
            _burger?.Prepare();
        }
    }

    // Template Method Pattern

    public abstract class Meal
    {
        public void PrepareMeal()
        {
            PrepareIngredients();
            Cook();
            Serve();
        }

        protected abstract void PrepareIngredients();
        protected abstract void Cook();

        private void Serve()
        {
            Console.WriteLine("Serving the meal");
        }
    }

    public class PastaMeal : Meal
    {
        protected override void PrepareIngredients()
        {
            Console.WriteLine("Preparing pasta ingredients");
        }

        protected override void Cook()
        {
            Console.WriteLine("Cooking pasta");
        }
    }

    public class SaladMeal : Meal
    {
        protected override void PrepareIngredients()
        {
            Console.WriteLine("Preparing salad ingredients");
        }

        protected override void Cook()
        {
            Console.WriteLine("Mixing salad");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Strategy Pattern Example
            BurgerContext burgerContext = new BurgerContext();

            burgerContext.SetBurger(new ChickenBurger());
            burgerContext.PrepareBurger();  /

            burgerContext.SetBurger(new BeefBurger());
            burgerContext.PrepareBurger();  

            burgerContext.SetBurger(new VeggieBurger());
            burgerContext.PrepareBurger();  

            Console.WriteLine();

            // Template Method Pattern Example
            Meal pastaMeal = new PastaMeal();
            pastaMeal.PrepareMeal();

            Console.WriteLine();

            Meal saladMeal = new SaladMeal();
            saladMeal.PrepareMeal();
           
        }
    }
}
