using Microsoft.Expression.Controls;
using System;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace AFD
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Storyboard storyboard = new Storyboard();
        DoubleAnimation da = new DoubleAnimation();        
        state cPos = state.sq0;
        int secs = 0;

        enum state
        {
            sq0,sq1,sq2,sq3
        }        

        public MainWindow()
        {
            InitializeComponent();
            storyboard.Completed += Storyboard_Completed;
        }

        private async void btnIniciar_Click(object sender, RoutedEventArgs e)
        {
            string str = txtCadena.Text.ToString().Trim();
            

            if (ValidLang(str))
            {
                char[] chars = str.ToCharArray();
                
                for (int i = 0; i < chars.Length; i++)
                {
                    if (i==0)
                    {                        
                       MarkStep(state.sq0, chars[i]);
                        await Task.Delay(TimeSpan.FromSeconds(4));
                    }
                    else
                    {                                                       
                        MarkStep(cPos, chars[i]);
                        await Task.Delay(TimeSpan.FromSeconds(secs * 2));
                    }                    
                }                
            }
            else
            {
                MessageBox.Show("El lenguaje ingresado no es válido!");
            }            
        }

        private async void MarkStep(state s,char c)
        {             
            if (s == state.sq0)
            {
                if (c.Equals('a'))
                {
                    cPos = state.sq1;
                    txtLetter.Text = c.ToString();
                    AnimateControl(q0, Shape.StrokeThicknessProperty);
                    await Task.Delay(TimeSpan.FromSeconds(2));
                    AnimateControl(q0Line, CompositeShape.StrokeThicknessProperty);
                    await Task.Delay(TimeSpan.FromSeconds(2));
                    AnimateControl(q1, Shape.StrokeThicknessProperty);

                    secs = 3;
                }
                if (c.Equals('b') || c.Equals('c'))
                {
                    cPos = state.sq0;
                    txtLetter.Text = c.ToString();
                    AnimateControl(q0, Shape.StrokeThicknessProperty);
                    await Task.Delay(TimeSpan.FromSeconds(2));
                    AnimateControlReturn(q0Return, HeightProperty);

                    secs = 2;
                }
               
            }
            if (s == state.sq1)
            {
                if (c.Equals('a'))
                {
                    cPos = state.sq1;
                    txtLetter.Text = c.ToString();
                    AnimateControl(q1, Shape.StrokeThicknessProperty);
                    await Task.Delay(TimeSpan.FromSeconds(2));
                    AnimateControlReturn(q1Return, HeightProperty);
                }
                if (c.Equals('b'))
                {
                    cPos = state.sq2;
                    txtLetter.Text = c.ToString();
                    AnimateControl(q1, Shape.StrokeThicknessProperty);
                    await Task.Delay(TimeSpan.FromSeconds(2));
                    AnimateControl(q1Lineb, CompositeShape.StrokeThicknessProperty);
                    
                    
                }
                secs = 2;
            }
        }

        private Storyboard AnimateControl(FrameworkElement fe,object animation)
        {
            da.From = 1;
            da.To = 10;
            da.Duration = new Duration(TimeSpan.FromSeconds(1));
            da.AutoReverse = true;


            storyboard.Children.Add(da);
            Storyboard.SetTargetName(da, fe.Name);
            Storyboard.SetTargetProperty(da, new PropertyPath(animation));//"Effect.Opacity"

            storyboard.Begin(fe);

            return storyboard;
        }

        private void Storyboard_Completed(object sender, EventArgs e)
        {
            //txtLetter.Text = ".";
        }

        private void AnimateControlReturn(FrameworkElement fe, object animation)
        {
            da.From = fe.ActualHeight;
            da.To = 20;
            da.Duration = new Duration(TimeSpan.FromSeconds(1));
            da.AutoReverse = true;


            storyboard.Children.Add(da);
            Storyboard.SetTargetName(da, fe.Name);
            Storyboard.SetTargetProperty(da, new PropertyPath(animation));//"Effect.Opacity"

            storyboard.Begin(fe);
        }

        private bool ValidLang(string str)
        {
            foreach (var item in str.ToCharArray())
            {               
                if (!item.Equals('a') && !item.Equals('b') && !item.Equals('c'))
                {
                    return false;
                }
            }
            
            return true;
        }
    }
}
