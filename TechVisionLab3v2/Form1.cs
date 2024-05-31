using System.Diagnostics.Metrics;
using System.IO;
using System.Windows.Forms;

namespace TechVisionLab3v2
{

    public partial class Form1 : Form
    {


        List<Rectangle> rect;
        List<Bitmap> bit;

        List<String> signName = new List<String>()
        {
            "Stop",
            "Forward",
            "Turn left",
            "Brick"
        };

        Bitmap bitmapBuffer;
        Bitmap objectBitmap;
        Bitmap maskBitmap;
        int encodIndex = 0;

        List<Cluster> clusters;

        string[] maskFiles = Directory.GetFiles("Lab03_Mask", "*.png");
        string[] templateFiles = Directory.GetFiles("Lab03_Template", "*.png");

        public Form1()
        {
            InitializeComponent();
        }

        private void SelectPicture_Click(object sender, EventArgs e)
        {
            try
            {
                using (var fbd = new FolderBrowserDialog())
                {
                    DialogResult result = fbd.ShowDialog();

                    if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                    {
                        string[] imageFiles = Directory.GetFiles(fbd.SelectedPath);
                        foreach (string imagePath in imageFiles)
                        {
                            ListViewItem item = new ListViewItem(Path.GetFileName(imagePath));
                            item.Tag = imagePath;
                            listView1.Items.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //pictureBox1.Image = null; 
            //pictureBox2.Image = null;
            bitmapBuffer = null;
            if (listView1.SelectedItems.Count == 1)
            {
                string selectedImagePath = listView1.SelectedItems[0].Tag.ToString();

                bitmapBuffer = new Bitmap(selectedImagePath);
                pictureBox.Image = bitmapBuffer;

                if (AutoMode.Checked)
                    AutoModeFunc(bitmapBuffer);
            }
        }

        private void AutoModeFunc(Bitmap bmp)
        {
            rect = new List<Rectangle>();
            bit = new List<Bitmap>();
            for (int i = 1; i < 3; i++)
            {
                Bitmap bmpForCluster = new Bitmap(bmp);
                bmpForCluster = CreateWhiteAndBlackImage(bmpForCluster, i);

                int threshold = 128;

                // Создание массива для отслеживания обработанных пикселей
                bool[,] processed = new bool[bmpForCluster.Width, bmpForCluster.Height];

                for (int y = 0; y < bmpForCluster.Height; y++)
                {
                    for (int x = 0; x < bmpForCluster.Width; x++)
                    {
                        if (processed[x, y])
                            continue;

                        Color pixelColor = bmpForCluster.GetPixel(x, y);

                        if (pixelColor.R >= threshold)
                        {
                            List<System.Drawing.Point> connectedComponent = FindConnectedComponent(bmpForCluster, x, y, threshold, processed);
                            Rectangle rectangle = GetBoundingBox(connectedComponent);

                            if (rectangle.Width > 10 && rectangle.Width < 1000 && rectangle.Height > 10 && rectangle.Height < 1000)
                            {
                                rect.Add(rectangle);
                                bit.Add(bmpForCluster.Clone(rectangle, bmpForCluster.PixelFormat));
                            }
                        }
                    }
                }
            }

            using (Graphics graphics = Graphics.FromImage(pictureBox.Image))
            {
                for (int i = 0; i < rect.Count; i++)
                {
                    Bitmap croppedBitmap = new Bitmap(bit[i]);

                    double bestSimilarity = 0;
                    int bestIndex = 0;

                    for (int j = 0; j < maskFiles.Length; j++)
                    {
                        Bitmap mask = new Bitmap(maskFiles[j]);
                        croppedBitmap = ResizeBitmap(croppedBitmap, mask.Width, mask.Height);

                        double similarity = CheckSimilarity(croppedBitmap, mask);

                        if (similarity > bestSimilarity)
                        {
                            bestSimilarity = similarity;
                            bestIndex = j;
                        }
                    }

                    if (bestSimilarity > (double)numericTrust.Value)
                    {
                        double trust = bestSimilarity * 100;
                        graphics.DrawString($"{signName[bestIndex]}, {(int)trust}%", new Font("Arial", 9, FontStyle.Bold), new SolidBrush(Color.DarkViolet), rect[i].X - 15, rect[i].Height + rect[i].Y);
                        graphics.DrawRectangle(Pens.Red, rect[i]);
                    }
                }
            };
        }

        private double CheckSimilarity(Bitmap croppedBitmap, Bitmap mask)
        {
            int counter = 0;
            int width = croppedBitmap.Width, height = croppedBitmap.Height;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Color colorSimFirst = croppedBitmap.GetPixel(i, j);
                    Color colorSimSecond = mask.GetPixel(i, j);

                    if (colorSimFirst == colorSimSecond)
                        counter++;
                }
            }

            return (double)counter / ((double)width * (double)height);
        }

        private Bitmap ResizeBitmap(Bitmap croppedBitmap, int width, int height)
        {
            Bitmap resizedBitmap = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(resizedBitmap))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(croppedBitmap, 0, 0, width, height);
            }
            return resizedBitmap;
        }

        private Rectangle GetBoundingBox(List<Point> connectedComponent)
        {
            int minX = int.MaxValue;
            int maxX = int.MinValue;
            int minY = int.MaxValue;
            int maxY = int.MinValue;

            foreach (System.Drawing.Point point in connectedComponent)
            {
                if (point.X < minX)
                    minX = point.X;
                if (point.X > maxX)
                    maxX = point.X;
                if (point.Y < minY)
                    minY = point.Y;
                if (point.Y > maxY)
                    maxY = point.Y;
            }
            Rectangle rectangle = new Rectangle(minX, minY, maxX - minX + 1, maxY - minY + 1);

            return rectangle;
        }

        private List<Point> FindConnectedComponent(Bitmap bmpForCluster, int startX, int startY, int threshold, bool[,] processed)
        {
            List<Point> connectedComponent = new List<System.Drawing.Point>();

            Queue<Point> queue = new Queue<System.Drawing.Point>();
            queue.Enqueue(new System.Drawing.Point(startX, startY));

            while (queue.Count > 0)
            {
                System.Drawing.Point current = queue.Dequeue();
                int x = current.X;
                int y = current.Y;

                if (processed[x, y])
                    continue;

                Color pixelColor = bmpForCluster.GetPixel(x, y);

                if (pixelColor.R >= threshold)
                {
                    connectedComponent.Add(new System.Drawing.Point(x, y));
                    processed[x, y] = true;

                    if (x > 0)
                        queue.Enqueue(new System.Drawing.Point(x - 1, y));
                    if (x < bmpForCluster.Width - 1)
                        queue.Enqueue(new System.Drawing.Point(x + 1, y));
                    if (y > 0)
                        queue.Enqueue(new System.Drawing.Point(x, y - 1));
                    if (y < bmpForCluster.Height - 1)
                        queue.Enqueue(new System.Drawing.Point(x, y + 1));
                }
            }

            return connectedComponent;
        }

        private Bitmap CreateWhiteAndBlackImage(Bitmap bmp, int indexForClr = 0)
        {
            switch (indexForClr)
            {
                case 0:
                    for (int i = 0; i < bmp.Width; i++)
                    {
                        for (int j = 0; j < bmp.Height; j++)
                        {
                            Color newColor = (int)bmp.GetPixel(i, j).R > (int)RminNum.Value && (int)bmp.GetPixel(i, j).R < (int)RmaxNum.Value && (int)bmp.GetPixel(i, j).G > (int)GminNum.Value && (int)bmp.GetPixel(i, j).G < (int)GmaxNum.Value && (int)bmp.GetPixel(i, j).B > (int)BminNum.Value && (int)bmp.GetPixel(i, j).B < (int)BmaxNum.Value ? Color.FromArgb(255, 255, 255) : Color.FromArgb(0, 0, 0);
                            bmp.SetPixel(i, j, newColor);
                        }
                    }
                    break;
                case 1: // Синий (автоанализ)
                    for (int i = 0; i < bmp.Width; i++)
                    {
                        for (int j = 0; j < bmp.Height; j++)
                        {
                            Color newColor = (int)bmp.GetPixel(i, j).R > 0 && (int)bmp.GetPixel(i, j).R < 20 && (int)bmp.GetPixel(i, j).G > 10 && (int)bmp.GetPixel(i, j).G < 100 && (int)bmp.GetPixel(i, j).B > 20 && (int)bmp.GetPixel(i, j).B < 170 ? Color.FromArgb(255, 255, 255) : Color.FromArgb(0, 0, 0);
                            bmp.SetPixel(i, j, newColor);
                        }
                    }
                    break;
                case 2: //Красный (автоанализ)
                    for (int i = 0; i < bmp.Width; i++)
                    {
                        for (int j = 0; j < bmp.Height; j++)
                        {
                            Color newColor = (int)bmp.GetPixel(i, j).R > 130 && (int)bmp.GetPixel(i, j).R < 255 && (int)bmp.GetPixel(i, j).G > 0 && (int)bmp.GetPixel(i, j).G < 110 && (int)bmp.GetPixel(i, j).B > 0 && (int)bmp.GetPixel(i, j).B < 110 ? Color.FromArgb(255, 255, 255) : Color.FromArgb(0, 0, 0);
                            bmp.SetPixel(i, j, newColor);
                        }
                    }
                    break;
            }

            return bmp;
        }

        private void SetMaskBtn_Click(object sender, EventArgs e)
        {
            if (pictureBox.Image != null)
            {
                Bitmap bmp = new Bitmap(bitmapBuffer);

                for (int i = 0; i < pictureBox.Width; i++)
                {
                    for (int j = 0; j < pictureBox.Height; j++)
                    {
                        Color newColor = (int)bmp.GetPixel(i, j).R > (int)RminNum.Value && (int)bmp.GetPixel(i, j).R < (int)RmaxNum.Value && (int)bmp.GetPixel(i, j).G > (int)GminNum.Value && (int)bmp.GetPixel(i, j).G < (int)GmaxNum.Value && (int)bmp.GetPixel(i, j).B > (int)BminNum.Value && (int)bmp.GetPixel(i, j).B < (int)BmaxNum.Value ? Color.FromArgb((int)bmp.GetPixel(i, j).R, (int)bmp.GetPixel(i, j).G, (int)bmp.GetPixel(i, j).B) : Color.FromArgb(0, 0, 0);
                        bmp.SetPixel(i, j, newColor);
                    }
                }

                pictureBox.Image = bmp;
            }
        }

        private void SetWhiteMaskBtn_Click(object sender, EventArgs e)
        {
            if (pictureBox.Image != null)
                pictureBox.Image = CreateWhiteAndBlackImage(bitmapBuffer);
        }

        private void SearchObject_Click(object sender, EventArgs e)
        {
            listBox.Items.Clear();

            Bitmap bmpForCluster = new Bitmap(pictureBox.Image);

            bmpForCluster = CreateWhiteAndBlackImage(bmpForCluster);

            Random random = new Random();
            clusters = new List<Cluster>();

            int maxXx = pictureBox.Width;
            int maxYy = pictureBox.Height;

            for (int i = 0; i < (int)numericClusterCount.Value; i++)
            {
                Cluster clst = new Cluster();
                clst.center = new Point(random.Next(0, maxXx), random.Next(0, maxYy));
                clusters.Add(clst);
            }

            // Размещаем все доступные точки по кластерам
            for (int i = 0; i < pictureBox.Width; i++)
            {
                for (int j = 0; j < pictureBox.Height; j++)
                {
                    Color clr = bmpForCluster.GetPixel(i, j);

                    if (clr.R > 0 || clr.G > 0 || clr.B > 0)
                    {
                        double distance = double.MaxValue;
                        int index = -1;

                        // Определяем ближайший кластер для текущей точки
                        for (int num = 0; num < clusters.Count; num++)
                        {
                            Point pointPixel = new Point(i, j);

                            int xDiff = Math.Abs(pointPixel.X - clusters[num].center.X);
                            int yDiff = Math.Abs(pointPixel.Y - clusters[num].center.Y);

                            double currentDistance = Math.Sqrt(xDiff * xDiff + yDiff * yDiff);

                            if (currentDistance < distance)
                            {
                                distance = currentDistance;
                                index = num;
                            }
                        }

                        if (index != -1) // Проверяем, был ли найден ближайший кластер
                            clusters[index].points.Add(new Point(i, j));
                    }
                }
            }

            // Вычисляем новые центры для каждого кластера
            for (int i = 0; i < clusters.Count; i++)
            {
                if (clusters[i].points.Count > 0)
                {
                    int sumX = clusters[i].center.X;
                    int sumY = clusters[i].center.Y;

                    foreach (Point point in clusters[i].points)
                    {
                        sumX += point.X;
                        sumY += point.Y;
                    }

                    clusters[i].center = new System.Drawing.Point(sumX / (clusters[i].points.Count + 1), sumY / (clusters[i].points.Count + 1));
                }
            }


            for (int i = 0; i < clusters.Count; i++)
            {
                if (clusters[i].points.Count > 0)
                {
                    int minX = clusters[i].points[0].X;
                    int minY = clusters[i].points[0].Y;
                    int maxX = clusters[i].points[0].X;
                    int maxY = clusters[i].points[0].Y;

                    // Находим минимальные и максимальные координаты по осям X и Y
                    foreach (System.Drawing.Point point in clusters[i].points)
                    {
                        if (point.X < minX)
                            minX = point.X;
                        if (point.Y < minY)
                            minY = point.Y;
                        if (point.X > maxX)
                            maxX = point.X;
                        if (point.Y > maxY)
                            maxY = point.Y;
                    }

                    // Создаем прямоугольник на основе найденных координат
                    Rectangle boundingBox = new Rectangle(minX, minY, maxX - minX, maxY - minY);

                    double rad = Math.Sqrt(boundingBox.Width * boundingBox.Width + boundingBox.Height * boundingBox.Height) / 2;

                    if (rad > (double)numericRmin.Value && rad < (double)numericRmax.Value && CheckDensity(boundingBox, i, (double)numericDensity.Value, bmpForCluster))
                        listBox.Items.Add(i);
                }
            }
        }

        private bool CheckDensity(Rectangle rect, int index, double value, Bitmap bmpForCluster)
        {
            int square = rect.Width * rect.Height;

            int counter = 0;
            for (int i = rect.X; i < rect.Width + rect.X; i++)
            {
                for (int j = rect.Y; j < rect.Height + rect.Y; j++)
                {
                    Color clr = bmpForCluster.GetPixel(i, j);
                    if (clr.R > 0 && clr.G > 0 && clr.B > 0)
                        counter++;
                }
            }

            double currentDensity = (double)counter / (double)square;
            return currentDensity > value;
        }

        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            pictureBox.Image = bitmapBuffer;
            Bitmap nBmp = new Bitmap(pictureBox.Image);
            Graphics g = Graphics.FromImage(nBmp);

            if (clusters[listBox.SelectedIndex].points.Count > 0)
            {
                int minX = clusters[listBox.SelectedIndex].points[0].X;
                int minY = clusters[listBox.SelectedIndex].points[0].Y;
                int maxX = clusters[listBox.SelectedIndex].points[0].X;
                int maxY = clusters[listBox.SelectedIndex].points[0].Y;

                // Находим минимальные и максимальные координаты по осям X и Y
                foreach (Point point in clusters[listBox.SelectedIndex].points)
                {
                    if (point.X < minX)
                        minX = point.X;
                    if (point.Y < minY)
                        minY = point.Y;
                    if (point.X > maxX)
                        maxX = point.X;
                    if (point.Y > maxY)
                        maxY = point.Y;
                }

                // Создаем прямоугольник на основе найденных координат
                Rectangle boundingBox = new Rectangle(minX, minY, maxX - minX, maxY - minY);

                Image image = pictureBox.Image;
                Bitmap bitmap = new Bitmap(image);
                Bitmap croppedBitmap = bitmap.Clone(boundingBox, bitmap.PixelFormat);

                Bitmap bestImage = new Bitmap(pictureBox.Image);
                Bitmap bestMask = new Bitmap(pictureBox.Image);
                double bestSimilarity = 0;
                int bestIndex = 0;

                for (int i = 0; i < maskFiles.Length; i++)
                {
                    // Загружаем изображение, маску и шаблон
                    Bitmap mask = new Bitmap(maskFiles[i]);
                    Bitmap template = new Bitmap(templateFiles[i]);
                    croppedBitmap = ResizeBitmap(croppedBitmap, mask.Width, mask.Height);
                    template = ResizeBitmap(template, mask.Width, mask.Height);

                    // Конвертируем для сравнения
                    Bitmap croppedBitmapForSimilarity = new Bitmap(croppedBitmap);
                    croppedBitmapForSimilarity = CreateWhiteBlackForMask(croppedBitmapForSimilarity);

                    // Находим коэффициент сходства
                    double similarity = CheckSimilarity(croppedBitmapForSimilarity, mask);

                    if (similarity > bestSimilarity)
                    {
                        bestSimilarity = similarity;
                        bestIndex = i;
                        bestImage = croppedBitmap;
                        bestMask = mask;
                    }
                }

                if (bestSimilarity > (double)numericTrust.Value)
                {
                    pictureBox1.Image = bestImage;
                    pictureBox2.Image = bestMask;

                    objectBitmap = bestImage;
                    maskBitmap = bestMask;
                    encodIndex = bestIndex;
                }

                g.DrawRectangle(Pens.Red, boundingBox);
            }
            pictureBox.Image = nBmp;
        }

        private Bitmap CreateWhiteBlackForMask(Bitmap image)
        {
            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    Color newColor = (int)image.GetPixel(i, j).R > (int)RminNum.Value && (int)image.GetPixel(i, j).R < (int)RmaxNum.Value && (int)image.GetPixel(i, j).G > (int)GminNum.Value && (int)image.GetPixel(i, j).G < (int)GmaxNum.Value && (int)image.GetPixel(i, j).B > (int)BminNum.Value && (int)image.GetPixel(i, j).B < (int)BmaxNum.Value ? Color.FromArgb(255, 255, 255) : Color.FromArgb(0, 0, 0);
                    image.SetPixel(i, j, newColor);
                }
            }

            return image;
        }

        private void ColorPatternChoose_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ColorPatternChoose.SelectedIndex)
            {
                case 0: // red
                    RminNum.Value = 150;
                    RmaxNum.Value = 255;
                    GminNum.Value = 0;
                    GmaxNum.Value = 110;
                    BminNum.Value = 0;
                    BmaxNum.Value = 110;
                    break;

                case 1: // blue
                    RminNum.Value = 0;
                    RmaxNum.Value = 20;
                    GminNum.Value = 10;
                    GmaxNum.Value = 100;
                    BminNum.Value = 100;
                    BmaxNum.Value = 170;
                    break;

                case 2: // yellow
                    RminNum.Value = 170;
                    RmaxNum.Value = 255;
                    GminNum.Value = 170;
                    GmaxNum.Value = 255;
                    BminNum.Value = 50;
                    BmaxNum.Value = 100;
                    break;

                case 3: // black-white
                    RminNum.Value = 100;
                    RmaxNum.Value = 175;
                    GminNum.Value = 100;
                    GmaxNum.Value = 175;
                    BminNum.Value = 100;
                    BmaxNum.Value = 180;
                    break;
            }
        }

        private void EncodeObject_Click(object sender, EventArgs e)
        {
            objectBitmap = CreateWhiteBlackForMask(objectBitmap);
            Bitmap template = new Bitmap(templateFiles[encodIndex]);
            Bitmap mask = new Bitmap(maskFiles[encodIndex]);

            int trustPixels = 0;

            for (int x = 0; x < objectBitmap.Width; x++)
            {
                for (int y = 0; y < objectBitmap.Height; y++)
                {
                    var clrObj = objectBitmap.GetPixel(x, y);
                    var clrMask = mask.GetPixel(x, y);
                    var clrTemp = template.GetPixel(x, y);

                    if (clrMask.R == 255 && clrObj.R == 255)
                        objectBitmap.SetPixel(x, y, Color.FromArgb(52, 201, 36)); // Красим в светло-зеленый
                    else if (clrMask.R == 0 && clrObj.R == 0)
                        objectBitmap.SetPixel(x, y, Color.FromArgb(10, 95, 56)); // Красим в темно-зеленый
                    else if (clrMask.R > 127 && clrObj.R < 127)
                        objectBitmap.SetPixel(x, y, Color.FromArgb(139, 0, 0)); // Красим в темно-красный
                    else if (clrMask.R < 127 && clrObj.R > 127)
                        objectBitmap.SetPixel(x, y, Color.FromArgb(230, 50, 68)); // Красим в светло-красный

                    if (clrTemp.R < 127 && clrObj.R < 127)
                        objectBitmap.SetPixel(x, y, Color.FromArgb(0, 83, 138)); // Красим в темно-синий
                    else if (clrTemp.R < 127 && clrObj.R > 127)
                        objectBitmap.SetPixel(x, y, Color.FromArgb(30, 144, 255)); // Красим в светло-синий

                    if ((clrMask.R == 255 && clrObj.R == 255) || (clrMask.R == 0 && clrObj.R == 0))
                        trustPixels++; // Подсчет уровня доверия
                }
            }

            pictureBox1.Image = objectBitmap;

            int desiredPixels = 0;

            for (int x = 0; x < template.Width; x++)
            {
                for (int y = 0; y < template.Height; y++)
                {
                    var clrTemp = template.GetPixel(x, y);
                    if (clrTemp.R > 127)
                        desiredPixels++;
                }
            }

            double trust = (double)trustPixels / (double)desiredPixels * 100;

            MessageBox.Show($"{(int)trust}%", "Trust result");
        }
    }
}
