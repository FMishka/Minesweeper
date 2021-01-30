using System;
using System.Collections.Generic;
using System.Text;

namespace Minesweeper
{
    class Field
    {
        private int height = 8;
        private int width = 8;
        private int countMine = 10;
        private string[,] _field;
        private string[,] gamingField;
        private bool isGaming = false;
        private bool? isWin = null;
        private Difficulty difficulty = Difficulty.Easy;
        public int Height
        {
            get
            {
                return height;
            }
        }
        public int Width
        {
            get
            {
                return width;
            }
        }
        public string[,] _Field
        {
            get
            {
                return _field;
            }
        }
        public string[,] GamingField
        {
            get
            {
                return gamingField;
            }
        }
        public bool IsGaming
        {
            get
            {
                return isGaming;
            }
        }
        public bool? IsWin
        {
            get
            {
                return isWin;
            }
        }
        public Field(Difficulty difficulty)
        {
            Initializing(difficulty);
        }
        public void NewGame(Difficulty difficulty)
        {
            _field = null;
            gamingField = null;
            Initializing(difficulty);
        }
        public void Opening(int height, int width)
        {
            if (gamingField != null)
            {
                if (isWin == null)
                {
                    if (_field[height, width] == null)
                    {
                        EmptinessOpening(height, width);
                    }
                    else if (_field[height, width] == "*")
                    {
                        for (int i = 0; i < _field.GetUpperBound(0) + 1; i++)
                        {
                            for (int j = 0; j < _field.GetUpperBound(1) + 1; j++)
                            {
                                if (_field[i, j] == "*")
                                {
                                    gamingField[i, j] = "*";
                                    isGaming = false;
                                    isWin = false;
                                }
                            }
                        }
                    }
                    else
                    {
                        gamingField[height, width] = _field[height, width];
                    }
                }
                else
                {
                    isGaming = false;
                }
                CheckWin();
            }
            else
            {
                ClearField(_field);
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        try
                        {
                            _field[height + i - 1, width + j - 1] = " ";
                        }
                        catch (IndexOutOfRangeException)
                        {
                        }

                    }
                }
                MineGeneration();
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        try
                        {
                            _field[height + i - 1, width + j - 1] = null;

                        }
                        catch (IndexOutOfRangeException)
                        {
                        }
                    }
                }
                FieldGeneration();
                gamingField = new string[this.height, this.width];
                Opening(height, width);
            }
        }
        private void Initializing(Difficulty difficulty)
        {
            this.difficulty = difficulty;
            SetDifficulty();
            _field = new string[height, width];
            isGaming = true;
            isWin = null;
        }
        private void SetDifficulty()
        {
            if (difficulty == Difficulty.Easy)
            {
                height = 8;
                width = 8;
                countMine = (int)difficulty;
            }
            else if (difficulty == Difficulty.Normal)
            {
                height = 16;
                width = 16;
                countMine = (int)difficulty;
            }
            else if (difficulty == Difficulty.Hard)
            {
                height = 16;
                width = 30;
                countMine = (int)difficulty;
            }
            else
            {
                throw new Exception("Difficulty set error!");
            }
        }
        private void MineGeneration()
        {
            Random setMine = new Random();
            int nowMine = 0;
            while (nowMine < countMine)
            {
                for (int i = 0; i < height; i++)
                {
                    if (nowMine < countMine) // Проверка на количество мин, чтобы не входить в цикл ниже
                    {
                        for (int j = 0; j < width; j++)
                        {
                            if (nowMine < countMine) // Проверка, чтобы не сделать лишнюю мину
                            {
                                if (setMine.Next(1, 10) > 8 && _field[i, j] == null)
                                {
                                    nowMine++;
                                    _field[i, j] = "*";
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
        private void FieldGeneration()
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (_field[i, j] != "*")
                    {
                        int countOfMineNearTheCage = 0;

                        countOfMineNearTheCage += CheckFirstLine(i, j);
                        countOfMineNearTheCage += CheckMiddleLine(i, j);
                        countOfMineNearTheCage += CheckLastLine(i, j);
                        if ((countOfMineNearTheCage != 0))
                        {
                            _field[i, j] = countOfMineNearTheCage.ToString();
                        }
                        else
                        {

                        }
                    }
                }
            }
        }
        private int CheckFirstLine(int height, int width)
        {
            int countOfMineNearTheCage = 0;
            for (int i = 0; i < 3; i++)
            {
                try
                {
                    if (_field[height - 1, width - 1 + i] == "*")
                    {
                        ++countOfMineNearTheCage;
                    }
                }
                catch (IndexOutOfRangeException)
                {

                }

            }
            return countOfMineNearTheCage;
        }
        private int CheckMiddleLine(int height, int width)
        {
            int countOfMineNearTheCage = 0;
            try
            {
                if (_field[height, width - 1] == "*")
                {
                    ++countOfMineNearTheCage;
                }
            }
            catch (IndexOutOfRangeException)
            {

            }
            try
            {
                if (_field[height, width + 1] == "*")
                {
                    ++countOfMineNearTheCage;
                }
            }
            catch (IndexOutOfRangeException)
            {

            }
            return countOfMineNearTheCage;
        }
        private int CheckLastLine(int height, int width)
        {
            int countOfMineNearTheCage = 0;
            for (int i = 0; i < 3; i++)
            {
                try
                {
                    if (_field[height + 1, width - 1 + i] == "*")
                    {
                        ++countOfMineNearTheCage;
                    }
                }
                catch (IndexOutOfRangeException)
                {

                }
            }
            return countOfMineNearTheCage;
        }
        private void ClearField(string[,] field)
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    field[i, j] = null;
                }
            }
        }
        private void EmptinessOpening(int height, int width)
        {

            _field[height, width] = "_";
            gamingField[height, width] = "_";
            PlusChecking(height, width);
            CrossChecking(height, width);

        }
        private void PlusChecking(int height, int width)
        {
            try
            {
                if (_field[height - 1, width] == null)
                {
                    EmptinessOpening(height - 1, width);
                }
                else if (_field[height - 1, width] != "*")
                {
                    gamingField[height - 1, width] = _field[height - 1, width];
                }

            }
            catch (IndexOutOfRangeException)
            {
            }
            try
            {
                if (_field[height + 1, width] == null)
                {
                    EmptinessOpening(height + 1, width);
                }
                else if (_field[height + 1, width] != "*")
                {
                    gamingField[height + 1, width] = _field[height + 1, width];
                }
            }
            catch (IndexOutOfRangeException)
            {
            }

            for (int i = -1; i < 2; i += 2)
            {
                try
                {
                    if (_field[height, width + i] == null)
                    {
                        EmptinessOpening(height, width + i);
                    }
                    else if (_field[height, width + i] != "*")
                    {
                        gamingField[height, width + i] = _field[height, width + i];
                    }
                }
                catch (IndexOutOfRangeException)
                {
                }
            }
        }
        private void CrossChecking(int height, int width)
        {
            for (int i = 0; i <= 2; i += 2)
            {
                for (int j = 0; j <= 2; j += 2)
                {
                    try
                    {
                        if (_field[height + i - 1, width + j - 1] == null)
                        {
                            EmptinessOpening(height + i - 1, width + j - 1);
                        }
                        else if (_field[height + i - 1, width + j - 1] != "*")
                        {
                            gamingField[height + i - 1, width + j - 1] = _field[height + i - 1, width + j - 1];
                        }
                    }
                    catch (IndexOutOfRangeException)
                    {
                    }

                }
            }
        }
        private void CheckWin()
        {
            int countEmptiness = 0;
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    if (gamingField[i, j] == null)
                    {
                        ++countEmptiness;
                    }
                }
            }
            if (countEmptiness == 10)
            {
                isWin = true;
                isGaming = false;
            }
            else if (isWin != false)
            {
                isWin = null;
            }
        }
    }
}
