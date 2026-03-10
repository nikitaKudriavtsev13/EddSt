


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