using System.Collections.Generic;
using System.Linq;

namespace STROOP.Structs
{
    public class MusicTable
    {
        private Dictionary<int, MusicEntry> _musicDictionary;

        public MusicTable(List<MusicEntry> musicEntries)
        {
            _musicDictionary = new Dictionary<int, MusicEntry>();
            foreach (MusicEntry musicEntry in musicEntries)
            {
                _musicDictionary.Add(musicEntry.Index, musicEntry);
            }
        }

        public List<MusicEntry> GetMusicEntryList()
        {
            return _musicDictionary.Values.ToList();
        }
    }
}
