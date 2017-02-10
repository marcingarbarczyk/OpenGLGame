using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpGL;
using System.Drawing;
using System.Drawing.Imaging;
using Example1.GameObjects;
using System.Media;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Example1.GameObjects
{
    class Game
    {
        public TimeSpan LevelNumberVisible { get; set; }
        public OpenGL ctrl { get; set; } // opengl context
        public float epsilon { get; set; } // for comparing the distance of collision
        public uint[] tex = new uint[255]; // array with textures
        public int life; // Life player
        public float deathFloor { get; set; }
        public float[] checkpoint; // Latest checkpoint
        public float renderDistance;
        public string explosionSound { get; set; }
        public string healthSound { get; set; }
        public string pointSound { get; set; }
        public string playerHitSound { get; set; }
        public string enemyHitSound { get; set; }
        public Label labelPoints { get; set; }
        public Label labelLevel { get; set; }
        public int render;
        public int level;
        public int levelMax { get; set; }
        public int points = 0;
        public bool devMode = false;
        public bool left, right, space = false;
        public Camera camera { get; set; }


        public void DrawHelpfulLines(OpenGL gl)
        {
            if (this.devMode)
            {
                gl.Color(1.0, 1.0, 0);
                gl.Begin(OpenGL.LINES);
                gl.Vertex(0, 0, 0);
                gl.Vertex(0, 0, 500);
                gl.Vertex(0, 0, 0);
                gl.Vertex(500, 0, 0);
                gl.Vertex(0, -500, 0);
                gl.Vertex(0, 500, 0);
                gl.End();
                gl.Color(1.0, 1.0, 1.0);
            }
        }

        public void BasicGameSettings(Player player, Model[] models)
        {
            var fileLines = System.IO.File.ReadAllLines("assets/config/default.data");
            this.camera = new GameObjects.Camera();
            string[] exp, exp2;
            string option, value;

            foreach (var singleLine in fileLines)
            {
                exp = singleLine.Split('=');
                option = exp[0];
                value = exp[1];

                switch(option)
                {
                    case "camera.eye":
                        exp2 = value.Split(';');
                        camera.eyeX = Convert.ToDouble(exp2[0]);
                        camera.eyeY = Convert.ToDouble(exp2[1]);
                        camera.eyeZ = Convert.ToDouble(exp2[2]);
                        break;

                    case "camera.center":
                        exp2 = value.Split(';');
                        camera.centerX = Convert.ToDouble(exp2[0]);
                        camera.centerY = Convert.ToDouble(exp2[1]);
                        camera.centerZ = Convert.ToDouble(exp2[2]);
                        break;

                    case "camera.up":
                        exp2 = value.Split(';');
                        camera.upX = Convert.ToDouble(exp2[0]);
                        camera.upY = Convert.ToDouble(exp2[1]);
                        camera.upZ = Convert.ToDouble(exp2[2]);
                        break;

                    case "game.renderDistance":
                        this.renderDistance = (float)Convert.ToDouble(value);
                        break;

                    case "game.explosionSound":
                        this.explosionSound = value;
                        break;

                    case "game.pointSound":
                        this.pointSound = value;
                        break;

                    case "game.healthSound":
                        this.healthSound = value;
                        break;

                    case "game.deathFloor":
                        this.deathFloor = (float)Convert.ToDouble(value);
                        break;

                    case "game.playerHitSound":
                        this.playerHitSound = value;
                        break;
                    case "game.enemyHitSound":
                        this.enemyHitSound = value;
                        break;

                    case "player.size":
                        exp2 = value.Split(';');
                        player.sizeX = (float)Convert.ToDouble(exp2[0]);
                        player.sizeY = (float)Convert.ToDouble(exp2[1]);
                        player.sizeZ = (float)Convert.ToDouble(exp2[2]);
                        break;

                    case "player.coords":
                        exp2 = value.Split(';');
                        player.x = (float)Convert.ToDouble(exp2[0]);
                        player.y = (float)Convert.ToDouble(exp2[1]);
                        player.z = (float)Convert.ToDouble(exp2[2]);
                        break;

                    case "player.speed":
                        player.speed = (float)Convert.ToDouble(value);
                        break;

                    case "player.jumpMax":
                        player.jumpMax = (float)Convert.ToDouble(value);
                        break;

                    case "player.modelStandard":
                        player.modelStandard = models[Convert.ToInt32(value)];
                        break;

                    case "player.modelJump":
                        player.modelJump = models[Convert.ToInt32(value)];
                        break;

                    case "player.modelFire":
                        player.modelFire = models[Convert.ToInt32(value)];
                        break;

                    case "player.modelWalk1":
                        player.modelWalk[0] = models[Convert.ToInt32(value)];
                        break;

                    case "player.modelWalk2":
                        player.modelWalk[1] = models[Convert.ToInt32(value)];
                        break;

                    case "player.modelTexture":
                        player.modelTexture = Convert.ToUInt32(value);
                        break;

                    case "player.shotSound":
                        player.shotSound = value;
                        break;

                    case "player.jumpSound":
                        player.jumpSound = value;
                        break;
                    case "player.bulletSize":
                        exp2 = value.Split(';');
                        player.bulletSizeX = (float)Convert.ToDouble(exp2[0]);
                        player.bulletSizeY = (float)Convert.ToDouble(exp2[1]);
                        player.bulletSizeZ = (float)Convert.ToDouble(exp2[2]);
                        break;

                    case "player.bulletModel":
                        player.bulletModel = models[Convert.ToInt32(value)];
                        break;

                    case "player.bulletTexture":
                        player.bulletTexture = Convert.ToUInt32(value);
                        break;
                }
            }
        }

        public void HideLevelMessage()
        {
            TimeSpan now = DateTime.Now.TimeOfDay;
            var difference = (now - this.LevelNumberVisible).TotalSeconds;
            if (difference > 2)
            {
                this.labelLevel.Visible = false;
                this.labelPoints.Visible = true;
            }
        }

        public void LoadTexturesAndModels(OpenGL gl, Model[] models)
        {
            var fileLines = System.IO.File.ReadAllLines("assets/config/settings.data");

            gl.GenTextures(255, this.tex);

            foreach (var singleLine in fileLines)
            {
                var lineSplit = singleLine.Split(';');
                var action = lineSplit[0];

                switch (action)
                {
                    case "texture":
                        var textureSRC = lineSplit[1];
                        var textureID = Convert.ToInt32(lineSplit[2]);
                        LoadTexture(textureSRC, textureID, gl);
                        break;
                    case "model":
                        var modelSRC = lineSplit[1];
                        var modelID = Convert.ToInt32(lineSplit[2]);
                        models[modelID] = LoadModel(modelSRC);
                        break;
                }

            }
        }

        public void ChangeLevelText()
        {
            this.labelLevel.Visible = true;
            this.labelPoints.Visible = false;
            this.labelLevel.Text = "Poziom " + (this.level + 1).ToString();
            this.LevelNumberVisible = DateTime.Now.TimeOfDay;
        }

        public void LoadTexture(string fn, int numer, OpenGL gl)
        {
            gl.BindTexture(OpenGL.TEXTURE_2D, tex[numer]);
            Bitmap obrazek1 = new Bitmap(fn);
            Bitmap obrazek2 = new Bitmap(512, 512, PixelFormat.Format24bppRgb);

            Color c;
            for (int y = 0; y < obrazek2.Height; y++)
                for (int x = 0; x < obrazek2.Width; x++)
                {

                    c = obrazek1.GetPixel((int)(((double)x / (double)obrazek2.Width) * obrazek1.Width), (int)(((double)y / (double)obrazek2.Height) * obrazek1.Height));
                    obrazek2.SetPixel(x, y, Color.FromArgb(c.B, c.G, c.R));

                }

            gl.TexImage2D(OpenGL.TEXTURE_2D, 0, 3, obrazek2.Width, obrazek2.Height, 0, OpenGL.RGB, OpenGL.UNSIGNED_BYTE,
            obrazek2.LockBits(new Rectangle(0, 0, obrazek2.Width, obrazek2.Height),
            ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb).Scan0);

            gl.TexParameter(OpenGL.TEXTURE_2D, OpenGL.TEXTURE_MIN_FILTER, OpenGL.LINEAR);
            gl.TexParameter(OpenGL.TEXTURE_2D, OpenGL.TEXTURE_MAG_FILTER, OpenGL.LINEAR);
        }

        public Model LoadModel(string src)
        {
            Model model = new GameObjects.Model();
            string plik1 = "";

            try
            {
                plik1 = System.IO.File.ReadAllText(src);
            }
            catch (Exception ex)
            {
                return null;
            }

            string plik = "";
            foreach (char c in plik1)
                if (c != '.')
                    plik += c;
                else
                    plik += ',';

            string[] linie = plik.Split('\n');

            double x = 0, y = 0, z = 0;
            int ile_v = 0;

            foreach (string linia in linie)
            {
                string[] słowa = linia.Split(' ');

                if (słowa.Length < 2)
                    continue;

                if (słowa[0] == "v")
                {
                    ModelCoords.XYZ xyz = new ModelCoords.XYZ();
                    xyz.X = double.Parse(słowa[1], System.Globalization.NumberStyles.Any);
                    xyz.Y = double.Parse(słowa[2]);
                    xyz.Z = double.Parse(słowa[3]);
                    model.lista_xyz.Add(xyz);

                    x += xyz.X; y += xyz.Y; z += xyz.Z; ile_v++;
                }

                if (słowa[0] == "vt")
                {
                    ModelCoords.UV uv = new ModelCoords.UV();
                    uv.U = double.Parse(słowa[1]);
                    uv.V = double.Parse(słowa[2]);

                    model.lista_uv.Add(uv);
                }

                if (słowa[0] == "vn")
                {
                    ModelCoords.XYZ xyz = new ModelCoords.XYZ();
                    xyz.X = double.Parse(słowa[1]);
                    xyz.Y = double.Parse(słowa[2]);
                    xyz.Z = double.Parse(słowa[3]);
                    model.lista_norm.Add(xyz);
                }

                if (słowa[0] == "f")
                {
                    List<ModelCoords.Vertex> face = new List<ModelCoords.Vertex>();
                    for (int i = 1; i < słowa.Length; i++)
                    {

                        string[] liczba = słowa[i].Split('/');
                        ModelCoords.Vertex v = new ModelCoords.Vertex();
                        v.V = int.Parse(liczba[0]);
                        v.VT = int.Parse(liczba[1]);
                        v.VN = int.Parse(liczba[2]);
                        face.Add(v);
                    }
                    model.lista_f.Add(face);
                }

            }

            return model;
        }

        public void CheckDistance(Player player)
        {
            if (player.x > this.renderDistance * (this.render + 1))
            {
                this.render++;
            }
        }

        public void ShowPoints()
        {
            this.labelPoints.Text = this.points.ToString();
        }

        public void DrawBackground(OpenGL gl, Level level)
        {
            if (this.devMode)
            {
                gl.Color(1.0, 0.0, 0);
                gl.Begin(OpenGL.QUADS);
                gl.Vertex(level.backgroundX, level.backgroundY, level.backgroundZ);
                gl.Vertex(level.backgroundX + level.backgroundSizeX, level.backgroundY, level.backgroundZ);
                gl.Vertex(level.backgroundX + level.backgroundSizeX, level.backgroundY + level.backgroundSizeY, level.backgroundZ);
                gl.Vertex(level.backgroundX, level.backgroundY + level.backgroundSizeY, level.backgroundZ);
                gl.End();
                gl.Color(1.0, 1.0, 1.0);
            }
            else
            {
                gl.Color(1.0f, 1.0f, 1.0f);
                gl.Enable(OpenGL.TEXTURE_2D);
                gl.BindTexture(OpenGL.TEXTURE_2D, level.textureID);
                gl.Begin(OpenGL.QUADS);
                gl.TexCoord(0, 7);
                gl.Vertex(level.backgroundX, level.backgroundY, level.backgroundZ);
                gl.TexCoord(7, 7);
                gl.Vertex(level.backgroundX + level.backgroundSizeX, level.backgroundY, level.backgroundZ);
                gl.TexCoord(7, 0);
                gl.Vertex(level.backgroundX + level.backgroundSizeX, level.backgroundY + level.backgroundSizeY, level.backgroundZ);
                gl.TexCoord(0, 0);
                gl.Vertex(level.backgroundX, level.backgroundY + level.backgroundSizeY, level.backgroundZ);

                gl.End();
                gl.Color(1.0, 1.0, 1.0);
                gl.Disable(OpenGL.TEXTURE_2D);
            }
        }

        private void SetLevelOptions(Level level, string setting, string value, Model[] models)
        {
            string[] exp;

            switch(setting)
            {
                case "end":
                    level.end = (float)Convert.ToDouble(value);
                    break;
                case "music":
                    level.music = value;
                    break;
                case "textureID":
                    level.textureID = Convert.ToUInt32(value);
                    break;
                case "element":
                    exp = value.Split(';');
                    int part = Convert.ToInt32(exp[0]);
                    float x = (float)Convert.ToDouble(exp[1]);
                    float y = (float)Convert.ToDouble(exp[2]);
                    float z = (float)Convert.ToDouble(exp[3]);
                    float sizeX = (float)Convert.ToDouble(exp[4]);
                    float sizeY = (float)Convert.ToDouble(exp[5]);
                    float sizeZ = (float)Convert.ToDouble(exp[6]);
                    int modelStandard = Convert.ToInt32(exp[7]);
                    uint modelTexture = Convert.ToUInt32(exp[8]);

                    Element element = new GameObjects.Element();
                    element.x = x;
                    element.y = y;
                    element.z = z;
                    element.sizeX = sizeX;
                    element.sizeY = sizeY;
                    element.sizeZ = sizeZ;
                    element.modelStandard = models[modelStandard];
                    element.modelTexture = modelTexture;

                                        
                    level.parts[part].elements.Add(element);
                    break;

                case "background":
                    exp = value.Split(';');
                    float bgX = (float)Convert.ToDouble(exp[0]);
                    float bgY = (float)Convert.ToDouble(exp[1]);
                    float bgZ = (float)Convert.ToDouble(exp[2]);
                    float sizebgX = (float)Convert.ToDouble(exp[3]);
                    float sizebgY = (float)Convert.ToDouble(exp[4]);
                    float sizebgZ = (float)Convert.ToDouble(exp[5]);
                    level.backgroundX = bgX;
                    level.backgroundY = bgY;
                    level.backgroundZ = bgZ;
                    level.backgroundSizeX = sizebgX;
                    level.backgroundSizeY = sizebgY;
                    level.backgroundSizeZ = sizebgZ;
                    break;

                case "medkit":
                    exp = value.Split(';');
                    int medkitPart = Convert.ToInt32(exp[0]);
                    float medkitX = (float)Convert.ToDouble(exp[1]);
                    float medkitY = (float)Convert.ToDouble(exp[2]);
                    float medkitZ = (float)Convert.ToDouble(exp[3]);
                    float medkitSizeX = (float)Convert.ToDouble(exp[4]);
                    float medkitSizeY = (float)Convert.ToDouble(exp[5]);
                    float medkitSizeZ = (float)Convert.ToDouble(exp[6]);
                    int medkitModel = Convert.ToInt32(exp[7]);
                    uint medkitModelTexture = Convert.ToUInt32(exp[8]);

                    Medkit medkit = new GameObjects.Medkit();
                    medkit.x = medkitX;
                    medkit.y = medkitY;
                    medkit.z = medkitZ;
                    medkit.sizeX = medkitSizeX;
                    medkit.sizeY = medkitSizeY;
                    medkit.sizeZ = medkitSizeZ;
                    medkit.modelStandard = models[medkitModel];
                    medkit.modelTexture = medkitModelTexture;

                    level.parts[medkitPart].medkits.Add(medkit);
                    break;

                case "point":
                    exp = value.Split(';');
                    int pointPart = Convert.ToInt32(exp[0]);
                    float pointX = (float)Convert.ToDouble(exp[1]);
                    float pointY = (float)Convert.ToDouble(exp[2]);
                    float pointZ = (float)Convert.ToDouble(exp[3]);
                    float pointSizeX = (float)Convert.ToDouble(exp[4]);
                    float pointSizeY = (float)Convert.ToDouble(exp[5]);
                    float pointSizeZ = (float)Convert.ToDouble(exp[6]);
                    int pointModel = Convert.ToInt32(exp[7]);
                    uint pointModelTexture = Convert.ToUInt32(exp[8]);

                    Point point = new GameObjects.Point();
                    point.x = pointX;
                    point.y = pointY;
                    point.z = pointZ;
                    point.sizeX = pointSizeX;
                    point.sizeY = pointSizeY;
                    point.sizeZ = pointSizeZ;
                    point.modelStandard = models[pointModel];
                    point.modelTexture = pointModelTexture;

                    level.parts[pointPart].points.Add(point);
                    break;


                case "enemy":
                    exp = value.Split(';');
                    int enemyPart = Convert.ToInt32(exp[0]);
                    float enemyX = (float)Convert.ToDouble(exp[1]);
                    float enemyY = (float)Convert.ToDouble(exp[2]);
                    float enemyZ = (float)Convert.ToDouble(exp[3]);
                    float enemySizeX = (float)Convert.ToDouble(exp[4]);
                    float enemySizeY = (float)Convert.ToDouble(exp[5]);
                    float enemySizeZ = (float)Convert.ToDouble(exp[6]);
                    int enemyModel = Convert.ToInt32(exp[7]);
                    int enemyModelJump = Convert.ToInt32(exp[8]);
                    int enemyModelWalk1 = Convert.ToInt32(exp[9]);
                    int enemyModelWalk2 = Convert.ToInt32(exp[10]);
                    uint enemyModelTexture = Convert.ToUInt32(exp[11]);
                    bool enemyFly = Convert.ToBoolean(exp[12]);
                    int enemyLife = Convert.ToInt32(exp[13]);
                    float enemyRangeLeft = (float)Convert.ToDouble(exp[14]);
                    float enemyRangeRight = (float)Convert.ToDouble(exp[15]);

                    Enemy enemy = new GameObjects.Enemy();
                    enemy.x = enemyX;
                    enemy.y = enemyY;
                    enemy.z = enemyZ;
                    enemy.sizeX = enemySizeX;
                    enemy.sizeY = enemySizeY;
                    enemy.sizeZ = enemySizeZ;
                    enemy.modelStandard = models[enemyModel];
                    enemy.modelJump = models[enemyModelJump];
                    enemy.modelWalk[0] = models[enemyModelWalk1];
                    enemy.modelWalk[1] = models[enemyModelWalk2];
                    enemy.modelTexture = enemyModelTexture;
                    enemy.fly = enemyFly;
                    enemy.life = enemyLife;
                    enemy.rangeLeft = enemyRangeLeft;
                    enemy.rangeRight = enemyRangeRight;
                    enemy.back = true;


                    level.parts[enemyPart].enemies.Add(enemy);
                    break;

                case "checkpoint":
                    exp = value.Split(';');
                    float checkpointX = (float)Convert.ToDouble(exp[0]);
                    float checkpointY = (float)Convert.ToDouble(exp[1]);
                    float[] checkpoint = new float[2];
                    checkpoint[0] = checkpointX;
                    checkpoint[1] = checkpointY;
                    level.checkpoints.Add(checkpoint);
                    break;
            }
        }

        public void LoadLevels(Level[] levels, Model[] models)
        {
            int levelNumber = 0;
            string settingName, settingValue, line;
            string[] exp;

            foreach (string file in Directory.EnumerateFiles("assets\\config", "level*.data"))
            {
                levelNumber = Convert.ToInt32(Regex.Match(file, @"\d+").Value);
                levels[levelNumber] = new GameObjects.Level();
                this.levelMax++;
                System.IO.StreamReader levelFile = new System.IO.StreamReader(file);

                while ((line = levelFile.ReadLine()) != null)
                {
                    exp = line.Split('=');
                    settingName = exp[0];
                    settingValue = exp[1];
                    SetLevelOptions(levels[levelNumber], settingName, settingValue, models);
                }

                levelFile.Close();
            }
        }

        public void DrawElement(OpenGL gl, Object obj)
        {
            if (this.devMode)
            {
                if (obj.GetType() == typeof(Point))
                    gl.Color(1.0f, 0.0f, 1.0f);
                if (obj.GetType() == typeof(Medkit))
                    gl.Color(0.3f, 0.2f, 1.0f);
                if (obj.GetType() == typeof(Enemy))
                    gl.Color(0.8f, 0.0f, 0.0f);
                if (obj.GetType() == typeof(Bullet))
                    gl.Color(1.0f, 1.0f, 0.0f);
                if (obj.GetType() == typeof(Element))
                    gl.Color(0.0f, 1.0f, 0.0f);
                if (obj.GetType() == typeof(Player))
                    gl.Color(1.0, 1.0, 0.0);

                gl.Begin(OpenGL.QUADS);

                
                    // Top
                    gl.Vertex(obj.x, obj.y + obj.sizeY, obj.z);
                    gl.Vertex(obj.x + obj.sizeX, obj.y + obj.sizeY, obj.z);
                    gl.Vertex(obj.x + obj.sizeX, obj.y + obj.sizeY, obj.z + obj.sizeZ);
                    gl.Vertex(obj.x, obj.y + obj.sizeY, obj.z + obj.sizeZ);

                    // Bottom
                    gl.Vertex(obj.x, obj.y, obj.z);
                    gl.Vertex(obj.x + obj.sizeX, obj.y, obj.z);
                    gl.Vertex(obj.x + obj.sizeX, obj.y, obj.z + obj.sizeZ);
                    gl.Vertex(obj.x, obj.y, obj.z + obj.sizeZ);

                    // Left
                    gl.Vertex(obj.x, obj.y, obj.z);
                    gl.Vertex(obj.x, obj.y + obj.sizeY, obj.z);
                    gl.Vertex(obj.x, obj.y + obj.sizeY, obj.z + obj.sizeZ);
                    gl.Vertex(obj.x, obj.y, obj.z + obj.sizeZ);

                    // Right
                    gl.Vertex(obj.x + obj.sizeX, obj.y, obj.z);
                    gl.Vertex(obj.x + obj.sizeX, obj.y + obj.sizeY, obj.z);
                    gl.Vertex(obj.x + obj.sizeX, obj.y + obj.sizeY, obj.z + obj.sizeZ);
                    gl.Vertex(obj.x + obj.sizeX, obj.y, obj.z + obj.sizeZ);

                    // Front
                    gl.Vertex(obj.x, obj.y, obj.z + obj.sizeZ);
                    gl.Vertex(obj.x + obj.sizeX, obj.y, obj.z + obj.sizeZ);
                    gl.Vertex(obj.x + obj.sizeX, obj.y + obj.sizeY, obj.z + obj.sizeZ);
                    gl.Vertex(obj.x, obj.y + obj.sizeY, obj.z + obj.sizeZ);
                

                gl.End();
            }
            else
            {
                if (obj.modelStandard != null)
                {
                    gl.Enable(OpenGL.TEXTURE_2D);
                    gl.Color(1.0, 1.0, 1.0);
                    gl.BindTexture(OpenGL.TEXTURE_2D, obj.modelTexture);
                    gl.PushMatrix();
                    gl.Translate(obj.x, obj.y, obj.z);

                    if (obj.GetType() == typeof(Point))
                    {
                        gl.Rotate(0, ((Point)obj).rotate, 0);
                        ((Point)obj).rotate += 4.0f;
                    }

                    if (obj.GetType() == typeof(Medkit))
                    {
                        gl.Rotate(0, ((Medkit)obj).rotate, 0);
                        ((Medkit)obj).rotate += 4.0f;
                    }

                    if (obj.GetType() == typeof(Bullet))
                    {
                        var bullet = (Object)obj;
                        if(bullet.back)
                            gl.Rotate(0, 180, 0);
                        
                    }

                    if (obj.GetType() == typeof(Enemy))
                    {
                        if (obj.back == true)
                            gl.Rotate(0, 180, 0);

                        if (!obj.colliderYbottom)
                            obj.modelJump.DisplayModel(gl);
                        else if(obj.isWalking)
                        {
                            var current = DateTime.Now.TimeOfDay;
                            if(obj.walkTime == null)
                                obj.walkTime = DateTime.Now.TimeOfDay;
                            var diffInSeconds = (current - obj.walkTime).TotalSeconds;

                            if (diffInSeconds > 0.5)
                            {
                                obj.modelWalk[1].DisplayModel(gl);

                                if (diffInSeconds > 1)
                                    obj.walkTime = DateTime.Now.TimeOfDay;
                            }
                            else
                            {
                                obj.modelWalk[0].DisplayModel(gl);
                            }
                        }
                        else
                        {
                            obj.modelStandard.DisplayModel(gl);
                        }
                    }
                    else
                        obj.modelStandard.DisplayModel(gl);

                    gl.PopMatrix();
                    gl.Disable(OpenGL.TEXTURE_2D);
                }
            }
        }

        public void DrawElements(OpenGL gl, Level level, int index)
        {
            for (int j = 0; j <= index; j++)
            {
                for (int i = 0; i < level.parts[j].elements.Count; i++)
                {
                    DrawElement(gl, level.parts[j].elements[i]);
                }
            }
        }

        public void CheckEnemyBulletHit(OpenGL gl, Player player, Level level, int render)
        {
            int[] index = new int[50];
            int countIndex = 0;

            for (int i = 0; i < player.bullets.Count; i++)
            {
                for (int j = 0; j <= render; j++)
                {
                    for (int k = 0; k < level.parts[j].enemies.Count; k++)
                    {
                        if (level.parts[j].enemies[k] != null && player.bullets[i] != null)
                        {
                            if (CheckObjectsCollider(player.bullets[i], level.parts[j].enemies[k]))
                            {
                                level.parts[j].enemies[k].life--;
                                index[countIndex] = i;
                                countIndex++;

                                if (level.parts[j].enemies[k].life <= 0)
                                {
                                    level.parts[j].enemies.RemoveAt(k);
                                    this.points += 30;
                                    ShowPoints();

                                    if (this.explosionSound != "none")
                                    {
                                        var playerExplosion = new WMPLib.WindowsMediaPlayer();
                                        playerExplosion.URL = this.explosionSound;
                                        playerExplosion.controls.play();
                                    }
                                }
                                else
                                {
                                    if (this.enemyHitSound != "none")
                                    {
                                        var enemyHit = new WMPLib.WindowsMediaPlayer();
                                        enemyHit.URL = this.enemyHitSound;
                                        enemyHit.controls.play();
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if(countIndex > 0)
                for (int i = 0; i < countIndex; i++)
                    player.bullets.RemoveAt(index[i]);
        }

        public void PlayLevelMusic(object gl, Level level)
        {
            if (level != null)
            {
                if (level.music != "none")
                {
                    using (var soundPlayer = new SoundPlayer(@level.music))
                    {
                        soundPlayer.PlayLooping();
                    }
                }
            }
            else
            {

            }
        }

        public void CheckCheckpoints(OpenGL gl, Player player, Level level)
        {
            for (int i = 0; i < level.checkpoints.Count; i++)
            {
                if (player.x >= level.checkpoints[i][0] && this.checkpoint[0] < level.checkpoints[i][0])
                {
                    this.checkpoint = level.checkpoints[i];
                }
            }
        }

        public void CheckBulletsCollider(OpenGL gl, Player player, Level level, int index)
        {
            int[] bulletHitIndex = new int[50];
            int countIndex = 0;

            for (int i = 0; i <= index; i++)
            {
                for (int j = 0; j < level.parts[i].elements.Count; j++)
                {
                    for (int k = 0; k < player.bullets.Count; k++)
                    {
                        if (CheckObjectsCollider(player.bullets[k], level.parts[i].elements[j]))
                        {
                            bulletHitIndex[countIndex] = k;
                            countIndex++;
                        }
                    }
                }
            }

            if (countIndex > 0)
                for (int l = 0; l <= countIndex; l++)
                    if(player.bullets.Count > 0)
                        player.bullets.RemoveAt(bulletHitIndex[l]);
        }

        public void MoveBullets(OpenGL gl, Player player)
        {
            for (int i = 0; i < player.bullets.Count; i++)
            {
                if (player.bullets[i].back)
                    player.bullets[i].x -= 0.5f;
                else
                    player.bullets[i].x += 0.5f;

                if (player.bullets[i].x - player.x > 12.0f || player.x - player.bullets[i].x > 12.0f || player.bullets[i].colliderXleft || player.bullets[i].colliderXright)
                {
                    player.bullets.RemoveAt(i);
                }
            }
        }

        public void DrawBullets(OpenGL gl, List<Bullet> bullets)
        {
            for (int i = 0; i < bullets.Count; i++)
            {
                DrawElement(gl, bullets[i]);
            }
        }

        public void CheckEnemyAttack(OpenGL gl, Level level, Player player, int index)
        {
            for (int i = 0; i <= index; i++)
            {
                for (int j = 0; j < level.parts[i].enemies.Count; j++)
                {
                    if (CheckObjectsCollider(player, level.parts[i].enemies[j]))
                    {
                        this.Respawn(player);
                    }
                }
            }
        }

        public void CheckEnemiesCollider(OpenGL gl, Level level, int index)
        {
            for (int i = 0; i <= index; i++)
            {
                for (int j = 0; j < level.parts[i].enemies.Count; j++)
                {
                    this.CheckCollider(gl, level.parts[i].enemies[j], level, index);
                }
            }
        }

        public void MoveEnemy(OpenGL gl, Level level, int index)
        {
            for (int i = 0; i <= index; i++)
            {
                for (int j = 0; j < level.parts[i].enemies.Count; j++)
                {
                    if (!level.parts[i].enemies[j].colliderYbottom && !level.parts[i].enemies[j].fly)
                    {
                        level.parts[i].enemies[j].y -= 0.1f;
                        level.parts[i].enemies[j].isWalking = false;
                    }

                    if ((level.parts[i].enemies[j].colliderXleft || level.parts[i].enemies[j].x < level.parts[i].enemies[j].rangeLeft) || (level.parts[i].enemies[j].fly && level.parts[i].enemies[j].x < level.parts[i].enemies[j].rangeLeft))
                        level.parts[i].enemies[j].back = false;

                    if ((level.parts[i].enemies[j].colliderXright || level.parts[i].enemies[j].x > level.parts[i].enemies[j].rangeRight) || (level.parts[i].enemies[j].fly && level.parts[i].enemies[j].x > level.parts[i].enemies[j].rangeRight))
                        level.parts[i].enemies[j].back = true;


                    if ((level.parts[i].enemies[j].back) || (level.parts[i].enemies[j].fly && level.parts[i].enemies[j].back))
                    {
                        level.parts[i].enemies[j].x -= 0.1f;
                        if (!level.parts[i].enemies[j].isWalking)
                            level.parts[i].enemies[j].walkTime = DateTime.Now.TimeOfDay;
                        level.parts[i].enemies[j].isWalking = true;
                    }

                    if (!level.parts[i].enemies[j].back || (level.parts[i].enemies[j].fly && !level.parts[i].enemies[j].back))
                    {
                        level.parts[i].enemies[j].x += 0.1f;
                        if (!level.parts[i].enemies[j].isWalking)
                            level.parts[i].enemies[j].walkTime = DateTime.Now.TimeOfDay;
                        level.parts[i].enemies[j].isWalking = true;
                    }

                    if (level.parts[i].enemies[j].y < this.deathFloor)
                    {
                        level.parts[i].enemies.RemoveAt(j);
                        j++;
                    }


                }
            }
        }

        public void DrawEnemies1(OpenGL gl, Level level, int index)
        {
            for (int i = 0; i <= index; i++)
            {
                for (int j = 0; j < level.parts[i].enemies.Count; j++)
                {
                    DrawElement(gl, level.parts[i].enemies[j]);
                }
            }
        }

        public bool CheckEndLevel(Player player, Level level)
        {
            if (player.x > level.end)
            {
                return true;
            }

            return false;
        }

        public void CheckPointGetting(OpenGL gl, Player player, Level level, int index)
        {
            for (int i = 0; i <= index; i++)
            {
                for (int j = 0; j < level.parts[i].points.Count; j++)
                {
                    if (CheckObjectsCollider(player, level.parts[i].points[j]))
                    {
                        this.points += 50;
                        ShowPoints();
                        level.parts[i].points.RemoveAt(j);

                        if (this.pointSound != "none")
                        {
                            var playerPoint = new WMPLib.WindowsMediaPlayer();
                            playerPoint.URL = this.pointSound;
                            playerPoint.controls.play();
                        }
                    }

                }
            }
        }

        public void DrawPoints(OpenGL gl, Level level, int index)
        {
            for (int i = 0; i <= index; i++)
            {
                for (int j = 0; j < level.parts[i].points.Count; j++)
                {
                    DrawElement(gl, level.parts[i].points[j]);
                }
            }
        }

        public void CheckMedkitGetting(OpenGL gl, Player player, Level level, int index)
        {
            for (int i = 0; i <= index; i++)
            {
                for (int j = 0; j < level.parts[i].medkits.Count; j++)
                {
                    if (CheckObjectsCollider(player, level.parts[i].medkits[j]))
                    {
                        this.life++;
                        this.points += 10;
                        ShowPoints();
                        level.parts[i].medkits.RemoveAt(j);

                        if (this.healthSound != "none")
                        {
                            var playerMedkit = new WMPLib.WindowsMediaPlayer();
                            playerMedkit.URL = this.healthSound;
                            playerMedkit.controls.play();
                        }
                    }

                }
            }
        }

        public bool CheckObjectsCollider(Object objectReal, Object objectStatic)
        {
            if (
                    (objectReal.x + objectReal.sizeX > objectStatic.x && objectReal.x + objectReal.sizeX < objectStatic.x + objectStatic.sizeX) &&
                    (objectReal.y + objectReal.sizeY > objectStatic.y && objectReal.y + objectReal.sizeY < objectStatic.y + objectStatic.sizeY)
                )
                return true;
            else if (
                    (objectReal.x > objectStatic.x && objectReal.x < objectStatic.x + objectStatic.sizeX) &&
                    ((objectReal.y > objectStatic.y && objectReal.y < objectStatic.y + objectStatic.sizeY)
                        
                    )
                )
            {
                return true;
            }
            else if ((objectStatic.x > objectReal.x && objectStatic.x < objectReal.x + objectReal.sizeX) &&
                    (objectReal.y + objectReal.sizeY > objectStatic.y && objectReal.y < objectStatic.y + objectStatic.sizeY)

                )
            {
                return true;
            }
            else
                return false;
        }

        internal void DrawMedkits(OpenGL gl, Level level, int index)
        {
            for (int i = 0; i <= index; i++)
            {
                for (int j = 0; j < level.parts[i].medkits.Count; j++)
                {
                    DrawElement(gl, level.parts[i].medkits[j]);
                }
            }
        }

        public void Draw2DUI(OpenGL gl)
        {
            gl.LoadIdentity();
            gl.Translate(-1.78, 0.95f, -2.5);
            gl.Scale(0.25, 0.25, 1);

            gl.Disable(OpenGL.DEPTH_TEST);
            gl.Disable(OpenGL.TEXTURE_2D);

            gl.Color(0, 0, 0);
            gl.Begin(OpenGL.LINE_LOOP);
            gl.Vertex(-0.0f, 0.01f);
            gl.Vertex(-0.0f, -0.51f);
            gl.Vertex(1.01f + this.life - 1, -0.51f);
            gl.Vertex(1.01f + this.life - 1, 0.01f);
            gl.End();


            for (int i = 0; i < this.life; i++)
            {
                if (this.life >= 3)
                    gl.Color(0.0f, 1.0f, 0.0f);

                if (this.life < 3)
                    gl.Color(1.0f, 0.5f, 0.0f);

                if (this.life == 1)
                    gl.Color(1.0f, 0.0f, 0.0f);

                gl.Begin(OpenGL.QUADS);
                gl.Vertex(0.0f + i, 0.0f);
                gl.Vertex(0.0f + i, -0.5f);
                gl.Vertex(1.0f + i, -0.5f);
                gl.Vertex(1.0f + i, 0.0f);
                gl.End();

                gl.Color(0, 0, 0);
                gl.Begin(OpenGL.LINES);
                gl.Vertex(0.0f + i, 0.0f);
                gl.Vertex(0.0f + i, -0.5f);
                gl.End();
            }

            gl.End();
            gl.Color(1.0f, 1.0f, 1.0f);
        }

        public void DrawPlayer(OpenGL gl, Player player)
        {
            gl.PushMatrix();

            if (player.back)
            {
                if (!this.devMode)
                {
                    gl.Translate(player.x + (player.sizeX / 2), player.y, player.z);
                    gl.Rotate(0, 180, 0);
                }
            }
            else
                if (!this.devMode)
                    gl.Translate(player.x, player.y, player.z);

            if (this.devMode)
                DrawElement(gl, player);
            else
            {
                gl.Enable(OpenGL.TEXTURE_2D);
                gl.BindTexture(OpenGL.TEXTURE_2D, player.modelTexture);
                if ((player.isJumping || !player.colliderYbottom) && !player.isFiring)
                {
                    player.modelJump.DisplayModel(gl);
                }
                else if (player.isFiring)
                {
                    player.modelFire.DisplayModel(gl);
                }
                else if (player.isWalking)
                {
                    var current = DateTime.Now.TimeOfDay;
                    if (player.walkTime == null)
                        player.walkTime = DateTime.Now.TimeOfDay;
                    var diffInSeconds = (current - player.walkTime).TotalSeconds;
                    


                    if (diffInSeconds > 0.2)
                    {
                        player.modelWalk[1].DisplayModel(gl);

                        if(diffInSeconds > 0.4)
                            player.walkTime = DateTime.Now.TimeOfDay;
                    }
                    else
                    {
                        player.modelWalk[0].DisplayModel(gl);
                    }
                }
                else
                {
                    player.modelStandard.DisplayModel(gl);
                }
                gl.Disable(OpenGL.TEXTURE_2D);
            }

            gl.PopMatrix();

        }

        public void MovePlayer(OpenGL gl, Player player)
        {
            if (left && !player.colliderXleft)
            {
                player.x -= player.speed;
                player.back = true;
                camera.eyeX -= player.speed;
                camera.centerX -= player.speed;
            }

            if (right && !player.colliderXright)
            {
                camera.centerX += player.speed;
                player.x += player.speed;
                player.back = false;
                camera.eyeX += player.speed;
            }

            if (player.isJumping)
            {
                if (player.colliderYtop)
                {
                    player.isJumping = false;

                }
                else
                {
                    if (player.y <= player.jumpingLimit)
                        player.y += 0.1f;
                    else
                        player.isJumping = false;
                }
            }

            if (!player.colliderYbottom && !player.isJumping)
            {
                player.y -= 0.1f;
            }

            if (player.y < this.deathFloor)
            {
                this.Respawn(player);
            }
        }

        public void Respawn(Player player)
        {
            double difference = 0;
            this.life--;
            this.points -= 100;
            ShowPoints();
            if (life > 0)
            {
                difference = player.x - camera.eyeX;
                player.x = this.checkpoint[0];
                player.y = this.checkpoint[1];
                camera.eyeX = this.checkpoint[0] - difference;
                camera.centerX = this.checkpoint[0];

                if (this.playerHitSound != "none")
                {
                    var playerHit = new WMPLib.WindowsMediaPlayer();
                    playerHit.URL = this.playerHitSound;
                    playerHit.controls.play();
                }
            }
        }

        public void CheckCollider(OpenGL gl, GameObjects.Object realObject, Level level, int index)
        {
            var element = new Element();
            float distance;

            realObject.colliderXleft = false;
            realObject.colliderXright = false;
            realObject.colliderYbottom = false;
            realObject.colliderYtop = false;

            for (int i = 0; i <= index; i++)
            {
                for (int j = 0; j < level.parts[i].elements.Count; j++)
                {
                    element = level.parts[i].elements[j];

                    // Check bottom collider
                    distance = Math.Abs(realObject.y - (element.y + element.sizeY));
                    if (((realObject.x > element.x && realObject.x < element.x + element.sizeX) || (realObject.x + realObject.sizeX > element.x && realObject.x + realObject.sizeX < element.x + element.sizeX)) && distance < this.epsilon)
                    {
                        realObject.colliderYbottom = true;
                    }

                    // Check right collider
                    distance = Math.Abs(realObject.x + realObject.sizeX - element.x);
                    if (((realObject.y > element.y && realObject.y < element.y + element.sizeY) || (realObject.y + realObject.sizeY > element.y && realObject.y + realObject.sizeY < element.y + element.sizeY)) && distance < this.epsilon)
                    {
                        realObject.colliderXright = true;
                    }

                    // Check left collider
                    distance = Math.Abs(element.x + element.sizeX - realObject.x);
                    if (((realObject.y > element.y && realObject.y < element.y + element.sizeY) || (realObject.y + realObject.sizeY > element.y && realObject.y + realObject.sizeY < element.y + element.sizeY)) && distance < this.epsilon)
                    {
                        realObject.colliderXleft = true;
                    }

                    // Check top collider
                    distance = Math.Abs(element.y - (realObject.y + realObject.sizeY));
                    if (((realObject.x > element.x && realObject.x < element.x + element.sizeX) || (realObject.x + realObject.sizeX > element.x && realObject.x + realObject.sizeX < element.x + element.sizeX)) && distance < this.epsilon)
                    {
                        realObject.colliderYtop = true;
                    }

                }
            }
        }
    }
}