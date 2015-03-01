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
            //генератор псевдослучайных чисел
            Random rnd = new Random();
            string[] names = new string[5] { "Вася", "Петр", 
                "Светлана", "Елена", "Иван" };

            /*
             * для реализации сортировки пришлось перейти от массива
             * к списку. суть примерно та же, но есть и отличия. подробнее
             * будет на следующих уроках
             */
            List<Worker> workers = new List<Worker>();
            //в цикле случайным образом заполняем наш список менеджерами и водителями
            //при этом возраст, ИНН и другие характеристики задаются случайным образом
            for (int i = 0; i < rnd.Next(5,10); i++)
            {
                if (rnd.Next(1,3) == 1)
                {
                    workers.Add(new Driver(
                        names[rnd.Next(0, names.GetLength(0))], 
                        rnd.Next(20, 70), 
                        rnd.Next(111111, 999999), 
                        "VAZ", 
                        rnd.Next(100, 256)));
                }
                else
                {
                    workers.Add(new Manager(
                        names[rnd.Next(0, names.GetLength(0))], 
                        rnd.Next(20, 70), 
                        rnd.Next(111111, 999999), 
                        rnd.Next(5, 20)));
                }
            }
            //этот метод отсортирует наших работников
            workers.Sort();

            for (int i = 0; i < workers.Count; i++)
            {
                workers[i].Print();
            }

            

            Driver dr1 = new Driver("Ivan", 35, 54246, "VAZ", 256);
            Console.WriteLine(dr1.PayTax());

            Console.ReadLine();
        }

        
    }

    //добавили собственный интерфейс
    public interface IPayTax
    {
        //в теле интерфейса определили сигнатуру метода,
        //который должны реализовать классы, реализующие этот интерфейс
        double PayTax();
    }

    //реализует интерфейс "Я сравнимый" для сортировки. см. метод Main();
    abstract class Worker : IComparable
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

        //абстрактный метод, который ОБЯЗАТЕЛЬНО должен быть реализован
        //в классах-наследниках
        abstract public int GetBonus();

        /*
         * этот метод помечен виртуальным, что означает, что он
         * МОЖЕТ БЫТЬ переопределен в классах-наследниках
         */
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

        /*
         * реализация метода из интерфейса "Я сравнимый".
         * сравнение будем проводить по размеру премии.
         */
        public int CompareTo(object obj)
        {
            if (this.GetBonus() < (obj as Worker).GetBonus())
            {
                return -1;
            }
            if (this.GetBonus() == (obj as Worker).GetBonus())
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }
    }

    /*
     * Водитель является наследником работника и реализует интерфейс "Я плачу налоги"
     */
    sealed class Driver : Worker, IPayTax
    {
        public string carType;
        public int hours;

        public double PayTax()
        {
            return 0.13 * salary;
        }

        /*
         * обращение к конструктору работника с помощью слова base(...)
         */
        public Driver(string name, int age, Int64 snn, string carType,
            int hours) : base(name, age, snn)
        {
            this.carType = carType;
            this.hours = hours;
            salary = 35000;
        }

        //переопределили для водителя метод начисления премии
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

    //является наследником работника
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
