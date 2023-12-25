using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.SpriteUtil
{
    /// <summary>
    /// Sprites which are stickable can be stuck to objects and follow them around.
    /// Thereby reducing some coupling between sprite and object.
    /// </summary>
    public interface IStickable
    {
        public void StickTo(ISticky sticky);
    }
}
