using System.Timers;
namespace GeneiFurius
{
    partial class BasicForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BasicForm));
            this.PlayersListBox = new System.Windows.Forms.ListBox();
            this.ThrowSpell = new System.Timers.Timer();
            this.UpdatePlayers = new System.Windows.Forms.Timer(this.components);
            this.AutoATTimer = new System.Timers.Timer();
            this.AutoPotas = new System.Timers.Timer();
            this.TSpeed = new System.Timers.Timer();
            this.AutoEntrenar = new System.Timers.Timer();
            this.AutoEntrenarCheckBox = new MonoFlat.MonoFlat_CheckBox();
            this.ConsoleButton = new MonoFlat.MonoFlat_Button();
            this.SeguroInviCheckBox = new MonoFlat.MonoFlat_CheckBox();
            this.SpeedHackCheckBox = new MonoFlat.MonoFlat_CheckBox();
            this.AutoRemoCheckBox = new MonoFlat.MonoFlat_CheckBox();
            this.VerInvisCheckBox = new MonoFlat.MonoFlat_CheckBox();
            this.AutoPotasCheckBox = new MonoFlat.MonoFlat_CheckBox();
            this.monoFlat_Separator1 = new MonoFlat.MonoFlat_Separator();
            this.RemoverCartelCheckBox = new MonoFlat.MonoFlat_CheckBox();
            this.CheaterLabel = new MonoFlat.MonoFlat_Label();
            this.AutoAttackCheckBox = new MonoFlat.MonoFlat_CheckBox();
            this.CheaterNick_Label = new MonoFlat.MonoFlat_Label();
            this.ConfigButton = new MonoFlat.MonoFlat_Button();
            this.StatusLabel = new MonoFlat.MonoFlat_Label();
            this.Status_Label = new MonoFlat.MonoFlat_Label();
            this.monoFlat_HeaderLabel1 = new MonoFlat.MonoFlat_HeaderLabel();
            this.ControlBox1 = new MonoFlat.MonoFlat_ControlBox();
            this.StartButton = new MonoFlat.MonoFlat_Button();
            ((System.ComponentModel.ISupportInitialize)(this.ThrowSpell)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AutoATTimer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AutoPotas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AutoEntrenar)).BeginInit();
            this.SuspendLayout();
            // 
            // PlayersListBox
            // 
            this.PlayersListBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.PlayersListBox.FormattingEnabled = true;
            this.PlayersListBox.Location = new System.Drawing.Point(12, 218);
            this.PlayersListBox.Name = "PlayersListBox";
            this.PlayersListBox.Size = new System.Drawing.Size(187, 134);
            this.PlayersListBox.TabIndex = 2;
            // 
            // ThrowSpell
            // 
            this.ThrowSpell.SynchronizingObject = this;
            this.ThrowSpell.Elapsed += new System.Timers.ElapsedEventHandler(this.ThrowSpell_Tick);
            // 
            // UpdatePlayers
            // 
            this.UpdatePlayers.Interval = 50;
            this.UpdatePlayers.Tick += new System.EventHandler(this.UpdatePlayers_Tick);
            // 
            // AutoATTimer
            // 
            this.AutoATTimer.Interval = 50D;
            this.AutoATTimer.SynchronizingObject = this;
            this.AutoATTimer.Elapsed += new System.Timers.ElapsedEventHandler(this.AutoATTimer_Tick);
            // 
            // AutoPotas
            // 
            this.AutoPotas.Interval = 400D;
            this.AutoPotas.SynchronizingObject = this;
            this.AutoPotas.Elapsed += new System.Timers.ElapsedEventHandler(this.AutoPotas_Tick);
            // 
            // TSpeed
            // 
            this.TSpeed.Interval = 50D;
            this.TSpeed.SynchronizingObject = this;
            this.TSpeed.Elapsed += new System.Timers.ElapsedEventHandler(this.TSpeed_Tick);
            // 
            // AutoEntrenar
            // 
            this.AutoEntrenar.Interval = 50D;
            this.AutoEntrenar.SynchronizingObject = this;
            this.AutoEntrenar.Elapsed += new System.Timers.ElapsedEventHandler(this.AutoEntrenar_Elapsed);
            // 
            // AutoEntrenarCheckBox
            // 
            this.AutoEntrenarCheckBox.Checked = false;
            this.AutoEntrenarCheckBox.Enabled = false;
            this.AutoEntrenarCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.AutoEntrenarCheckBox.Location = new System.Drawing.Point(12, 194);
            this.AutoEntrenarCheckBox.Name = "AutoEntrenarCheckBox";
            this.AutoEntrenarCheckBox.Size = new System.Drawing.Size(113, 16);
            this.AutoEntrenarCheckBox.TabIndex = 26;
            this.AutoEntrenarCheckBox.Text = "AutoEntrenar";
            this.AutoEntrenarCheckBox.Visible = false;
            this.AutoEntrenarCheckBox.CheckedChanged += new MonoFlat.MonoFlat_CheckBox.CheckedChangedEventHandler(this.AutoEntrenarCheckBox_CheckedChanged);
            // 
            // ConsoleButton
            // 
            this.ConsoleButton.BackColor = System.Drawing.Color.Transparent;
            this.ConsoleButton.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.ConsoleButton.Image = null;
            this.ConsoleButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ConsoleButton.Location = new System.Drawing.Point(135, 190);
            this.ConsoleButton.Name = "ConsoleButton";
            this.ConsoleButton.Size = new System.Drawing.Size(68, 20);
            this.ConsoleButton.TabIndex = 25;
            this.ConsoleButton.Text = "Console";
            this.ConsoleButton.TextAlignment = System.Drawing.StringAlignment.Center;
            this.ConsoleButton.Click += new System.EventHandler(this.ConsoleButton_Click);
            // 
            // SeguroInviCheckBox
            // 
            this.SeguroInviCheckBox.Checked = false;
            this.SeguroInviCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.SeguroInviCheckBox.Location = new System.Drawing.Point(12, 172);
            this.SeguroInviCheckBox.Name = "SeguroInviCheckBox";
            this.SeguroInviCheckBox.Size = new System.Drawing.Size(113, 16);
            this.SeguroInviCheckBox.TabIndex = 24;
            this.SeguroInviCheckBox.Text = "SeguroInvi";
            this.SeguroInviCheckBox.CheckedChanged += new MonoFlat.MonoFlat_CheckBox.CheckedChangedEventHandler(this.SeguroInviCheckBox_CheckedChanged);
            // 
            // SpeedHackCheckBox
            // 
            this.SpeedHackCheckBox.Checked = false;
            this.SpeedHackCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.SpeedHackCheckBox.Location = new System.Drawing.Point(12, 150);
            this.SpeedHackCheckBox.Name = "SpeedHackCheckBox";
            this.SpeedHackCheckBox.Size = new System.Drawing.Size(113, 16);
            this.SpeedHackCheckBox.TabIndex = 23;
            this.SpeedHackCheckBox.Text = "SpeedHack";
            this.SpeedHackCheckBox.CheckedChanged += new MonoFlat.MonoFlat_CheckBox.CheckedChangedEventHandler(this.SpeedHackCheckBox_CheckedChanged);
            // 
            // AutoRemoCheckBox
            // 
            this.AutoRemoCheckBox.Checked = false;
            this.AutoRemoCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.AutoRemoCheckBox.Location = new System.Drawing.Point(12, 128);
            this.AutoRemoCheckBox.Name = "AutoRemoCheckBox";
            this.AutoRemoCheckBox.Size = new System.Drawing.Size(113, 16);
            this.AutoRemoCheckBox.TabIndex = 22;
            this.AutoRemoCheckBox.Text = "AutoRemo";
            this.AutoRemoCheckBox.CheckedChanged += new MonoFlat.MonoFlat_CheckBox.CheckedChangedEventHandler(this.AutoRemoCheckBox_CheckedChanged);
            // 
            // VerInvisCheckBox
            // 
            this.VerInvisCheckBox.Checked = false;
            this.VerInvisCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.VerInvisCheckBox.Location = new System.Drawing.Point(12, 106);
            this.VerInvisCheckBox.Name = "VerInvisCheckBox";
            this.VerInvisCheckBox.Size = new System.Drawing.Size(113, 16);
            this.VerInvisCheckBox.TabIndex = 21;
            this.VerInvisCheckBox.Text = "Ver Invis";
            this.VerInvisCheckBox.CheckedChanged += new MonoFlat.MonoFlat_CheckBox.CheckedChangedEventHandler(this.VerInvisCheckBox_CheckedChanged);
            // 
            // AutoPotasCheckBox
            // 
            this.AutoPotasCheckBox.Checked = false;
            this.AutoPotasCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.AutoPotasCheckBox.Location = new System.Drawing.Point(12, 84);
            this.AutoPotasCheckBox.Name = "AutoPotasCheckBox";
            this.AutoPotasCheckBox.Size = new System.Drawing.Size(113, 16);
            this.AutoPotasCheckBox.TabIndex = 20;
            this.AutoPotasCheckBox.Text = "AutoPotas";
            this.AutoPotasCheckBox.CheckedChanged += new MonoFlat.MonoFlat_CheckBox.CheckedChangedEventHandler(this.AutoPotasCheckBox_CheckedChanged);
            // 
            // monoFlat_Separator1
            // 
            this.monoFlat_Separator1.Location = new System.Drawing.Point(-3, 28);
            this.monoFlat_Separator1.Name = "monoFlat_Separator1";
            this.monoFlat_Separator1.Size = new System.Drawing.Size(212, 10);
            this.monoFlat_Separator1.TabIndex = 19;
            this.monoFlat_Separator1.Text = "monoFlat_Separator1";
            // 
            // RemoverCartelCheckBox
            // 
            this.RemoverCartelCheckBox.Checked = false;
            this.RemoverCartelCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.RemoverCartelCheckBox.Location = new System.Drawing.Point(12, 62);
            this.RemoverCartelCheckBox.Name = "RemoverCartelCheckBox";
            this.RemoverCartelCheckBox.Size = new System.Drawing.Size(113, 16);
            this.RemoverCartelCheckBox.TabIndex = 18;
            this.RemoverCartelCheckBox.Text = "RemoverCartel";
            this.RemoverCartelCheckBox.CheckedChanged += new MonoFlat.MonoFlat_CheckBox.CheckedChangedEventHandler(this.RemoverCartelCheckBox_CheckedChanged);
            // 
            // CheaterLabel
            // 
            this.CheaterLabel.AutoSize = true;
            this.CheaterLabel.BackColor = System.Drawing.Color.Transparent;
            this.CheaterLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.CheaterLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(125)))), ((int)(((byte)(132)))));
            this.CheaterLabel.Location = new System.Drawing.Point(9, 355);
            this.CheaterLabel.Name = "CheaterLabel";
            this.CheaterLabel.Size = new System.Drawing.Size(54, 15);
            this.CheaterLabel.TabIndex = 17;
            this.CheaterLabel.Text = "Cheater :";
            // 
            // AutoAttackCheckBox
            // 
            this.AutoAttackCheckBox.Checked = false;
            this.AutoAttackCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.AutoAttackCheckBox.Location = new System.Drawing.Point(12, 40);
            this.AutoAttackCheckBox.Name = "AutoAttackCheckBox";
            this.AutoAttackCheckBox.Size = new System.Drawing.Size(90, 16);
            this.AutoAttackCheckBox.TabIndex = 16;
            this.AutoAttackCheckBox.Text = "AutoAttack";
            this.AutoAttackCheckBox.CheckedChanged += new MonoFlat.MonoFlat_CheckBox.CheckedChangedEventHandler(this.AutoAttackCheckBox_CheckedChanged);
            // 
            // CheaterNick_Label
            // 
            this.CheaterNick_Label.AutoSize = true;
            this.CheaterNick_Label.BackColor = System.Drawing.Color.Transparent;
            this.CheaterNick_Label.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.CheaterNick_Label.ForeColor = System.Drawing.Color.Red;
            this.CheaterNick_Label.Location = new System.Drawing.Point(63, 355);
            this.CheaterNick_Label.Name = "CheaterNick_Label";
            this.CheaterNick_Label.Size = new System.Drawing.Size(0, 15);
            this.CheaterNick_Label.TabIndex = 15;
            // 
            // ConfigButton
            // 
            this.ConfigButton.BackColor = System.Drawing.Color.Transparent;
            this.ConfigButton.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.ConfigButton.Image = null;
            this.ConfigButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ConfigButton.Location = new System.Drawing.Point(147, 164);
            this.ConfigButton.Name = "ConfigButton";
            this.ConfigButton.Size = new System.Drawing.Size(56, 20);
            this.ConfigButton.TabIndex = 14;
            this.ConfigButton.Text = "Config";
            this.ConfigButton.TextAlignment = System.Drawing.StringAlignment.Center;
            this.ConfigButton.Click += new System.EventHandler(this.ConfigButton_Click);
            // 
            // StatusLabel
            // 
            this.StatusLabel.AutoSize = true;
            this.StatusLabel.BackColor = System.Drawing.Color.Transparent;
            this.StatusLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StatusLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(125)))), ((int)(((byte)(132)))));
            this.StatusLabel.Location = new System.Drawing.Point(157, 57);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(0, 21);
            this.StatusLabel.TabIndex = 11;
            // 
            // Status_Label
            // 
            this.Status_Label.AutoSize = true;
            this.Status_Label.BackColor = System.Drawing.Color.Transparent;
            this.Status_Label.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Status_Label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(125)))), ((int)(((byte)(132)))));
            this.Status_Label.Location = new System.Drawing.Point(144, 45);
            this.Status_Label.Name = "Status_Label";
            this.Status_Label.Size = new System.Drawing.Size(65, 15);
            this.Status_Label.TabIndex = 10;
            this.Status_Label.Text = "-- Status --";
            // 
            // monoFlat_HeaderLabel1
            // 
            this.monoFlat_HeaderLabel1.AutoSize = true;
            this.monoFlat_HeaderLabel1.BackColor = System.Drawing.Color.Transparent;
            this.monoFlat_HeaderLabel1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.monoFlat_HeaderLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.monoFlat_HeaderLabel1.Location = new System.Drawing.Point(18, 5);
            this.monoFlat_HeaderLabel1.Name = "monoFlat_HeaderLabel1";
            this.monoFlat_HeaderLabel1.Size = new System.Drawing.Size(79, 20);
            this.monoFlat_HeaderLabel1.TabIndex = 7;
            this.monoFlat_HeaderLabel1.Text = "Genei AO ";
            // 
            // ControlBox1
            // 
            this.ControlBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.ControlBox1.EnableHoverHighlight = false;
            this.ControlBox1.EnableMaximizeButton = false;
            this.ControlBox1.EnableMinimizeButton = true;
            this.ControlBox1.Location = new System.Drawing.Point(107, 0);
            this.ControlBox1.Name = "ControlBox1";
            this.ControlBox1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ControlBox1.Size = new System.Drawing.Size(100, 25);
            this.ControlBox1.TabIndex = 6;
            this.ControlBox1.Text = "monoFlat_ControlBox1";
            // 
            // StartButton
            // 
            this.StartButton.BackColor = System.Drawing.Color.Transparent;
            this.StartButton.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.StartButton.Image = null;
            this.StartButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.StartButton.Location = new System.Drawing.Point(66, 377);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(75, 18);
            this.StartButton.TabIndex = 3;
            this.StartButton.Text = "Start";
            this.StartButton.TextAlignment = System.Drawing.StringAlignment.Center;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // BasicForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(41)))), ((int)(((byte)(50)))));
            this.ClientSize = new System.Drawing.Size(207, 402);
            this.Controls.Add(this.AutoEntrenarCheckBox);
            this.Controls.Add(this.ConsoleButton);
            this.Controls.Add(this.SeguroInviCheckBox);
            this.Controls.Add(this.SpeedHackCheckBox);
            this.Controls.Add(this.AutoRemoCheckBox);
            this.Controls.Add(this.VerInvisCheckBox);
            this.Controls.Add(this.AutoPotasCheckBox);
            this.Controls.Add(this.monoFlat_Separator1);
            this.Controls.Add(this.RemoverCartelCheckBox);
            this.Controls.Add(this.CheaterLabel);
            this.Controls.Add(this.AutoAttackCheckBox);
            this.Controls.Add(this.CheaterNick_Label);
            this.Controls.Add(this.ConfigButton);
            this.Controls.Add(this.StatusLabel);
            this.Controls.Add(this.Status_Label);
            this.Controls.Add(this.monoFlat_HeaderLabel1);
            this.Controls.Add(this.ControlBox1);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.PlayersListBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BasicForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TeamSpeak 3";
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BasicForm_FormClosing);
            this.Load += new System.EventHandler(this.BasicForm_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BasicForm_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.ThrowSpell)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AutoATTimer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AutoPotas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AutoEntrenar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox PlayersListBox;
        private MonoFlat.MonoFlat_Button StartButton;
        private MonoFlat.MonoFlat_ControlBox ControlBox1;
        private MonoFlat.MonoFlat_HeaderLabel monoFlat_HeaderLabel1;
        private System.Timers.Timer ThrowSpell;
        private System.Windows.Forms.Timer UpdatePlayers;
        private MonoFlat.MonoFlat_Label Status_Label;
        private MonoFlat.MonoFlat_Label StatusLabel;
        private MonoFlat.MonoFlat_Button ConfigButton;
        private MonoFlat.MonoFlat_Label CheaterNick_Label;
        private System.Timers.Timer AutoATTimer;
        private MonoFlat.MonoFlat_CheckBox AutoAttackCheckBox;
        private MonoFlat.MonoFlat_Label CheaterLabel;
        private MonoFlat.MonoFlat_CheckBox RemoverCartelCheckBox;
        private MonoFlat.MonoFlat_Separator monoFlat_Separator1;
        private System.Timers.Timer AutoPotas;
        private MonoFlat.MonoFlat_CheckBox AutoPotasCheckBox;
        private MonoFlat.MonoFlat_CheckBox VerInvisCheckBox;
        private MonoFlat.MonoFlat_CheckBox AutoRemoCheckBox;
        private MonoFlat.MonoFlat_CheckBox SpeedHackCheckBox;
        private System.Timers.Timer TSpeed;
        private MonoFlat.MonoFlat_CheckBox SeguroInviCheckBox;
        private MonoFlat.MonoFlat_Button ConsoleButton;
        private Timer AutoEntrenar;
        private MonoFlat.MonoFlat_CheckBox AutoEntrenarCheckBox;
    }
}

