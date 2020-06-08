using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ELA
{
    public partial class Form : System.Windows.Forms.Form
    {
        #region Initialization

        //fields
        bool isToggled = false;
        bool isImageFromFilesystem;

        //Image
        int quality;
        string loadedImagePath;
        JPEGLibrary.Encoder encodedImage;
        Bitmap inputImage;
        Bitmap decodedImage;

        //Window Movement
        int moving;
        int movX;
        int movY;

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // height of ellipse
            int nHeightEllipse // width of ellipse
        );

        public Form()
        {
            InitializeComponent();
            GotToUntoggledState();
            loadingPicture.Hide();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            this.Location = Screen.AllScreens[1].WorkingArea.Location;
        }

        #endregion

        #region Methods
        private async Task ApplyJpegAlgorithm()
        {
            decodedPictureBox.Image = null;

            encodedImage = isImageFromFilesystem
                ? new JPEGLibrary.Encoder(loadedImagePath, quality: quality, saveTextfiles: saveTxtCheckBox.Checked)
                : new JPEGLibrary.Encoder(loadedPictureBox.Image, loadedImagePath, quality: quality, saveTextfiles: saveTxtCheckBox.Checked);

            await encodedImage.Encode();

            JPEGLibrary.Decoder decoder = new JPEGLibrary.Decoder(encodedImage);
            await decoder.Decode();

            decodedImage = decoder.DecodedImage;

            //decodedPictureBox.Image = decodedImage;
        }

        private async Task ApplyELA(int M)
        {
            await Task.Run(() =>
            {
                if (loadedPictureBox.Image != null && decodedImage != null)
                {
                    
                    Bitmap input = new Bitmap(loadedPictureBox.Image);
                    Bitmap output = new Bitmap(decodedImage.Width, decodedImage.Height);

                    for (int x = 0; x < loadedPictureBox.Image.Width; x++)
                    {
                        for (int y = 0; y < loadedPictureBox.Image.Height; y++)
                        {
                            double difference_r = input.GetPixel(x, y).R - decodedImage.GetPixel(x, y).R;
                            double abs_r = Math.Abs(difference_r);
                            double r = (abs_r) * M;


                            double difference_g = input.GetPixel(x, y).G - decodedImage.GetPixel(x, y).G;
                            double abs_g = Math.Abs(difference_g);
                            double g = (abs_g) * M;


                            double difference_b = input.GetPixel(x, y).B - decodedImage.GetPixel(x, y).B;
                            double abs_b = Math.Abs(difference_b);
                            double b = (abs_b) * M;

                            if (r > 255)
                                r = 255;

                            if (g > 255)
                                g = 255;

                            if (b > 255)
                                b = 255;

                            Color color = Color.FromArgb((byte)r, (byte)g, (byte)b);

                            //Color color = Color.FromArgb(r, g, b);
                            output.SetPixel(x, y, color);
                        }
                    }
                    output.Save("ELA.png");
                    decodedPictureBox.Image = output;
                }
            });
        }

        #endregion

        #region Button Clicks

        private void chooseImageButton_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                //openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                openFileDialog.InitialDirectory = @"C:\Users\Christoph\Google Drive\Studium\Master\2. Semester\Datenkompression\Beleg\dkprp4\Testbilder";
                openFileDialog.Filter = ".png|*.png|.jpg|*.jpg";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        loadedImagePath = openFileDialog.FileName;
                        inputImage = new Bitmap(loadedImagePath);

                        loadedPictureBox.Image = inputImage;
                        isImageFromFilesystem = true;
                    }
                    catch
                    {
                        MessageBox.Show("Fehler bei der ausgewählten Datei. Bitte wählen Sie ein anderes Bild.");
                    }
                }
            }

        }
        private async void startButton_Click(object sender, EventArgs e)
        {
            //if (decodedImage != null && isNewImage == false)
            //{
            //    loadingPicture.Show();

            //    decodedPictureBox.Image = null;
            //    await ApplyELA(int.Parse(contrastTextBox.Text));

            //    loadingPicture.Hide();
            //    return;
            //}

            if (loadedPictureBox.Image != null)
            {
                
                if (!int.TryParse(qualityTextBox.Text, out quality) || quality <= 0 || quality > 100)
                {
                    MessageBox.Show("Ungültiger Qualitätswert. Qualität darf nur zwischen 0 und 100 sein");
                    qualityTextBox.Text = "";
                    return;
                }
                if (isToggled == false)
                {
                    ToggleImage2();
                }

                loadingPicture.Show();
                await ApplyJpegAlgorithm();

                if (saveDecodedImagecheckBox.Checked)
                {
                    decodedImage.Save($"{ encodedImage.ImageName }_quality={ encodedImage.Quality}.png", ImageFormat.Png);
                }

                await ApplyELA(int.Parse(contrastTextBox.Text));
                loadingPicture.Hide();
            }
            else
            {
                chooseImageButton_Click(this, null);
            }

            
        }
        private void SetDecodedAsInputButton_Click(object sender, EventArgs e)
        {
            if (decodedPictureBox.Image != null)
            {
                loadedPictureBox.Image = decodedPictureBox.Image;
                decodedPictureBox.Image = null;
                isImageFromFilesystem = false;
            }
            else
            {
                MessageBox.Show("Bitte wähle zuerst ein Bild aus und decodiere es um es als Input festlegen zu können.");
            }

        }
        private void deleteInputButton_Click(object sender, EventArgs e)
        {
            loadedPictureBox.Image = null;
        }
        private void deleteOutputButton_Click(object sender, EventArgs e)
        {
            decodedPictureBox.Image = null;
        }
        private void expandImageButton_Click(object sender, EventArgs e)
        {
            //FullscreenImage fullscreen = new FullscreenImage(decodedPictureBox.Image);
            //fullscreen.Show();
        }
        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Expanding
        private void expandButton_Click(object sender, EventArgs e)
        {
            ToggleImage2();
        }
        private void ToggleImage2()
        {
            isToggled = !isToggled;

            if (isToggled)
            {
                GoToToggledState();
            }
            else
            {
                GotToUntoggledState();
            }
        }

        private void GoToToggledState()
        {
            this.Width = 1200;
            expandButton.Text = "<";
            closeButton.Location = new Point(1177, closeButton.Location.Y);

            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));

        }
        private void GotToUntoggledState()
        {
            this.Width = 718;
            expandButton.Text = ">";
            closeButton.Location = new Point(695, closeButton.Location.Y);

            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        #endregion

        #region Movement
        private void Form_MouseMove(object sender, MouseEventArgs e)
        {

            if (moving == 1)
            {
                this.SetDesktopLocation(MousePosition.X - movX, MousePosition.Y - movY);
            }
        }

        private void Form_MouseUp(object sender, MouseEventArgs e)
        {
            moving = 0;
        }

        private void Form_MouseDown(object sender, MouseEventArgs e)
        {
            moving = 1;
            movX = e.X;
            movY = e.Y;
        }

        #endregion

        
    }
}
