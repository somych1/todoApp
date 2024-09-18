namespace todoMVCApp.Models
{
    public class ToDo
    {
        public int ID { get; set; }
        public string? Task { get; set; }
        public Status status { get; set; }

        public ToDo(){}

        public ToDo(string task)
        {
            this.Task = task;
            this.status = Status.Pending;
        }
    }
}

//dotnet aspnet-codegenerator controller -name TodoController -m ToDo -dc todoMVCApp.Data.TodoContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries --databaseProvider sqlite