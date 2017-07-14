
function Cloud::onMouseDragged(%this, %modifier, %worldPos, %mouseClicks)
{        
   if(%this.getName() $= "rainCloud" && %this.isBeingDragged && !%this.isAboveWaterTower)
   {  
      %this.setPosition(%worldPos); 
   }
}

function Cloud::onAddToScene(%this)
{
   //%this.enableUpdateCallback();
   %this.setBlending(true);
   
   %this.doAcid();
}

function Cloud::onUpdate(%this)
{ 

}

function Cloud::onMouseUp(%this, %modifier, %pos, %clicks)
{     
   %this.isBeingDragged = false;
   
   if(getWord(%pos,0) > 25 && getWord(%pos,0) < 45 && getWord(%pos,1) < -15 && getWord(%pos,1) > -40)
   {
      if(%this.makesRain)
      {
         %this.isAboveWaterTower = 1;
      }
   }
}

function Cloud::doAcid(%this)
{
   %this.setBlendColor(getRandom(), getRandom(), getRandom(), 1.0);
   
   if(!%this.isAboveWaterTower)
   {
      %this.schedule(getRandom(3200,5400),doAcid);
   }
}