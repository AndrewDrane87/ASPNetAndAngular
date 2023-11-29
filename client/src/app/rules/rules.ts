export interface Rule {
  header: string;
  text: string;
}

export class Rules {
  challenge: Rule = { header:"Challenge's",text:"Role a d100 and add on the displayed modifier. If the end value is less than you're current skill value for the category, you pass." };
}
