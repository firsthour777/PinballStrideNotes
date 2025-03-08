using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stride.Core.Mathematics;
using Stride.Input;
using Stride.Engine;

namespace PinballStride.Scripts.Skeletons;

public class AsyncScriptSkeleton : AsyncScript
{
   public override async Task Execute()
   {
      StartAsyncStride();

      while (Game.IsRunning)
      {
         UpdateAsyncStride();
         await Script.NextFrame();  // Tells game to move to next frame. If already waiting for next frame, don't do this.
      }
   }

   private async Task StartAsyncStride()
   {

   }


   private async Task UpdateAsyncStride()
   {

   }

   



}

