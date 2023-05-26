
namespace Chesso
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1=new Label();
            button1=new Button();
            button2=new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize=true;
            label1.Font=new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location=new Point(8, 9);
            label1.Name="label1";
            label1.Size=new Size(128, 41);
            label1.TabIndex=1;
            label1.Text="CHESSO";
            // 
            // button1
            // 
            button1.Location=new Point(16, 81);
            button1.Name="button1";
            button1.Size=new Size(176, 34);
            button1.TabIndex=2;
            button1.Text="Start as white";
            button1.UseVisualStyleBackColor=true;
            // 
            // button2
            // 
            button2.Location=new Point(209, 81);
            button2.Name="button2";
            button2.Size=new Size(176, 34);
            button2.TabIndex=3;
            button2.Text="Start as black";
            button2.UseVisualStyleBackColor=true;
            button2.Click+=button2_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions=new SizeF(10F, 25F);
            AutoScaleMode=AutoScaleMode.Font;
            ClientSize=new Size(796, 792);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label1);
            MaximizeBox=false;
            MinimizeBox=false;
            Name="Form1";
            StartPosition=FormStartPosition.Manual;
            Text="Form1";
            Load+=Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private Button button1;
        private Button button2;
    }
}