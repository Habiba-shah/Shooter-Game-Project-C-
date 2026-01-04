using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media;

namespace GameProjectOop
{
    public class SoundSystem
    {
        private MediaPlayer? _mediaPlayer;
        private string? _tempFilePath;
        private bool _isLooping = false;

        public void Play(Stream resourceStream)
        {
            // For one-off sounds, we still need a temp file for MediaPlayer
            string tempFile = CreateTempFile(resourceStream);

            // Create a new player for each "Play" to allow simultaneous sounds
            // even if the same SoundSystem instance is called repeatedly (like for shots)
            MediaPlayer player = new MediaPlayer();
            player.Open(new Uri(Path.GetFullPath(tempFile)));

            // Clean up temp file when sound ends
            player.MediaEnded += (s, e) =>
            {
                player.Close();
                try { File.Delete(tempFile); } catch { }
            };

            player.Play();
        }

        public void PlayLoop(Stream resourceStream)
        {
            Stop(); // Stop any existing loop/sound

            _tempFilePath = CreateTempFile(resourceStream);
            _mediaPlayer = new MediaPlayer();
            _isLooping = true;

            _mediaPlayer.Open(new Uri(Path.GetFullPath(_tempFilePath)));
            _mediaPlayer.MediaEnded += (s, e) =>
            {
                if (_isLooping && _mediaPlayer != null)
                {
                    _mediaPlayer.Position = TimeSpan.Zero;
                    _mediaPlayer.Play();
                }
            };

            _mediaPlayer.Play();
        }

        public void Stop()
        {
            _isLooping = false;
            if (_mediaPlayer != null)
            {
                _mediaPlayer.Stop();
                _mediaPlayer.Close();
                _mediaPlayer = null;
            }

            if (_tempFilePath != null && File.Exists(_tempFilePath))
            {
                try { File.Delete(_tempFilePath); } catch { }
                _tempFilePath = null;
            }
        }

        private string CreateTempFile(Stream resourceStream)
        {
            string tempFile = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".wav");
            using (FileStream fs = new FileStream(tempFile, FileMode.Create, FileAccess.Write))
            {
                resourceStream.Position = 0;
                resourceStream.CopyTo(fs);
            }
            return tempFile;
        }
    }
}

