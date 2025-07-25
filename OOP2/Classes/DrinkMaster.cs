using OOP2.Classes.Actions;
using OOP2.Classes.Ingredients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOP2.Classes.Drinks;
using System.Diagnostics.Contracts;
using OOP2.Classes.Interfaces;

namespace OOP2.Classes
{
    internal class DrinkMaster
    {
        public DrinkMaster()
        {
            this.drinks = new List<Drink>();
            this.AddTemplateDrinks();
        }

        List<OOP2.Classes.Actions.Action> actions = new List<OOP2.Classes.Actions.Action>
        {
            new Mix(),
            new Add(),
            new Beat(),
            new Boil(),
            new Pour(),
            new Grind()
        };

        List<Ingredient> ingredients = new List<Ingredient>
        {
            new Ice(),
            new Milk(),
            new Syrup(),
            new Water(),
            new CoffeeBean()
        };

        public List<Drink> drinks;

        public void create(OOP2.Classes.Actions.Action currentNode)
        {
            Console.Clear();
            Console.WriteLine("Текущее дерево: ");
            this.drinks[this.drinks.Count - 1].print();
            Console.WriteLine($"Текущий узел: {currentNode.name}");
            Console.WriteLine("Введите -1 для завершения ветви. Если в множестве введенных элементов встретится -1, ветка завершится\n");
            
            {
                int i = 0;
                Console.WriteLine("Actions:");
                while (i < actions.Count)
                {
                    Console.WriteLine($"{i}. " + this.actions[i].name);
                    ++i;
                }
                Console.WriteLine("Ingredients:");
                while (i < actions.Count + this.ingredients.Count)
                {
                    Console.WriteLine($"{i}. " + this.ingredients[i - this.actions.Count].name);
                    ++i;
                }
            }
            Console.WriteLine("Введите потомков для текущего узла '" + currentNode.name + "':");

            string input = Console.ReadLine();

            List<int> numbers = input
                .Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .Where(x => x < actions.Count + this.ingredients.Count)
                .ToList();

            if (numbers.Contains(-1)) return;

            List<int> actionsIndexes = numbers
                .Where(x =>
                    x >= 0 &&
                    x < this.actions.Count)
                .ToList();

            List<int> ingredietnsIndexes = numbers
                .Where(x =>
                    x >= 0 &&
                    x >= this.actions.Count &&
                    x < this.actions.Count + this.ingredients.Count)
                .Select(x => x - this.actions.Count)
                .ToList();

            currentNode.content.AddRange(
                actionsIndexes
                    .Select(i => this.actions[i])
                    .ToList());

            currentNode.content.AddRange(
                ingredietnsIndexes
                    .Select(i => this.ingredients[i])
                    .ToList());
             
            foreach (var elem in ingredietnsIndexes.Select(i => this.ingredients[i]).ToList())
            {
                Console.WriteLine("Введите массу для " + elem.name + ":");
                int mass = Convert.ToInt32(Console.ReadLine());

                var ing = currentNode.content
                    .OfType<Ingredient>()
                    .FirstOrDefault(ingredient => ingredient.name == elem.name);
                if (ing != null) ing.netMass = mass;
            }

            foreach (var act in actionsIndexes.Select(i => this.actions[i]).ToList())
                create(act);
        }

        public void initialization()
        {
            Console.WriteLine("Enter drink name: ");
            Drink drink = new Drink();
            drink.rootNode = new Add();
            drink.name = Console.ReadLine();
            drinks.Add(drink);
            this.create(drink.rootNode);
        }

        public void read()
        {
            Console.Clear();
            if (this.drinks.Count == 0) Console.WriteLine("empty...");
            foreach (var drink in this.drinks)
                drink.print();
            Console.ReadLine();
        }

        public void update()
        {
            int i = 0;
            foreach (var drink in this.drinks)
            {
                Console.Write($"{i++}.");
                drink.print();
            }
            Console.WriteLine("Choose drink number to update (any other to break): ");
            i = Convert.ToInt32(Console.ReadLine());
            if (i < 0 || i >= this.drinks.Count)
                return;
            else
            {
                this.drinks.RemoveAt(i);
                this.initialization();
            }
        }

        public void delete()
        {
            int i = 0;
            foreach (var drink in this.drinks)
            {
                Console.Write($"{i++}. ");
                drink.print();
            }
            Console.WriteLine("Choose drink to delete: ");
            i = Convert.ToInt32(Console.ReadLine());

            try
            {
                this.drinks.RemoveAt(i);
            }
            catch { }
        }

        public void start()
        {
            bool isRunning = true;
            while (isRunning)
            {
                Console.Clear();
                Console.WriteLine(
                    "CRUD Drink\n" +
                    "0. Create drink\n" +
                    "1. Check drinks\n" +
                    "2. Update some drink\n" +
                    "3. Delete drink\n" +
                    "-. end program");

                switch (Convert.ToInt32(Console.ReadLine()))
                {
                    case 0:
                        this.initialization();
                        break;
                    case 1:
                        this.read();
                        break;
                    case 2:
                        this.update();
                        break;
                    case 3:
                        this.delete();
                        break;
                    default:
                        isRunning = false;
                        break;
                }
            }
        }
        public void AddTemplateDrinks()
        {
            var water1 = new Water { netMass = 150 };
            var coffee1 = new CoffeeBean { netMass = 10 };

            var coffee2 = new CoffeeBean { netMass = 8 };
            var water2 = new Water { netMass = 100 };
            var milk2 = new Milk { netMass = 150 };

            var water3 = new Water { netMass = 100 };
            var coffee3 = new CoffeeBean { netMass = 10 };
            var milk3 = new Milk { netMass = 150 };
            var ice3 = new Ice { netMass = 50 };

            var water4 = new Water { netMass = 150 };
            var syrup4 = new Syrup { netMass = 30 };
            var ice4 = new Ice { netMass = 50 };

            // Американо
            var boilWater1 = new Boil { content = new List<IElement> { water1 } };
            var grindCoffee1 = new Grind { content = new List<IElement> { coffee1 } };

            // Латте
            var grind2 = new Grind { content = new List<IElement> { coffee2 } };
            var boil2 = new Boil { content = new List<IElement> { water2 } };
            var mix2 = new Mix { content = new List<IElement> { boil2, milk2 } };

            // Кофейный милкшейк
            var boil3 = new Boil { content = new List<IElement> { water3 } };
            var grind3 = new Grind { content = new List<IElement> { coffee3 } };
            var addMilkIce3 = new Add { content = new List<IElement> { milk3, ice3 } };
            var addBoilGrind3 = new Add { content = new List<IElement> { boil3, grind3 } };
            var mix3 = new Mix { content = new List<IElement> { addBoilGrind3, addMilkIce3 } };

            // Холодный чай
            var boil4 = new Boil { content = new List<IElement> { water4 } };
            var addIce4 = new Add { content = new List<IElement> { ice4 } };
            var mix4 = new Mix { content = new List<IElement> { boil4, syrup4, addIce4 } };


            var americano = new Drink
            {
                name = "Американо",
                rootNode = new Add { content = new List<IElement> { boilWater1, grindCoffee1 } }
            };
            drinks.Add(americano);

            var latte = new Drink
            {
                name = "Латте",
                rootNode = new Add { content = new List<IElement> { grind2, mix2 } }
            };
            drinks.Add(latte);

            var milkshake = new Drink
            {
                name = "Кофейный милкшейк",
                rootNode = mix3
            };
            drinks.Add(milkshake);

            var icedTea = new Drink
            {
                name = "Холодный чай с сиропом",
                rootNode = mix4
            };
            drinks.Add(icedTea);
        }
    }
}