

using System.Collections.ObjectModel;
using System.ComponentModel;// INotifyPropertyChanged
using System.Runtime.CompilerServices; 
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;



namespace EddSt.Page.data
{
   
    internal class Egg 
    {
        private static int idink=0;
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
            this.id = ++idink;
            this.howManyEggs = HMEggs;
            this.dataAdd = DateOnly.FromDateTime(DateTime.Now);

        }


    }

    internal class EggViewModel : INotifyPropertyChanged  
    {
        int id;
        DateOnly dataAdd;
        int howManyEggs =1;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            //ик события, вызываем делегат  и передаем имя свойства
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        }
        public event PropertyChangedEventHandler? PropertyChanged;
        public ICommand AddCommand { get; set; }
        public ICommand RemoveCommand { get; set; }
        public  ObservableCollection<Egg> EggsViewModel { get;  } =new ();

        public EggViewModel()
        {

            // команда добовления
            AddCommand = new Command(() =>
            {
                EggsViewModel.Insert(0,new Egg(HowManyEggs));

                HowManyEggs = 1;

            });
            // устанавливаем команду удаления
            RemoveCommand = new Command((object? args) =>
            {
                if (args is Egg egg) EggsViewModel.Remove(egg);
            });
         }
        public int EggAll()
        {
            int eggAll = 0;
            foreach (var egg in EggsViewModel)
            {
             eggAll += egg.HowManyEggs;
            }
            return eggAll;

        }
        public int EggSred()
        {
            int eggSred = 0;
            foreach (var egg in EggsViewModel)
            {
                eggSred += egg.HowManyEggs;
            }
            return eggSred/ EggsViewModel.Count;

        }


        public DateOnly DataAdd
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
            set { if(howManyEggs != value)
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
                { id = value; OnPropertyChanged();} 
               }
            
        }

    
    }

    public class ApplicationContext : DbContext
    {
        internal DbSet<Egg> EggsAnd { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=EggsAnddb.db");
        }
    }
}
