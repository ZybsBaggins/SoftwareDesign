using System;

abstract class AbstractClass
{
    public void TemplateMethod()
    {
        MethodX();
        MethodA();
        MethodB();
        MethodY();
    }

    protected abstract void MethodA();
    protected abstract void MethodB();

    private void MethodX()
    {
        Console.WriteLine("MethodX called");
    }

    private void MethodY()
    {
        Console.WriteLine("MethodY called");
    }
}

class ConcreteClass1 : AbstractClass
{
    protected override void MethodA()
    {
        Console.WriteLine("ConcreteClass1 MethodA called");
    }

    protected override void MethodB()
    {
        Console.WriteLine("ConcreteClass1 MethodB called");
    }
}

class ConcreteClass2 : AbstractClass
{
    protected override void MethodA()
    {
        Console.WriteLine("ConcreteClass2 MethodA called");
    }

    protected override void MethodB()
    {
        Console.WriteLine("ConcreteClass2 MethodB called");
    }
}

class Program
{
    static void Main()
    {
        AbstractClass instance1 = new ConcreteClass1();
        instance1.TemplateMethod();

        AbstractClass instance2 = new ConcreteClass2();
        instance2.TemplateMethod();
    }
}

