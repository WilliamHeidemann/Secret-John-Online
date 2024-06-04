namespace _2_Game
{
    public class Membership
    {
        public readonly Alignment Alignment;
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