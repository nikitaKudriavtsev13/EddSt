


using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel;// INotifyPropertyChanged
using System.ComponentModel.DataAnnotations;

using System.Runtime.CompilerServices; 
using System.Windows.Input;




namespace EddSt.Page.data
{

    public class Egg
    {
        [Key]
        private int id;
        private int howManyEggs;
        private DateOnly dataAdd;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public int HowManyEggs
        {
            get { return howManyEggs; }
            set { howManyEggs = value; }
        }
        public DateOnly DataAdd
        {
            get { return dataAdd; }
            set { dataAdd = value; }
        }
        public Egg() { }
        public Egg(int HMEggs)
        {
            
            this.howManyEggs = HMEggs;
            this.dataAdd = DateOnly.FromDateTime(DateTime.Now);

        }


    }
    public class HeghtLine 
    {
        
       int heght;
        int weght;
        string? dataAdd;
        public DateOnly Date { get; set; }
        public int Heght
        {
            get => heght;
            set => heght = value;
        }
        public int Weght
        {
            get => weght;
            set => weght = value;
        }
        public string? DataAdd { get => dataAdd; set => dataAdd = value; }
        

        public HeghtLine() {  }

        public HeghtLine(int heght,int weght, string dataAdd,DateOnly Date)
        {
            
            this.heght = heght;
            this.weght = weght;
            this.dataAdd = dataAdd;
            this.Date = Date;


        }

    }
    public class EggViewModel : INotifyPropertyChanged
    {

        int id;
        DateOnly? dataAdd;
        int howManyEggs = 1;
        int sredEgg;
        string eggsForAllTime;
        int allEgg;
        int heghtL;
        int weghtL;
        int weghtLN;
        DateOnly dataAddN;
        HeghtLine? selectedHeghtLines;
        int upDown { get; set; }
        public ObservableCollection<Egg> Eggs { get; private set; } = new();
        public ObservableCollection<HeghtLine> HeghtLines { get; private set; } =  new() ;

        ApplicationContext db = new();

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            //ик события, вызываем делегат  и передаем имя свойства
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        }
        public event PropertyChangedEventHandler? PropertyChanged;

        public ICommand? AddCommand { get; private set; }
        public ICommand? RemoveCommand { get; private set; }
        public ICommand? nowLineCommand { get; private set; }
        public  ICommand? AllSredEgg { get; private set; }



        public EggViewModel()
        {
             // db.Database.EnsureDeleted(); // гарантируем, что бд удалена
          
            db.Database.EnsureCreated();// гарантируем, что бд будет создана
            db.EggsAnd.Load();

            //Eggs = db.EggsAnd.Local.ToObservableCollection();
            var iG = db.EggsAnd.Local.ToObservableCollection();
            
            foreach (var item in iG.Reverse())
            {
                Eggs.Add( item);
            }
            EggsForAllTime = EggsAllTime(Eggs);
            (AllEgg, SredEgg) = Line(DateTime.DaysInMonth(DateOnly.FromDateTime(DateTime.Now).Year,
                             DateOnly.FromDateTime(DateTime.Now).Month),
                            new DateOnly(DateOnly.FromDateTime(DateTime.Now).Year, DateOnly.FromDateTime(DateTime.Now).Month, 1),
                            "dd", 7,  Eggs, HeghtLines);
            

            AddCommand = new Command(() =>// команда добовления
                {
                    var itemE = (new Egg(HowManyEggs));
                    Eggs.Insert(0, itemE);

                    db.EggsAnd.Add(itemE);
                    db.SaveChanges();
                   // Eggs = db.EggsAnd.Local.ToObservableCollection();
                    

                    EggsForAllTime = EggsAllTime(Eggs);
                    HowManyEggs = 1;
                });

            RemoveCommand = new Command((object? args) =>// устанавливаем команду удаления
            {
                if (args is Egg egg)
                {
                    db.EggsAnd.Remove(egg);
                    db.SaveChanges();
                    Eggs.Remove(egg);
                    EggsForAllTime = EggsAllTime(Eggs);
                }
            });
            
            nowLineCommand = new Command((object? args) =>// устанавливаем команду количества яиц в период
            {
                if (args is HeghtLine heghtLine)
                {
                     WeghtLN = heghtLine.Weght;
                     DataAddN = heghtLine.Date;
                }
            });

            AllSredEgg = new Command<string>((key) =>   //  команду подщета среднего его
            {
                OnPropertyChanged();
                DateOnly dNow = DateOnly.FromDateTime(DateTime.Now);

       

                switch (key)
                    {
                     case "1":// неделя
                        if (key == "1") upDown = 0;
                        DateOnly dateStartWeek = new DateOnly(dNow.Year, dNow.Month, dNow.Day-(int)dNow.DayOfWeek);
                        
                        (AllEgg, SredEgg) = Line(7, dateStartWeek.AddDays(upDown * 7), "ddd",18,
                                                     Eggs, HeghtLines);
                        SelectedHeghtLines = HeghtLines[0];
                        break;//WEEK
                    case "2":
                        //SRM(dNow.AddMonths(-1)); месяц
                        if (key == "2") upDown = 0;
                        DateOnly dateStart = new DateOnly(dNow.Year, dNow.Month, 1);
                        
                        (AllEgg, SredEgg) =  Line(DateTime.DaysInMonth(dNow.Year, dNow.Month), 
                            dateStart.AddMonths(upDown), "dd",7, 
                            Eggs, HeghtLines);
                        SelectedHeghtLines = HeghtLines[0];

                        break;// week
                    case "3":// год
                        if (key == "3") upDown = 0;
                        DateOnly dateStartY = new DateOnly(dNow.Year, 1, 1);
                        
                        (AllEgg, SredEgg) = Line(12, dateStartY.AddYears(upDown), "MMM", 10,
                                                     Eggs, HeghtLines);
                        SelectedHeghtLines = HeghtLines[0];
                        break;//year
                    case "4"://up
                        upDown++;
                        if (HeghtLines.Count==7) goto case "1"; 
                        
                        if (HeghtLines.Count == 12) goto case "3";
                        
                        if (HeghtLines.Count>12)  goto case "2";
                        
                        break;
                    case "5"://down 
                        upDown--;
                        if (HeghtLines.Count == 7) goto case "1";
                        
                        if (HeghtLines.Count == 12) goto case "3";
                        
                        if (HeghtLines.Count > 12) goto case "2";
                        
                        break;
                    default: break;
                     }
                
            });

            

        }
        static public string EggsAllTime(ObservableCollection<Egg> Eggs)
        {
            int allEggsTime = 0;
            foreach (var item in Eggs)
            {
                allEggsTime += item.HowManyEggs;
            }

            return $"за все время  : {allEggsTime} яиц" ;
        }

         static public (int,int) Line(int repitPointCount, DateOnly dateStart,string tipWeek,int HeghtL,
            ObservableCollection<Egg> Eggs,ObservableCollection<HeghtLine> HeghtLines)
        {
            int i = 1;
            int itemAll = 0;
            int AllEgg = 0;
            int SredEgg = 0;
            DateOnly dNow = DateOnly.FromDateTime(DateTime.Now);
            HeghtLines.Clear();

            //DateTime.DaysInMonth(dNow.Year, nowManf);
            List<HeghtLine> itemLin = new();
            for (int ij = 0; ij < repitPointCount; ij++)
            {
                itemLin.Add(new HeghtLine());
            }
            

            foreach (var item in Eggs)
            {
                
                if (dateStart <= item.DataAdd)
                {

                    
                    
                    for (int j = 0; j < repitPointCount; j++)
                    {

                        if (tipWeek == "MMM" ? dateStart.AddMonths(j) <= item.DataAdd&&
                                                 dateStart.AddMonths(j+1) >= item.DataAdd
                                                : dateStart.AddDays(j) == item.DataAdd)
                        {
                            itemLin[j].DataAdd = item.DataAdd.ToString(tipWeek);
                            itemLin[j].Date = item.DataAdd;
                             itemLin[j].Weght += item.HowManyEggs;
                            itemAll += item.HowManyEggs;
                            

                        }
                        else
                        {
                            itemLin[j].DataAdd = tipWeek == "MMM"
                                ? dateStart.AddMonths(j).ToString(tipWeek)
                                : dateStart.AddDays(j).ToString(tipWeek);
                            itemLin[j].Date = tipWeek == "MMM"
                                ? dateStart.AddMonths(j)
                                : dateStart.AddDays(j);
                        }

                        itemLin[j].Heght = HeghtL;// ширена столбика

                    }
                }
             else// ЕСЛИ НЕТ ДАЕЕЫХ
               {
                    itemLin[0].Heght = HeghtL;
                    itemLin[0].DataAdd = "НЕТ ДАННЫХ";
                    itemLin[0].Date =  dateStart;
                    itemLin[0].Weght = 0;
                    i = 1;
                    
               }
            }
            //itemLin.Reverse();
            foreach (var item in itemLin)
            {
                HeghtLines.Add(new(item.Heght, item.Weght, item.DataAdd, item.Date));
                if (item.Weght != 0) i++;
            }
            AllEgg = itemAll;
            SredEgg = itemAll / (i-1==0?1:i-1);
            
            return (AllEgg, SredEgg);

        }

        public DateOnly? DataAdd
        {
            get { return dataAdd; }
            set
            {
                if (dataAdd != value)
                {
                    dataAdd = value;
                    OnPropertyChanged();
                }
            }
        }
        public int HowManyEggs
        {
            get { return howManyEggs; }
            set
            {
                if (howManyEggs != value)
                {
                    howManyEggs = value;
                    OnPropertyChanged();
                }
            }
        }
        public int Id
        {
            get { return id; }
            set
            {
                if (id != value)
                { id = value; OnPropertyChanged(); }
            }
        }
        public  int SredEgg
        {
            get => sredEgg;
            set { if (sredEgg != value) { sredEgg = value; OnPropertyChanged(); } }
        }
        public  int AllEgg
        {
            get => allEgg;
            set {  if( allEgg != value) { allEgg = value; OnPropertyChanged();}  }
        }
         public int HeghtL { get => heghtL; 
            set  { if (heghtL != value) { heghtL = value; OnPropertyChanged(); } } }
        public int WeghtL { get => weghtL; 
            set { if (weghtL != value) { weghtL = value; OnPropertyChanged(); } } }

        public int WeghtLN
        {
            get => weghtLN;
            set { if (weghtLN != value) { weghtLN = value; OnPropertyChanged(); } }
        }
     

        public DateOnly DataAddN { get => dataAddN; 
                set { if (dataAddN != value) { dataAddN = value; OnPropertyChanged(); } }
          }

        public HeghtLine? SelectedHeghtLines
        {
            get
            {
                return selectedHeghtLines;
            }
            set
            {
                if (selectedHeghtLines != value) selectedHeghtLines = value;
                OnPropertyChanged();
            }
        }

        public string EggsForAllTime
        {
            get => eggsForAllTime;
            set { eggsForAllTime = value; OnPropertyChanged(); }
        }
    }
    public class ApplicationContext : DbContext
    {
        internal DbSet<Egg> EggsAnd { get; set; } = null!;
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "app.db");
            optionsBuilder.UseSqlite($"Filename={dbPath}");
        }

    }// подключение бд
}

//9280130189 эльвира музафировна