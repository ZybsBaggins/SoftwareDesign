using System;
using System.Collections.Generic;
using System.Linq;

class SubjectData
{
    public int Measurement { get; set; }
}

interface IObserver
{
    void Update(SubjectData subjectData);
}

interface ISubject
{
    void Attach(IObserver observer);
    void Detach(IObserver observer);
    void Notify();
}

class ConcreteSubject : ISubject
{
    private readonly List<IObserver> observers = new();
    private readonly SubjectData state = new();

    public void Attach(IObserver observer)
    {
        observers.Add(observer);
    }

    public void Detach(IObserver observer)
    {
        observers.Remove(observer);
    }

    public void Notify()
    {
        foreach (var observer in observers)
        {
            observer.Update(state);
        }
    }

    public void SetData(int data)
    {
        state.Measurement = data;
        Notify();
    }
}

class ConcreteObserver : IObserver
{
    public ConcreteObserver(ConcreteSubject subject)
    {
        subject.Attach(this);
    }

    public void Update(SubjectData subjectData)
    {
        Console.WriteLine($"Observer received new data: {subjectData.Measurement}");
    }
}

class Program
{
    static void Main()
    {
        var subject = new ConcreteSubject();
        var observer1 = new ConcreteObserver(subject);
        var observer2 = new ConcreteObserver(subject);

        subject.SetData(2);
        subject.SetData(3);
    }
}
