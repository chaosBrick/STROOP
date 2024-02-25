namespace STROOP.Structs
{
    public class MusicEntry
    {
        public readonly int Index;
        public readonly string Name;

        public MusicEntry(int index, string name)
        {
            Index = index;
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
