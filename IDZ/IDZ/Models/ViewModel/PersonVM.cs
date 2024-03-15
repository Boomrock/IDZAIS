namespace WebApiPersonSkills.Models.ViewModel
{
    public class PersonVM
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string DisplayName { get; set; } = null!;

        public List<SkillVM> SkillVMs { get; set; }

        public PersonVM() { }

        public PersonVM(Person person)
        {
            Id  = person.Id; 
            Name = person.Name;
            DisplayName = person.DisplayName;
            SkillVMs = new List<SkillVM>();
            foreach (var skill in person.Skills)
            {
                SkillVMs.Add(new SkillVM(skill));
            }
        }

        internal Person GetPerson()
        {
            var person = new Person();
            person.Id = Id;
            person.Name = Name;
            person.DisplayName = DisplayName;

            foreach (var skillVM in SkillVMs)
            {
                person.Skills?.Add(skillVM.GetSkill());
            }

            return person;
        }
    }
}
