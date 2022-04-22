namespace skess
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnFlip = new System.Windows.Forms.Button();
            this.timerWhite = new System.Windows.Forms.Timer(this.components);
            this.timerBlack = new System.Windows.Forms.Timer(this.components);
            this.tbxBlackTime = new System.Windows.Forms.TextBox();
            this.tbxWhiteTime = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnShowFen = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.gbx_friend = new System.Windows.Forms.GroupBox();
            this.tbxRemotePort = new System.Windows.Forms.TextBox();
            this.tbxRemoteIP = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.gbx_me = new System.Windows.Forms.GroupBox();
            this.tbxLocalPort = new System.Windows.Forms.TextBox();
            this.tbxLocalIP = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnGiveUp = new System.Windows.Forms.Button();
            this.btnStalemate = new System.Windows.Forms.Button();
            this.tbxMessage = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.lbxMessages = new System.Windows.Forms.ListBox();
            this.btnTakeback = new System.Windows.Forms.Button();
            this.cbxPlayAsWhite = new System.Windows.Forms.CheckBox();
            this.btnRestart = new System.Windows.Forms.Button();
            this.lbxNotationList = new System.Windows.Forms.ListBox();
            this.btnSaveGame = new System.Windows.Forms.Button();
            this.btnOpenGame = new System.Windows.Forms.Button();
            this.btnSwitchPorts = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.BtnNotationRightArrow = new System.Windows.Forms.Button();
            this.BtnNotationCircle = new System.Windows.Forms.Button();
            this.BtnNotationLeftArrow = new System.Windows.Forms.Button();
            this.cbxPlayVsBot = new System.Windows.Forms.CheckBox();
            this.gbx_friend.SuspendLayout();
            this.gbx_me.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnFlip
            // 
            this.btnFlip.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnFlip.FlatAppearance.BorderSize = 0;
            this.btnFlip.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFlip.ForeColor = System.Drawing.Color.White;
            this.btnFlip.Location = new System.Drawing.Point(895, 203);
            this.btnFlip.Name = "btnFlip";
            this.btnFlip.Size = new System.Drawing.Size(87, 27);
            this.btnFlip.TabIndex = 0;
            this.btnFlip.Text = "Flip Board";
            this.btnFlip.UseVisualStyleBackColor = false;
            this.btnFlip.Click += new System.EventHandler(this.BtnFlip_Click);
            // 
            // timerWhite
            // 
            this.timerWhite.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // timerBlack
            // 
            this.timerBlack.Tick += new System.EventHandler(this.Timer2_Tick);
            // 
            // tbxBlackTime
            // 
            this.tbxBlackTime.Enabled = false;
            this.tbxBlackTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxBlackTime.Location = new System.Drawing.Point(804, 118);
            this.tbxBlackTime.Name = "tbxBlackTime";
            this.tbxBlackTime.Size = new System.Drawing.Size(87, 71);
            this.tbxBlackTime.TabIndex = 1;
            this.tbxBlackTime.Text = "10:00";
            // 
            // tbxWhiteTime
            // 
            this.tbxWhiteTime.Enabled = false;
            this.tbxWhiteTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxWhiteTime.Location = new System.Drawing.Point(804, 39);
            this.tbxWhiteTime.Name = "tbxWhiteTime";
            this.tbxWhiteTime.Size = new System.Drawing.Size(87, 71);
            this.tbxWhiteTime.TabIndex = 2;
            this.tbxWhiteTime.Text = "10:00";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(801, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "White Time:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(801, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "Black Time:";
            // 
            // btnShowFen
            // 
            this.btnShowFen.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnShowFen.FlatAppearance.BorderSize = 0;
            this.btnShowFen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShowFen.ForeColor = System.Drawing.Color.White;
            this.btnShowFen.Location = new System.Drawing.Point(803, 303);
            this.btnShowFen.Name = "btnShowFen";
            this.btnShowFen.Size = new System.Drawing.Size(180, 27);
            this.btnShowFen.TabIndex = 5;
            this.btnShowFen.Text = "Show Board As Fen Notation";
            this.btnShowFen.UseVisualStyleBackColor = false;
            this.btnShowFen.Click += new System.EventHandler(this.BtnShowFen_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnConnect.FlatAppearance.BorderSize = 0;
            this.btnConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConnect.ForeColor = System.Drawing.Color.White;
            this.btnConnect.Location = new System.Drawing.Point(1213, 253);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(107, 44);
            this.btnConnect.TabIndex = 8;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = false;
            this.btnConnect.Click += new System.EventHandler(this.BtnConnect_Click);
            // 
            // gbx_friend
            // 
            this.gbx_friend.Controls.Add(this.tbxRemotePort);
            this.gbx_friend.Controls.Add(this.tbxRemoteIP);
            this.gbx_friend.Controls.Add(this.label4);
            this.gbx_friend.Controls.Add(this.label5);
            this.gbx_friend.ForeColor = System.Drawing.Color.White;
            this.gbx_friend.Location = new System.Drawing.Point(1213, 117);
            this.gbx_friend.Name = "gbx_friend";
            this.gbx_friend.Size = new System.Drawing.Size(173, 100);
            this.gbx_friend.TabIndex = 7;
            this.gbx_friend.TabStop = false;
            this.gbx_friend.Text = "Friend";
            // 
            // tbxRemotePort
            // 
            this.tbxRemotePort.Location = new System.Drawing.Point(42, 72);
            this.tbxRemotePort.Name = "tbxRemotePort";
            this.tbxRemotePort.Size = new System.Drawing.Size(100, 20);
            this.tbxRemotePort.TabIndex = 5;
            this.tbxRemotePort.Text = "1338";
            // 
            // tbxRemoteIP
            // 
            this.tbxRemoteIP.Location = new System.Drawing.Point(42, 39);
            this.tbxRemoteIP.Name = "tbxRemoteIP";
            this.tbxRemoteIP.Size = new System.Drawing.Size(100, 20);
            this.tbxRemoteIP.TabIndex = 4;
            this.tbxRemoteIP.Text = "127.0.0.1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 15);
            this.label4.TabIndex = 3;
            this.label4.Text = "Port";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 42);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(18, 15);
            this.label5.TabIndex = 2;
            this.label5.Text = "IP";
            // 
            // gbx_me
            // 
            this.gbx_me.Controls.Add(this.tbxLocalPort);
            this.gbx_me.Controls.Add(this.tbxLocalIP);
            this.gbx_me.Controls.Add(this.label3);
            this.gbx_me.Controls.Add(this.label6);
            this.gbx_me.ForeColor = System.Drawing.Color.White;
            this.gbx_me.Location = new System.Drawing.Point(1213, 11);
            this.gbx_me.Name = "gbx_me";
            this.gbx_me.Size = new System.Drawing.Size(173, 100);
            this.gbx_me.TabIndex = 6;
            this.gbx_me.TabStop = false;
            this.gbx_me.Text = "Me";
            // 
            // tbxLocalPort
            // 
            this.tbxLocalPort.Location = new System.Drawing.Point(42, 71);
            this.tbxLocalPort.Name = "tbxLocalPort";
            this.tbxLocalPort.Size = new System.Drawing.Size(100, 20);
            this.tbxLocalPort.TabIndex = 3;
            this.tbxLocalPort.Text = "1337";
            // 
            // tbxLocalIP
            // 
            this.tbxLocalIP.Location = new System.Drawing.Point(42, 39);
            this.tbxLocalIP.Name = "tbxLocalIP";
            this.tbxLocalIP.Size = new System.Drawing.Size(100, 20);
            this.tbxLocalIP.TabIndex = 2;
            this.tbxLocalIP.Text = "127.0.0.1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 15);
            this.label3.TabIndex = 1;
            this.label3.Text = "Port";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 39);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(18, 15);
            this.label6.TabIndex = 0;
            this.label6.Text = "IP";
            // 
            // btnGiveUp
            // 
            this.btnGiveUp.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnGiveUp.FlatAppearance.BorderSize = 0;
            this.btnGiveUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGiveUp.ForeColor = System.Drawing.Color.White;
            this.btnGiveUp.Location = new System.Drawing.Point(804, 236);
            this.btnGiveUp.Name = "btnGiveUp";
            this.btnGiveUp.Size = new System.Drawing.Size(87, 27);
            this.btnGiveUp.TabIndex = 9;
            this.btnGiveUp.Text = "Give Up";
            this.btnGiveUp.UseVisualStyleBackColor = false;
            this.btnGiveUp.Click += new System.EventHandler(this.BtnGiveUp_Click);
            // 
            // btnStalemate
            // 
            this.btnStalemate.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnStalemate.FlatAppearance.BorderSize = 0;
            this.btnStalemate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStalemate.ForeColor = System.Drawing.Color.White;
            this.btnStalemate.Location = new System.Drawing.Point(804, 269);
            this.btnStalemate.Name = "btnStalemate";
            this.btnStalemate.Size = new System.Drawing.Size(179, 27);
            this.btnStalemate.TabIndex = 10;
            this.btnStalemate.Text = "Propose Draw";
            this.btnStalemate.UseVisualStyleBackColor = false;
            this.btnStalemate.Click += new System.EventHandler(this.BtnStalemate_Click);
            // 
            // tbxMessage
            // 
            this.tbxMessage.Enabled = false;
            this.tbxMessage.Location = new System.Drawing.Point(804, 758);
            this.tbxMessage.Name = "tbxMessage";
            this.tbxMessage.Size = new System.Drawing.Size(293, 20);
            this.tbxMessage.TabIndex = 11;
            // 
            // btnSend
            // 
            this.btnSend.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnSend.Enabled = false;
            this.btnSend.FlatAppearance.BorderSize = 0;
            this.btnSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSend.ForeColor = System.Drawing.Color.White;
            this.btnSend.Location = new System.Drawing.Point(1102, 756);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 21);
            this.btnSend.TabIndex = 12;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = false;
            this.btnSend.Click += new System.EventHandler(this.BtnSend_Click);
            // 
            // lbxMessages
            // 
            this.lbxMessages.FormattingEnabled = true;
            this.lbxMessages.Location = new System.Drawing.Point(804, 422);
            this.lbxMessages.Name = "lbxMessages";
            this.lbxMessages.Size = new System.Drawing.Size(375, 329);
            this.lbxMessages.TabIndex = 13;
            // 
            // btnTakeback
            // 
            this.btnTakeback.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnTakeback.Enabled = false;
            this.btnTakeback.FlatAppearance.BorderSize = 0;
            this.btnTakeback.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTakeback.ForeColor = System.Drawing.Color.White;
            this.btnTakeback.Location = new System.Drawing.Point(895, 236);
            this.btnTakeback.Name = "btnTakeback";
            this.btnTakeback.Size = new System.Drawing.Size(87, 27);
            this.btnTakeback.TabIndex = 15;
            this.btnTakeback.Text = "Takeback";
            this.btnTakeback.UseVisualStyleBackColor = false;
            this.btnTakeback.Click += new System.EventHandler(this.BtnTakeback_Click);
            // 
            // cbxPlayAsWhite
            // 
            this.cbxPlayAsWhite.AutoSize = true;
            this.cbxPlayAsWhite.Checked = true;
            this.cbxPlayAsWhite.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxPlayAsWhite.ForeColor = System.Drawing.Color.White;
            this.cbxPlayAsWhite.Location = new System.Drawing.Point(895, 183);
            this.cbxPlayAsWhite.Name = "cbxPlayAsWhite";
            this.cbxPlayAsWhite.Size = new System.Drawing.Size(104, 21);
            this.cbxPlayAsWhite.TabIndex = 16;
            this.cbxPlayAsWhite.Text = "Play as white";
            this.cbxPlayAsWhite.UseVisualStyleBackColor = true;
            this.cbxPlayAsWhite.CheckedChanged += new System.EventHandler(this.CbxPlayAsWhite_CheckedChanged);
            // 
            // btnRestart
            // 
            this.btnRestart.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnRestart.FlatAppearance.BorderSize = 0;
            this.btnRestart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRestart.ForeColor = System.Drawing.Color.White;
            this.btnRestart.Location = new System.Drawing.Point(803, 203);
            this.btnRestart.Name = "btnRestart";
            this.btnRestart.Size = new System.Drawing.Size(87, 27);
            this.btnRestart.TabIndex = 17;
            this.btnRestart.Text = "Restart";
            this.btnRestart.UseVisualStyleBackColor = false;
            this.btnRestart.Click += new System.EventHandler(this.BtnRestart_Click);
            // 
            // lbxNotationList
            // 
            this.lbxNotationList.Font = new System.Drawing.Font("Courier New", 9.857143F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lbxNotationList.FormattingEnabled = true;
            this.lbxNotationList.ItemHeight = 27;
            this.lbxNotationList.Location = new System.Drawing.Point(5, 17);
            this.lbxNotationList.Name = "lbxNotationList";
            this.lbxNotationList.Size = new System.Drawing.Size(211, 274);
            this.lbxNotationList.TabIndex = 18;
            this.lbxNotationList.DoubleClick += new System.EventHandler(this.LbxNotationList_DoubleClick);
            this.lbxNotationList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LbxNotationList_KeyDown);
            // 
            // btnSaveGame
            // 
            this.btnSaveGame.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnSaveGame.FlatAppearance.BorderSize = 0;
            this.btnSaveGame.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveGame.ForeColor = System.Drawing.Color.White;
            this.btnSaveGame.Location = new System.Drawing.Point(5, 305);
            this.btnSaveGame.Name = "btnSaveGame";
            this.btnSaveGame.Size = new System.Drawing.Size(98, 23);
            this.btnSaveGame.TabIndex = 19;
            this.btnSaveGame.Text = "Save Game";
            this.btnSaveGame.UseVisualStyleBackColor = false;
            this.btnSaveGame.Click += new System.EventHandler(this.BtnSaveGame_Click);
            // 
            // btnOpenGame
            // 
            this.btnOpenGame.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnOpenGame.FlatAppearance.BorderSize = 0;
            this.btnOpenGame.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenGame.ForeColor = System.Drawing.Color.White;
            this.btnOpenGame.Location = new System.Drawing.Point(111, 305);
            this.btnOpenGame.Name = "btnOpenGame";
            this.btnOpenGame.Size = new System.Drawing.Size(103, 23);
            this.btnOpenGame.TabIndex = 20;
            this.btnOpenGame.Text = "Open Game";
            this.btnOpenGame.UseVisualStyleBackColor = false;
            this.btnOpenGame.Click += new System.EventHandler(this.BtnOpenGame_Click);
            // 
            // btnSwitchPorts
            // 
            this.btnSwitchPorts.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnSwitchPorts.FlatAppearance.BorderSize = 0;
            this.btnSwitchPorts.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSwitchPorts.ForeColor = System.Drawing.Color.White;
            this.btnSwitchPorts.Location = new System.Drawing.Point(1213, 223);
            this.btnSwitchPorts.Name = "btnSwitchPorts";
            this.btnSwitchPorts.Size = new System.Drawing.Size(75, 23);
            this.btnSwitchPorts.TabIndex = 14;
            this.btnSwitchPorts.Text = "Switch Ports";
            this.btnSwitchPorts.UseVisualStyleBackColor = false;
            this.btnSwitchPorts.Click += new System.EventHandler(this.BtnSwitchPorts_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.BtnNotationRightArrow);
            this.groupBox1.Controls.Add(this.BtnNotationCircle);
            this.groupBox1.Controls.Add(this.BtnNotationLeftArrow);
            this.groupBox1.Controls.Add(this.lbxNotationList);
            this.groupBox1.Controls.Add(this.btnOpenGame);
            this.groupBox1.Controls.Add(this.btnSaveGame);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(988, 11);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Size = new System.Drawing.Size(220, 402);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Notation";
            // 
            // BtnNotationRightArrow
            // 
            this.BtnNotationRightArrow.BackColor = System.Drawing.Color.RoyalBlue;
            this.BtnNotationRightArrow.FlatAppearance.BorderSize = 0;
            this.BtnNotationRightArrow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnNotationRightArrow.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.BtnNotationRightArrow.Location = new System.Drawing.Point(148, 333);
            this.BtnNotationRightArrow.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.BtnNotationRightArrow.Name = "BtnNotationRightArrow";
            this.BtnNotationRightArrow.Size = new System.Drawing.Size(68, 46);
            this.BtnNotationRightArrow.TabIndex = 23;
            this.BtnNotationRightArrow.Text = "RightArrow";
            this.BtnNotationRightArrow.UseVisualStyleBackColor = false;
            this.BtnNotationRightArrow.Click += new System.EventHandler(this.BtnNotationRightArrow_Click);
            // 
            // BtnNotationCircle
            // 
            this.BtnNotationCircle.BackColor = System.Drawing.Color.RoyalBlue;
            this.BtnNotationCircle.FlatAppearance.BorderSize = 0;
            this.BtnNotationCircle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnNotationCircle.Location = new System.Drawing.Point(76, 333);
            this.BtnNotationCircle.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.BtnNotationCircle.Name = "BtnNotationCircle";
            this.BtnNotationCircle.Size = new System.Drawing.Size(68, 46);
            this.BtnNotationCircle.TabIndex = 22;
            this.BtnNotationCircle.Text = "Circle";
            this.BtnNotationCircle.UseVisualStyleBackColor = false;
            this.BtnNotationCircle.Click += new System.EventHandler(this.BtnNotationCircle_Click);
            // 
            // BtnNotationLeftArrow
            // 
            this.BtnNotationLeftArrow.BackColor = System.Drawing.Color.RoyalBlue;
            this.BtnNotationLeftArrow.FlatAppearance.BorderSize = 0;
            this.BtnNotationLeftArrow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnNotationLeftArrow.Location = new System.Drawing.Point(5, 333);
            this.BtnNotationLeftArrow.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.BtnNotationLeftArrow.Name = "BtnNotationLeftArrow";
            this.BtnNotationLeftArrow.Size = new System.Drawing.Size(68, 46);
            this.BtnNotationLeftArrow.TabIndex = 21;
            this.BtnNotationLeftArrow.Text = "LeftArrow";
            this.BtnNotationLeftArrow.UseVisualStyleBackColor = false;
            this.BtnNotationLeftArrow.Click += new System.EventHandler(this.BtnNotationLeftArrow_Click);
            // 
            // cbxPlayVsBot
            // 
            this.cbxPlayVsBot.AutoSize = true;
            this.cbxPlayVsBot.Checked = true;
            this.cbxPlayVsBot.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxPlayVsBot.ForeColor = System.Drawing.Color.White;
            this.cbxPlayVsBot.Location = new System.Drawing.Point(804, 183);
            this.cbxPlayVsBot.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbxPlayVsBot.Name = "cbxPlayVsBot";
            this.cbxPlayVsBot.Size = new System.Drawing.Size(97, 21);
            this.cbxPlayVsBot.TabIndex = 22;
            this.cbxPlayVsBot.Text = "Play v.s. Bot";
            this.cbxPlayVsBot.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1417, 800);
            this.Controls.Add(this.cbxPlayVsBot);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnRestart);
            this.Controls.Add(this.cbxPlayAsWhite);
            this.Controls.Add(this.btnTakeback);
            this.Controls.Add(this.btnSwitchPorts);
            this.Controls.Add(this.lbxMessages);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.tbxMessage);
            this.Controls.Add(this.btnStalemate);
            this.Controls.Add(this.btnGiveUp);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.gbx_friend);
            this.Controls.Add(this.gbx_me);
            this.Controls.Add(this.btnShowFen);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbxWhiteTime);
            this.Controls.Add(this.tbxBlackTime);
            this.Controls.Add(this.btnFlip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.RightToLeftLayout = true;
            this.Text = "Chess";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.gbx_friend.ResumeLayout(false);
            this.gbx_friend.PerformLayout();
            this.gbx_me.ResumeLayout(false);
            this.gbx_me.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnFlip;
        private System.Windows.Forms.Timer timerWhite;
        private System.Windows.Forms.Timer timerBlack;
        private System.Windows.Forms.TextBox tbxBlackTime;
        private System.Windows.Forms.TextBox tbxWhiteTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnShowFen;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.GroupBox gbx_friend;
        private System.Windows.Forms.TextBox tbxRemotePort;
        private System.Windows.Forms.TextBox tbxRemoteIP;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox gbx_me;
        private System.Windows.Forms.TextBox tbxLocalPort;
        private System.Windows.Forms.TextBox tbxLocalIP;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnGiveUp;
        private System.Windows.Forms.Button btnStalemate;
        private System.Windows.Forms.TextBox tbxMessage;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.ListBox lbxMessages;
        private System.Windows.Forms.Button btnTakeback;
        private System.Windows.Forms.CheckBox cbxPlayAsWhite;
        private System.Windows.Forms.Button btnRestart;
        private System.Windows.Forms.ListBox lbxNotationList;
        private System.Windows.Forms.Button btnSaveGame;
        private System.Windows.Forms.Button btnOpenGame;
        private System.Windows.Forms.Button btnSwitchPorts;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button BtnNotationRightArrow;
        private System.Windows.Forms.Button BtnNotationCircle;
        private System.Windows.Forms.Button BtnNotationLeftArrow;
        private System.Windows.Forms.CheckBox cbxPlayVsBot;
    }
}

