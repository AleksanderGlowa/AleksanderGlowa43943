using System;
using System.Collections.Generic;
using System.Linq;

namespace Schronisko
{
    public class Volunteer
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        public Volunteer(string name, string surname)
        {
            Name = name;
            Surname = surname;
        }

        public override string ToString()
        {
            return $"{Name} {Surname}";
        }
    }
    public class Animal
    {
        public string Name { get; set; }
        public string Species { get; set; }
        public int Age { get; set; }
        public bool IsAdopted { get; set; }
        public Volunteer Caretaker { get; set; } 

        public Animal(string name, string species, int age)
        {
            Name = name;
            Species = species;
            Age = age;
            IsAdopted = false;
        }

        public override string ToString()
        {
            string caretakerInfo = Caretaker != null ? $", Opiekun: {Caretaker}" : ", Brak opiekuna";
            string status = IsAdopted ? "[ADOPTOWANY]" : "[W SCHRONISKU]";
            return $"{status} {Species} {Name}, Wiek: {Age}{caretakerInfo}";
        }
    }
    public class Adoption
    {
        public Animal AdoptedAnimal { get; set; }
        public string AdopterName { get; set; }
        public DateTime AdoptionDate { get; set; }

        public Adoption(Animal animal, string adopterName)
        {
            AdoptedAnimal = animal;
            AdopterName = adopterName;
            AdoptionDate = DateTime.Now;
            animal.IsAdopted = true; 
        }

        public override string ToString()
        {
            return $"Adopcja: {AdoptedAnimal.Name} -> {AdopterName} (Data: {AdoptionDate.ToShortDateString()})";
        }
    }

    class Program
    {
        static List<Animal> animals = new List<Animal>();
        static List<Volunteer> volunteers = new List<Volunteer>();
        static List<Adoption> adoptions = new List<Adoption>();

        static void Main(string[] args)
        {
            bool running = true;

            while (running)
            {
                Console.WriteLine("\n MENU SCHRONISKA");
                Console.WriteLine("1 - Zarejestruj zwierzaka");
                Console.WriteLine("2 - Zarejestruj wolontariusza");
                Console.WriteLine("3 - Przydziel Pana który będzie się opiekowal");
                Console.WriteLine("4 - Adoptuj zwierza");
                Console.WriteLine("5 - Wypisz listę i zakończ");
                Console.Write("Co chcesz zrobić: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Podaj imię: ");
                        string name = Console.ReadLine();
                        Console.Write("Podaj gatunek: ");
                        string species = Console.ReadLine();
                        Console.Write("Podaj wiek: ");
                        int age = int.Parse(Console.ReadLine());
                        animals.Add(new Animal(name, species, age));
                        break;

                    case "2":
                        Console.Write("Imię wolontariusza: ");
                        string vName = Console.ReadLine();
                        Console.Write("Nazwisko wolontariusza: ");
                        string vSurname = Console.ReadLine();
                        volunteers.Add(new Volunteer(vName, vSurname));
                        break;

                    case "3":
                        if (animals.Count == 0 || volunteers.Count == 0)
                        {
                            Console.WriteLine("Brak danych do przydzielenia.");
                            break;
                        }

                        Console.WriteLine("\nWybierz ID zwierzęcia:");
                        for (int i = 0; i < animals.Count; i++)
                            Console.WriteLine($"{i}. {animals[i].Name}");
                        int aIdx = int.Parse(Console.ReadLine());

                        Console.WriteLine("Wybierz ID wolontariusza:");
                        for (int i = 0; i < volunteers.Count; i++)
                            Console.WriteLine($"{i}. {volunteers[i]}");
                        int vIdx = int.Parse(Console.ReadLine());

                        animals[aIdx].Caretaker = volunteers[vIdx];
                        Console.WriteLine("Opiekun przypisany!");
                        break;

                    case "4":
                        var availableAnimals = animals.Where(a => !a.IsAdopted).ToList();
                        if (availableAnimals.Count == 0)
                        {
                            Console.WriteLine("Brak zwierzaków do adopcji.");
                            break;
                        }

                        Console.WriteLine("\n Zwierzaki dostępne do adopcji:");
                        for (int i = 0; i < availableAnimals.Count; i++)
                            Console.WriteLine($"{i}. {availableAnimals[i].Name}");
                        
                        Console.Write("Wybierz numer z listy powyżej: ");
                        int adoptIdx = int.Parse(Console.ReadLine());
                        
                        Console.Write("Nazwisko adoptującego: ");
                        string adopter = Console.ReadLine();

                        adoptions.Add(new Adoption(availableAnimals[adoptIdx], adopter));
                        Console.WriteLine("Adopcja skuteczna ha!");
                        break;

                    case "5":
                        Console.WriteLine("\n LISTA ZWIERZĄT");
                        foreach (var a in animals) Console.WriteLine(a);

                        Console.WriteLine("\n LISTA WOLONTARIUSZY");
                        foreach (var v in volunteers) Console.WriteLine(v);

                        Console.WriteLine("\n HISTORIA ADOPCJI");
                        foreach (var ad in adoptions) Console.WriteLine(ad);
                        
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Nieznana opcja.");
                        break;
                }
            }
        }
    }
}