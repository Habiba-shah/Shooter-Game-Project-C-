using GameProjectOop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProjectOop.Extensions
{
    public class AmmoPowerUp : PowerUp
    {

        SoundSystem pickupSound = new SoundSystem();
        public int AmmoAmount { get; set; } = 5;

        public override void OnCollision(GameObject other)
        {
            if (other is Player player)
            {
                player.Ammo += AmmoAmount;
                IsActive = false;

                pickupSound.Play(
                    GameProjectOop.Properties.Resources.ammo
                );
            }
        }
    }
}