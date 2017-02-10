using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;

namespace Example1.GameObjects
{
    class Player : Object
    {
        public float speed { get; set; }
        public float weight { get; set; }

        public bool isJumping { get; set; }
        public bool isFiring { get; set; }
        public float jumpingLimit { get; set; }
        public float jumpMax { get; set; }

        public TimeSpan shotTime { get; set; }
        public List<Bullet> bullets = new List<Bullet>();

        public string shotSound  { get; set; }
        public string jumpSound { get; set; }

        public float bulletSizeX { get; set; }
        public float bulletSizeY { get; set; }
        public float bulletSizeZ { get; set; }
        public Model bulletModel { get; set; }
        public uint bulletTexture { get; set; }

        public Player()
        {
            this.modelWalk = new Model[2];
        }

        public void Shot()
        {
            var current = DateTime.Now.TimeOfDay;

            if(shotTime == null)
            {
                bullets.Add(new GameObjects.Bullet
                {
                    x = x,
                    y = y,
                    sizeX = bulletSizeX,
                    sizeY = bulletSizeY,
                    sizeZ = bulletSizeZ,
                    modelTexture = bulletTexture,
                    modelStandard = bulletModel,
                    back = back
                });

                this.shotTime = DateTime.Now.TimeOfDay;
            }
            else
            {
                var diffInSeconds = (current - this.shotTime).TotalSeconds;

                if(diffInSeconds > 0.3)
                {
                    bullets.Add(new GameObjects.Bullet
                    {
                        x = this.x,
                        y = this.y + (this.sizeY / 2),
                        sizeX = bulletSizeX,
                        sizeY = bulletSizeY,
                        sizeZ = bulletSizeZ,
                        modelTexture = bulletTexture,
                        modelStandard = bulletModel,
                        back = this.back
                    });

                    this.shotTime = DateTime.Now.TimeOfDay;

                    if (this.shotSound != "none")
                    {
                        var player = new WMPLib.WindowsMediaPlayer();
                        player.URL = this.shotSound;
                        player.controls.play();
                    }

                }
            }
        }


    }
}
