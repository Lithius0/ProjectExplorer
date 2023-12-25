using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;


namespace ProjectExplorer.SoundEffects
{
    public class SoundFactory
    {
        private static SoundFactory instance = new SoundFactory();

        public static SoundFactory Instance
        {
            get { return instance; }
        }

        Dictionary<String, SoundEffect> sounds = new Dictionary<String, SoundEffect>();
        SoundEffect currSound = null;
        Song song = null;
        

        public void LoadAllSounds(ContentManager content)
        {
            //Could move this to a separate method later to decide when to play what song
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.2f;
            MediaPlayer.Play(song);

            // Look at in future
            // MUSIC INSTANCE vs SONG
            // soundEffect = load<SounfEffect>
            // musicInstance = soundEffect.createInstance();
            // musicInstance.isLooped() = true;
            // musicInstance.Volume() *= 0.1f;
            // musicInstance.Play();
        }

        public void PlaySound(String sound)
        {
            //sounds.TryGetValue(sound, out currSound);
            //currSound.Play();
        }

        public void PlaySound(String sound, float volume, float pitch, float pan)
        {
            //sounds.TryGetValue(sound, out currSound);
            //currSound.Play(volume, pitch, pan);
        }

        public void PlayMusic()
        {
            //Possible future implementation
        }
    }
}
