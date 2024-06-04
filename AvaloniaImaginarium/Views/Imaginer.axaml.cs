using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using CatSAT;
using Imaginarium;
using Imaginarium.Generator;
using Imaginarium.Ontology;
using AvaloniaImaginarium;
using System.Diagnostics;
using System;
using System.IO;
using Imaginarium.Parsing;
using Imaginarium.Driver;
using System.Text;
using System.Reflection.Emit;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using Avalonia.Controls.Shapes;

namespace AvaloniaImaginarium.Views;

public partial class Imaginer : UserControl
{   
    public Ontology OntologyInstance { get; private set; }
    private Parser p;
    public Imaginer()
    {
        InitializeComponent();

        //UserPreferences.SetString("DefinitionsDictionary", "hi");

        //Path.GetFileName()

        Imaginarium.Driver.DataFiles.DataHome = "../../../Assets/";
        // (str name, str DefinitionsDirectory)
        UserPreferences.SetString("DefinitionsDirectory", "");
        //generator = UserPreferences.GetString("DefinitionsDirectory", null);
        OntologyInstance = new Ontology("", "../../../Assets/");
        //OntologyInstance = new Ontology("asdf", generator);
        //p = new Parser(OntologyInstance, parser => ReplCommands.Commands(parser));
    }

    public void ReloadImaginarium(object? sender, RoutedEventArgs e)
    {
        try
        {
            string generator = UserPreferences.GetString("DefinitionsDirectory", null);
            OntologyInstance.DefinitionsDirectory = generator;
            OntologyInstance.Reload();
            History.Text = "";
            History.Text += $"Reloaded: {generator}\n";
        }
        catch (ArgumentException)
        {
            History.Text += "Nothing to reload...\n";
        }

        catch (Exception ex)
        {
            History.Text += ex;
        }
    }

    private void Quit(object? sender, RoutedEventArgs e)
    {
        MainWindow.Instance.Close();
    }

    private async void SelectProjectFolder(object? sender, RoutedEventArgs e)
    {
        var chosen = await MainWindow.Instance.StorageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions
        {
            AllowMultiple = false,
            Title = "Select a project directory",

            SuggestedStartLocation = await MainWindow.Instance.StorageProvider.TryGetFolderFromPathAsync("../../../Assets/")
        });

        if (chosen.Count == 0 || !Directory.Exists(chosen[0].Path.LocalPath)) return;
        string path = chosen[0].Path.LocalPath;
        UserPreferences.SetString("DefinitionsDirectory", path);
        //OntologyInstance = new Ontology("hehehehe", path);
        OntologyInstance.DefinitionsDirectory = path;
        p = new Parser(OntologyInstance, parser => ReplCommands.Commands(parser));

        History.Text += $"Loaded: {path}\n";
        InputBox.IsVisible = true;
    }

    private void InputBox_OnKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key != Key.Return || e.Key != Key.Enter) return;
        if (sender is not TextBox textBox) return;


        if (e.Key == Key.Enter)
        {
            string command = textBox.Text;
            textBox.Text = "";
            History.Text += $"{command}\n";

            try
            {
                p.ParseAndExecute(command);
                var responses = ImaginariumResponses.Responses;

                foreach (var r in responses)
                {
                    History.Text += $"\n{r}";
                }

                responses.Clear();
            }

            // user hits enter without typing cmd
            catch (NullReferenceException) { }

            catch (Imaginarium.Parsing.GrammaticalError)
            {
                History.Text += "Invalid command. Enter 'help' for a list of commands.";
            }


            catch (Exception ex)
            {
                History.Text += $"{ex}";
            }
            // scrolls down 
            History.CaretIndex = int.MaxValue;
            //History.ScrollToLine(History.Text.Split('\n').Length);

        }
    }
}