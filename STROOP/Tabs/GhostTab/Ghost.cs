using System;
using System.Collections.Generic;
using System.IO;

namespace STROOP.Tabs.GhostTab
{
    class Ghost
    {
        public uint playbackBaseFrame = 0;
        public Dictionary<uint, GhostFrame> playbackFrames = new Dictionary<uint, GhostFrame>();
        public GhostFrame lastValidPlaybackFrame;
        public uint originalPlaybackBaseFrame { get; private set; }
        public uint maxFrame { get; private set; }
        public uint numFrames => (maxFrame - originalPlaybackBaseFrame) + 1;
        public string name, fileName = "-";
        public Ghost() { }
        public Ghost(uint playbackBaseFrame, Dictionary<uint, GhostFrame> playbackFrames)
        {
            this.playbackBaseFrame = originalPlaybackBaseFrame = playbackBaseFrame;
            this.playbackFrames = playbackFrames;
            
        }
        public static Ghost FromFile(BinaryReader reader)
        {
            Ghost result = new Ghost();
            try
            {
                result.playbackBaseFrame = result.originalPlaybackBaseFrame = reader.ReadUInt32();
                int numFrames = reader.ReadInt32();
                for (int i = 0; i < numFrames; i++)
                {
                    var index = reader.ReadUInt32();
                    var frame = GhostFrame.ReadFrom(reader);
                    result.playbackFrames[index] = frame;
                    result.maxFrame = Math.Max(result.maxFrame, index);
                }
            }
            catch (IOException)
            {
                return null;
            }
            return result;
        }

        public override string ToString()
        {
            return name ?? "<unnamed ghost>";
        }
    }
}
