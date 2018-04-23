using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace utsuge
{
    class student
    {
        public student(string n)
        {
            this.name = n;
            this.loveliness = 0;
        }

        private string name;
        public string Name
        {
            set { this.name = value; }
            get { return this.name; }
        }

        private float loveliness;
        public float Loveliness
        {
            set { this.loveliness = value; }
            get { return this.loveliness; }
        }
    }

    public partial class MainPage : ContentPage
    {
        private student[] mStudents;
        private List<student>[] mPlace;
        private int mCurrentPlace = -1;
        private int mLuck = 1;

        public MainPage()
        {
            InitializeComponent();

            // 生徒生成
            mStudents = new student[5];
            mStudents[0] = new student("巣鴨睦月");
            mStudents[1] = new student("高田望美");
            mStudents[2] = new student("田町まひる");
            mStudents[3] = new student("目黒御幸");
            mStudents[4] = new student("上野こより");

            // 場所生成
            mPlace = new List<student>[4];
            for (int i = 0; i < 4; i++)
            {
                mPlace[i] = new List<student> { };
            }

            initPlace();
        }

        void initPlace()
        {
            System.Random r = new System.Random();

            // 配置
            foreach (var p in mPlace)
            {
                p.Clear();
            }
            foreach (var s in mStudents)
            {
                mPlace[r.Next(4)].Add(s);
            }
        }

        void placeChange(int id)
        {
            Label label;
            ProgressBar prog;
            StackLayout slay;

            /// 初期化
            slayout.Children.Clear();

            // 更新
            if (mCurrentPlace == id)
            {
                foreach (var s in mPlace[id])
                {
                    s.Loveliness += (float)(5.0 / mPlace[id].Count) * (float)mLuck;
                }
                mCurrentPlace = -1;
                if (mPlace[id].Count > 0) {
                    msgLabel.Text = "一緒にいた生徒の親愛度が" + (float)(5.0 / mPlace[id].Count) * (float)mLuck + "UP!";
                }
                else
                {
                    Random r = new Random();
                    if (r.Next(2) == 0)
                    {
                        msgLabel.Text = "大森となえに会いに行った。LUCK UP↑";
                        mLuck++;
                    }
                    else
                    {
                        msgLabel.Text = "高島瀬美奈に出くわした。LUCK DOWN↓";
                        mLuck--;
                    }
                }
            }
            else
            {
                mCurrentPlace = id;
                msgLabel.Text = "";
            }

            /// 表示
            foreach (var s in mPlace[id])
            {
                slay = new StackLayout { Orientation = StackOrientation.Horizontal, HorizontalOptions = LayoutOptions.Center };
                label = new Label { FontSize = 14, Text = s.Name, TextColor = Color.White, Font = Font.OfSize("ＭＳ 明朝", NamedSize.Medium), HorizontalOptions = LayoutOptions.Start, VerticalOptions = LayoutOptions.Center, WidthRequest = 130 };
                prog = new ProgressBar { HeightRequest = 10, WidthRequest = 100, BackgroundColor = Color.White, Progress = s.Loveliness / 100 };    

                slay.Children.Add(label);
                slay.Children.Add(prog);
                slayout.Children.Add(slay);
            }

            // 場所更新
            if (mCurrentPlace == -1) initPlace();
        }

        void place1(object sender, EventArgs e)
        {
            placeChange(0);
        }
        void place2(object sender, EventArgs e)
        {
            placeChange(1);
        }
        void place3(object sender, EventArgs e)
        {
            placeChange(2);
        }
        void place4(object sender, EventArgs e)
        {
            placeChange(3);
        }
    }
}
