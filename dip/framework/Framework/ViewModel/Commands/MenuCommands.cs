﻿using Emgu.CV;
using Emgu.CV.Structure;

using System.Windows;
using System.Drawing.Imaging;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Controls;

using Framework.View;
using static Framework.Utilities.DataProvider;
using static Framework.Utilities.FileHelper;
using static Framework.Utilities.DrawingHelper;
using static Framework.Converters.ImageConverter;

using Algorithms.Tools;
using Algorithms.Utilities;
using Framework.Utilities;
using System.Collections.Generic;
using Algorithms.Sections;

namespace Framework.ViewModel
{
    public class MenuCommands : BaseVM
    {
        private readonly MainVM _mainVM;

        public MenuCommands(MainVM mainVM)
        {
            _mainVM = mainVM;
        }

        #region Private properties

        private ImageSource InitialImage
        {
            get => _mainVM.InitialImage;
            set => _mainVM.InitialImage = value;
        }

        private ImageSource ProcessedImage
        {
            get => _mainVM.ProcessedImage;
            set => _mainVM.ProcessedImage = value;
        }

        private double ScaleValue
        {
            get => _mainVM.ScaleValue;
            set => _mainVM.ScaleValue = value;
        }

        #endregion

        #region File

        #region Load grayscale image
        private RelayCommand _loadGrayImageCommand;
        public RelayCommand LoadGrayImageCommand
        {
            get
            {
                if (_loadGrayImageCommand == null)
                    _loadGrayImageCommand = new RelayCommand(LoadGrayImage);
                return _loadGrayImageCommand;
            }
        }

        private void LoadGrayImage(object parameter)
        {
            Clear(parameter);

            string fileName = LoadFileDialog("Select a gray picture");
            if (fileName != null)
            {
                GrayInitialImage = new Image<Gray, byte>(fileName);
                InitialImage = Convert(GrayInitialImage);
            }
        }
        #endregion

        #region Load color image
        private ICommand _loadColorImageCommand;
        public ICommand LoadColorImageCommand
        {
            get
            {
                if (_loadColorImageCommand == null)
                    _loadColorImageCommand = new RelayCommand(LoadColorImage);
                return _loadColorImageCommand;
            }
        }

        private void LoadColorImage(object parameter)
        {
            Clear(parameter);

            string fileName = LoadFileDialog("Select a color picture");
            if (fileName != null)
            {
                ColorInitialImage = new Image<Bgr, byte>(fileName);
                InitialImage = Convert(ColorInitialImage);
            }
        }
        #endregion

        #region Save processed image
        private ICommand _saveProcessedImageCommand;
        public ICommand SaveProcessedImageCommand
        {
            get
            {
                if (_saveProcessedImageCommand == null)
                    _saveProcessedImageCommand = new RelayCommand(SaveProcessedImage);
                return _saveProcessedImageCommand;
            }
        }

        private void SaveProcessedImage(object parameter)
        {
            if (GrayProcessedImage == null && ColorProcessedImage == null)
            {
                MessageBox.Show("If you want to save your processed image, " +
                    "please load and process an image first!");
                return;
            }

            string imagePath = SaveFileDialog("image.jpg");
            if (imagePath != null)
            {
                GrayProcessedImage?.Bitmap.Save(imagePath, GetJpegCodec("image/jpeg"), GetEncoderParameter(Encoder.Quality, 100));
                ColorProcessedImage?.Bitmap.Save(imagePath, GetJpegCodec("image/jpeg"), GetEncoderParameter(Encoder.Quality, 100));
                OpenImage(imagePath);
            }
        }
        #endregion

        #region Save both images
        private ICommand _saveImagesCommand;
        public ICommand SaveImagesCommand
        {
            get
            {
                if (_saveImagesCommand == null)
                    _saveImagesCommand = new RelayCommand(SaveImages);
                return _saveImagesCommand;
            }
        }

        private void SaveImages(object parameter)
        {
            if (GrayInitialImage == null && ColorInitialImage == null)
            {
                MessageBox.Show("If you want to save both images, " +
                    "please load and process an image first!");
                return;
            }

            if (GrayProcessedImage == null && ColorProcessedImage == null)
            {
                MessageBox.Show("If you want to save both images, " +
                    "please process your image first!");
                return;
            }

            string imagePath = SaveFileDialog("image.jpg");
            if (imagePath != null)
            {
                IImage processedImage = null;
                if (GrayInitialImage != null && GrayProcessedImage != null)
                    processedImage = Utils.Combine(GrayInitialImage, GrayProcessedImage);

                if (GrayInitialImage != null && ColorProcessedImage != null)
                    processedImage = Utils.Combine(GrayInitialImage, ColorProcessedImage);

                if (ColorInitialImage != null && GrayProcessedImage != null)
                    processedImage = Utils.Combine(ColorInitialImage, GrayProcessedImage);

                if (ColorInitialImage != null && ColorProcessedImage != null)
                    processedImage = Utils.Combine(ColorInitialImage, ColorProcessedImage);

                processedImage?.Bitmap.Save(imagePath, GetJpegCodec("image/jpeg"), GetEncoderParameter(Encoder.Quality, 100));
                OpenImage(imagePath);
            }
        }
        #endregion

        #region Exit
        private ICommand _exitCommand;
        public ICommand ExitCommand
        {
            get
            {
                if (_exitCommand == null)
                    _exitCommand = new RelayCommand(Exit);
                return _exitCommand;
            }
        }

        private void Exit(object parameter)
        {
            CloseWindows();
            System.Environment.Exit(0);
        }
        #endregion

        #endregion

        #region Edit

        #region Remove drawn shapes from initial canvas
        private ICommand _removeInitialDrawnShapesCommand;
        public ICommand RemoveInitialDrawnShapesCommand
        {
            get
            {
                if (_removeInitialDrawnShapesCommand == null)
                    _removeInitialDrawnShapesCommand = new RelayCommand(RemoveInitialDrawnShapes);
                return _removeInitialDrawnShapesCommand;
            }
        }

        private void RemoveInitialDrawnShapes(object parameter)
        {
            RemoveUiElements(parameter as Canvas);
        }
        #endregion

        #region Remove drawn shapes from processed canvas
        private ICommand _removeProcessedDrawnShapesCommand;
        public ICommand RemoveProcessedDrawnShapesCommand
        {
            get
            {
                if (_removeProcessedDrawnShapesCommand == null)
                    _removeProcessedDrawnShapesCommand = new RelayCommand(RemoveProcessedDrawnShapes);
                return _removeProcessedDrawnShapesCommand;
            }
        }

        private void RemoveProcessedDrawnShapes(object parameter)
        {
            RemoveUiElements(parameter as Canvas);
        }
        #endregion

        #region Remove drawn shapes from both canvases
        private ICommand _removeDrawnShapesCommand;
        public ICommand RemoveDrawnShapesCommand
        {
            get
            {
                if (_removeDrawnShapesCommand == null)
                    _removeDrawnShapesCommand = new RelayCommand(RemoveDrawnShapes);
                return _removeDrawnShapesCommand;
            }
        }

        private void RemoveDrawnShapes(object parameter)
        {
            var canvases = (object[])parameter;
            RemoveUiElements(canvases[0] as Canvas);
            RemoveUiElements(canvases[1] as Canvas);
        }
        #endregion

        #region Clear initial canvas
        private ICommand _clearInitialCanvasCommand;
        public ICommand ClearInitialCanvasCommand
        {
            get
            {
                if (_clearInitialCanvasCommand == null)
                    _clearInitialCanvasCommand = new RelayCommand(ClearInitialCanvas);
                return _clearInitialCanvasCommand;
            }
        }

        private void ClearInitialCanvas(object parameter)
        {
            RemoveUiElements(parameter as Canvas);

            GrayInitialImage = null;
            ColorInitialImage = null;
            InitialImage = null;
        }
        #endregion

        #region Clear processed canvas
        private ICommand _clearProcessedCanvasCommand;
        public ICommand ClearProcessedCanvasCommand
        {
            get
            {
                if (_clearProcessedCanvasCommand == null)
                    _clearProcessedCanvasCommand = new RelayCommand(ClearProcessedCanvas);
                return _clearProcessedCanvasCommand;
            }
        }

        private void ClearProcessedCanvas(object parameter)
        {
            RemoveUiElements(parameter as Canvas);

            GrayProcessedImage = null;
            ColorProcessedImage = null;
            ProcessedImage = null;
        }
        #endregion

        #region Closing all open windows and clear both canvases
        private ICommand _clearCommand;
        public ICommand ClearCommand
        {
            get
            {
                if (_clearCommand == null)
                    _clearCommand = new RelayCommand(Clear);
                return _clearCommand;
            }
        }

        private void Clear(object parameter)
        {
            CloseWindows();

            ScaleValue = 1;

            var canvases = (object[])parameter;
            ClearInitialCanvas(canvases[0] as Canvas);
            ClearProcessedCanvas(canvases[1] as Canvas);
        }
        #endregion

        #endregion

        #region Tools

        #region Magnifier
        private ICommand _magnifierCommand;
        public ICommand MagnifierCommand
        {
            get
            {
                if (_magnifierCommand == null)
                    _magnifierCommand = new RelayCommand(Magnifier);
                return _magnifierCommand;
            }
        }

        private void Magnifier(object parameter)
        {
            if (MagnifierOn == true) return;
            if (VectorOfMousePosition.Count == 0)
            {
                MessageBox.Show("Please select an area first.");
                return;
            }

            MagnifierWindow magnifierWindow = new MagnifierWindow();
            magnifierWindow.Show();
        }
        #endregion

        #region Display Gray/Color levels

        #region On row
        private ICommand _displayLevelsOnRowCommand;
        public ICommand DisplayLevelsOnRowCommand
        {
            get
            {
                if (_displayLevelsOnRowCommand == null)
                    _displayLevelsOnRowCommand = new RelayCommand(DisplayLevelsOnRow);
                return _displayLevelsOnRowCommand;
            }
        }

        private void DisplayLevelsOnRow(object parameter)
        {
            if (RowColorLevelsOn == true) return;
            if (VectorOfMousePosition.Count == 0)
            {
                MessageBox.Show("Please select an area first.");
                return;
            }

            ColorLevelsWindow window = new ColorLevelsWindow(_mainVM, CLevelsType.Row);
            window.Show();
        }
        #endregion

        #region On column
        private ICommand _displayLevelsOnColumnCommand;
        public ICommand DisplayLevelsOnColumnCommand
        {
            get
            {
                if (_displayLevelsOnColumnCommand == null)
                    _displayLevelsOnColumnCommand = new RelayCommand(DisplayLevelsOnColumn);
                return _displayLevelsOnColumnCommand;
            }
        }

        private void DisplayLevelsOnColumn(object parameter)
        {
            if (ColumnColorLevelsOn == true) return;
            if (VectorOfMousePosition.Count == 0)
            {
                MessageBox.Show("Please select an area first.");
                return;
            }

            ColorLevelsWindow window = new ColorLevelsWindow(_mainVM, CLevelsType.Column);
            window.Show();
        }
        #endregion

        #endregion

        #region Visualize image histogram

        #region Initial image histogram
        private ICommand _histogramInitialImageCommand;
        public ICommand HistogramInitialImageCommand
        {
            get
            {
                if (_histogramInitialImageCommand == null)
                    _histogramInitialImageCommand = new RelayCommand(HistogramInitialImage);
                return _histogramInitialImageCommand;
            }
        }

        private void HistogramInitialImage(object parameter)
        {
            if (InitialHistogramOn == true) return;
            if (InitialImage == null)
            {
                MessageBox.Show("Please add an image !");
                return;
            }

            HistogramWindow window = null;

            if (ColorInitialImage != null)
            {
                window = new HistogramWindow(_mainVM, ImageType.InitialColor);
            }
            else if (GrayInitialImage != null)
            {
                window = new HistogramWindow(_mainVM, ImageType.InitialGray);
            }

            window.Show();
        }
        #endregion

        #region Processed image histogram
        private ICommand _histogramProcessedImageCommand;
        public ICommand HistogramProcessedImageCommand
        {
            get
            {
                if (_histogramProcessedImageCommand == null)
                    _histogramProcessedImageCommand = new RelayCommand(HistogramProcessedImage);
                return _histogramProcessedImageCommand;
            }
        }

        private void HistogramProcessedImage(object parameter)
        {
            if (ProcessedHistogramOn == true) return;
            if (ProcessedImage == null)
            {
                MessageBox.Show("Please process an image !");
                return;
            }

            HistogramWindow window = null;

            if (ColorProcessedImage != null)
            {
                window = new HistogramWindow(_mainVM, ImageType.ProcessedColor);
            }
            else if (GrayProcessedImage != null)
            {
                window = new HistogramWindow(_mainVM, ImageType.ProcessedGray);
            }

            window.Show();
        }
        #endregion

        #endregion


        #region Copy image
        private ICommand _copyImageCommand;
        public ICommand CopyImageCommand
        {
            get
            {
                if (_copyImageCommand == null)
                    _copyImageCommand = new RelayCommand(CopyImage);
                return _copyImageCommand;
            }
        }

        private void CopyImage(object parameter)
        {
            if (InitialImage == null)
            {
                MessageBox.Show("Please add an image !");
                return;
            }

            ClearProcessedCanvas(parameter);

            if (ColorInitialImage != null)
            {
                ColorProcessedImage = Tools.Copy(ColorInitialImage);
                ProcessedImage = Convert(ColorProcessedImage);
            }
            else if (GrayInitialImage != null)
            {
                GrayProcessedImage = Tools.Copy(GrayInitialImage);
                ProcessedImage = Convert(GrayProcessedImage);
            }
        }
        #endregion

        #region Invert image
        private ICommand _invertImageCommand;
        public ICommand InvertImageCommand
        {
            get
            {
                if (_invertImageCommand == null)
                    _invertImageCommand = new RelayCommand(InvertImage);
                return _invertImageCommand;
            }
        }

        private void InvertImage(object parameter)
        {
            if (InitialImage == null)
            {
                MessageBox.Show("Please add an image !");
                return;
            }

            ClearProcessedCanvas(parameter);

            if (GrayInitialImage != null)
            {
                GrayProcessedImage = Tools.Invert(GrayInitialImage);
                ProcessedImage = Convert(GrayProcessedImage);
            }
            else if (ColorInitialImage != null)
            {
                ColorProcessedImage = Tools.Invert(ColorInitialImage);
                ProcessedImage = Convert(ColorProcessedImage);
            }
        }
        #endregion

        #region Convert color image to grayscale image
        private ICommand _convertImageToGrayscaleCommand;
        public ICommand ConvertImageToGrayscaleCommand
        {
            get
            {
                if (_convertImageToGrayscaleCommand == null)
                    _convertImageToGrayscaleCommand = new RelayCommand(ConvertImageToGrayscale);
                return _convertImageToGrayscaleCommand;
            }
        }

        private void ConvertImageToGrayscale(object parameter)
        {
            if (InitialImage == null)
            {
                MessageBox.Show("Please add an image !");
                return;
            }

            ClearProcessedCanvas(parameter);

            if (ColorInitialImage != null)
            {
                GrayProcessedImage = Tools.Convert(ColorInitialImage);
                ProcessedImage = Convert(GrayProcessedImage);
            }
            else
            {
                MessageBox.Show("It is possible to convert only color images !");
            }
        }
        #endregion

        #region Thresholding

        private ICommand _thresholdingCommand;

        public ICommand ThresholdingCommand
        {
            get
            {
                if (_thresholdingCommand == null)
                {
                    _thresholdingCommand = new RelayCommand(ThresholdingProcedure);
                }
                return _thresholdingCommand;
            }
        }

        private void ThresholdingProcedure(object parameter)
        {
            byte? threshold = TestThresholdingProcedure();
            if (threshold == null)
            {
                return;
            }

            ClearProcessedCanvas(parameter);

            if (GrayInitialImage != null)
            {
                GrayProcessedImage = Tools.Thresholding(GrayInitialImage, threshold.Value);
            }
            else
            {
                GrayProcessedImage = Tools.Thresholding(ColorInitialImage, threshold.Value);
            }
            ProcessedImage = Convert(GrayProcessedImage);
        }

        #endregion

        #region Crop

        private ICommand _cropCommand;

        public ICommand CropCommand
        {
            get
            {
                if (_cropCommand == null)
                {
                    _cropCommand = new RelayCommand(CropProcedure);
                }
                return _cropCommand;
            }
        }

        private void CropProcedure(object parameter)
        {
            int? clicks = TestCropProcedure();
            if (clicks == null)
            {
                return;
            }

            object[] canvases = (object[])parameter;
            ClearProcessedCanvas(canvases[1]);

            Point topLeft = VectorOfMousePosition[clicks.Value - 2];
            Point bottomRight = VectorOfMousePosition[clicks.Value - 1];

            Utils.ToTopLeftBottomRight(ref topLeft, ref bottomRight);

            DrawRectangle(canvases[0] as Canvas, topLeft,
               bottomRight, 2, Brushes.Red, ScaleValue);

            if (GrayInitialImage != null)
            {
                GrayProcessedImage = Tools.Crop(GrayInitialImage, topLeft, bottomRight);
                ProcessedImage = Convert(GrayProcessedImage);
                return;
            }
            if (ColorInitialImage != null)
            {
                ColorProcessedImage = Tools.Crop(ColorInitialImage, topLeft, bottomRight);
                ProcessedImage = Convert(ColorProcessedImage);
            }
        }

        #endregion

        #region Vertical mirror

        private ICommand _verticalMirrorCommand;

        public ICommand VerticalMirrorCommand
        {
            get
            {
                if (_verticalMirrorCommand == null)
                {
                    _verticalMirrorCommand = new RelayCommand(VerticalMirrorProcedure);
                }
                return _verticalMirrorCommand;
            }
        }

        private void VerticalMirrorProcedure(object parameter)
        {
            if (InitialImageMissing())
            {
                return;
            }

            ClearProcessedCanvas(parameter);

            if (GrayInitialImage != null)
            {
                GrayProcessedImage = Tools.VerticalMirror(GrayInitialImage);
                ProcessedImage = Convert(GrayProcessedImage);
                return;
            }
            if (ColorInitialImage != null)
            {
                ColorProcessedImage = Tools.VerticalMirror(ColorInitialImage);
                ProcessedImage = Convert(ColorProcessedImage);
            }
        }

        #endregion

        #region Rotate by 90 degrees

        #region Clockwise

        private ICommand _rotateBy90DegreesClockwiseCommand;

        public ICommand RotateBy90DegreesClockwiseCommand
        {
            get
            {
                if (_rotateBy90DegreesClockwiseCommand == null)
                {
                    _rotateBy90DegreesClockwiseCommand = new RelayCommand(RotateBy90DegreesClockwiseProcedure);
                }
                return _rotateBy90DegreesClockwiseCommand;
            }
        }

        private void RotateBy90DegreesClockwiseProcedure(object parameter)
        {
            if (InitialImageMissing())
            {
                return;
            }

            ClearProcessedCanvas(parameter);

            if (GrayInitialImage != null)
            {
                GrayProcessedImage = Tools.RotateBy90Degrees(GrayInitialImage);
                ProcessedImage = Convert(GrayProcessedImage);
                return;
            }
            if (ColorInitialImage != null)
            {
                ColorProcessedImage = Tools.RotateBy90Degrees(ColorInitialImage);
                ProcessedImage = Convert(ColorProcessedImage);
            }
        }

        #endregion

        #region Anti-clockwise

        private ICommand _rotateBy90DegreesAnticlockwiseCommand;

        public ICommand RotateBy90DegreesAnticlockwiseCommand
        {
            get
            {
                if (_rotateBy90DegreesAnticlockwiseCommand == null)
                {
                    _rotateBy90DegreesAnticlockwiseCommand = new RelayCommand(RotateBy90DegreesAnticlockwiseProcedure);
                }
                return _rotateBy90DegreesAnticlockwiseCommand;
            }
        }

        private void RotateBy90DegreesAnticlockwiseProcedure(object parameter)
        {
            if (InitialImageMissing())
            {
                return;
            }

            ClearProcessedCanvas(parameter);

            if (GrayInitialImage != null)
            {
                GrayProcessedImage = Tools.RotateBy90Degrees(GrayInitialImage, false);
                ProcessedImage = Convert(GrayProcessedImage);
                return;
            }
            if (ColorInitialImage != null)
            {
                ColorProcessedImage = Tools.RotateBy90Degrees(ColorInitialImage, false);
                ProcessedImage = Convert(ColorProcessedImage);
            }
        }

        #endregion

        #endregion

        #endregion

        #region Pointwise operations

        #region Modify the brightness

        private ICommand _brightnessCommand;

        public ICommand BrightnessCommand
        {
            get
            {
                if (_brightnessCommand == null)
                {
                    _brightnessCommand = new RelayCommand(BrightnessProcedure);
                }
                return _brightnessCommand;
            }
        }

        private void BrightnessProcedure(object parameter)
        {
            if (InitialImageMissing())
            {
                return;
            }

            ClearProcessedCanvas(parameter);

            if (GrayInitialImage != null)
            {
                GrayProcessedImage = PointwiseOperations.ModifyBrightnessUsingAdditionOperation(GrayInitialImage, 80);
                ProcessedImage = Convert(GrayProcessedImage);
                return;
            }
            if (ColorInitialImage != null)
            {
                //ColorProcessedImage = Tools.RotateBy90Degrees(ColorInitialImage);
                //ProcessedImage = Convert(ColorProcessedImage);
            }
        }

        #endregion

        #region Modify the contrast

        private ICommand _contrastCommand;

        public ICommand ContrastCommand
        {
            get
            {
                if (_contrastCommand == null)
                {
                    _contrastCommand = new RelayCommand(ContrastProcedure);
                }
                return _contrastCommand;
            }
        }

        private void ContrastProcedure(object parameter)
        {
            if (InitialImageMissing())
            {
                return;
            }

            List<string> labels = new List<string>
            {
                "i1","o1","i2","o2"
            };

            DialogBox dialogBox = new DialogBox(_mainVM, labels);
            dialogBox.ShowDialog();

            List<string> values = dialogBox.Values;

            byte i1 = byte.Parse(values[0]);
            byte o1 = byte.Parse(values[1]);
            byte i2 = byte.Parse(values[2]);
            byte o2 = byte.Parse(values[3]);

            ClearProcessedCanvas(parameter);

            if (GrayInitialImage != null)
            {
                GrayProcessedImage = PointwiseOperations.ModifyContrast(GrayInitialImage, i1, o1, i2, o2);
                ProcessedImage = Convert(GrayProcessedImage);
                return;
            }
            if (ColorInitialImage != null)
            {
                //ColorProcessedImage = Tools.RotateBy90Degrees(ColorInitialImage);
                //ProcessedImage = Convert(ColorProcessedImage);
            }
        }

        #endregion

        #endregion

        #region Thresholding
        #endregion

        #region Filters
        #endregion

        #region Morphological operations
        #endregion

        #region Geometric transformations
        #endregion

        #region Segmentation
        #endregion

        #region Save processed image as original image
        private ICommand _saveAsOriginalImageCommand;
        public ICommand SaveAsOriginalImageCommand
        {
            get
            {
                if (_saveAsOriginalImageCommand == null)
                    _saveAsOriginalImageCommand = new RelayCommand(SaveAsOriginalImage);
                return _saveAsOriginalImageCommand;
            }
        }

        private void SaveAsOriginalImage(object parameter)
        {
            if (ProcessedImage == null)
            {
                MessageBox.Show("Please process an image first.");
                return;
            }

            var canvases = (object[])parameter;

            ClearInitialCanvas(canvases[0] as Canvas);

            if (GrayProcessedImage != null)
            {
                GrayInitialImage = GrayProcessedImage;
                InitialImage = Convert(GrayInitialImage);
            }
            else if (ColorProcessedImage != null)
            {
                ColorInitialImage = ColorProcessedImage;
                InitialImage = Convert(ColorInitialImage);
            }

            ClearProcessedCanvas(canvases[1] as Canvas);
        }
        #endregion

        #region Tests

        #region Validators

        private bool InitialImageMissing()
        {
            if (InitialImage == null)
            {
                MessageBox.Show(Constant.Message.NullInitialImage);
                return true;
            }
            return false;
        }

        #endregion

        #region Procedures tests

        private byte? TestThresholdingProcedure()
        {
            if (InitialImageMissing())
            {
                return null;
            }

            List<string> labels = new List<string>
            {
                "Threshold"
            };

            DialogBox dialogBox = new DialogBox(_mainVM, labels);
            bool? dialogResult = dialogBox.ShowDialog();
            if (!dialogResult.HasValue || dialogResult.Value == false)
            {
                return null;
            }

            List<string> values = dialogBox.Values;
            if (values.Count != dialogBox.AmountOfNonemptyValues)
            {
                MessageBox.Show(Constant.Message.ArgumentsMissingInDialogBox);
                return null;
            }

            byte threshold;
            if (!byte.TryParse(values[0], out threshold))
            {
                MessageBox.Show(Constant.Message.ParsingStringToByte);
                return null;
            }
            if (threshold < Constant.Number.ThresholdLeftBound ||
                threshold > Constant.Number.ThresholdRightBound)
            {
                MessageBox.Show(Constant.Message.ByteOutOfBounds(Constant.Number.ThresholdLeftBound,
                    Constant.Number.ThresholdRightBound));
                return null;
            }

            return threshold;
        }

        private int? TestCropProcedure()
        {
            if (InitialImageMissing())
            {
                return null;
            }
            int clicks = VectorOfMousePosition.Count;
            if (clicks < 2)
            {
                MessageBox.Show(Constant.Message.NoAreaSelected);
                return null;
            }
            return clicks;
        }

        #endregion

        #endregion
    }
}