using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using STROOP.Utilities;
using STROOP.Extensions;
using STROOP.Structs.Configurations;
using STROOP.Controls;

namespace STROOP.Structs
{
    public class ObjectAssociations
    {
        public static IEnumerable<System.Reflection.FieldInfo> GetImageFields()
        {
            foreach (var field in typeof(ObjectAssociations).GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance))
                if (field.FieldType == typeof(Lazy<Image>))
                    yield return field;
        }

        HashSet<ObjectBehaviorAssociation> _objAssoc = new HashSet<ObjectBehaviorAssociation>();
        List<SpawnHack> _spawnHacks = new List<SpawnHack>();

        Lazy<Image> _transparentDefaultImage;
        ObjectBehaviorAssociation nullAssociation;

        public Lazy<Image>
            DefaultImage,
            EmptyImage,
            MarioImage,
            HudImage,
            DebugImage,
            MiscImage,
            CameraImage,
            CameraMapImage,
            HolpImage,
            HomeImage,
            IntendedNextPositionImage,
            MarioMapImage,
            MarioMapNoHatImage,
            MarioMapHatOnlyImage,
            GreenMarioMapImage,
            OrangeMarioMapImage,
            PurpleMarioMapImage,
            BlueMarioMapImage,
            TurquoiseMarioMapImage,
            YellowMarioMapImage,
            PinkMarioMapImage,
            BrownMarioMapImage,
            WhiteMarioMapImage,
            GreyMarioMapImage,
            TriangleFloorImage,
            TriangleWallImage,
            TriangleCeilingImage,
            TriangleOtherImage,
            HitboxHackTrisImage,
            CellGridlinesImage,
            CurrentCellImage,
            UnitGridlinesImage,
            CurrentUnitImage,
            NextPositionsImage,
            ArrowImage,
            IwerlipsesImage,
            CylinderImage,
            SphereImage,
            PathImage,
            CustomPointsImage,
            CustomGridlinesImage;

        public Color
            MarioColor,
            HudColor,
            DebugColor,
            MiscColor,
            CameraColor;

        public uint MarioBehavior;
        public uint SegmentTable { get => RomVersionConfig.SwitchMap(SegmentTableUS, SegmentTableJP, SegmentTableSH, SegmentTableEU); }
        public uint SegmentTableUS = 0x8033B400;
        public uint SegmentTableJP = 0x8033A090;
        public uint SegmentTableSH = 0x8031DC58;
        public uint SegmentTableEU = 0x803096C8;
        public uint BehaviorBankStart;

        Dictionary<Image, Image> _cachedBufferedObjectImages = new Dictionary<Image, Image>();
        object _cachedBufferedObjectImageLocker = new object();

        public HashSet<ObjectBehaviorAssociation> BehaviorAssociations => _objAssoc;

        public List<SpawnHack> SpawnHacks => _spawnHacks;
        public ObjectAssociations()
        {
            _transparentDefaultImage = new Lazy<Image>(() => DefaultImage.Value.GetOpaqueImage(0.5f));
            nullAssociation = new ObjectBehaviorAssociation()
            {
                Criteria = new BehaviorCriteria(),
                Image = DefaultImage,
                Name = "null",
                TransparentImage = _transparentDefaultImage
            };
        }

        public bool AddAssociation(ObjectBehaviorAssociation objAsooc)
        {
            return _objAssoc.Add(objAsooc);
        }

        public bool AddEmptyAssociation()
        {
            return AddAssociation(
                new ObjectBehaviorAssociation()
                {
                    Name = "Uninitialized Object",
                    Criteria = new BehaviorCriteria()
                    {
                        BehaviorAddress = 0x0000,
                    },
                    RotatesOnMap = false,
                    Image = EmptyImage,
                    MapImage = EmptyImage,
                });
        }

        public void AddSpawnHack(SpawnHack hack)
        {
            _spawnHacks.Add(hack);
        }

        private Dictionary<BehaviorCriteria, ObjectBehaviorAssociation> _cachedObjAssoc = new Dictionary<BehaviorCriteria, ObjectBehaviorAssociation>();
        public ObjectBehaviorAssociation FindObjectAssociation(BehaviorCriteria behaviorCriteria)
        {
            if (_cachedObjAssoc.ContainsKey(behaviorCriteria))
            {
                return _cachedObjAssoc[behaviorCriteria];
            }

            var possibleAssoc = _objAssoc.Where(objAssoc => objAssoc.MeetsCriteria(behaviorCriteria));

            if (possibleAssoc.Count() > 1 && possibleAssoc.Any(objAssoc => objAssoc.Criteria.BehaviorOnly()))
                possibleAssoc = possibleAssoc.Where(objAssoc => !objAssoc.Criteria.BehaviorOnly());

            var behaviorAssoc = possibleAssoc.FirstOrDefault();
            if (behaviorAssoc == null)
                behaviorAssoc = nullAssociation;
            _cachedObjAssoc[behaviorCriteria] = behaviorAssoc;

            return behaviorAssoc;
        }

        public Lazy<Image> GetObjectImage(BehaviorCriteria behaviorCriteria, bool transparent = false)
        {
            if (behaviorCriteria.BehaviorAddress == 0)
                return EmptyImage;

            var assoc = FindObjectAssociation(behaviorCriteria);
            if (assoc == null)
                return transparent ? _transparentDefaultImage : DefaultImage;

            return transparent ? assoc.TransparentImage : assoc.Image;
        }

        public Lazy<Image> GetObjectImage(string objName)
        {
            ObjectBehaviorAssociation assoc = GetObjectAssociation(objName);
            if (assoc == null) return EmptyImage;
            return assoc.Image;
        }

        public ObjectBehaviorAssociation GetObjectAssociation(string objName)
        {
            return _objAssoc.FirstOrDefault(a => a.Name.ToLower() == objName.ToLower());
        }

        public Lazy<Image> GetObjectMapImage(BehaviorCriteria behaviorCriteria)
        {
            if (behaviorCriteria.BehaviorAddress == 0)
                return EmptyImage;

            var assoc = FindObjectAssociation(behaviorCriteria);
            if (assoc == null)
                return DefaultImage;
            if (assoc.MapImage == null)
                return DefaultImage;
            
            return assoc.MapImage;
        }

        public bool GetObjectMapRotates(BehaviorCriteria behaviorCriteria)
        {
            var assoc = FindObjectAssociation(behaviorCriteria);

            if (assoc == null)
                return false;

            return assoc.RotatesOnMap;
        }

        public string GetObjectName(BehaviorCriteria behaviorCriteria)
        {
            var assoc = FindObjectAssociation(behaviorCriteria);

            if (assoc == null)
                return "Unknown Object";

            return assoc.Name;
        }

        public Image GetCachedBufferedObjectImage(Image objectImage, Size size)
        {
            lock (_cachedBufferedObjectImageLocker)
            {
                if (!_cachedBufferedObjectImages.ContainsKey(objectImage))
                    return null;

                // Make sure cached size matches
                var _bufferedImage = _cachedBufferedObjectImages[objectImage];
                if (size != _bufferedImage.Size)
                    return null;

                return _bufferedImage;
            }
        }

        public void CreateCachedBufferedObjectImage(Image objectImage, Image bufferedObjectImage)
        {
            // Dispose of previous image
            lock (_cachedBufferedObjectImageLocker)
            {
                if (_cachedBufferedObjectImages.ContainsKey(objectImage))
                    _cachedBufferedObjectImages[objectImage]?.Dispose();

                _cachedBufferedObjectImages[objectImage] = bufferedObjectImage;
            }
        }

        public IEnumerable<WatchVariable> GetWatchVarControls(BehaviorCriteria behaviorCriteria)
        {
            var assoc = FindObjectAssociation(behaviorCriteria);

            if (assoc == null)
                return new WatchVariable[0];

            else return assoc.Precursors;
        }

        public uint AlignJPBehavior(uint segmented)
        {
            if (segmented >= 0x13002ea0)
                return segmented + 32;
            if (segmented >= 0x13002c6c)
                return segmented + 36;
            if (segmented >= 0x13002998)
                return segmented + 24;
            return segmented;
        }

        ~ObjectAssociations()
        {
            lock (_cachedBufferedObjectImageLocker)
            {
                foreach (var img in _cachedBufferedObjectImages)
                {
                    img.Value?.Dispose();
                }
            }

            // Unload and dispose of all images
            foreach (var obj in _objAssoc)
            {
                obj.Image?.Dispose();
                obj.TransparentImage?.Dispose();
                obj.MapImage?.Dispose();
            }

            _transparentDefaultImage?.Dispose();
            DefaultImage?.Dispose();
            foreach (var field in GetImageFields())
                ((Lazy<Image>)field.GetValue(this)).Dispose();
        }
    }
}
