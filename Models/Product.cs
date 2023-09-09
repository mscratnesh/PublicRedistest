namespace RedisTest.Models
{
    public class Member
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public int Age { get; set; }

        public Member() { }
        public Member(int id, string name, string role, int age)
        {
            Id = id;
            Name = name;
            Role = role;
            Age = age;
        }

        public List<Member> GetFamily()
        {
            List<Member> Family = new List<Member>();
            Family.Add(new Member(1, "Ratnesh", "Father", 41));
            Family.Add(new Member(2, "Swati", "Mother", 35));
            Family.Add(new Member(3, "Anika", "Daughter", 7));
            Family.Add(new Member(4, "Aniket", "Son", 1));
            return Family;
        }

        public bool AddFamilyMember(List<Member> Family,Member obj)
        {
            
             Family.Add(obj);
            return true;    
        }

        public bool DeleteFamilyMember(List<Member> Family, Member obj)
        {

            Member oldMem = Family.Where(a => a.Name == obj.Name).FirstOrDefault();
            if (oldMem != null)
            {
                Family.Remove(oldMem);
            }
            return true;
        }
        public bool UpdateFamilyMember(List<Member> Family, Member obj)
        {

            Member oldMem= Family.Where(a=>a.Name==obj.Name).FirstOrDefault();
            if (oldMem!=null)
            {
                oldMem.Name=obj.Name;
                oldMem.Role = obj.Role;
                oldMem.Age   = obj.Age;
            }
            return true;
        }
    }
}
