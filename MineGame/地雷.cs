﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MineGame
{
    public enum 区块状态 { 未知, 标记, 安全, 爆炸, 可疑 }
    class 地雷 : System.Windows.Forms.Button, I地雷
    {
        int _在雷场中的行位置;
        public int 在雷场中的行位置
        {
            get { return _在雷场中的行位置; }
            set { _在雷场中的行位置 = value; }
        }
        int _在雷场中的列位置;
        public int 在雷场中的列位置
        {
            get { return _在雷场中的列位置; }
            set { _在雷场中的列位置 = value; }
        }
        static int _长 = 35;
        public static int 高度
        {
            get { return 地雷._长; }
        }
        static int _宽 = 35;
        public static int 长度
        {
            get { return 地雷._宽; }
        }
        Boolean _is地雷;
        public Boolean Is地雷
        {
            get { return _is地雷; }
        }
        static 雷场 _所属雷场;
        public static 雷场 所属雷场
        {
            get { return 地雷._所属雷场; }
        }

        static Boolean _游戏是否结束 = false;
        public static Boolean 游戏是否结束
        {
            get { return 地雷._游戏是否结束; }
            private set { 地雷._游戏是否结束 = value; }
        }

        static int _已打开安全区数量 = 0;
        private int 已打开安全区数量
        {
            set
            {
                _已打开安全区数量++;

                if (_所属雷场.地雷总数 + _已打开安全区数量 == _所属雷场.行数 * _所属雷场.列数)
                    游戏结束(true);
            }
        }
        区块状态 _当前状态;
        public 区块状态 当前状态
        {
            set
            {
                _当前状态 = value;
                显示当前外观(value);

                if (_当前状态 == 区块状态.爆炸)
                    游戏结束(false);
                else if (_当前状态 == 区块状态.安全)
                    已打开安全区数量 = 1;

            }
        }


        public 地雷(Boolean 是否是地雷, 雷场 雷场)
        {
            地雷._所属雷场 = 雷场;
            设置地雷(是否是地雷);
        }
        void 设置地雷(Boolean 是否是地雷)
        {
            _is地雷 = 是否是地雷;
            this.Size = new System.Drawing.Size(_长, _宽);
            this.MouseDown += 处理鼠标被点击的方法;
        }
        void 处理鼠标被点击的方法(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (游戏是否结束)
            {
                return;
            }
            //改变当前方块状态
            switch (e.Button)
            {
                case System.Windows.Forms.MouseButtons.Left:
                    if (_当前状态 == 区块状态.未知)
                        当前状态 = _is地雷 ? 区块状态.爆炸 : 区块状态.安全;
                    break;

                case System.Windows.Forms.MouseButtons.Right:
                    if (_当前状态 == 区块状态.未知)
                        当前状态 = 区块状态.标记;
                    if (_当前状态 == 区块状态.标记)
                        当前状态 = 区块状态.可疑;
                    if (_当前状态 == 区块状态.可疑)
                        当前状态 = 区块状态.未知;
                    break;
                default:
                    break;
            }

        }
        
        void 显示当前外观(区块状态 当前外观)
        {
            System.Drawing.Image img;
            img = Properties.Resources.n;

            #region
            switch (当前外观)
            {
                case 区块状态.安全:
                    显示地雷外观();
                    
                    break;
                case 区块状态.爆炸:
                    img = Properties.Resources.c;
                    break;
                case 区块状态.标记:
                    img = Properties.Resources.f;
                    break;
                case 区块状态.可疑:
                    img = Properties.Resources.q;
                    break;
                default:
                    break;
            }
            #endregion

            this.Image = img;
            //this.Text = 当前外观.ToString();
        }

        void 显示地雷外观()
        {
            System.Drawing.Image img;

            switch (_所属雷场.雷场状态[_在雷场中的行位置, _在雷场中的列位置])
            {
                case 0:
                    img = Properties.Resources._0;
                    break;
                case 1:
                    img = Properties.Resources._1;
                    break;
                case 2:
                    img = Properties.Resources._2;
                    break;
                case 3:
                    img = Properties.Resources._3;
                    break;
                case 4:
                    img = Properties.Resources._4;
                    break;
                case 5:
                    img = Properties.Resources._5;
                    break;
                case 6:
                    img = Properties.Resources._6;
                    break;
                case 7:
                    img = Properties.Resources._7;
                    break;
                case 8:
                    img = Properties.Resources._8;
                    break;
                case -1:
                    img = Properties.Resources.b;
                    break;
                default:
                    img = Properties.Resources.n;
                    break;
            }
            this.Image = img;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="胜利"></param>
        void 游戏结束(Boolean 胜利)
        {
            游戏是否结束 = true;
            if (!胜利)
            {
                引爆所有地雷();
                MessageBox.Show("you are faild！");
            }else
            MessageBox.Show("you are win！");
        }
        void 引爆所有地雷()
        {
            foreach (Object control in _所属雷场.Controls)
            {
                try
                {
                    地雷 _地雷 = (地雷)control;
                    _地雷.显示地雷外观();
                }
                catch (Exception) { ;}
            }
        }
    }


}
