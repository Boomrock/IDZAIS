namespace WebApiPersonSkills.Models.ViewModel
{
    public class SkillVM
    {
        public string Name { get; set; } = null!;
        public byte Level { get; set; }
        public SkillVM() { }
        public SkillVM(Skill skill)
        {
            Name = skill.Name;
            Level = skill.Level;
        }

        public Skill GetSkill()
        {
            var skill = new Skill();
            skill.Name = Name;
            skill.Level = Level;

            return skill;
        }
    }
}
