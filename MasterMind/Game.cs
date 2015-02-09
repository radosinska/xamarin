using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Android.Graphics.Drawables;
using Android.Graphics;

namespace MasterMind
{
    [Activity(Label = "Game")]
    public class Game : Activity, View.IOnDragListener, View.IOnLongClickListener
    {
        #region Pola klasy

        Button checkButton;
        TextView textScore;

        List<LinearLayout> DraggableElements = new List<LinearLayout>();    //Tablica przechowuj¹ca ruchome 'diamenty'

        LinearLayout[,] DropFields = new LinearLayout[8, 4];                //Tablica przechowuj¹ca pola planszy
        LinearLayout[,] Score = new LinearLayout[8, 4];                     //Tablica przechowuj¹ca pola do umieszczania wyniku
        
        int[] Puzzle = new int[4];                                          //Tablica przechowuj¹ca wylosowan¹ kombinacje 'diamentów'

        int[,] Board = new int[8, 4];                                       //Tablica przechowuj¹ca umieszczone 'diamenty' na planszy
        
        int level;                                                          //Zmienna zapamiêtuj¹ca level
        int hard;                                                           //Zmienna przechowuj¹ca wybrany przez uzytkownika poziom trudnoœci

        double points;                                                      //Zmienna przechowuj¹ca zdobyte punkty

        #endregion

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.LayoutGame);

            checkButton = FindViewById<Button>(Resource.Id.CheckButton);
            textScore = FindViewById<TextView>(Resource.Id.TextScore);

            #region Umieszczenie LinearLayout'ów w tablicach

            //Umieszczenie 'diamentów' na liœcie DraggableElements 
            DraggableElements.Add(FindViewById<LinearLayout>(Resource.Id.C101));
            DraggableElements.Add(FindViewById<LinearLayout>(Resource.Id.C102));
            DraggableElements.Add(FindViewById<LinearLayout>(Resource.Id.C103));
            DraggableElements.Add(FindViewById<LinearLayout>(Resource.Id.C104));
            DraggableElements.Add(FindViewById<LinearLayout>(Resource.Id.C105));

            //Umieszczenie pól planszy w tablicy DropFields
            DropFields[0, 0] = FindViewById<LinearLayout>(Resource.Id.F91);
            DropFields[0, 1] = FindViewById<LinearLayout>(Resource.Id.F92);
            DropFields[0, 2] = FindViewById<LinearLayout>(Resource.Id.F93);
            DropFields[0, 3] = FindViewById<LinearLayout>(Resource.Id.F94);

            DropFields[1, 0] = FindViewById<LinearLayout>(Resource.Id.F81);
            DropFields[1, 1] = FindViewById<LinearLayout>(Resource.Id.F82);
            DropFields[1, 2] = FindViewById<LinearLayout>(Resource.Id.F83);
            DropFields[1, 3] = FindViewById<LinearLayout>(Resource.Id.F84);

            DropFields[2, 0] = FindViewById<LinearLayout>(Resource.Id.F71);
            DropFields[2, 1] = FindViewById<LinearLayout>(Resource.Id.F72);
            DropFields[2, 2] = FindViewById<LinearLayout>(Resource.Id.F73);
            DropFields[2, 3] = FindViewById<LinearLayout>(Resource.Id.F74);

            DropFields[3, 0] = FindViewById<LinearLayout>(Resource.Id.F61);
            DropFields[3, 1] = FindViewById<LinearLayout>(Resource.Id.F62);
            DropFields[3, 2] = FindViewById<LinearLayout>(Resource.Id.F63);
            DropFields[3, 3] = FindViewById<LinearLayout>(Resource.Id.F64);

            DropFields[4, 0] = FindViewById<LinearLayout>(Resource.Id.F51);
            DropFields[4, 1] = FindViewById<LinearLayout>(Resource.Id.F52);
            DropFields[4, 2] = FindViewById<LinearLayout>(Resource.Id.F53);
            DropFields[4, 3] = FindViewById<LinearLayout>(Resource.Id.F54);

            DropFields[5, 0] = FindViewById<LinearLayout>(Resource.Id.F41);
            DropFields[5, 1] = FindViewById<LinearLayout>(Resource.Id.F42);
            DropFields[5, 2] = FindViewById<LinearLayout>(Resource.Id.F43);
            DropFields[5, 3] = FindViewById<LinearLayout>(Resource.Id.F44);

            DropFields[6, 0] = FindViewById<LinearLayout>(Resource.Id.F31);
            DropFields[6, 1] = FindViewById<LinearLayout>(Resource.Id.F32);
            DropFields[6, 2] = FindViewById<LinearLayout>(Resource.Id.F33);
            DropFields[6, 3] = FindViewById<LinearLayout>(Resource.Id.F34);

            DropFields[7, 0] = FindViewById<LinearLayout>(Resource.Id.F21);
            DropFields[7, 1] = FindViewById<LinearLayout>(Resource.Id.F22);
            DropFields[7, 2] = FindViewById<LinearLayout>(Resource.Id.F23);
            DropFields[7, 3] = FindViewById<LinearLayout>(Resource.Id.F24);

            //Umieszczenie pól wyników w tablicy score
            Score[0, 0] = FindViewById<LinearLayout>(Resource.Id.F951);
            Score[0, 1] = FindViewById<LinearLayout>(Resource.Id.F952);
            Score[0, 2] = FindViewById<LinearLayout>(Resource.Id.F953);
            Score[0, 3] = FindViewById<LinearLayout>(Resource.Id.F954);

            Score[1, 0] = FindViewById<LinearLayout>(Resource.Id.F851);
            Score[1, 1] = FindViewById<LinearLayout>(Resource.Id.F852);
            Score[1, 2] = FindViewById<LinearLayout>(Resource.Id.F853);
            Score[1, 3] = FindViewById<LinearLayout>(Resource.Id.F854);

            Score[2, 0] = FindViewById<LinearLayout>(Resource.Id.F751);
            Score[2, 1] = FindViewById<LinearLayout>(Resource.Id.F752);
            Score[2, 2] = FindViewById<LinearLayout>(Resource.Id.F753);
            Score[2, 3] = FindViewById<LinearLayout>(Resource.Id.F754);

            Score[3, 0] = FindViewById<LinearLayout>(Resource.Id.F651);
            Score[3, 1] = FindViewById<LinearLayout>(Resource.Id.F652);
            Score[3, 2] = FindViewById<LinearLayout>(Resource.Id.F653);
            Score[3, 3] = FindViewById<LinearLayout>(Resource.Id.F654);

            Score[4, 0] = FindViewById<LinearLayout>(Resource.Id.F551);
            Score[4, 1] = FindViewById<LinearLayout>(Resource.Id.F552);
            Score[4, 2] = FindViewById<LinearLayout>(Resource.Id.F553);
            Score[4, 3] = FindViewById<LinearLayout>(Resource.Id.F554);

            Score[5, 0] = FindViewById<LinearLayout>(Resource.Id.F451);
            Score[5, 1] = FindViewById<LinearLayout>(Resource.Id.F452);
            Score[5, 2] = FindViewById<LinearLayout>(Resource.Id.F453);
            Score[5, 3] = FindViewById<LinearLayout>(Resource.Id.F454);


            Score[6, 0] = FindViewById<LinearLayout>(Resource.Id.F351);
            Score[6, 1] = FindViewById<LinearLayout>(Resource.Id.F352);
            Score[6, 2] = FindViewById<LinearLayout>(Resource.Id.F353);
            Score[6, 3] = FindViewById<LinearLayout>(Resource.Id.F354);

            Score[7, 0] = FindViewById<LinearLayout>(Resource.Id.F251);
            Score[7, 1] = FindViewById<LinearLayout>(Resource.Id.F252);
            Score[7, 2] = FindViewById<LinearLayout>(Resource.Id.F253);
            Score[7, 3] = FindViewById<LinearLayout>(Resource.Id.F254);


            #endregion

            string tmp = Intent.GetStringExtra("Level");
            hard = (tmp == "hard") ? 1 : 0;

            //Przypisanie ClickHandler'a do przycisku; i wy³¹czenie akcji
            checkButton.Click += ClickHandler;
            checkButton.Clickable = false;

            //Przypisanie LongListenera do 'diamentów'
            foreach (var circle in DraggableElements)
                circle.SetOnLongClickListener(this);


            if (bundle != null)
            {
                
                GameState state = (MasterMind.GameState)bundle.GetSerializable("data");

                level = state.Level;

                Puzzle = state.Puzzle;

                points = state.Points;
                textScore.Text = points.ToString();

                for(int i=0;i<level+1;i++)
                    for(int j=0;j<4;j++)
                    {
                        Board[i, j] = state.Board[i, j];
                        DropFields[i, j].Background = state.Fields[i, j].Background;
                        Score[i, j].Background = state.Score[i, j].Background;
                    }

                bool f = false;
                f = CheckFill(level);
                if (f == true)
                {
                    //W³¹czenie akcji dla przycisku 'sprawdŸ'
                    checkButton.Clickable = true;
                    checkButton.SetBackgroundColor(Color.ParseColor(GetString(Resource.Color.MyGreen)));
                }

            }
            else
            {
                //Level: 0
                level = 0;
                points = 0.0;
                textScore.Text = points.ToString();

                //Losowanie liczby 1-5 (koloru) i wpisanie do tablicy puzzle 
                //                    - stworzenie kombinacji do odgadniêcia
                Random rand = new Random();
                for (int i = 0; i < 4; i++)
                {
                    Puzzle[i] = rand.Next(1, 6);
                }
            }

            //W³¹czenie DragListenera dla pól danego levelu
            for (int j = 0; j < 4; j++)
                DropFields[level, j].SetOnDragListener(this);
        }

        protected override void OnSaveInstanceState(Bundle savedInstanceState)
        {
            //Zapisywanie stanu gry 
            GameState state = new GameState(DropFields, Score, Board, Puzzle, level, points);

            savedInstanceState.PutSerializable("data", state);

            base.OnSaveInstanceState(savedInstanceState);
        }

        public bool OnLongClick(View v)
        {
            DragShadow dragShadow = new DragShadow(v,(v.Tag).ToString());
            ClipData data = ClipData.NewPlainText("", "");
            v.StartDrag(data, dragShadow, v, 0);
            return false;
        }

        public bool OnDrag(View v, DragEvent e)
        {
            var dragEvent = e.Action;
            switch (dragEvent)
            {
                case DragAction.Started:
                    break;
                case DragAction.Entered:
                    break;
                case DragAction.Exited:
                    break;
                case DragAction.Drop:
                    
                    LinearLayout target = (LinearLayout)v;
                    LinearLayout dragged = (LinearLayout)e.LocalState;
                    
                    target.SetBackgroundDrawable(dragged.Background);

                    string tmp1=(target.Tag).ToString();
                    string tmp2=(dragged.Tag).ToString();

                    int j=Convert.ToInt16(tmp1);    //Druga wspó³rzêdna dla Board
                    int a=Convert.ToInt16(tmp2);    //Wartoœæ do wpisania - kolor
                    Board[level, j] = a;

                    //Sprawdzenie czy wszystkie pola s¹ zape³nione
                    bool f = false;
                    f=CheckFill(level);
                    if(f==true)
                    {
                        //W³¹czenie akcji dla przycisku 'sprawdŸ'
                        checkButton.Clickable = true;
                        checkButton.SetBackgroundColor(Color.ParseColor(GetString(Resource.Color.MyGreen)));
                    }
                    break;
                case DragAction.Ended:
                    break;
            }
            return true;
        }

        private void ClickHandler(object sender, EventArgs e)
        {
            //Czynnoœci wykonywane po klikniêciu w przycisk 'SprawdŸ'

            //Porównanie kombinacji wprowadzonej z wylosowan¹:

            //zmienne zliczaj¹ce 'diamenty' na swojej pozycji (black)
            //i na z³ej pozycji, ale znajduj¹ce siê w kombinacji wylosowanej (white)
            int black = 0;
            int white = 0;

            //Wype³nienia do rysowania "wyniku"
            int BlackRect = Resource.Drawable.BlackRect;
            int WhiteRect = Resource.Drawable.WhiteRect;

            //Porównywananie - wyszukanie kolorów na swoim i nie na swoim miejscu
            for (int j = 0; j < 4; j++ )
            {
                if (Puzzle[j] == Board[level, j])
                {
                    black++;
                    Board[level, j] = 0; //zamazanie nr pionka

                }
                else
                {
                    for (int k=0; k<4;k++)
                    {
                        if(Puzzle[j] == Board[level, k])
                        {
                            white++;
                            Board[level, k] = 0;
                            break;
                        }
                    }
                }
            }

            points += (14 - 2*level) * (black + 0.5 * white);
            textScore.Text = points.ToString();

            int a = 0;
            
            if (black != 0)
            {
                for (int i = 0; i < black; i++)
                {
                    Score[level, a].SetBackgroundResource(BlackRect);
                    a++;
                }

            }

            if (black == 4 || level==7)
            {
                //OBS£UGA ZAKOÑCZENIA GRY

                string message = "";
                if(black==4)
                {
                    message = GetString(Resource.String.WinGame);
                }
                else
                {
                    message = GetString(Resource.String.LoseGame);
                }

                message += " ";
                message+= GetString(Resource.String.Score);

                string tmp = points.ToString();
                message += " ";
                message += tmp;

                EndGame(message);
            }
            else
            {
                //Narysowanie wyniku - bia³e
                if (white != 0)
                {
                    for (int i = 0; i < white; i++)
                    {
                        Score[level, a].SetBackgroundResource(WhiteRect);
                        a++;
                    }

                }
                            
                //Jeœli wybrany poziom: hard - zamalowanie wyniku
                if(level>0 && level%2==0)
                {
                    int GreyCircle = Resource.Drawable.GreyCircle;
                    for(int i=0; i<4;i++)
                    {
                        DropFields[level - 1, i].SetBackgroundResource(GreyCircle);
                        DropFields[level - 2, i].SetBackgroundResource(GreyCircle);
                    }
                }
                
                
                //PRZEJSCIE NA KOLEJNY ETAP
                //Wy³¹czenie DragListenera dla pól sprawdzonego levelu
                for (int j = 0; j < 4; j++)
                    DropFields[level, j].SetOnDragListener(null);
                level += 1;
                //W³¹czenie DragListenera dla pól danego levelu
                for (int j = 0; j < 4; j++)
                    DropFields[level, j].SetOnDragListener(this);

                //Dezaktywacja przycisku sprawdŸ
                checkButton.Clickable = false;
                checkButton.SetBackgroundColor(Color.Gray);
            }

        }

        private void ClickHandlerBack(object sender, DialogClickEventArgs e)
        {
            this.StartActivity(typeof(MainActivity));
        }

        private void EndGame(string message)
        {
            //Metoda do wywo³ania alertu na koniec gry

            AlertDialog.Builder builder1 = new AlertDialog.Builder(this);
            builder1.SetMessage(message);
            builder1.SetCancelable(false);

            String message2 = GetString(Resource.String.Back);

            builder1.SetPositiveButton(message2, ClickHandlerBack);

            AlertDialog alert11 = builder1.Create();
            alert11.Show();
        }

        private bool CheckFill(int level)
        {
            //Metoda sprawdzaj¹ca czy wszystkie pola etapu zosta³y uzupe³nione
            bool result = true;
            for(int j=0; j<4;j++)
            {
                if (Board[level, j] == 0)
                    result = false;
            }
            return result;
        }

    }
    
}