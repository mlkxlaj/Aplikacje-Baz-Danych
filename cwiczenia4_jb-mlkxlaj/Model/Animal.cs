namespace Zadanie4.Model
{
    public class Animal
    {
        public int idAnimal { get; set; }
        public string name { get; set;}
        public string description { get; set;}
        public string category { get; set;}
        public string area { get; set;}

        public Animal(int idAnimal, string name, string description, string category, string area)
        {
            this.idAnimal = idAnimal;
            this.name = name;
            this.description = description;
            this.category = category;
            this.area = area;
        }

        public override string ToString()
        {
            return $"{idAnimal}, '{name}', '{description}', '{category}', '{area}'";
        }
    }
}
