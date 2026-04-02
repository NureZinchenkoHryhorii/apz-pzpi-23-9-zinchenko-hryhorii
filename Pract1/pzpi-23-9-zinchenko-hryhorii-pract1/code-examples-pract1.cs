using System;

// Target — інтерфейс, який очікує клієнт
public interface ITarget
{
    void Request();
}

// Adaptee — існуючий клас з несумісним інтерфейсом
public class Adaptee
{
    public void SpecificRequest()
    {
        Console.WriteLine("Виклик SpecificRequest з Adaptee");
    }
}

// Adapter — узгоджує інтерфейси
public class Adapter : ITarget
{
    private Adaptee adaptee;

    public Adapter(Adaptee adaptee)
    {
        this.adaptee = adaptee;
    }

    public void Request()
    {
        // Перетворення виклику
        adaptee.SpecificRequest();
    }
}

// Client — використовує Target
class Program
{
    static void Main(string[] args)
    {
        Adaptee adaptee = new Adaptee();
        ITarget target = new Adapter(adaptee);

        target.Request();
    }
}
