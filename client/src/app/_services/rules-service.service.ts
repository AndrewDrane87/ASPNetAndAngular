import { Injectable } from '@angular/core';
import { Rule } from '../rules/rules';

@Injectable({
  providedIn: 'root',
})
export class RulesServiceService {
  classes: Rule = {
    header: 'Classes',
    text: 'In this game, your classes and skills are based on the items you equip instead of being tied to your character. This means you can flex as you progress through the adventure. Equipment comes with modifiers which will add or subtract from your skill values.',
  };

stats: Rule ={
  header: 'Stats',
  text: 'There are 5 main stats in this game which reflect how good your character is at certain tasks. For instance a fighter is probably good with weapons is strong but is poor at magical tasks or knowing thing\'s, while a mage will be better at magic, knowing arcane lore etc. These stats will be tied to challenges throughout your adventures.'
}

  challenge: Rule = {
    header: "Challenge's",
    text: 'The game uses a role under system. You role a d100 and if the value on the dice is less than your displayed skill value, the challenge is a pass. Sometimes challenges will have a modifer. In this case, you roll your dice and add the modifier before comparing it to your skill value.',
  };

gettingStarted: Rule ={
  header: 'Gettings started',
  text: 'To get started, one person must create an adventure. After that each person playing will create a character and add it to the adventure. This can be done manually using a paper character sheet, but it will be preferred for everyone to use the browser based character sheet, as it will handle item tracking and all the tedious calculations for you. Once these are done, just click play and choose your way through the adventure.'
}

infoBox: Rule ={
  header: 'Finding help',
  text: 'Look for the info box in popups and around the navigation screens. They will help guide you through all aspects of the game.'
}

  constructor() {}
}
