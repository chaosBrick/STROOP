using STROOP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace STROOP.Forms
{
    public partial class InfoForm : Form
    {
        public InfoForm()
        {
            InitializeComponent();
            buttonOk.Click += (sender, e) => Close();
            textBoxTriangleInfo.DoubleClick += (sender, e) => textBoxTriangleInfo.SelectAll();
        }

        public void SetTriangleCoordinates(short[] coordinates)
        {
            Text = "Triangle Info";
            textBoxTitle.Text = "Triangle Coordinates";
            textBoxTriangleInfo.Text = StringifyCoordinates(coordinates);
        }

        public void SetTriangleEquation(float normalX, float normalY, float normalZ, float normalOffset)
        {
            Text = "Triangle Info";
            textBoxTitle.Text = "Triangle Equation";
            textBoxTriangleInfo.Text =
                $"{normalX}x + {normalY}y + {normalZ}z + {normalOffset} = 0";
        }

        public void SetTriangleData(List<short[]> coordinateList, bool repeatFirstVertex)
        {
            Text = "Triangle Info";
            textBoxTitle.Text = "Triangle Data";
            textBoxTriangleInfo.Text = String.Join(
                "\r\n\r\n",
                coordinateList.ConvertAll(
                    coordinates => StringifyCoordinates(coordinates, repeatFirstVertex)));
        }

        public void SetTriangleVertices(List<short[]> coordinateList)
        {
            this.Text = "Triangle Info";
            textBoxTitle.Text = "Triangle Vertices";
            var vertexList = new List<short[]>();

            coordinateList.ForEach(AddVertices);

            List<short[]> uniqueVertexList = new List<short[]>();

            vertexList.ForEach(
                UniqueVertex);

            uniqueVertexList.Sort(
                (short[] v1, short[] v2) =>
                {
                    var diff = v1[0] - v2[0];
                    if (diff != 0) return diff;
                    diff = v1[1] - v2[1];
                    if (diff != 0) return diff;
                    diff = v1[2] - v2[2];
                    return diff;
                });

            textBoxTriangleInfo.Text = string.Join(
                "\r\n",
                uniqueVertexList.ConvertAll(
                    Converter));
            return;

            void UniqueVertex(short[] vertex)
            {
                var hasAlready = uniqueVertexList.Any(v => v.SequenceEqual(vertex));
                if (!hasAlready) uniqueVertexList.Add(vertex);
            }

            string Converter(short[] coordinate) => StringifyCoordinate(coordinate);

            void AddVertices(short[] coordinates)
            {
                vertexList.Add(new[] { coordinates[0], coordinates[1], coordinates[2] });
                vertexList.Add(new[] { coordinates[3], coordinates[4], coordinates[5] });
                vertexList.Add(new[] { coordinates[6], coordinates[7], coordinates[8] });
            }
        }

        public void SetTriangles(List<TriangleDataModel> triangleList)
        {
            Text = "Triangle Info";
            textBoxTitle.Text = $"{triangleList.Count} Triangles";
            textBoxTriangleInfo.Text = $"{TriangleDataModel.GetFieldNameString()}\n{String.Join("\n", triangleList)}";
        }

        private String StringifyCoordinates(short[] coordinates, bool repeatCoordinates = false)
        {
            if (coordinates.Length != 9) throw new ArgumentOutOfRangeException();

            var text =
                $"{coordinates[0]}\t{coordinates[1]}\t{coordinates[2]}\r\n" +
                $"{coordinates[3]}\t{coordinates[4]}\t{coordinates[5]}\r\n" +
                $"{coordinates[6]}\t{coordinates[7]}\t{coordinates[8]}";

            if (repeatCoordinates)
            {
                text += "\r\n" + coordinates[0] + "\t" + coordinates[1] + "\t" + coordinates[2];
            }

            return text;
        }

        private static string StringifyCoordinate(short[] coordinate)
        {
            if (coordinate.Length != 3) throw new ArgumentOutOfRangeException();
            var text = coordinate[0] + "\t" + coordinate[1] + "\t" + coordinate[2];
            return text;
        }

        public void SetDictionary<TKey, TValue>(Dictionary<TKey, TValue> dictionary, string keyName = null, string valueName = null)
        {
            Text = "Dictionary Info";
            textBoxTitle.Text = "Dictionary";
            var text = "";
            if (keyName != null && valueName != null)
            {
                text += (keyName + "\t" + valueName + "\r\n");
            }

            text = dictionary.Aggregate(text, (current, entry) => string.Format("{0}{1}", current, (entry.Key + "\t" + entry.Value + "\r\n")));
            textBoxTriangleInfo.Text = text;
        }

        public void SetText(string formTitle, string textTitle, string text)
        {
            this.Text = formTitle;
            textBoxTitle.Text = textTitle;
            textBoxTriangleInfo.Text = text;
        }

        public static void ShowValue(object value, string formTitle = "Title", string textTitle = "Text")
        {
            var infoForm = new InfoForm();
            infoForm.SetText(formTitle, textTitle, value.ToString());
            infoForm.Show();
        }
    }
}
