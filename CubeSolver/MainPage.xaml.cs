﻿/* Program .....: CubeSolver.sln
 * Display name : Cube Solver gg
 * Author ......: Geert Geerits - E-mail: geertgeerits@gmail.com
 * Copyright ...: (C) 1981-2025
 * Version .....: 2.0.42
 * Date ........: 2025-07-23 (YYYY-MM-DD)
 * Language ....: Microsoft Visual Studio 2022: .NET MAUI 9 - C# 13.0
 * Description .: Solving the Cube
 * Note ........: This program is based on the program 'SolCube' I wrote in 1981 in MS Basic-80 for the Commodore PET 2001
 * Dependencies : None
 * Thanks to ...: Gerald Versluis - https://www.youtube.com/@jfversluis
 *                Herbert Kociemba - https://kociemba.org/cube.htm
 *                Matt Colbourne - https://github.com/Megalomatt/Kociemba */

using Microsoft.Maui.Controls.Shapes;

// Workaround for !!!BUG!!! in iOS.NET9.0: if CharacterSpacing is set, the text of the Button is not updaded
#if IOS
using Microsoft.Maui.Platform;
#endif

namespace CubeSolver
{
    public sealed partial class MainPage : ContentPage
    {
        //// Local variables
        private bool bColorDrop;
        private bool bSolvingCube;
        private bool bSolvingCubeStop;
        private bool bSolved;
        private bool bTestSolveCube;
        private bool bTurnIsBackwards;
        private bool bTurnContinuously;
        private bool bKociembaTablesExist;
        private int nDurationFirstKociembaSolve = 60;
        private string cCurrentTurn = string.Empty;

        //// Array with cube turns for the cube scramble generator
        private readonly string[] ScrambledCubeTurns = [
            Globals.turnFrontCW, Globals.turnFrontCCW, Globals.turnFront2,
            Globals.turnRightCW, Globals.turnRightCCW, Globals.turnRight2,
            Globals.turnBackCW, Globals.turnBackCCW, Globals.turnBack2,
            Globals.turnLeftCW, Globals.turnLeftCCW, Globals.turnLeft2,
            Globals.turnUpCW, Globals.turnUpCCW, Globals.turnUp2,
            Globals.turnDownCW, Globals.turnDownCCW, Globals.turnDown2 ];

        //// Initialize the _buttonPressed field with a new instance of TaskCompletionSource<bool>,
        //   which can be used to create and control the lifecycle of a task that will eventually complete with a boolean result
        private TaskCompletionSource<bool> _buttonPressed = new();

        public MainPage()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                DisplayAlert("InitializeComponent: MainPage", ex.Message, "OK");
                return;
            }
#if WINDOWS
            //// Set the margins for the controls in the title bar for Windows if using the Shell
            imgbtnAbout.Margin = new Thickness(20, 0, 0, 0);
            lblTitlePage.Margin = new Thickness(16, 10, 0, 0);
#endif
#if IOS
            //// !!!BUG!!!? in iOS - Set the margin for the label 'lblExplainTurnCube' because Padding does not work in iOS
            lblExplainTurnCube.Margin = new Thickness(5, 0, 5, 0);

            //// Set the scale of the activity indicator for iOS - If the scale is set in the xaml page then the app runs slower on Android and Windows while the activity indicator is running
            // Copilot Chat: Yes, it is possible that setting the scale of an activity indicator in the XAML page can cause performance issues on different platforms like Android and Windows
            // This can happen due to the way the UI elements are rendered and handled by the platform-specific implementations
            // To avoid this issue, you can set the scale of the activity indicator programmatically in the code-behind file
            activityIndicator.Scale = 2;
#endif
            //// Get the saved settings
            Globals.cTheme = Preferences.Default.Get("SettingTheme", "System");
            Globals.nFontSize = Preferences.Default.Get("SettingFontSize", 18d);
            Globals.cLanguage = Preferences.Default.Get("SettingLanguage", "");
            Globals.cLanguageSpeech = Preferences.Default.Get("SettingLanguageSpeech", "");
            Globals.bExplainText = Preferences.Default.Get("SettingExplainText", false);
            Globals.bExplainSpeech = Preferences.Default.Get("SettingExplainSpeech", false);    //                      Original    Alternative
            Globals.aFaceColors[1] = Preferences.Default.Get("SettingCubeColor1", "#FF4F4F");   // Front face: Red      FF0000      FF4F4F
            Globals.aFaceColors[2] = Preferences.Default.Get("SettingCubeColor2", "#00AAFF");   // Right face: Blue     0000FF      00AAFF
            Globals.aFaceColors[3] = Preferences.Default.Get("SettingCubeColor3", "#FFB300");   // Back face: Orange    FF8000      FFB300
            Globals.aFaceColors[4] = Preferences.Default.Get("SettingCubeColor4", "#00EA00");   // Left face: Green     008000      00EA00
            Globals.aFaceColors[5] = Preferences.Default.Get("SettingCubeColor5", "#FAFAFA");   // Up face: White       FFFFFF      FAFAFA
            Globals.aFaceColors[6] = Preferences.Default.Get("SettingCubeColor6", "#FFFF40");   // Down face: Yellow    FFFF00      FFFF40
            Globals.bKociembaSolution = Preferences.Default.Get("SettingKociembaSolution", true);
            Globals.bLicense = Preferences.Default.Get("SettingLicense", false);

            //// Set the theme
            Globals.SetTheme();

            //// Get and set the user interface language after a first start or reset of the application
            // For testing the languages
            //Globals.cLanguage = "id";
            //Globals.cLanguageSpeech = "id-ID";
            //Globals.SetCultureSelectedLanguage("id-ID");
            //Globals.cLanguage = "ar";
            //Globals.SetCultureSelectedLanguage("ar-SA");
            try
            {
                if (string.IsNullOrEmpty(Globals.cLanguage))
                {
                    Globals.cLanguage = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;

                    // Chinese needs the language code as zh-CN and zh-TW
                    if (Globals.cLanguage == "zh")
                    {
                        Globals.cLanguage = Thread.CurrentThread.CurrentUICulture.Name;
                    }
                }
            }
            catch (Exception)
            {
                Globals.cLanguage = "en";
            }
            finally
            {
                Preferences.Default.Set("SettingLanguage", Globals.cLanguage);
                Debug.WriteLine("Globals.cLanguage: " + Globals.cLanguage);
            }

            // Set the text language
            _ = SetTextLanguageAsync();

            //// Initialize text to speech
            _ = InitializeTextToSpeechAsync();

            //// Reset the colors of the cube
            ClassColorsCube.ResetCube();
            GetCubeColorsFromArrays();
#if DEBUG
            //// Set the button to true and 'bSolveNewSolutionsTest' to false in debug mode for testing purposes
            //btnSolveNewSolutionsTest.IsVisible = false;
            //Globals.bSolveNewSolutionsTest = false;

            //// For testing the logging of the executed line in the class 'ClassProgramLogging.cs'
            //ClassProgramLogging.LogExecutedLineTest();
#endif
            // Herbert Kociemba solution (for testing)
            //string searchString = "UUUUUULLLURRURRURRFFFFFFFFFRRRDDDDDDLLDLLDLLDBBBBBBBBB";
            //string searchString = ClassCubeKociemba.ConvertCubeToKociembaCube();
            //Debug.WriteLine("searchString: " + searchString);
            //string info = "";
            //string solution = "";
            //solution = SearchRunTime.solution(searchString, out info, buildTables: true);
            ////string solution = Search.solution(searchString, out info);
            //Debug.WriteLine("Search.solution: " + solution);
        }

        /// <summary>
        /// Initialize text to speech and get and set the speech language
        /// Must be called in the constructor of the MainPage and not in the ClassSpeech.cs
        /// The InitializeTextToSpeechAsync method is called asynchronously after the UI components are initialized
        /// Once the asynchronous operation completes, the Globals.bTextToSpeechAvailable value is checked, and the UI is updated accordingly
        /// </summary>
        private static async Task InitializeTextToSpeechAsync()
        {
            // Search for the speech language after a first start or reset of the application
            try
            {
                if (string.IsNullOrEmpty(Globals.cLanguageSpeech))
                {
                    Globals.cLanguageSpeech = Thread.CurrentThread.CurrentUICulture.Name;
                }
            }
            catch (Exception)
            {
                Globals.cLanguageSpeech = "en-US";
            }
            finally
            {
                Globals.bTextToSpeechAvailable = await ClassSpeech.InitializeTextToSpeechAsync();
                ClassSpeech.SearchArrayWithSpeechLanguages(Globals.cLanguageSpeech);
                Preferences.Default.Set("SettingLanguageSpeech", Globals.cLanguageSpeech);
                Debug.WriteLine("Globals.cLanguageSpeech: " + Globals.cLanguageSpeech);
            }

            if (!Globals.bTextToSpeechAvailable)
            {
                Globals.bExplainSpeech = false;
            }

            Debug.WriteLine("MainPage - Globals.bTextToSpeechAvailable: " + Globals.bTextToSpeechAvailable);
            Debug.WriteLine("MainPage - Globals.cLanguageSpeech: " + Globals.cLanguageSpeech);
        }

        //// TitleView buttons clicked events
        /// <summary>
        /// Go to the About page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnPageAboutClicked(object sender, EventArgs e)
        {
            // Cancel text to speech
            ClassSpeech.CancelTextToSpeech();

            await Navigation.PushAsync(new PageAbout());
        }

//        /// <summary>
//        /// Use the toggle button 'btnSolveNewSolutionsTest' to solve the cube with or without new test turns in debug mode
//        /// </summary>
//        /// <param name="sender"></param>
//        /// <param name="e"></param>
//        private void OnBtnSolveNewSolutionsTestClicked(object sender, EventArgs e)
//        {
//#if DEBUG
//            Globals.bSolveNewSolutionsTest = !Globals.bSolveNewSolutionsTest;

//            if (Globals.bSolveNewSolutionsTest)
//            {
//                btnSolveNewSolutionsTest.Text = "+";
//            }
//            else
//            {
//                btnSolveNewSolutionsTest.Text = "-";
//            }
//#endif
//        }

        /// <summary>
        /// Go to the Settings page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnPageSettingsClicked(object sender, EventArgs e)
        {
            // Cancel text to speech
            ClassSpeech.CancelTextToSpeech();

            await Navigation.PushAsync(new PageSettings());
        }

        /// <summary>
        /// Select a color by tapping on a cube and put it in a temporary polygon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnGetColorTapped(object sender, TappedEventArgs args)
        {
            if (sender is Polygon polygon)
            {
                plgCubeColorSelect.Fill = polygon.Fill;
            }
        }

        /// <summary>
        /// Set the color by tapping on a cube and fill the cube with the color from the temporary polygon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnSetColorTapped(object sender, TappedEventArgs args)
        {
            if (plgCubeColorSelect.Fill is not null && bColorDrop && sender is Polygon polygon)
            {
                polygon.Fill = plgCubeColorSelect.Fill;
                SetCubeColorsInArrays();
            }
        }

        /// <summary>
        /// Set the colors on the cube
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnButtonSetColorsCubeClicked(object sender, EventArgs e)
        {
            bColorDrop = !bColorDrop;

            if (bColorDrop)
            {
                btnSolveCube.IsEnabled = false;
                grdCubeColorSelect.BackgroundColor = Color.FromArgb("#969696");
                plgCubeColorSelect.Fill = plgCubeColor6.Fill;
                IsVisibleCubeColors(true);
                IsEnabledArrows(false);

                imgbtnTurnUpHorMiddleToRightFace.IsEnabled = true;
                imgbtnTurnUpVerMiddleToFrontFace.IsEnabled = true;
                imgbtnTurnFrontHorMiddleToRightFace.IsEnabled = true;
                imgbtnTurnFrontHorMiddleToLeftFace.IsEnabled = true;
                imgbtnTurnUpVerMiddleToBackFace.IsEnabled = true;
                imgbtnTurnUpHorMiddleToLeftFace.IsEnabled = true;

                ToolTipProperties.SetText(imgbtnTurnUpHorMiddleToRightFace, CubeLang.TurnCubeUpFaceToRightFace_Text);
                ToolTipProperties.SetText(imgbtnTurnUpVerMiddleToFrontFace, CubeLang.TurnCubeFrontFaceToDownFace_Text);
                ToolTipProperties.SetText(imgbtnTurnFrontHorMiddleToRightFace, CubeLang.TurnCubeFrontFaceToRightFace_Text);
                ToolTipProperties.SetText(imgbtnTurnFrontHorMiddleToLeftFace, CubeLang.TurnCubeFrontFaceToLeftFace_Text);
                ToolTipProperties.SetText(imgbtnTurnUpVerMiddleToBackFace, CubeLang.TurnCubeFrontFaceToUpFace_Text);
                ToolTipProperties.SetText(imgbtnTurnUpHorMiddleToLeftFace, CubeLang.TurnCubeUpFaceToLeftFace_Text);
            }
            else
            {
                Globals.lCubeTurns.Clear();

                if (!CheckNumberColorsCube())
                {
                    bColorDrop = true;
                    return;
                }

                ToolTipProperties.SetText(imgbtnTurnUpHorMiddleToRightFace, CubeLang.TurnUpMiddleToRightFace_Text);
                ToolTipProperties.SetText(imgbtnTurnUpVerMiddleToFrontFace, CubeLang.TurnUpMiddleToFrontFace_Text);
                ToolTipProperties.SetText(imgbtnTurnFrontHorMiddleToRightFace, CubeLang.TurnFrontMiddleToRightFace_Text);
                ToolTipProperties.SetText(imgbtnTurnFrontHorMiddleToLeftFace, CubeLang.TurnRightMiddleToFrontFace_Text);
                ToolTipProperties.SetText(imgbtnTurnUpVerMiddleToBackFace, CubeLang.TurnFrontMiddleToUpFace_Text);
                ToolTipProperties.SetText(imgbtnTurnUpHorMiddleToLeftFace, CubeLang.TurnRightMiddleToUpFace_Text);

                IsEnabledArrows(false);
                IsVisibleCubeColors(false);
                grdCubeColorSelect.BackgroundColor = Color.FromArgb("#00000000");

                btnSolveCube.IsEnabled = true;
            }
        }

        /// <summary>
        /// Solve the cube
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnBtnSolveCubeClicked(object sender, EventArgs e)
        {
            // Check the number of colors of the cube
            if (!CheckNumberColorsCube())
            {
                return;
            }

            // Check if the cube is already solved
            if (ClassColorsCube.CheckIfSolved())
            {
                Globals.lCubeTurns.Clear();
                return;
            }

            // Check if the tables exist for the Kociemba solution
            if (ClassSolveCubeKociemba.CheckIfTableExists())
            {
                bKociembaTablesExist = true;
            }
            else
            {
                // Calculate the duration in seconds for the first Kociemba solve with creation of the tables
                nDurationFirstKociembaSolve = await ClassFileOperations.DurationFirstKociembaSolveAsync();
            }

            // Set the text of the label 'lblCubeOutsideView' to show the time to solve the cube when using the Kociemba solution for the first time
            if (Globals.bKociembaSolution && !bKociembaTablesExist && Globals.lCubeTurns.Count == 0)
            {
                lblCubeOutsideView.Text = string.Format(CubeLang.WaitFirstGameLaunch_Text, nDurationFirstKociembaSolve);
            }

            // Control settings
            btnSolveCube.IsEnabled = false;
            imgbtnSetColorsCube.IsEnabled = false;
            imgbtnOpenCube.IsEnabled = false;
            imgbtnSaveCube.IsEnabled = false;
            imgbtnScrambleCube.IsEnabled = false;
            imgbtnResetCube.IsEnabled = false;
            btnGoOneTurnBackward.IsEnabled = false;
            btnGoOneTurnForward.IsEnabled = false;
            btnTurnContinuously.IsEnabled = false;
            lblCubeInsideView.IsVisible = false;

            // Settings
            bColorDrop = false;
            bSolvingCube = true;
            bSolved = true;
            SetArrowTooltips(false);
            IsEnabledArrows(false);

            string cMethod = string.Empty;

            // Start the activity indicator
            activityIndicator.IsRunning = true;
            await Task.Delay(200);

            // Create and start a stopwatch instance
            long startTime = Stopwatch.GetTimestamp();

            // Solve the cube from the turns the user has made in reverse order
            if (Globals.lCubeTurns.Count > 0)
            {
                cMethod = CubeLang.ReverseOrder_Text;
                Globals.nTestedSolutions = 1;
                Globals.lCubeTurns.Reverse();
                ClassCleanCubeTurns.CleanListCubeTurns(Globals.lCubeTurns, true);
                bSolved = true;
            }
            // Solve the cube from the turns the program has made
            else
            {
                // Save the start colors of the cube to array aStartPieces[]
                Array.Copy(Globals.aPieces, Globals.aStartPieces, 54);

                // Solve the cube with the Kociemba solution (using the SolveCubeFromMultiplePositionsAsync("Kociemba") takes a long time +10 minutes)
                if (Globals.bKociembaSolution)
                {
                    cMethod = "Kociemba";
                    bSolved = await ClassSolveCubeKociemba.SolveTheCubeKociembaAsync();

                    // Set bKociembaTablesExist to true after using the Kociemba solution for the first time
                    if (!bKociembaTablesExist)
                    {
                        bKociembaTablesExist = true;
                    }

                    if (bSolved)
                    {
                        Globals.nTestedSolutions = 1;
                    }
                }

                // Solve the cube with the CFOP solution
                if (!bSolved || !Globals.bKociembaSolution)
                {
                    cMethod = "CFOP";
                    Globals.nTestedSolutions = 0;
                    bSolved = await ClassSolveCubeMain.SolveCubeFromMultiplePositionsAsync("CFOP");
                }

                // For testing comment out the lines 338-339 and 402-426 (and change the line 450 to bTestSolveCube = true)
                // and uncomment one of the lines 433-435 to test one of the solutions to solve the cube.
                // If using the method 'TestCubeTurnsAsync()' then include the file 'ClassTestCubeTurns.cs' in the project,
                // otherwise exclude the file 'ClassTestCubeTurns.cs' from the project.

                //bSolved = await ClassTestCubeTurns.TestCubeTurnsAsync();          // Test the turns of the cube
                //bSolved = await ClassSolveCubeCFOP.SolveTheCubeCFOPAsync();       // For testing CFOP solution
                //ClassCleanCubeTurns.CleanListCubeTurns(Globals.lCubeTurns, true); // For testing the clean list cube turns

                // Restore the start colors of the cube from array aStartPieces[]
                Array.Copy(Globals.aStartPieces, Globals.aPieces, 54);
            }

            // Stop the activity indicator
            activityIndicator.IsRunning = false;

            // Stop the stopwatch and get the elapsed time
            TimeSpan delta = Stopwatch.GetElapsedTime(startTime);
            string elapsedMilliseconds = delta.TotalMilliseconds.ToString("F0");

            // Test variable to disable the 'steps one at a time' to solve te cube in the task MakeExplainTurnAsync()
            // If not testing the solution to solve the cube then set bTestSolveCube = false
            bTestSolveCube = false;

            if (bSolved)
            {
                // Display the number of turns and the elapsed time in milliseconds
                int nNumberOfTurns = Globals.lCubeTurns.Count;

                if (Globals.nTestedSolutions <= 1)
                {
                    await DisplayAlert("", $"{CubeLang.SolvingMethod_Text} {cMethod}\n{CubeLang.ResultTurns_Text} {nNumberOfTurns}\n{CubeLang.ResultTime_Text} {elapsedMilliseconds}", CubeLang.ButtonClose_Text);
                }
                else
                {
                    await DisplayAlert("", $"{CubeLang.SolvingMethod_Text} {cMethod}\n{CubeLang.ResultTurns_Text} {nNumberOfTurns}\n{CubeLang.ResultSolutions_Text} {Globals.nTestedSolutions}\n{CubeLang.ResultTime_Text} {elapsedMilliseconds}", CubeLang.ButtonClose_Text);
                }

                await Task.Delay(500);

                // Control settings
                lblCubeOutsideView.IsVisible = false;
                lblExplainTurnCube.IsVisible = true;
                btnGoOneTurnBackward.IsVisible = true;
                btnGoOneTurnForward.IsVisible = true;
                btnTurnContinuously.IsVisible = true;
                btnGoOneTurnForward.IsEnabled = true;
                btnTurnContinuously.IsEnabled = true;
                imgbtnResetCube.IsEnabled = true;

                // Variables for the turns of the cube
                bSolvingCubeStop = false;
                bTurnIsBackwards = false;
                int nTurnIndex = -1;
                string cTurn = "";

                // Loop through the turns of the cube
                while (true)
                {
                    // Forward turn
                    if (!bTurnIsBackwards)
                    {
                        // Add 1 to the turn index
                        if (nTurnIndex < nNumberOfTurns)
                        {
                            nTurnIndex++;
                        }
                    }
                    // Backward turn
                    else if (bTurnIsBackwards)
                    {
                        // Decrease the turn index by 1
                        if (nTurnIndex > 0)
                        {
                            nTurnIndex--;
                        }

                        // Turn the faces of the cube in reverse order
                        await ClassCubeTurns.TurnCubeLayersReversedAsync(Globals.lCubeTurns[nTurnIndex]);

                        // Set the cube colors from the arrays in the polygons
                        GetCubeColorsFromArrays();
                    }

                    // Enable or disable the button to go one turn backward
                    if (!bTurnContinuously)
                    {
                        btnGoOneTurnBackward.IsEnabled = nTurnIndex >= 1 && nTurnIndex <= nNumberOfTurns - 1;
                    }

                    // Enable or disable the save cube button
                    imgbtnSaveCube.IsEnabled = nTurnIndex == 0;

                    // Get the turn of the cube
                    // Use 'Globals.lCubeTurns.Count' instead of 'nNumberOfTurns' because the list can be empty after a reset
                    if (nTurnIndex < Globals.lCubeTurns.Count)
                    {
                        cTurn = Globals.lCubeTurns[nTurnIndex];
                    }

                    // Set the turn number of the cube
                    lblNumberTurns.Text = $"{nTurnIndex}/{nNumberOfTurns}";

                    // Set the turn of the cube
                    btnGoOneTurnForward.Text = cTurn;
#if IOS
                    // Workaround for !!!BUG!!! in iOS.NET9.0: if CharacterSpacing is set, the text of the button is not updaded
                    if (btnGoOneTurnForward.Handler?.PlatformView is UIKit.UIButton platformButton)
                    {
                        var titleLabel = platformButton.TitleLabel;
                        titleLabel.Text = btnGoOneTurnForward.Text;

                        var attributedText = titleLabel.AttributedText?.WithCharacterSpacing(btnGoOneTurnForward.CharacterSpacing);
                        platformButton.SetAttributedTitle(attributedText, UIKit.UIControlState.Normal);
                    }
#endif
                    // Make and explain the turn of the cube
                    await MakeExplainTurnAsync(cTurn);

                    // Exit the loop if the reset button has been pressed and the solving of the cube is stopped
                    if (bSolvingCubeStop)
                    {
                        ClassColorsCube.ResetCube();
                        GetCubeColorsFromArrays();

                        break;
                    }

                    // Exit the loop if the cube is solved
                    if ((!bTurnIsBackwards && nTurnIndex >= nNumberOfTurns - 1) || (bTurnIsBackwards && nTurnIndex >= nNumberOfTurns))
                    {
                        break;
                    }
                }

                if (!bSolvingCubeStop)
                {
                    // Set the last turn number of the cube
                    lblNumberTurns.Text = $"{nTurnIndex + 1}/{nNumberOfTurns}";

                    // Control settings
                    // !!!BUG!!! in iOS. btnGoOneTurnForward.Text needs a space to erase the text, string.Empty or "" does not work
                    // Even the space is not working anymore with Microsoft.Maui.Controls Version 9.0.30 ? and higher
                    // Had to replace the IsEnabled with IsVisible to hide the text of the btnGoOneTurnForward control
                    btnGoOneTurnForward.Text = " ";
                    btnGoOneTurnBackward.IsVisible = false;
                    btnGoOneTurnForward.IsVisible = false;
                    btnTurnContinuously.IsVisible = false;

                    // Wait for 500 milliseconds
                    await Task.Delay(500);

                    // Check if the cube is solved and display a message
                    if (ClassColorsCube.CheckIfSolved())
                    {
                        if (Globals.bExplainSpeech)
                        {
                            _ = ClassSpeech.ConvertTextToSpeechAsync(CubeLang.MessageCubeIsSolved_Text);
                        }

                        await DisplayAlert("", CubeLang.MessageCubeIsSolved_Text, CubeLang.ButtonClose_Text);
                    }
                    else
                    {
                        await DisplayAlert("", CubeLang.MessageCubeBackInPreviousState_Text, CubeLang.ButtonClose_Text);
                    }
                }
            }

            if (!bSolved && !bSolvingCubeStop)
            {
                await DisplayAlert("", CubeLang.MessageCubeCannotBeSolved_Text, CubeLang.ButtonClose_Text);
                imgbtnResetCube.IsEnabled = true;
            }

            // Clear the list with the cube turns
            Globals.lCubeTurns.Clear();

            // Reset for the next iteration
            _buttonPressed = new TaskCompletionSource<bool>();

            // Control settings
            bTurnContinuously = false;
            btnTurnContinuously.ImageSource = "ic_action_playback_play.png";
            lblNumberTurns.Text = "";
            lblExplainTurnCube.Text = "";
            lblExplainTurnCube.IsVisible = false;
            lblCubeOutsideView.Text = CubeLang.CubeOutsideView_Text;
            lblCubeOutsideView.IsVisible = true;
            btnGoOneTurnBackward.IsVisible = false;
            btnGoOneTurnForward.IsVisible = false;
            btnTurnContinuously.IsVisible = false;
            lblCubeInsideView.IsVisible = true;

            // Settings
            IsEnabledArrows(true);
            SetArrowTooltips(true);
            bSolvingCube = false;

            // Control settings
            btnSolveCube.IsEnabled = true;
            imgbtnSetColorsCube.IsEnabled = true;
            imgbtnOpenCube.IsEnabled = true;
            imgbtnSaveCube.IsEnabled = true;
            imgbtnScrambleCube.IsEnabled = true;
        }

        /// <summary>
        /// Make and explain the turn of the cube called from the main task SolveTheCubeAsync()
        /// </summary>
        /// <param name="cTurn"></param>
        /// <returns></returns>
        private async Task MakeExplainTurnAsync(string cTurn)
        {
            // If bTestSolveCube = true then do not use the 'steps one at a time' to solve te cube
            if (bTestSolveCube)
            {
                // Turn the faces of the cube
                await ClassCubeTurns.TurnCubeLayersAsync(cTurn);

                // Set the cube colors from the arrays in the polygons
                GetCubeColorsFromArrays();

                return;
            }

            // Enable the arrow button and set the background color to Active
            await SetImageButtonArrowIsEnabledAsync(cTurn, true);

            // Show the text
            string cTurnCubeText = await SetExplainTextAsync(cTurn);
            lblExplainTurnCube.Text = cTurnCubeText;

            // Convert or cancel text to speech
            if (!bTurnContinuously)
            {
                // Convert text to speech
                ExplainTurnCubeSpeech(cTurnCubeText);
            }
            else if (bTurnContinuously && Globals.bTextToSpeechIsBusy)
            {
                // Cancel text to speech
                ClassSpeech.CancelTextToSpeech();
            }

            // Wait for 300 milliseconds on the button click event handler
            // because of a possible error 'The operation was canceled' in the Task ClassSpeech.ConvertTextToSpeechAsync()
            await Task.Delay(300);

            // Wait or not wait for the button to be pressed before continuing
            if (!bTurnContinuously)
            {
                // Wait for the button to be pressed before continuing
                _ = await _buttonPressed.Task;

                // Reset for the next iteration
                _buttonPressed = new TaskCompletionSource<bool>();
            }
            else if (bTurnContinuously)
            {
                // Wait for 400 milliseconds between two turns
                await Task.Delay(400);
            }

            // Restore settings
            await SetImageButtonArrowIsEnabledAsync(cTurn, false);

            // Forward turn
            if (!bTurnIsBackwards)
            {
                if (cTurn[^1] == '2' && !bTurnContinuously)
                {
                    await SplitHalfTurnInTwoQuarterTurnsAsync(cTurn);
                }
                else
                {
                    await ClassCubeTurns.TurnCubeLayersAsync(cTurn);
                }
                GetCubeColorsFromArrays();
            }
        }

        /// <summary>
        /// Split a half turn in two quarter turns to give the impression of rotational movement of the cube faces
        /// </summary>
        /// <param name="cTurn"></param>
        private async Task SplitHalfTurnInTwoQuarterTurnsAsync(string cTurn)
        {
            const int nMilliseconds = 100;

            switch (cTurn)
            {
                // Face rotations
                case Globals.turnFront2:                                // F2
                    await ClassCubeTurns.TurnCubeLayersAsync("F");
                    GetCubeColorsFromArrays();
                    await Task.Delay(nMilliseconds);
                    await ClassCubeTurns.TurnCubeLayersAsync("F");
                    break;
                case Globals.turnRight2:                                // R2
                    await ClassCubeTurns.TurnCubeLayersAsync("R");
                    GetCubeColorsFromArrays();
                    await Task.Delay(nMilliseconds);
                    await ClassCubeTurns.TurnCubeLayersAsync("R");
                    break;
                case Globals.turnBack2:                                 // B2
                    await ClassCubeTurns.TurnCubeLayersAsync("B");
                    GetCubeColorsFromArrays();
                    await Task.Delay(nMilliseconds);
                    await ClassCubeTurns.TurnCubeLayersAsync("B");
                    break;
                case Globals.turnLeft2:                                 // L2
                    await ClassCubeTurns.TurnCubeLayersAsync("L");
                    GetCubeColorsFromArrays();
                    await Task.Delay(nMilliseconds);
                    await ClassCubeTurns.TurnCubeLayersAsync("L");
                    break;
                case Globals.turnUp2:                                   // U2
                    await ClassCubeTurns.TurnCubeLayersAsync("U");
                    GetCubeColorsFromArrays();
                    await Task.Delay(nMilliseconds);
                    await ClassCubeTurns.TurnCubeLayersAsync("U");
                    break;
                case Globals.turnDown2:                                 // D2
                    await ClassCubeTurns.TurnCubeLayersAsync("D");
                    GetCubeColorsFromArrays();
                    await Task.Delay(nMilliseconds);
                    await ClassCubeTurns.TurnCubeLayersAsync("D");
                    break;

                // Middle layer rotations
                case Globals.turnUpHorMiddle2:                          // S2
                    await ClassCubeTurns.TurnCubeLayersAsync("S");
                    GetCubeColorsFromArrays();
                    await Task.Delay(nMilliseconds);
                    await ClassCubeTurns.TurnCubeLayersAsync("S");
                    break;
                case Globals.turnUpVerMiddle2:                          // M2
                    await ClassCubeTurns.TurnCubeLayersAsync("M");
                    GetCubeColorsFromArrays();
                    await Task.Delay(nMilliseconds);
                    await ClassCubeTurns.TurnCubeLayersAsync("M");
                    break;
                case Globals.turnFrontHorMiddle2:                       // E2
                    await ClassCubeTurns.TurnCubeLayersAsync("E");
                    GetCubeColorsFromArrays();
                    await Task.Delay(nMilliseconds);
                    await ClassCubeTurns.TurnCubeLayersAsync("E");
                    break;

                // Two layers at the same time
                case Globals.turn2LayersFront2:                         // f2
                    await ClassCubeTurns.TurnCubeLayersAsync("f");
                    GetCubeColorsFromArrays();
                    await Task.Delay(nMilliseconds);
                    await ClassCubeTurns.TurnCubeLayersAsync("f");
                    break;
                case Globals.turn2LayersRight2:                         // r2
                    await ClassCubeTurns.TurnCubeLayersAsync("r");
                    GetCubeColorsFromArrays();
                    await Task.Delay(nMilliseconds);
                    await ClassCubeTurns.TurnCubeLayersAsync("r");
                    break;
                case Globals.turn2LayersBack2:                          // b2
                    await ClassCubeTurns.TurnCubeLayersAsync("b");
                    GetCubeColorsFromArrays();
                    await Task.Delay(nMilliseconds);
                    await ClassCubeTurns.TurnCubeLayersAsync("b");
                    break;
                case Globals.turn2LayersLeft2:                          // l2
                    await ClassCubeTurns.TurnCubeLayersAsync("l");
                    GetCubeColorsFromArrays();
                    await Task.Delay(nMilliseconds);
                    await ClassCubeTurns.TurnCubeLayersAsync("l");
                    break;
                case Globals.turn2LayersUp2:                            // u2
                    await ClassCubeTurns.TurnCubeLayersAsync("u");
                    GetCubeColorsFromArrays();
                    await Task.Delay(nMilliseconds);
                    await ClassCubeTurns.TurnCubeLayersAsync("u");
                    break;
                case Globals.turn2LayersDown2:                          // d2
                    await ClassCubeTurns.TurnCubeLayersAsync("d");
                    GetCubeColorsFromArrays();
                    await Task.Delay(nMilliseconds);
                    await ClassCubeTurns.TurnCubeLayersAsync("d");
                    break;

                // Cube rotations
                case Globals.turnCubeFrontToLeft2:                      // y2
                    await ClassCubeTurns.TurnCubeLayersAsync("y");
                    GetCubeColorsFromArrays();
                    await Task.Delay(nMilliseconds);
                    await ClassCubeTurns.TurnCubeLayersAsync("y");
                    break;
                case Globals.turnCubeFrontToUp2:                        // x2
                    await ClassCubeTurns.TurnCubeLayersAsync("x");
                    GetCubeColorsFromArrays();
                    await Task.Delay(nMilliseconds);
                    await ClassCubeTurns.TurnCubeLayersAsync("x");
                    break;
                case Globals.turnCubeUpToRight2:                        // z2
                    await ClassCubeTurns.TurnCubeLayersAsync("z");
                    GetCubeColorsFromArrays();
                    await Task.Delay(nMilliseconds);
                    await ClassCubeTurns.TurnCubeLayersAsync("z");
                    break;

                default:
                    await ClassCubeTurns.TurnCubeLayersAsync(cTurn);
                    break;
            }
        }

        /// <summary>
        /// Check the number of colors of the cube
        /// </summary>
        /// <returns></returns>
        private bool CheckNumberColorsCube()
        {
            SetCubeColorsInArrays();
            return ClassColorsCube.CheckNumberColors();
        }

        /// <summary>
        /// Go one turn backwards
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnButtonGoOneTurnBackwardClicked(object sender, EventArgs e)
        {
            bTurnIsBackwards = true;
            _ = _buttonPressed.TrySetResult(true);
        }

        /// <summary>
        /// Go one turn forwards
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnButtonGoOneTurnForwardClicked(object sender, EventArgs e)
        {
            bTurnIsBackwards = false;
            _ = _buttonPressed.TrySetResult(true);
        }

        /// <summary>
        /// Turn the cube without pressing a button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnButtonTurnContinuouslyClicked(object sender, EventArgs e)
        {
            bTurnContinuously = !bTurnContinuously;
            bTurnIsBackwards = false;

            if (bTurnContinuously)
            {
                btnTurnContinuously.ImageSource = "ic_action_playback_stop.png";
                btnGoOneTurnBackward.IsEnabled = false;
                btnGoOneTurnForward.IsEnabled = false;
            }
            else if (!bTurnContinuously)
            {
                btnTurnContinuously.ImageSource = "ic_action_playback_play.png";
                btnGoOneTurnBackward.IsEnabled = true;
                btnGoOneTurnForward.IsEnabled = true;
            }

            _ = _buttonPressed.TrySetResult(true);
        }

        /// <summary>
        /// Turn the front face clockwise (to right +)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTurnFrontFaceToRightClicked(object sender, EventArgs e)
        {
            if (bSolvingCube)
            {
                bTurnIsBackwards = false;
                _ = _buttonPressed.TrySetResult(true);
                return;
            }

            // Reverse the turn when solving the cube in reverse order
            if (!bSolvingCube && !bColorDrop)
            {
                Globals.lCubeTurns.Add(Globals.turnFrontCCW);
            }

            _ = ExplainTurnCube(CubeLang.TurnFrontFaceToRight_Text);
            ClassCubeTurns.TurnFrontFaceTo("CW");
            GetCubeColorsFromArrays();
        }

        /// <summary>
        /// Turn the upper horizontal middle to the right face (+)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTurnUpHorMiddleToRightFaceClicked(object sender, EventArgs e)
        {
            if (bColorDrop)
            {
                TurnCubeUpFaceToRightFace();
                return;
            }

            if (bSolvingCube)
            {
                bTurnIsBackwards = false;
                _ = _buttonPressed.TrySetResult(true);
                return;
            }

            // Reverse the turn when solving the cube in reverse order
            if (!bSolvingCube && !bColorDrop)
            {
                Globals.lCubeTurns.Add(Globals.turnUpHorMiddleLeft);
            }

            _ = ExplainTurnCube(CubeLang.TurnUpMiddleToRightFace_Text);
            ClassCubeTurns.TurnUpHorMiddleTo("CW");
            GetCubeColorsFromArrays();
        }

        /// <summary>
        /// Turn the back face counter clockwise (to left -)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTurnBackFaceToLeftClicked(object sender, EventArgs e)
        {
            if (bSolvingCube)
            {
                bTurnIsBackwards = false;
                _ = _buttonPressed.TrySetResult(true);
                return;
            }

            // Reverse the turn when solving the cube in reverse order
            if (!bSolvingCube && !bColorDrop)
            {
                Globals.lCubeTurns.Add(Globals.turnBackCW);
            }

            _ = ExplainTurnCube(CubeLang.TurnBackFaceToLeft_Text);
            ClassCubeTurns.TurnBackFaceTo("CCW");
            GetCubeColorsFromArrays();
        }

        /// <summary>
        /// Turn the left face clockwise (to right +)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTurnLeftFaceToRightClicked(object sender, EventArgs e)
        {
            if (bSolvingCube)
            {
                bTurnIsBackwards = false;
                _ = _buttonPressed.TrySetResult(true);
                return;
            }

            // Reverse the turn when solving the cube in reverse order
            if (!bSolvingCube && !bColorDrop)
            {
                Globals.lCubeTurns.Add(Globals.turnLeftCCW);
            }

            _ = ExplainTurnCube(CubeLang.TurnLeftFaceToRight_Text);
            ClassCubeTurns.TurnLeftFaceTo("CW");
            GetCubeColorsFromArrays();
        }

        /// <summary>
        /// Turn the upper vertical middle to the front face (-)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTurnUpVerMiddleToFrontFaceClicked(object sender, EventArgs e)
        {
            if (bColorDrop)
            {
                TurnCubeFrontFaceToDownFace();
                return;
            }

            if (bSolvingCube)
            {
                bTurnIsBackwards = false;
                _ = _buttonPressed.TrySetResult(true);
                return;
            }

            // Reverse the turn when solving the cube in reverse order
            if (!bSolvingCube && !bColorDrop)
            {
                Globals.lCubeTurns.Add(Globals.turnUpVerMiddleBack);
            }

            _ = ExplainTurnCube(CubeLang.TurnUpMiddleToFrontFace_Text);
            ClassCubeTurns.TurnUpVerMiddleTo("CCW");
            GetCubeColorsFromArrays();
        }

        /// <summary>
        /// Turn the right face counter clockwise (to left -)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTurnRightFaceToLeftClicked(object sender, EventArgs e)
        {
            if (bSolvingCube)
            {
                bTurnIsBackwards = false;
                _ = _buttonPressed.TrySetResult(true);
                return;
            }

            // Reverse the turn when solving the cube in reverse order
            if (!bSolvingCube && !bColorDrop)
            {
                Globals.lCubeTurns.Add(Globals.turnRightCW);
            }

            _ = ExplainTurnCube(CubeLang.TurnRightFaceToLeft_Text);
            ClassCubeTurns.TurnRightFaceTo("CCW");
            GetCubeColorsFromArrays();
        }

        /// <summary>
        /// Turn the upper face counter clockwise (to left -)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTurnUpFaceToLeftClicked(object sender, EventArgs e)
        {
            if (bSolvingCube)
            {
                bTurnIsBackwards = false;
                _ = _buttonPressed.TrySetResult(true);
                return;
            }

            // Reverse the turn when solving the cube in reverse order
            if (!bSolvingCube && !bColorDrop)
            {
                Globals.lCubeTurns.Add(Globals.turnUpCW);
            }

            _ = ExplainTurnCube(CubeLang.TurnUpFaceToLeft_Text);
            ClassCubeTurns.TurnUpFaceTo("CCW");
            GetCubeColorsFromArrays();
        }

        /// <summary>
        /// Turn the front horizontal middle to the right face (-)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTurnFrontHorMiddleToRightFaceClicked(object sender, EventArgs e)
        {
            if (bColorDrop)
            {
                TurnCubeFrontFaceToRightFace();
                return;
            }

            if (bSolvingCube)
            {
                bTurnIsBackwards = false;
                _ = _buttonPressed.TrySetResult(true);
                return;
            }

            // Reverse the turn when solving the cube in reverse order
            if (!bSolvingCube && !bColorDrop)
            {
                Globals.lCubeTurns.Add(Globals.turnFrontHorMiddleLeft);
            }

            _ = ExplainTurnCube(CubeLang.TurnFrontMiddleToRightFace_Text);
            ClassCubeTurns.TurnFrontHorMiddleTo("CCW");
            GetCubeColorsFromArrays();
        }

        /// <summary>
        /// Turn the down face clockwise (to right +)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTurnDownFaceToRightClicked(object sender, EventArgs e)
        {
            if (bSolvingCube)
            {
                bTurnIsBackwards = false;
                _ = _buttonPressed.TrySetResult(true);
                return;
            }

            // Reverse the turn when solving the cube in reverse order
            if (!bSolvingCube && !bColorDrop)
            {
                Globals.lCubeTurns.Add(Globals.turnDownCCW);
            }

            _ = ExplainTurnCube(CubeLang.TurnDownFaceToRight_Text);
            ClassCubeTurns.TurnDownFaceTo("CW");
            GetCubeColorsFromArrays();
        }

        /// <summary>
        /// Turn the upper face clockwise (to right +)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTurnUpFaceToRightClicked(object sender, EventArgs e)
        {
            if (bSolvingCube)
            {
                bTurnIsBackwards = false;
                _ = _buttonPressed.TrySetResult(true);
                return;
            }

            // Reverse the turn when solving the cube in reverse order
            if (!bSolvingCube && !bColorDrop)
            {
                Globals.lCubeTurns.Add(Globals.turnUpCCW);
            }

            _ = ExplainTurnCube(CubeLang.TurnUpFaceToRight_Text);
            ClassCubeTurns.TurnUpFaceTo("CW");
            GetCubeColorsFromArrays();
        }

        /// <summary>
        /// Turn the front horizontal middle to the left face (+)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTurnFrontHorMiddleToLeftFaceClicked(object sender, EventArgs e)
        {
            if (bColorDrop)
            {
                TurnCubeFrontFaceToLeftFace();
                return;
            }

            if (bSolvingCube)
            {
                bTurnIsBackwards = false;
                _ = _buttonPressed.TrySetResult(true);
                return;
            }

            // Reverse the turn when solving the cube in reverse order
            if (!bSolvingCube && !bColorDrop)
            {
                Globals.lCubeTurns.Add(Globals.turnFrontHorMiddleRight);
            }

            _ = ExplainTurnCube(CubeLang.TurnRightMiddleToFrontFace_Text);
            ClassCubeTurns.TurnFrontHorMiddleTo("CW");
            GetCubeColorsFromArrays();
        }

        /// <summary>
        /// Turn the down face counter clockwise (to left -)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTurnDownFaceToLeftClicked(object sender, EventArgs e)
        {
            if (bSolvingCube)
            {
                bTurnIsBackwards = false;
                _ = _buttonPressed.TrySetResult(true);
                return;
            }

            // Reverse the turn when solving the cube in reverse order
            if (!bSolvingCube && !bColorDrop)
            {
                Globals.lCubeTurns.Add(Globals.turnDownCW);
            }

            _ = ExplainTurnCube(CubeLang.TurnDownFaceToLeft_Text);
            ClassCubeTurns.TurnDownFaceTo("CCW");
            GetCubeColorsFromArrays();
        }

        /// <summary>
        /// Turn the left face counter clockwise (to left -)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTurnLeftFaceToLeftClicked(object sender, EventArgs e)
        {
            if (bSolvingCube)
            {
                bTurnIsBackwards = false;
                _ = _buttonPressed.TrySetResult(true);
                return;
            }

            // Reverse the turn when solving the cube in reverse order
            if (!bSolvingCube && !bColorDrop)
            {
                Globals.lCubeTurns.Add(Globals.turnLeftCW);
            }

            _ = ExplainTurnCube(CubeLang.TurnLeftFaceToLeft_Text);
            ClassCubeTurns.TurnLeftFaceTo("CCW");
            GetCubeColorsFromArrays();
        }

        /// <summary>
        /// Turn the upper vertical middle to the back face (+)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTurnUpVerMiddleToBackFaceClicked(object sender, EventArgs e)
        {
            if (bColorDrop)
            {
                TurnCubeFrontFaceToUpFace();
                return;
            }

            if (bSolvingCube)
            {
                bTurnIsBackwards = false;
                _ = _buttonPressed.TrySetResult(true);
                return;
            }

            // Reverse the turn when solving the cube in reverse order
            if (!bSolvingCube && !bColorDrop)
            {
                Globals.lCubeTurns.Add(Globals.turnUpVerMiddleFront);
            }

            _ = ExplainTurnCube(CubeLang.TurnFrontMiddleToUpFace_Text);
            ClassCubeTurns.TurnUpVerMiddleTo("CW");
            GetCubeColorsFromArrays();
        }

        /// <summary>
        /// Turn the right face clockwise (to right +)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTurnRightFaceToRightClicked(object sender, EventArgs e)
        {
            if (bSolvingCube)
            {
                bTurnIsBackwards = false;
                _ = _buttonPressed.TrySetResult(true);
                return;
            }

            // Reverse the turn when solving the cube in reverse order
            if (!bSolvingCube && !bColorDrop)
            {
                Globals.lCubeTurns.Add(Globals.turnRightCCW);
            }

            _ = ExplainTurnCube(CubeLang.TurnRightFaceToRight_Text);
            ClassCubeTurns.TurnRightFaceTo("CW");
            GetCubeColorsFromArrays();
        }

        /// <summary>
        /// Turn the front face counter clockwise (to left -)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTurnFrontFaceToLeftClicked(object sender, EventArgs e)
        {
            if (bSolvingCube)
            {
                bTurnIsBackwards = false;
                _ = _buttonPressed.TrySetResult(true);
                return;
            }

            // Reverse the turn when solving the cube in reverse order
            if (!bSolvingCube && !bColorDrop)
            {
                Globals.lCubeTurns.Add(Globals.turnFrontCW);
            }

            _ = ExplainTurnCube(CubeLang.TurnFrontFaceToLeft_Text);
            ClassCubeTurns.TurnFrontFaceTo("CCW");
            GetCubeColorsFromArrays();
        }

        /// <summary>
        /// Turn the upper horizontal middle to the left face (-)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTurnUpHorMiddleToLeftFaceClicked(object sender, EventArgs e)
        {
            if (bColorDrop)
            {
                TurnCubeUpFaceToLeftFace();
                return;
            }

            if (bSolvingCube)
            {
                bTurnIsBackwards = false;
                _ = _buttonPressed.TrySetResult(true);
                return;
            }

            // Reverse the turn when solving the cube in reverse order
            if (!bSolvingCube && !bColorDrop)
            {
                Globals.lCubeTurns.Add(Globals.turnUpHorMiddleRight);
            }

            _ = ExplainTurnCube(CubeLang.TurnRightMiddleToUpFace_Text);
            ClassCubeTurns.TurnUpHorMiddleTo("CCW");
            GetCubeColorsFromArrays();
        }

        /// <summary>
        /// Turn the back face clockwise (to right +)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTurnBackFaceToRightClicked(object sender, EventArgs e)
        {
            if (bSolvingCube)
            {
                bTurnIsBackwards = false;
                _ = _buttonPressed.TrySetResult(true);
                return;
            }

            // Reverse the turn when solving the cube in reverse order
            if (!bSolvingCube && !bColorDrop)
            {
                Globals.lCubeTurns.Add(Globals.turnBackCCW);
            }

            _ = ExplainTurnCube(CubeLang.TurnBackFaceToRight_Text);
            ClassCubeTurns.TurnBackFaceTo("CW");
            GetCubeColorsFromArrays();
        }

        /// <summary>
        /// Rotate the entire cube a quarter turn so that the front goes to the left face
        /// </summary>
        private void TurnCubeFrontFaceToLeftFace()
        {
            if (!bSolvingCube)
            {
                bTurnIsBackwards = false;
                _ = ExplainTurnCube(CubeLang.TurnCubeFrontFaceToLeftFace_Text);
            }

            ClassCubeTurns.TurnUpFaceTo("CW");
            ClassCubeTurns.TurnFrontHorMiddleTo("CW");
            ClassCubeTurns.TurnDownFaceTo("CCW");
            GetCubeColorsFromArrays();
        }

        /// <summary>
        /// Rotate the entire cube a quarter turn so that the front goes to the right face
        /// </summary>
        private void TurnCubeFrontFaceToRightFace()
        {
            if (!bSolvingCube)
            {
                bTurnIsBackwards = false;
                _ = ExplainTurnCube(CubeLang.TurnCubeFrontFaceToRightFace_Text);
            }

            ClassCubeTurns.TurnUpFaceTo("CCW");
            ClassCubeTurns.TurnFrontHorMiddleTo("CCW");
            ClassCubeTurns.TurnDownFaceTo("CW");
            GetCubeColorsFromArrays();
        }

        /// <summary>
        /// Rotate the entire cube a quarter turn so that the front goes to the upper face
        /// </summary>
        private void TurnCubeFrontFaceToUpFace()
        {
            if (!bSolvingCube)
            {
                bTurnIsBackwards = false;
                _ = ExplainTurnCube(CubeLang.TurnCubeFrontFaceToUpFace_Text);
            }

            ClassCubeTurns.TurnRightFaceTo("CW");
            ClassCubeTurns.TurnUpVerMiddleTo("CW");
            ClassCubeTurns.TurnLeftFaceTo("CCW");
            GetCubeColorsFromArrays();
        }

        /// <summary>
        /// Rotate the entire cube a quarter turn so that the front goes to the down face
        /// </summary>
        private void TurnCubeFrontFaceToDownFace()
        {
            if (!bSolvingCube)
            {
                bTurnIsBackwards = false;
                _ = ExplainTurnCube(CubeLang.TurnCubeFrontFaceToDownFace_Text);
            }

            ClassCubeTurns.TurnRightFaceTo("CCW");
            ClassCubeTurns.TurnUpVerMiddleTo("CCW");
            ClassCubeTurns.TurnLeftFaceTo("CW");
            GetCubeColorsFromArrays();
        }

        /// <summary>
        /// Rotate the entire cube a quarter turn so that the upper face goes to the right face
        /// </summary>
        private void TurnCubeUpFaceToRightFace()
        {
            if (!bSolvingCube)
            {
                bTurnIsBackwards = false;
                _ = ExplainTurnCube(CubeLang.TurnCubeUpFaceToRightFace_Text);
            }

            ClassCubeTurns.TurnFrontFaceTo("CW");
            ClassCubeTurns.TurnUpHorMiddleTo("CW");
            ClassCubeTurns.TurnBackFaceTo("CCW");
            GetCubeColorsFromArrays();
        }

        /// <summary>
        /// Rotate the entire cube a quarter turn so that the upper face goes to the left face
        /// </summary>
        private void TurnCubeUpFaceToLeftFace()
        {
            if (!bSolvingCube)
            {
                bTurnIsBackwards = false;
                _ = ExplainTurnCube(CubeLang.TurnCubeUpFaceToLeftFace_Text);
            }

            ClassCubeTurns.TurnFrontFaceTo("CCW");
            ClassCubeTurns.TurnUpHorMiddleTo("CCW");
            ClassCubeTurns.TurnBackFaceTo("CW");
            GetCubeColorsFromArrays();
        }

        /// <summary>
        /// Explain the turn of the cube called from OnTurn....Clicked and Turn.... methods
        /// </summary>
        /// <param name="cTurnCubeText"></param>
        private async Task ExplainTurnCube(string cTurnCubeText)
        {
            // Convert text to speech
            ExplainTurnCubeSpeech(cTurnCubeText);

            if (Globals.bExplainText)
            {
                await DisplayAlert("", cTurnCubeText, CubeLang.ButtonClose_Text);
            }
        }

        /// <summary>
        /// Enalbe or disable the arrow imagebuttons
        /// </summary>
        /// <param name="cTurn"></param>
        /// <param name="bIsEnabled"></param>
        /// <returns></returns>
        private async Task SetImageButtonArrowIsEnabledAsync(string cTurn, bool bIsEnabled)
        {
            switch (cTurn)
            {
                // Face rotations
                case Globals.turnFrontCW:
                case Globals.turnFront2:
                    imgbtnTurnFrontFaceToRight.IsEnabled = bIsEnabled;
                    break;
                case Globals.turnFrontCCW:
                    imgbtnTurnFrontFaceToLeft.IsEnabled = bIsEnabled;
                    break;
                case Globals.turnRightCW:
                case Globals.turnRight2:
                    imgbtnTurnRightFaceToRight.IsEnabled = bIsEnabled;
                    break;
                case Globals.turnRightCCW:
                    imgbtnTurnRightFaceToLeft.IsEnabled = bIsEnabled;
                    break;
                case Globals.turnBackCW:
                case Globals.turnBack2:
                    imgbtnTurnBackFaceToRight.IsEnabled = bIsEnabled;
                    break;
                case Globals.turnBackCCW:
                    imgbtnTurnBackFaceToLeft.IsEnabled = bIsEnabled;
                    break;
                case Globals.turnLeftCW:
                case Globals.turnLeft2:
                    imgbtnTurnLeftFaceToRight.IsEnabled = bIsEnabled;
                    break;
                case Globals.turnLeftCCW:
                    imgbtnTurnLeftFaceToLeft.IsEnabled = bIsEnabled;
                    break;
                case Globals.turnUpCW:
                case Globals.turnUp2:
                    imgbtnTurnUpFaceToRight.IsEnabled = bIsEnabled;
                    break;
                case Globals.turnUpCCW:
                    imgbtnTurnUpFaceToLeft.IsEnabled = bIsEnabled;
                    break;
                case Globals.turnDownCW:
                case Globals.turnDown2:
                    imgbtnTurnDownFaceToRight.IsEnabled = bIsEnabled;
                    break;
                case Globals.turnDownCCW:
                    imgbtnTurnDownFaceToLeft.IsEnabled = bIsEnabled;
                    break;

                // Middle layer rotations
                case Globals.turnUpHorMiddleRight:
                case Globals.turnUpHorMiddle2:
                    imgbtnTurnUpHorMiddleToRightFace.IsEnabled = bIsEnabled;
                    break;
                case Globals.turnUpHorMiddleLeft:
                    imgbtnTurnUpHorMiddleToLeftFace.IsEnabled = bIsEnabled;
                    break;
                case Globals.turnUpVerMiddleBack:
                case Globals.turnUpVerMiddle2:
                    imgbtnTurnUpVerMiddleToBackFace.IsEnabled = bIsEnabled;
                    break;
                case Globals.turnUpVerMiddleFront:
                    imgbtnTurnUpVerMiddleToFrontFace.IsEnabled = bIsEnabled;
                    break;
                case Globals.turnFrontHorMiddleLeft:
                case Globals.turnFrontHorMiddle2:
                    imgbtnTurnFrontHorMiddleToLeftFace.IsEnabled = bIsEnabled;
                    break;
                case Globals.turnFrontHorMiddleRight:
                    imgbtnTurnFrontHorMiddleToRightFace.IsEnabled = bIsEnabled;
                    break;

                // Two layers at the same time
                case Globals.turn2LayersFrontCW:
                case Globals.turn2LayersFront2:
                    imgbtnTurnUpHorMiddleToRightFace.IsEnabled = bIsEnabled;
                    imgbtnTurnFrontFaceToRight.IsEnabled = bIsEnabled;
                    break;
                case Globals.turn2LayersFrontCCW:
                    imgbtnTurnUpHorMiddleToLeftFace.IsEnabled = bIsEnabled;
                    imgbtnTurnFrontFaceToLeft.IsEnabled = bIsEnabled;
                    break;
                case Globals.turn2LayersRightCW:
                case Globals.turn2LayersRight2:
                    imgbtnTurnUpVerMiddleToBackFace.IsEnabled = bIsEnabled;
                    imgbtnTurnRightFaceToRight.IsEnabled = bIsEnabled;
                    break;
                case Globals.turn2LayersRightCCW:
                    imgbtnTurnUpVerMiddleToFrontFace.IsEnabled = bIsEnabled;
                    imgbtnTurnRightFaceToLeft.IsEnabled = bIsEnabled;
                    break;
                case Globals.turn2LayersBackCW:
                case Globals.turn2LayersBack2:
                    imgbtnTurnUpHorMiddleToLeftFace.IsEnabled = bIsEnabled;
                    imgbtnTurnBackFaceToRight.IsEnabled = bIsEnabled;
                    break;
                case Globals.turn2LayersBackCCW:
                    imgbtnTurnUpHorMiddleToRightFace.IsEnabled = bIsEnabled;
                    imgbtnTurnBackFaceToLeft.IsEnabled = bIsEnabled;
                    break;
                case Globals.turn2LayersLeftCW:
                case Globals.turn2LayersLeft2:
                    imgbtnTurnUpVerMiddleToFrontFace.IsEnabled = bIsEnabled;
                    imgbtnTurnLeftFaceToRight.IsEnabled = bIsEnabled;
                    break;
                case Globals.turn2LayersLeftCCW:
                    imgbtnTurnUpVerMiddleToBackFace.IsEnabled = bIsEnabled;
                    imgbtnTurnLeftFaceToLeft.IsEnabled = bIsEnabled;
                    break;
                case Globals.turn2LayersUpCW:
                case Globals.turn2LayersUp2:
                    imgbtnTurnFrontHorMiddleToLeftFace.IsEnabled = bIsEnabled;
                    imgbtnTurnUpFaceToRight.IsEnabled = bIsEnabled;
                    break;
                case Globals.turn2LayersUpCCW:
                    imgbtnTurnFrontHorMiddleToRightFace.IsEnabled = bIsEnabled;
                    imgbtnTurnUpFaceToLeft.IsEnabled = bIsEnabled;
                    break;
                case Globals.turn2LayersDownCW:
                case Globals.turn2LayersDown2:
                    imgbtnTurnFrontHorMiddleToRightFace.IsEnabled = bIsEnabled;
                    imgbtnTurnDownFaceToRight.IsEnabled = bIsEnabled;
                    break;
                case Globals.turn2LayersDownCCW:
                    imgbtnTurnFrontHorMiddleToLeftFace.IsEnabled = bIsEnabled;
                    imgbtnTurnDownFaceToLeft.IsEnabled = bIsEnabled;
                    break;

                // Cube rotations
                case Globals.turnCubeFrontToRight:
                    imgbtnTurnUpFaceToLeft.IsEnabled = bIsEnabled;
                    imgbtnTurnFrontHorMiddleToRightFace.IsEnabled = bIsEnabled;
                    imgbtnTurnDownFaceToRight.IsEnabled = bIsEnabled;
                    break;
                case Globals.turnCubeFrontToLeft:
                case Globals.turnCubeFrontToLeft2:
                    imgbtnTurnUpFaceToRight.IsEnabled = bIsEnabled;
                    imgbtnTurnFrontHorMiddleToLeftFace.IsEnabled = bIsEnabled;
                    imgbtnTurnDownFaceToLeft.IsEnabled = bIsEnabled;
                    break;
                case Globals.turnCubeFrontToUp:
                case Globals.turnCubeFrontToUp2:
                    imgbtnTurnLeftFaceToLeft.IsEnabled = bIsEnabled;
                    imgbtnTurnUpVerMiddleToBackFace.IsEnabled = bIsEnabled;
                    imgbtnTurnRightFaceToRight.IsEnabled = bIsEnabled;
                    break;
                case Globals.turnCubeFrontToDown:
                    imgbtnTurnLeftFaceToRight.IsEnabled = bIsEnabled;
                    imgbtnTurnUpVerMiddleToFrontFace.IsEnabled = bIsEnabled;
                    imgbtnTurnRightFaceToLeft.IsEnabled = bIsEnabled;
                    break;
                case Globals.turnCubeUpToRight:
                case Globals.turnCubeUpToRight2:
                    imgbtnTurnFrontFaceToRight.IsEnabled = bIsEnabled;
                    imgbtnTurnUpHorMiddleToRightFace.IsEnabled = bIsEnabled;
                    imgbtnTurnBackFaceToLeft.IsEnabled = bIsEnabled;
                    break;
                case Globals.turnCubeUpToLeft:
                    imgbtnTurnFrontFaceToLeft.IsEnabled = bIsEnabled;
                    imgbtnTurnUpHorMiddleToLeftFace.IsEnabled = bIsEnabled;
                    imgbtnTurnBackFaceToRight.IsEnabled = bIsEnabled;
                    break;

                default:
                    await DisplayAlert(CubeLang.ErrorTitle_Text, $"SetImageButtonArrowIsEnabledAsync\ncTurn not found:\n{cTurn}", CubeLang.ButtonClose_Text);
                    break;
            }
        }

        /// <summary>
        /// Set the explain text depending on the direction of rotation of the cube
        /// </summary>
        /// <param name="cTurn"></param>
        /// <returns></returns>
        private async Task<string> SetExplainTextAsync(string cTurn)
        {
            string cTurnCubeText = "";
            cCurrentTurn = cTurn;

            switch (cTurn)
            {
                // Face rotations
                case Globals.turnFrontCW:
                    cTurnCubeText = CubeLang.TurnFrontFaceToRight_Text;
                    break;
                case Globals.turnFrontCCW:
                    cTurnCubeText = CubeLang.TurnFrontFaceToLeft_Text;
                    break;
                case Globals.turnFront2:
                    cTurnCubeText = CubeLang.TurnFrontFaceHalfTurn_Text;
                    break;
                case Globals.turnRightCW:
                    cTurnCubeText = CubeLang.TurnRightFaceToRight_Text;
                    break;
                case Globals.turnRightCCW:
                    cTurnCubeText = CubeLang.TurnRightFaceToLeft_Text;
                    break;
                case Globals.turnRight2:
                    cTurnCubeText = CubeLang.TurnRightFaceHalfTurn_Text;
                    break;
                case Globals.turnBackCW:
                    cTurnCubeText = CubeLang.TurnBackFaceToRight_Text;
                    break;
                case Globals.turnBackCCW:
                    cTurnCubeText = CubeLang.TurnBackFaceToLeft_Text;
                    break;
                case Globals.turnBack2:
                    cTurnCubeText = CubeLang.TurnBackFaceHalfTurn_Text;
                    break;
                case Globals.turnLeftCW:
                    cTurnCubeText = CubeLang.TurnLeftFaceToRight_Text;
                    break;
                case Globals.turnLeftCCW:
                    cTurnCubeText = CubeLang.TurnLeftFaceToLeft_Text;
                    break;
                case Globals.turnLeft2:
                    cTurnCubeText = CubeLang.TurnLeftFaceHalfTurn_Text;
                    break;
                case Globals.turnUpCW:
                    cTurnCubeText = CubeLang.TurnUpFaceToRight_Text;
                    break;
                case Globals.turnUpCCW:
                    cTurnCubeText = CubeLang.TurnUpFaceToLeft_Text;
                    break;
                case Globals.turnUp2:
                    cTurnCubeText = CubeLang.TurnUpFaceHalfTurn_Text;
                    break;
                case Globals.turnDownCW:
                    cTurnCubeText = CubeLang.TurnDownFaceToRight_Text;
                    break;
                case Globals.turnDownCCW:
                    cTurnCubeText = CubeLang.TurnDownFaceToLeft_Text;
                    break;
                case Globals.turnDown2:
                    cTurnCubeText = CubeLang.TurnDownFaceHalfTurn_Text;
                    break;

                // Middle layer rotations
                case Globals.turnUpHorMiddleRight:
                    cTurnCubeText = CubeLang.TurnUpMiddleToRightFace_Text;
                    break;
                case Globals.turnUpHorMiddleLeft:
                    cTurnCubeText = CubeLang.TurnRightMiddleToUpFace_Text;
                    break;
                case Globals.turnUpHorMiddle2:
                    cTurnCubeText = CubeLang.TurnMiddleLayerHalfTurn_Text;
                    break;

                case Globals.turnUpVerMiddleBack:
                    cTurnCubeText = CubeLang.TurnFrontMiddleToUpFace_Text;
                    break;
                case Globals.turnUpVerMiddleFront:
                    cTurnCubeText = CubeLang.TurnUpMiddleToFrontFace_Text;
                    break;
                case Globals.turnUpVerMiddle2:
                    cTurnCubeText = CubeLang.TurnMiddleLayerHalfTurn_Text;
                    break;

                case Globals.turnFrontHorMiddleLeft:
                    cTurnCubeText = CubeLang.TurnRightMiddleToFrontFace_Text;
                    break;
                case Globals.turnFrontHorMiddleRight:
                    cTurnCubeText = CubeLang.TurnFrontMiddleToRightFace_Text;
                    break;
                case Globals.turnFrontHorMiddle2:
                    cTurnCubeText = CubeLang.TurnMiddleLayerHalfTurn_Text;
                    break;

                // Two layers at the same time
                case Globals.turn2LayersFrontCW:
                    cTurnCubeText = CubeLang.Turn2LayersFrontCW_Text;
                    break;
                case Globals.turn2LayersFrontCCW:
                    cTurnCubeText = CubeLang.Turn2LayersFrontCCW_Text;
                    break;
                case Globals.turn2LayersFront2:
                    cTurnCubeText = CubeLang.Turn2LayersFront2_Text;
                    break;
                case Globals.turn2LayersRightCW:
                    cTurnCubeText = CubeLang.Turn2LayersRightCW_Text;
                    break;
                case Globals.turn2LayersRightCCW:
                    cTurnCubeText = CubeLang.Turn2LayersRightCCW_Text;
                    break;
                case Globals.turn2LayersRight2:
                    cTurnCubeText = CubeLang.Turn2LayersRight2_Text;
                    break;
                case Globals.turn2LayersBackCW:
                    cTurnCubeText = CubeLang.Turn2LayersBackCW_Text;
                    break;
                case Globals.turn2LayersBackCCW:
                    cTurnCubeText = CubeLang.Turn2LayersBackCCW_Text;
                    break;
                case Globals.turn2LayersBack2:
                    cTurnCubeText = CubeLang.Turn2LayersBack2_Text;
                    break;
                case Globals.turn2LayersLeftCW:
                    cTurnCubeText = CubeLang.Turn2LayersLeftCW_Text;
                    break;
                case Globals.turn2LayersLeftCCW:
                    cTurnCubeText = CubeLang.Turn2LayersLeftCCW_Text;
                    break;
                case Globals.turn2LayersLeft2:
                    cTurnCubeText = CubeLang.Turn2LayersLeft2_Text;
                    break;
                case Globals.turn2LayersUpCW:
                    cTurnCubeText = CubeLang.Turn2LayersUpCW_Text;
                    break;
                case Globals.turn2LayersUpCCW:
                    cTurnCubeText = CubeLang.Turn2LayersUpCCW_Text;
                    break;
                case Globals.turn2LayersUp2:
                    cTurnCubeText = CubeLang.Turn2LayersUp2_Text;
                    break;
                case Globals.turn2LayersDownCW:
                    cTurnCubeText = CubeLang.Turn2LayersDownCW_Text;
                    break;
                case Globals.turn2LayersDownCCW:
                    cTurnCubeText = CubeLang.Turn2LayersDownCCW_Text;
                    break;
                case Globals.turn2LayersDown2:
                    cTurnCubeText = CubeLang.Turn2LayersDown2_Text;
                    break;

                // Cube rotations
                case Globals.turnCubeFrontToRight:
                    cTurnCubeText = CubeLang.TurnCubeFrontFaceToRightFace_Text;
                    break;
                case Globals.turnCubeFrontToLeft:
                    cTurnCubeText = CubeLang.TurnCubeFrontFaceToLeftFace_Text;
                    break;
                case Globals.turnCubeFrontToLeft2:
                    cTurnCubeText = CubeLang.TurnCubeFrontFaceToBackFaceUpStays_Text;
                    break;
                case Globals.turnCubeFrontToUp:
                    cTurnCubeText = CubeLang.TurnCubeFrontFaceToUpFace_Text;
                    break;
                case Globals.turnCubeFrontToUp2:
                    cTurnCubeText = CubeLang.TurnCubeFrontFaceToBackFaceRightStays_Text;
                    break;
                case Globals.turnCubeFrontToDown:
                    cTurnCubeText = CubeLang.TurnCubeFrontFaceToDownFace_Text;
                    break;
                case Globals.turnCubeUpToRight:
                    cTurnCubeText = CubeLang.TurnCubeUpFaceToRightFace_Text;
                    break;
                case Globals.turnCubeUpToRight2:
                    cTurnCubeText = CubeLang.TurnCubeUpFaceToDownFaceFrontStays_Text;
                    break;
                case Globals.turnCubeUpToLeft:
                    cTurnCubeText = CubeLang.TurnCubeUpFaceToLeftFace_Text;
                    break;

                default:
                    await DisplayAlert(CubeLang.ErrorTitle_Text, $"SetExplainTextAsync\ncTurn not found:\n{cTurn}", CubeLang.ButtonClose_Text);
                    break;
            }

            return cTurnCubeText;
        }

        /// <summary>
        /// Explain the turn of the cube with speech
        /// </summary>
        /// <param name="cTurnCubeText"></param>
        private static void ExplainTurnCubeSpeech(string cTurnCubeText)
        {
            if (Globals.bExplainSpeech && !string.IsNullOrEmpty(cTurnCubeText))
            {
                // Replace (+) and (-) with empty string
                if (cTurnCubeText.Length > 6)
                {
                    cTurnCubeText = cTurnCubeText.Replace("(+)", "");
                    cTurnCubeText = cTurnCubeText.Replace("(-)", "");
                }

                _ = ClassSpeech.ConvertTextToSpeechAsync(cTurnCubeText);
            }
        }

        /// <summary>
        /// On clicked event: Open, restore the cube
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnButtonOpenCubeClicked(object sender, EventArgs e)
        {
            _ = ClassFileOperations.CubeDataOpen();

            GetCubeColorsFromArrays();
            Globals.lCubeTurns.Clear();
        }

        /// <summary>
        /// On clicked event: Save the cube
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnButtonSaveCubeClicked(object sender, EventArgs e)
        {
            SetCubeColorsInArrays();
            _ = ClassFileOperations.CubeDataSave();
        }

        /// <summary>
        /// On clicked event: Scramble the cube
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnButtonScrambleCubeClicked(object sender, EventArgs e)
        {
            // Instantiate random number generator using system-supplied value as seed
            Random randNumber = new();

            // Generate a random integer from 20 to 40 turns
            int nNumberOfTurns = randNumber.Next(20, 41);
            Debug.WriteLine($"nNumberOfTurns: {nNumberOfTurns}");

            // Test variable to disable the 'steps one at a time' to solve te cube in the task MakeExplainTurnAsync()
            bTestSolveCube = true;

            string cTurns = string.Empty;

            // Loop through the random number of turns
            for (int ctr = 0; ctr <= nNumberOfTurns - 1; ctr++)
            {
                // Generate random indexes for cube turns
                int nIndex = randNumber.Next(ScrambledCubeTurns.Length);

                // Make the cube turn
                await MakeExplainTurnAsync(ScrambledCubeTurns[nIndex]);

                cTurns = $"{cTurns}{ScrambledCubeTurns[nIndex]} ";
            }

            // Display the cube turns in the output window for testing purposes
            Debug.WriteLine($"ScrambledCubeTurns: {cTurns}");

            // Reset the test variable
            bTestSolveCube = false;
        }

        /// <summary>
        /// On clicked event: Stop the solving of the cube and/or reset the colors of the cube
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnButtonResetClicked(object sender, EventArgs e)
        {
            // Cancel text to speech
            ClassSpeech.CancelTextToSpeech();

            // Reset the cube colors
            Globals.lCubeTurns.Clear();

            if (bSolvingCube)
            {
                bSolvingCubeStop = true;
                _ = _buttonPressed.TrySetResult(true);
            }

            ClassColorsCube.ResetCube();
            GetCubeColorsFromArrays();

            if (!bColorDrop)
            {
                IsEnabledArrows(true);
            }
        }

        /// <summary>
        /// Store the cube colors from the polygons in the arrays
        /// </summary>
        private void SetCubeColorsInArrays()
        {
            // Create and start a stopwatch instance
            //long startTime = Stopwatch.GetTimestamp();

            // Create a span for the arrays
            Span<string> aFaceColorsSpan = Globals.aFaceColors.AsSpan();
            Span<string> aPiecesSpan = Globals.aPieces.AsSpan();

            for (int i = 1; i < 7; i++)
            {
                Polygon polygon = this.FindByName<Polygon>($"plgCubeColor{i}");
                aFaceColorsSpan[i] = GetHexColorPolygon(polygon);
            }

            for (int i = 0; i < 54; i++)
            {
                Polygon polygon = this.FindByName<Polygon>($"plgPiece{i}");
                aPiecesSpan[i] = GetHexColorPolygon(polygon);
            }

            // Stop the stopwatch and get the elapsed time
            //TimeSpan delta = Stopwatch.GetElapsedTime(startTime);
            //_ = DisplayAlert("SetCubeColorsInArrays", $"Time elapsed (hh:mm:ss.xxxxxxx): {delta}", "OK");
        }

        /// <summary>
        /// Set the cube colors from the arrays in the polygons
        /// </summary>
        private void GetCubeColorsFromArrays()
        {
            // Create and start a stopwatch instance
            //long startTime = Stopwatch.GetTimestamp();

            // Create a span for the arrays
            ReadOnlySpan<string> aFaceColorsSpan = Globals.aFaceColors.AsSpan();
            ReadOnlySpan<string> aPiecesSpan = Globals.aPieces.AsSpan();

            for (int i = 1; i < 7; i++)
            {
                Polygon polygon = this.FindByName<Polygon>($"plgCubeColor{i}");
                polygon.Fill = Color.FromArgb(aFaceColorsSpan[i]);
            }

            for (int i = 0; i < 54; i++)
            {
                Polygon polygon = this.FindByName<Polygon>($"plgPiece{i}");
                polygon.Fill = Color.FromArgb(aPiecesSpan[i]);
            }

            // Stop the stopwatch and get the elapsed time
            //TimeSpan delta = Stopwatch.GetElapsedTime(startTime);
            //_ = DisplayAlert("GetCubeColorsFromArrays", $"Time elapsed (hh:mm:ss.xxxxxxx): {delta}", "OK");
        }

        /// <summary>
        /// Get the hexadecimal color code from the polygon fill property
        /// </summary>
        /// <param name="polygon"></param>
        /// <returns></returns>
        private static string GetHexColorPolygon(Polygon polygon)
        {
            SolidColorBrush brush = (SolidColorBrush)polygon.Fill;
            Color color = brush.Color;

            color = Color.FromRgb(color.Red, color.Green, color.Blue);
            return color.ToHex();
        }

        ///// <summary>
        ///// Get the decimal color code from the polygon fill property
        ///// </summary>
        ///// <param name="polygon"></param>
        ///// <returns></returns>
        //private static int GetDecColorPolygon(Polygon polygon)
        //{
        //    SolidColorBrush brush = (SolidColorBrush)polygon.Fill;
        //    Color color = brush.Color;

        //    color = Color.FromRgb(color.Red, color.Green, color.Blue);
        //    return int.Parse(color.ToHex().Replace("#", ""), NumberStyles.HexNumber);
        //}

        /// <summary>
        /// Set the cube colors for drag and drop to visible or invisible
        /// </summary>
        /// <param name="bEnableDisable"></param>
        private void IsVisibleCubeColors(bool bEnableDisable)
        {
            plgCubeColorSelect.IsVisible = bEnableDisable;
            plgCubeColor1.IsVisible = bEnableDisable;
            plgCubeColor2.IsVisible = bEnableDisable;
            plgCubeColor3.IsVisible = bEnableDisable;
            plgCubeColor4.IsVisible = bEnableDisable;
            plgCubeColor5.IsVisible = bEnableDisable;
            plgCubeColor6.IsVisible = bEnableDisable;
        }

        /// <summary>
        /// Set the arrow buttons tooltip
        /// </summary>
        /// <param name="bSetArrowTooltip"></param>
        private void SetArrowTooltips(bool bSetArrowTooltip)
        {
            if (bSetArrowTooltip)
            {
                ToolTipProperties.SetText(imgbtnTurnFrontFaceToRight, CubeLang.TurnFrontFaceToRight_Text);
                ToolTipProperties.SetText(imgbtnTurnUpHorMiddleToRightFace, CubeLang.TurnUpMiddleToRightFace_Text);
                ToolTipProperties.SetText(imgbtnTurnBackFaceToLeft, CubeLang.TurnBackFaceToLeft_Text);
                ToolTipProperties.SetText(imgbtnTurnLeftFaceToRight, CubeLang.TurnLeftFaceToRight_Text);
                ToolTipProperties.SetText(imgbtnTurnUpVerMiddleToFrontFace, CubeLang.TurnUpMiddleToFrontFace_Text);
                ToolTipProperties.SetText(imgbtnTurnRightFaceToLeft, CubeLang.TurnRightFaceToLeft_Text);
                ToolTipProperties.SetText(imgbtnTurnUpFaceToLeft, CubeLang.TurnUpFaceToLeft_Text);
                ToolTipProperties.SetText(imgbtnTurnFrontHorMiddleToRightFace, CubeLang.TurnFrontMiddleToRightFace_Text);
                ToolTipProperties.SetText(imgbtnTurnDownFaceToRight, CubeLang.TurnDownFaceToRight_Text);
                ToolTipProperties.SetText(imgbtnTurnUpFaceToRight, CubeLang.TurnUpFaceToRight_Text);
                ToolTipProperties.SetText(imgbtnTurnFrontHorMiddleToLeftFace, CubeLang.TurnRightMiddleToFrontFace_Text);
                ToolTipProperties.SetText(imgbtnTurnDownFaceToLeft, CubeLang.TurnDownFaceToLeft_Text);
                ToolTipProperties.SetText(imgbtnTurnLeftFaceToLeft, CubeLang.TurnLeftFaceToLeft_Text);
                ToolTipProperties.SetText(imgbtnTurnUpVerMiddleToBackFace, CubeLang.TurnFrontMiddleToUpFace_Text);
                ToolTipProperties.SetText(imgbtnTurnRightFaceToRight, CubeLang.TurnRightFaceToRight_Text);
                ToolTipProperties.SetText(imgbtnTurnFrontFaceToLeft, CubeLang.TurnFrontFaceToLeft_Text);
                ToolTipProperties.SetText(imgbtnTurnUpHorMiddleToLeftFace, CubeLang.TurnRightMiddleToUpFace_Text);
                ToolTipProperties.SetText(imgbtnTurnBackFaceToRight, CubeLang.TurnBackFaceToRight_Text);
            }
            else
            {
                ToolTipProperties.SetText(imgbtnTurnFrontFaceToRight, "");
                ToolTipProperties.SetText(imgbtnTurnUpHorMiddleToRightFace, "");
                ToolTipProperties.SetText(imgbtnTurnBackFaceToLeft, "");
                ToolTipProperties.SetText(imgbtnTurnLeftFaceToRight, "");
                ToolTipProperties.SetText(imgbtnTurnUpVerMiddleToFrontFace, "");
                ToolTipProperties.SetText(imgbtnTurnRightFaceToLeft, "");
                ToolTipProperties.SetText(imgbtnTurnUpFaceToLeft, "");
                ToolTipProperties.SetText(imgbtnTurnFrontHorMiddleToRightFace, "");
                ToolTipProperties.SetText(imgbtnTurnDownFaceToRight, "");
                ToolTipProperties.SetText(imgbtnTurnUpFaceToRight, "");
                ToolTipProperties.SetText(imgbtnTurnFrontHorMiddleToLeftFace, "");
                ToolTipProperties.SetText(imgbtnTurnDownFaceToLeft, "");
                ToolTipProperties.SetText(imgbtnTurnLeftFaceToLeft, "");
                ToolTipProperties.SetText(imgbtnTurnUpVerMiddleToBackFace, "");
                ToolTipProperties.SetText(imgbtnTurnRightFaceToRight, "");
                ToolTipProperties.SetText(imgbtnTurnFrontFaceToLeft, "");
                ToolTipProperties.SetText(imgbtnTurnUpHorMiddleToLeftFace, "");
                ToolTipProperties.SetText(imgbtnTurnBackFaceToRight, "");
            }
        }

        /// <summary>
        /// Enable or Disable the arrows
        /// </summary>
        /// <param name="bEnableDisable"></param>
        private void IsEnabledArrows(bool bEnableDisable)
        {
            imgbtnTurnFrontFaceToRight.IsEnabled = bEnableDisable;
            imgbtnTurnUpHorMiddleToRightFace.IsEnabled = bEnableDisable;
            imgbtnTurnBackFaceToLeft.IsEnabled = bEnableDisable;
            imgbtnTurnLeftFaceToRight.IsEnabled = bEnableDisable;
            imgbtnTurnUpVerMiddleToFrontFace.IsEnabled = bEnableDisable;
            imgbtnTurnRightFaceToLeft.IsEnabled = bEnableDisable;
            imgbtnTurnUpFaceToLeft.IsEnabled = bEnableDisable;
            imgbtnTurnFrontHorMiddleToRightFace.IsEnabled = bEnableDisable;
            imgbtnTurnDownFaceToRight.IsEnabled = bEnableDisable;
            imgbtnTurnUpFaceToRight.IsEnabled = bEnableDisable;
            imgbtnTurnFrontHorMiddleToLeftFace.IsEnabled = bEnableDisable;
            imgbtnTurnDownFaceToLeft.IsEnabled = bEnableDisable;
            imgbtnTurnLeftFaceToLeft.IsEnabled = bEnableDisable;
            imgbtnTurnUpVerMiddleToBackFace.IsEnabled = bEnableDisable;
            imgbtnTurnRightFaceToRight.IsEnabled = bEnableDisable;
            imgbtnTurnFrontFaceToLeft.IsEnabled = bEnableDisable;
            imgbtnTurnUpHorMiddleToLeftFace.IsEnabled = bEnableDisable;
            imgbtnTurnBackFaceToRight.IsEnabled = bEnableDisable;
        }

        /// <summary>
        /// Show license using the Loaded event of the MainPage.xaml
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnPageLoad(object sender, EventArgs e)
        {
            // Show license
            string cLicense = CubeLang.License_Text + "\n\n" + CubeLang.LicenseMit2_Text;

            if (Globals.bLicense == false)
            {
                Globals.bLicense = await Application.Current!.Windows[0].Page!.DisplayAlert(CubeLang.LicenseTitle_Text, cLicense, CubeLang.Agree_Text, CubeLang.Disagree_Text);

                if (Globals.bLicense)
                {
                    Preferences.Default.Set("SettingLicense", true);
                }
                else
                {
#if IOS
                    //Thread.CurrentThread.Abort();  // Not allowed in iOS
                    imgbtnAbout.IsEnabled = false;
                    imgbtnSettings.IsEnabled = false;
                    btnSolveCube.IsEnabled = false;
                    imgbtnSetColorsCube.IsEnabled = false;
                    imgbtnOpenCube.IsEnabled = false;
                    imgbtnSaveCube.IsEnabled = false;
                    imgbtnScrambleCube.IsEnabled = false;
                    imgbtnResetCube.IsEnabled = false;
                    IsEnabledArrows(false);

                    await DisplayAlert(CubeLang.LicenseTitle_Text, CubeLang.CloseApplication_Text, CubeLang.ButtonClose_Text);
#else
                    Microsoft.Maui.Controls.Application.Current.Quit();
#endif
                }
            }
        }

        /// <summary>
        /// Set language using the Appearing event of the MainPage.xaml
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnPageAppearing(object sender, EventArgs e)
        {
            if (Globals.bLanguageChanged)
            {
                _ = SetTextLanguageAsync();
                Globals.bLanguageChanged = false;
            }
        }

        /// <summary>
        /// Put text in the chosen language in the controls
        /// </summary>
        private async Task SetTextLanguageAsync()
        {
            // Set the current UI culture of the selected language
            Globals.SetCultureSelectedLanguage(Globals.cLanguage);

            // Set the global font size
            Globals.SetGlobalFontSize();

            // Set the flow direction of the text elements
            Globals.SetFlowDirection(this);
            Globals.SetFlowDirection(lblExplainTurnCube);

            // Set the text of the controls in the chosen language
            lblCubeOutsideView.Text = CubeLang.CubeOutsideView_Text;

            if (!string.IsNullOrEmpty(cCurrentTurn))
            {
                lblExplainTurnCube.Text = await SetExplainTextAsync(cCurrentTurn);

                if (!bTurnContinuously)
                {
                    // Convert text to speech
                    ExplainTurnCubeSpeech(lblExplainTurnCube.Text);
                }
            }

            if (!bSolvingCube)
            {
                SetArrowTooltips(true);
            }
        }
    }
}
/*
Numbering of cube surfaces for solutions: CFOP, Basic, Daisy, Cross
-------------------------------------------------------------------

    Outside view              Up              Inside view              Back
                     ______ ______ ______                      ______ ______ ______
                   /      /      /      /|                   /|      !      !      |
                 /  36  /  37  /  38  /  |                 /  |  20  !  19  !  18  |
               /______/______/______/ 11 |               / 27 |______!______!______|
             /      /      /      / !    /       Left  / !    |      !      !      |
           /  39  /  40  /  41  /   !  / |           /   !  / |  23  !  22  !  21  |
         /______/______/______/  10 !/ 14|         /  28 !/   |______!______!______|
       /      /      /      / !    /!    /       /  !   /! 30 |      !      !      |
     /  42  /  43  /  44  /   !  /  !  / |     /    ! /  !  / |  26  !  25  !  24  |
   /______/______/______/  9  !/ 13 !/17 |    | 29  ! 31 !/   |______!______!______|
   |      !      !      |    /!    /!    /    |   / !   /!33 /      /      /      /
   |  0   !  1   !  2   |  /  !  /  !  /      | /   ! /  !  /  51  /  52 /  53  /
   |______!______!______|/ 12 !/ 16 !/        | 32  ! 34 !/______/______/______/
   |      !      !      |    /!    /          |   / !   /      /      /      /
   |  3   !  4   !  5   |  /  !  /            | /   ! / 48   / 49   /  50  /
   |______!______!______|/ 15 !/ Right        | 35  !______/______/______/
   |      !      !      |    /                |   /      /      /      /
   |  6   !  7   !  8   |  /                  | /  45  /  46  / 47   /
   |______!______!______|/                    |______/______/______/
           Front                                       Down

                                  Back
                        _________________________
                        |       |       |       |
                        |  26   |  25   |  24   |
                        |_______|_______|_______|
                        |       |       |       |
                        |  23   |  22   |  21   |
                        |_______|_______|_______|
                        |       |       |       |
           Left         |  20   |  19   |  18   |         Right                   Down
________________________|_______|_______|_______|________________________________________________
|       |       |       |       |       |       |       |       |       |       |       |       |
|  33   |  30   |  27   |  36   |  37   |  38   |  11   |  14   |  17   |  53   |  52   |  51   |
|_______|_______|_______|_______|_______|_______|_______|_______|_______|_______|_______|_______|
|       |       |       |       |  Up   |       |       |       |       |       |       |       |
|  34   |  31   |  28   |  39   |  40   |  41   |  10   |  13   |  16   |  50   |  49   |  48   |
|_______|_______|_______|_______|_______|_______|_______|_______|_______|_______|_______|_______|
|       |       |       |       |       |       |       |       |       |       |       |       |
|  35   |  32   |  29   |  42   |  43   |  44   |   9   |  12   |  15   |  47   |  46   |  45   |
|_______|_______|_______|_______|_______|_______|_______|_______|_______|_______|_______|_______|
                        |       |       |       |
                        |   0   |   1   |   2   |
                        |_______|_______|_______|
                        |       |       |       |
                        |   3   |   4   |   5   |
                        |_______|_______|_______|
                        |       |       |       |
                        |   6   |   7   |   8   |
                        |_______|_______|_______|
                                  Front
*/
