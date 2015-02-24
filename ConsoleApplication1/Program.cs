using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            
            /*workers[0] = new Worker("Вася", 45, 165168168);
            workers[1] = new Worker("Петя", 27, 41649164);
            workers[2] = new Worker("Светлана", 23);
            workers[3] = new Driver("Ivan",35,54246,"VAZ",256);
            workers[4] = new Manager("Вася", 45, 165168168, 10);*/

            Random rnd = new Random();
            string[] names = new string[5] { "Вася", "Петр", 
                "Светлана", "Елена", "Иван" };

            Worker[] workers = new Worker[rnd.Next(5,10)];
            for (int i = 0; i < workers.GetLength(0); i++)
            {
                if (rnd.Next(1,3) == 1)
                {
                    workers[i] = new Driver(
                        names[rnd.Next(0, names.GetLength(0))], 
                        rnd.Next(20, 70), 
                        rnd.Next(111111, 999999), 
                        "VAZ", 
                        rnd.Next(100, 256));
                }
                else
                {
                    workers[i] = new Manager(
                        names[rnd.Next(0, names.GetLength(0))], 
                        rnd.Next(20, 70), 
                        rnd.Next(111111, 999999), 
                        rnd.Next(5, 20));
                }
            }

            for (int i = 0; i < workers.GetLength(0); i++)
            {
                workers[i].Print();
            }

            /*Driver driver1 = new Driver("Oleg", 25, 62626416, "BMW", 128);
            Worker worker1 = driver1;
            worker1.Print();

            Driver dr = (Driver)worker1;*/
            //Console.WriteLine(dr.hours);

            //---------------------------------------

            /*Driver dr1 = new Driver("Вася", 45, 165168168,"UAZ",
                10);
            worker1 = dr1;*/

            /*if (worker1 is Driver)
            {
                dr = (Driver)worker1;
                Console.WriteLine(dr.hours);
            }*/

            /*dr = worker1 as Driver;
            if (dr != null)
            {
                Console.WriteLine(dr.hours);
            }*/
            




            /*for (int i = 0; i < workers.GetLength(0); i++)
            {
                workers[i].Print();
                Console.WriteLine();
            }*/

            //Console.WriteLine(Worker.count);

            /*Worker.PrintWorkers(workers);

            Worker worker = new Worker("Jenny", 26);
            worker.Print();

            double x = Math.Sin(1.57);
            Console.WriteLine(x);*/

            /*Driver driver = new Driver("Ivan",35,54246,"VAZ",256);
            driver.Print();

            Manager manager1 = new Manager("Вася", 45, 165168168, 10);
            manager1.Print();*/

            Driver dr1 = new Driver("Ivan", 35, 54246, "VAZ", 256);
            Console.WriteLine(dr1.PayTax());

            Console.ReadLine();
        }

        
    }

    public interface IPayTax
    {
        double PayTax();
    }

    abstract class Worker
    {
        private string name;
        private int age;
        public Int64 snn;
        public static int count;
        protected double salary;

        public string Name
        {
            get { return name; }
        }
        public int Age
        {
            get { return age; }
            set
            {
                if (value < 0)
                {
                    Console.WriteLine("Неверно задан возраст!");
                }
                else
                {
                    age = value;
                }
            }
        }

        abstract public int GetBonus();

       public virtual void Print()
        {
            Console.WriteLine("Имя: " + name);
            Console.WriteLine("Возраст: " + age);
            Console.WriteLine("ИНН: " + snn);
            Console.WriteLine("Премия: " + GetBonus());
        }

        public static void PrintWorkers(Worker[] workers)
        {
            for (int i = 0; i < workers.GetLength(0); i++)
            {
                workers[i].Print();
            }
        }

       public Worker(string name, int age, Int64 snn)
        {
            this.name = name;
            this.Age = age;
            this.snn = snn;
            count++;
            salary = 20000;
        }

        public Worker(string name, int age)
            : this(name, age, 0)
        {
            
        }
        static Worker()
        {
            count = 0;
        }
        public Worker()
        { }
    }
    sealed class Driver : Worker, IPayTax
    {
        public string carType;
        public int hours;

        public double PayTax()
        {
            return 0.13 * salary;
        }

        public Driver(string name, int age, Int64 snn, string carType,
            int hours) : base(name, age, snn)
        {
            this.carType = carType;
            this.hours = hours;
            salary = 35000;
        }

        public override int GetBonus()
        {
            return hours * 100;
        }

        public override void Print()
        {
            base.Print();
            Console.WriteLine("Марка машины: " + carType);
            Console.WriteLine("Количество часов: " + hours);
            Console.WriteLine();
        }
    }

    sealed class Manager : Worker 
    {
        public int projectsCount;

        public Manager(string name, int age, Int64 snn, 
            int projectsCount) : base(name, age, snn)
        {
            this.projectsCount = projectsCount;
            salary = 40000;
        }

        public override int GetBonus()
        {
            return projectsCount*1500;
        }

        public override void Print()
        {
            base.Print();
            Console.WriteLine("Количество проектов: " + projectsCount);
            Console.WriteLine();
        }
    }

    
}
