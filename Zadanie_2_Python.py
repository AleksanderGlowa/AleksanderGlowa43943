import datetime

class Volunteer:
    def __init__(self, name, surname):
        self.name = name
        self.surname = surname

    def __str__(self):
        return f"{self.name} {self.surname}"

class Animal:
    def __init__(self, name, species, age):
        self.name = name
        self.species = species
        self.age = age
        self.caretaker = None  
        self.is_adopted = False

    def assign_caretaker(self, volunteer):
        self.caretaker = volunteer

    def __str__(self):
        caretaker_info = f", Opiekun: {self.caretaker}" if self.caretaker else ", Brak opiekuna"
        status = "ADOPTOWANY" if self.is_adopted else "W schronisku"
        return f"[{status}] {self.species} {self.name}, Wiek: {self.age}{caretaker_info}"

class Adoption:
    def __init__(self, animal, adopter_name):
        self.animal = animal
        self.adopter_name = adopter_name
        self.date = datetime.date.today()
        self.animal.is_adopted = True  

    def __str__(self):
        return f"Adopcja: {self.animal.name} trafił/a do: {self.adopter_name} (Data: {self.date})"

animals = []
volunteers = []
adoptions = []

def main():
    while True:
        print("\n MENU SCHRONISKA ")
        print("1 - Zarejestruj zwierzę")
        print("2 - Zarejestruj wolontariusza")
        print("3 - Przydziel opiekuna do zwierzaka")
        print("4 - Adopcja zwierzaka")
        print("5 - Wypisz wszystkie dane i zakończ")
        
        choice = input("Wybierz opcję: ")

        if choice == '1':
            name = input("Podaj imię zwierza: ")
            species = input("Podaj gatunek (np. Pies, Kot, Labrador, Fafik): ")
            age = input("Podaj wiek (lata - liczba np. 7): ")
            animals.append(Animal(name, species, age))
            print("Zwierzak dodany!")

        elif choice == '2':
            name = input("Imię wolontariusza: ")
            surname = input("Nazwisko wolontariusza: ")
            volunteers.append(Volunteer(name, surname))
            print("Wolontariusz dodany!")

        elif choice == '3':
            if not animals or not volunteers:
                print("Brak zwierząt lub wolontariuszy w bazie!")
                continue
            
            print("\nDostępne zwierzaki:")
            for i, animal in enumerate(animals):
                print(f"{i}. {animal.name} ({animal.species})")
            a_idx = int(input("Wybierz numer zwierzaka: "))

            print("\nDostępni wolontariusze:")
            for i, vol in enumerate(volunteers):
                print(f"{i}. {vol}")
            v_idx = int(input("Wybierz numer wolontariusza: "))

            animals[a_idx].assign_caretaker(volunteers[v_idx])
            print("Opiekun przydzielony!")

        elif choice == '4':
           
            available_animals = [a for a in animals if not a.is_adopted]
            
            if not available_animals:
                print("Brak zwierzaków do adopcji!")
                continue

            print("\nZwierzaki do adopcji:")
            for i, animal in enumerate(available_animals):
                print(f"{i}. {animal.name}")
            
            idx = int(input("Wybierz numer zwierzaka: "))
            adopter = input("Podaj nazwisko osoby adoptującej: ")
            
            adoptions.append(Adoption(available_animals[idx], adopter))
            print("Zwierzak zaadoptowany, brawo!")

        elif choice == '5':
            print("\n RAPORT KOŃCOWY ")
            print("\nLISTA ZWIERZAKÓW:")
            for a in animals: print(a)
            
            print("\nLISTA WOLONTARIUSZY:")
            for v in volunteers: print(v)
            
            print("\nLISTA ADOPCJI:")
            for ad in adoptions: print(ad)
            break
        else:
            print("Nieprawidłowy wybór.")

if __name__ == "__main__":
    main()