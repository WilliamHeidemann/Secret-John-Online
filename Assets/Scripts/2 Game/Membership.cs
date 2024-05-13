namespace _2_Game
{
    public struct Membership
    {
        public Alignment Alignment;
        public Role Role;

        public Membership(Alignment alignment, Role role)
        {
            Alignment = alignment;
            Role = role;
        }
    }
    public enum Alignment
    {
        Liberal,
        Fascist 
    }

    public enum Role
    {
        Hitler,
        Member
    }
}