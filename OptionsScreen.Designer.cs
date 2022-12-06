using System.Windows.Forms.Design;

namespace TWIC;

partial class OptionsScreen
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>    
    private System.ComponentModel.IContainer components = null;
    private Button SaveButton;
    private System.Windows.Forms.NotifyIcon notifyIcon;
    private System.Windows.Forms.ContextMenuStrip notifyContextmenu;

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
        this.components = new System.ComponentModel.Container();
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(800, 450);
        this.Text = "Settings";

        SaveButton = new Button();
        SaveButton.Name = "Save Button";
        SaveButton.Text = "Save";
        SaveButton.Location = new System.Drawing.Point(20,20);

        this.Controls.Add(SaveButton);


        this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
        this.notifyIcon.Icon = new Icon("notify.ico");

        this.notifyIcon.Text = "Form1 (NotifyIcon example)";
        this.notifyIcon.Visible = true;

        this.notifyContextmenu = new System.Windows.Forms.ContextMenuStrip();
        this.notifyContextmenu.Items.Add("Settings", null, this.ShowSettingsScreen);
        this.notifyContextmenu.Items.Add("Exit", null, this.MenuExit);

        this.notifyIcon.ContextMenuStrip = this.notifyContextmenu;

        notifyIcon.DoubleClick += new System.EventHandler(this.notifyIcon_DoubleClick);

        
    }    

    private void notifyIcon_DoubleClick(object Sender, EventArgs e) 
    {
        // Show the form when the user double clicks on the notify icon.

        // Set the WindowState to normal if the form is minimized.
        this.Visible = true;    
        this.ShowInTaskbar = true;    
        this.WindowState = FormWindowState.Normal;

        // Activate the form.
        this.Activate();
    }

    private void ShowSettingsScreen(object Sender, EventArgs e)
    {
        this.Visible = true;
        this.ShowInTaskbar = true;    
        this.WindowState = FormWindowState.Normal;

    }

        private void MenuExit(object Sender, EventArgs e)
    {
        this.Close();
    }

    #endregion

}
