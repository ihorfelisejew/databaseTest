using DatabaseTest.Database;
using DatabaseTest.Database.Entities;
using DatabaseTest.Database.GenericReposotiry;
using EFCoreProject.Services.BuyerServices;
using EFCoreProject.Services.CheckServices;
using EFCoreProject.Services.ProductServices;

namespace DatabaseTest
{
    public class Program
    {
        static ApplicationDbContext dBContext = new ApplicationDbContext();

        static IGenericRepository<BuyerEntity> buyerRepository = new GenericRepository<BuyerEntity>(dBContext);
        static IBuyerService buyerService = new BuyerService(buyerRepository);

        static IGenericRepository<CheckEntity> checkRepository = new GenericRepository<CheckEntity>(dBContext);
        static ICheckService checkService = new CheckService(checkRepository);

        static IGenericRepository<ProductEntity> productRepository = new GenericRepository<ProductEntity>(dBContext);
        static IProductService productService = new ProductService(productRepository);
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.InputEncoding = System.Text.Encoding.Unicode;

            bool checkExit = false;
            do
            {
                Console.WriteLine("\n\bЩо бажаєте зробити?\n" +
                "   1 - Оформити покупку\n" +
                "   2 - Подивитись товари з конкретного чеку\n" +
                "   3 - Подивитись всі товари куплені покупцем\n" +
                "   4 - Подивитись всі товари куплені покупцем (з деякими даними про покупця)\n" +
                "   5 - Подивитись всі не куплені товари\n" +
                "   6 - Подивитись дані про окремий чек конкретного покупця\n" +
                "\n   n - Покинути програму");
                string s = Console.ReadLine();
                switch (s)
                {
                    case "1":
                        BuyerCreate();
                        break;
                    case "2":
                        CheckProducts();
                        break;
                    case "3":
                        CheckAllProducts();
                        break;
                    case "4":
                        CheckAllProductsWithDataAboutBuyer();
                        break;
                    case "5":
                        GetAllNoBuyProducts();
                        break;
                    case "6":
                        GetCheckFromBuyer();
                        break;
                    case "n":
                        checkExit = true;
                        break;
                    default:
                        Console.WriteLine("Такого варіанту немає!");
                        break;
                }
            } while (!checkExit);
        }
        static void GetCheckFromBuyer()
        {
            Console.WriteLine("Введіть ім'я покупця: ");
            string name = Console.ReadLine();
            Console.WriteLine("Введіть прізвище покупця: ");
            string surname = Console.ReadLine();
            Console.WriteLine("Введіть № чеку: ");
            int n = int.Parse(Console.ReadLine());

            CheckEntity check = buyerService
                .GetCheckFromBuyerCheckList(name, surname, n);

            Console.WriteLine($"\nНомер чеку: {check.Id}\n" +
                   $"Дата покупки: {check.DateBuy.ToShortDateString()} {check.DateBuy.ToLongTimeString()}\n");

            List<ProductEntity> products = checkService.GetProductsByCheckId(check.Id);

            if (products != null)
            {
                List<int> count = new List<int>();
                for (int i = 0; i < products.Count; i++)
                {
                    count.Add(1);
                    for (int j = 0; j < products.Count; j++)
                    {
                        if (products[i].Name == products[j].Name && i != j)
                        {
                            count[i]++;
                        }
                    }
                }

                Console.WriteLine("\nОсь товари з даного чеку:");
                double summ = 0;
                for (int i = 0; i < products.Count; i += count[i])
                {
                    Console.WriteLine($"Код:{products[i].Id} {products[i].Name} ({count[i]} шт.)...........{products[i].Price} грн.");
                    summ += products[i].Price * count[i];
                }
                Console.WriteLine($"\nTotal price: {summ} грн");
            }
            else
            {
                Console.WriteLine("Дані вказані некоректно!");
            }

        }
        static void GetAllNoBuyProducts()
        {
            List<ProductEntity> noBuyProducts = productService.GetAllNoBuy();

            List<int> count = new List<int>();
            for (int i = 0; i < noBuyProducts.Count; i++)
            {
                count.Add(1);
                for (int j = 0; j < noBuyProducts.Count; j++)
                {
                    if (noBuyProducts[i].Name == noBuyProducts[j].Name && i != j)
                    {
                        count[i]++;
                    }
                }
            }
            for (int i = 0; i < noBuyProducts.Count; i += count[i])
            {
                Console.WriteLine($"Код:{noBuyProducts[i].Id} {noBuyProducts[i].Name} ({count[i]} шт.)...........{noBuyProducts[i].Price} грн.");
            }
        }
        static void CheckAllProductsWithDataAboutBuyer()
        {
            List<BuyerEntity> buyers = buyerService.GetAllBuyers();
            foreach(BuyerEntity buyer in buyers)
            {
                Console.WriteLine(new String('=', 80));
                Console.WriteLine($"\n\nІм'я покупця: {buyer.Name}\n" +
                        $"Прізвище покупця: {buyer.Surname}\n" +
                        $"Номер телефону: {buyer.Phone}\n" +
                        $"Дата народження: {buyer.BirthDate.ToLongDateString()}");
                for (int c = 0; c < buyer.Checks.Count; c++)
                {
                    List<ProductEntity> products = checkService.GetProductsByCheckId(buyer.Checks.ToList()[c].Id);
                    if (products != null)
                    {
                        List<int> count = new List<int>();
                        for (int i = 0; i < products.Count; i++)
                        {
                            count.Add(1);
                            for (int j = 0; j < products.Count; j++)
                            {
                                if (products[i].Name == products[j].Name && i != j)
                                {
                                    count[i]++;
                                }
                            }
                        }

                        Console.WriteLine($"\nОсь товари з чеку №{c+1}:");
                        double summ = 0;
                        for (int i = 0; i < products.Count; i += count[i])
                        {
                            Console.WriteLine($"Код:{products[i].Id} {products[i].Name} ({count[i]} шт.)...........{products[i].Price} грн.");
                            summ += products[i].Price * count[i];
                        }
                        Console.WriteLine($"\nTotal price: {summ} грн");
                    }
                    else
                    {
                        Console.WriteLine("Дані вказані некоректно!");
                    }
                    Console.WriteLine("\n" + new String('-', 20));
                }
                Console.WriteLine(new String('=', 80));
            }
        }
        static void CheckAllProducts()
        {
            Console.WriteLine("Введіть ім'я покупця: ");
            string name = Console.ReadLine();
            Console.WriteLine("Введіть прізвище покупця: ");
            string surname = Console.ReadLine();

            int countOfChecks = buyerService.GetCheckCounts(name, surname);
            if (countOfChecks > 0)
            {
                for (int c = 1; c <= countOfChecks; c++)
                {
                    List<ProductEntity> products = buyerService.GetBuyedProducts(name, surname, c);
                    if (products != null)
                    {
                        List<int> count = new List<int>();
                        for (int i = 0; i < products.Count; i++)
                        {
                            count.Add(1);
                            for (int j = 0; j < products.Count; j++)
                            {
                                if (products[i].Name == products[j].Name && i != j)
                                {
                                    count[i]++;
                                }
                            }
                        }

                        Console.WriteLine($"\nОсь товари з чеку №{c}:");
                        double summ = 0;
                        for (int i = 0; i < products.Count; i += count[i])
                        {
                            Console.WriteLine($"Код:{products[i].Id} {products[i].Name} ({count[i]} шт.)...........{products[i].Price} грн.");
                            summ += products[i].Price * count[i];
                        }
                        Console.WriteLine($"\nTotal price: {summ} грн");
                    }
                    else
                    {
                        Console.WriteLine("Дані вказані некоректно!");
                    }
                    Console.WriteLine(new String('-', 20));
                }
            }
            else
            {
                Console.WriteLine("Схоже дані вказані не коректно." +
                    "\nСпробуйте ще раз!");
            }
            
        }
        static void CheckProducts()
        {
            Console.WriteLine("Введіть ім'я покупця: ");
            string name = Console.ReadLine();
            Console.WriteLine("Введіть прізвище покупця: ");
            string surname = Console.ReadLine();
            Console.WriteLine("Введіть № чеку: ");
            int n = int.Parse(Console.ReadLine());

            List<ProductEntity> products = buyerService.GetBuyedProducts(name, surname, n);

            if (products != null)
            {
                List<int> count = new List<int>();
                for(int i = 0; i < products.Count; i++)
                {
                    count.Add(1);
                    for(int j = 0; j < products.Count; j++)
                    {
                        if (products[i].Name == products[j].Name && i!=j)
                        {
                            count[i]++;
                        }
                    }
                }

                Console.WriteLine("\nОсь товари з даного чеку:");
                double summ = 0;
                for(int i = 0; i < products.Count; i += count[i])
                {
                    Console.WriteLine($"Код:{products[i].Id} {products[i].Name} ({count[i]} шт.)...........{products[i].Price} грн.");
                    summ += products[i].Price * count[i];
                }
                Console.WriteLine($"\nTotal price: {summ} грн");
            }
            else
            {
                Console.WriteLine("Дані вказані некоректно!");
            }
        }
        static void BuyerCreate()
        {
            Console.WriteLine("Введіть ім'я покупця:");
            string name = Console.ReadLine();
            Console.WriteLine("Введіть прізвище покупця:");
            string surname = Console.ReadLine();

            BuyerEntity buyer = buyerService.GetByNameAndSurname(name, surname);
            if (buyer == null)
            {
                Console.WriteLine("Введіть номер телефону:");
                string phone = Console.ReadLine();
                Console.WriteLine("Дату народження:");
                DateTime birthDate = DateTime.Parse(Console.ReadLine());
                buyer = new BuyerEntity()
                {
                    Name = name,
                    Surname = surname,
                    Phone = phone,
                    BirthDate = birthDate
                };
                buyerService.Create(buyer);
            }

            CheckEntity check = new CheckEntity()
            {
                DateBuy = DateTime.Now,
                BuyerFK = buyer.Id
            };
            checkService.Create(check);

            for (int i = 0; i >= 0; i++)
            {
                Console.WriteLine("Введіть товар який варто додати: ");
                string nameProduct = Console.ReadLine();
                if (nameProduct == "n")
                    break;

                Console.WriteLine("Введіть кількість товару: ");
                int countProduct = int.Parse(Console.ReadLine());
                for (int j = 0; j < countProduct; j++)
                {
                    if(!productService.BuyProducts(check.Id, nameProduct))
                    {
                        Console.WriteLine("Даного товару немає в наявності!");
                        break;
                    }
                }
            }
            checkService.Update(check);
            buyerService.Update(buyer);
        }
    }
}