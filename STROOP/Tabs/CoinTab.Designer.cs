using STROOP.Controls;

namespace STROOP.Tabs
{
    partial class CoinTab
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitContainerCoin = new BetterSplitContainer();
            this.buttonCoinCalculate = new System.Windows.Forms.Button();
            this.buttonCoinClear = new System.Windows.Forms.Button();
            this.groupBoxCoinFilter = new System.Windows.Forms.GroupBox();
            this.textBoxCoinFilterRequiredNumOfQualifiedCoins = new BetterTextbox();
            this.textBoxCoinFilterAngleMin = new BetterTextbox();
            this.textBoxCoinFilterAngleMax = new BetterTextbox();
            this.labelCoinFilterHSpeedFrom = new System.Windows.Forms.Label();
            this.textBoxCoinFilterVSpeedMax = new BetterTextbox();
            this.labelCoinFilterMin = new System.Windows.Forms.Label();
            this.labelCoinFilterMax = new System.Windows.Forms.Label();
            this.textBoxCoinFilterVSpeedMin = new BetterTextbox();
            this.labelCoinFilterHSpeedTo = new System.Windows.Forms.Label();
            this.textBoxCoinFilterHSpeedMax = new BetterTextbox();
            this.labelCoinFilterVSpeedFrom = new System.Windows.Forms.Label();
            this.labelCoinFilterVSpeedTo = new System.Windows.Forms.Label();
            this.textBoxCoinFilterHSpeedMin = new BetterTextbox();
            this.labelCoinFilterAngleFrom = new System.Windows.Forms.Label();
            this.labelCoinFilterAngleTo = new System.Windows.Forms.Label();
            this.labelCoinFilterRequiredNumOfQualifiedCoins = new System.Windows.Forms.Label();
            this.textBoxCoinNumCoins = new BetterTextbox();
            this.textBoxCoinParamOrder = new BetterTextbox();
            this.textBoxCoinVSpeedOffset = new BetterTextbox();
            this.textBoxCoinVSpeedScale = new BetterTextbox();
            this.textBoxCoinHSpeedScale = new BetterTextbox();
            this.labelCoinNumCoins = new System.Windows.Forms.Label();
            this.labelCoinVSpeedRange = new System.Windows.Forms.Label();
            this.labelCoinParamOrder = new System.Windows.Forms.Label();
            this.labelCoinTableEntries = new System.Windows.Forms.Label();
            this.labelCoinHSpeedRange = new System.Windows.Forms.Label();
            this.labelCoinVSpeedOffset = new System.Windows.Forms.Label();
            this.labelCoinVSpeedScale = new System.Windows.Forms.Label();
            this.labelCoinHSpeedScale = new System.Windows.Forms.Label();
            this.listBoxCoinObjects = new System.Windows.Forms.ListBox();
            this.groupBoxCoinCustomization = new System.Windows.Forms.GroupBox();
            this.checkBoxCoinCustomizatonDisplayNonQualifiedCoinsOfAQualifiedCoinGroup = new System.Windows.Forms.CheckBox();
            this.textBoxCoinCustomizatonStartingRngIndex = new BetterTextbox();
            this.labelCoinCustomizatonNumDecimalDigits = new System.Windows.Forms.Label();
            this.labelCoinCustomizatonStartingRngIndex = new System.Windows.Forms.Label();
            this.textBoxCoinCustomizatonNumDecimalDigits = new BetterTextbox();
            this.dataGridViewCoin = new System.Windows.Forms.DataGridView();
            this.rngIndex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rngValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rngToGo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.coinHSpeed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.coinVSpeed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.coinAngle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerCoin)).BeginInit();
            this.splitContainerCoin.Panel1.SuspendLayout();
            this.splitContainerCoin.Panel2.SuspendLayout();
            this.splitContainerCoin.SuspendLayout();
            this.groupBoxCoinFilter.SuspendLayout();
            this.groupBoxCoinCustomization.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCoin)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerCoin
            // 
            this.splitContainerCoin.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerCoin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainerCoin.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerCoin.Location = new System.Drawing.Point(0, 0);
            this.splitContainerCoin.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainerCoin.Name = "splitContainerCoin";
            // 
            // splitContainerCoin.Panel1
            // 
            this.splitContainerCoin.Panel1.AutoScroll = true;
            this.splitContainerCoin.Panel1.Controls.Add(this.buttonCoinCalculate);
            this.splitContainerCoin.Panel1.Controls.Add(this.buttonCoinClear);
            this.splitContainerCoin.Panel1.Controls.Add(this.groupBoxCoinFilter);
            this.splitContainerCoin.Panel1.Controls.Add(this.textBoxCoinNumCoins);
            this.splitContainerCoin.Panel1.Controls.Add(this.textBoxCoinParamOrder);
            this.splitContainerCoin.Panel1.Controls.Add(this.textBoxCoinVSpeedOffset);
            this.splitContainerCoin.Panel1.Controls.Add(this.textBoxCoinVSpeedScale);
            this.splitContainerCoin.Panel1.Controls.Add(this.textBoxCoinHSpeedScale);
            this.splitContainerCoin.Panel1.Controls.Add(this.labelCoinNumCoins);
            this.splitContainerCoin.Panel1.Controls.Add(this.labelCoinVSpeedRange);
            this.splitContainerCoin.Panel1.Controls.Add(this.labelCoinParamOrder);
            this.splitContainerCoin.Panel1.Controls.Add(this.labelCoinTableEntries);
            this.splitContainerCoin.Panel1.Controls.Add(this.labelCoinHSpeedRange);
            this.splitContainerCoin.Panel1.Controls.Add(this.labelCoinVSpeedOffset);
            this.splitContainerCoin.Panel1.Controls.Add(this.labelCoinVSpeedScale);
            this.splitContainerCoin.Panel1.Controls.Add(this.labelCoinHSpeedScale);
            this.splitContainerCoin.Panel1.Controls.Add(this.listBoxCoinObjects);
            this.splitContainerCoin.Panel1.Controls.Add(this.groupBoxCoinCustomization);
            this.splitContainerCoin.Panel1MinSize = 0;
            // 
            // splitContainerCoin.Panel2
            // 
            this.splitContainerCoin.Panel2.Controls.Add(this.dataGridViewCoin);
            this.splitContainerCoin.Panel2MinSize = 0;
            this.splitContainerCoin.Size = new System.Drawing.Size(915, 463);
            this.splitContainerCoin.SplitterDistance = 290;
            this.splitContainerCoin.SplitterWidth = 1;
            this.splitContainerCoin.TabIndex = 39;
            // 
            // buttonCoinCalculate
            // 
            this.buttonCoinCalculate.Location = new System.Drawing.Point(133, 391);
            this.buttonCoinCalculate.Name = "buttonCoinCalculate";
            this.buttonCoinCalculate.Size = new System.Drawing.Size(93, 23);
            this.buttonCoinCalculate.TabIndex = 42;
            this.buttonCoinCalculate.Text = "Calculate";
            this.buttonCoinCalculate.UseVisualStyleBackColor = true;
            // 
            // buttonCoinClear
            // 
            this.buttonCoinClear.Location = new System.Drawing.Point(35, 391);
            this.buttonCoinClear.Name = "buttonCoinClear";
            this.buttonCoinClear.Size = new System.Drawing.Size(93, 23);
            this.buttonCoinClear.TabIndex = 42;
            this.buttonCoinClear.Text = "Clear";
            this.buttonCoinClear.UseVisualStyleBackColor = true;
            // 
            // groupBoxCoinFilter
            // 
            this.groupBoxCoinFilter.Controls.Add(this.textBoxCoinFilterRequiredNumOfQualifiedCoins);
            this.groupBoxCoinFilter.Controls.Add(this.textBoxCoinFilterAngleMin);
            this.groupBoxCoinFilter.Controls.Add(this.textBoxCoinFilterAngleMax);
            this.groupBoxCoinFilter.Controls.Add(this.labelCoinFilterHSpeedFrom);
            this.groupBoxCoinFilter.Controls.Add(this.textBoxCoinFilterVSpeedMax);
            this.groupBoxCoinFilter.Controls.Add(this.labelCoinFilterMin);
            this.groupBoxCoinFilter.Controls.Add(this.labelCoinFilterMax);
            this.groupBoxCoinFilter.Controls.Add(this.textBoxCoinFilterVSpeedMin);
            this.groupBoxCoinFilter.Controls.Add(this.labelCoinFilterHSpeedTo);
            this.groupBoxCoinFilter.Controls.Add(this.textBoxCoinFilterHSpeedMax);
            this.groupBoxCoinFilter.Controls.Add(this.labelCoinFilterVSpeedFrom);
            this.groupBoxCoinFilter.Controls.Add(this.labelCoinFilterVSpeedTo);
            this.groupBoxCoinFilter.Controls.Add(this.textBoxCoinFilterHSpeedMin);
            this.groupBoxCoinFilter.Controls.Add(this.labelCoinFilterAngleFrom);
            this.groupBoxCoinFilter.Controls.Add(this.labelCoinFilterAngleTo);
            this.groupBoxCoinFilter.Controls.Add(this.labelCoinFilterRequiredNumOfQualifiedCoins);
            this.groupBoxCoinFilter.Location = new System.Drawing.Point(3, 148);
            this.groupBoxCoinFilter.Name = "groupBoxCoinFilter";
            this.groupBoxCoinFilter.Size = new System.Drawing.Size(264, 128);
            this.groupBoxCoinFilter.TabIndex = 41;
            this.groupBoxCoinFilter.TabStop = false;
            this.groupBoxCoinFilter.Text = "Filter";
            // 
            // textBoxCoinFilterRequiredNumOfQualifiedCoins
            // 
            this.textBoxCoinFilterRequiredNumOfQualifiedCoins.Location = new System.Drawing.Point(175, 100);
            this.textBoxCoinFilterRequiredNumOfQualifiedCoins.Name = "textBoxCoinFilterRequiredNumOfQualifiedCoins";
            this.textBoxCoinFilterRequiredNumOfQualifiedCoins.Size = new System.Drawing.Size(53, 20);
            this.textBoxCoinFilterRequiredNumOfQualifiedCoins.TabIndex = 40;
            this.textBoxCoinFilterRequiredNumOfQualifiedCoins.Text = "1";
            this.textBoxCoinFilterRequiredNumOfQualifiedCoins.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxCoinFilterAngleMin
            // 
            this.textBoxCoinFilterAngleMin.Location = new System.Drawing.Point(101, 76);
            this.textBoxCoinFilterAngleMin.Name = "textBoxCoinFilterAngleMin";
            this.textBoxCoinFilterAngleMin.Size = new System.Drawing.Size(53, 20);
            this.textBoxCoinFilterAngleMin.TabIndex = 38;
            this.textBoxCoinFilterAngleMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxCoinFilterAngleMax
            // 
            this.textBoxCoinFilterAngleMax.Location = new System.Drawing.Point(175, 76);
            this.textBoxCoinFilterAngleMax.Name = "textBoxCoinFilterAngleMax";
            this.textBoxCoinFilterAngleMax.Size = new System.Drawing.Size(53, 20);
            this.textBoxCoinFilterAngleMax.TabIndex = 38;
            this.textBoxCoinFilterAngleMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelCoinFilterHSpeedFrom
            // 
            this.labelCoinFilterHSpeedFrom.AutoSize = true;
            this.labelCoinFilterHSpeedFrom.Location = new System.Drawing.Point(31, 31);
            this.labelCoinFilterHSpeedFrom.Name = "labelCoinFilterHSpeedFrom";
            this.labelCoinFilterHSpeedFrom.Size = new System.Drawing.Size(69, 13);
            this.labelCoinFilterHSpeedFrom.TabIndex = 37;
            this.labelCoinFilterHSpeedFrom.Text = "HSpeed from";
            // 
            // textBoxCoinFilterVSpeedMax
            // 
            this.textBoxCoinFilterVSpeedMax.Location = new System.Drawing.Point(175, 52);
            this.textBoxCoinFilterVSpeedMax.Name = "textBoxCoinFilterVSpeedMax";
            this.textBoxCoinFilterVSpeedMax.Size = new System.Drawing.Size(53, 20);
            this.textBoxCoinFilterVSpeedMax.TabIndex = 39;
            this.textBoxCoinFilterVSpeedMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelCoinFilterMin
            // 
            this.labelCoinFilterMin.AutoSize = true;
            this.labelCoinFilterMin.Location = new System.Drawing.Point(116, 12);
            this.labelCoinFilterMin.Name = "labelCoinFilterMin";
            this.labelCoinFilterMin.Size = new System.Drawing.Size(24, 13);
            this.labelCoinFilterMin.TabIndex = 37;
            this.labelCoinFilterMin.Text = "Min";
            // 
            // labelCoinFilterMax
            // 
            this.labelCoinFilterMax.AutoSize = true;
            this.labelCoinFilterMax.Location = new System.Drawing.Point(188, 12);
            this.labelCoinFilterMax.Name = "labelCoinFilterMax";
            this.labelCoinFilterMax.Size = new System.Drawing.Size(27, 13);
            this.labelCoinFilterMax.TabIndex = 37;
            this.labelCoinFilterMax.Text = "Max";
            // 
            // textBoxCoinFilterVSpeedMin
            // 
            this.textBoxCoinFilterVSpeedMin.Location = new System.Drawing.Point(101, 52);
            this.textBoxCoinFilterVSpeedMin.Name = "textBoxCoinFilterVSpeedMin";
            this.textBoxCoinFilterVSpeedMin.Size = new System.Drawing.Size(53, 20);
            this.textBoxCoinFilterVSpeedMin.TabIndex = 39;
            this.textBoxCoinFilterVSpeedMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelCoinFilterHSpeedTo
            // 
            this.labelCoinFilterHSpeedTo.AutoSize = true;
            this.labelCoinFilterHSpeedTo.Location = new System.Drawing.Point(158, 31);
            this.labelCoinFilterHSpeedTo.Name = "labelCoinFilterHSpeedTo";
            this.labelCoinFilterHSpeedTo.Size = new System.Drawing.Size(16, 13);
            this.labelCoinFilterHSpeedTo.TabIndex = 37;
            this.labelCoinFilterHSpeedTo.Text = "to";
            // 
            // textBoxCoinFilterHSpeedMax
            // 
            this.textBoxCoinFilterHSpeedMax.Location = new System.Drawing.Point(175, 28);
            this.textBoxCoinFilterHSpeedMax.Name = "textBoxCoinFilterHSpeedMax";
            this.textBoxCoinFilterHSpeedMax.Size = new System.Drawing.Size(53, 20);
            this.textBoxCoinFilterHSpeedMax.TabIndex = 40;
            this.textBoxCoinFilterHSpeedMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelCoinFilterVSpeedFrom
            // 
            this.labelCoinFilterVSpeedFrom.AutoSize = true;
            this.labelCoinFilterVSpeedFrom.Location = new System.Drawing.Point(31, 55);
            this.labelCoinFilterVSpeedFrom.Name = "labelCoinFilterVSpeedFrom";
            this.labelCoinFilterVSpeedFrom.Size = new System.Drawing.Size(68, 13);
            this.labelCoinFilterVSpeedFrom.TabIndex = 36;
            this.labelCoinFilterVSpeedFrom.Text = "VSpeed from";
            // 
            // labelCoinFilterVSpeedTo
            // 
            this.labelCoinFilterVSpeedTo.AutoSize = true;
            this.labelCoinFilterVSpeedTo.Location = new System.Drawing.Point(158, 55);
            this.labelCoinFilterVSpeedTo.Name = "labelCoinFilterVSpeedTo";
            this.labelCoinFilterVSpeedTo.Size = new System.Drawing.Size(16, 13);
            this.labelCoinFilterVSpeedTo.TabIndex = 36;
            this.labelCoinFilterVSpeedTo.Text = "to";
            // 
            // textBoxCoinFilterHSpeedMin
            // 
            this.textBoxCoinFilterHSpeedMin.Location = new System.Drawing.Point(101, 28);
            this.textBoxCoinFilterHSpeedMin.Name = "textBoxCoinFilterHSpeedMin";
            this.textBoxCoinFilterHSpeedMin.Size = new System.Drawing.Size(53, 20);
            this.textBoxCoinFilterHSpeedMin.TabIndex = 40;
            this.textBoxCoinFilterHSpeedMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelCoinFilterAngleFrom
            // 
            this.labelCoinFilterAngleFrom.AutoSize = true;
            this.labelCoinFilterAngleFrom.Location = new System.Drawing.Point(31, 79);
            this.labelCoinFilterAngleFrom.Name = "labelCoinFilterAngleFrom";
            this.labelCoinFilterAngleFrom.Size = new System.Drawing.Size(57, 13);
            this.labelCoinFilterAngleFrom.TabIndex = 35;
            this.labelCoinFilterAngleFrom.Text = "Angle from";
            // 
            // labelCoinFilterAngleTo
            // 
            this.labelCoinFilterAngleTo.AutoSize = true;
            this.labelCoinFilterAngleTo.Location = new System.Drawing.Point(158, 79);
            this.labelCoinFilterAngleTo.Name = "labelCoinFilterAngleTo";
            this.labelCoinFilterAngleTo.Size = new System.Drawing.Size(16, 13);
            this.labelCoinFilterAngleTo.TabIndex = 35;
            this.labelCoinFilterAngleTo.Text = "to";
            // 
            // labelCoinFilterRequiredNumOfQualifiedCoins
            // 
            this.labelCoinFilterRequiredNumOfQualifiedCoins.AutoSize = true;
            this.labelCoinFilterRequiredNumOfQualifiedCoins.Location = new System.Drawing.Point(28, 103);
            this.labelCoinFilterRequiredNumOfQualifiedCoins.Name = "labelCoinFilterRequiredNumOfQualifiedCoins";
            this.labelCoinFilterRequiredNumOfQualifiedCoins.Size = new System.Drawing.Size(148, 13);
            this.labelCoinFilterRequiredNumOfQualifiedCoins.TabIndex = 37;
            this.labelCoinFilterRequiredNumOfQualifiedCoins.Text = "Req\'d Num of Qualified Coins:";
            // 
            // textBoxCoinNumCoins
            // 
            this.textBoxCoinNumCoins.Location = new System.Drawing.Point(214, 102);
            this.textBoxCoinNumCoins.Name = "textBoxCoinNumCoins";
            this.textBoxCoinNumCoins.Size = new System.Drawing.Size(53, 20);
            this.textBoxCoinNumCoins.TabIndex = 38;
            this.textBoxCoinNumCoins.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxCoinParamOrder
            // 
            this.textBoxCoinParamOrder.Location = new System.Drawing.Point(214, 78);
            this.textBoxCoinParamOrder.Name = "textBoxCoinParamOrder";
            this.textBoxCoinParamOrder.Size = new System.Drawing.Size(53, 20);
            this.textBoxCoinParamOrder.TabIndex = 38;
            this.textBoxCoinParamOrder.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxCoinVSpeedOffset
            // 
            this.textBoxCoinVSpeedOffset.Location = new System.Drawing.Point(214, 54);
            this.textBoxCoinVSpeedOffset.Name = "textBoxCoinVSpeedOffset";
            this.textBoxCoinVSpeedOffset.Size = new System.Drawing.Size(53, 20);
            this.textBoxCoinVSpeedOffset.TabIndex = 38;
            this.textBoxCoinVSpeedOffset.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxCoinVSpeedScale
            // 
            this.textBoxCoinVSpeedScale.Location = new System.Drawing.Point(214, 30);
            this.textBoxCoinVSpeedScale.Name = "textBoxCoinVSpeedScale";
            this.textBoxCoinVSpeedScale.Size = new System.Drawing.Size(53, 20);
            this.textBoxCoinVSpeedScale.TabIndex = 39;
            this.textBoxCoinVSpeedScale.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxCoinHSpeedScale
            // 
            this.textBoxCoinHSpeedScale.Location = new System.Drawing.Point(214, 6);
            this.textBoxCoinHSpeedScale.Name = "textBoxCoinHSpeedScale";
            this.textBoxCoinHSpeedScale.Size = new System.Drawing.Size(53, 20);
            this.textBoxCoinHSpeedScale.TabIndex = 40;
            this.textBoxCoinHSpeedScale.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelCoinNumCoins
            // 
            this.labelCoinNumCoins.AutoSize = true;
            this.labelCoinNumCoins.Location = new System.Drawing.Point(133, 105);
            this.labelCoinNumCoins.Name = "labelCoinNumCoins";
            this.labelCoinNumCoins.Size = new System.Drawing.Size(61, 13);
            this.labelCoinNumCoins.TabIndex = 35;
            this.labelCoinNumCoins.Text = "Num Coins:";
            // 
            // labelCoinVSpeedRange
            // 
            this.labelCoinVSpeedRange.AutoSize = true;
            this.labelCoinVSpeedRange.Location = new System.Drawing.Point(137, 130);
            this.labelCoinVSpeedRange.Name = "labelCoinVSpeedRange";
            this.labelCoinVSpeedRange.Size = new System.Drawing.Size(83, 13);
            this.labelCoinVSpeedRange.TabIndex = 35;
            this.labelCoinVSpeedRange.Text = "VSpeed Range:";
            // 
            // labelCoinParamOrder
            // 
            this.labelCoinParamOrder.AutoSize = true;
            this.labelCoinParamOrder.Location = new System.Drawing.Point(133, 81);
            this.labelCoinParamOrder.Name = "labelCoinParamOrder";
            this.labelCoinParamOrder.Size = new System.Drawing.Size(69, 13);
            this.labelCoinParamOrder.TabIndex = 35;
            this.labelCoinParamOrder.Text = "Param Order:";
            // 
            // labelCoinTableEntries
            // 
            this.labelCoinTableEntries.AutoSize = true;
            this.labelCoinTableEntries.Location = new System.Drawing.Point(68, 419);
            this.labelCoinTableEntries.Name = "labelCoinTableEntries";
            this.labelCoinTableEntries.Size = new System.Drawing.Size(72, 13);
            this.labelCoinTableEntries.TabIndex = 35;
            this.labelCoinTableEntries.Text = "Table Entries:";
            // 
            // labelCoinHSpeedRange
            // 
            this.labelCoinHSpeedRange.AutoSize = true;
            this.labelCoinHSpeedRange.Location = new System.Drawing.Point(5, 130);
            this.labelCoinHSpeedRange.Name = "labelCoinHSpeedRange";
            this.labelCoinHSpeedRange.Size = new System.Drawing.Size(84, 13);
            this.labelCoinHSpeedRange.TabIndex = 35;
            this.labelCoinHSpeedRange.Text = "HSpeed Range:";
            // 
            // labelCoinVSpeedOffset
            // 
            this.labelCoinVSpeedOffset.AutoSize = true;
            this.labelCoinVSpeedOffset.Location = new System.Drawing.Point(133, 57);
            this.labelCoinVSpeedOffset.Name = "labelCoinVSpeedOffset";
            this.labelCoinVSpeedOffset.Size = new System.Drawing.Size(79, 13);
            this.labelCoinVSpeedOffset.TabIndex = 35;
            this.labelCoinVSpeedOffset.Text = "VSpeed Offset:";
            // 
            // labelCoinVSpeedScale
            // 
            this.labelCoinVSpeedScale.AutoSize = true;
            this.labelCoinVSpeedScale.Location = new System.Drawing.Point(133, 33);
            this.labelCoinVSpeedScale.Name = "labelCoinVSpeedScale";
            this.labelCoinVSpeedScale.Size = new System.Drawing.Size(78, 13);
            this.labelCoinVSpeedScale.TabIndex = 36;
            this.labelCoinVSpeedScale.Text = "VSpeed Scale:";
            // 
            // labelCoinHSpeedScale
            // 
            this.labelCoinHSpeedScale.AutoSize = true;
            this.labelCoinHSpeedScale.Location = new System.Drawing.Point(133, 9);
            this.labelCoinHSpeedScale.Name = "labelCoinHSpeedScale";
            this.labelCoinHSpeedScale.Size = new System.Drawing.Size(79, 13);
            this.labelCoinHSpeedScale.TabIndex = 37;
            this.labelCoinHSpeedScale.Text = "HSpeed Scale:";
            // 
            // listBoxCoinObjects
            // 
            this.listBoxCoinObjects.FormattingEnabled = true;
            this.listBoxCoinObjects.Location = new System.Drawing.Point(3, 3);
            this.listBoxCoinObjects.Name = "listBoxCoinObjects";
            this.listBoxCoinObjects.Size = new System.Drawing.Size(125, 121);
            this.listBoxCoinObjects.TabIndex = 17;
            // 
            // groupBoxCoinCustomization
            // 
            this.groupBoxCoinCustomization.Controls.Add(this.checkBoxCoinCustomizatonDisplayNonQualifiedCoinsOfAQualifiedCoinGroup);
            this.groupBoxCoinCustomization.Controls.Add(this.textBoxCoinCustomizatonStartingRngIndex);
            this.groupBoxCoinCustomization.Controls.Add(this.labelCoinCustomizatonNumDecimalDigits);
            this.groupBoxCoinCustomization.Controls.Add(this.labelCoinCustomizatonStartingRngIndex);
            this.groupBoxCoinCustomization.Controls.Add(this.textBoxCoinCustomizatonNumDecimalDigits);
            this.groupBoxCoinCustomization.Location = new System.Drawing.Point(3, 279);
            this.groupBoxCoinCustomization.Name = "groupBoxCoinCustomization";
            this.groupBoxCoinCustomization.Size = new System.Drawing.Size(264, 103);
            this.groupBoxCoinCustomization.TabIndex = 41;
            this.groupBoxCoinCustomization.TabStop = false;
            this.groupBoxCoinCustomization.Text = "Customizaton";
            // 
            // checkBoxCoinCustomizatonDisplayNonQualifiedCoinsOfAQualifiedCoinGroup
            // 
            this.checkBoxCoinCustomizatonDisplayNonQualifiedCoinsOfAQualifiedCoinGroup.AutoSize = true;
            this.checkBoxCoinCustomizatonDisplayNonQualifiedCoinsOfAQualifiedCoinGroup.Checked = true;
            this.checkBoxCoinCustomizatonDisplayNonQualifiedCoinsOfAQualifiedCoinGroup.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxCoinCustomizatonDisplayNonQualifiedCoinsOfAQualifiedCoinGroup.Location = new System.Drawing.Point(52, 17);
            this.checkBoxCoinCustomizatonDisplayNonQualifiedCoinsOfAQualifiedCoinGroup.Name = "checkBoxCoinCustomizatonDisplayNonQualifiedCoinsOfAQualifiedCoinGroup";
            this.checkBoxCoinCustomizatonDisplayNonQualifiedCoinsOfAQualifiedCoinGroup.Size = new System.Drawing.Size(156, 30);
            this.checkBoxCoinCustomizatonDisplayNonQualifiedCoinsOfAQualifiedCoinGroup.TabIndex = 43;
            this.checkBoxCoinCustomizatonDisplayNonQualifiedCoinsOfAQualifiedCoinGroup.Text = "Display Non-Qualified Coins\r\nof a Qualified Coin Group";
            this.checkBoxCoinCustomizatonDisplayNonQualifiedCoinsOfAQualifiedCoinGroup.UseVisualStyleBackColor = true;
            // 
            // textBoxCoinCustomizatonStartingRngIndex
            // 
            this.textBoxCoinCustomizatonStartingRngIndex.Location = new System.Drawing.Point(155, 74);
            this.textBoxCoinCustomizatonStartingRngIndex.Name = "textBoxCoinCustomizatonStartingRngIndex";
            this.textBoxCoinCustomizatonStartingRngIndex.Size = new System.Drawing.Size(53, 20);
            this.textBoxCoinCustomizatonStartingRngIndex.TabIndex = 40;
            this.textBoxCoinCustomizatonStartingRngIndex.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelCoinCustomizatonNumDecimalDigits
            // 
            this.labelCoinCustomizatonNumDecimalDigits.AutoSize = true;
            this.labelCoinCustomizatonNumDecimalDigits.Location = new System.Drawing.Point(51, 53);
            this.labelCoinCustomizatonNumDecimalDigits.Name = "labelCoinCustomizatonNumDecimalDigits";
            this.labelCoinCustomizatonNumDecimalDigits.Size = new System.Drawing.Size(102, 13);
            this.labelCoinCustomizatonNumDecimalDigits.TabIndex = 37;
            this.labelCoinCustomizatonNumDecimalDigits.Text = "Num Decimal Digits:";
            // 
            // labelCoinCustomizatonStartingRngIndex
            // 
            this.labelCoinCustomizatonStartingRngIndex.AutoSize = true;
            this.labelCoinCustomizatonStartingRngIndex.Location = new System.Drawing.Point(51, 77);
            this.labelCoinCustomizatonStartingRngIndex.Name = "labelCoinCustomizatonStartingRngIndex";
            this.labelCoinCustomizatonStartingRngIndex.Size = new System.Drawing.Size(102, 13);
            this.labelCoinCustomizatonStartingRngIndex.TabIndex = 37;
            this.labelCoinCustomizatonStartingRngIndex.Text = "Starting RNG Index:";
            // 
            // textBoxCoinCustomizatonNumDecimalDigits
            // 
            this.textBoxCoinCustomizatonNumDecimalDigits.Location = new System.Drawing.Point(155, 50);
            this.textBoxCoinCustomizatonNumDecimalDigits.Name = "textBoxCoinCustomizatonNumDecimalDigits";
            this.textBoxCoinCustomizatonNumDecimalDigits.Size = new System.Drawing.Size(53, 20);
            this.textBoxCoinCustomizatonNumDecimalDigits.TabIndex = 40;
            this.textBoxCoinCustomizatonNumDecimalDigits.Text = "3";
            this.textBoxCoinCustomizatonNumDecimalDigits.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // dataGridViewCoin
            // 
            this.dataGridViewCoin.AllowUserToAddRows = false;
            this.dataGridViewCoin.AllowUserToDeleteRows = false;
            this.dataGridViewCoin.AllowUserToOrderColumns = true;
            this.dataGridViewCoin.AllowUserToResizeRows = false;
            this.dataGridViewCoin.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewCoin.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewCoin.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCoin.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.rngIndex,
            this.rngValue,
            this.rngToGo,
            this.coinHSpeed,
            this.coinVSpeed,
            this.coinAngle});
            this.dataGridViewCoin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewCoin.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewCoin.Margin = new System.Windows.Forms.Padding(0);
            this.dataGridViewCoin.MultiSelect = false;
            this.dataGridViewCoin.Name = "dataGridViewCoin";
            this.dataGridViewCoin.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewCoin.Size = new System.Drawing.Size(622, 461);
            this.dataGridViewCoin.TabIndex = 3;
            // 
            // rngIndex
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.rngIndex.DefaultCellStyle = dataGridViewCellStyle2;
            this.rngIndex.HeaderText = "RNG Index";
            this.rngIndex.MinimumWidth = 2;
            this.rngIndex.Name = "rngIndex";
            // 
            // rngValue
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.rngValue.DefaultCellStyle = dataGridViewCellStyle3;
            this.rngValue.HeaderText = "RNG Value";
            this.rngValue.MinimumWidth = 2;
            this.rngValue.Name = "rngValue";
            // 
            // rngToGo
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.rngToGo.DefaultCellStyle = dataGridViewCellStyle4;
            this.rngToGo.HeaderText = "RNG To Go";
            this.rngToGo.MinimumWidth = 2;
            this.rngToGo.Name = "rngToGo";
            // 
            // coinHSpeed
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.coinHSpeed.DefaultCellStyle = dataGridViewCellStyle5;
            this.coinHSpeed.HeaderText = "Coin HSpeed";
            this.coinHSpeed.Name = "coinHSpeed";
            // 
            // coinVSpeed
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.coinVSpeed.DefaultCellStyle = dataGridViewCellStyle6;
            this.coinVSpeed.HeaderText = "Coin VSpeed";
            this.coinVSpeed.Name = "coinVSpeed";
            // 
            // coinAngle
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.coinAngle.DefaultCellStyle = dataGridViewCellStyle7;
            this.coinAngle.HeaderText = "Coin Angle";
            this.coinAngle.MinimumWidth = 2;
            this.coinAngle.Name = "coinAngle";
            // 
            // CoinTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerCoin);
            this.Name = "CoinTab";
            this.Size = new System.Drawing.Size(915, 463);
            this.splitContainerCoin.Panel1.ResumeLayout(false);
            this.splitContainerCoin.Panel1.PerformLayout();
            this.splitContainerCoin.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerCoin)).EndInit();
            this.splitContainerCoin.ResumeLayout(false);
            this.groupBoxCoinFilter.ResumeLayout(false);
            this.groupBoxCoinFilter.PerformLayout();
            this.groupBoxCoinCustomization.ResumeLayout(false);
            this.groupBoxCoinCustomization.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCoin)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private BetterSplitContainer splitContainerCoin;
        private System.Windows.Forms.Button buttonCoinCalculate;
        private System.Windows.Forms.Button buttonCoinClear;
        private System.Windows.Forms.GroupBox groupBoxCoinFilter;
        private BetterTextbox textBoxCoinFilterRequiredNumOfQualifiedCoins;
        private BetterTextbox textBoxCoinFilterAngleMin;
        private BetterTextbox textBoxCoinFilterAngleMax;
        private System.Windows.Forms.Label labelCoinFilterHSpeedFrom;
        private BetterTextbox textBoxCoinFilterVSpeedMax;
        private System.Windows.Forms.Label labelCoinFilterMin;
        private System.Windows.Forms.Label labelCoinFilterMax;
        private BetterTextbox textBoxCoinFilterVSpeedMin;
        private System.Windows.Forms.Label labelCoinFilterHSpeedTo;
        private BetterTextbox textBoxCoinFilterHSpeedMax;
        private System.Windows.Forms.Label labelCoinFilterVSpeedFrom;
        private System.Windows.Forms.Label labelCoinFilterVSpeedTo;
        private BetterTextbox textBoxCoinFilterHSpeedMin;
        private System.Windows.Forms.Label labelCoinFilterAngleFrom;
        private System.Windows.Forms.Label labelCoinFilterAngleTo;
        private System.Windows.Forms.Label labelCoinFilterRequiredNumOfQualifiedCoins;
        private BetterTextbox textBoxCoinNumCoins;
        private BetterTextbox textBoxCoinParamOrder;
        private BetterTextbox textBoxCoinVSpeedOffset;
        private BetterTextbox textBoxCoinVSpeedScale;
        private BetterTextbox textBoxCoinHSpeedScale;
        private System.Windows.Forms.Label labelCoinNumCoins;
        private System.Windows.Forms.Label labelCoinVSpeedRange;
        private System.Windows.Forms.Label labelCoinParamOrder;
        private System.Windows.Forms.Label labelCoinTableEntries;
        private System.Windows.Forms.Label labelCoinHSpeedRange;
        private System.Windows.Forms.Label labelCoinVSpeedOffset;
        private System.Windows.Forms.Label labelCoinVSpeedScale;
        private System.Windows.Forms.Label labelCoinHSpeedScale;
        private System.Windows.Forms.ListBox listBoxCoinObjects;
        private System.Windows.Forms.GroupBox groupBoxCoinCustomization;
        private System.Windows.Forms.CheckBox checkBoxCoinCustomizatonDisplayNonQualifiedCoinsOfAQualifiedCoinGroup;
        private BetterTextbox textBoxCoinCustomizatonStartingRngIndex;
        private System.Windows.Forms.Label labelCoinCustomizatonNumDecimalDigits;
        private System.Windows.Forms.Label labelCoinCustomizatonStartingRngIndex;
        private BetterTextbox textBoxCoinCustomizatonNumDecimalDigits;
        private System.Windows.Forms.DataGridView dataGridViewCoin;
        private System.Windows.Forms.DataGridViewTextBoxColumn rngIndex;
        private System.Windows.Forms.DataGridViewTextBoxColumn rngValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn rngToGo;
        private System.Windows.Forms.DataGridViewTextBoxColumn coinHSpeed;
        private System.Windows.Forms.DataGridViewTextBoxColumn coinVSpeed;
        private System.Windows.Forms.DataGridViewTextBoxColumn coinAngle;
    }
}
