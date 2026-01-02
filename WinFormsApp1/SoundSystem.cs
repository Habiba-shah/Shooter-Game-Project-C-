using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace GameProjectOop
{
    public class SoundSystem
    {
        SoundPlayer player;
        MemoryStream savedStream;

        public void Play(Stream resourceStream)
        {
            MemoryStream ms = new MemoryStream();
            resourceStream.Position = 0;
            resourceStream.CopyTo(ms);
            ms.Position = 0;

            player = new SoundPlayer(ms);
            player.Play();
        }

        public void PlayLoop(Stream resourceStream)
        {
            player?.Stop(); // Purana stop
            player?.Dispose();
            MemoryStream ms = new MemoryStream();
            resourceStream.Position = 0;
            resourceStream.CopyTo(ms);
            ms.Position = 0;

            player = new SoundPlayer(ms);
            player.Load();
            player.PlayLooping();

        }

        public void Stop()
        {
            if (player != null)
                player.Stop();
        }
    }
}

