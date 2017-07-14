$raining = false;
$waterSpouting = false;
$haveRock = false;
$manGotRock = false;
$danceAmount = 15;

function t2dSceneGraph::onLevelLoaded(%this, %scenegraph)
{
   playMusic("bg", 1.0);
   startTimer();
   
   birdOnStone.schedule(4500,dance);
}

function t2dStaticSprite::onLevelLoaded(%this, %sceneGraph)
{

}

function t2dStaticSprite::dance(%this)
{
   $danceAmount = $danceAmount * -1;
   
   %this.setRotation(getRandom() * $danceAmount);
   
   %this.schedule(getRandom(400,500),dance);
}

function clickableItem::onMouseDown(%this, %modifier, %pos, %clicks)
{
   schedule(5000,0,clearMessage);
   
   switch$(%this.getName())
   {      
      case "rainCloud":
         //play sound and show message from dynamic fields
         playSE(%this.clickSoundName, $VOLUME); 
         displayMessage(%this.clickText);
         
         %this.isBeingDragged = true;
         
         //make it rain
         if(%this.makesRain && %this.isAboveWaterTower)
         {
            $raining = true;
            rain.visible = true; 
         }
         
      case "waterTower":
         //play sound and show message from dynamic fields
         playSE(%this.clickSoundName, $VOLUME); 
         displayMessage(%this.clickText);         
         
         if($raining)
         {            
            $waterSpouting = true;
            
            water.setTimerOn(1500); 
         
            water.visible = true;
            
            if(!$manGotRock)
            {
               smallRock.visible = true;
            }
         }
      
      case "shack":
         //play sound and show message from dynamic fields
         playSE(%this.clickSoundName, $VOLUME); 
         displayMessage(%this.clickText);         
         
         if($waterSpouting)
         {
            man.visible = true;
         }
         
      case "man":
         //play sound and show message from dynamic fields
         playSE(%this.clickSoundName, $VOLUME); 
         displayMessage(%this.clickText);         
         
         if($waterSpouting)
         {
            $manGotRock = true;
            smallRock.visible = false;
            if(!$haveRock)
            {
               bigRock.Visible = true;
            }
         }
      
      case "bigRock":
         //play sound and show message from dynamic fields
         playSE(%this.clickSoundName, $VOLUME); 
         displayMessage(%this.clickText);
                     
         $haveRock = true;
         bigRock.Visible = false;  
  
      case "birdOnStone":
         //play sound and show message from dynamic fields
         playSE(%this.clickSoundName, $VOLUME); 
         displayMessage(%this.clickText);         
         
         if($haveRock)
         {
            birdOnStone.visible = false;
            birdInVines.visible = true;
            vineDown.visible = true;
            
            sceneWindow2D.startCameraShake(10, 0.75); 
         }   
         
      case "vineDown":
         //play sound and show message from dynamic fields
         playSE(%this.clickSoundName, $VOLUME); 
         displayMessage(%this.clickText);
         
         //TODO: go back to main menu         
         gameOver();
             
   }
}

function timedObject::onTimer(%this)
{
   echo("Water disappearing");  
   
   $waterSpouting = false;
   man.visible = false;
   water.visible = false;
   smallRock.visible = false;
}

function displayMessage(%message)
{
   GuiMessage.text = %message;
}

function clearMessage()
{
   GuiMessage.text = "";
}

function gameOver()
{
   quit();  
}